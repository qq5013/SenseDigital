<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MemoQuery.aspx.cs" Inherits="WebUI_Label_MemoQuery" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html >
<head id="Head1" runat="server">
   <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
  
    <title>生產描述查詢</title> 
        <link href="~/Styles/Main.css" type="text/css" rel="stylesheet" /> 
        <link href="~/Styles/op.css" type="text/css" rel="stylesheet" /> 
</head>
<body style="margin:0px">
    <form id="form1" runat="server">
    <div>
     <table class="maintable" cellspacing="0" cellpadding="0" width="100%" align="center" borderColor="#ffffff" border="1" >

			<tr>
						        <td class="musttitle" align="center" width="15%" ><asp:Literal 
                                ID="ltlBillID" Text="訂購單號"
                                runat="server"></asp:Literal></td>
						        <td width="85%" colspan="2" >&nbsp;<asp:textbox id="txtBillID" runat="server" CssClass="TextRead" 
                                ReadOnly ="true" Width="30%" 
                                MaxLength="20" ></asp:textbox>
                                &nbsp;
								</td>
						        
						        
					</tr>
					<tr>
						<td class="musttitle" align="center" ><asp:Literal ID="ltlStyleName" Text="款式編號"
                                runat="server"></asp:Literal></td>
						<td >&nbsp;<asp:textbox id="txtStyleID" runat="server" CssClass="TextRead"  
                                ReadOnly ="true" Width="30%" 
                                MaxLength="20" ></asp:textbox>
                                &nbsp;<asp:textbox id="txtLabelMode" runat="server" CssClass="TextRead" 
                                Width="65%" MaxLength="20" ReadOnly="true"></asp:textbox>
								
                        </td>
					</tr>                   
                  
                  
                    <tr>
                     <td class="musttitle" align="center" ><asp:Literal ID="Literal1" Text="生產描述"
                                runat="server"></asp:Literal></td>
                     <td width="100%" height="220px">

                         &nbsp;

                         <asp:textbox id="txtDescription" runat="server" CssClass="TextRead" 
                                ReadOnly ="true" Width="96%" Height="98%" TextMode="MultiLine" 
                                 ></asp:textbox>
                                </td>
                     </tr>
             </table>
    </div>
    </form>
</body>
</html>

