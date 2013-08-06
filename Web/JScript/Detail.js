var tbTableId = Array(); //明細table名稱。
var tbRowName = Array(); //Row
var fixCols = Array();//固定行,若沒指定，值為1,
//var tbColName = Array()//欄位名稱(資料庫),只包含表單中顯示的欄位。其中每個明細用Object表示。 
var tbColHeadText = Array(); //欄位中文名稱，跟tbColName一一對應。

var tbColWidth = Array(); //欄位寬度，跟tbColName一一對應。

var tbColType = Array(); //欄位類型，跟tbColName一一對應。
var subData = Array(); //明細數據
var divWidth = Array();

var detailCurCell = null;
var pattern = /^\d*\.?\d{0,6}$/;

//更新異動人員,時間
function LastModify(id) {
    var arr = id.split('_');
    if (arr[3] == "radio") return
    var obj = $("#" + id.replace(arr[3], "") + "LastModifyDate")
    BindDateValue(obj, new Date(), false); //日期付值
    $("#" + id.replace(arr[3], "") + "LastModifyUserName").val(strUserName);
}
//初始化明細div寬度
function getDivWidth() {
    for (var i = 0; i < tbTableId.length; i++) {
        divWidth[i] = $("#div_" + tbTableId[i])[0].offsetWidth;
        //divWidth[i] = $("#div_" + tbTableId[i])[0].style.pixelWidth
    }
}
//動態新增明細Table
//並寫入初始值
function AddToDetail() {
    getDivWidth();
    AddDetailTableHead();
    for (var i = 0; i < tbTableId.length; i++) {
        if (subData[i] != null) {
            AddDetailAndBindHandle(i, subData[i].length);
            BindJsonToDetail(i, subData[i]);

            $("#" + tbRowName[i] + "_" + 1 + "_radio").attr('checked', 'checked');
            $("#" + tbRowName[i] + "F_" + 1 + "_radio").attr('checked', 'checked');
        }
        
    }
    
}

