<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DepartmentView.aspx.cs" Inherits="WebUI_BusinessUnit_DepartmentView" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head id="Head1" runat="server">
		<meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
  
    <title></title> 
        <link href="~/Styles/Main.css" type="text/css" rel="stylesheet" /> 
        <link href="~/Styles/op.css" type="text/css" rel="stylesheet" />
        
        <script type="text/javascript" src='<%=ResolveUrl("~/Scripts/JQuery/jquery-1.8.3.min.js") %>'></script>  
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Commons.js") %>'></script>
        <script type="text/javascript">
            
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
            <table style="width: 100%; height: 20px;" class="OperationBar">
                <tr>
                    <td align="right" style="width:60%">
                        <asp:Button ID="btnFirst" runat="server" Text="首筆" CssClass="ButtonFirst" 
                            onclick="btnFirst_Click" meta:resourcekey="btnFirstResource1" />
                        <asp:Button ID="btnPre" runat="server" Text="上一筆" CssClass="ButtonPre" 
                            onclick="btnPre_Click" meta:resourcekey="btnPreResource1" />
                        <asp:Button ID="btnNext" runat="server" Text="下一筆" CssClass="ButtonNext" 
                            onclick="btnNext_Click" meta:resourcekey="btnNextResource1" />
                        <asp:Button ID="btnLast" runat="server" Text="尾筆" CssClass="ButtonLast" 
                            onclick="btnLast_Click" meta:resourcekey="btnLastResource1" />
                    </td>
                    <td align="right">
                        <asp:Button ID="btnPrint" runat="server" Text="列印" CssClass="ButtonPrint" 
                            onclick="btnPrint_Click" meta:resourcekey="btnPrintResource1" />
                        <asp:Button ID="btnAdd" runat="server" Text="新增" CssClass="ButtonCreate" 
                            OnClientClick="Edit(false);return false" meta:resourcekey="btnAddResource1" />
                        <asp:Button ID="btnDelete" runat="server" Text="刪除" CssClass="ButtonDel" OnClientClick="return ViewDelete()"
                            onclick="btnDelete_Click" meta:resourcekey="btnDeleteResource1" />
                        <asp:Button ID="btnEdit" runat="server" Text="修改" CssClass="ButtonModify" 
                            OnClientClick="return Edit(true);" onclick="btnEdit_Click" 
                            meta:resourcekey="btnEditResource1" />
                        <asp:Button ID="btnExit" runat="server" Text="離開" OnClientClick="return Exit()" 
                            CssClass="ButtonExit" meta:resourcekey="btnExitResource1" />
                        <asp:Button ID="btnBack" runat="server" Text="返回" OnClientClick="return Back()" 
                            CssClass="ButtonBack" />
                    </td>
                </tr>
            </table>
			<table class="maintable" cellspacing="0" cellpadding="0" width="100%" align="center" borderColor="#ffffff" border="1" >
					<tr>
						<td  valign="middle" align="left" colspan="4" height="22" class="title1">
							<p><asp:Literal ID="ltlTitle" Text="營運部門資料[ 單筆明細畫面 ]" runat="server" 
                                    meta:resourcekey="ltlTitleResource1"></asp:Literal></p>
						</td>
					</tr>
						<tr>
						<td class="musttitle" align="center" width="15%" >
                            <asp:Literal ID="ltlDepartmentID" Text="部門編號"
                                runat="server" meta:resourcekey="ltlDepartmentIDResource1"></asp:Literal></td>
						<td width="35%" >&nbsp;
								<asp:textbox id="txtDepartmentID" runat="server" CssClass="TextRead" 
                                Width="90%" ReadOnly="True" meta:resourcekey="txtDepartmentIDResource1"></asp:textbox>
                        </td>
						<td class="musttitle" align="center" width="15%">
                            <asp:Literal 
                                ID="ltlDepartmentName" Text="部門名稱"
                                runat="server" meta:resourcekey="ltlDepartmentNameResource1"></asp:Literal></td>
						<td  width="35%">&nbsp;
								<asp:textbox id="txtDepartmentName" runat="server" CssClass="TextRead" Width="90%" 
                                ReadOnly="True" meta:resourcekey="txtDepartmentNameResource1"></asp:textbox>
                        </td>
					</tr>
					<tr>
						<td class="smalltitle" align="center" >
                            <asp:Literal ID="ltlDepartmentEName" Text="英文名稱"
                                runat="server" meta:resourcekey="ltlDepartmentENameResource1"></asp:Literal></td>
						<td width="85%" colspan="3" >&nbsp;
								<asp:textbox id="txtDepartmentEName" runat="server" CssClass="TextRead" 
                                Width="96%" ReadOnly="True" meta:resourcekey="txtDepartmentENameResource1"></asp:textbox>
                        </td>
					</tr>
					<tr>
						<td class="smalltitle" align="center" >
                            <asp:Literal 
                                ID="ltlMemo" Text="備&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;註"
                                runat="server" meta:resourcekey="ltlMemoResource1"></asp:Literal></td>
						<td  width="85%" colspan="3" height="70">&nbsp;
								<asp:textbox id="txtMemo" runat="server" CssClass="TextRead" Width="96%" TextMode="MultiLine"
									Height="59px" ForeColor="Black" ReadOnly="True" meta:resourcekey="txtMemoResource1"></asp:textbox>
                         </td>
					</tr>
                    <tr>
						<td class="smalltitle" align="center">
                            <asp:Literal ID="ltlCreateUserName" Text="建檔人員"
                                runat="server" meta:resourcekey="ltlCreateUserNameResource1"></asp:Literal></td>
						<td >&nbsp;
								<asp:textbox id="txtCreateUserName" runat="server" CssClass="TextRead" Width="90%" 
                                ReadOnly="True" meta:resourcekey="txtCreateUserNameResource1"></asp:textbox>
                         </td>
						<td class="smalltitle" align="center">                            
                            <asp:Literal ID="ltlCreateDate" runat="server" Text="建檔日期" 
                                meta:resourcekey="ltlCreateDateResource1"></asp:Literal>						
						</td>
						<td>&nbsp;
								<asp:textbox id="txtCreateDate" runat="server" CssClass="TextRead" Width="90%" 
                                ReadOnly="True" meta:resourcekey="txtCreateDateResource1"></asp:textbox></td>
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
                                ReadOnly="True" meta:resourcekey="txtLastModifyDateResource1"></asp:textbox></td>
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
								<asp:textbox id="txtCheckDate" runat="server" CssClass="TextRead" 
                                Width="61%" meta:resourcekey="txtCheckDateResource1" 
                                ></asp:textbox>
                            &nbsp;
                            <asp:button id="btnCheck" runat="server" Text="覆核" 
                                            CssClass="but" Width="75px" onclick="btnCheck_Click" 
                                meta:resourcekey="btnCheckResource1">
                                    </asp:button></td>
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
                                runat="server" meta:resourcekey="ltlStopDateResource1"></asp:Literal>
						</td>
						<td >&nbsp;
								<asp:textbox id="txtStopDate" runat="server" CssClass="TextRead" Width="61%" 
                                ReadOnly="True" meta:resourcekey="txtStopDateResource1"></asp:textbox>
                            &nbsp;
                            <asp:button id="btnStop" runat="server" Text="停用" 
                                            CssClass="but" Width="75px" onclick="btnStop_Click" 
                                meta:resourcekey="btnStopResource1">
                                    </asp:button></td>
					</tr>
			</table>
			</ContentTemplate>
            </asp:UpdatePanel>
		</form>
	</body>
</html>
