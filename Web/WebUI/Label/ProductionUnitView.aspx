<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProductionUnitView.aspx.cs" Inherits="WebUI_Label_ProductionUnitView" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />  
        <title></title> 
        <link href="~/Styles/Main.css" type="text/css" rel="stylesheet" /> 
        <link href="~/Styles/op.css" type="text/css" rel="stylesheet" /> 
        <script type="text/javascript" src='<%=ResolveUrl("~/Scripts/JQuery/jquery-1.8.2.min.js") %>'></script>
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Commons.js") %>'></script>
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Ajax.js") %>'></script>        

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
			<table id="Table1" class="maintable" cellspacing="0" cellpadding="0" width="100%" align="center" bordercolor="#ffffff"
				border="1" runat="server">			
					<tr>
						<td  valign="middle" align="left" colspan="4" height="22" class="title1">
							<p><asp:Literal ID="ltlTitle" Text="生產單位設定[ 單筆明細畫面 ]" runat="server"  ></asp:Literal></p>
						</td>
					</tr>				
						
					<tr>
						<td class="musttitle" align="center" width="15%" >
                            <asp:Literal ID="ltlProductionUnitID" Text="單位編號"
                                runat="server" ></asp:Literal>
                        </td>
						<td width="35%" >&nbsp;
								<asp:textbox id="txtProductionUnitID" runat="server" CssClass="TextRead"  ReadOnly="true"
                                Width="90%" MaxLength="10" ></asp:textbox>
                        </td>
						<td class="musttitle" align="center" width="15%">
                            <asp:Literal 
                                ID="ltlProductionUnitName" Text="生產單位名稱"
                                runat="server" ></asp:Literal>
                        </td>
						<td  width="35%">&nbsp;
								<asp:textbox id="txtProductionUnitName" runat="server" CssClass="TextRead"  ReadOnly="true" Width="90%" 
                                MaxLength="20" ></asp:textbox>
                        </td>
					</tr>
                    
				
					<tr>
						<td class="smalltitle" align="center" >
                            <asp:Literal 
                                ID="ltlMemo" Text="備&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;註"
                                runat="server" ></asp:Literal>
                        </td>
						<td  width="85%" colspan="3" height="70">&nbsp;
								<asp:textbox id="txtMemo" runat="server" CssClass="TextRead"  ReadOnly="true" Width="96%" 
                                MaxLength="2000" TextMode="MultiLine"
									Height="59px" ForeColor="Black"></asp:textbox>
                         </td>
					</tr>
                     <tr>
						<td class="smalltitle" align="center">
                            <asp:Literal ID="ltlCreateUserName" Text="建檔人員"
                                runat="server"></asp:Literal></td>
						<td >&nbsp;
								<asp:textbox id="txtCreateUserName" runat="server" CssClass="TextRead" Width="90%" 
                                MaxLength="20" ReadOnly="True" ></asp:textbox>
                         </td>
						<td class="smalltitle" align="center">                            
                            <asp:Literal ID="ltlLastModifyUserName" runat="server" Text="異動人員" ></asp:Literal>						
						</td>
						<td>&nbsp;
								<asp:textbox id="txtLastModifyUserName" runat="server" CssClass="TextRead" Width="90%" 
                                MaxLength="20" ReadOnly="True" ></asp:textbox></td>
					</tr>
                    <tr>
						<td class="smalltitle" align="center">
                            <asp:Literal ID="ltlCreateDate" Text="建檔日期"
                                runat="server" ></asp:Literal></td>
						<td >&nbsp;
								<asp:textbox id="txtCreateDate" runat="server" CssClass="TextRead" 
                                Width="90%" MaxLength="20" ReadOnly="True" ></asp:textbox>
                        </td>
						<td class="smalltitle" align="center"> 
                            <asp:Literal ID="ltlLastModifyDate" Text="異動日期"
                                runat="server" ></asp:Literal>							
						</td>
						<td >&nbsp;
                        <asp:textbox id="txtLastModifyDate" runat="server" CssClass="TextRead" Width="90%" 
                                MaxLength="20" ReadOnly="True" ></asp:textbox></td>
					</tr>
                    <tr>
						<td class="smalltitle" align="center">
                            <asp:Literal ID="ltlCheckUserName" Text="營運覆核"
                                runat="server"></asp:Literal></td>
						<td >&nbsp;
								<asp:textbox id="txtCheckUserName" runat="server" CssClass="TextRead" 
                                Width="90%" MaxLength="20"></asp:textbox>
                        </td>
						<td class="smalltitle" align="center">
                            <asp:Literal ID="ltlStopUserName" Text="停用人員"
                                runat="server" ></asp:Literal>
						
						</td>
						<td >&nbsp;
								<asp:textbox id="txtStopUserName" runat="server" CssClass="TextRead" Width="72%" 
                                MaxLength="20" ></asp:textbox>
                          <asp:button id="btnCheck" runat="server" Text="覆核" 
                                            CssClass="but" Width="75px" onclick="btnCheck_Click" >
                                    </asp:button>
                        </td>
					</tr>
                    <tr>
						<td class="smalltitle" align="center">
                            <asp:Literal ID="ltlCheckDate" Text="營運覆核日期"
                                runat="server" ></asp:Literal></td>
						<td >&nbsp;
								<asp:textbox id="txtCheckDate" runat="server" CssClass="TextRead" 
                                Width="90%" MaxLength="20" ReadOnly="True" ></asp:textbox>
                        </td>
						<td class="smalltitle" align="center">
                            <asp:Literal ID="ltlStopDate" Text="停用日期"
                                runat="server" ></asp:Literal>
						</td>
						<td >&nbsp;
								<asp:textbox id="txtStopDate" runat="server" CssClass="TextRead" Width="72%" 
                                MaxLength="20" ReadOnly="True" ></asp:textbox>
                                   <asp:Button ID="btnStop" runat="server" CssClass="but" 
                                meta:resourcekey="btnStopResource1" onclick="btnStop_Click" Text="停用" 
                                Width="75px" />
                        </td>
					</tr>				
			</table>
            </ContentTemplate>
            </asp:UpdatePanel>
    </form>
</body>
</html>
