$(document).ready(function () {
    //Подменю
    $(".MenuHoverFirst").hover(function () {
        var ids = $(this).attr("id");
        $("#div" + ids).addClass("Hover");
    }, function () {
        var ids = $(this).attr("id");
        $("#div" + ids).removeClass("Hover");
    });

    $(".MenuItemSecond").hover(function () {
        $(this).addClass("Hover");
    }, function () {
        $(this).removeClass("Hover");
    });

    //ПодПод меню
    $(".MenuHoverSecond").hover(function () {
        var ids = $(this).attr("id");
        $("#div" + ids).addClass("Hover");

    }, function () {
        var ids = $(this).attr("id");
        $("#div" + ids).removeClass("Hover");
    });

    $(".MenuItemThird").hover(function () {
        $(this).addClass("Hover");
    }, function () {
        $(this).removeClass("Hover");
    });
});