var index = 0;
var onlyOpenTitle = "桌面";



function addSubTab() {
    if ($('#dtt').tabs('exists', 'New Tab 2')) {
        $('#dtt').tabs('select', 'New Tab 2');
        return;
    }
    index++;
    $('#dtt').tabs('add', {
        title: 'New Tab ' + index,
        content: '<iframe scrolling="auto" frameborder="0" src="Tab2.aspx" style="width:100%; height:100%"></iframe>',
        iconCls: 'icon-save',
        closable: true,
        tools: [{
            iconCls: 'icon-mini-refresh',
            handler: function () {
                alert('refresh');
            }
        }]
    });
    tabClose();
    //tabCloseEven();
}
function addTab(title, url) {


    if ($('#tabs').tabs('exists', title)) {
        $('#tabs').tabs('select', title); //选中并刷新
        var currTab = $('#tabs').tabs('getSelected');
        var url = $(currTab.panel('options').content).attr('src');
        if (url != undefined && currTab.panel('options').title != 'Home') {
            $('#tabs').tabs('update', {
                tab: currTab,
                options: {
                    content: createFrame(url)
                }
            })
        }
    } else {
        //由于新增tab会是url Load两次，所以用以下方法解决
        var iframe = $('<iframe scrolling="auto" frameborder="0" style="width:100%;height:100%;" />');
        var content = createFrame(url);
        $('#tabs').tabs('add', {
            title: title,
            style: "padding:0px",
            content: iframe,
            closable: true
        });
        iframe.attr("src", url);
    }
    tabClose();
}
function createFrame(url) {
    var s = '<iframe scrolling="auto" frameborder="0"  src="' + url + '" style="width:100%;height:100%;"></iframe>';
    return s;
}
$(function () {
    tabClose();
    tabCloseEven();
    //$('#tabs').tabs('select', '會員資料');

    //$.fn.zTree.init($("#treeDemo"), setting, treeNodes);
})
function setUrl() {
    var str = createFrame('Tab1.aspx');
    var tabindex = $("#tabs").tabs("getTabIndex", $("#tabs").tabs("getTab", '會員資料'));
    tabindex++;
    $("#tabs").find(".tabs-panels .panel:eq(" + tabindex + ") div").html(str);
}
function tabClose() {
    /*双击关闭TAB选项卡*/
    $(".tabs-inner").dblclick(function () {
        var subtitle = $(this).children("span").text();
        $('#tabs').tabs('close', subtitle);
    })

    $(".tabs-inner").bind('contextmenu', function (e) {
        $('#mm').menu('show', {
            left: e.pageX,
            top: e.pageY
        });

        var subtitle = $(this).children("span").text();
        $('#mm').data("currtab", subtitle);

        return false;
    });
}
function tabCloseEven() {
    $('#mm').menu({
        onClick: function (item) {
            closeTab(item.id);
        }
    });
    return false;
}
function closeTab(action) {
    var alltabs = $('#tabs').tabs('tabs');
    var currentTab = $('#tabs').tabs('getSelected');
    var allTabtitle = [];
    $.each(alltabs, function (i, n) {
        allTabtitle.push($(n).panel('options').title);
    })
    switch (action) {
        case "refresh":
            var iframe = $(currentTab.panel('options').content);
            var src = iframe.attr('src');
            $('#tabs').tabs('update', {
                tab: currentTab,
                options: {
                    content: createFrame(src)
                }
            })
            break;
        case "close":
            var currtab_title = currentTab.panel('options').title;
            $('#tabs').tabs('close', currtab_title);
            break;
        case "closeall":
            $.each(allTabtitle, function (i, n) {
                if (n != onlyOpenTitle) {
                    $('#tabs').tabs('close', n);
                }
            });
            break;
        case "closeother":
            var currtab_title = currentTab.panel('options').title;
            $.each(allTabtitle, function (i, n) {
                if (n != currtab_title && n != onlyOpenTitle) {
                    $('#tabs').tabs('close', n);
                }
            });
            break;
        case "closeright":
            var tabIndex = $('#tabs').tabs('getTabIndex', currentTab);
            if (tabIndex == alltabs.length - 1) {
                alert('亲，后边没有啦 ^@^!!');
                return false;
            }
            $.each(allTabtitle, function (i, n) {
                if (i > tabIndex) {
                    if (n != onlyOpenTitle) {
                        $('#tabs').tabs('close', n);
                    }
                }
            });
            break;
        case "closeleft":
            var tabIndex = $('#tabs').tabs('getTabIndex', currentTab);
            if (tabIndex == 1) {
                alert('亲，前边那个上头有人，咱惹不起哦。 ^@^!!');
                return false;
            }
            $.each(allTabtitle, function (i, n) {
                if (i < tabIndex) {
                    if (n != onlyOpenTitle) {
                        $('#tabs').tabs('close', n);
                    }
                }
            });
            break;
        case "exit":
            $('#closeMenu').menu('hide');
            break;
    }
}

