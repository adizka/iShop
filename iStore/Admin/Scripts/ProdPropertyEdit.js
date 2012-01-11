var delimeter = "!~!!~!";

function PackVals(name, val, sortInd) {
    return delimeter + delimeter + name + delimeter + val + delimeter + sortInd;
}



function AssignEvents() {
    $('.DeleteButton').unbind('click');
    $(".DeleteButton").each(function () { $(this).click(function () { return DeleteProperty(this); }); });
    $('.EditButton').unbind('click');
    $(".EditButton").each(function () { $(this).click(function () { return EditProperty(this); }); });
    $('.MoveDown').unbind('click');
    $(".MoveDown").each(function () { $(this).click(function () { return MoveDown(this); }); });
    $('.MoveUp').unbind('click');
    $(".MoveUp").each(function () { $(this).click(function () { return MoveUp(this); }); });
}
function UpdateData(el) {
    var parent = $(el).parent().parent();
    var newName = ($("#EditName", parent).val());
    var newVal = ($("#EditVal", parent).val());



    var isSuchNameExist = false;
    var counter = 0;
    $(".ProductPropertyItem").each(
            function () {
                counter++;
                if ($.trim(newName) == ($.trim($(".PropertyName", this).html())) && $(this).has("input").length == 0)
                    isSuchNameExist = true;
            });

    if (isSuchNameExist) {
        $("#EditErrorMsg").css("display", "");
        return;
    }
    $("#EditErrorMsg").css("display", "none");

    var oldName = ($(".PropertyName", parent).html());
    $(".PropertyName", parent).html(newName)
    var oldVal = ($(".PropertyValue", parent).html());
    var index = ($(".SortIndex", parent).html()) / 1;
    $(".PropertyValue", parent).html(newVal);
    if (newName.length == 0 || newVal.length == 0)
        return;

    var toRemove = PackVals(oldName, oldVal, index);
    var newData = PackVals(newName, newVal, index);

    parent.children().css("display", "");

    $(el).parent().remove();
    UpdateStoredValues();
}

function EditProperty(el) {
    var parent = $(el).parent();

    parent.children().css("display", "none");
    var template = $("#EditPropertyTemplate").clone();
    template.attr("id", "");
    $("#EditName", template).val($(".PropertyName", parent).html());
    $("#EditVal", template).val($(".PropertyValue", parent).html());


    $(template).css("display", "");
    $("#ChangeID", template).click(function () { UpdateData(this) });
    parent.append(template);
}


function DeleteProperty(el) {
    var parent = $(el).parent();
    var elVal = $(".SortIndex", parent).html() / 1;
    $(".DeleteButton").each(
        function () {
            var container = $(this).parent();

            var ind = $(".SortIndex", container);
            if (elVal < ind.html() / 1)
                ind.html(ind.html() / 1 - 1);
        });
        parent.remove();
        UpdateStoredValues();
    return false;
}



function AddProperty() {
    if ($("#PropName").val().length == 0 || $("#ValName").val().length == 0)
        return;

    var isSuchNameExist = false;
    var counter = 0;
    $(".ProductPropertyItem").each(
            function () {
                counter++;
                if ($.trim($("#PropName").val()) == $.trim($(".PropertyName", this).html()))
                    isSuchNameExist = true;
            });

    if (isSuchNameExist) {
        $("#WarningMsg").css("display", "");
        return;
    }
    $("#WarningMsg").css("display", "none");

    var clone = $(".ProductPropertyItemTemplate").clone();
    clone.addClass("ProductPropertyItem").removeClass("ProductPropertyItemTemplate");

    $(".PropertyName", clone).html(($("#PropName").val()));
    $(".PropertyValue", clone).html(($("#ValName").val()));
    $(".SortIndex", clone).html(counter);
    $(clone).css("display", "");
    $("#ProductPropertyContainer").append(clone);
    AssignEvents();
    $("#AddFormID").css('display', 'none');
    $("#AddButton").css('display', '');
    UpdateStoredValues();
}

function ShowInputForm() {
    $("#AddFormID").css('display', '');
    $("#AddButton").css('display', 'none');
    $("input", "#AddFormID").val("");
}

function MoveUp(el) {

    var parent = $(el).parent();
    var sortIndex = $(".SortIndex", parent).html() / 1;
    if (sortIndex == 0)
        return;
    var upperEl;

    var totalProps = 0;
    $(".ProductPropertyItem").each(
            function () {
                totalProps++;
                if ($(".SortIndex", this).html() / 1 + 1 == sortIndex)
                    upperEl = this;
            });

    if ($(upperEl).has("input").lenght != 0) {
        $(upperEl).children().css("display", "");
        $("input", upperEl).parent().parent().remove();
    }
    var uName = $(".PropertyName", upperEl).html();
    var uVal = $(".PropertyValue", upperEl).html();
    var lName = $(".PropertyName", parent).html();
    var lVal = $(".PropertyValue", parent).html();

    $(".PropertyName", upperEl).html(lName);
    $(".PropertyValue", upperEl).html(lVal);
    $(".PropertyName", parent).html(uName);
    $(".PropertyValue", parent).html(uVal);

    UpdateStoredValues();
}
function MoveDown(el) {
    var parent = $(el).parent();
    var sortIndex = $(".SortIndex", parent).html() / 1;

    var lowerEl;

    var totalProps = 0;
    $(".ProductPropertyItem").each(
            function () {
                totalProps++;
                if ($(".SortIndex", this).html() / 1 - 1 == sortIndex)
                    lowerEl = this;
            });

    if (sortIndex == totalProps - 1)
        return;
    if ($(lowerEl).has("input").lenght != 0) {
        $(lowerEl).children().css("display", "");
        $("input", lowerEl).parent().parent().remove();
    }
    MoveUp($(".MoveUp", lowerEl));
}
function UpdateStoredValues() {
    var val = "";
    $(".ProductPropertyItem").each(
        function () {
            val += PackVals($(".PropertyName", this).html(), $(".PropertyValue", this).html(), $(".SortIndex", this).html());
        });
    $("#hf").val(val);
}