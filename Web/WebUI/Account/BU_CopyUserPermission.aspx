<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BU_CopyUserPermission.aspx.cs" Inherits="WebUI_Account_BU_CopyUserPermission" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>權限複製載入</title>
    <link href="~/Styles/Main.css" type="text/css" rel="stylesheet" /> 
    <link href="~/Styles/op.css" type="text/css" rel="stylesheet" /> 

    <script type="text/javascript" src='<%=ResolveUrl("~/Scripts/JQuery/jquery-1.8.3.min.js") %>'></script>
    <script type="text/javascript" src='<%=ResolveUrl("~/JScript/DataProcess.js") %>'></script>
    <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Commons.js") %>'></script>
    <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Ajax.js") %>'></script>        
    <script type="text/javascript">
        function OK() {
            window.returnValue = $("#ddlUserID").val();
            window.close();
            return false;
        }
        function Cancel() {
            window.returnValue = null;
            window.close();
            return false;
        }
        </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table class="maintable" cellspacing="0" cellpadding="0" width="100%" align="center" bordercolor="#ffffff" border="1" >			

					<tr>
						<td class="musttitle" align="center" ><asp:Literal ID="ltlUserName" Text="登入帳號"
                                runat="server"></asp:Literal>
                        </td>
						<td width="60%" >&nbsp;
								<asp:DropDownList ID="ddlUserID" runat="server" Width="90%">
                            </asp:DropDownList>
                        </td>
						
                                           
					</tr>
                    
                    <tr>
						<td class="smalltitle" align="center" colspan="2">
                         
                             <asp:Button ID="btnOK" runat="server" CssClass="but" Text="確定" OnClientClick="return OK()"
                                 Width="75px" />
                             &nbsp;
                            <asp:Button ID="btnCancel" runat="server" CssClass="but" Text="取消" OnClientClick="return Cancel()"
                                 Width="70px" />
                        </td>
						
					</tr>                    
			</table>
    </div>
    </form>
</body>
</html>

