<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DepartmentEdit.aspx.cs" Inherits="WebUI_BusinessUnit_DepartmentEdit" culture="auto" meta:resourcekey="PageResource2" uiculture="auto" %>

<%@ Register src="../../Controls/Calendar.ascx" tagname="Calendar" tagprefix="uc2" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head id="Head1" runat="server">
		<meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />  
        <title></title> 
        <link href="~/Styles/Main.css" type="text/css" rel="stylesheet" /> 
        <link href="~/Styles/op.css" type="text/css" rel="stylesheet" /> 

        <script src='<%=ResolveUrl("~/Scripts/JQuery/jquery-1.8.3.min.js") %>' type="text/javascript"></script>
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Commons.js") %>'></script>
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Ajax.js") %>'></script>        

        <script type="text/javascript">
            
            function Save() {
                $("#txtDepartmentID").val(trim($("#txtDepartmentID").val()));
                $("#txtDepartmentName").val(trim($("#txtDepartmentName").val()));
                if ($("#txtDepartmentID").val()=="") {
                    alert("<%=Resources.Resource.BU_DepartmentID_NotNull %>");
                    $("#txtDepartmentID").focus();
                    return false;
                }
                if ($("#txtDepartmentName").val() == "") {
                    alert("<%=Resources.Resource.BU_DepartmentName_NotNull %>");
                    $("#txtDepartmentName").focus();
                    return false;
                }
                if ("<%= ID%>" == "") {
                    var exist = IsExists("<%= FormID%>", $("#txtDepartmentID").val());
                    if (exist) {
                        alert("<%=Resources.Resource.BU_DepartmentID_Exists %>");
                        $("#txtDepartmentID").focus();
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
                        <asp:Button ID="btnCancel" runat="server" Text="放棄" 
                            OnClientClick="return Cancel()" CssClass="ButtonCancel" 
                            meta:resourcekey="btnCancelResource1" />
                        <asp:Button ID="btnSave" runat="server" Text="保存" OnClientClick="return Save()" 
                            CssClass="ButtonSave" onclick="btnSave_Click" 
                            meta:resourcekey="btnSaveResource1" />
                        <asp:Button ID="btnExit" runat="server" Text="離開" OnClientClick="return Exit()" 
                            CssClass="ButtonExit" meta:resourcekey="btnExitResource1" />
                    </td>
                </tr>
            </table>
			<table class="maintable" cellspacing="0" cellpadding="0" width="100%" align="center" bordercolor="#ffffff"
				border="1" runat="server">			
					<tr>
						<td  valign="middle" align="left" colspan="4" height="22" class="title1">
							<p><asp:Literal ID="ltlTitle" Text="營運部門資料[ 單筆編輯畫面 ]" runat="server" 
                                    meta:resourcekey="ltlTitleResource1"></asp:Literal></p>
						</td>
					</tr>				
						
					<tr>
						<td class="musttitle" align="center" width="15%" >
                            <asp:Literal ID="ltlDepartmentID" Text="部門編號"
                                runat="server" meta:resourcekey="ltlDepartmentIDResource1"></asp:Literal>
                        </td>
						<td width="35%" >&nbsp;
								<asp:textbox id="txtDepartmentID" runat="server" CssClass="TextBox" 
                                Width="90%" MaxLength="10" meta:resourcekey="txtDepartmentIDResource1"></asp:textbox>
                        </td>
						<td class="musttitle" align="center" width="15%">
                            <asp:Literal 
                                ID="ltlDepartmentName" Text="部門名稱"
                                runat="server" meta:resourcekey="ltlDepartmentNameResource1"></asp:Literal>
                        </td>
						<td  width="35%">&nbsp;
								<asp:textbox id="txtDepartmentName" runat="server" CssClass="TextBox" Width="90%" 
                                MaxLength="20" meta:resourcekey="txtDepartmentNameResource1"></asp:textbox>
                        </td>
					</tr>
					<tr>
						<td class="smalltitle" align="center" >
                            <asp:Literal ID="ltlDepartmentEName" Text="英文名稱"
                                runat="server" meta:resourcekey="ltlDepartmentENameResource1"></asp:Literal>
                        </td>
						<td width="85%" colspan="3" >&nbsp;
								<asp:textbox id="txtDepartmentEName" runat="server" CssClass="TextBox" 
                                Width="96%" MaxLength="30" meta:resourcekey="txtDepartmentENameResource1"></asp:textbox>
                        </td>
					</tr>
					<tr>
						<td class="smalltitle" align="center" >
                            <asp:Literal 
                                ID="ltlMemo" Text="備&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;註"
                                runat="server" meta:resourcekey="ltlMemoResource1"></asp:Literal>
                        </td>
						<td  width="85%" colspan="3" height="70">&nbsp;
								<asp:textbox id="txtMemo" runat="server" CssClass="TextBox" Width="96%" 
                                MaxLength="2000" TextMode="MultiLine"
									Height="59px" ForeColor="Black" meta:resourcekey="txtMemoResource1"></asp:textbox>
                         </td>
					</tr>
                     <tr>
						<td class="smalltitle" align="center">
                            <asp:Literal ID="ltlCreateUserName" Text="建檔人員"
                                runat="server" meta:resourcekey="ltlCreateUserNameResource1"></asp:Literal></td>
						<td >&nbsp;
								<asp:textbox id="txtCreateUserName" runat="server" CssClass="TextRead" Width="90%" 
                                MaxLength="20" ReadOnly="True" 
                                meta:resourcekey="txtCreateUserNameResource1"></asp:textbox>
                         </td>
						<td class="smalltitle" align="center">                            
                            <asp:Literal ID="ltlCreateDate" runat="server" Text="建檔日期" 
                                meta:resourcekey="ltlCreateDateResource1"></asp:Literal>						
						</td>
						<td>&nbsp;
								<asp:textbox id="txtCreateDate" runat="server" CssClass="TextRead" Width="90%" 
                                MaxLength="20" ReadOnly="True" meta:resourcekey="txtCreateDateResource1"></asp:textbox></td>
					</tr>
                    <tr>
						<td class="smalltitle" align="center">
                            <asp:Literal ID="ltlLastModifyUserName" Text="異動人員"
                                runat="server" meta:resourcekey="ltlLastModifyUserNameResource1"></asp:Literal></td>
						<td >&nbsp;
								<asp:textbox id="txtLastModifyUserName" runat="server" CssClass="TextRead" 
                                Width="90%" MaxLength="20" ReadOnly="True" 
                                meta:resourcekey="txtLastModifyUserNameResource1" ></asp:textbox>
                        </td>
						<td class="smalltitle" align="center"> 
                            <asp:Literal ID="ltlLastModifyDate" Text="異動日期"
                                runat="server" meta:resourcekey="ltlLastModifyDateResource1"></asp:Literal>							
						</td>
						<td >&nbsp;
                        <asp:textbox id="txtLastModifyDate" runat="server" CssClass="TextRead" Width="90%" 
                                MaxLength="20" ReadOnly="True" 
                                meta:resourcekey="txtLastModifyDateResource1"></asp:textbox></td>
					</tr>
                    <tr>
						<td class="smalltitle" align="center">
                            <asp:Literal ID="ltlCheckUserName" Text="覆核人員"
                                runat="server" meta:resourcekey="ltlCheckUserNameResource1"></asp:Literal></td>
						<td >&nbsp;
								<asp:textbox id="txtCheckUserName" runat="server" CssClass="TextRead" 
                                Width="90%" MaxLength="20" meta:resourcekey="txtCheckUserNameResource1"></asp:textbox>
                        </td>
						<td class="smalltitle" align="center">
                            <asp:Literal ID="ltlCheckDate" Text="覆核日期"
                                runat="server" meta:resourcekey="ltlCheckDateResource1"></asp:Literal>
						
						</td>
						<td >&nbsp;
								<asp:textbox id="txtCheckDate" runat="server" CssClass="TextRead" Width="90%" 
                                MaxLength="20" meta:resourcekey="txtCheckDateResource1"></asp:textbox>
                        </td>
					</tr>
                    <tr>
						<td class="smalltitle" align="center">
                            <asp:Literal ID="ltlStopUserName" Text="停用人員"
                                runat="server" meta:resourcekey="ltlStopUserNameResource1"></asp:Literal></td>
						<td >&nbsp;
								<asp:textbox id="txtStopUserName" runat="server" CssClass="TextRead" 
                                Width="90%" MaxLength="20" ReadOnly="True" 
                                meta:resourcekey="txtStopUserNameResource1"></asp:textbox>
                        </td>
						<td class="smalltitle" align="center">
                            <asp:Literal ID="ltlStopDate" Text="停用日期"
                                runat="server" meta:resourcekey="ltlStopDateResource2"></asp:Literal>
						</td>
						<td >&nbsp;
								<asp:textbox id="txtStopDate" runat="server" CssClass="TextRead" Width="90%" 
                                MaxLength="20" ReadOnly="True" meta:resourcekey="txtStopDateResource1"></asp:textbox>
                        </td>
					</tr>				
			</table>
			
		</form>
	</body>
</html>
