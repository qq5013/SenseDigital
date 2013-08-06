<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PersonView.aspx.cs" Inherits="WebUI_BusinessUnit_PersonView" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />  
    <title></title> 
        <link href="~/Styles/Main.css" type="text/css" rel="stylesheet" /> 
        <link href="~/Styles/op.css" type="text/css" rel="stylesheet" />
        <script type="text/javascript" src='<%=ResolveUrl("~/Scripts/JQuery/jquery-1.8.3.min.js") %>'></script>
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Commons.js") %>'></script>
         <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Resize.js") %>'></script>  
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
              <div id="surdiv" style="overflow:auto">
            <table id="Table1" class="maintable" cellspacing="0" cellpadding="0" width="100%" align="center" bordercolor="#ffffff"
				border="1" runat="server">			
					<tr runat="server">
						<td  valign="middle" align="left" colspan="4" height="22" class="title1" 
                            runat="server">
							<p><asp:Literal ID="ltlTitle" Text="營運人員資料[ 單筆明細畫面 ]" runat="server"></asp:Literal></p>
						</td>
					</tr>							
						
					<tr runat="server">
						<td class="musttitle" align="center" width="15%" runat="server" ><asp:Literal ID="ltlPersonID" Text="人員編號"
                                runat="server"></asp:Literal>
                        </td>
						<td width="35%" runat="server" >&nbsp;
								<asp:textbox id="txtPersonID" runat="server" CssClass="TextRead" 
                                ReadOnly="True"  Width="90%" MaxLength="10"></asp:textbox>
                        </td>
						<td class="musttitle" align="center" width="15%" runat="server"><asp:Literal 
                                ID="ltlDepartmentID" Text="所屬部門"
                                runat="server"></asp:Literal>
                        </td>
						<td  width="35%" runat="server">&nbsp;
								<asp:DropDownList ID="ddlDepartmentID" Width="90%" runat="server"></asp:DropDownList>
                        </td>
					</tr>
                    		
					<tr runat="server">
						<td class="musttitle" align="center" width="15%" runat="server" ><asp:Literal ID="ltlPersonName" Text="人員姓名"
                                runat="server"></asp:Literal>
                        </td>
						<td width="35%" runat="server" >&nbsp;
								<asp:textbox id="txtPersonName" runat="server" CssClass="TextRead" 
                                ReadOnly="True" Width="90%" MaxLength="20"></asp:textbox>
                        </td>
						<td class="smalltitle" align="center" width="15%" runat="server"><asp:Literal 
                                ID="ltlPersonEName" Text="英文姓名"
                                runat="server"></asp:Literal>
                        </td>
						<td  width="35%" runat="server">&nbsp;
								<asp:textbox id="txtPersonEName" runat="server" CssClass="TextRead" 
                                ReadOnly="True" Width="90%" 
                                MaxLength="20"></asp:textbox>
                        </td>
					</tr>
                    <tr runat="server">
						<td class="musttitle" align="center" width="15%" runat="server" ><asp:Literal ID="ltlPost" Text="職務名稱"
                                runat="server"></asp:Literal>
                        </td>
						<td width="35%" runat="server" >&nbsp;
								<asp:textbox id="txtPost" runat="server" CssClass="TextRead" 
                                ReadOnly="True" Width="90%" MaxLength="20"></asp:textbox>
                        </td>
						<td class="smalltitle" align="center" width="15%" runat="server"><asp:Literal 
                                ID="ltlIDNumber" Text="身份證號"
                                runat="server"></asp:Literal>
                        </td>
						<td  width="35%" runat="server">&nbsp;
								<asp:textbox id="txtIDNumber" runat="server" CssClass="TextRead" 
                                ReadOnly="True" Width="90%" 
                                MaxLength="20"></asp:textbox>
                        </td>
					</tr>
                    <tr runat="server">
						<td class="smalltitle" align="center" width="15%" runat="server" ><asp:Literal ID="ltlBirthday" Text="出生日期"
                                runat="server"></asp:Literal>
                        </td>
						<td width="35%" runat="server" >&nbsp;
								<asp:textbox id="txtBirthday" runat="server" CssClass="TextRead" 
                                ReadOnly="True" Width="90%" MaxLength="20"></asp:textbox>
                        </td>
						<td class="smalltitle" align="center" width="15%" runat="server"><asp:Literal 
                                ID="ltlOnJobDate" Text="到職日期"
                                runat="server"></asp:Literal>
                        </td>
						<td  width="35%" runat="server">&nbsp;
								<asp:textbox id="txtOnJobDate" runat="server" CssClass="TextRead" 
                                ReadOnly="True" Width="90%" 
                                MaxLength="20"></asp:textbox>
                        </td>
					</tr>
                      <tr runat="server">
						<td class="smalltitle" align="center" width="15%" runat="server" ><asp:Literal ID="ltlCellPhone" Text="行動電話"
                                runat="server"></asp:Literal>
                        </td>
						<td width="35%" runat="server" >&nbsp;
								<asp:textbox id="txtCellPhone" runat="server" CssClass="TextRead" 
                                ReadOnly="True" Width="90%" MaxLength="16"></asp:textbox>
                        </td>
						<td class="smalltitle" align="center" width="15%" runat="server"><asp:Literal 
                                ID="ltlLeftJobDate" Text="離職日期"
                                runat="server"></asp:Literal>
                        </td>
						<td  width="35%" runat="server">&nbsp;
								<asp:textbox id="txtLeftJobDate" runat="server" CssClass="TextRead" 
                                ReadOnly="True" Width="90%" 
                                MaxLength="20"></asp:textbox>
                        </td>
					</tr>
                     <tr runat="server">
						<td class="smalltitle" align="center" width="15%" runat="server" ><asp:Literal ID="ltlHomePhone" Text="住家電話"
                                runat="server"></asp:Literal>
                        </td>
						<td width="35%"  colspan="3" runat="server">&nbsp;
								<asp:textbox id="txtHomePhone" runat="server" CssClass="TextRead" 
                                ReadOnly="True" Width="37%" MaxLength="16"></asp:textbox>
                        </td>
						
					</tr>
					<tr runat="server">
						<td class="smalltitle" align="center" runat="server" ><asp:Literal ID="ltlEmail" Text="E-MAIL"
                                runat="server"></asp:Literal>
                        </td>
						<td width="85%" colspan="3" runat="server" >&nbsp;
								<asp:textbox id="txtEmail" runat="server" CssClass="TextRead" 
                                ReadOnly="True" Width="96%" MaxLength="30"></asp:textbox>
                        </td>
					</tr>
                    <tr runat="server">
						<td class="smalltitle" align="center" runat="server" ><asp:Literal ID="ltlContactAddress" Text="連絡地址"
                                runat="server"></asp:Literal>
                        </td>
						<td width="85%" colspan="3" runat="server" >&nbsp;
								<asp:textbox id="txtContactAddress" runat="server" CssClass="TextRead" 
                                ReadOnly="True" Width="96%" MaxLength="100"></asp:textbox>
                        </td>
					</tr>
                    <tr runat="server">
						<td class="smalltitle" align="center" runat="server" ><asp:Literal ID="ltlHomeAddress" Text="住家地址"
                                runat="server"></asp:Literal>
                        </td>
						<td width="85%" colspan="3" runat="server" >&nbsp;
								<asp:textbox id="txtHomeAddress" runat="server" CssClass="TextRead" 
                                ReadOnly="True" Width="96%" MaxLength="100"></asp:textbox>
                        </td>
					</tr>
					 <tr runat="server">
						<td class="smalltitle" align="center" width="15%" runat="server" ><asp:Literal ID="ltlLinkMan" Text="緊急聯絡人"
                                runat="server"></asp:Literal>
                        </td>
						<td width="35%"  height="22px" align="left" runat="server">
                          <table width="92%" style="height:100%;margin:0px;padding:0px;">
                           <tr>
                           <td width="40%">&nbsp;<asp:textbox id="txtLinkMan" runat="server" 
                                   CssClass="TextRead" ReadOnly="True" Width="92%" MaxLength="15"></asp:textbox>
                           </td>
                            <td width="20%" class="smalltitle" align="center">
                              <asp:Literal ID="ltlRelation" Text="關係" runat="server"></asp:Literal>
                           </td>
                            <td width="38%">
                              <asp:textbox id="txtRelation" runat="server" CssClass="TextRead" ReadOnly="True" 
                                    Width="99%" MaxLength="15"></asp:textbox>
                        </td>
                        
                          </tr></table>
						</td>	
                            
						<td class="smalltitle" align="center" width="15%" runat="server"><asp:Literal 
                                ID="ltlLinkPhone" Text="緊急聯絡電話"
                                runat="server"></asp:Literal>
                        </td>
						<td  width="35%" runat="server">&nbsp;
								<asp:textbox id="txtLinkPhone" runat="server" CssClass="TextRead" 
                                ReadOnly="True" Width="90%" 
                                MaxLength="16"></asp:textbox>
                        </td>
					</tr>
                    <tr runat="server">
						<td class="smalltitle" align="center" runat="server" ><asp:Literal 
                                ID="ltlMemo" Text="備&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;註"
                                runat="server"></asp:Literal>
                        </td>
						<td  width="85%" colspan="3" height="70" runat="server">&nbsp;
								<asp:textbox id="txtMemo" runat="server"  CssClass="TextRead" 
                                ReadOnly="True" Width="96%" MaxLength="2000" TextMode="MultiLine"
									Height="59px" ForeColor="Black"></asp:textbox>
                         </td>
					</tr>
                    <tr runat="server">
						<td class="smalltitle" align="center" runat="server"><asp:Literal ID="ltlCreateUserName" Text="建檔人員"
                                runat="server"></asp:Literal>
                        </td>
						<td runat="server" >&nbsp;
								<asp:textbox id="txtCreateUserName" runat="server" CssClass="TextRead" Width="90%" 
                                MaxLength="20" ReadOnly="True"></asp:textbox>
                         </td>
						<td class="smalltitle" align="center" runat="server">                            
                            <asp:Literal ID="ltlCreateDate" runat="server" Text="建檔日期"></asp:Literal>							
						    
						</td>
						<td runat="server">&nbsp;
								<asp:textbox id="txtCreateDate" runat="server" CssClass="TextRead" Width="90%" 
                                MaxLength="20" ReadOnly="True"></asp:textbox></td>
					</tr>
                    <tr runat="server">
						<td class="smalltitle" align="center" runat="server"><asp:Literal ID="ltlLastModifyUserName" Text="異動人員"
                                runat="server"></asp:Literal>
                        </td>
						<td runat="server" >&nbsp;
								<asp:textbox id="txtLastModifyUserName" runat="server" CssClass="TextRead" 
                                Width="90%" MaxLength="20" ReadOnly="True" ></asp:textbox>
                        </td>
						<td class="smalltitle" align="center" runat="server">							
						 
                            <asp:Literal ID="ltlLastModifyDate" Text="異動日期"
                                runat="server"></asp:Literal>
						</td>
						<td runat="server" >&nbsp;
                        <asp:textbox id="txtLastModifyDate" runat="server" CssClass="TextRead" Width="90%" 
                                MaxLength="20" ReadOnly="True"></asp:textbox></td>
					</tr>
                    <tr runat="server">
						<td class="smalltitle" align="center" runat="server"><asp:Literal ID="ltlCheckUserName" Text="覆核人員"
                                runat="server"></asp:Literal>
                        </td>
						<td runat="server" >&nbsp;
								<asp:textbox id="txtCheckUserName" runat="server" CssClass="TextRead" 
                                Width="90%" MaxLength="20" ReadOnly="True"></asp:textbox>
                        </td>
						<td class="smalltitle" align="center" runat="server">	
                            <asp:Literal ID="ltlCheckDate" Text="覆核日期"
                                runat="server"></asp:Literal>
						</td>
						<td runat="server" >&nbsp;
								<asp:textbox id="txtCheckDate" runat="server" CssClass="TextRead" 
                                Width="70%" ReadOnly="True"
                                MaxLength="20"></asp:textbox>&nbsp;
                                <asp:button id="btnCheck" runat="server" Text="覆核" onclick="btnCheck_Click"
                                            CssClass="but" Width="75px">
                                    </asp:button>                          
                        </td>
					</tr>
                    <tr runat="server">
						<td class="smalltitle" align="center" runat="server"><asp:Literal ID="ltlStopUserName" Text="停用人員"
                                runat="server"></asp:Literal>
                        </td>
						<td runat="server" >&nbsp;
								<asp:textbox id="txtStopUserName" runat="server" CssClass="TextRead" Width="90%" MaxLength="20"></asp:textbox>
                        </td>
						<td class="smalltitle" align="center" runat="server">
						 
                            <asp:Literal ID="ltlStopDate" Text="停用日期"
                                runat="server"></asp:Literal>
						</td>
						<td runat="server" >&nbsp;
								<asp:textbox id="txtStopDate" runat="server" CssClass="TextRead" Width="70%" 
                                MaxLength="20"></asp:textbox> &nbsp;<asp:button id="btnStop" runat="server" Text="停用" 
                                            CssClass="but" Width="75px" onclick="btnStop_Click">
                                    </asp:button>
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
