function JsDDL_changetitle(ddlId, textBoxId) {    
    var drp = document.all(ddlId);
    var t = document.all(textBoxId);
    var j = 0;
    if (t.value == "") {
    }
    else {
        for (var i = 0; i < drp.length; i++) {
            if (t.value == drp.options(i).text) {
                drp.value = drp.options(i).value;
                break;
            }
            else {
                j = j + 1;
            }
        }
        if (j == drp.length) {
            var tOption = document.createElement("Option");
            tOption.text = t.value;
            tOption.value = t.value;
            drp.add(tOption);
        }
        drp.value = t.value;
    }
}

jQuery.fn.extend({
    DDLDataSoure: function (r) {
        this[0].children[0].id
        //    var r = ["1測試", "2測試", "3測試", "4測試"];
        var drp = this[0].children[0];
        drp.innerHTML = "";
        var tOption = document.createElement("Option");
        tOption.text = "";
        tOption.value = "";
        drp.add(tOption);
        for (var i = 0; i < r.length; i++) {
            var tOption = document.createElement("Option");
            tOption.text = r[i];
            tOption.value = r[i];
            drp.add(tOption);
        }
    }
});
//js綁定DDLDataSoure
//$(document).ready(function () {
//    $("#txtDeliverCountry").DDLDataSoure(["1測試", "2測試", "3測試", "4測試"]);
//});