<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UpImage.aspx.cs" Inherits="WebUI_Label_UpImage" %>

<%@ Register src="../../Controls/Calendar.ascx" tagname="Calendar" tagprefix="uc2" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html >
<head id="Head1" runat="server">
   <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
  
    <title>訂單轉生產單</title> 
  <link href="~/Styles/Main.css" type="text/css" rel="stylesheet" /> 
        <link href="~/Styles/op.css" type="text/css" rel="stylesheet" /> 
        <link href="~/Styles/Detail.css" type="text/css" rel="stylesheet" /> 
        <link href="~/Styles/superTables.css" type="text/css" rel="stylesheet" /> 
        <link rel="stylesheet" type="text/css" href="~/ext-3.3.1/resources/css/ext-all.css" />
 	    <script type="text/javascript" src='<%=ResolveUrl("~/ext-3.3.1/ext-base.js") %>'></script>
        <script type="text/javascript" src='<%=ResolveUrl("~/ext-3.3.1/ext-all.js") %>'></script>

        <script type="text/javascript" src='<%=ResolveUrl("~/Scripts/JQuery/jquery-1.8.2.min.js") %>'></script> 
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Resize.js") %>'></script>  
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Detail.js") %>'></script>
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/superTables.js") %>'></script>
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Scrolltable.js") %>'></script>
        <script type="text/javascript" src="Js/UpImage.js"></script>

        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/EditDropDownList/Js/jquery.ui.core.js") %>'></script> 
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/EditDropDownList/Js/jquery.ui.widget.js") %>'></script> 
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/EditDropDownList/Js/jquery.ui.button.js") %>'></script> 
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/EditDropDownList/Js/jquery.ui.position.js") %>'></script> 
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/EditDropDownList/Js/jquery.ui.autocomplete.js") %>'></script> 
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/EditDropDownList/Js/jquery.ui.combobox.js") %>'></script> 
        
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Commons.js") %>'></script>
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Ajax.js") %>'></script>   
        <script type="text/javascript">
            var strDateFormat = "y/MM/dd";
            $(document).ready(function () {
                //                var arr = window.dialogArguments.split(',');
                //                $('#txtBillID').val(arr[0]); $('#txtEnterpriseID').val(arr[1]); $('#txtEnterpriseName').val(arr[2]);

                content_resize();
                subData[0] = jQuery.parseJSON($('#HdnSubDetail1').val());
                initialTable();
                AddToDetail();
                //                $('#btnAddDetail').bind('click', function () { doOption('Add', 1); });
                //                $('#btnDelDetail').bind('click', function () { doOption('Del'); });
                //                $('#btnInsDetail').bind('click', function () { doOption('Ins', 1); });

                //                var tabPanel = new Ext.TabPanel({
                //                    height: 300,
                //                    width: "100%",
                //                    autoTabs: true, //自动扫描页面中的div然后将其转换为标签页
                //                    deferredRender: false, //不进行延时渲染
                //                    activeTab: 0, //默认激活第一个tab页
                //                    animScroll: true, //使用动画滚动效果
                //                    enableTabScroll: true, //tab标签超宽时自动出现滚动按钮
                //                    applyTo: 'tabs'
                //                });
            });

        </script>
</head>
<body>
    <form id="form1" runat="server">
  <asp:ScriptManager ID="ScriptManager1" runat="server" /> 
     <table cellspacing="0" cellpadding="0" width="100%" align="center"   >

						<tr>
						<td align="center" width="15%" ><asp:Literal ID="ltlStyleID" Text="入庫單號:"
                                runat="server"></asp:Literal></td>
						<td  Width="35%" >
								&nbsp;<asp:textbox id="txtStyleID" runat="server" CssClass="Textbox" 
                                Width="90%" MaxLength="20" ReadOnly="true"></asp:textbox>
								~</td><td  Width="15%" align ="center">
								<asp:Literal ID="ltlStyleID0" Text="入庫單號:"
                                runat="server"></asp:Literal>
								</td>
                                <td width="35%">  &nbsp;<asp:textbox id="Textbox3" runat="server" CssClass="Textbox" 
                                Width="30%" MaxLength="20" ReadOnly="true"></asp:textbox>
                                          <asp:textbox id="Textbox4" runat="server" CssClass="Textbox" 
                                Width="60%" MaxLength="20" ReadOnly="true"></asp:textbox>         
                                                    </td>
                        </tr>
                        <tr>
                     <td align="center"><asp:Literal ID="Literal2" Text="企業編號:"
                                runat="server"></asp:Literal></td>
                                <td colspan = "3">
								    &nbsp;<asp:textbox id="txtStyleID0" runat="server" CssClass="Textbox" 
                                Width="30%" MaxLength="20" ReadOnly="true"></asp:textbox>
								 
                                    <asp:textbox id="txtStyleID1" runat="server" CssClass="Textbox" 
                                Width="65%" MaxLength="20" ReadOnly="true"></asp:textbox>
								 
                                </tr>
                     <tr>

                     <td align="center"><asp:Literal ID="Literal3" Text="檔案目錄:"
                                runat="server"></asp:Literal></td>
                                <td colspan ="3">
								    &nbsp;<asp:textbox id="txtStyleID2" runat="server" CssClass="Textbox" 
                                Width="96%" MaxLength="20" ReadOnly="true"></asp:textbox>
								</td><td>
								&nbsp;</td><td>&nbsp; </td>
                                </tr>

                                <tr><td></td>
                                <td>
								<asp:button id="btnCheck3" runat="server" Text=" 清除清單" 
                                            CssClass="but" Width="75px"  >
                                    </asp:button>
                        &nbsp;<asp:button id="btnCheck4" runat="server" Text=" 以下全清" 
                                            CssClass="but" Width="75px"   >
                                    </asp:button>
                        &nbsp;<asp:button id="btnCheck5" runat="server" Text=" 區間清除" 
                                            CssClass="but" Width="75px"   >
                                    </asp:button>
                                    </td>
                                <td></td>
                                <td>
								<asp:button id="btnCheck1" runat="server" Text=" 瀏覽檔案" 
                                            CssClass="but" Width="75px"   >
                                    </asp:button>
                        &nbsp;<asp:button id="btnCheck2" runat="server" Text=" 上傳" 
                                            CssClass="but" Width="75px"   >
                                    </asp:button>
                                    </td></tr>

                                <tr><td colspan ="4" align="right">
								    <div id="div_Detail1" class="fakeContainer" style=" margin:5px;height:205px;">
                                    </div>
                                    </td></tr>
                                    <tr><td align ="center">
                                        <asp:Literal ID="Literal4" Text="檔案目錄:"
                                runat="server"></asp:Literal></td>
                                    <td><asp:textbox id="txtStyleID3" runat="server" CssClass="Textbox" 
                                Width="90%" MaxLength="20" ReadOnly="true"></asp:textbox>
								        </td>
                                    <td align ="center"></td>
                                    <td></td></tr>
                        </table>
     <input id="HdnSubDetail1" type="hidden" runat="server" />
    </form>
</body>
</html>


