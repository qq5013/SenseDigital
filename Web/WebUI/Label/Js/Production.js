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

        //初值 插入點後 附初值
        //開始****************
        var insertpoint = RowIndex.length == 1 ? RowIndex[i] : (RowIndex[BindIndex] == null ? (i + 1) : RowIndex[BindIndex] + i);
        var obj = $("#" + tbRowName[tableIndex] + "_" + insertpoint + "_CreateDate")
        BindDateValue(obj, new Date(), false);
        $("#" + tbRowName[tableIndex] + "_" + insertpoint + "_CreateUserName").val(strUserName);
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
//合計
function Sub1_Total() {
    //合計
    var table = $("#" + tbTableId[0] + "_sDataTable");
    var rowCount = table.find('tr').length - 1;
    var num1 = 0, num2 = 0;
    for (var i = 0; i < rowCount; i++) {
        //Detail1_Row_1_Quantity
        num1 += toNumber($("#" + tbTableId[0] + "_Row_" + (i + 1) + "_RealQty").val());
        num2 += toNumber($("#" + tbTableId[0] + "_Row_" + (i + 1) + "_RemainQty").val());
    }

    $("#txtOrderQtyTotal").val(formatNumber(num1, strDataFormat[0]));
    $("#txtProduceQtyTotal").val(formatNumber(num2, strDataFormat[0]));
}