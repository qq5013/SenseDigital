var CurrencyItems;

tbTableId[0] = "Detail1";
fixCols[0] = 2;

function initialTable() {
//    var row = new Object(); //object的屬性必須為col+index


//    row.RowID = "(序號)";
//    //    row.SubID = "原品序號";
//    row.StyleID = "(款式編號)";
//    row.LabelMode = "(標籤模式)";
//    row.Quantity = "(訂購數量)";
//    row.CancelQty = "(取消數量)";
//    row.RealQty = "(實際數量)";
//    //row.SparesPercent = "備品比率";
//    row.SparesQty = "(備品數量)";
//    row.RemainQty = "(保留數量)";
//    row.StartNo = "(起始序號)";
//    row.EndNo = "(終止序號)";
//    //row.IsLimitedProduct = "產品限定";
//    row.PreDeliverDate = "(原預交日期)";
//    row.InitialPreDeliverDate = "新預交日期";
//    //row.IsOverOrder = "上架超訂";
//    //row.IsOverOrder1 = "(存證圖片)";
//    row.Memo = "備註";
//    row.State = "(狀態)";
//    row.ProduceBillID = "(生產單號)";
//    //    row.InStockBillID = "(入庫單號)";
//    row.InStockQty = "(入庫數量)";
//    row.InStockCancelQty = "(未交數量)";
//    row.OnShelfQty = "(上架數量)";
//    row.OffShelfQty = "(未上架量)";

//    row.CreateDate = "(建檔日期)";
//    row.CreateUserName = "(建檔人員)";
//    row.LastModifyDate = "(異動日期)";
//    row.LastModifyUserName = "(異動人員)";
//    tbColHeadText.push(row);
    //欄位信息
    init_subColsName1();
    //第二個明細
    //init_subColsName2();

    //載入數據
    subData[0] = jQuery.parseJSON($('#HdnSubDetail1').val());
    AddToDetail();

    //固定 隱藏
    //isShowColumn("Detail1_Col_SubID", false);
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
        //下拉
        var obj = $("#" + tbRowName[tableIndex] + "_" + RowIndex[i] + "_IsClose");
        if (obj[0].length == 0) $(ddlIsClose).appendTo(obj);
        //日期綁定
        obj = $("#" + tbRowName[tableIndex] + "_" + RowIndex[i] + "_PreDeliverDate");        
        //BindDateEvent(obj, true,callback)綁定日期事件
        BindDateEvent(obj, false, function (n) {
            var arr = n[0].id.split('_');
            var obj = $("#" + n[0].id.replace(arr[3], "") + "InitialPreDeliverDate")
            BindDateValue(obj, new Date(subData[0][parseInt(arr[2]) - 1]["PreDeliverDate"]), false); //日期付值
            LastModify(n[0].id);
        });
        
        //異動日，人員事件
        $("[id*=" + tbRowName[tableIndex] + "_" + RowIndex[i] + "_]").bind('change', function () {
            LastModify(this.id);
        });
        //結束****************


        //初值 插入點後 附初值
        //開始****************
//        var insertpoint = RowIndex.length == 1 ? RowIndex[i] : (RowIndex[BindIndex] == null ? (i + 1) : RowIndex[BindIndex] + i);
//        $("#" + tbRowName[tableIndex] + "_" + insertpoint + "_Quantity").val("1");
//        $("#" + tbRowName[tableIndex] + "_" + insertpoint + "_RealQty").val("1");
//        $("#" + tbRowName[tableIndex] + "_" + insertpoint + "_RemainQty").val("1");
////        $("#" + tbRowName[tableIndex] + "_" + insertpoint + "_IsSaveImage")[0].checked = false;
//        //下拉初值  bool(false,true) 注意都為小字
//        $("#" + tbRowName[tableIndex] + "_" + insertpoint + "_IsLimitedProduct").val("false");
//        $("#" + tbRowName[tableIndex] + "_" + insertpoint + "_IsOverOrder").val("false");
//        //日期
//        obj = $("#" + tbRowName[tableIndex] + "_" + insertpoint + "_PreDeliverDate")
////        obj.bind("focus", function () { alert(11); });
//        BindDateValue(obj, new Date($('#txtBillDate_txtDate').attr("datevalue")), false);

//        obj = $("#" + tbRowName[tableIndex] + "_" + insertpoint + "_CreateDate")
//        BindDateValue(obj, new Date(), false);
//        obj = $("#" + tbRowName[tableIndex] + "_" + insertpoint + "_LastModifyDate")
//        BindDateValue(obj, new Date(), false);
//        $("#" + tbRowName[tableIndex] + "_" + insertpoint + "_CreateUserName").val(strUserName);
//        $("#" + tbRowName[tableIndex] + "_" + insertpoint + "_LastModifyUserName").val(strUserName);
        //結束****************
    }
    return RowIndex;
}
