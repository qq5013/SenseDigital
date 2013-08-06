<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SessionTimeOut.aspx.cs" Inherits="WebUI_Start_SessionTimeOut" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">

        function opennew() {
            alert("對不起，操作時限已過，請重新登入！");
            var udswin = window.open("Start.aspx", "", "toolbar=no,status=yes,resizable=no");
            udswin.moveTo(0, 0);
            udswin.resizeTo(window.screen.availWidth, window.screen.availHeight);
            window.opener = null;
            closeWindow();
        }
        function closeWindow() {
            window.open('', '_self', '');
            window.close();
        }
    </script>
</head>
<body onload="opennew()">
    <form id="form1" runat="server">
    <div>
    
    </div>
    </form>
</body>
</html>