function AddDetailTableHead() {
    for (var i = 0; i < tbTableId.length; i++) {
        tbRowName[i] = tbTableId[i] + "_Row";
        var tbw = 0;
        for (key in tbColWidth[i]) {
            tbw += parseInt(tbColWidth[i][key].toString());
        }
        
        
        var ndiv = $("#div_" + tbTableId[i]);
        var table = $("<table id='" + tbTableId[i] + "' class='table_backcolor' style='width:" + tbw + "px;'></table>");
                
        var tr = $("<tr></tr>");
        var h1 = $("<th  class='Title' style='width:30px; height:24px;'></th>");
        h1.appendTo(tr);
        var ColWidth = Array();
        ColWidth[0] = 30;
        var ColName = Array();
        ColName[0] = tbTableId[i] + "_Col_Sel";
        var kk = 1;
        for (key in tbColHeadText[i]) {

            var th = $("<th  class='Title' style='width:" + tbColWidth[i][key].toString() + "px; height:24px;overflow:hidden;'>" + tbColHeadText[i][key].toString() + "</th>");
            th.appendTo(tr);
            ColWidth[kk] = tbColWidth[i][key];
            ColName[kk] = tbTableId[i] + "_Col_" + key;
            kk++;

        }        
        tr.appendTo(table);
        table.appendTo(ndiv);

       
      
        var mySt = new superTable(tbTableId[i], {
            cssSkin: "sSky",
            fixedCols: fixCols[i],
            colWidths: ColWidth,
            colNames: ColName,
            headerRows: 1
        });
    }
}
//新增明細或者插入明細（插入明細時,傳遞第三個參數，與新增為區別，可插入多行）；
function AddRow(tableIndex, RowCount) {
    var Row = Array();
    var table = $("#" + tbTableId[tableIndex] + "_sDataTable");
    var tableF = $("#" + tbTableId[tableIndex] + "_sFDataTable");
    var bln;
    
    var insertRow = 0;
    if (arguments[2]==undefined) 
        bln = false;
    else
        bln = true;
    if (bln) {
        insertRow = GetSelectedRow(tableF);
        if (insertRow == 0) {
            alert("請先選擇插入的行");
            return false;
        }
    }

    var addRowIndex = table.find('tr').length;
    var Rows = addRowIndex;
    for (var i = 0; i < RowCount; i++) {

        var row = $("<tr id='" + tbRowName[tableIndex] + "_" + addRowIndex + "' class='Same'></tr>");
        var rowF = $("<tr id='" + tbRowName[tableIndex] + "F_" + addRowIndex + "' class='Same'></tr>");

        if (bln) {
            var td1 = $("<td align=\"center\"><input type=\"radio\" id=\"" + tbRowName[tableIndex] + "_" + addRowIndex + "_radio\" name=\"" + tbTableId[tableIndex] + "_rowindex\" ></td>");
            td1.appendTo(row);

            var td1F = $("<td align=\"center\"><input type=\"radio\" id=\"" + tbRowName[tableIndex] + "F_" + addRowIndex + "_radio\" name=\"" + tbTableId[tableIndex] + "_rowindex\"></td>");
            td1F.appendTo(rowF);
        }
        else {
            var td1 = $("<td align=\"center\"><input type=\"radio\" id=\"" + tbRowName[tableIndex] + "_" + addRowIndex + "_radio\" name=\"" + tbTableId[tableIndex] + "_rowindex\"></td>");
            td1.appendTo(row);

            var td1F = $("<td align=\"center\"><input type=\"radio\" id=\"" + tbRowName[tableIndex] + "F_" + addRowIndex + "_radio\" name=\"" + tbTableId[tableIndex] + "_rowindex\"></td>");
            td1F.appendTo(rowF);
        }

        var RowIndex = addRowIndex;
        Row[i] = RowIndex;
        var j = 1;
        var add = 0;
        var tddisplay = "";
        for (key in tbColType[tableIndex]) {
            tddisplay = "";
            if (table[0].rows[0].cells[add + 1] != null && table[0].rows[0].cells[add + 1].style.display != "") tddisplay = "style='display:" + table[0].rows[0].cells[add + 1].style.display + ";'"
            var td = $("<td " + tddisplay + "></td>");
            var tdF = $("<td " + tddisplay + "></td>");
            add++;
            var control;
            var controlF;

            var tbColTypeString = tbColType[tableIndex][key].toString().toLowerCase();
            if (tbColTypeString == "label") {
                td.attr('align', 'center');
                if (key == "SubID") {
                    //先找最大的SubID
                    var SubID = 1;
                    for (var iSid = 1; iSid < addRowIndex; iSid++) {
                        var thisSubID = parseInt($("#" + tbRowName[tableIndex] + "_" + iSid + "_" + key).text());
                        if (thisSubID >= SubID)
                            SubID = thisSubID + 1;
                    }
                    //指定SubID值
                    control = $("<span id=\"" + tbRowName[tableIndex] + "_" + RowIndex + "_" + key + "\">" + SubID + "</span>");
                    control.appendTo(td);
                    td.appendTo(row);

                    if (j < fixCols[tableIndex]) {
                        tdF.attr('align', 'center');
                        controlF = $("<span id=\"" + tbRowName[tableIndex] + "F_" + RowIndex + "_" + key + "\">" + SubID + "</span>");
                        controlF.appendTo(tdF);
                        tdF.appendTo(rowF);
                    }
                }
                else {
                    control = $("<span id=\"" + tbRowName[tableIndex] + "_" + RowIndex + "_" + key + "\">" + RowIndex + "</span>");
                    control.appendTo(td);
                    td.appendTo(row);

                    if (j < fixCols[tableIndex]) {
                        tdF.attr('align', 'center');
                        controlF = $("<span id=\"" + tbRowName[tableIndex] + "F_" + RowIndex + "_" + key + "\">" + RowIndex + "</span>");
                        controlF.appendTo(tdF);
                        tdF.appendTo(rowF);
                    }
                }
            }
            if (tbColTypeString == "text") {
                control = $("<input id=\"" + tbRowName[tableIndex] + "_" + RowIndex + "_" + key + "\" class=\"detailtext\" type=\"text\" >");
                control.appendTo(td);
                td.appendTo(row);

                if (j < fixCols[tableIndex]) {
                    controlF = $("<input id=\"" + tbRowName[tableIndex] + "F_" + RowIndex + "_" + key + "\" class=\"detailtext\" type=\"text\" >");
                    controlF.appendTo(tdF);
                    tdF.appendTo(rowF);
                }
            }
            if (tbColTypeString.indexOf("text") > 0) {
                var strclass = "detailtext";
                if (tbColTypeString.indexOf("readonly") >= 0)
                    strclass = "detailtextReadOnly";

                control = $("<input id=\"" + tbRowName[tableIndex] + "_" + RowIndex + "_" + key + "\" class=\"" + strclass + "\"" + tbColTypeString + ">");
                control.appendTo(td);
                td.appendTo(row);

                if (j < fixCols[tableIndex]) {
                    controlF = $("<input id=\"" + tbRowName[tableIndex] + "F_" + RowIndex + "_" + key + "\" class=\"" + strclass + "\"" + tbColTypeString + ">");
                    controlF.appendTo(tdF);
                    tdF.appendTo(rowF);
                }
            }
            if (tbColTypeString.indexOf("numeric") > 0) { //numeric_m:金額；numeric_q:數量；numeric_p:單價;numeric_r:匯率
                var format = "N0";
                if (tbColTypeString.indexOf("numeric_m") > 0)
                    format = strDataFormat[2];
                if (tbColTypeString.indexOf("numeric_q") > 0)
                    format = strDataFormat[0];
                if (tbColTypeString.indexOf("numeric_p") > 0)
                    format = strDataFormat[1];
                if (tbColTypeString.indexOf("numeric_r") > 0)
                    format = strDataFormat[3];


                var strTmp = tbColTypeString.replace("numeric_m", "text").replace("numeric_q", "text").replace("numeric_p", "text").replace("numeric_r", "text").replace("numeric", "text");
                var strclass = "detailnum";
                if (strTmp.indexOf("readonly") >= 0)
                    strclass = "detailnumReadOnly";

                var strvalue = "0";
                if (strTmp.indexOf("value=") >= 0) {
                    strvalue = strTmp.substring(strTmp.indexOf("value=") + 7, strTmp.indexOf('"',strTmp.indexOf("value=") + 7));
                }


                control = $("<input id=\"" + tbRowName[tableIndex] + "_" + RowIndex + "_" + key + "\" class=\"" + strclass + "\" " + strTmp + " onkeypress=\"return regInput(this, " + pattern + ",String.fromCharCode(event.keyCode)) ;\" ondrop=\"return regInput(this, " + pattern + ",event.dataTransfer.getData('Text'));\" onpaste=\"return regInput(this, " + pattern + ",window.clipboardData.getData('Text'));\" onfocus=\"$(this).attr('oldvalue',this.value);detailCurCell=this;\" onblur=\"this.value=formatNumber(this.value,'" + format + "');\">");
                control.bind("onblur", function() { alert("dd"); });
                control.appendTo(td);
                control.val(formatNumber(strvalue, format));
                td.appendTo(row);

                if (j < fixCols[tableIndex]) {
                    controlF = $("<input id=\"" + tbRowName[tableIndex] + "F_" + RowIndex + "_" + key + "\" class=\"" + strclass + "\" " + strTmp + " onkeypress=\"return regInput(this, " + pattern + ",String.fromCharCode(event.keyCode)) ;\" ondrop=\"return regInput(this, " + pattern + ",event.dataTransfer.getData('Text'));\" onpaste=\"return regInput(this, " + pattern + ",window.clipboardData.getData('Text'));\" onfocus=\"$(this).attr('oldvalue',this.value);detailCurCell=this;\" onblur=\"this.value=formatNumber(this.value,'" + format + "');\">");
                    controlF.appendTo(tdF);
                    controlF.val(formatNumber(strvalue, format));
                    tdF.appendTo(rowF);
                }
            } 
            if (tbColTypeString == "numeric") {
                var format = "N0";
                control = $("<input id=\"" + tbRowName[tableIndex] + "_" + RowIndex + "_" + key + "\" class=\"detailnum\" type=\"text\" onkeypress=\"return regInput(this, " + pattern + ",String.fromCharCode(event.keyCode)) ;\" ondrop=\"return regInput(this, " + pattern + ",event.dataTransfer.getData('Text'));\" onpaste=\"return regInput(this, " + pattern + ",window.clipboardData.getData('Text'));\" onfocus=\"$(this).attr('oldvalue',this.value);detailCurCell=this;\" onblur=\"this.value=formatNumber(this.value,'" + format + "');\">");
                control.appendTo(td);
                control.val(formatNumber('0', format));
                td.appendTo(row);

                if (j < fixCols[tableIndex]) {
                    controlF = $("<input id=\"" + tbRowName[tableIndex] + "F_" + RowIndex + "_" + key + "\" class=\"detailnum\" type=\"text\" onkeypress=\"return regInput(this, " + pattern + ",String.fromCharCode(event.keyCode)) ;\" ondrop=\"return regInput(this, " + pattern + ",event.dataTransfer.getData('Text'));\" onpaste=\"return regInput(this, " + pattern + ",window.clipboardData.getData('Text'));\" onfocus=\"$(this).attr('oldvalue',this.value);detailCurCell=this;\" onblur=\"this.value=formatNumber(this.value,'" + format + "');\">");
                    controlF.appendTo(tdF);
                    controlF.val(formatNumber('0', format));
                    tdF.appendTo(rowF);
                }
            }
            if (tbColTypeString.indexOf("numeric_") == 0) {
                var format = "N0";
                if (tbColTypeString=="numeric_m")
                    format = strDataFormat[2];
                if (tbColTypeString=="numeric_q")
                    format = strDataFormat[0];
                if (tbColTypeString=="numeric_p")
                    format = strDataFormat[1];
                if (tbColTypeString=="numeric_r")
                    format = strDataFormat[3];
                control = $("<input id=\"" + tbRowName[tableIndex] + "_" + RowIndex + "_" + key + "\" class=\"detailnum\" type=\"text\" onkeypress=\"return regInput(this, " + pattern + ",String.fromCharCode(event.keyCode)) ;\" ondrop=\"return regInput(this, " + pattern + ",event.dataTransfer.getData('Text'));\" onpaste=\"return regInput(this, " + pattern + ",window.clipboardData.getData('Text'));\" onfocus=\"$(this).attr('oldvalue',this.value);detailCurCell=this;\" onblur=\"this.value=formatNumber(this.value,'" + format + "');\">");
                control.appendTo(td);
                control.val(formatNumber('0', format));
                td.appendTo(row);

                if (j < fixCols[tableIndex]) {
                    controlF = $("<input id=\"" + tbRowName[tableIndex] + "F_" + RowIndex + "_" + key + "\" class=\"detailnum\" type=\"text\"   onkeypress=\"return regInput(this, " + pattern + ",String.fromCharCode(event.keyCode)) ;\" ondrop=\"return regInput(this, " + pattern + ",event.dataTransfer.getData('Text'));\" onpaste=\"return regInput(this, " + pattern + ",window.clipboardData.getData('Text'));\" onfocus=\"$(this).attr('oldvalue',this.value);detailCurCell=this;\" onblur=\"this.value=formatNumber(this.value,'" + format + "');\">");
                    controlF.appendTo(tdF);
                    controlF.val(formatNumber('0', format));
                    tdF.appendTo(rowF);
                }
            }
            if (tbColTypeString == "date") {
                control = $("<input id=\"" + tbRowName[tableIndex] + "_" + RowIndex + "_" + key + "\" value=\"\" style=\"width:75%;\" class=\"detailtext\" type=\"text\" value=\"\" maxlength=\"10\" onfocus=\"$(this).attr('oldvalue',this.value);detailCurCell=this;\" >");
                control.appendTo(td);
                td.appendTo(row);             
                if (j < fixCols[tableIndex]) {
                    control = $("<input id=\"" + tbRowName[tableIndex] + "F_" + RowIndex + "_" + key + "\" value=\"\" style=\"width:75%;\" class=\"detailtext\" type=\"text\" value=\"\" maxlength=\"10\" onfocus=\"$(this).attr('oldvalue',this.value);detailCurCell=this;\">");
                    controlF.appendTo(tdF);
                    tdF.appendTo(rowF);
                }
            }
            if (tbColTypeString == "check") {
                td.attr('align', 'center');
                control = $("<input id=\"" + tbRowName[tableIndex] + "_" + RowIndex + "_" + key + "\" type=\"checkbox\" >");
                control.appendTo(td); 
                td.appendTo(row);

                if (j < fixCols[tableIndex]) {
                    tdF.attr('align', 'center');
                    controlF = $("<input id=\"" + tbRowName[tableIndex] + "F_" + RowIndex + "_" + key + "\" type=\"checkbox\" >");
                    controlF.appendTo(tdF);
                    tdF.appendTo(rowF);
                }
            }
            if (tbColTypeString.indexOf("checkbox") != -1) {
                td.attr('align', 'center');
                control = $("<input id=\"" + tbRowName[tableIndex] + "_" + RowIndex + "_" + key + "\" " + tbColTypeString + ">");
                control.appendTo(td);
                td.appendTo(row);

                if (j < fixCols[tableIndex]) {
                    tdF.attr('align', 'center');
                    controlF = $("<input id=\"" + tbRowName[tableIndex] + "F_" + RowIndex + "_" + key + "\" " + tbColTypeString + ">");
                    controlF.appendTo(tdF);
                    tdF.appendTo(rowF);
                }
            }
            if (tbColTypeString == "dropdown") {
                if (bln) {
                    control = $("select[id='" + tbRowName[tableIndex] + "_" + insertRow + "_" + key + "']").clone();
                    control[0].id = tbRowName[tableIndex] + "_" + RowIndex + "_" + key;
                    control.appendTo(td);
                    td.appendTo(row);
                    if (j < fixCols[tableIndex]) {
                        control = $("select[id='" + tbRowName[tableIndex] + "F_" + insertRow + "_" + key + "']").clone();
                        control[0].id = tbRowName[tableIndex] + "F_" + RowIndex + "_" + key;
                        control.appendTo(tdF);
                        td.appendTo(rowF);
                    }
                }
                else {
                    control = $("<select id=\"" + tbRowName[tableIndex] + "_" + RowIndex + "_" + key + "\" class=\"detailtext\">");
                    control.appendTo(td);
                    td.appendTo(row);
                    if (j < fixCols[tableIndex]) {
                        controlF = $("<select id=\"" + tbRowName[tableIndex] + "F_" + RowIndex + "_" + key + "\" class=\"detailtext\">");
                        controlF.appendTo(tdF);
                        tdF.appendTo(rowF);
                    }
                }

            }
            if (tbColTypeString.indexOf("dropdown") != -1) {
                if (bln) {
                    control = $("select[id='" + tbRowName[tableIndex] + "_" + insertRow + "_" + key + "']").clone();
                    control[0].id = tbRowName[tableIndex] + "_" + RowIndex + "_" + key;
                    control.appendTo(td);
                    td.appendTo(row);
                    if (j < fixCols[tableIndex]) {
                        control = $("select[id='" + tbRowName[tableIndex] + "F_" + insertRow + "_" + key + "']").clone();
                        control[0].id = tbRowName[tableIndex] + "F_" + RowIndex + "_" + key;
                        control.appendTo(tdF);
                        td.appendTo(rowF);
                    }
                }
                else {
                    control = $("<select id=\"" + tbRowName[tableIndex] + "_" + RowIndex + "_" + key + "\" class=\"detailtext\"  " + tbColTypeString + ">");
                    control.appendTo(td);
                    td.appendTo(row);
                    if (j < fixCols[tableIndex]) {
                        controlF = $("<select id=\"" + tbRowName[tableIndex] + "F_" + RowIndex + "_" + key + "\" class=\"detailtext\"  " + tbColTypeString + ">");
                        controlF.appendTo(tdF);
                        tdF.appendTo(rowF);
                    }     
                }                         
            }
            j++;
           
        } //key
        row.appendTo(table);
        rowF.appendTo(tableF);
        var a = $("[id^=" + tbRowName[tableIndex] + "_]");
        a.bind("focus", function() { RowSelected(this.id) });
        var aF = $("[id^=" + tbRowName[tableIndex] + "F_]");
        aF.bind("focus", function() { RowSelected(this.id) });
        addRowIndex++;

    }  //RowCount

    if (bln) {
        Row[RowCount] = insertRow;//插入狀態，最後一個保存插入的位置。
        for (var j = Rows - 1; j >= insertRow; j--) {
            var n = j + RowCount;
            var t = 1;
            for (key in tbColType[tableIndex]) {
                var tbColTypeString = tbColType[tableIndex][key].toString().toLowerCase();
                if (tbColTypeString == "check" || tbColTypeString.indexOf("checkbox") != -1) {
                    //$("#" + tbRowName[tableIndex] + "_" + n + "_" + key)[0].checked = $("#" + tbRowName[tableIndex] + "_" + j + "_" + key)[0].checked;
                    if ($("#" + tbRowName[tableIndex] + "_" + j + "_" + key)[0].checked) {
                        $("#" + tbRowName[tableIndex] + "_" + n + "_" + key).attr('checked', 'checked');
                        $("#" + tbRowName[tableIndex] + "_" + j + "_" + key).removeAttr('checked');
                    }
                    else
                        $("#" + tbRowName[tableIndex] + "_" + n + "_" + key).removeAttr('checked');
                    if (t < fixCols[tableIndex]) {
                       //$("#" + tbRowName[tableIndex] + "_" + n + "_" + key)[0].checked = $("#" + tbRowName[tableIndex] + "_" + j + "_" + key)[0].checked;
                        if ($("#" + tbRowName[tableIndex] + "F_" + j + "_" + key)[0].checked) {
                            $("#" + tbRowName[tableIndex] + "F_" + n + "_" + key).attr('checked', 'checked');
                            $("#" + tbRowName[tableIndex] + "F_" + j + "_" + key).removeAttr('checked');
                        }
                        else
                            $("#" + tbRowName[tableIndex] + "F_" + n + "_" + key).removeAttr('checked');
                    }

                }
               else if (tbColTypeString == "dropdown" || tbColTypeString.indexOf("dropdown") != -1) {
                   $("#" + tbRowName[tableIndex] + "_" + n + "_" + key).val($("#" + tbRowName[tableIndex] + "_" + j + "_" + key).val());
                    //$("#" + tbRowName[tableIndex] + "_" + j + "_" + key)[0][0].selected = true;
                    if (t < fixCols[tableIndex]) {
                        $("#" + tbRowName[tableIndex] + "F_" + n + "_" + key).val("" + $("#" + tbRowName[tableIndex] + "F_" + j + "_" + key).val());
                        //$("#" + tbRowName[tableIndex] + "_" + j + "_" + key)[0][0].attr('selected', 'true');
                    }
                }
                
                else {
                    if (key == "SubID") {
                        var SubID = $("#" + tbRowName[tableIndex] + "_" + n + "_" + key).text();
                        $("#" + tbRowName[tableIndex] + "_" + n + "_" + key).text($("#" + tbRowName[tableIndex] + "_" + j + "_" + key).text());
                        $("#" + tbRowName[tableIndex] + "_" + j + "_" + key).text(SubID);
                    }
                    else {
                        var dvalue = $("#" + tbRowName[tableIndex] + "_" + n + "_" + key).val();
                        $("#" + tbRowName[tableIndex] + "_" + n + "_" + key).val($("#" + tbRowName[tableIndex] + "_" + j + "_" + key).val());
                        $("#" + tbRowName[tableIndex] + "_" + j + "_" + key).val(dvalue);
                        if (t < fixCols[tableIndex]) {
                            dvalue = $("#" + tbRowName[tableIndex] + "F_" + n + "_" + key).val();
                            $("#" + tbRowName[tableIndex] + "F_" + n + "_" + key).val($("#" + tbRowName[tableIndex] + "F_" + j + "_" + key).val());
                            $("#" + tbRowName[tableIndex] + "F_" + j + "_" + key).val(dvalue);
                        }
                    }
                }
                t++;

            } //key
        } //insertRows
    } //insert
    //增加隔行樣式
    $("table#" + tbTableId[tableIndex] + "_sDataTable tr:nth-child(odd)").addClass("myClass");
    $("table#" + tbTableId[tableIndex] + "_sFDataTable tr:nth-child(odd)").addClass("myClass");
    setRowCtlClass(tableIndex);
    
    return Row;
}
function setRowCtlClass(tableIndex) {
    var table = $("#" + tbTableId[tableIndex] + "_sDataTable");
    var tableF = $("#" + tbTableId[tableIndex] + "_sFDataTable");
    for (var i = 2; i < table.find('tr').length; i = i + 2) {
        var ctl = $("[id^=" + tbRowName[tableIndex] + "_" + i + "_]");
        for (var j = 0; j < ctl.length; j++) {
            if (ctl[j].className != "date_calendar1")
                $("#" + ctl[j].id).addClass("myClass");
            //ctl[j].className = "myClass";
        }
    }
}
function RowSelected(name) {

    var b = name.substring(0, name.lastIndexOf("_"));

    $("#" + b + "_radio").attr('checked', 'checked');
    $("#" + b.replace('Row', 'RowF') + "_radio").attr('checked', 'checked');
}
//清空明細
function clearRow(tableIndex) {
    var table = $("#" + tbTableId[tableIndex] + "_sDataTable");
    var tableF = $("#" + tbTableId[tableIndex] + "_sFDataTable");
    var RowCount = table.find('tr').length - 1;
    for (var r = 0; r < RowCount; r++) {

        tableF.find('tr').last().remove();
        table.find('tr').last().remove();
    }
}
//存檔是刪除空的明細
function delNullDetail(tableIndex, pFldNameString) {
    var arr = pFldNameString.split(",");
    var table = $("#" + tbTableId[tableIndex] + "_sDataTable");
    var rowCount = table.find('tr').length - 1;

    for (var i = rowCount; i > 0; i--) {
        var s = "";
        for (var j = 0; j < arr.length; j++) {
            if ($.trim($("#" + tbRowName[tableIndex] + "_" + i + "_" + arr[j]).val()) == "") {
                s = s + "0";
            }
            else {
                s = s + "1";
            }
        }
        
        if (parseInt(s) == 0) {
            $("#" + tbRowName[tableIndex] + "_" + i + "_radio").attr('checked', 'checked');
            $("#" + tbRowName[tableIndex] + "F_" + i + "_radio").attr('checked', 'checked');
            delRow(tableIndex);
        }
    }
}
//刪除明細
function delRow(tableIndex) {
    var table = $("#" + tbTableId[tableIndex] + "_sDataTable");
    var tableF = $("#" + tbTableId[tableIndex] + "_sFDataTable");
    var RowCount = table.find('tr').length - 1;
    var delIndex = GetSelectedRow(tableF);
    if (delIndex == 0) {
        alert("請先選擇刪除的行!");
        return false;
    }
    for (var r = delIndex; r < RowCount; r++) {
        var n = r + 1;
        var j = 1;
        for (key in tbColType[tableIndex]) {
            var tbColTypeString = tbColType[tableIndex][key].toString().toLowerCase();
            if (tbColTypeString == "check") {
                if ($("#" + tbRowName[tableIndex] + "_" + n + "_" + key)[0].checked)
                    $("#" + tbRowName[tableIndex] + "_" + r + "_" + key).attr('checked', 'checked');
                else
                    $("#" + tbRowName[tableIndex] + "_" + r + "_" + key).removeAttr('checked');
                if (j < fixCols[tableIndex]) {
                    if ($("#" + tbRowName[tableIndex] + "F_" + n + "_" + key)[0].checked)
                        $("#" + tbRowName[tableIndex] + "F_" + r + "_" + key).attr('checked', 'checked');
                    else
                        $("#" + tbRowName[tableIndex] + "F_" + r + "_" + key).removeAttr('checked');
                }

            }
            else {
                $("#" + tbRowName[tableIndex] + "_" + r + "_" + key).val($("#" + tbRowName[tableIndex] + "_" + n + "_" + key).val());
                if (j < fixCols[tableIndex])
                    $("#" + tbRowName[tableIndex] + "F_" + r + "_" + key).val($("#" + tbRowName[tableIndex] + "F_" + n + "_" + key).val());
            }
            j++;
        }
    }
    tableF.find('tr').last().remove();
    table.find('tr').last().remove();
   // $("DiV[class='sData']").scrollTop($("DiV[class='sData']").scrollTop()-26);
    // $("DiV[class='sData']").scrollTop($("DiV[class='sData']").scrollTop() + 26);
    var a = $("DiV[class='sData']").scrollTop();
    var b = $("DiV[class='sData']").scrollLeft();
    $("DiV[class='sFDataInner']").css({ top: a * -1 + 'px' });
    $("DiV[class='sHeaderInner']").css({ right: b + 'px' });
    
    if (delIndex == RowCount && delIndex != 1) {
        var k = delIndex - 1;
        $("#" + tbRowName[tableIndex] + "_" + k + "_radio").attr('checked', 'checked');
        $("#" + tbRowName[tableIndex] + "F_" + k + "_radio").attr('checked', 'checked');
    }
    if (delIndex != RowCount) {
        $("#" + tbRowName[tableIndex] + "_" + delIndex + "_radio").attr('checked', 'checked');
        $("#" + tbRowName[tableIndex] + "F_" + delIndex+"_radio").attr('checked', 'checked');
    }
    //增加隔行樣式
    //$("table#" + tbTableId[tableIndex] + "_sDataTable tr:nth-child(odd)").addClass("myClass");
    //$("table#" + tbTableId[tableIndex] + "_sFDataTable tr:nth-child(odd)").addClass("myClass");
    //sumContractList();
}
function getCodeIndex(obj) {
    if (obj) {
        if (obj.length) {
            for (i = 0; i < obj.length; i++) {
                if (obj[i].checked) {
                    return i + 1;
                }
            }
        }
    }
}

