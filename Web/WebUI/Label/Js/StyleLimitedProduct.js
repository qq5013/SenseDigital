
tbTableId[0] = "Detail1";
fixCols[0] = 2;

function initialTable() {
    init_subColsName1();
    //第二個明細
    //init_subColsName2();
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
        $("#" + tbRowName[tableIndex] + "_" + RowIndex[i] + "_ProductID").bind("dblclick", function () { doOption('Dbl'); });
        $("#" + tbRowName[tableIndex] + "_" + RowIndex[i] + "_ProductID").bind("change", function () { ProductIDChange(this); });

    }
    return RowIndex;
}
var fldNameList = 'ProductID,ProductName';
var pFldNameList = 'ProductID';

//選取資料設定
function GetData(tableIndex, option) {

    var strReturn = showSelectWindow("EP_Product", "Enterprise", option, getWhere());
    strReturn = isExistsInDetail(tableIndex, strReturn, pFldNameList);

    if (arguments[2] == null)//按鈕點擊，選取資料
        setData(tableIndex, strReturn, fldNameList);
    else
        setData(tableIndex, strReturn, fldNameList, 1); //明細欄位雙擊，選取資料
}
function ProductIDChange(obj) {
    if (!isExistsInput(0, pFldNameList)) {
        GetDetailBaseData("EP_Product", "Enterprise", obj, fldNameList);
    }

}
//提交檢驗
function getWhere() {
    return " EnterpriseID = '" + $("#txtEnterpriseID").val() + "' ";
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

