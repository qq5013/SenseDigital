var lastsel;

var mydata = [
                 { id: "1", userName: "polaris", gender: "男", email: "fef@163.com", QQ: "33334444", mobilePhone: "13223423424", birthday: "1985-10-01" },
                 { id: "2", userName: "李四", gender: "女", email: "faf@gmail.com", QQ: "222222222", mobilePhone: "13223423", birthday: "1986-07-01" },
                 { id: "3", userName: "王五", gender: "男", email: "fae@163.com", QQ: "99999999", mobilePhone: "1322342342", birthday: "1985-10-01" },
                 { id: "4", userName: "馬六", gender: "女", email: "aaaa@gmail.com", QQ: "23333333", mobilePhone: "132234662", birthday: "1987-05-01" },
                 { id: "5", userName: "趙錢", gender: "男", email: "4fja@gmail.com", QQ: "22222222", mobilePhone: "1343434662", birthday: "1982-10-01" },
                 { id: "6", userName: "小毛", gender: "男", email: "ahfi@yahoo.com", QQ: "4333333", mobilePhone: "1328884662", birthday: "1987-12-01" },
                 { id: "7", userName: "小李", gender: "女", email: "note@sina.com", QQ: "21122323", mobilePhone: "13220046620", birthday: "1985-10-01" },
                 { id: "8", userName: "小三", gender: "男", email: "oefh@sohu.com", QQ: "242424366", mobilePhone: "1327734662", birthday: "1988-12-01" },
                 { id: "9", userName: "孫先", gender: "男", email: "76454533@qq.com", QQ: "76454533", mobilePhone: "132290062", birthday: "1989-11-21" },
                 { id: "10", userName: "polaris", gender: "男", email: "fef@163.com", QQ: "33334444", mobilePhone: "13223423424", birthday: "1985-10-01" },
                 { id: "11", userName: "李四", gender: "女", email: "faf@gmail.com", QQ: "222222222", mobilePhone: "13223423", birthday: "1986-07-01" },
                 { id: "12", userName: "王五", gender: "男", email: "fae@163.com", QQ: "99999999", mobilePhone: "1322342342", birthday: "1985-10-01" },
                 { id: "13", userName: "馬六", gender: "女", email: "aaaa@gmail.com", QQ: "23333333", mobilePhone: "132234662", birthday: "1987-05-01" },
                 { id: "14", userName: "趙錢", gender: "男", email: "4fja@gmail.com", QQ: "22222222", mobilePhone: "1343434662", birthday: "1982-10-01" },
                 { id: "15", userName: "小毛", gender: "男", email: "ahfi@yahoo.com", QQ: "4333333", mobilePhone: "1328884662", birthday: "1987-12-01" },
                 { id: "16", userName: "小李", gender: "女", email: "note@sina.com", QQ: "21122323", mobilePhone: "13220046620", birthday: "1985-10-01" },
                 { id: "17", userName: "小三", gender: "男", email: "oefh@sohu.com", QQ: "242424366", mobilePhone: "1327734662", birthday: "1988-12-01" },
                 { id: "18", userName: "孫先", gender: "男", email: "76454533@qq.com", QQ: "76454533", mobilePhone: "132290062", birthday: "1989-11-21" },
                 { id: "19", userName: "polaris", gender: "男", email: "fef@163.com", QQ: "33334444", mobilePhone: "13223423424", birthday: "1985-10-01" },
                 { id: "20", userName: "李四", gender: "女", email: "faf@gmail.com", QQ: "222222222", mobilePhone: "13223423", birthday: "1986-07-01" },
                 { id: "21", userName: "王五", gender: "男", email: "fae@163.com", QQ: "99999999", mobilePhone: "1322342342", birthday: "1985-10-01" },
                 { id: "22", userName: "馬六", gender: "女", email: "aaaa@gmail.com", QQ: "23333333", mobilePhone: "132234662", birthday: "1987-05-01" },
                 { id: "23", userName: "趙錢", gender: "男", email: "4fja@gmail.com", QQ: "22222222", mobilePhone: "1343434662", birthday: "1982-10-01" },
                 { id: "24", userName: "小毛", gender: "男", email: "ahfi@yahoo.com", QQ: "4333333", mobilePhone: "1328884662", birthday: "1987-12-01" },
                 { id: "25", userName: "小李", gender: "女", email: "note@sina.com", QQ: "21122323", mobilePhone: "13220046620", birthday: "1985-10-01" },
                 { id: "26", userName: "小三", gender: "男", email: "oefh@sohu.com", QQ: "242424366", mobilePhone: "1327734662", birthday: "1988-12-01" },
                 { id: "27", userName: "孫先", gender: "男", email: "76454533@qq.com", QQ: "76454533", mobilePhone: "132290062", birthday: "1989-11-21" }
                 ];