function GetSelectedRow(obj) { 
    var name = obj.selector.substring(0, obj.selector.lastIndexOf("_")).replace('#', '');
    var radio = obj.find("input[name='" + name + "_rowindex']");
    var delIndex = getCodeIndex(radio);
    if (isNaN(delIndex))
        delIndex = 0;
    return delIndex;

}

//將明細中的值產生json數據。

function GenerateDetailToJson(tableIndex) {
    var json = "[";
    var table = $("#" + tbTableId[tableIndex] + "_sDataTable");
    var tableF = $("#" + tbTableId[tableIndex] + "_sFDataTable");
    
    var RowCount = table.find('tr').length - 1;
    for (var i = 1; i <= RowCount; i++) {
        var jtr = "{";
        var str = "";
        var j = 1;
        for (key in tbColType[tableIndex]) {
            var tbColTypeString = tbColType[tableIndex][key].toString().toLowerCase();
            if (j < fixCols[tableIndex]) {
                $("#" + tbRowName[tableIndex] + "_" + i + "_" + key).val($("#" + tbRowName[tableIndex] + "F_" + i + "_" + key).val());
            }
            str = str + "\"" + key + "\": ";
            if (tbColTypeString == "label")
                str = str + "\"" + escape($("#" + tbRowName[tableIndex] + "_" + i + "_" + key)[0].innerText) + "\",";
            //else if (tbColTypeString == "text" || tbColTypeString == "dropdown")
            //str = str + "\"" + escape($("#" + tbRowName[tableIndex] + "_" + i + "_" + key).val()) + "\",";
            else if (tbColTypeString == "check")
                str = str + "" + $("#" + tbRowName[tableIndex] + "_" + i + "_" + key)[0].checked + ",";
            else if (tbColTypeString.indexOf("checkbox") != -1)
                str = str + "" + $("#" + tbRowName[tableIndex] + "_" + i + "_" + key)[0].checked + ",";
            else if (key.toString().toLowerCase().indexOf("date") >= 0)
                str = str + "\"" + GetStringToDateInt(strDateFormat, $("#" + tbRowName[tableIndex] + "_" + i + "_" + key).val()) + "\",";
            else if (key.toString().toLowerCase().substring(key.toString().length - 2) == "ym")
                str = str + "\"" + GetStringToYm(strDateFormat, $("#" + tbRowName[tableIndex] + "_" + i + "_" + key).val()) + "\",";
            else if (tbColTypeString.indexOf("numeric") >= 0)//(tbColTypeString == "numeric")
                str = str + "" + $("#" + tbRowName[tableIndex] + "_" + i + "_" + key).val().replace(',', '') + ",";
            else
                str = str + "\"" + escape($("#" + tbRowName[tableIndex] + "_" + i + "_" + key).val()) + "\",";

            j++;
        }

        str = str.substring(0, str.length - 1);
        jtr = jtr + str;
        if (i == RowCount)
            jtr = jtr + "}";
        else
            jtr = jtr + "},";

        json = json + jtr;
    }
    json = json + "]";
    return json;

}
//在修改時，將後臺明細資料，賦值到所新增的行中。
function BindJsonToDetail(tableIndex, data) {
var aa ="";
aa.toLowerCase ()
    for (var i = 0; i < data.length; i++) {
        var j = 1;
        var n = i + 1;
        for (key in data[i]) {
            if (tbColType[tableIndex][key] != null) {
                var tbColTypeString = tbColType[tableIndex][key].toString().toLowerCase();
                if (tbColTypeString == "check") {
                    if (data[i][key].toString() == "true")
                        $("#" + tbRowName[tableIndex] + "_" + n + "_" + key).attr('checked', 'checked');
                }
                else if (tbColTypeString.indexOf("numeric_q") >= 0)
                    $("#" + tbRowName[tableIndex] + "_" + n + "_" + key).val(formatNumber(unescape(data[i][key].toString()), strDataFormat[0]));
                else if (tbColTypeString.indexOf("numeric_p") >= 0)
                    $("#" + tbRowName[tableIndex] + "_" + n + "_" + key).val(formatNumber(unescape(data[i][key].toString()), strDataFormat[1]));
                else if (tbColTypeString.indexOf("numeric_m") >= 0)
                    $("#" + tbRowName[tableIndex] + "_" + n + "_" + key).val(formatNumber(unescape(data[i][key].toString()), strDataFormat[2]));
                else if (tbColTypeString.indexOf("numeric_r") >= 0)
                    $("#" + tbRowName[tableIndex] + "_" + n + "_" + key).val(formatNumber(unescape(data[i][key].toString()), strDataFormat[3]));
                else if (key.trim().toLowerCase().substring(key.length, key.length - 4) == "date") {
                $("#" + tbRowName[tableIndex] + "_" + n + "_" + key).val(GetDateToString(strDateFormat, new Date(data[i][key])));
                $("#" + tbRowName[tableIndex] + "_" + n + "_" + key).attr("datevalue", data[i][key]);
                }
                else if (key.trim().toLowerCase().substring(key.length, key.length - 2) == "ym") {
                $("#" + tbRowName[tableIndex] + "_" + n + "_" + key).val(GetYmToString(strDateFormat, data[i][key]));
                $("#" + tbRowName[tableIndex] + "_" + n + "_" + key).attr("datevalue", data[i][key]);
                }
                else//set to input $("#" + tbRowName[tableIndex] + "_" + n + "_" + key).val("")
                    if (data[i][key] != null) $("#" + tbRowName[tableIndex] + "_" + n + "_" + key).val(unescape(data[i][key].toString()));
                
                if (j < fixCols[tableIndex]) {
                    if (tbColTypeString == "check") {
                        if (data[i][key].toString() == "true")
                            $("#" + tbRowName[tableIndex] + "F_" + n + "_" + key).attr('checked', 'checked');
                    }
                    else if (tbColTypeString.indexOf("numeric_q") >= 0)
                        $("#" + tbRowName[tableIndex] + "F_" + n + "_" + key).val(formatNumber(unescape(data[i][key].toString()), strDataFormat[0]));
                    else if (tbColTypeString.indexOf("numeric_p") >= 0)
                        $("#" + tbRowName[tableIndex] + "F_" + n + "_" + key).val(formatNumber(unescape(data[i][key].toString()), strDataFormat[1]));
                    else if (tbColTypeString.indexOf("numeric_m") >= 0)
                        $("#" + tbRowName[tableIndex] + "F_" + n + "_" + key).val(formatNumber(unescape(data[i][key].toString()), strDataFormat[2]));
                    else if (tbColTypeString.indexOf("numeric_r") >= 0)
                        $("#" + tbRowName[tableIndex] + "F_" + n + "_" + key).val(formatNumber(unescape(data[i][key].toString()), strDataFormat[3]));
                    else 
                        $("#" + tbRowName[tableIndex] + "F_" + n + "_" + key).val(unescape(data[i][key].toString()));

                }
                j++;
            }
        }
    }
}

