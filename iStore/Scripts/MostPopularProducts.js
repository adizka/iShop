//MostPopular Scripts For iShop 2011
//Authors Adiz Abdullaev

//Первые 4 элементы видны
$(document).ready(function () {
    for (var i = 0; i < 4; i++) {
        $("#li" + i.toString()).css("display", "block");
    }
});

//Влево
function SlideToLeft() {
    var max = $(".HiddenRight").html();
    if (parseInt(max) < 4) { return; }
    var current = $(".HiddenLeft").html();
    if (current == "0") { return; }
    var currentItemLeft = parseInt(current) - 1;
    $(".HiddenLeft").html(currentItemLeft.toString());
    $("#li" + currentItemLeft.toString()).toggle();
    $("#li" + (currentItemLeft + 4).toString()).toggle();
}

//Вправо
function SlideToRight() {
    var max = $(".HiddenRight").html();
    if (parseInt(max) < 4) { return; }
    var current = $(".HiddenLeft").html();
    if (current == parseInt(max - 4).toString()) { return; }
    var currentItemLeft = parseInt(current) + 1;
    $(".HiddenLeft").html(currentItemLeft.toString());
    $("#li" + current).toggle();
    $("#li" + (currentItemLeft + 3).toString()).toggle();
}