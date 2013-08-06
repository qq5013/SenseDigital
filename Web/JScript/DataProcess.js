function setFocus() {
    //    var obj = event.srcElement;
    //    var txt = obj.createTextRange();

    //    obj.value = obj.value.replace(',', '');
    //    txt.moveStart('character', obj.value.length);
    //    txt.collapse(true);
    //    txt.select();
}  
function GetOtherValue(Sysid, PermissionID, objName,fldName,strWhere) {
    var strReturn = showdialog(Sysid, PermissionID, strWhere);
    //alert(strReturn);
    if (strReturn != null) {
        //alert(strReturn);
        var fieldname = strReturn[0].split('@|$');
        var fieldvalue = strReturn[1].split('@|$');
        var obj = objName.split(',');
        var fld = fldName.split(',');
        for (var i = 0; i < obj.length; i++) {

            if (fld.length > 0)
                //document.getElementById(obj[i]).value = getfieldtext(fieldname, fieldvalue, fld[i]);
                $("input[id$=" + obj[i] + "]").val(getfieldtext(fieldname, fieldvalue, fld[i]));
            else
                $("input[id$=" + obj[i] + "]").val(getfieldtext(fieldname, fieldvalue, obj[i]));
                //document.getElementById(obj[i]).value = getfieldtext(fieldname, fieldvalue, obj[i]);
        }
    }
}  
 //顯示(單/多)選對話框 //返回值包含﹒欄位名稱+內容;傳回所有值
 //sysid.length>=2;  第一位表示(0:單選／1:多選);第二位表示(系統sysid);
 //PermissionID:系統表單id
 //'@|$' 表示 <<|>>; @,# 表示 <<,>> @^&  表示 <<'>>
function showdialog(Sysid, PermissionID, strWhere,strPath) {
    var returnvalue;
    var Where;

    if (strWhere == null)
        Where = '';
    else
        Where = '&Where=' + strWhere;
    if (strPath == null)
        returnvalue = window.showModalDialog('../../TempPage.aspx?Sysid=' + Sysid + '&PermissionID=' + PermissionID + Where, window, 'DialogHeight:560px;DialogWidth:600px;help:no;scroll:auto;Resizable:yes');
    else
        returnvalue = window.showModalDialog(strPath + 'TempPage.aspx?Sysid=' + Sysid + '&PermissionID=' + PermissionID + Where, window, 'DialogHeight:560px;DialogWidth:600px;help:no;scroll:auto;Resizable:yes');
    //alert(returnvalue);
    if (returnvalue != "undefined" && returnvalue != null) {
        if (returnvalue != "") {
            var t1 = returnvalue.replace(/&nbsp;/g, "");
            var t2 = t1.replace(/&quot;/g, '"');
            var t3 = t2.replace(/&lt;/g, '<');
            var t4 = t3.replace(/&gt;/g, '>');
            var t5 = t4.replace("@^&", "'");
            var t6 = t5.replace(/&amp;/g, '&');
            return t6.split("@,#");
        }
        else
            return null;
    }
    else {
        return null;
    }
}

//鼠標移動 改變顏色
function HandleMouseEvent(evt)
{
	var el=event.srcElement;
	var c;
	if(el.tagName=="TD")
	{
		switch(evt)
		{
			case "over":
                if (el.parentElement.className!="bottomtable")
                {
                    c=el.parentElement.className;
                    el.parentElement.className="table_titlebgcolor";
                }   
			    break;
			case "out":
			    if (el.parentElement.className!="bottomtable")
                {					
			        el.parentElement.className="table_bordercolor";
			    }
			    break;
			case "dblclick":
			break;
		}
	}
}

//根据欄位名稱﹐獲得該欄位的值
function getfieldtext(fieldname,fieldvalue,field)
{
    var intpos=-1;
    for( var i=0;i<fieldname.length;i++)
    {
        if(fieldname[i]==field)
        {
            intpos=i;
            break;
        }   
    }
    if (intpos!=-1 && intpos<fieldvalue.length)
        return fieldvalue[intpos];
    else
        return "";
}

