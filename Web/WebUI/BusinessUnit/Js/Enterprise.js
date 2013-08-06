var CurrencyItems;

tbTableId[0] = "Detail1";
fixCols[0] = 2;

function initialTable(lang) {
    var row = new Object(); //object的屬性必須為col+index
    if (lang == "zh-tw") {
        row.RowID = "(序號)";
        row.SubID = "原始序號";
        row.LinkMan = "連絡人員";
        row.Post = "連絡職稱";
        row.CellPhone = "行動電話";
        row.Phone = "連絡電話";
        row.EMail = "郵箱地址";
        row.Memo = "備註";
    }
    else if (lang == "zh-cn") {
        row.RowID = "(序号)";
        row.SubID = "原始序号";
        row.LinkMan = "连络人员";
        row.Post = "连络职称";
        row.CellPhone = "手机";
        row.Phone = "连络电话";
        row.EMail = "邮箱地址";
        row.Memo = "备注";
    }
        
    tbColHeadText.push(row);
    
    row = new Object();
    row.RowID = "40";
    row.SubID = "60";
    row.LinkMan = "120";
    row.Post = "200";
    row.CellPhone = "100";
    row.Phone = "100";
    row.EMail = "100";    
    row.Memo = "100";    
    tbColWidth.push(row);

    row = new Object();
    row.RowID = "label";
    row.SubID = "label";
    row.LinkMan = "type=\"text\" maxlength=\"10\"";
    row.Post = "type=\"text\" maxlength=\"16\"";
    row.CellPhone = "type=\"text\" maxlength=\"20\"";
    row.Phone = "type=\"text\" maxlength=\"20\"";
    row.EMail = "type=\"text\" maxlength=\"100\"";
    row.Memo = "type=\"text\" maxlength=\"1000\"";    
    tbColType.push(row);
}


//新增或插入行
//控件事件綁定
function AddDetailAndBindHandle(tableIndex, rowCount) {

    var RowIndex;
    var BindIndex = 0;
    var blnAdd = true;
    if (arguments[2] == undefined) {
        blnAdd = true;
        RowIndex = AddRow(tableIndex, rowCount);
        BindIndex = RowIndex.length;
    }
    else {
        blnAdd = false;
        RowIndex = AddRow(tableIndex, rowCount, 1);
        BindIndex = RowIndex.length - 1; //新增狀況， RowIndex[rowCount]保存插入的位置。
    }
    
    
    for (var i = 0; i < BindIndex; i++) {
        //綁定事件
        var rowIdx = RowIndex[i];
        $("#" + tbRowName[tableIndex] + "_" + RowIndex[i] + "_ProductID").bind("dblclick", function() { doOption('Dbl'); });
        $("#" + tbRowName[tableIndex] + "_" + RowIndex[i] + "_ProductID").bind("change", function() { ProductIDChange(this); });
        
        $("#" + tbRowName[tableIndex] + "_" + RowIndex[i] + "_Qty").bind("change", function() {
            SubTotal(this);
        });
        $("#" + tbRowName[tableIndex] + "_" + RowIndex[i] + "_Price").bind("change", function() {
            SubTotal(this);
        });
        $("#" + tbRowName[tableIndex] + "_" + RowIndex[i] + "_Discount").bind("change", function() {
            SubTotal(this);
        });
        $("#" + tbRowName[tableIndex] + "_" + RowIndex[i] + "_SubTotal").bind("change", function() {
            SubTotal(this);
        });
        $("#" + tbRowName[tableIndex] + "_" + RowIndex[i] + "_Gift").bind("change", function() {
            SubTotal(this);
        });
        
        var obj = $("#" + tbRowName[tableIndex] + "_" + RowIndex[i] + "_PreDeliverDate")
        BindDateValue(obj, new Date($('#cldBillDate_txtDate').attr("datevalue")));
        //$(tableRow + RowIndex[i] + "_CurrID").bind("change", function() { dropDownChange(rowIdx); });

//        if (blnAdd) {//新增狀態中，必須賦值，跟加載下拉框。
//            //賦值
////            var obj = $(tableRow + RowIndex[i] + "_BillDate");
////            BindDateValue(obj, new Date(1912, 1, 1));
////            // BindDateValue(obj, GetStringToDate($('#Calendar1_txtDate').val()));
////            //加載下拉選擇框
////            var sel = $(tableRow + RowIndex[i] + "_CurrencyID");
////            sel.find("option").remove();
////            for (var j = 0; j < CurrencyItems.length; j++) {
////                var op = $("<option value=\"" + CurrencyItems[j]["CurrencyID"].toString() + "\">" + CurrencyItems[j]["IDName"].toString() + "</option>");
////                op.appendTo(sel);
//            }
//        }
//        else {//插入狀態，新插入的行必須賦值，但不要加載下拉框。RowIndex[rowCount]保存插入的位置。

////            var obj = $(tableRow + (RowIndex[rowCount] + i) + "_BillDate");
////            BindDateValue(obj, new Date(2011, 1, 1));
//        }
    }
    return RowIndex;
}
var flag = 'prd';
var fldNameList = 'ProductID,ProductName,Unit';
var pFldNameList = 'ProductID';

