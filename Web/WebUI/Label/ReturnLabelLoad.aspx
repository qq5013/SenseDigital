<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReturnLabelLoad.aspx.cs" Inherits="WebUI_Label_ReturnLabelLoad" %>


<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html >
<head id="Head1" runat="server">
   <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
  
    <title>入庫序號轉入退貨</title> 
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
        <script type="text/javascript" src="Js/ReturnLabelLoad.js"></script>

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
                $("#btnLoad").bind("click", function () {
                    window.showModalDialog('ReturnLabelLoad.aspx', null, 'dialogHeight:300px;dialogWidth:820px;toolbar=no,status=yes,resizable=no'); return false;
                    return false;
                });
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
    <style type="text/css">
        .style1
        {
            height: 28px;
        }
        .style4
        {
            height: 25px;
        }
        .style5
        {
            height: 27px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server"><asp:ScriptManager ID="ScriptManager1" runat="server" />  
    <div>
     <table style="font-size: 9pt;" cellspacing="0" cellpadding="0" width="100%" align="center"   >

						<tr>
						<td align="center" width="10%" class="style1" ><asp:Literal ID="Literal2" Text="訂單單號:"
                                runat="server"></asp:Literal></td>
						<td  Width="35%" class="style1" >
								&nbsp;<asp:textbox id="txtStyleID" runat="server" CssClass="Textbox" 
                                Width="90%" MaxLength="20" ReadOnly="true"></asp:textbox>
								~</td><td  Width="35%" class="style1"><asp:textbox id="txtStyleID3" runat="server" CssClass="Textbox" 
                                Width="90%" MaxLength="20" ReadOnly="true"></asp:textbox>
								</td>
                                <td width="20%" class="style1"><asp:button id="btnCheck1" runat="server" Text=" 指定" 
                                            CssClass="but" Width="50px"  
                                         >
                                    </asp:button>
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:button id="btnCheck4" runat="server" Text=" 納入" 
                                            CssClass="but" Width="50px"  
                                        >
                                    </asp:button>
                                                    </td>
                        </tr>
                        <tr>
                     <td align="center" class="style4"><asp:Literal ID="ltlStyleID" Text="入庫單號:"
                                runat="server"></asp:Literal></td>
                                <td class="style4">
                                &nbsp;<asp:textbox id="Textbox3" runat="server" CssClass="Textbox" 
                                Width="90%" MaxLength="20" ReadOnly="true"></asp:textbox>
								~
								</td>
                                <td class="style4"><asp:textbox id="Textbox4" runat="server" CssClass="Textbox" 
                                Width="90%" MaxLength="20" ReadOnly="true"></asp:textbox>
								&nbsp;</td>
                                <td class="style4"><asp:button 
                                        id="btnCheck5" runat="server" Text=" 指定" 
                                            CssClass="but" Width="50px"  
                                         >
                                    </asp:button>
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    &nbsp;<asp:button id="btnCheck" runat="server" CssClass="but" Text=" 取回" Width="50px">
                                    </asp:button>
                            </td>
                                </tr>
                     <tr>
                     <td align="center" class="style5"><asp:Literal ID="Literal1" Text="款式編號:"
                                runat="server"></asp:Literal></td>
                                <td class="style5">
								    &nbsp;<asp:textbox id="Textbox1" runat="server" CssClass="Textbox" 
                                Width="90%" MaxLength="20" ReadOnly="true"></asp:textbox>
								~</td><td class="style5">
								<asp:textbox id="Textbox2" runat="server" CssClass="Textbox" 
                                Width="90%" MaxLength="20" ReadOnly="true"></asp:textbox></td>
                         <td class="style5"><asp:button id="btnCheck2" runat="server" Text=" 指定" 
                                            CssClass="but" Width="50px"  >
                                    </asp:button>
                         &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:button id="btnCheck0" runat="server" Text=" 放棄" 
                                            CssClass="but" Width="50px"   >
                                    </asp:button>
                         </td>
                                </tr>
                     <tr><td colspan ="4">
                     <br />
                     <div>

                         <div id="div_Detail1" class="fakeContainer" style=" margin:5px;height:205px;">
                         </div>
            
                     </div>
                     </td> </tr>
                        </table>
    </div><input id="HdnSubDetail1" type="hidden" runat="server" />
    </form>
</body>
</html>