function formatNumber(num, pattern) {
    if (num == undefined || num.length == 0) num = "0";
    num = num.toString();
    num = num.replace(/,/g, '')//.replace(",", "");
    if (num != "") {
        var pat = "###";
        var point = 0;
        switch (pattern) {
            case "N0":
                pat = "#,###";
                point = 0;
                break;
            case "N1":
                pat = "#,###.#";
                point = 1;
                break;
            case "N2":
                pat = "#,###.##";
                point = 2;
                break;
            case "N3":
                pat = "#,###.###";
                point = 3;
                break;
            case "N4":
                pat = "#,###.####";
                point = 4;
                break;
            case "N5":
                pat = "#,###.#####";
                point = 5;
                break;
            case "N6":
                pat = "#,###.######";
                point = 6;
                break;
            case "F0":
                pat = "####";
                point = 0;
                break;
            case "F1":
                pat = "####.#";
                point = 1;
                break;
            case "F2":
                pat = "####.##";
                point = 2;
                break;
            case "F3":
                pat = "####.###";
                point = 3;
                break;
            case "F4":
                pat = "####.####";
                point = 4;
                break;
            case "F5":
                pat = "####.#####";
                point = 5;
                break;
            case "F6":
                pat = "####.######";
                point = 6;
                break;
            default:
                break;
        }
       // num = Math.round(num, point);
        var strarr = num ? num.toString().split('.') : ['0'];
        var fmtarr = pat ? pat.split('.') : [''];
        var retstr = '';

        // 整?部分   
        var str = strarr[0];
        var fmt = fmtarr[0];
        var i = str.length - 1;
        var comma = false;
        for (var f = fmt.length - 1; f >= 0; f--) {
            switch (fmt.substr(f, 1)) {
                case '#':
                    if (i >= 0) retstr = str.substr(i--, 1) + retstr;
                    break;
                case '0':
                    if (i >= 0) retstr = str.substr(i--, 1) + retstr;
                    else retstr = '0' + retstr;
                    break;
                case ',':
                    comma = true;
                    retstr = ',' + retstr;
                    break;
            }
        }
        if (i >= 0) {
            if (comma) {
                var l = str.length;
                for (; i >= 0; i--) {
                    retstr = str.substr(i, 1) + retstr;
                    if (i > 0 && ((l - i) % 3) == 0) retstr = ',' + retstr;
                }
            }
            else retstr = str.substr(0, i + 1) + retstr;
        }

        retstr = retstr + '.';
        // ?理小?部分   
        str = strarr.length > 1 ? strarr[1] : '';
        fmt = fmtarr.length > 1 ? fmtarr[1] : '';
        if (fmt.length > str.length)
            str = str + "0000000";
        i = 0;
        for (var f = 0; f < fmt.length; f++) {
            switch (fmt.substr(f, 1)) {
                case '#':
                    if (i < str.length) retstr += str.substr(i++, 1);
                    break;
                case '0':
                    if (i < str.length) retstr += str.substr(i++, 1);
                    else retstr += '0';
                    break;
            }
        }
        retstr = retstr.replace(/^,+/, '').replace(/\.$/, '').replace(/^[0][,]/, '').replace(/^0*/, '');
        if (retstr == "")
            retstr = "0";
        if (retstr.indexOf('.') == 0)
            retstr = '0' + retstr;
        var d = str.substr(f, 1);
        if (d >= 5)
            retstr = GetNumAddOne(retstr);
    }
    else {
        retstr = "";
    }
   
    return retstr;
}
function formatCurrNumericControl(obj) {
    if (obj.id.indexOf("_Q") > 0)
        obj.value = formatNumber(obj.value, strDataFormat[0]);
    else if (obj.id.indexOf("_P") > 0)
        obj.value = formatNumber(obj.value, strDataFormat[1]);
    else if (obj.id.indexOf("_M") > 0)
        obj.value = formatNumber(obj.value, strDataFormat[2]);
    else if (obj.id.indexOf("_R") > 0)
        obj.value = formatNumber(obj.value, strDataFormat[3]);
    else
        obj.value = formatNumber(obj.value, "N0");
}
function formatNumericControl() {
    var ctlItems = $("input[id$=_Q]");
    for (var i = 0; i < ctlItems.length; i++)
        ctlItems[i].value = formatNumber(ctlItems[i].value, strDataFormat[0]);
    ctlItems = $("input[id$=_P]");
    for (var i = 0; i < ctlItems.length; i++)
        ctlItems[i].value = formatNumber(ctlItems[i].value, strDataFormat[1]);
    ctlItems = $("input[id$=_M]");
    for (var i = 0; i < ctlItems.length; i++)
        ctlItems[i].value = formatNumber(ctlItems[i].value, strDataFormat[2]);
    ctlItems = $("input[id$=_R]");
    for (var i = 0; i < ctlItems.length; i++)
        ctlItems[i].value = formatNumber(ctlItems[i].value, strDataFormat[3]);
//    if ($("input[id$=_P]").length > 0)
//        $("input[id$=_P]").val(formatNumber($("input[id$=_P]").val(), strDataFormat[1]));
//    if ($("input[id$=_M]").length > 0)
//        $("input[id$=_M]").val(formatNumber($("input[id$=_M]").val(), strDataFormat[2]));
//    if ($("input[id$=_R]").length > 0)
//        $("input[id$=_R]").val(formatNumber($("input[id$=_R]").val(), strDataFormat[3]));
}
function GetNumAddOne(str) {
    var strvalue = "1";
    if (str != "") {
        var strNew = str.toString();
        switch (strNew.substr(strNew.length-1, 1)) {
            case "9":
                if (strNew.length == 1)
                    strvalue = "10";
                else
                    strvalue = GetNumAddOne(strNew.substr(0, strNew.length - 1)) + "0";
                break;
            case ".":
                strvalue = GetNumAddOne(strNew.substr(0, strNew.indexOf("."))) + strNew.substring(strNew.indexOf("."));
                break;
            case ",":
                strvalue = GetNumAddOne(strNew.substr(0, strNew.lastIndexOf(","))) + strNew.substring(strNew.lastIndexOf(","));
                break;
            default:
                var i = parseInt(strNew.substr(strNew.length - 1, 1)) + 1;
                strvalue = strNew.substr(0, strNew.length - 1) + i.toString();
                break;
        }
    }
    return strvalue;
}