function KeyInKeyEnter(obj) {
    var tableIndex = 0;
    
    if (obj.value != null) {

        var strReturn = getAjax2(flag, obj.value, fldNameList, '');
        if (strReturn != null) {
            strReturn = isExistsInDetail(tableIndex, strReturn, pFldNameList);
            setData(tableIndex, strReturn, fldNameList);
            //obj.value = obj.value + " ";
        }
        else {
            alert("此産品編號不存在,請重新輸入！");

            //obj.focus();
        }
        obj.value = "";
    }
}
//選取資料設定
function GetData(tableIndex, option) {

    var strReturn = showSelectWindow(flag, option, getWhere());
    strReturn = isExistsInDetail(tableIndex, strReturn, pFldNameList);

    if (arguments[2] == null)//按鈕點擊，選取資料
        setData(tableIndex, strReturn, fldNameList);
    else
        setData(tableIndex, strReturn, fldNameList, 1); //明細欄位雙擊，選取資料
}
function ProductIDChange(obj) {
    if (!isExistsInput(0, pFldNameList)) {
        GetDetailBaseData(flag, obj, fldNameList);        
    }
    
}
//提交檢驗
function getWhere() {
    return "";
}
function CheckValue() {

    //主檔驗證提示
    if ($.trim($("input[id=txtEnterpriseID]").val()) == "") {
        alert("單號不能為空!");
        $("input[id=txtBillID]").focus();
        return false;
    }
    if ($.trim($("input[id=cldBillDate_txtDate]").val()) == "") {
        alert("日期不能為空!");
        $("input[id=cldBillDate_txtDate]").focus();
        return false;
    }
    if ($.trim($("input[id=txtObjectID]").val()) == "") {
        alert("客戶編號不能為空!");
        $("input[id=txtObjectID]").focus();
        return false;
    }
    if ($.trim($("input[id=txtBusPersonID]").val()) == "") {
        alert("業務人員不能為空!");
        $("input[id=txtBusPersonID]").focus();
        return false;
    }

    if ($.trim($("#ddlCurrencyID").val()) == "") {
        alert("幣別編號不能為空!");
        $("#ddlCurrencyID").focus();
        return false;
    }
    if ($.trim($("#ddlBillType").val()) == "2") {
        if ($.trim($("#txtAgentID").val()) == "") {
            alert("進口廠商不能為空!");
            $("#txtAgentID").focus();
            return false;
        }
    }

    ////子檔驗證提示
    var table = $("#" + tbTableId[0] + "_sDataTable");
    var rowCount = table.find('tr').length - 1;
    if (rowCount == 0) { alert("明細不能為空!"); return false; }
    for (var i = 0; i < rowCount; i++) {
        var rowIndex = i+1;
        if ($.trim($("#" + tbRowName[0] + "_" + rowIndex + "_ProductID").val()) == "") {
            alert("產品編號不能為空!");
            $("#" + tbRowName[0] + "_" + rowIndex + "_ProductID").focus();
            return false;
        }
    }
    var data = GenerateDetailToJson(0);
    $('#HdnSubDetail1').val(data);
    return true;
}

