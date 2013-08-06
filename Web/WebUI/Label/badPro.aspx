<%@ Page Language="C#" AutoEventWireup="true" CodeFile="badPro.aspx.cs" Inherits="WebUI_Label_badPro" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html >
<head id="Head1" runat="server">
   <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
  
    <title>壞品清單</title> 
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
        <script type="text/javascript" src="Js/badPro.js"></script>
        
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
        <table class="maintable" cellspacing="0" cellpadding="0" width="100%" align="center" borderColor="#ffffff" border="1" >
			<tr>
						        <td class="musttitle" align="center" width="15%" ><asp:Literal 
                                ID="ltlBillID" Text="訂購單號"
                                runat="server"></asp:Literal></td>
						        <td width="85%" colspan="2" >&nbsp;<asp:textbox id="txtBillID" runat="server" CssClass="TextRead" 
                                ReadOnly ="true" Width="30%" 
                                MaxLength="20" ></asp:textbox>
                                &nbsp;<asp:button id="btnCheck5" runat="server" Text=" 清單正確" 
                                            CssClass="but" Width="75px"></asp:button>
								</td>
                                <td>
                                <asp:button id="btnCheck4" runat="server" Text=" 離開" 
                                            CssClass="but" Width="50px"   >
                                    </asp:button>&nbsp;
                                </td>
					</tr>			         
             </table>

                            <div id="div_Detail1" class="fakeContainer" style=" margin:5px;height:205px;">
                            </div>
                            
                        <table class="maintable" cellspacing="0" cellpadding="0" width="100%" align="center" borderColor="#ffffff" border="1" >
			<tr>
                        <td width="70%">
                        </td>
						        <td class="smalltitle" align="center" width="15%" ><asp:Literal 
                                ID="Literal1" Text="合計數量"
                                runat="server"></asp:Literal></td>
						        <td >&nbsp;<asp:textbox id="Textbox1" runat="server" CssClass="TextRead" 
                                ReadOnly ="true" Width="90%" 
                                MaxLength="20" ></asp:textbox>
                                &nbsp;</td>
					</tr>			         
             </table>
    <input id="HdnSubDetail1" type="hidden" runat="server" />
    </form>
</body>
</html>