function getAjax2(flag, idValue, fieldName, strWhere) {
    var temp;  

    $.ajax({
        async: false,
        type: "Post",
        url: getRootPath() + "/WebService/GetBaseData.asmx/strBaseData",
        data: "{'Flag':'" + flag + "','strIdValue':'" + escape(idValue) + "','strFieldName':'" + escape(fieldName) + "','strWhere':'" + escape(strWhere) + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function(data) {
            //debugger;
            //你可以 alert(data.d)看数据返回的格式
            data = jQuery.parseJSON(data.d);
            if (data.length > 0) {
                temp = data;
            }   
        }
    });
    return temp;
}
//明細中，獲得輸入的值。
//GetDetailBaseData('Sbj',this,'SubjectID',SubjectID1')
function GetDetailBaseData(FormID, cnKey, obj, baseFiledName) {

    var detailCtlName = '';
    var rowindex = obj.id.substring(0, obj.id.lastIndexOf("_"));
    var fields = baseFiledName.split(',');
    if (arguments[4] != null)
        fields = arguments[4].split(',');

    for (var i = 0; i < fields.length; i++) {
        var ctl = rowindex + "_" + fields[i];
        if (i == 0)
            detailCtlName += ctl;
        else
            detailCtlName += "," + ctl;
    }
    getAjax(FormID, cnKey, obj.value, baseFiledName, detailCtlName, '');
}                    
//                                       組合關鍵字
//GetDetailBaseDataA(FormID, cnKey, obj, v, baseFiledName)
function GetDetailBaseDataA(FormID, cnKey, obj,v, baseFiledName) {

    var detailCtlName = '';
    var rowindex = obj.id.substring(0, obj.id.lastIndexOf("_"));
    var fields = baseFiledName.split(',');
    if (arguments[5] != null)
        fields = arguments[5].split(',');

    for (var i = 0; i < fields.length; i++) {
        var ctl = rowindex + "_" + fields[i];
        if (i == 0)
            detailCtlName += ctl;
        else
            detailCtlName += "," + ctl;
    }
    getAjax(FormID, cnKey, v, baseFiledName, detailCtlName, '');
}

