var CurrencyItems;

tbTableId[0] = "Detail1";
fixCols[0] = 2;

function initialTable() {
    //欄位信息
    init_subColsName1();
    //第二個明細
    //init_subColsName2();

    //載入數據
    subData[0] = jQuery.parseJSON($('#HdnSubDetail1').val());
    AddToDetail();

    //固定 隱藏
    //    isShowColumn("Detail1_Col_SubID", false);
    isShowColumn_subColsName1();
    //
    $('#btnAddDetail').bind('click', function () {
//        if ($("#txtEnterpriseID").val() == "") {
//            alert(msg_firstKeyinEnterpriseID);
//            $("#txtEnterpriseID").focus();
//            return;
//        }
        doOption('Add', 1);
        Sub1_Total();
    });
    $('#btnDelDetail').bind('click', function () { doOption('Del'); Sub1_Total(); });
    $('#btnInsDetail').bind('click', function () { doOption('Ins', 1); Sub1_Total(); });
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
        //開始****************
        $("#" + tbRowName[tableIndex] + "_" + RowIndex[i] + "_StyleID").bind("dblclick", function () {
            var oldvalue = this.value;
            var i = doOption('Dbl');
            //異動人員、日期
            var arr = this.id.split('_');
            if (i != null && isEditState && oldvalue != this.value) {
                LastModify("" + arr[0] + "_" + arr[1] + "_" + (parseInt(arr[2]) + i - 1) + "_" + arr[3]);
            }
            Sub1_Total();
        });
        $("#" + tbRowName[tableIndex] + "_" + RowIndex[i] + "_StyleID").bind("change", function () { ProductIDChange(this); Sub1_Total(); });
        //下拉
//        var obj = $("#" + tbRowName[tableIndex] + "_" + RowIndex[i] + "_IsGift");
//        if (obj[0].length == 0) $(ddlIsGift).appendTo(obj);

//        obj = $("#" + tbRowName[tableIndex] + "_" + RowIndex[i] + "_IsClose");
//        if (obj[0].length == 0) $(ddlIsClose).appendTo(obj);

        obj = $("#" + tbRowName[tableIndex] + "_" + RowIndex[i] + "_IsEstimate");
        if (obj[0].length == 0) $(ddlIsEstimate).appendTo(obj);
        //日期綁定
        obj = $("#" + tbRowName[tableIndex] + "_" + RowIndex[i] + "_PreDeliverDate")
        //BindDateEvent(obj, true,callback)綁定日期事件
        BindDateEvent(obj, false, function (n) {
            LastModify(n[0].id);
        });
        //訂購數量
        $("#" + tbRowName[tableIndex] + "_" + RowIndex[i] + "_Pages").bind("change", function () { Count(this); });

        //異動日，人員事件
        $("[id*=" + tbRowName[tableIndex] + "_" + RowIndex[i] + "_]").bind('change', function () {
            LastModify(this.id);
        });
        //結束****************


        //初值 插入點後 附初值
        //開始****************
        var insertpoint = RowIndex.length == 1 ? RowIndex[i] : (RowIndex[BindIndex] == null ? (i + 1) : RowIndex[BindIndex] + i);
        $("#" + tbRowName[tableIndex] + "_" + insertpoint + "_Pages").val("1");
        $("#" + tbRowName[tableIndex] + "_" + insertpoint + "_NoDeliverPages").val("1");
        //下拉初值  bool(false,true) 注意都為小字
//        $("#" + tbRowName[tableIndex] + "_" + insertpoint + "_IsGift").val("false");
//        $("#" + tbRowName[tableIndex] + "_" + insertpoint + "_IsClose").val("false");
        $("#" + tbRowName[tableIndex] + "_" + insertpoint + "_IsEstimate").val("false");
        //日期
        obj = $("#" + tbRowName[tableIndex] + "_" + insertpoint + "_PreDeliverDate")
        BindDateValue(obj, new Date($('#txtBillDate_txtDate').attr("datevalue")), false);

        obj = $("#" + tbRowName[tableIndex] + "_" + insertpoint + "_CreateDate")
        BindDateValue(obj, new Date(), false);
        obj = $("#" + tbRowName[tableIndex] + "_" + insertpoint + "_LastModifyDate")
        BindDateValue(obj, new Date(), false);
        $("#" + tbRowName[tableIndex] + "_" + insertpoint + "_CreateUserName").val(strUserName);
        $("#" + tbRowName[tableIndex] + "_" + insertpoint + "_LastModifyUserName").val(strUserName);
        //結束****************

    }
    return RowIndex;
}
var fldNameList = 'StyleID,StyleName';
var pFldNameList = 'StyleID';

