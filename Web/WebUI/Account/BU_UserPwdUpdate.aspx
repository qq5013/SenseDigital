<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BU_UserPwdUpdate.aspx.cs" Inherits="WebUI_Account_BU_UserPwdUpdate" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="~/Styles/Main.css" type="text/css" rel="stylesheet" /> 
    <link href="~/Styles/op.css" type="text/css" rel="stylesheet" /> 

    <script type="text/javascript" src='<%=ResolveUrl("~/Scripts/JQuery/jquery-1.8.3.min.js") %>'></script>
    <script type="text/javascript" src='<%=ResolveUrl("~/JScript/DataProcess.js") %>'></script>
    <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Commons.js") %>'></script>
    <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Ajax.js") %>'></script>        
    <script type="text/javascript">
        function Save() {


            if ($("#txtPassword").val() == "") {
                alert("<%=Resources.Resource.CT_Password_NotNull %>");
                $("#txtPassword").focus();
                return false;
            }
            if ($("#txtConfirmPwd").val() != $("#txtPassword").val()) {
                alert("<%=Resources.Resource.Password_NotSame %>");
                $("#txtConfirmPwd").focus();
                return false;
            }
            var exist = ConfirmPwd('<%=Session["User"] %>',$("#txtOldPassword").val());
            if (!exist) {
                alert("<%=Resources.Resource.OldPassword_NotRight %>");
                $("#txtOldPassword").focus();
                return false;
            }

            return true;
        }        
        </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />  
    <asp:UpdateProgress ID="updateprogress" runat="server" AssociatedUpdatePanelID="updatePanel">
    <ProgressTemplate>            
             <div id="progressBackgroundFilter" style="display:none"></div>
        <div id="processMessage"> Loading...<br /><br />
             <img alt="Loading" src="../../images/main/loading.gif" />
        </div>            
 
        </ProgressTemplate>
 
    </asp:UpdateProgress>  
                <asp:UpdatePanel ID="updatePanel" runat="server" UpdateMode="Conditional">                
                <ContentTemplate>
    <div>
    <table style="width: 100%; height: 20px;" class="OperationBar">
                <tr>
                    <td align="right">                        
                        <asp:Button ID="btnSave" runat="server" Text="保存" CssClass="ButtonSave" 
                            OnClientClick="return Save()" onclick="btnSave_Click" />                            
                        <asp:Button ID="btnExit" runat="server" Text="離開" OnClientClick="Exit()" CssClass="ButtonExit" />
                    </td>
                </tr>
       </table>
        <table class="maintable" cellspacing="0" cellpadding="0" width="100%" align="center" bordercolor="#ffffff" border="1" >			
                    <tr>
						<td class="musttitle" align="center" width="20%" ><asp:Literal ID="ltlOldPassword" Text="原&nbsp;&nbsp;密&nbsp;&nbsp;&nbsp;碼"
                                runat="server"></asp:Literal>
                        </td>
						<td width="80%" >&nbsp;
								<asp:textbox id="txtOldPassword" runat="server" CssClass="TextBox" Width="50%" 
                                MaxLength="15" TextMode="Password"></asp:textbox>
                        </td>
						
					</tr>
					 <tr>
						<td class="musttitle" align="center" width="20%" ><asp:Literal ID="ltlPassword" Text="密&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;碼"
                                runat="server"></asp:Literal>
                        </td>
						<td width="80%" >&nbsp;
								<asp:textbox id="txtPassword" runat="server" CssClass="TextBox" Width="50%" 
                                MaxLength="15" TextMode="Password"></asp:textbox>
                        </td>
						
					</tr>
                    <tr>
                        <td class="musttitle" align="center" width="20%" ><asp:Literal ID="ltlConfirmPwd" Text="密碼確認"
                                runat="server"></asp:Literal>
                        </td>
						<td width="80%" >&nbsp;
								<asp:textbox id="txtConfirmPwd" runat="server" CssClass="TextBox" Width="50%" 
                                MaxLength="15" TextMode="Password"></asp:textbox>
                        </td>
                    </tr>
			</table>
    </div>
    </ContentTemplate>
            </asp:UpdatePanel> 
    </form>
</body>
</html>