//明細中，獲得輸入的值。可傳入條件
//GetDetailBaseData('Sbj',this,'SubjectID',SubjectID1','1=1')
function GetDetailWhereBaseData(FormID, cnKey, obj, baseFieldName, detailFieldName) {

    var strWhere = '';
    var detailCtlName = '';
    var rowindex = obj.id.substring(0, obj.id.lastIndexOf("_"));
    var fields = detailFieldName.split(',');
    
    for (var i = 0; i < fields.length; i++) {
        var ctl = rowindex + "_" + fields[i];
        if (i == 0)
            detailCtlName += ctl;
        else
            detailCtlName += "," + ctl;
    }

    if (arguments[4] != undefined) {
        strWhere = arguments[4];
    }

    getAjax(FormID, cnKey, obj.value, baseFieldName, detailCtlName, strWhere);
}

//根據flag，獲取主關鍵字=idvalue的欄位(fieldvalue)值，并寫入控件<'txt'+fieldvalue>,不能傳遞條件
//若寫入的控件名稱不為<'txt'+fieldvalue>,請傳遞第四個參數--(控件名稱).
//若調用多個欄位，請用<,>分開。若有第四個參數(同欄位一樣處理。)
//舉例： getBaseData('Dep', '03', 'DepartmentName')，返回值部門名稱寫入控件txtDepartmentName.
//getBaseData('Dep', '03', 'DepartmentName','txtProfitDepName')返回值部門名稱寫入控件txtProfitDepName
//getBaseData('Dep', '03', 'DepartmentID,DepartmentName')，返回值部門名稱寫入控件txtDepartmentID,txtDepartmentName.
//getBaseData('Dep', '03', 'DepartmentID,DepartmentName','txtProfitDepID,txtProfitDepName')返回值部門名稱寫入控件txtProfitDepID,txtProfitDepName。
function getBaseData(FormID, cnKey, idValue, fieldName) {
    var ctlName = "";    
    
    if (arguments[4] != undefined) {        
        ctlName = arguments[4];
    }
    else {
        var fields = fieldName.split(',');
        for (var i = 0; i < fields.length; i++) {
            var ctl = "txt" + fields[i];
            if (i == 0)
                ctlName += ctl;
            else
                ctlName += "," + ctl;
        }   
    }
    getAjax(FormID, cnKey, idValue, fieldName, ctlName, '');
  
}
//根據flag，獲取主關鍵字=idvalue的欄位(fieldvalue)值，并寫入控件<'txt'+fieldvalue>。可傳遞或不傳遞條件。
//若調用多個欄位，請用<,>分開
//getBaseData('Dep', '03', 'DepartmentName','txtProfitDepName')返回值部門名稱寫入控件txtProfitDepName,無條件
//getBaseData('Dep', '03', 'DepartmentID,DepartmentName','txtProfitDepID,txtProfitDepName','1=1')返回值部門名稱寫入控件txtProfitDepID,txtProfitDepName,傳遞條件
function getWhereBaseData(FormID, cnKey, idValue, fieldName, ctlName) {
    var strWhere = '';
    if (arguments[5] != undefined) {
        strWhere = arguments[5];
    }
    getAjax(FormID, cnKey,idValue, fieldName, ctlName, strWhere);
   
}
function getAjax(FormID, cnKey, idValue, fieldName, ctlName, strWhere) {
    $.ajax({
        async: false,
        type: "Post",
        url: getRootPath() + "/WebService/BaseService.asmx/strBaseData",
        data: "{'FormID':'" + FormID + "','strIdValue':'" + escape(idValue) + "','strFieldName':'" + escape(fieldName) + "','strWhere':'" + escape(strWhere) + "','cnKey':'" + escape(cnKey) + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function(data) {
            //debugger;
            //你可以 alert(data.d)看数据返回的格式
            data = jQuery.parseJSON(data.d);
            var ctl = ctlName.split(",");
            var fl = fieldName.split(",");
            var strName;
            if (data.length > 0) {
                var currValue;
                for (var i = 0; i < ctl.length; i++) {
                    currValue = unescape(data[0][fl[i]].toString());
                    strName = ctl[i].substring(ctl[i].length - 2);
                    //判断是否是明细控件
                    if (ctl[i].lastIndexOf("_Row_") != -1) {
                    //if (typeof (tbTableId) != "undefined") {
                        var tableName = ctl[i].substring(0, ctl[i].indexOf("_Row"))
                        var tableIdx;
                        var key;
                        if (ctl[i].lastIndexOf("Row_") >= 0)
                            key = ctl[i].substring(ctl[i].lastIndexOf("_") + 1);
                        for (var ti = 0; ti < tbTableId.length; ti++) {
                            if (tbTableId[ti] == tableName) {
                                tableIdx = ti;
                                break;
                            }
                        }
                        if (tableIdx != null) {
                            var tbColTypeString = tbColType[tableIdx][key].toString().toLowerCase();
                            if (tbColTypeString.indexOf("numeric_q") >= 0)
                                $("input[id$=" + ctl[i] + "]").val(formatNumber(currValue, strDataFormat[0]));
                            else if (tbColTypeString.indexOf("numeric_p") >= 0)
                                $("input[id$=" + ctl[i] + "]").val(formatNumber(currValue, strDataFormat[1]));
                            else if (tbColTypeString.indexOf("numeric_m") >= 0)
                                $("input[id$=" + ctl[i] + "]").val(formatNumber(currValue, strDataFormat[2]));
                            else if (tbColTypeString.indexOf("numeric_r") >= 0)
                                $("input[id$=" + ctl[i] + "]").val(formatNumber(currValue, strDataFormat[3]));
                            else
                                $("#" + ctl[i]).val(currValue);
                                //$("input[id$=" + ctl[i] + "]").val(currValue);
                        }
                        else {
                            if (strName.indexOf("_Q") >= 0)
                                $("input[id$=" + ctl[i] + "]").val(formatNumber(currValue, strDataFormat[0]));
                            else if (strName.indexOf("_P") >= 0)
                                $("input[id$=" + ctl[i] + "]").val(formatNumber(currValue, strDataFormat[1]));
                            else if (strName.indexOf("_M") >= 0)
                                $("input[id$=" + ctl[i] + "]").val(formatNumber(currValue, strDataFormat[2]));
                            else if (strName.indexOf("_R") >= 0)
                                $("input[id$=" + ctl[i] + "]").val(formatNumber(currValue, strDataFormat[3]));
                            else
                                $("#" + ctl[i]).val(currValue);
                                //$("input[id$=" + ctl[i] + "]").val(currValue);
                        }

                    }
                    else {
                        if (strName.indexOf("_Q") >= 0)
                            $("input[id$=" + ctl[i] + "]").val(formatNumber(currValue, strDataFormat[0]));
                        else if (strName.indexOf("_P") >= 0)
                            $("input[id$=" + ctl[i] + "]").val(formatNumber(currValue, strDataFormat[1]));
                        else if (strName.indexOf("_M") >= 0)
                            $("input[id$=" + ctl[i] + "]").val(formatNumber(currValue, strDataFormat[2]));
                        else if (strName.indexOf("_R") >= 0)
                            $("input[id$=" + ctl[i] + "]").val(formatNumber(currValue, strDataFormat[3]));
                        else
                            $("#" + ctl[i]).val(currValue);
                            //$("input[id$=" + ctl[i] + "]").val(currValue);
                    }

                }
            }
            else {

                for (var i = 0; i < ctl.length; i++) {
                    strName = ctl[i].substring(ctl[i].length - 2);
                    //判断是否是明细控件
                    if (ctl[i].lastIndexOf("_Row_")!=-1){
                    //if (typeof (tbTableId) != "undefined") {
                        var tableName = ctl[i].substring(0, ctl[i].indexOf("_Row"))
                        var tableIdx;
                        var key;
                        if (ctl[i].lastIndexOf("Row_") >= 0)
                            key = ctl[i].substring(ctl[i].lastIndexOf("_") + 1);
                        for (var ti = 0; ti < tbTableId.length; ti++) {
                            if (tbTableId[ti] == tableName) {
                                tableIdx = ti;
                                break;
                            }
                        }
                        if (tableIdx != null) {
                            var tbColTypeString = tbColType[tableIdx][key].toString().toLowerCase();
                            if (tbColTypeString.indexOf("numeric_q") >= 0)
                                $("input[id$=" + ctl[i] + "]").val(formatNumber(currValue, strDataFormat[0]));
                            else if (tbColTypeString.indexOf("numeric_p") >= 0)
                                $("input[id$=" + ctl[i] + "]").val(formatNumber(currValue, strDataFormat[1]));
                            else if (tbColTypeString.indexOf("numeric_m") >= 0)
                                $("input[id$=" + ctl[i] + "]").val(formatNumber(currValue, strDataFormat[2]));
                            else if (tbColTypeString.indexOf("numeric_r") >= 0)
                                $("input[id$=" + ctl[i] + "]").val(formatNumber(currValue, strDataFormat[3]));
                            else
                                $("#" + ctl[i]).val(currValue);
                                //$("input[id$=" + ctl[i] + "]").val(currValue);
                        }
                    }
                    else {
                        if (strName.indexOf("_Q") >= 0)
                            $("input[id$=" + ctl[i] + "]").val(formatNumber('0', strDataFormat[0]));
                        else if (strName.indexOf("_P") >= 0)
                            $("input[id$=" + ctl[i] + "]").val(formatNumber('0', strDataFormat[1]));
                        else if (strName.indexOf("_M") >= 0)
                            $("input[id$=" + ctl[i] + "]").val(formatNumber('0', strDataFormat[2]));
                        else if (strName.indexOf("_R") >= 0)
                            $("input[id$=" + ctl[i] + "]").val(formatNumber('1', strDataFormat[3]));
                        else
                            $("#" + ctl[i]).val('');
                            //$("input[id$=" + ctl[i] + "]").val('');
                    }

                }
            }
            isGetAjax = true;
            //  JSON再次转换为Table 形式；  
            //可以是用  data[Row][Column].toString()来读取值；Row：第几行 Column：数据字段
            //alert(data[0]["ID"].toString() + ";"+data[0]["Name"].toString() + ";"+data[0]["Address"].toString());


            //$('#Div1').html(BuildDetails(data));
        },
        error: function(err) {
            alert(err + "err");
        }
    });

}
//flag:表示選擇標誌，如：部門資料為 com_department
//option:  0:單選／1:多選
//strWhere:選擇條件
function showSelectWindow(FormID, cnKey, option, strWhere) {
    var returnvalue;
    var Where;

    if (strWhere == null)
        Where = '';
    else
        Where = strWhere;
    returnvalue = window.showModalDialog('../../TempPage.aspx?FormID=' + FormID + '&Option=' + option + '&where=' + escape(Where) + '&cnKey=' + cnKey, window, 'dialogWidth:800px;dialogHeight:520px;help:no;status:no;scroll:auto;Resizable:yes;');
   
    if (returnvalue != "undefined" && returnvalue != null) {
        if (returnvalue != "") {//json數據，處理賦值時，必須用unescape()解碼。
            //
            return jQuery.parseJSON(returnvalue);   
        }
        else
            return null;
    }
    else {
        return null;
    }
}
//js获取网站根路径(站点及虚拟目录)，获得网站的根目录或虚拟目录的根地址
function getRootPath() {
    var strFullPath = window.document.location.href;
    var strPath = window.document.location.pathname;
    var pos = strFullPath.indexOf(strPath);
    var prePath = strFullPath.substring(0, pos);
    var postPath = strPath.substring(0, strPath.substr(1).indexOf('/') + 1);
    return (prePath + postPath);
}

