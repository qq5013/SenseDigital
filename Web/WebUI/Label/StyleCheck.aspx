<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StyleCheck.aspx.cs" Inherits="WebUI_Label_StyleCheck" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html >
<head id="Head1" runat="server">
		<meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
  
    <title></title> 
        <link href="~/Styles/Main.css" type="text/css" rel="stylesheet" /> 
        <link href="~/Styles/op.css" type="text/css" rel="stylesheet" /> 
</head>
<body>
    <form id="form1" runat="server" style ="height :100px; width : 400px" >
    <div style ="height :100px; width : 400px">
    <table  cellspacing="0" cellpadding="0" width="100%" height ="100%" align="center" border="0" >
				<%--	<tr>
						<td  valign="middle" align="left" colspan="2" class="title1" width="100%" >
							<p><asp:Literal ID="ltlTitle" Text="營運單位覆核人員" runat="server"></asp:Literal>  &nbsp;  <asp:Literal ID="Literal1" Text="王小明" runat="server"></asp:Literal></p>
						</td>
					</tr>--%>
						<tr>
						<td align="center" width="20%" ><asp:Literal ID="ltlStyleID" Text="請輸入使用都密碼"
                                runat="server"></asp:Literal></td>
						<td width="35%" >&nbsp;
								<asp:textbox id="txtStyleID" runat="server" CssClass="TextBox" 
                                Width="90%" ></asp:textbox>
                        </td>
                        </tr>
              
                        </table>
    </div>
    </form>
</body>
</html>
