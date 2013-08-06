<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ImageQuery.aspx.cs" Inherits="WebUI_Label_ImageQuery" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <title>款式圖片查詢</title>
    <link href="~/Styles/Main.css" type="text/css" rel="stylesheet" />
    <link href="~/Styles/op.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table class="maintable" cellspacing="0" cellpadding="0" width="100%" align="center"
            bordercolor="#ffffff" border="1">
            <tr>
                <td class="musttitle" align="center" width="15%">
                    <asp:Literal ID="ltlBillID" Text="訂購單號" runat="server"></asp:Literal>
                </td>
                <td width="85%" colspan="2">
                    &nbsp;<asp:TextBox ID="txtBillID" runat="server" CssClass="TextRead" ReadOnly="true"
                        Width="30%" MaxLength="20"></asp:TextBox>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="musttitle" align="center">
                    <asp:Literal ID="ltlStyleName" Text="款式編號" runat="server"></asp:Literal>
                </td>
                <td>
                    &nbsp;<asp:TextBox ID="txtStyleID" runat="server" CssClass="TextRead" ReadOnly="true"
                        Width="30%" MaxLength="20"></asp:TextBox>
                    &nbsp;<asp:TextBox ID="txtLabelMode" runat="server" CssClass="TextRead" Width="65%"
                        MaxLength="20" ReadOnly="true">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td width="100%" height="220px" align="center" colspan="2">
                    <asp:Image ID="Image1" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
