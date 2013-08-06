var CurrencyItems;

tbTableId[0] = "Detail1";
fixCols[0] = 2;

function initialTable() {
    var row = new Object(); //object的屬性必須為col+index

    //row.RowID = "序號";
    row.SubID = "(序號)";
    row.LabelReelNo = "標籤卷編號";
    row.StartNo = "(起始序號)";
    row.EndNo = "終止序號";
    row.Quantity = "數量";
    row.ProductID = "產品編號";
    row.ProductName = "品名規格";
    row.ResumeID = "生產履歷編號";
    row.LogisticsID = "物流資訊編號";
    row.IsCheckImage = "(圖檔檢查狀態)";
    row.EnableDate = "(啟用期限)";
    row.BadQty = "(壞品張數)";
    row.CreateDate = "建檔日期";
    row.CreateUserName = "建檔人員";
    row.LastModifyDate = "異動日期";
    row.LastModifyUserName = "異動人員";
    tbColHeadText.push(row);

    row = new Object();
    row.SubID = 70;
    row.LabelReelNo = 100;
    row.StartNo = 80;
    row.EndNo = 80;
    row.Quantity = 80;
    row.ProductID = 100;
    row.ProductName = 100;
    row.ResumeID = 100;
    row.LogisticsID = 100;
    row.IsCheckImage = 100;
    row.EnableDate = 100;
    row.BadQty = 100;
    row.CreateDate = 100;
    row.CreateUserName = 100;
    row.LastModifyDate = 100;
    row.LastModifyUserName = 100;
    tbColWidth.push(row);

    row = new Object();
    //row.RowID = "label";
    //row.RowID = "type=\"text\" maxlength=\"10\"";
    row.SubID = "type=\"text\" maxlength=\"10\"";
    row.LabelReelNo = "type=\"text\" maxlength=\"10\"";
    row.StartNo = "type=\"text\" maxlength=\"10\"";
    row.EndNo = "type=\"text\" maxlength=\"10\"";
    row.Quantity = "type=\"text\" maxlength=\"10\"";
    row.ProductID = "type=\"text\" maxlength=\"10\"";
    row.ProductName = "type=\"text\" maxlength=\"10\"";
    row.ResumeID = "type=\"text\" maxlength=\"10\"";
    row.LogisticsID = "type=\"text\" maxlength=\"10\"";
    row.IsCheckImage = "type=\"text\" maxlength=\"10\"";
    row.EnableDate = "type=\"text\" maxlength=\"10\"";
    row.BadQty = "type=\"text\" maxlength=\"10\"";
    row.CreateDate = "type=\"text\" maxlength=\"10\"";
    row.CreateUserName = "type=\"text\" maxlength=\"10\"";
    row.LastModifyDate = "type=\"text\" maxlength=\"10\"";
    row.LastModifyUserName = "type=\"text\" maxlength=\"10\"";
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
        //        $("#" + tbRowName[tableIndex] + "_" + RowIndex[i] + "_ProductID").bind("dblclick", function () { doOption('Dbl'); });
        //        $("#" + tbRowName[tableIndex] + "_" + RowIndex[i] + "_ProductID").bind("change", function () { ProductIDChange(this); });

        //        $("#" + tbRowName[tableIndex] + "_" + RowIndex[i] + "_Qty").bind("change", function () {
        //            SubTotal(this);
        //        });
        //        $("#" + tbRowName[tableIndex] + "_" + RowIndex[i] + "_Price").bind("change", function () {
        //            SubTotal(this);
        //        });
        //        $("#" + tbRowName[tableIndex] + "_" + RowIndex[i] + "_Discount").bind("change", function () {
        //            SubTotal(this);
        //        });
        //        $("#" + tbRowName[tableIndex] + "_" + RowIndex[i] + "_SubTotal").bind("change", function () {
        //            SubTotal(this);
        //        });
        //        $("#" + tbRowName[tableIndex] + "_" + RowIndex[i] + "_Gift").bind("change", function () {
        //            SubTotal(this);
        //        });

        //        var obj = $("#" + tbRowName[tableIndex] + "_" + RowIndex[i] + "_PreDeliverDate")
        //        BindDateValue(obj, new Date($('#cldBillDate_txtDate').attr("datevalue")));
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
        var rowIndex = i + 1;
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