//選取資料設定
function GetData(tableIndex, option) {
    var strReturn = showSelectWindow("LB_Style", cnKey, option, getWhere());
    strReturn = isExistsInDetail(tableIndex, strReturn, pFldNameList);

    if (strReturn == null) return 0;
    if (arguments[2] == null)//按鈕點擊，選取資料
        setData(tableIndex, strReturn, fldNameList);
    else
        setData(tableIndex, strReturn, fldNameList, 1); //明細欄位雙擊，選取資料
    return strReturn.length;
}
function ProductIDChange(obj) {
    if (!isExistsInput(0, pFldNameList)) {
        GetDetailBaseData("LB_Style", cnKey, obj, fldNameList);
        //組合關鍵字  $("#txtEnterpriseID").val() + "_" + obj.value
        //GetDetailBaseDataA("LB_Style", cnKey, obj, $("#txtEnterpriseID").val() + "_" + obj.value, fldNameList);
    }

}
//提交檢驗
function getWhere() {
    return ""; // " EnterpriseID = '"+ $("#txtEnterpriseID").val() +"'";
}

//計算
function Count(obj) {
    //alert(1);
    var arr = obj.id.split('_');
    var idhead = obj.id.replace("_" + arr[3], "");
    idhead = idhead + "_";
    //    switch (arr[3]) {
    //        case "Quantity": //訂購數量 toNumber
    //            //實際數量
    //            $("#" + idhead + "RealQty").val(formatNumber(toNumber(obj.value) - toNumber($("#" + idhead + "CancelQty").val()),strDataFormat[0]));
    //            //備品數量
    //            $("#" + idhead + "SparesQty").val(formatNumber(toNumber($("#" + idhead + "RealQty").val()) * toNumber($("#" + idhead + "SparesPercent").val()) / 100, strDataFormat[0]));
    //            break;
    //        case "SparesPercent": //備品比率
    //            //備品數量
    //            $("#" + idhead + "SparesQty").val(formatNumber(toNumber($("#" + idhead + "RealQty").val()) * toNumber(obj.value) / 100, strDataFormat[0]));            
    //            break;
    //        case "SparesQty": //備品數量
    //            if (toNumber($("#" + idhead + "RealQty").val()) != 0)
    //                $("#" + idhead + "SparesPercent").val(formatNumber(toNumber(obj.value) / toNumber($("#" + idhead + "RealQty").val())*100, strDataFormat[3]));
    //            else
    //                $("#" + idhead + "SparesPercent").val(formatNumber(0, strDataFormat[3]));
    //            break;
    //    }
//    if ($("#" + idhead + "IsClose").val() == "true" || objCancelPages.val().indexOf("-") != -1) {
//        $("#" + idhead + "CancelPages").val(formatNumber(toNumber(objCancelPages.attr("oldvalue")), strDataFormat[0]));
//        return;
//    }

//    //未交張數
//    $("#" + idhead + "NoDeliverPages").val(formatNumber(toNumber($("#" + idhead + "Pages").val()) - toNumber($("#" + idhead + "CancelPages").val()), strDataFormat[0]));

    //合計
    Sub1_Total();
}
//合計
function Sub1_Total() {
    //合計
    var table = $("#" + tbTableId[0] + "_sDataTable");
    var rowCount = table.find('tr').length - 1;
    var num1 = 0, num2 = 0;
    for (var i = 0; i < rowCount; i++) {
        //Detail1_Row_1_Quantity
        num1 += toNumber($("#" + tbTableId[0] + "_Row_" + (i + 1) + "_Pages").val());
    }

    $("#txtTotalPages").val(formatNumber(num1, strDataFormat[0]));
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
            }
            detailCurCell = null;
        }
}
function BindDeliverAddress() {
    var cbo1 = $("#txtDeliverAddress");
    if ($('#txtEnterpriseID').val().length == 0) {
        cbo1.combobox({ source: [], refreshsource: true });
        return;
    }
    var row = new Object();
    row.dll_className = "Js.DAO.Label.OrderDao"; //注意大小寫
    row.dll_ModeName = "historyAddress"; //注意大小寫
    row.FormID = FormID; //初始化參數
    row.cnKey = cnKey; //初始化參數
    row.filter = $("#txtEnterpriseID").val(); // historyAddress 參數                    
    var newone = parseJosnArray(jQuery.parseJSON(jsAjax("doJsDAOMode", parseXmlPara(row))));
    cbo1.combobox({ source: newone, refreshsource: true });
}

function BindDeliverCountry() {
    var cbo1 = $("#txtDeliverCountry");

    var row = new Object();
    row.dll_className = "Js.DAO.Label.OrderDao"; //注意大小寫
    row.dll_ModeName = "historyCountry"; //注意大小寫
    row.FormID = FormID; //初始化參數
    row.cnKey = cnKey; //初始化參數
    row.filter = "";  // 參數                    
    var newone = parseJosnArray(jQuery.parseJSON(jsAjax("doJsDAOMode", parseXmlPara(row))));
    cbo1.combobox({ source: newone, refreshsource: true });
}

//txtDeliverMehtod
function BindDeliverMehtod() {
    var cbo1 = $("#txtDeliverMehtod");

    var row = new Object();
    row.dll_className = "Js.DAO.Label.OrderDao"; //注意大小寫
    row.dll_ModeName = "historyMehtod"; //注意大小寫
    row.FormID = FormID; //初始化參數
    row.cnKey = cnKey; //初始化參數
    row.filter = "";  // 參數                    
    var newone = parseJosnArray(jQuery.parseJSON(jsAjax("doJsDAOMode", parseXmlPara(row))));
    cbo1.combobox({ source: newone, refreshsource: true });
}
