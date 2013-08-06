<%@ Page Language="C#" AutoEventWireup="true" CodeFile="msgbox.aspx.cs" Inherits="WebUI_Label_msgbox" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <title>提示訊息</title>
    <link href="~/Styles/Main.css" type="text/css" rel="stylesheet" />
    <link href="~/Styles/op.css" type="text/css" rel="stylesheet" /> 
    <script type="text/javascript" src='<%=ResolveUrl("~/Scripts/JQuery/jquery-1.8.2.min.js") %>'></script>
    
    <script type="text/javascript">
        $(document).ready(function () {
            $("#Button1").bind('click', function () {                 
                window.returnValue = true;
                window.close(); return false;
            });
            $("#Button2").bind('click', function () { window.close(); return false; });
        });

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table  cellspacing="0" cellpadding="0" width="100%" align="center"
              >
            <tr>
                <td align="center"  height="50px" style = "color:blue; " >
                    <asp:Literal ID="ltlBillID" Text="**確認是否要切換已估算記錄?" runat="server"></asp:Literal>
                </td>
                
            </tr>
            <tr>
                <td  align="center">
                   <asp:Button ID="Button1" runat="server" Text=" 確認"   Width="75px"></asp:Button>&nbsp;
                    <asp:Button ID="Button2" runat="server" Text="放棄"  Width="75px">
                    </asp:Button>&nbsp;&nbsp;
                </td>
                
            </tr>
        </table>
    </div>

    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    </form>
</body>
</html>
