<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Parameter.aspx.cs" Inherits="WebUI_System_Parameter" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />  
    <title></title>
        <link href="~/Styles/Main.css" type="text/css" rel="stylesheet" /> 
        <link href="~/Styles/op.css" type="text/css" rel="stylesheet" /> 
        <script type="text/javascript" src='<%=ResolveUrl("~/Scripts/JQuery/jquery-1.8.3.min.js") %>'></script>
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Commons.js") %>'></script>
    <script src="Js/AutoCode.js" type="text/javascript"></script>
        <script type="text/javascript">
            
            function Cancel() {
                return confirm("<%=Resources.Resource.Question_Cancel %>");
            }

            function Modify() {
                var page = "ParameterCloseDate.aspx";
                var AnnounceID = $("#txtCloseDate").val();
                var TitleName = escape("<%=Resources.Resource.CloseDateModify %>");
                var strReturn = window.showModalDialog('TempPage.aspx?page=' + page + '&AnnounceID=' + AnnounceID + '&FormID=<% =FormID%>&TitleName=' + TitleName, window, 'DialogHeight:240px;DialogWidth:400px;help:no;scroll:no');
                if (strReturn == "0")
                    return false;
                else
                    return true;
            }
            function Save() {
                $("#txtCompanyNo").val(trim($("#txtCompanyNo").val()));
                $("#txtCompanyName").val(trim($("#txtCompanyName").val()));
                if ($("#txtCompanyNo").val() == "") {
                    alert("<%=Resources.Resource.BU_DepartmentID_NotNull %>");
                    $("#txtCompanyNo").focus();
                    return false;
                }
                if ($("#txtCompanyName").val() == "") {
                    alert("<%=Resources.Resource.BU_CompanyName_NotNull %>");
                    $("#txtCompanyName").focus();
                    return false;
                }
                if ($("#txtUnionID").val() == "") {
                    alert("<%=Resources.Resource.BU_UnionID_NotNull %>");
                    $("#txtUnionID").focus();
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
                        &nbsp;&nbsp;<asp:Button ID="btnCancel" runat="server" Text="放棄" CssClass="ButtonCancel" OnClientClick="return Cancel()"
                            onclick="btnCancel_Click" />
                        <asp:Button ID="btnEdit" runat="server" Text="修改" CssClass="ButtonModify" onclick="btnEdit_Click" />
                        <asp:Button ID="btnSave" runat="server" Text="保存" CssClass="ButtonSave" OnClientClick="return Save()"
                            onclick="btnSave_Click" />
                        <asp:Button ID="btnExit" runat="server" Text="離開" OnClientClick="return Exit()" CssClass="ButtonExit" />
                    </td>
                </tr>
       </table>
      <table id="Table1" class="maintable" cellspacing="0" cellpadding="0" width="100%" align="center" bordercolor="#ffffff"
				border="1" runat="server">			
					<tr>
						<td style="HEIGHT: 29px" valign="middle" align="center" colspan="4" height="22">
							<table cellspacing="0" cellpadding="0" width="100%" border="0">
								<tr>
									<td class="titline1">
										<table cellspacing="0" cellpadding="0" width="100%" border="0">
											<tr>
												<td class="title1"><p><asp:Literal ID="ltlTitle" Text="營運單位參數設定" runat="server"></asp:Literal></p></td>
											</tr>
											
										</table>
									</td>
									<td class="titline4" width="85%" align="right">                                     
                                    </td>
									
								</tr>
							</table>
						</td>
					</tr>							
                    <tr>
						<td class="smalltitle" align="center" width="15%" ><asp:Literal ID="ltlOrderCode" Text="標籤訂購單編碼原則"
                                runat="server"></asp:Literal>
                        </td>
						<td width="35%" >&nbsp;
								<asp:textbox id="txtOrderCodeView" runat="server" CssClass="TextRead" 
                                Width="85%"  ></asp:textbox> 
                            <asp:Button ID="btnOrderCode" runat="server" CssClass="ButtonCss" Text="..." 
                                />
                            &nbsp;<asp:Button ID="btnCopy" runat="server" CssClass="ButtonCss" 
                                Text="複" />
                        </td>
						<td class="smalltitle" align="center" width="15%"><asp:Literal  ID="ltlPercentDecimalDigits" Text="比例數值設定"
                                runat="server"></asp:Literal>
                        </td>
						<td  width="35%">&nbsp;
								<asp:DropDownList ID="ddlPercentDecimalDigits" Width="88%" runat="server">
                                    <asp:ListItem>0</asp:ListItem>
                                    <asp:ListItem>1</asp:ListItem>
                                    <asp:ListItem>2</asp:ListItem>
                                    <asp:ListItem>3</asp:ListItem>
                                    <asp:ListItem>4</asp:ListItem>
                                    <asp:ListItem>5</asp:ListItem>
                                    <asp:ListItem>6</asp:ListItem>
                            </asp:DropDownList>
                            <asp:Literal ID="ltlDigits" runat="server" Text="位"></asp:Literal>
                        </td>
					</tr>				
                    <tr>
						<td class="smalltitle" align="center" width="15%" >
                            <asp:Literal ID="ltlScheduleCode" runat="server" Text="生產計劃單編碼原則"></asp:Literal>
                        </td>
						<td width="35%" >&nbsp;
                            <asp:TextBox ID="txtScheduleCodeView" runat="server" CssClass="TextRead" 
                                MaxLength="26" Width="85%"></asp:TextBox>
                            <asp:Button ID="btnScheduleCode" runat="server" CssClass="ButtonCss" 
                                Text="..." />
                            &nbsp;</td>
						<td class="smalltitle" align="center" width="15%">
                            <asp:Literal ID="ltlServiceYears" runat="server" Text="預設使用年限"></asp:Literal>
                        </td>
						<td  width="35%">&nbsp;
                            <asp:TextBox ID="txtServiceYears" runat="server" CssClass="TextBox" 
                                MaxLength="20" Width="88%"></asp:TextBox>
                            <asp:Literal ID="ltlYears" runat="server" Text="年"></asp:Literal>
                        </td>
					</tr>
					 <tr>
                         <td align="center" class="smalltitle" width="15%">
                             <asp:Literal ID="ltlProductionCode" runat="server" Text="標籤生產單編碼原則"></asp:Literal>
                         </td>
                         <td width="35%">
                             &nbsp;
                             <asp:TextBox ID="txtProductionCodeView" runat="server" CssClass="TextRead" 
                                 MaxLength="26" Width="85%"></asp:TextBox>
                             <asp:Button ID="btnProductionCode" runat="server" CssClass="ButtonCss" 
                                 Text="..." />
                         </td>
                         <td align="center" class="smalltitle" width="15%">
                             <asp:Literal ID="ltlEnableMonths" runat="server" Text="預設啟用期限"></asp:Literal>
                         </td>
                         <td width="35%">
                             &nbsp;
                             <asp:DropDownList ID="ddlEnableMonths" runat="server" Width="88%">
                                 <asp:ListItem>1</asp:ListItem>
                                 <asp:ListItem>2</asp:ListItem>
                                 <asp:ListItem>3</asp:ListItem>
                                 <asp:ListItem>4</asp:ListItem>
                                 <asp:ListItem>5</asp:ListItem>
                                 <asp:ListItem>6</asp:ListItem>
                                 <asp:ListItem>7</asp:ListItem>
                                 <asp:ListItem>8</asp:ListItem>
                                 <asp:ListItem>9</asp:ListItem>
                                 <asp:ListItem>10</asp:ListItem>
                                 <asp:ListItem>11</asp:ListItem>
                                 <asp:ListItem>12</asp:ListItem>
                             </asp:DropDownList>
                             <asp:Literal ID="ltlMonths" runat="server" Text="月"></asp:Literal>
                         </td>
                    </tr>
					 <tr>
                         <td align="center" class="smalltitle" width="15%">
                             <asp:Literal ID="ltlInStockCode" runat="server" Text="標籤入庫單編碼原則"></asp:Literal>
                         </td>
                         <td width="35%">
                             &nbsp;
                             <asp:TextBox ID="txtInStockCodeView" runat="server" CssClass="TextRead" 
                                 MaxLength="26" Width="85%"></asp:TextBox>
                             <asp:Button ID="btnInStockCode" runat="server" CssClass="ButtonCss" Text="..." />
                         </td>
                         <td align="center" class="smalltitle" width="15%">
                             <asp:Literal ID="ltlClearDate" runat="server" Text="交易單上次結清日期"></asp:Literal>
                         </td>
                         <td width="35%">
                             &nbsp;
                             <asp:TextBox ID="txtClearDate" runat="server" CssClass="TextRead" 
                                 MaxLength="20" ReadOnly="True" Width="90%"></asp:TextBox>
                         </td>
                    </tr>
                    <tr>
						<td class="smalltitle" align="center" ><asp:Literal ID="ltlInvalidLabelCode" Text="生產作廢單編碼原則"  runat="server"></asp:Literal>
                        </td>
						<td>&nbsp;
								<asp:textbox id="txtInvalidLabelCodeView" runat="server" CssClass="TextRead" 
                                Width="85%" MaxLength="26" ></asp:textbox>
                            <asp:Button ID="btnInvalidLabelCode" runat="server" CssClass="ButtonCss" Text="..." />
                        </td>
                        <td class="smalltitle" align="center" width="15%">
                            <asp:Literal ID="ltlCloseDate" runat="server" Text="關帳日期"></asp:Literal>
                        </td>
						<td  width="35%">&nbsp;
								<asp:TextBox ID="txtCloseDate" runat="server" CssClass="TextRead" 
                                MaxLength="26" Width="90%"></asp:TextBox>
                        </td>
					</tr>
                     <tr>
						<td class="smalltitle" align="center" >
                            <asp:Literal ID="ltlDeliverCode" runat="server" Text="標籤出貨單編碼原則"></asp:Literal>
                        </td>
						<td>&nbsp;
                            <asp:TextBox ID="txtDeliverCodeView" runat="server" CssClass="TextRead" 
                                MaxLength="26" Width="85%"></asp:TextBox>
                            <asp:Button ID="btnDeliverCode" runat="server" CssClass="ButtonCss" 
                                Text="..." />
                            &nbsp;</td>
                        <td class="smalltitle" align="center" width="15%">
                            <asp:Literal ID="ltlLastModifyUserName" runat="server" Text="關帳日期異動人員"></asp:Literal>
                         </td>
						<td  width="35%">&nbsp;&nbsp;
                            <asp:TextBox ID="txtLastModifyUserName" runat="server" CssClass="TextRead" 
                                MaxLength="20" ReadOnly="True" Width="90%"></asp:TextBox>
                         </td>
					</tr>
					
                    <tr>
                        <td align="center" class="smalltitle">
                            <asp:Literal ID="ltlReturnLabelCode" runat="server" Text="退貨入庫單編碼原則"></asp:Literal>
                        </td>
                        <td>
                            &nbsp;
                            <asp:TextBox ID="txtReturnLabelCodeView" runat="server" CssClass="TextRead" 
                                MaxLength="26" Width="85%"></asp:TextBox>
                            <asp:Button ID="btnReturnLabelCode" runat="server" CssClass="ButtonCss" 
                                Text="..." />
                            &nbsp;</td>
                        <td align="center" class="smalltitle" width="15%">
                            <asp:Literal ID="ltlLastModifyDate" runat="server" Text="關帳日期異動日期"></asp:Literal>
                        </td>
                        <td width="35%">
                            &nbsp;&nbsp;
                            <asp:TextBox ID="txtLastModifyDate" runat="server" CssClass="TextRead" 
                                MaxLength="20" ReadOnly="True" Width="90%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" class="smalltitle">
                            <asp:Literal ID="ltlTransferCode" runat="server" Text="倉庫調撥單編碼原則"></asp:Literal>
                        </td>
                        <td>
                            &nbsp;
                            <asp:TextBox ID="txtTransferCodeView" runat="server" CssClass="TextRead" 
                                MaxLength="26" Width="85%"></asp:TextBox>
                            <asp:Button ID="btnTransferCode" runat="server" CssClass="ButtonCss" 
                                Text="..." />
                            &nbsp;</td>
                        <td align="center" class="smalltitle" width="15%">
                            &nbsp;</td>
                        <td width="35%">
                            &nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnModify" runat="server" CssClass="but" 
                                onclick="btnModify_Click" OnClientClick="return Modify()" Text="關帳日期異動" />
                        </td>
                    </tr>
                    <tr>
                        <td align="center" class="smalltitle">
                            <asp:Literal ID="ltlEnableLabelNoCode" runat="server" Text="標籤啟用單編碼原則"></asp:Literal>
                        </td>
                        <td>
                            &nbsp;
                            <asp:TextBox ID="txtEnableLabelNoCodeView" runat="server" CssClass="TextRead" 
                                MaxLength="26" Width="85%"></asp:TextBox>
                            <asp:Button ID="btnEnableLabelNoCode" runat="server" CssClass="ButtonCss" 
                                Text="..." />
                        </td>
                        <td align="center" class="smalltitle" width="15%">
                            &nbsp;</td>
                        <td width="35%">
                            &nbsp; &nbsp;</td>
                    </tr>
					
                    <tr>
						<td class="smalltitle" align="center" ><asp:Literal ID="ltlLabelNoActionCode" Text="序號異動單編碼原則"  runat="server"></asp:Literal>
                        </td>
						<td>&nbsp;
								<asp:textbox id="txtLabelNoActionCodeView" runat="server" CssClass="TextRead" 
                                Width="85%" MaxLength="26" ></asp:textbox>
                                <asp:Button ID="btnLabelNoActionCode" runat="server" CssClass="ButtonCss" Text="..." />
                        </td>
                        <td class="smalltitle" align="center" width="15%">&nbsp;</td>
						<td  width="35%">&nbsp;
								</td>
					</tr>
                    <tr>
						<td class="smalltitle" align="center" ><asp:Literal ID="ltlBatchActionCode" Text="批次異動單編碼原則"  runat="server"></asp:Literal>
                        </td>
						<td >&nbsp;
								<asp:textbox id="txtBatchActionCodeView" runat="server" 
                                CssClass="TextRead" Width="85%" MaxLength="26" ></asp:textbox>
                                <asp:Button ID="btnBatchActionCode" runat="server" CssClass="ButtonCss" Text="..." />
                        </td>
                        <td class="smalltitle" align="center" width="15%">&nbsp;</td>
						<td  width="35%">&nbsp;
								</td>
					</tr>
                    <tr>
						<td class="smalltitle" align="center"><asp:Literal ID="ltlLabelRegisterCode" Text="資訊登錄單編碼原則"
                                runat="server"></asp:Literal>
                        </td>
						<td >&nbsp;
								<asp:textbox id="txtLabelRegisterCodeView" runat="server" CssClass="TextRead" Width="85%" 
                                MaxLength="26"></asp:textbox>
                                <asp:Button ID="btnLabelRegisterCode" runat="server" CssClass="ButtonCss" Text="..." />
                         </td>
						<td class="smalltitle" align="center">                            
                            &nbsp;</td>
						<td>&nbsp;
								</td>
					</tr>
                    
			        <tr>
                        <td align="center" class="smalltitle">
                            <asp:Literal ID="ltlBatchRegisterCode" runat="server" Text="批次資訊登錄單編碼原則"></asp:Literal>
                        </td>
                        <td>
                            &nbsp;
                            <asp:TextBox ID="txtBatchRegisterCodeView" runat="server" CssClass="TextRead" 
                                MaxLength="26" Width="85%"></asp:TextBox>
                            <asp:Button ID="btnBatchRegisterCode" runat="server" CssClass="ButtonCss" 
                                Text="..." />
                            &nbsp;</td>
                        <td align="center" class="smalltitle">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    
			</table>
    </div>
    </ContentTemplate>
            </asp:UpdatePanel> 
            <input type="hidden" id="txtOrderCode" runat="server" />
            <input type="hidden" id="txtScheduleCode" runat="server" />
            <input type="hidden" id="txtProductionCode" runat="server" />
            <input type="hidden" id="txtInStockCode" runat="server" />
            <input type="hidden" id="txtInvalidLabelCode" runat="server" />            
            <input type="hidden" id="txtDeliverCode" runat="server" />
            <input type="hidden" id="txtReturnLabelCode" runat="server" />
            <input type="hidden" id="txtTransferCode" runat="server" />
            <input type="hidden" id="txtEnableLabelNoCode" runat="server" />
            <input type="hidden" id="txtLabelNoActionCode" runat="server" />
            <input type="hidden" id="txtBatchActionCode" runat="server" />
            <input type="hidden" id="txtLabelRegisterCode" runat="server" />
            <input type="hidden" id="txtBatchRegisterCode" runat="server" />            
    </form>
</body>
</html>
