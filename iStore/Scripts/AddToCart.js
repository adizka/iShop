function Incr(el, cont) {

    var count = $(".ProdCount" ,$(el).parent()).html() / 1;

    if (count == 1)
        $($(el).parent().children()[0]).css("cursor", "pointer")

    $(".ProdCount", $(el).parent()).html(count + 1);
    $("#" + cont).val(count + 1);
}
function Decr(el, cont) {

    var count =$(".ProdCount" ,$(el).parent()).html() / 1;

    if (count == 1) {
        $(el).css("cursor", "");
        return;
    }
    else {
        $(el).css("cursor", "pointer");
    }

    $(".ProdCount" ,$(el).parent()).html(count - 1);
    $("#" + cont).val(count - 1);
}