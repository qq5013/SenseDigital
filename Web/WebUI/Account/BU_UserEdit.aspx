<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BU_UserEdit.aspx.cs" Inherits="WebUI_Account_BU_UserEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="~/Styles/Main.css" type="text/css" rel="stylesheet" /> 
    <link href="~/Styles/op.css" type="text/css" rel="stylesheet" /> 

    <script type="text/javascript" src='<%=ResolveUrl("~/Scripts/JQuery/jquery-1.8.3.min.js") %>'></script>
    <script type="text/javascript" src='<%=ResolveUrl("~/JScript/DataProcess.js") %>'></script>
    <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Commons.js") %>'></script>
    <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Ajax.js") %>'></script>        
    <script type="text/javascript">
        $(document).ready(function () {
            $('#txtPersonID').bind('dblclick', function () { GetOtherJsonValue('BU_Person', '', 'PersonID,PersonName'); });
            $('#txtPersonID').bind('change', function () { getBaseData('BU_Person', '', this.value, 'PersonID,PersonName'); });
            if ('<%=UserName%>' == '')
                $('#trPwd').show();
            else
                $('#trPwd').hide();
        });
        function Save() {
            $("#txtUserName").val(trim($("#txtUserName").val()));
            $("#txtTrueName").val(trim($("#txtTrueName").val()));
            if ($("#txtUserName").val() == "") {
                alert("<%=Resources.Resource.UserName_NotNull %>");
                $("#txtUserName").focus();
                return false;
            }
            if ($("#txtTrueName").val() == "") {
                alert("<%=Resources.Resource.TrueName_NotNull %>");
                $("#txtTrueName").focus();
                return false;
            }
            if ($("#txtPassword").val() == "") {
                alert("<%=Resources.Resource.Password_NotNull %>");
                $("#txtPassword").focus();
                return false;
            }
            if ($("#txtConfirmPwd").val() != $("#txtPassword").val()) {
                alert("<%=Resources.Resource.Password_NotSame %>");
                $("#txtConfirmPwd").focus();
                return false;
            }
            if ($("#txtPersonID").val() == "") {
                alert("<%=Resources.Resource.PersonID_NotNull %>");
                $("#txtPersonID").focus();
                return false;
            }
            if ($("#txtEMail").val() == "") {
                alert("<%=Resources.Resource.EMail_NotNull %>");
                $("#txtEMail").focus();
                return false;
            }
            if ("<%= UserName%>" == "") {
                var exist = IsExists("<%= FormID%>", $("#txtUserName").val());
                if (exist) {
                    alert("<%=Resources.Resource.UserName_Exists %>");
                    $("#txtUserName").focus();
                    return false;
                }
            }
            return true;
        }
        function Cancel() {
            if (confirm("<%=Resources.Resource.Question_Cancel %>"))
                location.href = "BU_User.aspx?FormID=<%= FormID%>";
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
                         <asp:Button ID="btnBack" runat="server" Text="返回" OnClientClick="return Back()" 
                            CssClass="ButtonBack" />
                    </td>
                </tr>
       </table>
        <table class="maintable" cellspacing="0" cellpadding="0" width="100%" align="center" bordercolor="#ffffff" border="1" >			

					<tr>
						<td class="musttitle" align="center" ><asp:Literal ID="ltlUserName" Text="登入帳號"
                                runat="server"></asp:Literal>
                        </td>
						<td width="35%" >&nbsp;
								<asp:textbox id="txtUserName" runat="server" CssClass="TextBox" Width="90%" MaxLength="20"></asp:textbox>
                        </td>
						
                        <td class="musttitle" width="10%"  align="center" ><asp:Literal 
                                ID="ltlTrueName" Text="真實姓名"
                                runat="server"></asp:Literal>
                        </td>
						<td  width="35%">&nbsp;
								<asp:textbox id="txtTrueName" runat="server" CssClass="TextBox" Width="90%" MaxLength="100" ></asp:textbox>
                         </td>                         
					</tr>
                    <tr id="trPwd">
						<td class="musttitle" align="center" width="15%" ><asp:Literal ID="ltlPassword" Text="密&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;碼"
                                runat="server"></asp:Literal>
                        </td>
						<td width="35%" >&nbsp;
								<asp:textbox id="txtPassword" runat="server" CssClass="TextBox" Width="90%" 
                                MaxLength="15" TextMode="Password"></asp:textbox>
                        </td>
						<td class="musttitle" align="center" width="15%" ><asp:Literal ID="Literal1" Text="密碼確認"
                                runat="server"></asp:Literal>
                        </td>
						<td width="35%" >&nbsp;
								<asp:textbox id="txtConfirmPwd" runat="server" CssClass="TextBox" Width="90%" 
                                MaxLength="15" TextMode="Password"></asp:textbox>
                        </td>
					</tr>
					<tr>
						<td class="musttitle" align="center" ><asp:Literal ID="ltlPersonID" Text="員工編號"
                                runat="server"></asp:Literal>
                        </td>
						<td width="25%" >&nbsp;
								<asp:textbox id="txtPersonID" runat="server" CssClass="TextBox" Width="30%" MaxLength="10"></asp:textbox>
                                &nbsp;<asp:textbox id="txtPersonName" runat="server" CssClass="TextRead" Width="58%" MaxLength="20"></asp:textbox>
                        </td>
						<td class="smalltitle" align="center" width="10%"><asp:Literal 
                                ID="ltlDepartmentID" Text="所屬部門"
                                runat="server"></asp:Literal>
                        </td>
						<td  width="55%" colspan="3">&nbsp;
							<asp:DropDownList runat="server" ID="ddlDepartmentID"  Width="90%" >                                
                            </asp:DropDownList>
                        </td>
					</tr>
					<tr>
						<td class="musttitle" width="10%"  align="center" ><asp:Literal 
                                ID="ltlEMail" Text="電子郵件"
                                runat="server"></asp:Literal>
                        </td>
						<td  width="35%">&nbsp;
								<asp:textbox id="txtEMail" runat="server" CssClass="TextBox" Width="90%" MaxLength="100" ></asp:textbox>
                         </td>
                         <td class="smalltitle" align="center" width="15%"><asp:Literal 
                                ID="ltlCountry" Text="性&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;別"
                                runat="server"></asp:Literal>
                        </td>
						<td  width="35%">&nbsp;
							<asp:DropDownList runat="server" ID="ddlSex"  Width="90%">
                                <asp:ListItem Value="0">男</asp:ListItem>
                                <asp:ListItem Value="1">女</asp:ListItem>
                            </asp:DropDownList>
                        </td>
					</tr>
                    <tr>
						<td class="smalltitle" align="center"><asp:Literal ID="ltlPhone" Text="連絡電話"
                                runat="server"></asp:Literal></td>
						<td >&nbsp;
								<asp:textbox id="txtPhone" runat="server" CssClass="TextBox" Width="90%" 
                                MaxLength="20" ></asp:textbox>
                         </td>
						<td class="smalltitle" align="center">                            
                            <asp:Literal ID="ltlCellPhone" runat="server" Text="行動電話"></asp:Literal>						
						</td>
						<td>&nbsp;
								<asp:textbox id="txtCellPhone" runat="server" CssClass="TextBox" Width="90%" 
                                MaxLength="20"></asp:textbox></td>
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
                                <asp:ListItem Text="超級用戶"></asp:ListItem>
                            </asp:DropDownList>
                         </td>
                         <td class="smalltitle" align="center" width="10%"><asp:Literal 
                                ID="ltlState" Text="狀&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;態"
                                runat="server"></asp:Literal>
                        </td>
						<td  width="20%">&nbsp;
							<asp:TextBox ID="txtState" runat="server" CssClass="TextRead" MaxLength="10" ReadOnly="true"
                                Width="90%"></asp:TextBox>
                        &nbsp;</td>
					</tr>
                    <tr>
						<td class="smalltitle" align="center"><asp:Literal ID="ltlEnableDate" Text="啟用日期"
                                runat="server"></asp:Literal></td>
						<td >&nbsp;
								<asp:textbox id="txtEnableDate" runat="server" CssClass="TextRead" Width="90%" 
                                MaxLength="20" ReadOnly="True"></asp:textbox>
                         </td>
						<td class="smalltitle" align="center">                            
                            <asp:Literal ID="ltlStopDate" runat="server" Text="停用日期"></asp:Literal>						
						</td>
						<td>&nbsp;
								<asp:textbox id="txtStopDate" runat="server" CssClass="TextRead" Width="90%" 
                                MaxLength="20" ReadOnly="True"></asp:textbox></td>
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
                                MaxLength="20" ReadOnly="true"></asp:textbox>
                            <asp:TextBox ID="txtUserID" CssClass="TextRead" runat="server" 
                                Width="0px"></asp:TextBox>
                        </td>
					</tr>                    
			</table>
    </div>
    <asp:HiddenField ID="hfdUserLevel" runat="server" />
    <asp:HiddenField ID="hfdParentLevel" runat="server" />
    </form>
</body>
</html>
