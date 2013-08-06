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
    //isShowColumn("Detail1_Col_SubID", false);

    $("#btnOK").bind('click', function () {
        if ($("#RadioButton1")[0].checked) {
            for (var i = 1; i <= subData[0].length; i++) {
                $("#" + tbRowName[0] + "_" + i + "_NoDeliverPages").val(formatNumber(0, strDataFormat[0]));
                $("#" + tbRowName[0] + "_" + i + "_CancelPages").val($("#" + tbRowName[0] + "_" + i + "_Pages").val());
                $("#" + tbRowName[0] + "_" + i + "_IsClose").val("true");
                LastModify("" + tbRowName[0] + "_" + i + "_Pages");
            }
        }
        else {
            for (var i = 1; i <= subData[0].length; i++) {
                if ((toNumber($("#" + tbRowName[0] + "_" + i + "_Pages").val()) - toNumber($("#" + tbRowName[0] + "_" + i + "_CancelPages").val())) <= 0)
                    $("#" + tbRowName[0] + "_" + i + "_IsClose").val("true");
                else
                    $("#" + tbRowName[0] + "_" + i + "_IsClose").val("false");
            }
        }
        window.returnValue = GenerateDetailToJson(0);
        window.close(); return false;
    });
    $("#btnCancel").bind('click', function () { window.close(); return false; });

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
        var obj = $("#" + tbRowName[tableIndex] + "_" + RowIndex[i] + "_IsEstimate");
        if (obj[0].length == 0) $(ddlIsEstimate).appendTo(obj);

        //異動日，人員事件
        $("[id*=" + tbRowName[tableIndex] + "_" + RowIndex[i] + "_]").bind('change', function () {
            LastModify(this.id);
        });
        $("#" + tbRowName[tableIndex] + "_" + RowIndex[i] + "_CancelPages").bind("change", function () {
            Count(this);
        });       
        //結束****************


        //初值 插入點後 附初值
        //開始****************
//        var insertpoint = RowIndex.length == 1 ? RowIndex[i] : (RowIndex[BindIndex] == null ? (i + 1) : RowIndex[BindIndex] + i);
//        $("#" + tbRowName[tableIndex] + "_" + insertpoint + "_Quantity").val("1");
//        $("#" + tbRowName[tableIndex] + "_" + insertpoint + "_RealQty").val("1");
//        $("#" + tbRowName[tableIndex] + "_" + insertpoint + "_RemainQty").val("1");
//        //        $("#" + tbRowName[tableIndex] + "_" + insertpoint + "_IsSaveImage")[0].checked = false;
//        //下拉初值  bool(false,true) 注意都為小字
//        $("#" + tbRowName[tableIndex] + "_" + insertpoint + "_IsLimitedProduct").val("false");
//        $("#" + tbRowName[tableIndex] + "_" + insertpoint + "_IsOverOrder").val("false");
//        //日期
//        obj = $("#" + tbRowName[tableIndex] + "_" + insertpoint + "_PreDeliverDate")
//        //        obj.bind("focus", function () { alert(11); });
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

function Count(obj) {
    //alert(1);
    var arr = obj.id.split('_');
    var idhead = obj.id.replace("_" + arr[3], "");
    idhead = idhead + "_";
    
    var objCancelPages = $("#" + idhead + "CancelPages");
    if (objCancelPages.val().indexOf("-") != -1) {
        $("#" + idhead + "CancelPages").val(formatNumber(toNumber(objCancelPages.attr("oldvalue")), strDataFormat[0]));
        return;
    }
    //未交張數
    if ((toNumber($("#" + idhead + "Pages").val()) - toNumber($("#" + idhead + "CancelPages").val())) < 0) {
        $("#" + idhead + "NoDeliverPages").val(formatNumber(0, strDataFormat[0]));
        $("#" + idhead + "CancelPages").val($("#" + idhead + "Pages").val());
    }
    else
        $("#" + idhead + "NoDeliverPages").val(formatNumber(toNumber($("#" + idhead + "Pages").val()) - toNumber($("#" + idhead + "CancelPages").val()), strDataFormat[0]));
}