//zTree

var setting = {
    //	        async: {
    //	            enable: true,
    //	            url: getUrl
    //	        },
    check: {
        enable: true
    },
    data: {
        simpleData: {
            enable: true  //数据是否采用简单 Array 格式，默认false
        }
    },
    view: {
        expandSpeed: ""
    },
    callback: {
        //beforeExpand: beforeExpand,
        //onAsyncSuccess: onAsyncSuccess,
        //onAsyncError: onAsyncError
    }
};

var zNodes = [
			{ name: "500个节点", id: "1", count: 500, times: 1, isParent: true },
			{ name: "1000个节点", id: "2", count: 1000, times: 1, isParent: true },
			{ name: "2000个节点", id: "3", count: 2000, times: 1, isParent: true }
		];
var zNodes1 = [
			{ name: "500个节点", id: "1", pId: "-1", count: 500, times: 1, isParent: true },
			{ name: "1000个节点", id: "2", pId: "1", count: 1000, times: 1, isParent: true },
			{ name: "2000个节点", id: "3", pId: "2", count: 2000, times: 1, isParent: false },
			{ name: "1000个节点", id: "4", pId: "1", count: 1000, times: 1, isParent: false },
			{ name: "2000个节点", id: "5", pId: "2", count: 2000, times: 1, isParent: false }
		];
var treeNodes = [
             { "id": 1, "pId": 0, "name": "test1" },
             { "id": 11, "pId": 1, "name": "test11" },
             { "id": 12, "pId": 1, "name": "test12" },
             { "id": 111, "pId": 11, "name": "test111" },
         ];
var zNode2 = [
                        { name: "手机", open: false, checked: true, id: "001", parentId: "-1", code: "001",
                            nodes: [
                        { name: "诺基亚", id: "001001", parentId: "001", code: "001001" },
                     { name: "三星", id: "001002", parentId: "001", code: "001002" },
                     { name: "索爱", id: "001003", parentId: "001", code: "001003" },
                     { name: "多普达", id: "001004", parentId: "001", code: "001004" }
                     ]
                        },
                     { name: "电脑", open: true, id: "002", parentId: "-1", code: "002",
                         nodes: [
                     { name: "硬件", id: "002001", parentId: "002", code: "002001" },
                     { name: "整机", id: "002002", parentId: "002", code: "002002" },
                     { name: "网络", id: "002003", parentId: "002", code: "002003" }
                     ]
                     },
                     { name: "家电", open: false, id: "003", parentId: "-1", code: "003",
                         nodes: [
                     { name: "电视", id: "003001", parentId: "003", code: "003001" },
                     { name: "冰箱", id: "003002", parentId: "003", code: "003002" },
                     { name: "空调", id: "003003", parentId: "003", code: "003003" }
                     ]
                     }
                     ];