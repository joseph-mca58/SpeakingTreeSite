$(document).ready(function () {
    //apply class to selected menu item
    var currentId;
    $('.menu').on('click', 'li', function () {
        currentId = $(this).attr("id");
        $('.menu li').removeClass('current-menu-item');
        $("#" + currentId).addClass('current-menu-item');
    });
});