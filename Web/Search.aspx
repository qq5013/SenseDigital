<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Search.aspx.cs" Inherits="Search" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <base target="_self" />
    <link href="~/Styles/Main.css" type="text/css" rel="stylesheet" /> 
    <script type="text/javascript">
//        window.onload = function () {
//            if (document.body.scrollWidth > (window.screen.availWidth - 100)) {
//                window.dialogWidth = (window.screen.availWidth - 100).toString() + "px"
//            } else {
//                window.dialogWidth = (document.body.scrollWidth + 50).toString() + "px"
//            }

//            if (document.body.scrollHeight > (window.screen.availHeight - 70)) {
//                window.dialogHeight = (window.screen.availHeight - 50).toString() + "px"
//            } else {
//                window.dialogHeight = (document.body.scrollHeight + 115).toString() + "px"
//            }
//            window.dialogWidth = "400px";
//            window.dialogHeight = "400px";
//            window.dialogLeft = ((window.screen.availWidth - document.body.clientWidth) / 2).toString() + "px"
//            window.dialogTop = ((window.screen.availHeight - document.body.clientHeight) / 2).toString() + "px"
//        }

    </script>
</head>
<body style="overflow:hidden">
    <form id="form1" runat="server">
      
        <table cellspacing="0" cellpadding="0"  border="0" style="height:100%;width:100%;background-color:#dfeff9">
            <tr> 
                <td style="height:5px;"></td>
            </tr>
            <tr>
                <td align="left" style="height:10px;">
                    &nbsp;按照以下欄位模糊查詢:
                </td>
            </tr>
            <tr>
                <td style="height:5px;"></td>
            </tr>
            <tr>
                <td align="center" style="width:98%;">
                  <div style="height:100%;width:100%;overflow:auto;">
                    <asp:placeholder id="PlaceHolder1" runat="server" EnableViewState="true"></asp:placeholder>
                  </div>
                </td>
            </tr>
            <tr>
                <td style="height:10px;"></td>
            </tr>
            <tr>
				<td style="HEIGHT: 30px;" align="center" >
					<asp:button id="btnEnter" runat="server"  CssClass="but" Text="確定" OnClick="btnEnter_Click"></asp:button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
					&nbsp;&nbsp;
					<asp:button id="btnClear" runat="server" CssClass="but" Text="清空" OnClientClick="return clearall()"></asp:button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
					&nbsp;&nbsp;
					<asp:button id="btnCancel" runat="server"  CssClass="but" Text="取消" OnClick="btnCancel_Click" ></asp:button>
				</td>
			</tr>
			 <tr>
                <td style="height:10px;"></td>
            </tr>    
        </table>
        <input id="hdnReturn" type="hidden" name="hdnReturn" runat="server" />
    
    </form>
</body>
</html>
