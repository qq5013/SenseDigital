<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BU_UserView.aspx.cs" Inherits="WebUI_Account_BU_UserView" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="~/Styles/Main.css" type="text/css" rel="stylesheet" /> 
    <link href="~/Styles/op.css" type="text/css" rel="stylesheet" /> 
    <script type="text/javascript" src='<%=ResolveUrl("~/Scripts/JQuery/jquery-1.8.3.min.js") %>'></script>
    <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Commons.js") %>'></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table style="width: 100%; height: 20px;" class="OperationBar">
                <tr>
                    <td align="right">                        
                        <asp:Button ID="btnEdit" runat="server" Text="修改" CssClass="ButtonModify" />
                        <asp:Button ID="btnSave" runat="server" Text="保存" CssClass="ButtonSave"  />
                            <asp:Button ID="btnCancel" runat="server" Text="放棄" CssClass="ButtonCancel" />
                        <asp:Button ID="btnExit" runat="server" Text="離開" OnClientClick="eExit()" CssClass="ButtonExit" />
                    </td>
                </tr>
       </table>
        <table class="maintable" cellspacing="0" cellpadding="0" width="100%" align="center" bordercolor="#ffffff" border="1" >			

					<tr>
						<td class="musttitle" align="center" ><asp:Literal ID="ltlUserName" Text="登入帳號"
                                runat="server"></asp:Literal>
                        </td>
						<td width="35%" >&nbsp;
								<asp:textbox id="txtUserName" runat="server" CssClass="TextBox" Width="70%" MaxLength="20"></asp:textbox>
                                &nbsp;<asp:button id="btnRole" runat="server" Text="所屬角色" 
                                            CssClass="but" Width="75px">
                                    </asp:button> 
                        </td>
						<td class="musttitle" width="10%"  align="center" ><asp:Literal 
                                ID="ltlTrueName" Text="真實姓名"
                                runat="server"></asp:Literal>
                        </td>
						<td  width="35%">&nbsp;
								<asp:textbox id="txtTrueName" runat="server" CssClass="TextBox" Width="90%" MaxLength="100" ></asp:textbox>
                         </td>
                        
					</tr>
                    <tr>
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
                                &nbsp;<asp:textbox id="txtPersonName" runat="server" CssClass="TextRead" ReadOnly="true" Width="58%" MaxLength="20"></asp:textbox>
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
                         <td class="musttitle" align="center" width="15%"><asp:Literal 
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
                            </asp:DropDownList>
                         </td>
                         <td class="smalltitle" align="center" width="10%"><asp:Literal 
                                ID="ltlState" Text="狀&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;態"
                                runat="server"></asp:Literal>
                        </td>
						<td  width="20%">&nbsp;
							<asp:TextBox ID="txtState" runat="server" CssClass="TextRead" MaxLength="10" ReadOnly="true"
                                Width="70%"></asp:TextBox>
                        &nbsp;<asp:button id="btnState" runat="server" Text="狀態切換" 
                                            CssClass="but" Width="75px">
                                    </asp:button>                          
                        </td>
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
                                MaxLength="20" ReadOnly="true"></asp:textbox></td>
					</tr>                    
			</table>
    </div>
    </form>
</body>
</html>