//$(document).ready(function () {
//    $("input[type=submit][id$=Code]").bind('click', function (e) {
//        alert("click");
//        return false;
//    });
//    $("#btnCopy").bind('click', function (e) {
//        alert("btnCopyclick");
//        return false;
//    });
//});

function copyCode() {
    //alert("copyCode"); disabled="disabled"
    //alert($("#btnEdit").attr("disabled"));
    if ($("#btnEdit").attr("disabled") != null) {
        var r = window.showModalDialog('CopyCode.aspx', null, 'dialogHeight:230px;dialogWidth:190px;toolbar=no,status=yes,resizable=no');
        if (r) {
            for (var i = 0; i < r.length; i++) {
                $("#txt" + r[i]).val($("#txtOrderCode").val());
                $("#txt" + r[i] + "View").val($("#txtOrderCode").val().split(' ')[0]);
            } 
        }
    }
    return false;
}

function autoCode() {
    //alert("autoCode");
    if ($("#btnEdit").attr("disabled") != null) {
        var obj = new Object();
        var t = $("#txt" + window.event.srcElement.name.substring(3));
//        obj.val = "K-YYYYMMDDXXXX 1"
        obj.val = t.val();

        var r = window.showModalDialog('AutoCode.aspx', obj, 'dialogHeight:230px;dialogWidth:420px;toolbar=no,status=yes,resizable=no');
        //alert(r.Code);
        if (r) {
            t.val(r.Code);
            $("#" + t[0].id + "View").val(r.Code.split(' ')[0]);
        }
        //t.attr("Code", "dd");
//        return true;
    } 
    return false;
}