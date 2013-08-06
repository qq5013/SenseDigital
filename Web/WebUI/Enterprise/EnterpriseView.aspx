<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EnterpriseView.aspx.cs" Inherits="WebUI_BusinessUnit_EnterpriseView" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head id="Head1" runat="server">
		<meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
  
    <title></title> 
        <link href="~/Styles/Main.css" type="text/css" rel="stylesheet" /> 
        <link href="~/Styles/op.css" type="text/css" rel="stylesheet" />
        <link rel="stylesheet" type="text/css" href="~/ext-3.3.1/resources/css/ext-all.css" />
 	    <script type="text/javascript" src='<%=ResolveUrl("~/ext-3.3.1/ext-base.js") %>'></script>
        <script type="text/javascript" src='<%=ResolveUrl("~/ext-3.3.1/ext-all.js") %>'></script>
        <script type="text/javascript" src='<%=ResolveUrl("~/ext-3.3.1/TabCloseMenu.js") %>'></script>

        <script type="text/javascript" src='<%=ResolveUrl("~/Scripts/JQuery/jquery-1.8.3.min.js") %>'></script> 
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Commons.js") %>'></script>
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Resize.js") %>'></script>  
        <script type="text/javascript">
            function initial() {
                var tabPanel = new Ext.TabPanel({
                    height: 400,
                    width: "100%",
                    autoTabs: true, //自动扫描页面中的div然后将其转换为标签页
                    deferredRender: false, //不进行延时渲染
                    activeTab: 0, //默认激活第一个tab页
                    animScroll: true, //使用动画滚动效果
                    enableTabScroll: true, //tab标签超宽时自动出现滚动按钮
                    applyTo: 'tabs'
                })
            }
            function ModifyRecord() {

                var TitleName = escape("<%=Resources.Resource.BU_EnterpriseLabelModifyRecord %>");
                var EnterpriseID = $("#ddlEnterpriseID").val();

                window.showModalDialog('../BusinessUnit/EnterpriseModifyRecord.aspx?EnterpriseID=' + EnterpriseID + '&TitleName=' + TitleName, window, 'dialogHeight:220px;dialogWidth:400px;toolbar=no,status=yes,resizable=no');
                return false;
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
                        &nbsp;</td>
                    <td align="right">
                        <asp:Button ID="btnPrint" runat="server" Text="列印" CssClass="ButtonPrint" 
                            onclick="btnPrint_Click" />
                        <asp:Button ID="btnEdit" runat="server" Text="修改" CssClass="ButtonModify"  OnClientClick="return Edit(true);" />
                        <asp:Button ID="btnExit" runat="server" Text="離開" OnClientClick="return Exit()" CssClass="ButtonExit" />
                    </td>
                </tr>
            </table>
            <div id="surdiv" style="overflow:auto">
            <table class="maintable" cellspacing="0" cellpadding="0" width="100%" align="center" bordercolor="#ffffff" border="1" >			
					<tr>
						<td  valign="middle" align="left" colspan="4" height="22" class="title1">
							<p><asp:Literal ID="ltlTitle" Text="企業用戶资料[ 單筆明细畫面 ]" runat="server"></asp:Literal></p>
						</td>
					</tr>							
			</table>
            <div id='tabs'>
                <div class='x-tab' title='基本資料'> 
            <table class="maintable" cellspacing="0" cellpadding="0" width="100%" align="center" bordercolor="#ffffff" border="1" >				
					<tr>
						<td class="musttitle" align="center" width="15%" ><asp:Literal ID="ltlEnterpriseID" Text="企業編號"
                                runat="server"></asp:Literal>
                        </td>
						<td width="35%" >&nbsp;
								<asp:dropdownlist id="ddlEnterpriseID" runat="server" Width="90%" 
                                AutoPostBack="True" 
                                onselectedindexchanged="ddlEnterpriseID_SelectedIndexChanged" ></asp:dropdownlist>
                        </td>
						<td class="musttitle" align="center" width="15%"><asp:Literal 
                                ID="ltlCategoryID" Text="企業類別"
                                runat="server"></asp:Literal>
                        </td>
						<td  width="35%">&nbsp;
                            <asp:DropDownList ID="ddlCategoryID" runat="server" Width="90%">
                            </asp:DropDownList>
                        </td>
					</tr>
                    		
					<tr>
						<td class="musttitle" align="center" width="15%" ><asp:Literal ID="ltlEnterpriseName" Text="企業名稱"
                                runat="server"></asp:Literal>
                        </td>
						<td width="85%"  colspan="3">&nbsp;
								<asp:textbox id="txtEnterpriseName" runat="server" CssClass="TextRead" ReadOnly="true" Width="96%" MaxLength="10"></asp:textbox>
                        </td>
						
					</tr>
                    <tr>
						<td class="musttitle" align="center" width="15%" ><asp:Literal ID="ltlEnterpriseEName" Text="企業英文名稱"
                                runat="server"></asp:Literal>
                        </td>
						<td width="85%"  colspan="3">&nbsp;
								<asp:textbox id="txtEnterpriseEName" runat="server" CssClass="TextRead" ReadOnly="true" Width="96%" MaxLength="10"></asp:textbox>
                        </td>
						
					</tr>
                     <tr>
						<td class="musttitle" align="center" width="15%" ><asp:Literal ID="ltlEnterpriseSName" Text="企業簡稱"
                                runat="server"></asp:Literal>
                        </td>
						<td width="85%"  colspan="3">&nbsp;
								<asp:textbox id="txtEnterpriseSName" runat="server" CssClass="TextRead" ReadOnly="true" Width="96%" MaxLength="20"></asp:textbox>
                        </td>
						
					</tr>
                    <tr>
						<td class="musttitle" align="center" width="15%" ><asp:Literal ID="ltlUnionID" Text="官方編號"
                                runat="server"></asp:Literal>
                        </td>
						<td width="35%" >&nbsp;
								<asp:textbox id="txtUnionID" runat="server" CssClass="TextRead" ReadOnly="true" Width="90%" MaxLength="20"></asp:textbox>
                        </td>
						<td class="smalltitle" align="center" width="15%">
                            <asp:Literal ID="ltlPhone" runat="server" Text="公司電話"></asp:Literal>
                        </td>
						<td  width="35%">&nbsp;
                            <asp:TextBox ID="txtPhone" runat="server" CssClass="TextRead" MaxLength="20" 
                                ReadOnly="true" Width="90%"></asp:TextBox>
								</td>
					</tr>
                    <tr>
						<td class="smalltitle" align="center" width="15%" ><asp:Literal ID="ltlPresident" Text="公司負責人"
                                runat="server"></asp:Literal>
                        </td>
						<td width="35%" >&nbsp;
								<asp:textbox id="txtPresident" runat="server" CssClass="TextRead" ReadOnly="true" Width="90%" MaxLength="20"></asp:textbox>

                        </td>
						<td class="smalltitle" align="center" width="15%">
                            <asp:Literal ID="ltlFax" runat="server" Text="公司傳真"></asp:Literal>
                        </td>
						<td  width="35%">&nbsp;
								<asp:TextBox ID="txtFax" runat="server" CssClass="TextRead" MaxLength="20" 
                                ReadOnly="true" Width="90%"></asp:TextBox>
								</td>
					</tr>
                    
                    <tr>
						<td class="smalltitle" align="center" ><asp:Literal ID="ltlWebUrl" Text="公司網址"
                                runat="server"></asp:Literal>
                        </td>
						<td width="85%" colspan="3" >&nbsp;
								<asp:textbox id="txtWebUrl" runat="server" CssClass="TextRead" ReadOnly="true" Width="92%" MaxLength="100"></asp:textbox>
                                &nbsp;
                            <asp:HyperLink ID="lnkWebUrl" Target="_blank" runat="server">Go</asp:HyperLink>
                        </td>
					</tr>
                    <tr>
						<td class="smalltitle" align="center" ><asp:Literal ID="ltlAddress" Text="公司地址"
                                runat="server"></asp:Literal>
                        </td>
						<td width="85%" colspan="3" >&nbsp;
								<asp:textbox id="txtAddress" runat="server" CssClass="TextRead" 
                                ReadOnly="true" Width="80%" MaxLength="100" ></asp:textbox>
                            &nbsp;<asp:TextBox ID="txtZipNo" runat="server" CssClass="TextRead" MaxLength="100" 
                                 ReadOnly="true" Width="15%"></asp:TextBox>
                        </td>
					</tr>
                    <tr>
						<td class="smalltitle" align="center" width="15%" >
                            <asp:Literal ID="ltlServiceYears" runat="server" Text="標籤使用年限"></asp:Literal>
                          </td>
						<td width="35%" >&nbsp;
								<asp:TextBox ID="txtServiceYears" runat="server" CssClass="TextRead" 
                                MaxLength="20" Width="90%"></asp:TextBox>
								</td>
						<td class="smalltitle" align="center" width="15%">
                            <asp:Literal ID="ltlEnableMonths" runat="server" Text="標籤啟用期限"></asp:Literal>
                          </td>
						<td  width="35%">&nbsp;
								<asp:DropDownList ID="ddlEnableMonths" runat="server" Width="70%">
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
								&nbsp;<asp:Button ID="btnLabelRecord" runat="server" OnClientClick="return ModifyRecord()" CssClass="but" Text="標籤異動" Width="75px" />
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
						<td class="smalltitle" align="center">                            
                            <asp:Literal ID="ltlCreateDate" runat="server" Text="建檔日期"></asp:Literal>							
						    
						</td>
						<td>&nbsp;
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
						<td class="smalltitle" align="center"><asp:Literal ID="ltlCheckUserName" Text="覆核人員"
                                runat="server"></asp:Literal>
                        </td>
						<td >&nbsp;
								<asp:textbox id="txtCheckUserName" runat="server" CssClass="TextRead" Width="90%" MaxLength="20" ReadOnly="true"></asp:textbox>
                        </td>
						<td class="smalltitle" align="center">	
                            <asp:Literal ID="ltlCheckDate" Text="覆核日期"
                                runat="server"></asp:Literal>
						</td>
						<td >&nbsp;
								<asp:textbox id="txtCheckDate" runat="server" CssClass="TextRead" Width="90%" ReadOnly="true"
                                MaxLength="20"></asp:textbox>&nbsp;
                            </td>
					</tr>
                   
			</table>

                <div class='x-tab' title='連絡人'> 
                                         
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                    SkinID="GridViewSkin" Width="1000px">
            <Columns>                
                <asp:BoundField DataField="RowID" HeaderText="欄號" 
                    SortExpression="RowID">
                    <ItemStyle HorizontalAlign="Center" Width="8%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="LinkMan" HeaderText="連絡人員" 
                    SortExpression="LinkMan">
                    <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="Post" HeaderText="職稱" 
                    SortExpression="Post">
                    <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                  <asp:BoundField DataField="CellPhone" HeaderText="行動電話" 
                    SortExpression="CellPhone" >
                    <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                 <asp:BoundField DataField="Phone" HeaderText="連絡電話" 
                    SortExpression="Phone">
                    <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                 <asp:BoundField DataField="Email" HeaderText="電子郵件" 
                    SortExpression="Email">
                    <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                 <asp:BoundField DataField="Memo" HeaderText="備註" 
                    SortExpression="Memo">
                    <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
        <%--     
                <asp:BoundField DataField="CreateDate" HeaderText="CreateDate" HtmlEncode="false" DataFormatString="{0:yyyy/MM/dd HH:mm}"
                    SortExpression="CreateDate">
                    <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="CreateUserName" HeaderText="CreateUserName" 
                    SortExpression="CreateUserName">
                    <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="LastModifyDate" HeaderText="LastModifyDate" HtmlEncode="false" DataFormatString="{0:yyyy/MM/dd HH:mm}"
                    SortExpression="LastModifyDate">
                    <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="LastModifyUserName" HeaderText="LastModifyUserName" 
                    SortExpression="LastModifyUserName">
                    <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>--%>
               
            </Columns>
            <PagerSettings Visible="False" />
        </asp:GridView>
            
                </div>

                <div class='x-tab' title='備註'>   
                <asp:TextBox ID="txtMemo" runat="server" CssClass="TextRead" TextMode="MultiLine" 
                        Width="98%" Height="300" ReadOnly="True"></asp:TextBox>
                </div>          
            </div>

        </div>
    </div>
    </ContentTemplate>
            </asp:UpdatePanel> 
    </form>
</body>
</html>