function GetProductData(rowIndex, selectType) {

    var sysId = selectType + '0';
    var strReturn = showdialog(sysId, '21');

    if (strReturn != null) {
        var strFieldName = strReturn[0].split("@|$");
        var strFieldValue = strReturn[1].split("@|$");
        var columnIndex;
        columnIndex = GetColumnIndex(strFieldName, 'Unit');
        if (selectType == 0) {
            //產品編
            //obj.value = strFieldValue[0];
            $(tableRow + rowIndex + "_ProductID").val(strFieldValue[0]);
        }
        else {

            var addRowIndex;
            if (rowIndex > 0)
                addRowIndex = AddDetailAndBindHandle(0, strReturn.length - 2,1);
            else
                addRowIndex = AddDetailAndBindHandle(0, strReturn.length-1,1);
            var table = $("#" + tbTableId[0] + "_sDataTable");

            if (rowIndex > 0) {
                $("#" + tbRowName[0] + "_" + rowIndex + "_ProductID").val(strFieldValue[0]);
                $("#" + tbRowName[0] + "_" + rowIndex + "_ProductName").val(strFieldValue[2]);
                $("#" + tbRowName[0] + "_" + rowIndex + "_Unit").val(strFieldValue[columnIndex]);
            }
            for (var i = 0; i < addRowIndex.length-1; i++) {
                strFieldValue = strReturn[i+2].split("@|$");
                var d = rowIndex + i + 1;
                $("#" + tbRowName[0] + "_" + d + "_ProductID").val(strFieldValue[0]);
                $("#" + tbRowName[0] + "_" + d + "_ProductName").val(strFieldValue[2]);
                $("#" + tbRowName[0] + "_" + d + "_Unit").val(strFieldValue[columnIndex]);
               
            }
        }
    }
    else
        return false;

}

function GetProductJsonData(rowIndex, selectType) {

    var sysId = selectType + '0';
    var strReturn = showdialog(sysId, '21');

    if (strReturn != null) {
        var strFieldName = strReturn[0].split("@|$");
        var strFieldValue = strReturn[1].split("@|$");
        var columnIndex;
        columnIndex = GetColumnIndex(strFieldName, 'Unit');
        if (selectType == 0) {
            //產品編
            //obj.value = strFieldValue[0];
            $(tableRow + rowIndex + "_ProductID").val(strFieldValue[0]);
        }
        else {

            var addRowIndex;
            if (rowIndex > 0)
                addRowIndex = AddDetailAndBindHandle(0, strReturn.length - 2, 1);
            else
                addRowIndex = AddDetailAndBindHandle(0, strReturn.length - 1, 1);
            var table = $("#" + tbTableId[0] + "_sDataTable");

            if (rowIndex > 0) {
                $("#" + tbRowName[0] + "_" + rowIndex + "_ProductID").val(strFieldValue[0]);
                $("#" + tbRowName[0] + "_" + rowIndex + "_ProductName").val(strFieldValue[2]);
                $("#" + tbRowName[0] + "_" + rowIndex + "_Unit").val(strFieldValue[columnIndex]);
            }
            for (var i = 0; i < addRowIndex.length - 1; i++) {
                strFieldValue = strReturn[i + 2].split("@|$");
                var d = rowIndex + i + 1;
                $("#" + tbRowName[0] + "_" + d + "_ProductID").val(strFieldValue[0]);
                $("#" + tbRowName[0] + "_" + d + "_ProductName").val(strFieldValue[2]);
                $("#" + tbRowName[0] + "_" + d + "_Unit").val(strFieldValue[columnIndex]);

            }
        }
    }
    else
        return false;

}


function GetColumnIndex(columnList, fieldName) {
    for (var i = 0; i < columnList.length; i++) {
        if (columnList[i] == fieldName) {
            return i;
        }
    }
    return -1;
}
function BelowSameValue() {
    if (detailCurCell != null) {
        var arr = detailCurCell.id.split('_');
        //        var t = arr[arr.length - 1].toLowerCase();
        //        var rowstr = detailCurCell.id.substring(0, detailCurCell.id.length - t.length - 1);

        var table = $("#" + arr[0] + "_sDataTable");
        var Rows = table.find('tr').length;
        for (var i = parseInt(arr[2]) + 1; i < Rows; i++) {
            if (arr[3] == "Price" || arr[3] == "SubTotal" || arr[3] == "Qty" || arr[3] == "Discount") {
                $("#" + arr[0] + "_" + arr[1] + "_" + i + "_" + arr[3]).val(detailCurCell.value);
                SubTotal($("#" + arr[0] + "_" + arr[1] + "_" + i + "_" + arr[3])[0]);
            }
            //            if (arr[3] == "Price")
            //                $("#" + arr[0] + "_" + arr[1] + "_" + i + "_" + arr[3]).val(formatNumber(detailCurCell.value, strDataFormat[1]));
            //            else if (arr[3] == "SubTotal")
            //                $("#" + arr[0] + "_" + arr[1] + "_" + i + "_" + arr[3]).val(formatNumber(detailCurCell.value, strDataFormat[2]));
            //            else if (arr[3] == "Qty")
            //                $("#" + arr[0] + "_" + arr[1] + "_" + i + "_" + arr[3]).val(formatNumber(detailCurCell.value, strDataFormat[0]));
        }
        detailCurCell = null;
    }
}