//雙擊選取資料，如 GetOtherJsonValue('dep','departmentid,departmentName') 獲取部門編號，跟部門名稱，并填入txtdepartmentid,txtdepartmentName
//GetOtherJsonValue('dep','departmentid,departmentName','txtProfitDepID,txtProfitDepName')獲取部門編號，跟部門名稱，并填入 txtProfitDepID,txtProfitDepName
//不能傳遞where 條件
function GetOtherJsonValue(FormID, cnKey, fieldName) {//不能傳遞where條件。
    var strReturn = showSelectWindow(FormID, cnKey, 0, '');

    var ctlName = "";

    if (arguments[3] != undefined) {
        ctlName = arguments[3];
    }
    else {
        var fields = fieldName.split(',');
        for (var i = 0; i < fields.length; i++) {
            var ctl = "txt" + fields[i];
            if (i == 0)
                ctlName += ctl;
            else
                ctlName += "," + ctl;
        }
    }
    var fields = fieldName.split(',');
    SetCtrlValue(strReturn, fieldName, ctlName);
}

//GetOtherJsonWhereValue('dep','departmentid,departmentName','txtProfitDepID,txtProfitDepName')獲取部門編號，跟部門名稱，并填入 txtProfitDepID,txtProfitDepName
//可傳遞where 條件，為第四個參數
function GetOtherJsonWhereValue(FormID, cnKey, objName, ctlName) { //可傳遞where條件，為第四個參數
    var where = '';
    if (arguments[4] != null)
        where = arguments[4];


    var strReturn = showSelectWindow(FormID, cnKey, 0, where);
    SetCtrlValue(strReturn, objName, ctlName);
}
function SetCtrlValue(strReturn, field, ctl) {
    if (strReturn != null) {

        var obj = ctl.split(',');
        var fld = field.split(',');
        for (var i = 0; i < obj.length; i++) {
            $("#" + obj[i]).val(unescape(strReturn[0][fld[i]].toString()));
        }
    }
}
function updateAjax(strSQL,isSysData) {
    $.ajax({
        type: "Post",
        url: "../../WebService/UpdateData.asmx/Update",
        data: "{'strSQL':'" + escape(strSQL) + "','isSysData':'" + isSysData + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function(data) {
            debugger;
            //你可以 alert(data.d)看数据返回的格式
            data = jQuery.parseJSON(data.d);
            
        },
        error: function(err) {
            alert(err + "err");
        }
    });

}
function DateDiff(sDate1, sDate2) { //sDate1和sDate2是2002-12-18格式      
    var aDate, oDate1, oDate2, iDays;
    aDate = sDate1.split("/");
    oDate1 = new Date(aDate[0], aDate[1] - 1, aDate[2]);
    aDate = sDate2.split("/");
    oDate2 = new Date(aDate[0], aDate[1] - 1, aDate[2]);

    iDays = parseInt(Math.abs(oDate1 - oDate2) / 1000 / 60 / 60 / 24);
    if ((oDate1 - oDate2) < 0) {
        return -iDays;
    }
    return iDays;
}
//多選單擊取消多選,改變Button顏色
//ButtonValue:多選按鈕Name,如"#btnMulSel1";  HdnValue:回傳回去的隱藏控件Name,如"#hdnMulSel1"
function ColClick(ButtonValue, HdnValue) {
    $(ButtonValue)[0].style.color = "Black";
    $(HdnValue).val("");
}
function ColClick1(ButtonValue, HdnValue) {
    $(ButtonValue)[0].color = "#000";
    $(HdnValue).val("");
}

