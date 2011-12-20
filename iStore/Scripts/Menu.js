//Menu Scripts For iShop 2011
//Authors Adiz Abdullaev


$(document).ready(function () {

    //Подменю
   $(".MenuHoverFirst").hover(function () {
        var ids = $(this).attr("id");
        $("#div" + ids).show();
    }, function () {
        var ids = $(this).attr("id");
        $("#div" + ids).hide();
    });

    $(".MenuItemSecond").hover(function () {
        $(this).show();
    }, function () {
        $(this).hide();
    });

    //ПодПод меню
    $(".MenuHoverSecond").hover(function () {
        var ids = $(this).attr("id");
        $("#div" + ids).show();

    }, function () {
        var ids = $(this).attr("id");
        $("#div" + ids).hide();
    });

    $(".MenuItemThird").hover(function () {
        $(this).show();
    }, function () {
        $(this).hide();
    });
});