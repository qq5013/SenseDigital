<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProductResumeEdit.aspx.cs" Inherits="WebUI_Enterprise_ProductResumeEdit" %>
<%@ Register Src="../../Controls/Calendar.ascx" TagName="Calendar" TagPrefix="uc2" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <link href="~/Styles/Main.css" type="text/css" rel="stylesheet" /> 
        <link href="~/Styles/op.css" type="text/css" rel="stylesheet" /> 
        <script type="text/javascript" src='<%=ResolveUrl("~/Scripts/JQuery/jquery-1.8.3.min.js") %>'></script>
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Commons.js") %>'></script>
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/DataProcess.js") %>'></script>
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Resize.js") %>'></script>  
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Ajax.js") %>'></script> 
 
         <script type="text/javascript">
             $(document).ready(function () {
                 $('#txtProductID').bind('dblclick', function () {
                     var enterpriseID = $("#txtEnterpriseID").val();
                     GetOtherJsonWhereValue('EP_Product', '', 'ProductID,ProductName', 'txtProductID,txtProductName', "EnterpriseID='" + enterpriseID + "'");
                  });
                 $('#txtProductID').bind('change', function () { getBaseData('EP_Product', '', this.value, 'ProductID,ProductName'); });
                 content_resize();
             });
             function Save() {
                 $("#txtResumeID").val(trim($("#txtResumeID").val()));
                 $("#txtProductID").val(trim($("#txtProductID").val()));
                 $("#txtProductName").val(trim($("#txtProductName").val()));
                 $("#txtOfficalUrl").val(trim($("#txtOfficalUrl").val()));
                 $("#txtServicePhone").val(trim($("#txtServicePhone").val()));

                 if ($("#txtResumeID").val() == "") {
                     alert("<%=Resources.Resource.EP_ResumeID_NotNull %>");
                     $("#txtResumeID").focus();
                     return false;
                 }
                 if ($("#txtProductID").val() == "") {
                     alert("<%=Resources.Resource.EP_ProductID_NotNull %>");
                     $("#txtProductID").focus();
                     return false;
                 }
                 if ($("#txtProductName").val() == "") {
                     alert("<%=Resources.Resource.EP_ProductName_NotNull %>");
                     $("#txtProductName").focus();
                     return false;
                 }
                 if ($("#ckbHasGarantee").checked && $("#txtGaranteeDate_txtDate").val() == "") {
                     alert("<%=Resources.Resource.EP_GaranteeDate_NotNull %>");
                     $("#txtGaranteeDate_txtDate").focus();
                     return false;
                 }
                 if ($("#txtProduceDate_txtDate").val() == "") {
                     alert("<%=Resources.Resource.EP_ProduceDate_NotNull %>");
                     $("#txtProduceDate_txtDate").focus();
                     return false;
                 }
                 if ($("#txtOfficalUrl").val() == "") {
                     alert("<%=Resources.Resource.EP_OfficalUrl_NotNull %>");
                     $("#txtOfficalUrl").focus();
                     return false;
                 }
                 if ($("#txtServicePhone").val() == "") {
                     alert("<%=Resources.Resource.EP_ServicePhone_NotNull %>");
                     $("#txtServicePhone").focus();
                     return false;
                 }
                 if ("<%= ID%>" == "") {
                     // var exist = IsExists("<%= FormID%>", $("#txtProductID").val(), "<%= cnKey%>");
                     var exist = IsExistsByFilter("<%= FormID%>", $("#txtEnterpriseID").val(), $("#txtResumeID").val(), "<%= cnKey%>");
                     if (exist == "1") {
                         alert("<%=Resources.Resource.EP_ProductResumeID_Exists %>");
                         $("#txtResumeID").focus();
                         return false;
                     }
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
        <div id="processMessage"> Loading...<br />
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
                        <asp:Button ID="btnCancel" runat="server" Text="放棄" OnClientClick="return Cancel()" CssClass="ButtonCancel" />
                        <asp:Button ID="btnSave" runat="server" Text="保存" OnClientClick="return Save()" 
                            CssClass="ButtonSave" onclick="btnSave_Click" />
                        <asp:Button ID="btnExit" runat="server" Text="離開" OnClientClick="return Exit()" CssClass="ButtonExit" />
                    </td>
                </tr>
            </table>
            <div id="surdiv" style="overflow:auto">
			<table id="Table1" class="maintable" cellspacing="0" cellpadding="0" width="100%" align="center" bordercolor="#ffffff"
				border="1" runat="server">			
					<tr>
						<td  valign="middle" align="left" colspan="4" height="22" class="title1">
							<p><asp:Literal ID="ltlTitle" Text="企業用戶産品生産履歷設定(未認證)[ 單筆編輯畫面 ]" runat="server"></asp:Literal></p>
						</td>
					</tr>		
					<tr>
						<td class="musttitle" align="center" width="10%" ><asp:Literal ID="ltlEnterpriseID" Text="企業編號"
                                runat="server"></asp:Literal>
                        </td>
						<td colspan="3">&nbsp;
								<asp:textbox id="txtEnterpriseID" runat="server" CssClass="TextRead"  ReadOnly="true"  Width="30%" MaxLength="6"></asp:textbox>&nbsp;
                                <asp:textbox id="txtEnterpriseName" runat="server" CssClass="TextRead"  ReadOnly="true"  Width="65%" MaxLength="100"></asp:textbox>
                        </td>						
						
					</tr>
					<tr>
						<td class="musttitle" align="center" width="15%" ><asp:Literal ID="ltlResumeID" Text="生産履歷編號"
                                runat="server"></asp:Literal></td>
						<td width="35%" >&nbsp;
								<asp:textbox id="txtResumeID" runat="server" CssClass="TextBox" 
                                Width="90%" MaxLength="20" ></asp:textbox>
                        </td>
						<td class="musttitle" align="center" width="15%"><asp:Literal 
                                ID="ltlProduceDate" Text="製造日期"
                                runat="server"></asp:Literal></td>
						<td  width="35%">&nbsp;
								<uc2:Calendar runat="server" ID="txtProduceDate" />
                        </td>
					</tr>						
					<tr>
						<td class="musttitle" align="center" width="15%" ><asp:Literal ID="ltlProductID" Text="産品編號"
                                runat="server"></asp:Literal>
                        </td>
						<td width="35%" >&nbsp;
								<asp:textbox id="txtProductID" runat="server" CssClass="TextBox" Width="90%" MaxLength="20"></asp:textbox>
                        </td>
						<td class="musttitle" align="center" width="15%"><asp:Literal 
                                ID="ltlGaranteeDate" Text="保固日期"
                                runat="server"></asp:Literal>
                        </td>
						<td  width="35%">&nbsp;
								<uc2:Calendar runat="server" ID="txtGaranteeDate" />
						  <asp:CheckBox runat="server" ID="ckbHasGarantee" Text="是否有保固期限"  Checked="true"/>
                        </td>
					</tr>
					<tr>
						<td class="musttitle" align="center" ><asp:Literal ID="ltlProductName" Text="産品名稱"
                                runat="server"></asp:Literal>
                        </td>
						<td width="85%" colspan="3" >&nbsp;
								<asp:textbox id="txtProductName" runat="server" CssClass="TextRead"  Width="96%" MaxLength="120"></asp:textbox>
                        </td>
					</tr>
					<tr>
						<td class="smalltitle" align="center" ><asp:Literal 
                                ID="ltlDescription" Text="生産履歷描述"
                                runat="server"></asp:Literal>
                        </td>
						<td  width="85%" colspan="3" height="70">&nbsp;
								<asp:textbox id="txtDescription" runat="server" CssClass="TextBox" Width="96%" MaxLength="200" TextMode="MultiLine"
									Height="59px" ForeColor="Black"></asp:textbox>
                         </td>
					</tr>
					<tr>
						<td class="smalltitle" align="center" ><asp:Literal 
                                ID="ltlEPResumeID" Text="企業生産履歷編號"
                                runat="server"></asp:Literal>
                        </td>
						<td  width="85%" colspan="3">&nbsp;
								<asp:textbox id="txtEPResumeID" runat="server" CssClass="TextBox" Width="96%" MaxLength="20" 
									 ></asp:textbox>
                         </td>
					</tr>
					<tr>
						<td class="smalltitle" align="center" ><asp:Literal 
                                ID="ltlResumeOfficalUrl" Text="生産履歷網址"
                                runat="server"></asp:Literal>
                        </td>
						<td  width="85%" colspan="3" >&nbsp;
								<asp:textbox id="txtResumeOfficalUrl" runat="server" CssClass="TextBox" Width="96%" MaxLength="50"></asp:textbox>
                         </td>
					</tr>
					<tr>
						<td class="musttitle" align="center" ><asp:Literal 
                                ID="ltlOfficalUrl" Text="產品官方網址"
                                runat="server"></asp:Literal>
                        </td>
						<td  width="85%" colspan="3" >&nbsp;
								<asp:textbox id="txtOfficalUrl" runat="server" CssClass="TextBox" Width="96%" MaxLength="50"  ></asp:textbox>
                         </td>
					</tr>
					<tr>
						<td class="musttitle" align="center" ><asp:Literal 
                                ID="ltlServicePhone" Text="客服電話"
                                runat="server"></asp:Literal>
                        </td>
						<td  width="85%" colspan="3" >&nbsp;
								<asp:textbox id="txtServicePhone" runat="server" CssClass="TextBox" Width="96%" MaxLength="20"  ></asp:textbox>
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
						<td class="smalltitle" align="center"><asp:Literal ID="ltlEP_CheckUserName" Text="企業覆核人員"
                                runat="server"></asp:Literal></td>
						<td >&nbsp;
								<asp:textbox id="txtEP_CheckUserName" runat="server" CssClass="TextRead" Width="90%" MaxLength="20"></asp:textbox>
                        </td>
						<td class="smalltitle" align="center"><asp:Literal ID="ltlEP_CheckDate" Text="企業覆核日期"
                                runat="server"></asp:Literal>
						
						</td>
						<td >&nbsp;
								<asp:textbox id="txtEP_CheckDate" runat="server" CssClass="TextRead" Width="90%" 
                                MaxLength="20"></asp:textbox>
                        </td>
					</tr>
					 <tr>
						<td class="smalltitle" align="center"><asp:Literal ID="ltlBU_CheckUserName" Text="營運覆核人員"
                                runat="server"></asp:Literal></td>
						<td >&nbsp;
								<asp:textbox id="txtBU_CheckUserName" runat="server" CssClass="TextRead" Width="90%" MaxLength="20"></asp:textbox>
                        </td>
						<td class="smalltitle" align="center"><asp:Literal ID="ltlBU_CheckDate" Text="營運覆核日期"
                                runat="server"></asp:Literal>
						
						</td>
						<td >&nbsp;
								<asp:textbox id="txtBU_CheckDate" runat="server" CssClass="TextRead" Width="90%" 
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
			</div>
      </div>
       </ContentTemplate>
   </asp:UpdatePanel> 
		</form>
</body>
</html>