$(function () {
    $("#gridTable").jqGrid({
        data: mydata,
        datatype: "local",
        height: 'auto',
        width: '100%',
        colNames: ['編號', '用戶名', '性别', '郵箱', 'QQ', '手機號碼', '出生日期'],
        colModel: [
                         { name: 'id', index: 'id', width: 60, sorttype: "int" },
                         { name: 'userName', index: 'userName', width: 90, editable: false, edittype: 'text' },
                         { name: 'gender', index: 'gender', width: 90, editable: false },
                         { name: 'email', index: 'email', width: 125, sorttype: "string", editable: false },
                         { name: 'QQ', index: 'QQ', width: 100, editable: false },
                         { name: 'mobilePhone', index: 'mobilePhone', width: 120, editable: false },
                         { name: 'birthday', index: 'birthday', width: 200, sorttype: "date" }
                 ],
        sortname: 'id',
        sortorder: 'asc',
        //altRows: true,
        altclass: 'ui-priority-secondary',
        viewrecords: true,
        rownumbers: true,
        rowNum: 10,
        rowList: [10, 20, 30],
//        onSelectRow: function (id) {
//            if (id && id !== lastsel) {
//                jQuery('#gridTable').jqGrid('restoreRow', lastsel);
//                jQuery('#gridTable').jqGrid('editRow', id, true);
//                lastsel = id;
//            }
//        },


        gridComplete: function () { //列表生成后,给某一列绑定操作 例如删除操作
            var ids = $("#gridTable").jqGrid('getDataIDs');
            for (var i = 0; i < ids.length; i++) {
                showInfo = "<a href='##' onclick=\"showInfo('" + ids[i] + "');\">" + $("#gridTable").jqGrid('getRowData', ids[i]).id + "</a>";
                $("#gridTable").jqGrid('setRowData', ids[i], { act: showInfo });

                
                jQuery("#gridTable").setRowData(ids[i], false, { height: 30 });
                var link = "<a href='##' onclick=\"addTab('客戶多筆資料','WebUI/BusinessUnit/Persons.aspx');\"><font color='red'>" + $("#gridTable").jqGrid('getRowData', ids[i]).userName + "</font></a>";
                jQuery("#gridTable").setRowData(ids[i], { userName: link });

                $(window).resize(content_resize);
                content_resize();
            }



        },
        postData: { //传递的数据，定义在form中

            //查询所用
            'searchId': function () { return $("#searchId").val(); },
            'searchName': function () { return $("#searchName").val(); }
        },


        pager: '#gridPager',
        page: 1,
        //caption: "我的第一个jqGrid例子",
        loadonce: true
    }).navGrid('#gridPager', { edit: true, edittext: '修改', add: true, addtext: '新增', del: true, deltext: '刪除', searchtext: '查詢' });

    //    for (var i = 0; i <= mydata.length; i++)
    //        jQuery("#gridTable").jqGrid('addRowData', i + 1, mydata[i]);
    $("#multipleSearchDialog").dialog({
        autoOpen: false,
        modal: true,
        resizable: true,
        width: 350,
        title: "自定义多条件查询",
        buttons: {
            "查询": multipleSearch
        }
    });
});
//多条件
var openMultipleSearchDialog = function () {
    $("#multipleSearchDialog").dialog("open");
};
var multipleSearch = function () {
    var rules = "";

    $("tbody tr", "#multipleSearchDialog").each(function (i) {    //(1)从multipleSearchDialog对话框中找到各个查询条件行  
        var searchField = $(".searchField", this).val();    //(2)获得查询字段  
        var searchOper = $(".searchOper", this).val();  //(3)获得查询方式  
        var searchString = $(".searchString", this).val();  //(4)获得查询值  

        if (searchField && searchOper && searchString) { //(5)如果三者皆有值且长度大于0，则将查询条件加入rules字符串  
            rules += ',{"field":"' + searchField + '","op":"' + searchOper + '","data":"' + searchString + '"}';
        }
    });

    if (rules) { //(6)如果rules不为空，且长度大于0，则去掉开头的逗号  
        rules = rules.substring(1);
    }

    //(7)串联好filtersStr字符串  
    var filtersStr = '{"groupOp":"AND","rules":[' + rules + ']}';

    var postData = $("#list").jqGrid("getGridParam", "postData");

    //(8)将filters参数串加入postData选项  
    $.extend(postData, { filters: filtersStr });

    $("#list").jqGrid("setGridParam", {
        search: true    //(9)将jqGrid的search选项设为true  
    }).trigger("reloadGrid", [{ page: 1}]);   //(10)重新载入Grid表格  

    $("#multipleSearchDialog").dialog("close");
};  
function uppage(pgButton) {
    var page = jQuery("#gridTable").jqGrid('getGridParam', 'page');
    jQuery("#gridTable").setGridParam({
        page: page
    }).trigger("reloadGrid");
}
function content_resize() {
    var grid = $("#gridTable");
    var h = $(window).height() - grid.offset().top - 40;
    $('.ui-jqgrid-bdiv').css("height", h);

    var winwidth = $(window).width(); //这里的0.5可以自己定
    $("#gridTable").setGridWidth(winwidth);
}