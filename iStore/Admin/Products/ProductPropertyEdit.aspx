<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductPropertyEdit.aspx.cs"
    Inherits="iStore.Admin.Products.ProductPropertyEdit" MasterPageFile="~/Admin/Admin.Master" %>

<%@ Register TagPrefix="iS" TagName="ValidateErrors" Src="~/Modules/Controls/Validators/ValidateErrors.ascx" %>
<asp:content id="Content1" runat="server" contentplaceholderid="head">
<script type="text/javascript" src="../../Scripts/jquery.fancybox-2.0/jquery.easing-1.3.pack.js"></script>
<script type="text/javascript" src="../../Scripts/jquery.fancybox-2.0/jquery.mousewheel-3.0.6.pack.js"></script>
<script type="text/javascript" src="../../Scripts/jquery.fancybox-2.0/jquery.fancybox.pack.js"></script>
<link rel="Stylesheet" type="text/css" href="../../Scripts/jquery.fancybox-2.0/jquery.fancybox.css" media="screen" />
<style type="text/css">
    
</style>
<script type="text/javascript">

</script>
</asp:content>
<asp:content id="Content2" runat="server" contentplaceholderid="main">
<asp:ScriptManager runat="server" ID="sm"></asp:ScriptManager>
<asp:UpdatePanel runat="server" ID="up">
<ContentTemplate>
<script type="text/javascript">
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
        var newName =( $("#EditName", parent).val());
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
        var index = ($(".SortIndex", parent).html())/1;
        $(".PropertyValue", parent).html(newVal);
        if (newName.length == 0 || newVal.length == 0)
            return;

        var toRemove = PackVals(oldName, oldVal, index);
        var newData = PackVals(newName, newVal, index);

        parent.children().css("display", "");

        $("#hf").val($("#hf").val().replace(toRemove, newData));
        $(el).parent().remove();
    }
    
    function EditProperty(el) {
        var parent = $(el).parent();

        parent.children().css("display", "none");
        var template = $("#EditPropertyTemplate").clone();
        template.attr("id","");
        $("#EditName", template).val($(".PropertyName", parent).html());
        $("#EditVal", template).val($(".PropertyValue", parent).html());


        $(template).css("display", "");
        $("#ChangeID", template).click(function () { UpdateData(this) });
        parent.append(template);
    }


    function DeleteProperty(el) {
        var parent = $(el).parent();
        var elVal = $(".SortIndex", parent).html()/1;
        $(".DeleteButton").each(
        function () {
            var container = $(this).parent();

            var ind = $(".SortIndex", container);
            if (elVal < ind.html()/1)
                ind.html(ind.html()/1 - 1);
        });
        var toRemove = PackVals($(".PropertyName", parent).html(), $(".PropertyValue", parent).html(), $(".SortIndex", parent).html());
        $("#hf").val($("#hf").val().replace(toRemove, ""));
        parent.remove();
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
        $(clone).css("display","");
        $("#ProductPropertyContainer").append(clone);
        AssignEvents();
        $("#AddFormID").css('display', 'none');
        $("#AddButton").css('display', '');
        $("#hf").val($("#hf").val() + PackVals(($("#PropName").val()), ($("#ValName").val()), counter));
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

        $("#hf").val($("#hf").val().replace(PackVals(uName, uVal, sortIndex - 1), PackVals(uName, uVal, sortIndex)));
        $("#hf").val($("#hf").val().replace(PackVals(lName, lVal, sortIndex), PackVals(lName, lVal, sortIndex - 1)));

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

</script>


<div style="font-size:30px;text-align:center;">
<%=Product.Name %>
</div>
    <div id="AddNewPropContainer">
       <a id="AddButton" href="javascript:ShowInputForm();">Add New Property</a>
        <div id="AddFormID" style="display:none">
            <span>
                <input type="text" id="PropName" />
            </span><span>
                <input type="text" id="ValName" />
            </span>
            <span id="AddID" style="cursor:pointer;color:Blue;" onclick="AddProperty()">Add</span>
        </div>
        <div id="WarningMsg" style="display:none;">Такое свойство уже существует.</div>
    </div>

    <div id="ProductPropertyContainer">
        <%
            int sortIndex = -1;
            foreach (var item in ProductsProperies)
              
          {
              sortIndex++; %>
        <div class="ProductPropertyItem">
                <span class="MoveUp" style="cursor:pointer;color:Blue;">MoveUp</span>
                <span class="MoveDown" style="cursor:pointer;color:Blue;">MoveDown</span>
                <span class="PropertyName"><%=item.PropertyName %></span>
                <span class="PropertyValue"><%=item.PropertyValue %></span>
                <span class="SortIndex"><%=sortIndex.ToString()%></span>
                <span class="DeleteButton" style="cursor:pointer;color:Blue;">Del</span>
                <span class="EditButton" style="cursor:pointer;color:Blue;">Edit</span>
        </div>
        <%} %>
    </div>
    <div class="ProductPropertyItemTemplate" style="display:none;">
                <span class="MoveUp" style="cursor:pointer;color:Blue;">MoveUp</span>
                <span class="MoveDown" style="cursor:pointer;color:Blue;">MoveDown</span>
                <span class="PropertyName"></span>
                <span class="PropertyValue"></span>
                <span class="SortIndex"></span>
                <span class="DeleteButton" style="cursor:pointer;color:Blue;">Del</span>
                <span class="EditButton" style="cursor:pointer;color:Blue;">Edit</span>
    </div>
    <div id="EditPropertyTemplate" style="display: none;z-index:100;height:100%;width:100%;">
        <span>
            <input type="text" id="EditName" />
        </span><span>
            <input type="text" id="EditVal" />
        </span><span id="ChangeID" style="cursor: pointer; color: Blue;">Change </span>
        <div id="EditErrorMsg" style="display:none;">Такое свойство уже существует.</div>
    </div>


    <p class="ProductEdit_Save">
        <asp:Button runat="server" ID="btnSave" Text="Save"  OnClick="Save" />
    </p>
    <asp:HiddenField runat="server" ID="hf" ClientIDMode="Static" />
    <script type="text/javascript">
        AssignEvents();
        var val = "";
        $(".ProductPropertyItem").each(
        function () {
            val += PackVals($(".PropertyName", this).html(), $(".PropertyValue", this).html(), $(".SortIndex", this).html());
        });
        $("#hf").val(val);
    </script>
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
