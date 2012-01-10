function Incr(parentEl, cont) {

    var count = $(".ProdCount", $("#" + parentEl)).html() / 1;

    if (count == 1)
        $("#dec", $("#" + parentEl)).css("cursor", "pointer")

    $("#cCont", $("#" + parentEl)).html(count + 1);
    $("#" + cont, $("#" + parentEl)).val(count + 1);
}
function Decr(parentEl, cont) {

    var count = $("#cCont", $("#" + parentEl)).html() / 1;

    if (count == 1) {
        $("#dec", $("#" + parentEl)).css("cursor", "");
        return;
    }
    else {
        $("#dec", $("#" + parentEl)).css("cursor", "pointer");
    }

    $("#cCont", $("#" + parentEl)).html(count - 1);

    $("#" + cont, $("#" + parentEl)).val(count - 1);
}