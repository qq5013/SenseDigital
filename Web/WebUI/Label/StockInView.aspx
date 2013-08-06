<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StockInView.aspx.cs" Inherits="WebUI_Label_StockInView" %>
<%@ Register src="../../Controls/Calendar.ascx" tagname="Calendar" tagprefix="uc2" %>

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

        <script type="text/javascript" src='<%=ResolveUrl("~/Scripts/JQuery/jquery-1.8.2.min.js") %>'></script> 
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Commons.js") %>'></script>
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Resize.js") %>'></script>  
        <script type="text/javascript">
            function initial() {
                var tabPanel = new Ext.TabPanel({
                    height: 180,
                    width: "100%",
                    defaults: { autoScroll: true },
                    autoTabs: true, //自动扫描页面中的div然后将其转换为标签页
                    deferredRender: false, //不进行延时渲染
                    activeTab: 0, //默认激活第一个tab页
                    animScroll: true, //使用动画滚动效果
                    enableTabScroll: true, //tab标签超宽时自动出现滚动按钮
                    applyTo: 'tabs'
                })
            }
            function Upload() {
                var page = "StockInUpload.aspx";
                var EnterpriseID = $("#txtEnterpriseID").val();
                var strReturn = window.showModalDialog('TempPage.aspx?page=' + page + '&EnterpriseID=' + EnterpriseID + '&FormID=<% =FormID%>&TitleName=' + escape('入庫標籤圖檔序號上傳'), window, 'DialogHeight:360px;DialogWidth:800px;help:no;scroll:no');
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
                        <asp:Button ID="btnAdd" runat="server" Text="新增" CssClass="ButtonCreate" 
                            OnClientClick="BillEdit(false);return false" onclick="btnAdd_Click" />
                        <asp:Button ID="btnDelete" runat="server" Text="刪除" CssClass="ButtonDel" 
                            onclick="btnDelete_Click" />
                        <asp:Button ID="btnEdit" runat="server" Text="修改" CssClass="ButtonModify" 
                            OnClientClick="return BillEdit(true);" onclick="btnEdit_Click" />
                        <asp:Button ID="btnExit" runat="server" Text="離開" OnClientClick="Exit()" 
                            CssClass="ButtonExit" />
                            <asp:Button ID="btnBack" runat="server" Text="返回" OnClientClick="return Back()" 
                            CssClass="ButtonBack" />
                    </td>
                </tr>
            </table>
             <div id="surdiv" style="overflow:auto">
	<table class="maintable" cellspacing="0" cellpadding="0" width="100%" align="center" borderColor="#ffffff" border="1" >
					<tr>
						<td  valign="middle" align="left" colspan="4" height="22" class="title1">
							<p><asp:Literal ID="ltlTitle" Text="標籤入庫單[ 單筆明細畫面 ]" runat="server"></asp:Literal></p>
						</td>
					</tr>
						<tr>
						<td class="musttitle" align="center" width="15%" ><asp:Literal ID="ltlStyleID" Text="入庫日期"
                                runat="server"></asp:Literal></td>
						<td width="35%" >&nbsp;
								<uc2:Calendar ID="txtBillDate" runat="server"  ReadOnly="true"/>
                        </td>
						<td class="musttitle" align="center" width="15%"><asp:Literal 
                                ID="ltlEnterpriseID" Text="入庫單號"
                                runat="server"></asp:Literal></td>
						<td  width="35%">&nbsp;
								<asp:textbox id="txtBillID" runat="server" CssClass="Textread" 
                                ReadOnly="true" Width="90%" 
                                MaxLength="20" ></asp:textbox>
                                &nbsp;
                                </td>
					</tr>
					<tr>
						<td class="musttitle" align="center" ><asp:Literal ID="ltlStyleName" Text="企業編號"
                                runat="server"></asp:Literal></td>
						<td  colspan="3">&nbsp;
								
                        		<asp:textbox id="txtEnterpriseID" runat="server" CssClass="Textread" ReadOnly="true" Width="30%" 
                                MaxLength="20" ></asp:textbox>&nbsp;
								
                       <asp:textbox id="txtEnterpriseName" runat="server" CssClass="TextRead" Width="65%" MaxLength="20" ReadOnly="true"></asp:textbox></td>
					</tr>
                    <tr>
                    <td  class="musttitle" align="center"><asp:Literal ID="Literal3" runat="server" Text="訂單單號"></asp:Literal></td>
                    <td >&nbsp;
								<asp:textbox id="txtOrderBillID" runat="server" CssClass="TextRead" Width="90%" 
                                MaxLength="20" ReadOnly="True"></asp:textbox></td>
                    <td  class="musttitle" align="center"><asp:Literal ID="Literal4" runat="server" Text="生產單號"></asp:Literal></td>
                    <td>&nbsp;
								<asp:textbox id="txtProduceBillID" runat="server" CssClass="TextRead" Width="90%" 
                                MaxLength="20" ReadOnly="True"></asp:textbox></td>
                    </tr>
                     <tr>
                    <td  class="musttitle" align="center"><asp:Literal ID="Literal5" runat="server" Text="生產款式"></asp:Literal></td>
                    <td>&nbsp;
                        <asp:TextBox ID="txtStyleID" runat="server" CssClass="Textread" ReadOnly="true" 
                            MaxLength="20" Width="30%"></asp:TextBox>&nbsp;
                        <asp:textbox id="txtLabelMode" runat="server" CssClass="TextRead" Width="57%" 
                            MaxLength="20" ReadOnly="true"></asp:textbox>
                        </td>
                   <td class="musttitle" align="center"><asp:Literal ID="Literal6" runat="server" Text="生產方式"></asp:Literal></td>
                    <td>
                        <asp:RadioButton ID="RadioButton3" runat="server" Text ="營運生產" Checked="True" 
                            GroupName="group1" />
                        <asp:RadioButton ID="RadioButton4" runat="server" Text ="企業自製" 
                            GroupName="group1"/>
                        </td>
                    </tr>
                   <tr>
                       <td  class="smalltitle" align="center"><asp:Literal ID="Literal11" runat="server" Text="訂購數量"></asp:Literal>
                       </td>
                      <td colspan="3" height="22px">
                        <table width="100%">
                          <tr>                             
                             <td width="20%">
                                 &nbsp;<asp:TextBox ID="TextBox1" runat="server" CssClass="TextRead" ReadOnly="true"
                            MaxLength="20" Width="90%" ></asp:TextBox>
                            </td>
                             <td class="smalltitle" align="center" width="15%"><asp:Literal ID="Literal12" runat="server" Text="備品數量"></asp:Literal>                             
                            </td>
                             <td width="20%">
                              <asp:TextBox ID="TextBox2" runat="server" CssClass="TextRead" ReadOnly="true"
                            MaxLength="20" Width="90%"></asp:TextBox>
                            </td>
                             <td class="smalltitle" align="center" width="15%"><asp:Literal ID="Literal13" runat="server" Text="預計生產量"></asp:Literal>    
                            
                            </td>
                             <td width="20%">
                              <asp:TextBox ID="TextBox3" runat="server" CssClass="TextRead" ReadOnly="true"
                            MaxLength="20" Width="85%"></asp:TextBox>
                            </td>
                          </tr>
                        </table>
                      </td>
                    </tr>
         <tr>
                       <td  class="smalltitle" align="center"><asp:Literal ID="Literal9" runat="server" Text="保留起始號"></asp:Literal>
                       </td>
                      <td colspan="3" height="22px">
                        <table width="100%">
                          <tr>                             
                             <td width="20%">
                                 &nbsp;<asp:textbox id="txtRemainStartNo" runat="server" CssClass="TextRead" Width="90%" 
                                MaxLength="20" ReadOnly="True"></asp:textbox>
                            </td>
                             <td class="smalltitle" align="center" width="15%"><asp:Literal ID="Literal21" runat="server" 
                            Text="保留終止號"></asp:Literal>                             
                            </td>
                             <td width="20%">
								<asp:textbox id="txtRemainEndNo" runat="server" CssClass="TextRead" Width="90%" 
                                MaxLength="20" ReadOnly="True"></asp:textbox>
								
                            </td>
                             <td class="smalltitle" align="center" width="15%">
                        <asp:Literal ID="Literal22" runat="server" 
                            Text="作廢單號"></asp:Literal>    
                            
                            </td>
                             <td width="20%">
								<asp:textbox id="txtInvalidBillID" runat="server" CssClass="TextRead" Width="85%" 
                                MaxLength="20" ReadOnly="True"></asp:textbox>
								
                            </td>
                          </tr>
                        </table>
                      </td>
                    </tr>
                    
                 <tr>
                    <td  class="smalltitle" align="center" colspan ="4">
                                    <asp:CheckBox ID="CheckBox2" runat="server" ForeColor="Blue" Text="圖片存證" />
                                    <asp:CheckBox ID="ckbIsNoDone" runat="server" ForeColor="Blue" 
                            Text="序號產生" />
                                    <asp:CheckBox ID="ckbIsCheckImage" runat="server" ForeColor="Blue" 
                            Text="圖檔檢查" />
                                    <asp:CheckBox ID="ckbIsImportImage" runat="server" ForeColor="Blue" 
                            Text="圖檔匯入" />
                                    <asp:CheckBox ID="ckbIsInvalidBad" runat="server" ForeColor="Blue" 
                            Text="壞品作廢" />
                                    
                                        &nbsp;<asp:button id="btnCheck2" runat="server" Text=" 款式圖片" 
                                            CssClass="but" Width="75px"  
                                OnClientClick ="window.showModalDialog('ImageQuery.aspx',null,'dialogHeight:300px;dialogWidth:600px;help:no;scroll:no');return false;" >
                                    </asp:button>
								&nbsp;<asp:button id="btnUpload" runat="server" Text=" 上傳圖檔" 
                                            CssClass="but" Width="75px"  
                                OnClientClick ="return Upload()" >
                                    </asp:button>
                        &nbsp;<asp:button id="btnCheck1" runat="server" Text=" 壞品清單" 
                                            CssClass="but" Width="75px"  
                                OnClientClick ="window.showModalDialog('badPro.aspx',null,'dialogHeight:280px;dialogWidth:800px;help:no;scroll:no');return false;" >
                                    </asp:button>
                                        &nbsp;<asp:button id="btnCheck3" runat="server" Text=" 標籤序號產出" 
                                            CssClass="but" Width="80px"  
                                
                                        OnClientClick ="window.showModalDialog('StyleCheck.aspx',null,'dialogHeight:50px;help:no;scroll:no');return false;" >
                                    </asp:button>
                                        &nbsp;<asp:button id="btnCheck4" runat="server" Text=" 圖檔檢查" 
                                            CssClass="but" Width="75px"  
                                
                                        OnClientClick ="window.showModalDialog('StyleCheck.aspx',null,'dialogHeight:50px;help:no;scroll:no');return false;" >
                                    </asp:button>
                                        &nbsp;<asp:button id="btnCheck5" runat="server" Text=" 標籤圖片匯入" 
                                            CssClass="but" Width="80px"  
                                
                                        OnClientClick ="window.showModalDialog('StyleCheck.aspx',null,'dialogHeight:50px;help:no;scroll:no');return false;" >
                                    </asp:button>
                                        </td>   
                    </tr>

                   
                    </table>
                    <div id='tabs'>
                        <div class='x-tab' title='入庫明細'> 
                                 <div style="overflow: auto; WIDTH: 100%;height:150px">
                <asp:HiddenField ID="HiddenField1" runat="server" />
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                    SkinID="GridViewSkin" Width="1200px" AllowSorting="True"  
                   >
            <Columns>
 
<%--<asp:BoundField DataField="RowID" HeaderText="序號" SortExpression="RowID" > 
<ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" /> <HeaderStyle Wrap="False" /> 
</asp:BoundField>  --%>
<asp:BoundField DataField="SubID" HeaderText="(序號)" SortExpression="SubID" > 
<ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" /> <HeaderStyle Wrap="False" /> 
</asp:BoundField>  
<asp:BoundField DataField="LabelReelNo" HeaderText="標籤卷編號" SortExpression="LabelReelNo" > 
<ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" /> <HeaderStyle Wrap="False" /> 
</asp:BoundField>  
<asp:BoundField DataField="StartNo" HeaderText="(起始序號)" SortExpression="StartNo" > 
<ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" /> <HeaderStyle Wrap="False" /> 
</asp:BoundField>  
<asp:BoundField DataField="EndNo" HeaderText="終止序號" SortExpression="EndNo" > 
<ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" /> <HeaderStyle Wrap="False" /> 
</asp:BoundField>  
<asp:BoundField DataField="Quantity" HeaderText="數量" SortExpression="Quantity" > 
<ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" /> <HeaderStyle Wrap="False" /> 
</asp:BoundField>  
<asp:BoundField DataField="ProductID" HeaderText="產品編號" SortExpression="ProductID" > 
<ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" /> <HeaderStyle Wrap="False" /> 
</asp:BoundField>  
<asp:BoundField DataField="ProductName" HeaderText="品名規格" SortExpression="ProductName" > 
<ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" /> <HeaderStyle Wrap="False" /> 
</asp:BoundField>  
<asp:BoundField DataField="ResumeID" HeaderText="生產履歷編號" SortExpression="ResumeID" > 
<ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" /> <HeaderStyle Wrap="False" /> 
</asp:BoundField>  
<asp:BoundField DataField="LogisticsID" HeaderText="物流資訊編號" SortExpression="LogisticsID" > 
<ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" /> <HeaderStyle Wrap="False" /> 
</asp:BoundField>  
<asp:CheckBoxField DataField="IsCheckImage" HeaderText="(圖檔檢查狀態)" SortExpression="IsCheckImage"> 
<ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" /> <HeaderStyle Wrap="False" /> 
</asp:CheckBoxField> 
<asp:BoundField DataField="EnableDate" HeaderText="(啟用期限)" HtmlEncode="false" DataFormatString="{0:yyyy/MM/dd HH:mm}" SortExpression="EnableDate" > 
<ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" /> <HeaderStyle Wrap="False" /> 
</asp:BoundField> 
<asp:BoundField DataField="BadQty" HeaderText="(壞品張數)" SortExpression="BadQty" > 
<ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" /> <HeaderStyle Wrap="False" /> 
</asp:BoundField>  
<asp:BoundField DataField="CreateDate" HeaderText="建檔日期" HtmlEncode="false" DataFormatString="{0:yyyy/MM/dd HH:mm}" SortExpression="CreateDate" > 
<ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" /> <HeaderStyle Wrap="False" /> 
</asp:BoundField> 
<asp:BoundField DataField="CreateUserName" HeaderText="建檔人員" SortExpression="CreateUserName" > 
<ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" /> <HeaderStyle Wrap="False" /> 
</asp:BoundField>  
<asp:BoundField DataField="LastModifyDate" HeaderText="異動日期" HtmlEncode="false" DataFormatString="{0:yyyy/MM/dd HH:mm}" SortExpression="LastModifyDate" > 
<ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" /> <HeaderStyle Wrap="False" /> 
</asp:BoundField> 
<asp:BoundField DataField="LastModifyUserName" HeaderText="異動人員" SortExpression="LastModifyUserName" > 
<ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" /> <HeaderStyle Wrap="False" /> 
</asp:BoundField>  
  


            </Columns>
            <PagerSettings Visible="False" />
        </asp:GridView>
        </div>	
            
                        </div>

                        <div class='x-tab' title='備註'>                              
                                
                             <table class="maintable" cellspacing="0" cellpadding="0" width="100%" align="center" borderColor="#ffffff" border="1" >
                                <tr>
                                <td class="smalltitle" align="center" width="15%"><asp:Literal ID="Literal20" Text="備註"
                                            runat="server"></asp:Literal></td>
                                <td >&nbsp;
                                <asp:TextBox ID="txtMemo" runat="server" CssClass="TextRead"  ReadOnly ="true" TextMode="MultiLine" 
                                        Width="98%" Height="150px" ></asp:TextBox></td>
                                </tr>
                                   
                                
                                </table> 
                        </div>          
                    </div>
				
                <table width="100%">
              
                <tr>
                       <td  class="smalltitle" align="center"><asp:Literal ID="Literal16" runat="server" Text="入庫總數量合計"></asp:Literal>
                       </td>
                      <td colspan="3" height="22px">
                        <table width="100%">
                          <tr>                             
                             <td width="20%">
                              <asp:TextBox ID="TextBox61" runat="server" CssClass="TextRead" ReadOnly="true"
                            MaxLength="20" Width="90%"></asp:TextBox>
                            </td>
                             <td class="smalltitle" align="center" width="15%"><asp:Literal ID="Literal17" runat="server" Text="尾數備品調減"></asp:Literal>                             
                            </td>
                             <td width="20%">
                              <asp:TextBox ID="TextBox71" runat="server"  CssClass="TextRead" ReadOnly="true"
                            MaxLength="20" Width="90%"></asp:TextBox>
                            </td>
                             <td class="smalltitle" align="center" width="15%"><asp:Literal ID="Literal78" runat="server" Text="差異數量"></asp:Literal>    
                            
                            </td>
                             <td width="20%">
                              <asp:TextBox ID="TextBox181" runat="server" CssClass="TextRead" ReadOnly="true"
                            MaxLength="20" Width="85%"></asp:TextBox>
                            </td>
                          </tr>
                        </table>
                      </td>
                    </tr>
                 <tr>
                    <td  class="smalltitle" align="center"><asp:Literal ID="Literal233" runat="server" Text="壞品數量合計"></asp:Literal></td>
                    <td >&nbsp;<asp:textbox id="txtQtyTotal" runat="server" CssClass="TextRead" Width="90%" 
                                MaxLength="20" ReadOnly="True">0</asp:textbox></td>
                    <td  class="smalltitle" align="center"><asp:Literal ID="Literal33" runat="server" Text="匯入圖檔數量"></asp:Literal></td>
                    <td>&nbsp;<asp:textbox id="txtLabelReels" runat="server" CssClass="TextRead" Width="90%" 
                                MaxLength="20" ReadOnly="True">0</asp:textbox></td>
                    </tr>
                   
                    

                                   
                                    <tr>
						                <td class="smalltitle" align="center" width="15%" ><asp:Literal ID="ltlCreateUserName" Text="建檔人員"
                                                runat="server"></asp:Literal>
                                        </td>
						                <td width="35%">&nbsp;<asp:textbox id="txtCreateUserName" runat="server" CssClass="TextRead" Width="90%" 
                                                MaxLength="20" ReadOnly="True"></asp:textbox>
                                         </td>
						                <td class="smalltitle" align="center" width="15%" >                            
                                            <asp:Literal ID="ltlCreateDate" runat="server" Text="建檔日期"></asp:Literal>							
						    
						                </td>
						                <td>&nbsp;<asp:textbox id="txtCreateDate" runat="server" CssClass="TextRead" Width="90%" 
                                                MaxLength="20" ReadOnly="True"></asp:textbox></td>
					                </tr>
                                    <tr>
						                <td class="smalltitle" align="center"><asp:Literal ID="ltlLastModifyUserName" Text="異動人員"
                                                runat="server"></asp:Literal>
                                        </td>
						                <td >&nbsp;<asp:textbox id="txtLastModifyUserName" runat="server" CssClass="TextRead" Width="90%" MaxLength="20" ReadOnly="true" ></asp:textbox>
                                        </td>
						                <td class="smalltitle" align="center">							
						 
                                            <asp:Literal ID="ltlLastModifyDate" Text="異動日期"
                                                runat="server"></asp:Literal>
						                </td>
						                <td >&nbsp;<asp:textbox id="txtLastModifyDate" runat="server" CssClass="TextRead" Width="90%" 
                                                MaxLength="20" ReadOnly="true"></asp:textbox></td>
					                </tr>
                                    <tr>
                    <td  class="smalltitle" align="center"><asp:Literal ID="Literal27" runat="server" Text="企業覆核"></asp:Literal></td>
                    <td >&nbsp;<asp:textbox id="txtEP_CheckUserName" runat="server" CssClass="TextRead" Width="90%" 
                                MaxLength="20" ReadOnly="True"></asp:textbox></td>
                    <td  class="smalltitle" align="center"><asp:Literal ID="Literal26" runat="server" Text="企業覆核日期"></asp:Literal></td>
                    <td>&nbsp;<asp:textbox id="txtEP_CheckDate" runat="server" CssClass="TextRead" Width="90%" 
                                MaxLength="20" ReadOnly="True"></asp:textbox> &nbsp;</td>
                    </tr>
                     <tr>
						<td class="smalltitle" align="center"><asp:Literal ID="Literal7" Text="營運覆核"
                                runat="server"></asp:Literal>
                        </td>
						<td >&nbsp;<asp:textbox id="txtBU_CheckUserName" runat="server" CssClass="TextRead" Width="90%" MaxLength="20" ReadOnly="true" ></asp:textbox>
                        </td>
						<td class="smalltitle" align="center">							
						 
                            <asp:Literal ID="Literal10" Text="營運覆核日期"
                                runat="server"></asp:Literal>
						</td>
						<td >&nbsp;<asp:textbox id="txtBU_CheckDate" runat="server" CssClass="TextRead" Width="60%" 
                                MaxLength="20" ReadOnly="true"></asp:textbox> 
                            
                            &nbsp;<asp:Button ID="btnCheck" runat="server" CssClass="but" 
                                onclick="btnCheck_Click" Text="營運覆核" Width="85px" />
                            
                        </td>
					</tr>
                                
                </table>
            </table>
            </div>
                </div>
    </ContentTemplate>
            </asp:UpdatePanel>
			
		</form>
	</body>
</html>
