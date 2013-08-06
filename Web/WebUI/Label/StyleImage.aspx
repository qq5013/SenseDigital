<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StyleImage.aspx.cs" Inherits="WebUI_Label_StyleImage" %>

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
                <td width="100%" height="220px" align="center" colspan="2">
                    <asp:Image ID="Image1" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>