//綁定明細中日期欄位的值。
//年月 BindDateValue(obj, value, true,callback1)
function BindDateValue(obj, value, blYm) {
    if (obj[0] != undefined) {

        if (blYm) {
            obj.val(GetYmToString(strDateFormat, value));
            obj.attr("datevalue", GetYmToString(strDateFormat, value));
        }
        else {
            obj.val(GetDateToString(strDateFormat, value));
            obj.attr("datevalue", GetDateToString(strDateFormat, value));
        }       
     
    }

}

function BindDateEvent(obj, blYm) {
    if (obj[0] != undefined) {
        var name = obj[0].id;

        var callback1 = function () { };

        if (arguments[2] != null) {
            callback1 = arguments[2]
        }

        $("#" + name).calendar({
            DateFormat: strDateFormat,
            Ym: blYm,      //年月標記
            isSub: true, //明細票記
            callback: callback1
        });
        //        obj.bind('blur', function() { Validate(strDateFormat, this); });
        //        $("#" + name + "_btnDate").bind('click', function() { return showCalendar(name, strDateFormat); });
    }

}
//第二个参数tableIndex
//第三个参数行数

function doOption(flag) {
    var tableIndex;
    var addRowCount = 1;
    var tabIndex = 0;
    
    var extender = $find("TabContainer1"); //get a reference to the extender
    if (extender != null) {
        var tabPanel = extender.TabPanel; //gets the ExtJs class
        
        for (var i = 0; i < tabPanel.items.items.length; i++) {
            if (tabPanel.items.get(i).isVisible()) {
                tabIndex = i;
                break;
            }
        }
        var tab1 = $("#" + tabPanel.items.get(tabIndex).id);
        var dd = tab1[0].getElementsByTagName('div');
        for (var t = 0; t < dd.length; t++) {
            var name = dd[t].id;
            if (name.indexOf('div_') >= 0) {
                var dname = name.substring(4);
                for (var tSub = 0; tSub < tbTableId.length; tSub++) {
                    if (tbTableId[tSub] == dname) {
                        tableIndex = tSub;
                        break;
                    }
                }
                
                break;
            }
        }
        if (tableIndex == undefined) {
            alert("請切換至執行該動作的頁簽！");
            return;
        }    
    }
    if (tableIndex == null)
        tableIndex = 0;
    if (arguments[1] != null)
        addRowCount = arguments[1];
    if (arguments[2] != null)
        tableIndex = arguments[2];  
    
    switch(flag)
    {
        case "Add":
            AddDetailAndBindHandle(tableIndex, addRowCount);
            break;
        case "Del":
            delRow(tableIndex);
            break;
        case "Ins":
            AddDetailAndBindHandle(tableIndex, addRowCount, 1);
            break;
        case "Sel"://按鈕點擊
            return GetData(tableIndex, 1);
            //break;
        case "Dbl"://明細雙擊多選
            return GetData(tableIndex, 1, 1);
            //break;
        case "Sgl": //明細雙擊單選
            return GetData(tableIndex, 0, 1);
            //break;
        default:
            break;
    }
}
function isExistsInput(tableIndex, pCtlNameList) {
    var tableF = $("#" + tbTableId[tableIndex] + "_sFDataTable");
    var rowIndex = GetSelectedRow(tableF);
    var rowCount = tableF.find('tr').length - 1;
    
    var pCtlName = pCtlNameList.split(',');

    var isExists = false;
    for (var j = 1; j <= rowCount; j++) {
        if (j != rowIndex) {
            //判斷現存明細中是否存在跟取回資料一致的記錄
            for (var k = 0; k < pCtlName.length; k++) {
                if ($.trim($("#" + tbRowName[tableIndex] + "_" + j + "_" + pCtlName[k]).val().toLowerCase()) == $.trim($("#" + tbRowName[tableIndex] + "_" + rowIndex + "_" + pCtlName[k]).val().toLowerCase())) {
                    isExists = true;
                }
                else {
                    isExists = false;
                    break;
                }
            }
            //如果存在不用繼續判斷
            if (isExists) {
                for (var k = 0; k < pCtlName.length; k++) {
                    $("#" + tbRowName[tableIndex] + "_" + rowIndex + "_" + pCtlName[k]).val("");

                }
                break;
            }            
        }
    }
    return isExists;
}
//過濾明細中重複的數據
function isExistsInDetail(tableIndex, strReturn, pFldNameList) {
    if (strReturn == null || strReturn.length == 0)
        return null;
    ////子檔驗證提示
    var table = $("#" + tbTableId[tableIndex] + "_sDataTable");
    var currowindex = GetSelectedRow($("#" + tbTableId[tableIndex] + "_sFDataTable"));
    var rowCount = table.find('tr').length - 1;
    var pFldName = pFldNameList.split(',');
    var pCtlName = pFldName;
    if (arguments[3] != null) {
        pCtlName = arguments[3].split(',');
    }    
    
    var strFilter = new Array();
    var f = 0;
    for (var i = 0; i < strReturn.length; i++) {
        var isExists = false;
        for (var j = 0; j < rowCount; j++) {
            var rowIndex = j + 1;
            //判斷現存明細中是否存在跟取回資料一致的記錄
            for (var k = 0; k < pFldName.length; k++) {
                if (currowindex != rowIndex && $.trim($("#" + tbRowName[tableIndex] + "_" + rowIndex + "_" + pCtlName[k]).val()) == unescape(strReturn[i][pFldName[k]].toString())) {
                    isExists = true;
                }
                else {
                    isExists = false;
                    break;
                }
            }
            //如果存在不用繼續判斷
            if (isExists)
                break;
        }
        //明細中不存在的記錄寫入數組
        if (!isExists) {
            strFilter[f] = strReturn[i];
            f++;
        }
    }
    return strFilter;
}

