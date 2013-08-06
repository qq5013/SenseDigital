var ZipNo = Array();
jQuery(document).ready(function () {
    var dp1 = $("#ddlCity");
    var dp2 = $("#ddlDistrict");
    var dp3 = $("#ddlTown");
    //
    loadAreas(0, 0, "ddlCity");
    //给省绑定事件，触发事件后填充市的数据
    $(dp1).bind("change keyup", function () {
        var cityID = dp1.attr("value");
        loadAreas(1, cityID, "ddlDistrict");
        $("#txtCodeTmp").val("");
        $("#ddlTown").html("");
        changeAddress();
        dp2.fadeIn("slow");
    });
    //给市绑定事件，触发事件后填充区的数据
    $(dp2).bind("change keyup", function () {
        var districtID = dp2.attr("value");
        $("#txtCodeTmp").val(ZipNo[dp2.get(0).selectedIndex - 1]);
        loadAreas(2, districtID, "ddlTown");
        changeAddress();
        dp3.fadeIn("slow");
    });
    //给市绑定事件，触发事件后填充区的数据
    $(dp3).bind("change keyup", function () {
        changeAddress();
    });
});
function changeAddress(itemText) {
    var dp1 = $("#ddlCity");
    var dp2 = $("#ddlDistrict");
    var dp3 = $("#ddlTown");
    if (itemText == null) {
        $("#txtAddress").val("");
        if (dp1.find("option:selected").text() != "--Select--")
            document.getElementById("txtAddress").value += dp1.find("option:selected").text();
        if (dp2.find("option:selected").text() != "--Select--")
            document.getElementById("txtAddress").value += dp2.find("option:selected").text();
        if (dp3.find("option:selected").text() != "--Select--")
            document.getElementById("txtAddress").value += dp3.find("option:selected").text();
    }
    else
    {
        document.getElementById("txtAddress").value += itemText;
    }
    
}
function getBack() {
    window.parent.returnValue = document.getElementById('txtAddress').value + "@#$" + document.getElementById('txtCodeTmp').value;
    window.parent.close();
}
function clearAddress() {
    document.getElementById("txtAddress").value = "";
    return false;
}
function closeAddress() {
    //window.parent.returnValue = document.getElementById('txtAddress').value + "@#$" + document.getElementById('txtCodeTmp').value;
    window.parent.close();
}
function loadAreas(level, val, item) {
    $.ajax({
        async: false,
        type: "Post",
        url: "WebService/BaseService.asmx/LoadAddress",
        data: "{'level': '" + level + "','strNo':'" + val + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        error: function () {
            return false;
        },
        success: function (data) {
            //debugger;
            //alert(data.d);
            //                    if (item == "ddlDistrict") 
            //                        ZipNo = Array();
            data = jQuery.parseJSON(data.d);
            $("#" + item).html("");
            $("#" + item).append("<option value='' selected='selected'>--Select--</option>");
            for (var i = 0; i < data.length; i++) {
                if (item == "ddlDistrict") {
                    ZipNo[i] = data[i].ZipNo;
                }
                $("#" + item).append(jQuery("<option></option>").val(data[i].SerNo).html(data[i].Name));
            };
        }
    });
}
