<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>  
        <link href="~/Styles/Main.css" type="text/css" rel="stylesheet" /> 
   <link href="~/Styles/op.css" type="text/css" rel="stylesheet" /> 
    <link rel="stylesheet" type="text/css" href="../ext-3.3.1/resources/css/ext-all.css" />
 	<script type="text/javascript" src="../ext-3.3.1/ext-base.js"></script>
    <script type="text/javascript" src="../ext-3.3.1/ext-all.js"></script>
    <script type="text/javascript" src="../ext-3.3.1/TabCloseMenu.js"></script>

    <script type="text/javascript" src='<%=ResolveUrl("~/Scripts/JQuery/jquery-1.8.3.min.js") %>'></script> 
    
    <script type="text/javascript">
        var tabHeight;
        var tabMaxHeight;
        var resize = false;
        var tabs = null;

        Ext.onReady(function () {

            tabs = new Ext.TabPanel({
                renderTo: 'tabs',
                resizeTabs: true, // turn on tab resizing
                minTabWidth: 115,
                tabWidth: 135,
                enableTabScroll: true,
                layoutOnTabChange: true,
                resizeTabs: true,
                width: '100%',
                height: document.body.offsetHeight - 10,
                //height: Ext.get("tabs").getComputedHeight(),
                defaults: { autoScroll: true, layout: 'fit' },
                plugins: new Ext.ux.TabCloseMenu()
            });

            addTab("main.aspx", "<%=Resources.Resource.Desktop %>", "default");

            $(window).resize(setTabsHeight);
            setTabsHeight();

            getNewMessage();
            setInterval(getNewMessage, 100000)
        });

        var AnnounceID = "";
        function getNewMessage() {
            var theImage = document.getElementById("imgMessage");
            if (theImage.className == "Show")
                return;
            AnnounceID = getAjax();

            if (AnnounceID == null)
                return;
            if (AnnounceID != "")
                theImage.className = "Show";
            else
                theImage.className = "Hide";
        }
        function addTab(url, name, idName) {
            //alert(tabs.items);
            if (tabs.findById(idName) != null) {
                tabs.setActiveTab(idName);
                window.frames["if_" + idName].location.reload();
                return;
            }
            var closeable = true;
            if (name == "<%=Resources.Resource.Desktop %>")
                closeable = false;
            tabs.add({
                title: name,
                id: idName,
                layout: 'fit',
                iconCls: 'tabs',
                html: "<iframe id='if_" + idName + "' src='" + url + "' width='100%' height='100%' frameborder='1' style='overflow:hidden' scrolling='no' ></iframe>",
                closable: closeable
            }).show();
        }
        function delTab() {
            //tabs.remove(id);
            tabs.remove(tabs.getActiveTab().id)
        }
        function setTabsHeight(top) {
            if (resize) {
                resize = false;
                return;
            }

            resize = true;
            if (top) {
                
                if (tabMaxHeight != null) {

                    tabs.setSize(document.body.offsetWidth, tabMaxHeight);                    
                    return;
                }
                tabMaxHeight = document.body.clientHeight - 16
                tabs.setSize(document.body.offsetWidth, tabMaxHeight);
            }
            else {
                if (tabHeight != null) {
                    tabs.setSize(document.body.offsetWidth, tabHeight);
                    return;
                }
                tabHeight = Ext.get("tabs").getComputedHeight() - 22;
                tabMaxHeight = tabHeight + 70;
                tabs.setSize(document.body.offsetWidth, tabHeight);
            }
            resize = false;
        }
        function winclose() {
            if (confirm("<%=Resources.Resource.Question_ExitSystem %>"))
                top.window.close();
        }
        function reLogin() {
            if (confirm("<%=Resources.Resource.Question_Relogin %>"))
                top.document.location = '../WebUI/Start/Login_<%= Session["UserType"]%>1.aspx';
        }
        function desktop() {
            addTab('main.aspx', '<%=Resources.Resource.Desktop %>', 'default');
            return false;
        }
        function getAjax() {
            var temp;

            $.ajax({
                async: false,
                type: "Post",
                url: "../WebService/BaseService.asmx/strAnnounceID",
                //data: "{'Flag':'" + flag + "','strIdValue':'" + escape(idValue) + "','strFieldName':'" + escape(fieldName) + "','strWhere':'" + escape(strWhere) + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    //debugger;
                    //你可以 alert(data.d)看数据返回的格式
                    data = data.d;
                    if (data.length > 0) {
                        temp = data;
                    }
                }
            });
            return temp;
        }
        function readMessage() {
            if (AnnounceID != "")
                addTab('../WebUI/System/ReadMessageView.aspx?FormID=Sys_ReadMessage&ID=' + AnnounceID, '<%=Resources.Resource.Sys_ReadMessage %>', 'tab_0_3');
            
            var theImage = document.getElementById("imgMessage");
            theImage.className = "Hide";           
        }
    </script>
</head>
<body  bgcolor="#F8FCFF" style="width:100%;Height:100%;">
    <form id="form1" runat="server">
<div>

    <table width="100%" border="0" cellpadding="0" cellspacing="0" background="../images/toolsbg.gif">
				<tr>
					<td id="tdUser" width="45%" align="left" style="font-size:13px"><asp:Literal 
                            ID="ltlUserName" runat="server" Text="當前登錄用戶："></asp:Literal>
                            <asp:Literal ID="ltlIP" runat="server" Text="登錄IP："></asp:Literal>
                            <asp:Literal ID="ltlDateTime" runat="server" Text="當前時間："></asp:Literal>
                    </td>
					<td width="55%" align="right">
						<table border="0" cellpadding="0" cellspacing="0">
                            <tr>

								<td width="100px" ><img id="imgMessage" src="../images/top/Message.gif" height="20" class="Hide"
										onclick="readMessage()" alt="" /></td>

                                <td >                                
                                    <asp:Button ID="btnDesk" runat="server" Text="桌面" CssClass="ButtonDesk" Width="60px"  
                                    OnClientClick="return desktop()" />
                                </td>
								<td><img src="../images/top/CRM_19.gif" alt="" /></td>
								<td >
                                     <asp:Button ID="btnRelogin" runat="server" Text="重新登錄" CssClass="ButtonRelogin"  OnClientClick="reLogin()"
                                        meta:resourcekey="btnReloginResource2" Width="80px" />
                                </td>
								<td><img src="../images/top/CRM_19.gif" alt="" /></td>
								<td >
                                   <asp:Button ID="btndown" runat="server" Text="退出系統" CssClass="Buttondown" OnClientClick="winclose()"
                                    meta:resourcekey="btndownResource2" />
                                </td>
							</tr>
						</table>
					</td>
				</tr>
			</table>

        <div id="tabs" style="width:100%;Height:95%;margin-left:0px;margin-top:0px;margin-right:0;">
        </div> 
    </div>
    </form>
</body> 
</html> 