//tableIndex：明細Index;
//strReturn:選擇資料，取回的值。
//fldNameList:欄位
//若傳遞第四個參數，表示明細中雙擊選取。

function setData(tableIndex, strReturn, fldNameList) {
    if (strReturn == null || strReturn.length==0)
        return false;
    var addRowIndex, rowIndex;
    var fldName = fldNameList.split(",");
    if (arguments[3] != null) {
        var tableF = $("#" + tbTableId[tableIndex] + "_sFDataTable");
        rowIndex = GetSelectedRow(tableF);
       
        addRowIndex = AddDetailAndBindHandle(0, strReturn.length - 1, 1);
    }
    else {
        addRowIndex = AddDetailAndBindHandle(tableIndex, strReturn.length);
    }
    var Len;
    if (arguments[3] != null) {
        for (var j = 0; j < fldName.length; j++) {
            
            var tbColTypeString = tbColType[tableIndex][fldName[j]].toString().toLowerCase();
            var currValue = unescape(strReturn[0][fldName[j]].toString());
            if (tbColTypeString.indexOf("numeric_q") >= 0)
                $("#" + tbRowName[tableIndex] + "_" + rowIndex + "_" + fldName[j]).val(formatNumber(currValue, strDataFormat[0]));
            else if (tbColTypeString.indexOf("numeric_p") >= 0)
                $("#" + tbRowName[tableIndex] + "_" + rowIndex + "_" + fldName[j]).val(formatNumber(currValue, strDataFormat[1]));
            else if (tbColTypeString.indexOf("numeric_m") >= 0)
                $("#" + tbRowName[tableIndex] + "_" + rowIndex + "_" + fldName[j]).val(formatNumber(currValue, strDataFormat[2]));
            else if (tbColTypeString.indexOf("numeric_r") >= 0)
                $("#" + tbRowName[tableIndex] + "_" + rowIndex + "_" + fldName[j]).val(formatNumber(currValue, strDataFormat[3]));
            else if (tbColTypeString == "dropdown" && (currValue == "True" || currValue == "False"))
                $("#" + tbRowName[tableIndex] + "_" + rowIndex + "_" + fldName[j]).val(currValue .toLowerCase());
            else
                $("#" + tbRowName[tableIndex] + "_" + rowIndex + "_" + fldName[j]).val(currValue);
            //$("#" + tbRowName[tableIndex] + "_" + rowIndex + "_" + fldName[j]).val(unescape(strReturn[0][fldName[j]].toString()));
        } 
        Len = addRowIndex.length - 1;
    }
    else
        Len = addRowIndex.length;
    for (var i = 0; i < Len; i++) {
        var d, index;
        if (arguments[3] != null) {
            d = rowIndex + i + 1;
            index = i + 1;
            
        }
        else {
            d = addRowIndex[i];
            index = i;
        }

        for (var j = 0; j < fldName.length; j++) {
            var tbColTypeString = tbColType[tableIndex][fldName[j]].toString().toLowerCase();
            var currValue = unescape(strReturn[index][fldName[j]].toString());
            if (tbColTypeString.indexOf("numeric_q") >= 0)
                $("#" + tbRowName[tableIndex] + "_" + d + "_" + fldName[j]).val(formatNumber(currValue, strDataFormat[0]));
            else if (tbColTypeString.indexOf("numeric_p") >= 0)
                $("#" + tbRowName[tableIndex] + "_" + d + "_" + fldName[j]).val(formatNumber(currValue, strDataFormat[1]));
            else if (tbColTypeString.indexOf("numeric_m") >= 0)
                $("#" + tbRowName[tableIndex] + "_" + d + "_" + fldName[j]).val(formatNumber(currValue, strDataFormat[2]));
            else if (tbColTypeString.indexOf("numeric_r") >= 0)
                $("#" + tbRowName[tableIndex] + "_" + d + "_" + fldName[j]).val(formatNumber(currValue, strDataFormat[3]));
            else
                $("#" + tbRowName[tableIndex] + "_" + d + "_" + fldName[j]).val(currValue);
                
            //$("#" + tbRowName[tableIndex] + "_" + d + "_" + fldName[j]).val(unescape(strReturn[index][fldName[j]].toString()));
        }
    }
    return true;
}
//tableIndex：明細Index;
//strReturn:選擇資料，取回的值。
//fldNameList:欄位
//若傳遞第五個參數，表示明細中雙擊選取。