function changeDate() {
    var row = new Object(); //object的屬性必須為col+index          //(不計參數位置)
    row.PrintPrefix = getPrintPrefix();                            //當前頁面如(SCBB_DiscountNote_Add.aspx) 中的 SCBB
    row.modname = "GetAutoCode"                                    //調用web後台方法 
    // row.strValue = 0;                                           //參數 
    row.Flag = 2;                                                  //參數
    row.dTime = $('#cldBillDate_txtDate').attr("datevalue");       //參數
    row.PersonID = $('#txtBusPersonID').val();                     //參數 
    //  row.DepartmentID = "";                                     //參數
    row.ProjectID = $('#ddlProjectID').val();               //參數 
    row.strWhere = "";                                             //參數
    $("input[id=txtBillID]").val(KcAjax("doIIBase", parseXmlPara(row)));  //向webserver post 開始

}

function Calculate() {
    var table = $("#" + tbTableId[0] + "_sDataTable");
    var Rows = table.find('tr').length;

    var num = 0;
    var mon = 0;
    pieces = Rows - 1;
    for (var i = 1; i < Rows; i++) {
        num += parseFloatA($("#" + tbRowName[0] + "_" + i + "_Qty").val());
        mon += parseFloatA($("#" + tbRowName[0] + "_" + i + "_SubTotal").val());

    }
    var tax = 0;
    if ($("#ddlTaxType").val() == "0")
        tax = mon * ord.OrderTaxRate;
    $("#txtTaxTotal_M").val(formatNumber(tax, strDataFormat[2]));
    $("#txtQtyTotal_Q").val(formatNumber(num, strDataFormat[0]));
    $("#txtTotal_M").val(formatNumber(mon, strDataFormat[2]));
    $("#txtSumTotal_M").val(formatNumber(mon + tax, strDataFormat[2]));
}

function SubTotal(obj) {
    var arr = obj.id.split('_');
    var t = arr[arr.length - 1].toLowerCase();
    var rowstr = obj.id.substring(0, obj.id.length - t.length - 1);
    if ($("#" + rowstr + "_Gift").attr("checked")) {
        if (t == "price") {
            $("#" + rowstr + "_Price").val(formatNumber(0, strDataFormat[1]));
        }
        if (t == "subtotal") {
            $("#" + rowstr + "_SubTotal").val(formatNumber(0, strDataFormat[2]));
        }
        if (t == "gift") {
            $("#" + rowstr + "_Price").val(formatNumber(0, strDataFormat[1]));
            $("#" + rowstr + "_SubTotal").val(formatNumber(0, strDataFormat[2]));
        }
        Calculate();
        return; //贈品不計算
    }
    var num = parseFloatA($("#" + rowstr + "_Qty").val());
    var price = parseFloatA($("#" + rowstr + "_Price").val());
    var dis = parseFloatA($("#" + rowstr + "_Discount").val()) / 100;
    var mon = parseFloatA($("#" + rowstr + "_SubTotal").val());
    if (t == "subtotal") {//反算單價
        if (num * dis == 0)//除以0
        {
            $("#" + rowstr + "_Price").val(formatNumber(0, strDataFormat[1]));
            $("#" + rowstr + "_SubTotal").val(formatNumber(0, strDataFormat[2]));
        }
        else
            $("#" + rowstr + "_Price").val(formatNumber(mon / num / dis, strDataFormat[1]));
    }
    else {
        $("#" + rowstr + "_SubTotal").val(formatNumber(num * price * dis, strDataFormat[2]));
    }
    if (t == "qty")
        $("#" + rowstr + "_NoOutQty").val(formatNumber(num, strDataFormat[0]));
    Calculate();
}