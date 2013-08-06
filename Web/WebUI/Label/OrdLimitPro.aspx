<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OrdLimitPro.aspx.cs" Inherits="WebUI_Label_OrdLimitPro" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html >
<head id="Head1" runat="server">
<base target ="_self" />
   <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
  
    <title>限用產品查詢</title> 
   
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
        <script type="text/javascript" src="Js/LimitPro.js"></script>

        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/EditDropDownList/Js/jquery.ui.core.js") %>'></script> 
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/EditDropDownList/Js/jquery.ui.widget.js") %>'></script> 
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/EditDropDownList/Js/jquery.ui.button.js") %>'></script> 
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/EditDropDownList/Js/jquery.ui.position.js") %>'></script> 
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/EditDropDownList/Js/jquery.ui.autocomplete.js") %>'></script> 
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/EditDropDownList/Js/jquery.ui.combobox.js") %>'></script> 
        
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Commons.js") %>'></script>
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Ajax.js") %>'></script>   
        <script type="text/javascript">
            $(document).ready(function () {
//                var arr = window.dialogArguments.split(',');
//                $('#txtBillID').val(arr[0]); $('#txtEnterpriseID').val(arr[1]); $('#txtEnterpriseName').val(arr[2]);

                // content_resize();
                subData[0] = jQuery.parseJSON($('#HdnSubDetail1').val());
                initialTable();
                AddToDetail();
                $("#btnOK").bind('click', function () { window.close(); return false; });
            });

        </script>
    <style type="text/css">
        .style1
        {
            height: 28px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" target="_self"><asp:ScriptManager ID="ScriptManager1" runat="server" />  
    <div>
      <table class="maintable" cellspacing="0" cellpadding="0" width="100%" align="center" borderColor="#ffffff" border="1" >

			<tr>
						        <td class="musttitle" align="center" width="15%" >
                                    <asp:Literal 
                                ID="ltlBillID" Text="訂購單號"
                                runat="server"></asp:Literal></td>
						        <td width="35%" >&nbsp;<asp:textbox id="txtBillID" runat="server" CssClass="TextRead" 
                                ReadOnly ="True" Width="72%" 
                                MaxLength="20" ></asp:textbox>
                                &nbsp;
								</td>
						        <td colspan="2" align="right">
                                <asp:button id="btnOK" runat="server" Text=" 確定" OnClientClick ="return false;" 
                                             CssClass="but" Width="75px" 
                                         >
                                    </asp:button>&nbsp;
                                                    &nbsp;&nbsp;
                                </td>
						        
					</tr>
					<tr>
						<td class="musttitle" align="center" >
                            <asp:Literal ID="ltlStyleName" Text="款式編號"
                                runat="server" meta:resourcekey="ltlStyleNameResource1"></asp:Literal></td>
						<td colspan="3" class="style1">&nbsp;<asp:textbox id="txtStyleID" runat="server" 
                                CssClass="TextRead"  ReadOnly ="True" Width="30%" 
                                MaxLength="20" meta:resourcekey="txtStyleIDResource1" >Q01
                                </asp:textbox>
                                &nbsp;<asp:textbox id="txtLabelMode" runat="server" CssClass="TextRead" 
                                Width="65%" MaxLength="20" ReadOnly="True" 
                                meta:resourcekey="txtStyleNameResource1"></asp:textbox>
								
                        </td>
					</tr>

                    
                  
                   
                    </table>
                     <br />
                     <div>

                         <div id="div_Detail1" class="fakeContainer" style=" margin:5px;height:205px;">
                         </div>
            
                     </div>
                     
    </div><input id="HdnSubDetail1" type="hidden" runat="server" />
    <div id="subColsName1" runat="server">
                    <asp:Literal ID="sub1xxRowID" Text="(序號),80,label,1" Visible="false" runat="server"></asp:Literal>  
                    <asp:Literal ID="sub1xxProductID" Text="(產品編號),100,text,1" Visible="false" runat="server"></asp:Literal>  
                    <asp:Literal ID="sub1xxProductName" Text="(品名規格),200,text,1" Visible="false" runat="server"></asp:Literal>    
                    <asp:Literal ID="sub1xxMemo" Text="(備註),200,text,1" Visible="false" runat="server"></asp:Literal>     
                       
                    </div>
    </form>
</body>
</html>

