<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BU_RoleEdit.aspx.cs" Inherits="WebUI_Account_BU_RoleEdit" %>

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
            $("#txtRoleName").val(trim($("#txtRoleName").val()));

            if ($("#txtRoleName").val() == "") {
                alert("<%=Resources.Resource.RoleName_NotNull %>");
                $("#txtRoleName").focus();
                return false;
            }

            if ("<%= RoleID%>" == "") {
                var exist = IsExists("<%= FormID%>", $("#txtRoleName").val());
                if (exist) {
                    alert("<%=Resources.Resource.RoleName_Exists %>");
                    $("#txtRoleName").focus();
                    return false;
                }
            }
            return true;
        }
        function Cancel() {
            if (confirm("<%=Resources.Resource.Question_Cancel %>"))
                location.href = "BU_Role.aspx?FormID=<%= FormID%>";
            return false;
        }
        </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table style="width: 100%; height: 20px;" class="OperationBar">
                <tr>
                    <td align="right">                        
                        <asp:Button ID="btnSave" runat="server" Text="保存" CssClass="ButtonSave" 
                            OnClientClick="return Save()" onclick="btnSave_Click" />
                            <asp:Button ID="btnCancel" runat="server" Text="放棄" CssClass="ButtonCancel" OnClientClick="return Cancel()" />
                        <asp:Button ID="btnExit" runat="server" Text="離開" OnClientClick="eExit()" CssClass="ButtonExit" />
                    </td>
                </tr>
       </table>
        <table class="maintable" cellspacing="0" cellpadding="0" width="100%" align="center" bordercolor="#ffffff" border="1" >			

					<tr>
						<td class="musttitle" align="center" >
                            <asp:Literal ID="ltlRoleID" Text="角色ID"
                                runat="server"></asp:Literal>
                        </td>
						<td width="35%" >&nbsp;
								<asp:textbox id="txtRoleID" runat="server" CssClass="TextRead" 
                                Width="90%" MaxLength="20" ReadOnly="True"></asp:textbox>
                        </td>
						
                        <td class="musttitle" width="10%"  align="center" >
                            <asp:Literal 
                                ID="ltlRoleName" Text="角色名稱"
                                runat="server"></asp:Literal>
                        </td>
						<td  width="35%">&nbsp;
								<asp:textbox id="txtRoleName" runat="server" CssClass="TextBox" Width="90%" MaxLength="100" ></asp:textbox>
                         </td>                         
					</tr>
                    <tr>
						<td class="smalltitle" align="center" ><asp:Literal 
                                ID="ltlUserLevel" Text="用戶等級"
                                runat="server"></asp:Literal>
                        </td>
						<td  width="35%">&nbsp;
								<asp:DropDownList runat="server" ID="ddlUserLevel"  Width="90%" >
                                <asp:ListItem Text="一般用戶"></asp:ListItem>
                                <asp:ListItem Text="管理用戶"></asp:ListItem>
                            </asp:DropDownList>
                         </td>
                         <td class="smalltitle" align="center" width="10%">
                        </td>
						<td  width="20%"></td>
					</tr>
                     <tr>
						<td class="smalltitle" align="center"><asp:Literal ID="ltlCreateUserName" Text="建檔人員"
                                runat="server"></asp:Literal></td>
						<td >&nbsp;
								<asp:textbox id="txtCreateUserName" runat="server" CssClass="TextRead" Width="90%" 
                                MaxLength="20" ReadOnly="True"></asp:textbox>
                         </td>
						<td class="smalltitle" align="center">                            
                            <asp:Literal ID="ltlCreateDate" runat="server" Text="建檔日期"></asp:Literal>						
						</td>
						<td>&nbsp;
								<asp:textbox id="txtCreateDate" runat="server" CssClass="TextRead" Width="90%" 
                                MaxLength="20" ReadOnly="True"></asp:textbox></td>
					</tr>
                    <tr>
						<td class="smalltitle" align="center"><asp:Literal ID="ltlLastModifyUserName" Text="異動人員"
                                runat="server"></asp:Literal></td>
						<td >&nbsp;
								<asp:textbox id="txtLastModifyUserName" runat="server" CssClass="TextRead" Width="90%" MaxLength="20" ReadOnly="true" ></asp:textbox>
                        </td>
						<td class="smalltitle" align="center"> <asp:Literal ID="ltlLastModifyDate" Text="異動日期"
                                runat="server"></asp:Literal>							
						</td>
						<td >&nbsp;
                        <asp:textbox id="txtLastModifyDate" runat="server" CssClass="TextRead" Width="90%" 
                                MaxLength="20" ReadOnly="true"></asp:textbox></td>
					</tr>
			</table>
    </div>
    </form>
</body>
</html>
