$(document).ready(function () {
    $(window).resize(content_resize);
    //content_resize();
});
function content_resize() {
    

    //編輯頁面 div高度
    var div = $("#surdiv");
    var h = 300;
    if ($(window).height() <= 0) {
        h = document.body.clientHeight - 23;
    }
    else {
        h = $(window).height() - 23;
    }
    $("#surdiv").css("height", h);

    var grid = $("#GridView1");
    var h = 300;
    if ($(window).height() <= 0) {
        if (grid.offset() != null)
            h = document.body.clientHeight - grid.offset().top - 22;
        else
            h = document.body.clientHeight - 22;
    }
    else {
        if (grid.offset() != null)
            h = $(window).height() - grid.offset().top - 22;
        else
            h = document.body.clientHeight - 5;
    }
    $("#table-container").css("height", h);
}
function content_resize2() {
    var grid = $("#GridView1");
    var h = 300;
    if ($(window).height() <= 0) {
        if (grid.offset() != null)
            h = document.body.clientHeight - grid.offset().top;
        else
            h = document.body.clientHeight;
    }
    else {
        if (grid.offset() != null)
            h = $(window).height() - grid.offset().top;
        else
            h = document.body.clientHeight;
    }
    $("#table-container").css("height", h);
}
function treeview_resize() {

    var h = 300;    if ($(window).height() <= 0) {        h = document.body.clientHeight - $("#toptable")[0].offsetHeight + 130;    }    else {        h = document.body.clientHeight - $("#toptable")[0].offsetHeight + 130;    }    $("#TreeView1").css("height", h);    var ht = h - 160;    $("#table-container").css("height", ht);
}
function treeview_resize2() {

    var h = 300;
    if ($(window).height() <= 0) {
        h = document.body.clientHeight - 30;
    }
    else {
        h = document.body.clientHeight - 30;
    }
    $("#TreeView1").css("height", h);
    $("#table-container").css("height", h);
}