<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DepartmentEdit.aspx.cs" Inherits="WebUI_BusinessUnit_DepartmentEdit" %>

<%@ Register Src="../../Controls/Calendar.ascx" TagName="Calendar" TagPrefix="uc2" %>
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
                   // var exist = IsExists("<%= FormID%>", $("#txtDepartmentID").val(), "<%= cnKey%>");
                    var exist = IsExistsByFilter("<%= FormID%>", $("#txtEnterpriseID").val(), $("#txtDepartmentID").val(), "<%= cnKey%>");
                    if (exist=="1") {
                        alert("<%=Resources.Resource.BU_DepartmentID_Exists %>");
                        $("#txtDepartmentID").focus();
                        return false;
                    }
                }
                return true;
            }
            function changeDate()
            { }
        </script>
	</head>
	<body >
		<form id="form1" runat="server">
        <table style="width: 100%; height: 20px;" class="OperationBar">
                <tr>
                    <td align="right">
                        <asp:Button ID="btnCancel" runat="server" Text="放棄" OnClientClick="return Cancel()" CssClass="ButtonCancel" />
                        <asp:Button ID="btnSave" runat="server" Text="保存" OnClientClick="return Save()" 
                            CssClass="ButtonSave" onclick="btnSave_Click" />
                        <asp:Button ID="btnExit" runat="server" Text="離開" OnClientClick="return Exit()" CssClass="ButtonExit" />
                    </td>
                </tr>
            </table>
			<table class="maintable" cellspacing="0" cellpadding="0" width="100%" align="center" bordercolor="#ffffff"
				border="1" runat="server">			
					<tr>
						<td  valign="middle" align="left" colspan="4" height="22" class="title1">
							<p><asp:Literal ID="ltlTitle" Text="企業使用單位資料[ 單筆編輯畫面 ]" runat="server"></asp:Literal></p>
						</td>
					</tr>				
					<tr>
						<td class="smalltitle" align="center" width="15%" ><asp:Literal ID="ltlEnterpriseID" Text="企業編號"
                                runat="server"></asp:Literal></td>
						<td width="35%" >&nbsp;
								<asp:textbox id="txtEnterpriseID" runat="server" CssClass="TextRead" 
                                Width="90%" MaxLength="6" ReadOnly="true"></asp:textbox>
                        </td>
						<td class="smalltitle" align="center" width="15%"><asp:Literal 
                                ID="ltlEnterpriseName" Text="企業名稱"
                                runat="server"></asp:Literal></td>
						<td  width="35%">&nbsp;
								<asp:textbox id="txtEnterpriseName" runat="server" CssClass="TextRead" Width="90%" 
                                MaxLength="20" ReadOnly="true"></asp:textbox>
                        </td>
					</tr>	
					<tr>
						<td class="musttitle" align="center" width="15%" ><asp:Literal ID="ltlDepartmentID" Text="使用單位編號"
                                runat="server"></asp:Literal>
                        </td>
						<td width="35%" >&nbsp;
								<asp:textbox id="txtDepartmentID" runat="server" CssClass="TextBox" Width="90%" MaxLength="10"></asp:textbox>
                        </td>
						<td class="musttitle" align="center" width="15%"><asp:Literal 
                                ID="ltlDepartmentName" Text="使用單位名稱"
                                runat="server"></asp:Literal>
                        </td>
						<td  width="35%">&nbsp;
								<asp:textbox id="txtDepartmentName" runat="server" CssClass="TextBox" Width="90%" 
                                MaxLength="20"></asp:textbox>
                        </td>
					</tr>
					<tr>
						<td class="smalltitle" align="center" ><asp:Literal ID="ltlDepartmentEName" Text="英文名稱"
                                runat="server"></asp:Literal>
                        </td>
						<td width="85%" colspan="3" >&nbsp;
								<asp:textbox id="txtDepartmentEName" runat="server" CssClass="TextBox" Width="96%" MaxLength="30"></asp:textbox>
                        </td>
					</tr>
					<tr>
						<td class="smalltitle" align="center" ><asp:Literal 
                                ID="ltlMemo" Text="備&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;註"
                                runat="server"></asp:Literal>
                        </td>
						<td  width="85%" colspan="3" height="70">&nbsp;
								<asp:textbox id="txtMemo" runat="server" CssClass="TextBox" Width="96%" MaxLength="2000" TextMode="MultiLine"
									Height="59px" ForeColor="Black"></asp:textbox>
                         </td>
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
                    <tr>
						<td class="smalltitle" align="center"><asp:Literal ID="ltlCheckUserName" Text="企業覆核人員"
                                runat="server"></asp:Literal></td>
						<td >&nbsp;
								<asp:textbox id="txtCheckUserName" runat="server" CssClass="TextRead" Width="90%" MaxLength="20"></asp:textbox>
                        </td>
						<td class="smalltitle" align="center"><asp:Literal ID="ltlCheckDate" Text="企業覆核日期"
                                runat="server"></asp:Literal>
						
						</td>
						<td >&nbsp;
								<asp:textbox id="txtCheckDate" runat="server" CssClass="TextRead" Width="90%" 
                                MaxLength="20"></asp:textbox>
                        </td>
					</tr>
                    <tr>
						<td class="smalltitle" align="center"><asp:Literal ID="ltlStopUserName" Text="停用人員"
                                runat="server"></asp:Literal></td>
						<td >&nbsp;
								<asp:textbox id="txtStopUserName" runat="server" CssClass="TextRead" Width="90%" MaxLength="20" ReadOnly="true"></asp:textbox>
                        </td>
						<td class="smalltitle" align="center"><asp:Literal ID="ltlStopDate" Text="停用日期"
                                runat="server"></asp:Literal>
						</td>
						<td >&nbsp;
								<asp:textbox id="txtStopDate" runat="server" CssClass="TextRead" Width="90%" 
                                MaxLength="20" ReadOnly="true"></asp:textbox>
                        </td>
					</tr>				
			</table>
			
		</form>
	</body>
</html>