//報表多選共用函數; KeyField:要查詢的欄位名稱,如DepartmentID; ButtonValue:多選按鈕Name,如"#btnMulSel1";  HdnValue:回傳回去的隱藏控件Name,如"#hdnMulSel1"
function getMultiItems(FormID, cnKey, option, KeyField, ButtonValue, HdnValue, strWhere) {
    if ($(ButtonValue)[0].value == "取消指定") {
        $(HdnValue).val("");
        $(ButtonValue)[0].value = "指定";
        return;
    }
    var sValue = "";
    var Where;
    if (strWhere == null)
        Where = '';
    else
        Where = strWhere;
    var returnvalue = window.showModalDialog('../../TempPage.aspx?FormID=' + FormID + '&Option=' + option + '&where=' + escape(Where) + '&cnKey=' + cnKey, window, 'dialogWidth:800px;dialogHeight:520px;help:no;status:no;scroll:auto;Resizable:yes;');
    if (returnvalue != "") {//json數據，處理賦值時，必須用unescape()解碼。
        var strReturn = jQuery.parseJSON(returnvalue);
    }
    if (strReturn != null) {
        for (var i = 0; i < strReturn.length; i++) {
            if (sValue == "")
                sValue += "'" + escape(strReturn[i][KeyField]) + "'";
            else
                sValue += ",'" + escape(strReturn[i][KeyField]) + "'";
        }
    }
    //按鈕顏色變紅色
    if (sValue == "")
        $(ButtonValue)[0].value = "指定";
    else
        $(ButtonValue)[0].value = "取消指定";
    $(HdnValue).val(sValue);
}
//取得歷史地址
function historyAddress(FormID, cnKey, ctrl, strwhere) {
    if (strwhere.length == 0) {
        $("#" + ctrl).combobox({ source: [], refreshsource: true });
        return;
    }
    $.ajax({
        async: false,
        type: "Post",
        url: getRootPath() + "/WebService/BaseService.asmx/historyAddress",
        data: "{'FormID':'" + FormID + "','cnKey':'" + escape(cnKey) + "','filter':'" + strwhere + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            //debugger;
            //你可以 alert(data.d)看数据返回的格式
            data = parseJosnArray(jQuery.parseJSON(data.d));
            //r = data.d;
            $("#" + ctrl).combobox({ source: data, refreshsource: true });
        },
        error: function (err) {
            alert(err + "err");
        }
    });
}
function parseJosnArray(para) {
    if (para == null) return null;
    var v = Array();
    for (var i = 0; i < para.length; i++) {
        for (key in para[i]) {
            //alert(key);
            v.push(para[i][key]);
        }
    }
    return v;
}
//單號自動編號
function autoCode(FormID, cnKey, ctrl_dateteime) {
    var d = $("#" + ctrl_dateteime + "_txtDate").attr('datevalue');
    var r = "";
    $.ajax({
        async: false,
        type: "Post",
        url: getRootPath() + "/WebService/BaseService.asmx/autoCode",
        data: "{'FormID':'" + FormID + "','cnKey':'" + escape(cnKey) + "','strdate':'" + d + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            //debugger;
            //你可以 alert(data.d)看数据返回的格式
            //data = jQuery.parseJSON(data.d);
            r= data.d;
        },
        error: function (err) {
            alert(err + "err");
        }
    });
    return r;
}

//聯動地址編輯
//例子 editAddress(obj, zip) or editAddress(obj)
function editAddress(obj, zip) {
    if (obj.children.length != 0)
        obj = obj.children[1];
    returnvalue = window.showModalDialog('../../TempAddress.aspx', window, 'dialogWidth:500px;dialogHeight:200px;help:no;status:no;scroll:auto;Resizable:yes;');
    if (returnvalue != null) {
        var part = returnvalue.split("@#$");
        if (obj) obj.value = part[0];
        if (zip) zip.value = part[1];
    }
}