function setCtlValue(tableIndex, strReturn, fldNameList,ctlNameList) {
    if (strReturn == null || strReturn.length==0)
        return false;
    var addRowIndex, rowIndex;
    var fldName = fldNameList.split(",");
    var ctlName = ctlNameList.split(",");
    
    if (arguments[4] != null) {
        var tableF = $("#" + tbTableId[tableIndex] + "_sFDataTable");
        rowIndex = GetSelectedRow(tableF);
        addRowIndex = AddDetailAndBindHandle(tableIndex, strReturn.length - 1, 1);
    }
    else 
        addRowIndex = AddDetailAndBindHandle(tableIndex, strReturn.length);
    
    var Len;
    if (arguments[4] != null) {
        for (var j = 0; j < fldName.length; j++) {
            var tbColTypeString = tbColType[tableIndex][ctlName[j]].toString().toLowerCase();
            var currValue = unescape(strReturn[0][fldName[j]].toString());
            if (tbColTypeString.indexOf("numeric_q") >= 0)
                $("#" + tbRowName[tableIndex] + "_" + rowIndex + "_" + ctlName[j]).val(formatNumber(currValue, strDataFormat[0]));
            else if (tbColTypeString.indexOf("numeric_p") >= 0)
                $("#" + tbRowName[tableIndex] + "_" + rowIndex + "_" + ctlName[j]).val(formatNumber(currValue, strDataFormat[1]));
            else if (tbColTypeString.indexOf("numeric_m") >= 0)
                $("#" + tbRowName[tableIndex] + "_" + rowIndex + "_" + ctlName[j]).val(formatNumber(currValue, strDataFormat[2]));
            else if (tbColTypeString.indexOf("numeric_r") >= 0)
                $("#" + tbRowName[tableIndex] + "_" + rowIndex + "_" + ctlName[j]).val(formatNumber(currValue, strDataFormat[3]));
            else
                $("#" + tbRowName[tableIndex] + "_" + rowIndex + "_" + ctlName[j]).val(currValue);
            //$("#" + tbRowName[tableIndex] + "_" + rowIndex + "_" + ctlName[j]).val(unescape(strReturn[0][fldName[j]].toString()));
        }
        Len = addRowIndex.length - 1;
    }
    else
        Len = addRowIndex.length;
    for (var i = 0; i < Len; i++) {
        var d, index;
        if (arguments[4] != null) {
            d = rowIndex + i + 1;
            index = i + 1;

        }
        else {
            d = addRowIndex[i];
            index = i;
        }

        for (var j = 0; j < fldName.length; j++) {
            var tbColTypeString = tbColType[tableIndex][ctlName[j]].toString().toLowerCase();
            var currValue = unescape(strReturn[index][fldName[j]].toString());
            if (tbColTypeString.indexOf("numeric_q") >= 0)
                $("#" + tbRowName[tableIndex] + "_" + d + "_" + ctlName[j]).val(formatNumber(currValue, strDataFormat[0]));
            else if (tbColTypeString.indexOf("numeric_p") >= 0)
                $("#" + tbRowName[tableIndex] + "_" + d + "_" + ctlName[j]).val(formatNumber(currValue, strDataFormat[1]));
            else if (tbColTypeString.indexOf("numeric_m") >= 0)
                $("#" + tbRowName[tableIndex] + "_" + d + "_" + ctlName[j]).val(formatNumber(currValue, strDataFormat[2]));
            else if (tbColTypeString.indexOf("numeric_r") >= 0)
                $("#" + tbRowName[tableIndex] + "_" + d + "_" + ctlName[j]).val(formatNumber(currValue, strDataFormat[3]));
            else
                $("#" + tbRowName[tableIndex] + "_" + d + "_" + ctlName[j]).val(currValue);
                
            //$("#" + tbRowName[tableIndex] + "_" + d + "_" + ctlName[j]).val(unescape(strReturn[index][fldName[j]].toString()));
        }
    }
}

