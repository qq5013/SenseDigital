<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProductView.aspx.cs" Inherits="WebUI_Enterprise_ProductView" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />  
    <title></title> 
        <link href="~/Styles/Main.css" type="text/css" rel="stylesheet" /> 
        <link href="~/Styles/op.css" type="text/css" rel="stylesheet" />
        <link rel="stylesheet" type="text/css" href="~/ext-3.3.1/resources/css/ext-all.css" /> 	   
        <script type="text/javascript" src='<%=ResolveUrl("~/Scripts/JQuery/jquery-1.8.3.min.js") %>'></script> 
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Commons.js") %>'></script>
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Resize.js") %>'></script>  
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Ajax.js") %>'></script>     

        <script type='text/javascript'>
            function pEdit(op) {
                var formId = "EP_ProductNoCheck";
                var id = $("#txtProductID").val();
                if (!op)
                    id = "";
                var exist = IsExistsByFilter(formId, $("#txtEnterpriseID").val(), id, "<%= cnKey%>");
                
                if (exist == "2") {
                    alert("<%=Resources.Resource.EP_ProductEnterpriseChecked%>");
                    return false;
                }
                if (op && FormID == "EP_Product") {
                    if (exist == "1") {
                        alert("<%=Resources.Resource.EP_ProductNo_Exists%>");
                        return false;
                    }
                }
                if (op)
                    location.href = "ProductEdit.aspx?FormID=<%= FormID%>&ID=" + id + "";
                else
                    location.href = "ProductEdit.aspx?FormID=" + formId + "&ID=";

                return false;
            }
            function Open() {
                var TitleName = escape("<%=Resources.Resource.ProductCompare %>");
                var page = "ProductModify.aspx";
                var EnterpriseID = $("#ddlEnterpriseID").val();
                var strReturn = window.showModalDialog('TempPage.aspx?page=' + page + '&EnterpriseID=' + EnterpriseID + '&FormID=<% =FormID%>&TitleName=' + TitleName, window, 'DialogHeight:350px;DialogWidth:800px;help:no;scroll:no');
                //return false;
            }
            function Modify() {
                
                var TitleName = escape("<%=Resources.Resource.ProductCompare %>");
                var EnterpriseID = $("#txtEnterpriseID").val();
                var ID = $("#txtProductID").val();
                //window.showModalDialog('ProductModify.aspx?EnterpriseID=' + EnterpriseID + '&FormID=<% =FormID%>&TitleName=' + TitleName, window, 'dialogHeight:230px;dialogWidth:420px;toolbar=no,status=yes,resizable=no');
                window.showModalDialog('ProductModify.aspx?EnterpriseID=' + EnterpriseID + '&ID=' + ID + '&FormID=<% =FormID%>&TitleName=' + TitleName, window, 'dialogHeight:250px;dialogWidth:800px;toolbar=no,status=yes,resizable=no');
                return false;
            }
            function SourceView() {
                var ID = $("#txtProductID").val();
                addTab('../WebUI/Enterprise/ProductVIew.aspx?FormID=EP_Product&ID=' + ID, '<%=Resources.Resource.EP_ProductTabName %>', 'tab_3_8');
                return false;
            }
            function addTab(url, name, idName) {
                //alert(tabs.items);
                if (this.parent.tabs.findById(idName) != null) {
                    this.parent.tabs.setActiveTab(idName);
                    this.parent.window.frames["if_" + idName].location.reload();
                    return;
                }
                var closeable = true;

                this.parent.tabs.add({
                    title: name,
                    id: idName,
                    layout: 'fit',
                    iconCls: 'tabs',
                    html: "<iframe id='if_" + idName + "' src='" + url + "' width='100%' height='100%' frameborder='1' style='overflow:hidden' scrolling='no' ></iframe>",
                    closable: closeable
                }).show();
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
                    <td align="right" style="width:60%">
                        <asp:Button ID="btnFirst" runat="server" Text="首筆" CssClass="ButtonFirst" 
                            onclick="btnFirst_Click" />
                        <asp:Button ID="btnPre" runat="server" Text="上一筆" CssClass="ButtonPre" 
                            onclick="btnPre_Click" />
                        <asp:Button ID="btnNext" runat="server" Text="下一筆" CssClass="ButtonNext" 
                            onclick="btnNext_Click" />
                        <asp:Button ID="btnLast" runat="server" Text="尾筆" CssClass="ButtonLast" 
                            onclick="btnLast_Click" />
                    </td>
                    <td align="right">
                        <asp:Button ID="btnPrint" runat="server" Text="列印" CssClass="ButtonPrint" 
                            onclick="btnPrint_Click" />
                        <asp:Button ID="btnAdd" runat="server" Text="新增" CssClass="ButtonCreate" OnClientClick="return pEdit(false)" />
                        <asp:Button ID="btnDelete" runat="server" Text="刪除" CssClass="ButtonDel" OnClientClick="return ViewDelete()"
                            onclick="btnDelete_Click" />
                        <asp:Button ID="btnEdit" runat="server" Text="修改" CssClass="ButtonModify" 
                            OnClientClick="return pEdit(true);" />
                        <asp:Button ID="btnExit" runat="server" Text="離開" OnClientClick="return Exit()" CssClass="ButtonExit" />
                         <asp:Button ID="btnBack" runat="server" Text="返回" OnClientClick="return Back()" 
                            CssClass="ButtonBack" />
                    </td>
                </tr>
            </table>
           <div id="surdiv" style="overflow:auto">
            <table id="Table1" class="maintable" cellspacing="0" cellpadding="0" width="100%" align="center" bordercolor="#ffffff"
				border="1" runat="server">			
					<tr>
						<td  valign="middle" align="left" colspan="4" height="22" class="title1">
							<p><asp:Literal ID="ltlTitle" Text="產品資料查詢[ 單筆明細畫面 ]" runat="server"></asp:Literal></p>
						</td>
					</tr>							
					<tr>
						<td class="musttitle" align="center" width="15%"><asp:Literal ID="ltlEnterpriseID" Text="企業編號"
                                runat="server"></asp:Literal>
                        </td>
						<td width="85%" colspan="3" >&nbsp;
								<asp:textbox id="txtEnterpriseID" runat="server" CssClass="TextRead"  ReadOnly="true" Width="30%" MaxLength="6"></asp:textbox>
                                 <asp:textbox id="txtEnterpriseName" runat="server" CssClass="TextRead" ReadOnly="true" Width="65%" MaxLength="100"></asp:textbox>
                        </td>
					</tr>	
					<tr>
						<td class="musttitle" align="center" width="15%" ><asp:Literal ID="ltlProductID" Text="產品編號"
                                runat="server"></asp:Literal>
                        </td>
						<td width="35%" >&nbsp;
								<asp:textbox id="txtProductID" runat="server" CssClass="TextRead"  ReadOnly="true" Width="90%" MaxLength="20"></asp:textbox>
                        </td>
						<td  colspan="2" width="50%">
                          <table width="100%" style="height:100%;margin:0px;padding:0px;">
                           <tr>
                           <td width="20%" class="smalltitle" align="center"><asp:Literal ID="ltlGroupName" Text="大類"
                                runat="server"></asp:Literal>
                           </td>
                            <td width="30%" >
                              <asp:DropDownList ID="ddlGroupName" Width="90%" runat="server" Enabled="false">
                            </asp:DropDownList>
                           </td>
                            <td width="20%" class="smalltitle" align="center"><asp:Literal ID="ltlClassName" Text="小類"
                                runat="server"></asp:Literal>
                           </td>
                            <td width="30%">
                              <asp:DropDownList ID="ddlClassName" Width="80%" runat="server" Enabled="false">
                            </asp:DropDownList>
                        </td>
                        
                          </tr></table>
                          
                        </td>
					</tr>               
                   
                     <tr>
						<td class="musttitle" align="center" width="15%" ><asp:Literal ID="ltlProductName" Text="產品名稱"
                                runat="server"></asp:Literal>
                        </td>
						<td width="85%"  colspan="3">&nbsp;
								<asp:textbox id="txtProductName" runat="server" CssClass="TextRead"  ReadOnly="true" Width="86%" MaxLength="120"></asp:textbox>
                            &nbsp;
                            <asp:Button ID="btnSourceView" runat="server" CssClass="but" 
                                OnClientClick="return SourceView()" Text="原單查詢" Width="75px" />
                        </td>
						
					</tr>
					<tr>
						<td class="smalltitle" align="center" ><asp:Literal ID="ltlProductEName" Text="英文品名"
                                runat="server"></asp:Literal>
                        </td>
						<td width="85%" colspan="3" >&nbsp;
								<asp:textbox id="txtProductEName" runat="server" CssClass="TextRead"  ReadOnly="true" Width="96%" MaxLength="200"></asp:textbox>
                        </td>
					</tr>
                      <tr>
						<td class="smalltitle" align="center" width="15%" ><asp:Literal ID="ltlBarcode1" Text="條碼編號1"
                                runat="server"></asp:Literal>
                        </td>
						<td width="35%" >&nbsp;
								<asp:textbox id="txtBarcode1" runat="server" CssClass="TextRead"  ReadOnly="true" Width="90%" MaxLength="20"></asp:textbox>

                        </td>
						<td class="smalltitle" align="center" width="15%"><asp:Literal 
                                ID="ltlBarcode2" Text="條碼編號2"
                                runat="server"></asp:Literal>
                        </td>
						<td  width="35%">&nbsp;
								<asp:textbox id="txtBarcode2" runat="server" CssClass="TextRead"  ReadOnly="true" Width="90%" 
                                MaxLength="20"></asp:textbox>
                        </td>
					</tr>
                   <tr>
						<td class="musttitle" align="center" ><asp:Literal ID="ltlOfficalUrl" Text="產品官方網址"
                                runat="server"></asp:Literal>
                        </td>
						<td width="85%" colspan="3" >&nbsp;
								<asp:textbox id="txtOfficalUrl" runat="server" CssClass="TextRead"  ReadOnly="true" Width="92%" MaxLength="50"></asp:textbox>
                                &nbsp;
                            <asp:HyperLink ID="lnkOfficalUrl" Target="_blank" runat="server">Go</asp:HyperLink>
                        </td>
					</tr>	
                     <tr>
						<td class="musttitle" align="center" ><asp:Literal ID="ltlServicePhone" Text="客服電話"
                                runat="server"></asp:Literal>
                        </td>
						<td width="85%" colspan="3" >&nbsp;
								<asp:textbox id="txtServicePhone" runat="server" CssClass="TextRead"  ReadOnly="true" Width="96%" MaxLength="20"></asp:textbox>
                        </td>
					</tr>	
                      <tr>
						<td class="smalltitle" align="center" ><asp:Literal 
                                ID="ltlDescription" Text="產品描述"
                                runat="server"></asp:Literal>
                        </td>
						<td  width="85%" colspan="3" height="70">&nbsp;
								<asp:textbox id="txtDescription" runat="server" CssClass="TextRead"  ReadOnly="true" Width="96%" MaxLength="200" TextMode="MultiLine"
									Height="59px" ForeColor="Black"></asp:textbox>
                         </td>
					</tr>				 
                    <tr>
						<td class="smalltitle" align="center" ><asp:Literal 
                                ID="ltlMemo" Text="備&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;註"
                                runat="server"></asp:Literal>
                        </td>
						<td  width="85%" colspan="3" height="70">&nbsp;
								<asp:textbox id="txtMemo" runat="server" CssClass="TextRead"  ReadOnly="true" Width="96%" MaxLength="1000" TextMode="MultiLine"
									Height="59px" ForeColor="Black"></asp:textbox>
                         </td>
					</tr>
                     
                    <tr>
						<td class="smalltitle" align="center"><asp:Literal ID="ltlCreateUserName" Text="建檔人員"
                                runat="server"></asp:Literal>
                        </td>
						<td >&nbsp;
								<asp:textbox id="txtCreateUserName" runat="server" CssClass="TextRead" Width="90%" 
                                MaxLength="20" ReadOnly="True"></asp:textbox>
                         </td>
						<td class="smalltitle" align="center" width="15%">                            
                            <asp:Literal ID="ltlCreateDate" runat="server" Text="建檔日期"></asp:Literal>							
						    
						</td>
						<td width="35%">&nbsp;
								<asp:textbox id="txtCreateDate" runat="server" CssClass="TextRead" Width="90%" 
                                MaxLength="20" ReadOnly="True"></asp:textbox></td>
					</tr>
                    <tr>
						<td class="smalltitle" align="center"><asp:Literal ID="ltlLastModifyUserName" Text="異動人員"
                                runat="server"></asp:Literal>
                        </td>
						<td >&nbsp;
								<asp:textbox id="txtLastModifyUserName" runat="server" CssClass="TextRead" Width="90%" MaxLength="20" ReadOnly="true" ></asp:textbox>
                        </td>
						<td class="smalltitle" align="center">							
						 
                            <asp:Literal ID="ltlLastModifyDate" Text="異動日期"
                                runat="server"></asp:Literal>
						</td>
						<td >&nbsp;
                        <asp:textbox id="txtLastModifyDate" runat="server" CssClass="TextRead" Width="90%" 
                                MaxLength="20" ReadOnly="true"></asp:textbox></td>
					</tr>
                      <tr>
						<td class="smalltitle" align="center"><asp:Literal ID="ltlEP_CheckUserName" Text="企業覆核人員"
                                runat="server"></asp:Literal>
                        </td>
						<td >&nbsp;
								<asp:textbox id="txtEP_CheckUserName" runat="server" CssClass="TextRead" Width="90%" MaxLength="20" ReadOnly="true"></asp:textbox>
                        </td>
						<td class="smalltitle" align="center">	
                            <asp:Literal ID="ltlEP_CheckDate" Text="企業覆核日期"
                                runat="server"></asp:Literal>
						</td>
						<td >&nbsp;
								<asp:textbox id="txtEP_CheckDate" runat="server" CssClass="TextRead" Width="90%" ReadOnly="true"
                                MaxLength="20"></asp:textbox>                           
                        </td>
					</tr>
                    <tr>
						<td class="smalltitle" align="center"><asp:Literal ID="ltlBU_CheckUserName" Text="營運覆核人員"
                                runat="server"></asp:Literal>
                        </td>
						<td >&nbsp;
								<asp:textbox id="txtBU_CheckUserName" runat="server" CssClass="TextRead" Width="90%" MaxLength="20" ReadOnly="true"></asp:textbox>
                        </td>
						<td class="smalltitle" align="center">	
                            <asp:Literal ID="ltlBU_CheckDate" Text="營運覆核日期"
                                runat="server"></asp:Literal>
						</td>
						<td >&nbsp;
								<asp:textbox id="txtBU_CheckDate" runat="server" CssClass="TextRead" Width="70%" ReadOnly="true"
                                MaxLength="20"></asp:textbox>     
                                 &nbsp;     
                                 <asp:button id="btnModify" runat="server" Text="修改記錄" OnClientClick="return Modify()"
                                            CssClass="but" Width="75px" >
                                    </asp:button>                       
                        </td>
					</tr>
                    <tr>
						<td class="smalltitle" align="center"><asp:Literal ID="ltlStopUserName" Text="停用人員"
                                runat="server"></asp:Literal>
                        </td>
						<td >&nbsp;
								<asp:textbox id="txtStopUserName" runat="server" CssClass="TextRead"  ReadOnly="true" Width="90%" MaxLength="20"></asp:textbox>
                                  
                        </td>
						<td class="smalltitle" align="center">
						 
                            <asp:Literal ID="ltlStopDate" Text="停用日期"
                                runat="server"></asp:Literal>
						</td>
						<td >&nbsp;
								<asp:textbox id="txtStopDate" runat="server" CssClass="TextRead"  ReadOnly="true" Width="70%" 
                                MaxLength="20"></asp:textbox>
                                 &nbsp;
                                 <asp:button id="btnStop" runat="server" Text="停用" 
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
