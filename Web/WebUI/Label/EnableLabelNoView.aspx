<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EnableLabelNoView.aspx.cs" Inherits="WebUI_Label_EnableLabelNoView" %>
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
//                var tabPanel = new Ext.TabPanel({
//                    height: 280,
//                    width: "100%",
//                    autoTabs: true, //自动扫描页面中的div然后将其转换为标签页
//                    deferredRender: false, //不进行延时渲染
//                    activeTab: 0, //默认激活第一个tab页
//                    animScroll: true, //使用动画滚动效果
//                    enableTabScroll: true, //tab标签超宽时自动出现滚动按钮
//                    applyTo: 'tabs'
//                })
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
                            OnClientClick="Edit(false);return false" onclick="btnAdd_Click" />
                        <asp:Button ID="btnDelete" runat="server" Text="刪除" CssClass="ButtonDel" 
                            onclick="btnDelete_Click" />
                        <asp:Button ID="btnEdit" runat="server" Text="修改" CssClass="ButtonModify" 
                            OnClientClick="return Edit(true);" onclick="btnEdit_Click" />
                        <asp:Button ID="btnExit" runat="server" Text="離開" OnClientClick="Exit()" 
                            CssClass="ButtonExit" />
                    </td>
                </tr>
            </table>
	<table id="Table1" class="maintable" cellspacing="0" cellpadding="0" width="100%" align="center" bordercolor="#ffffff"
				border="1" runat="server">			
					<tr>
						<td  valign="middle" align="left" colspan="5" height="22" class="title1">
							<p><asp:Literal ID="ltlTitle" Text="登錄啟用標籤序號[ 單筆明細畫面 ]" runat="server"></asp:Literal></p>
						</td>
					</tr>							
						
					<tr>
						<td class="musttitle" align="center" width="10%" ><asp:Literal ID="ltlEnterpriseID" Text="啟用日期"
                                runat="server"></asp:Literal>
                        </td>
						<td width="30%" >&nbsp;
							<uc2:Calendar runat="server"  ID="txtEnableDate" ReadOnly="true" />
                     &nbsp;&nbsp;
								  </td>
                        <td class="musttitle" align="center" width="10%" ><asp:Literal ID="Literal3" Text="啟用單號"
                                runat="server"></asp:Literal>
                        </td>
						<td width="30%" >&nbsp;
								<asp:textbox id="txtEnableLabelNoID" runat="server" CssClass="Textread" ReadOnly="true"  
                                Width="90%" MaxLength="6"></asp:textbox>
                 
								  
                        </td>
                       <td width="20%"><asp:button id="btnOnShelf" runat="server" Text="執行上架"  CssClass="but" 
                               Width="80px"></asp:button>&nbsp;
                       </td>
					</tr>
                    		
					<tr>
						<td class="musttitle" align="center" width="10%" ><asp:Literal ID="ltlEnterpriseName" Text="企業編號"
                                runat="server"></asp:Literal>
                        </td>
						<td colspan="3">&nbsp;
								<asp:textbox id="txtEnterpriseID" runat="server" CssClass="Textread" ReadOnly="true" Width="30%" MaxLength="10"></asp:textbox>
                                <asp:textbox id="txtEnterpriseName" runat="server" CssClass="TextRead"  ReadOnly="true" Width="65%" MaxLength="10"></asp:textbox>
                        </td>
                       		<td>
                       <asp:button id="Button7" runat="server" Text="上架檢查"  CssClass="but" Width="80px"></asp:button></td>				
					</tr>                 
					
			</table>    
       
               <div style="overflow: auto; WIDTH: 100%;Height:200px">
                <asp:HiddenField ID="HiddenField1" runat="server" />
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                    SkinID="GridViewSkin" Width="1200px" AllowSorting="True"  
                   >
            <Columns>

<asp:BoundField DataField="RowID" HeaderText="序號" SortExpression="RowID" > 
<ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" /> <HeaderStyle Wrap="False" /> 
</asp:BoundField>  
<asp:BoundField DataField="LabelReelNo" HeaderText="標籤卷編號" SortExpression="LabelReelNo" > 
<ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" /> <HeaderStyle Wrap="False" /> 
</asp:BoundField>  
<asp:BoundField DataField="StartNo" HeaderText="標籤起始序號" SortExpression="StartNo" > 
<ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" /> <HeaderStyle Wrap="False" /> 
</asp:BoundField>  
<asp:BoundField DataField="EndNo" HeaderText="標籤結束序號" SortExpression="EndNo" > 
<ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" /> <HeaderStyle Wrap="False" /> 
</asp:BoundField>  
<asp:BoundField DataField="Quantity" HeaderText="數量" SortExpression="Quantity" > 
<ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" /> <HeaderStyle Wrap="False" /> 
</asp:BoundField>  
<asp:BoundField DataField="BadQty" HeaderText="壞品張數" SortExpression="BadQty" > 
<ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" /> <HeaderStyle Wrap="False" /> 
</asp:BoundField>  

<asp:BoundField DataField="RealQty" HeaderText="實際張數" SortExpression="RealQty" > 
<ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" /> <HeaderStyle Wrap="False" /> 
</asp:BoundField> 
<asp:BoundField DataField="OrderBillID" HeaderText="訂單單號" SortExpression="OrderBillID" > 
<ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" /> <HeaderStyle Wrap="False" /> 
</asp:BoundField>  
 <asp:CheckBoxField DataField="IsOverOrder" HeaderText="上架超訂" SortExpression="IsOverOrder"> 
<ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" /> <HeaderStyle Wrap="False" /> 
</asp:CheckBoxField> 
<asp:BoundField DataField="State" HeaderText="檢查狀態" SortExpression="State" > 
<ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" /> <HeaderStyle Wrap="False" /> 
</asp:BoundField>  

<asp:BoundField DataField="StyleID" HeaderText="標籤款式編號" SortExpression="StyleID" > 
<ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" /> <HeaderStyle Wrap="False" /> 
</asp:BoundField>  
<asp:BoundField DataField="CreateDate" HeaderText="預計除役日期" HtmlEncode="false" DataFormatString="{0:yyyy/MM/dd HH:mm}" SortExpression="CreateDate" > 
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
         <table class="maintable" cellspacing="0" cellpadding="0" width="100%" align="center" bordercolor="#ffffff"
				border="1" >			
               
					  <tr>
						<td class="smalltitle" width="10%" align="center"><asp:Literal ID="Literal10" Text="狀態"
                                runat="server"></asp:Literal>
                        </td>
						<td width="40%">&nbsp;
								<asp:DropDownList ID="ddlBillState" runat="server" Width="90%" Enabled = "false">
                                    <asp:ListItem Value="0">未上架</asp:ListItem>
                                    <asp:ListItem Value="1">異常待修正</asp:ListItem>
                                    <asp:ListItem Value="2">檢查OK</asp:ListItem>
                                    <asp:ListItem Value="3">上架中</asp:ListItem>
                                    <asp:ListItem Value="4">已上架</asp:ListItem>
                            </asp:DropDownList>
                        </td>
						<td class="smalltitle" align="center" width="10%">	
                            <asp:Literal ID="Literal11" Text="數量合計"
                                runat="server"></asp:Literal>
						</td>
						<td >&nbsp;
								<asp:textbox id="txtQtyTotal" runat="server" CssClass="TextRead" Width="90%" ReadOnly="true"
                                MaxLength="20">0</asp:textbox>&nbsp;
                                </td>
					</tr>
                                     <tr>
						<td class="smalltitle" align="center" ><asp:Literal ID="Literal12" Text="建檔人員"
                                runat="server"></asp:Literal>
                        </td>
						<td >&nbsp;
								<asp:textbox id="txtCreateUserName" runat="server" CssClass="TextRead" Width="90%" 
                                MaxLength="20" ReadOnly="True"></asp:textbox>
                         </td>
						<td class="smalltitle" align="center">                            
                            <asp:Literal ID="Literal13" runat="server" Text="建檔日期"></asp:Literal>							
						    
						</td>
						<td >&nbsp;
								<asp:textbox id="txtCreateDate" runat="server" CssClass="TextRead" Width="90%" 
                                MaxLength="20" ReadOnly="True"></asp:textbox></td>
					</tr>
                    <tr>
						<td class="smalltitle" align="center"><asp:Literal ID="Literal14" Text="異動人員"
                                runat="server"></asp:Literal>
                        </td>
						<td >&nbsp;
								<asp:textbox id="txtLastModifyUserName" runat="server" CssClass="TextRead" Width="90%" MaxLength="20" ReadOnly="true" ></asp:textbox>
                        </td>
						<td class="smalltitle" align="center">							
						 
                            <asp:Literal ID="Literal15" Text="異動日期"
                                runat="server"></asp:Literal>
						</td>
						<td >&nbsp;
                        <asp:textbox id="txtLastModifyDate" runat="server" CssClass="TextRead" Width="90%" 
                                MaxLength="20" ReadOnly="true"></asp:textbox></td>
					</tr>
                       <tr>
						<td class="smalltitle" align="center"><asp:Literal ID="Literal16" Text="企業覆核"
                                runat="server"></asp:Literal>
                        </td>
						<td >&nbsp;
								<asp:textbox id="txtEP_CheckUserName" runat="server" CssClass="TextRead" Width="90%" MaxLength="20" ReadOnly="true"></asp:textbox>
                        </td>
						<td class="smalltitle" align="center">	
                            <asp:Literal ID="Literal17" Text="企業覆核日期"
                                runat="server"></asp:Literal>
						</td>
						<td >&nbsp;
								<asp:textbox id="txtEP_CheckDate" runat="server" CssClass="TextRead" Width="70%" ReadOnly="true"
                                MaxLength="20"></asp:textbox>&nbsp;<asp:Button ID="btnEPCheck" runat="server" 
                                CssClass="but" Enabled="false" Text="企業覆核" Width="80px" />
&nbsp;</td>
					</tr>
                    <tr>
						<td class="smalltitle" align="center"><asp:Literal ID="Literal18" Text="營運覆核"
                                runat="server"></asp:Literal>
                        </td>
						<td >&nbsp;
								<asp:textbox id="txtBU_CheckUserName" runat="server" CssClass="TextRead" Width="90%" MaxLength="20" ReadOnly="true"></asp:textbox>
                        </td>
						<td class="smalltitle" align="center">	
                            <asp:Literal ID="Literal19" Text="營運覆核日期"
                                runat="server"></asp:Literal>
						</td>
						<td >&nbsp;
								<asp:textbox id="txtBU_CheckDate" runat="server" CssClass="TextRead" Width="70%" ReadOnly="true"
                                MaxLength="20"></asp:textbox>&nbsp;<asp:Button ID="btnBUCheck" runat="server" 
                                CssClass="but" Text="營運覆核" Width="80px" />
                        </td>
					</tr>
			</table>              
           
					 
				</div>
    </ContentTemplate>
            </asp:UpdatePanel>	 
		</form>
	</body>
</html>