function getColIndex(col) {
    var tbindex = parseInt(col.split('_')[0].substring(col.split('_')[0].length - 1, col.split('_')[0].length)) - 1;
    var add = 0;
    for (key in tbColHeadText[tbindex]) {
        if (key.toLowerCase() == col.split('_')[2].toLowerCase())
            return add;
        add++;
    }
    return -1;
}

function isShowColumn(col, isShow) {
    if (col == null || col.length == 0) return;
    
    var j = getColIndex(col);

    if (j < 0) return;
    j = j + 2;
    if (isShow) {
        $("#" + col.split('_')[0] + "_sDataTable tr th:nth-child(" + j + ")").css('display', 'block');
        $("#" + col.split('_')[0] + "_sFDataTable tr th:nth-child(" + j + ")").css('display', 'block');
        $("#" + col.split('_')[0] + "_sHeaderTable tr th:nth-child(" + j + ")").css('display', 'block');
        $("#" + col.split('_')[0] + "_sDataTable tr td:nth-child(" + j + ")").css('display', 'block');

    }
    else {

//       var j = 0;
//        for (var i = 0; i < tbColHeadText[0].length; i++) {
//            if (tbColHeadText[0][i] == col) {
//                j = i;
//                break;
//            }
        //        }
        
//       $("#Detail1_sHeaderTable tr td:nth-child(" + j + ")").hide();
//       $("#Detail1_sHeaderTable tr th:nth-child(" + j + ")").hide();
//       $("#Detail1_sFHeaderTable tr td:nth-child(" + j + ")").hide();
//       $("#Detail1_sFHeaderTable tr th:nth-child(" + j + ")").hide();
//       $("#Detail1_sFDataTable tr td:nth-child(" + j + ")").hide();
//       $("#Detail1_sFDataTable tr th:nth-child(" + j + ")").hide();
//       $("#Detail1_sDataTable tr td:nth-child(" + j + ")").hide();
        //       $("#Detail1_sDataTable tr th:nth-child(" + j + ")").hide();

        $("#" + col.split('_')[0] + "_sDataTable tr th:nth-child(" + j + ")").css('display', 'none');
        $("#" + col.split('_')[0] + "_sFDataTable tr th:nth-child(" + j + ")").css('display', 'none');
        $("#" + col.split('_')[0] + "_sHeaderTable tr th:nth-child(" + j + ")").css('display', 'none');       
        $("#" + col.split('_')[0] + "_sDataTable tr td:nth-child(" + j + ")").css('display', 'none');
//    
//        $get(tableId + "_sHeaderTable").getElementsByTagName('th')[thID].style.width = width + 'px';
//        $get(tableId + "_sFHeaderTable").getElementsByTagName('th')[thID].style.width = width + 'px';
//        $get(tableId + "_sDataTable").getElementsByTagName('th')[thID].style.width = width + 'px';
//        $get(tableId + "_sFDataTable").getElementsByTagName('th')[thID].style.width = width + 'px';         
    }
}
//1,000 to 1000
function parseFloatA(num) {
    var v = "" + num;
    var arr = v.split(',');
    v = "";
    for (var i = 0; i < arr.length; i++) {
        v += arr[i];
    }
    return parseFloat(v);    
}