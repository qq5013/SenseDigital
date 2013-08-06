<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MemberEdit.aspx.cs" Inherits="WebUI_Customer_MemberEdit" %>
<%@ Register src="../../Controls/Calendar.ascx" tagname="Calendar" tagprefix="uc2" %>
<%@ Register Assembly="EditableDropDownList" Namespace="EditableControls" TagPrefix="editable" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head id="Head1" runat="server">
		<meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />  
        <title></title> 
        <link href="~/Styles/Main.css" type="text/css" rel="stylesheet" /> 
        <link href="~/Styles/op.css" type="text/css" rel="stylesheet" /> 
        <link href="~/JScript/EditDropDownList/css/jquery-ui.css" rel="stylesheet" type="text/css" />

        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/EditDropDownList/Js/jquery-1.6.4.min.js") %>'></script> 
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/EditDropDownList/Js/jquery.ui.core.js") %>'></script> 
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/EditDropDownList/Js/jquery.ui.widget.js") %>'></script> 
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/EditDropDownList/Js/jquery.ui.button.js") %>'></script> 
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/EditDropDownList/Js/jquery.ui.position.js") %>'></script> 
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/EditDropDownList/Js/jquery.ui.autocomplete.js") %>'></script> 
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/EditDropDownList/Js/jquery.ui.combobox.js") %>'></script> 

        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Commons.js") %>'></script>
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Ajax.js") %>'></script>        

        <script type="text/javascript">
            $(document).ready(function () {
                $("#txtAddress1").dblclick(function () { load(this); });
                $("#txtAddress2").dblclick(function () { load(this); });
            });
            function load(obj) {
                returnvalue = window.showModalDialog('../../TempAddress.aspx', window, 'dialogWidth:540px;dialogHeight:200px;help:no;status:no;scroll:auto;Resizable:yes;');
                if (returnvalue != null) {
                    var part = returnvalue.split("@#$");
                    if (obj.id == "txtAddress1") {
                        document.getElementById("txtAddress1").value = part[0];
                        document.getElementById("txtZipCode1").value = part[1];
                    }
                    else {
                        document.getElementById("txtAddress2").value = part[0];
                        document.getElementById("txtZipCode2").value = part[1];
                    }
                }
            } 
            function Save() {
                $("#txtMemberID").val(trim($("#txtMemberID").val()));
                $("#EddlCountry").val(trim($("#EddlCountry").val()));
                $("#txtPasswordHint").val(trim($("#txtPasswordHint").val()));
                $("#txtEMail").val(trim($("#txtEMail").val()));

                if ($("#txtMemberID").val() == "") {
                    alert("<%=Resources.Resource.CT_MemberID_NotNull %>");
                    $("#txtMemberID").focus();
                    return false;
                }
                if ($("#EddlCountry").val() == "") {
                    alert("<%=Resources.Resource.CT_Country_NotNull %>");
                    $("#EddlCountry").focus();
                    return false;
                }
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
                if ($("#txtName").val() == "") {
                    alert("<%=Resources.Resource.CT_MemberName_NotNull %>");
                    $("#txtName").focus();
                    return false;
                }
                if ($("#txtPasswordHint").val() == "") {
                    alert("<%=Resources.Resource.CT_PasswordHint_NotNull %>");
                    $("#txtPasswordHint").focus();
                    return false;
                }
                if ($("#txtEMail").val() == "") {
                    alert("<%=Resources.Resource.CT_Mail_NotNull %>");
                    $("#txtEMail").focus();
                    return false;
                }
                if ("<%= ID%>" == "") {
                    var exist = IsExists("<%= FormID%>", $("#txtMemberID").val(),'Customer');
                    if (exist) {
                        alert("<%=Resources.Resource.CT_MemberID_Exists %>");
                        $("#txtMemberID").focus();
                        return false;
                    }
                }
                return true;
            }
        </script>
	</head>
	<body >
		<form id="form1" runat="server">
        <table style="width: 100%; height: 20px;" class="OperationBar">
                <tr>
                    <td align="right">
                        <asp:Button ID="btnCancel" runat="server" Text="放棄" OnClientClick="return Cancel()" CssClass="ButtonCancel" />
                        <asp:Button ID="btnSave" runat="server" Text="保存" OnClientClick="return Save()" 
                            CssClass="ButtonCreate" onclick="btnSave_Click" />
                        <asp:Button ID="btnExit" runat="server" Text="離開" OnClientClick="Exit()" CssClass="ButtonExit" />
                    </td>
                </tr>
            </table>
			<table id="Table1" class="maintable" cellspacing="0" cellpadding="0" width="100%" align="center" bordercolor="#ffffff"
				border="1" runat="server">			
					<tr>
						<td  valign="middle" align="left" colspan="4" height="22" class="title1">
							<p><asp:Literal ID="ltlTitle" Text="會員資料[ 單筆編輯畫面 ]" runat="server"></asp:Literal></p>
						</td>
					</tr>				
						
					<tr>
						<td class="musttitle" align="center" width="15%" ><asp:Literal ID="ltlMemberID" Text="會&nbsp;員&nbsp;&nbsp;ID"
                                runat="server"></asp:Literal>
                        </td>
						<td width="35%" >&nbsp;
								<asp:textbox id="txtMemberID" runat="server" CssClass="TextBox" Width="90%" MaxLength="20"></asp:textbox>
                        </td>
						<td class="musttitle" align="center" width="15%"><asp:Literal 
                                ID="ltlCountry" Text="國&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;別"
                                runat="server"></asp:Literal>
                        </td>
						<td  width="35%">&nbsp;
							<editable:EditableDropDownList ID="EddlCountry" runat="server" Sorted="true" Width="90%">
                            </editable:EditableDropDownList>
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
						<td class="musttitle" align="center" width="15%" ><asp:Literal ID="ltlConfirmPwd" Text="密碼確認"
                                runat="server"></asp:Literal>
                        </td>
						<td width="35%" >&nbsp;
								<asp:textbox id="txtConfirmPwd" runat="server" CssClass="TextBox" Width="90%" 
                                MaxLength="15" TextMode="Password"></asp:textbox>
                        </td>
					</tr>
					<tr>
						<td class="musttitle" align="center" width="15%" ><asp:Literal ID="ltlName" Text="姓&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;名"
                                runat="server"></asp:Literal>
                        </td>
						<td width="35%" >&nbsp;
								<asp:textbox id="txtName" runat="server" CssClass="TextBox" Width="90%" MaxLength="20"></asp:textbox>
                        </td>
						<td class="musttitle" align="center" width="15%"><asp:Literal 
                                ID="ltlPasswordHint" Text="密碼提示"
                                runat="server"></asp:Literal>
                        </td>
						<td  width="35%">&nbsp;
							<asp:textbox id="txtPasswordHint" runat="server" CssClass="TextBox" Width="90%" MaxLength="20"></asp:textbox>
                        </td>
					</tr>
                     <tr>
						<td class="smalltitle" align="center" ><asp:Literal 
                                ID="ltlBirthday" Text="出生日期"
                                runat="server"></asp:Literal>
                        </td>
						<td  width="35%" >&nbsp;
							<uc2:Calendar runat="server" ID="txtBirthday" />
                         </td>
                         <td class="smalltitle" align="center" width="15%"><asp:Literal 
                                ID="ltlSex" Text="性&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;別"
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
						<td class="musttitle" align="center" ><asp:Literal 
                                ID="ltlEMail" Text="電子郵件"
                                runat="server"></asp:Literal>
                        </td>
						<td  width="85%" colspan="3" >&nbsp;
								<asp:textbox id="txtEMail" runat="server" CssClass="TextBox" Width="96%" MaxLength="30" ></asp:textbox>
                         </td>
					</tr>
                    <tr>
						<td class="smalltitle" align="center" ><asp:Literal 
                                ID="ltlSpareEMail" Text="備用郵件"
                                runat="server"></asp:Literal>
                        </td>
						<td  width="85%" colspan="3" >&nbsp;
								<asp:textbox id="txtSpareEMail" runat="server" CssClass="TextBox" Width="96%" MaxLength="30" ></asp:textbox>
                         </td>
					</tr>
                    
                       <tr>
						<td class="smalltitle" align="center" ><asp:Literal 
                                ID="ltlCellPhone" Text="行動電話"
                                runat="server"></asp:Literal>
                        </td>
						<td  width="85%" colspan="3" >&nbsp;
								<asp:textbox id="txtCellPhone" runat="server" CssClass="TextBox" Width="96%" MaxLength="16" ></asp:textbox>
                         </td>
					</tr>
                       <tr>
						<td class="smalltitle" align="center" ><asp:Literal 
                                ID="ltlLinkPhone" Text="聯絡電話"
                                runat="server"></asp:Literal>
                        </td>
						<td  width="85%" colspan="3" >&nbsp;
								<asp:textbox id="txtLinkPhone" runat="server" CssClass="TextBox" Width="96%" MaxLength="16" ></asp:textbox>
                         </td>
					</tr>
                    <tr>
						<td class="smalltitle" align="center" ><asp:Literal 
                                ID="ltlAddress1" Text="通訊地址1"
                                runat="server"></asp:Literal>
                        </td>
						<td  width="85%" colspan="3">&nbsp; <asp:textbox id="txtZipCode1" runat="server" CssClass="TextBox" Width="20%" MaxLength="100"
									ForeColor="Black"></asp:textbox>
								&nbsp;<asp:textbox id="txtAddress1" runat="server" CssClass="TextBox" Width="75%" MaxLength="100"
									ForeColor="Black"></asp:textbox>
                         </td>
					</tr>
                      <tr>
						<td class="smalltitle" align="center" ><asp:Literal 
                                ID="ltlAddress2" Text="通訊地址2"
                                runat="server"></asp:Literal>
                        </td>
						<td  width="85%" colspan="3">&nbsp; <asp:textbox id="txtZipCode2" runat="server" CssClass="TextBox" Width="20%" MaxLength="100"
									ForeColor="Black"></asp:textbox>
								&nbsp;<asp:textbox id="txtAddress2" runat="server" CssClass="TextBox" Width="75%" MaxLength="100"
									ForeColor="Black"></asp:textbox>
                         </td>
					</tr>
                    <tr>
						<td class="smalltitle" align="center" ><asp:Literal 
                                ID="ltlWebSite" Text="個人網站"
                                runat="server"></asp:Literal>
                        </td>
						<td  width="85%" colspan="3" >&nbsp;
								<asp:textbox id="txtWebSite" runat="server" CssClass="TextBox" Width="96%" MaxLength="50" ></asp:textbox>
                         </td>
					</tr>
			</table>
			
		</form>
	</body>
</html>
