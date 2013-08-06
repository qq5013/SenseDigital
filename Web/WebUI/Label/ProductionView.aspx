<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProductionView.aspx.cs" Inherits="WebUI_Label_ProductionView" %>
<%@ Register Assembly="Js.PageControl" Namespace="Js.PageControl" TagPrefix="cc1" %>
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
                    height: 200,
                    width: "100%",
                    defaults: { autoScroll: true },
                    autoTabs: true, //自动扫描页面中的div然后将其转换为标签页
                    deferredRender: false, //不进行延时渲染
                    activeTab: 0, //默认激活第一个tab页
                    animScroll: true, //使用动画滚动效果
                    enableTabScroll: true, //tab标签超宽时自动出现滚动按钮
                    applyTo: 'tabs'
                });
                $("#btnStyleQuery").bind("click", function () {

                    parent.addTab('../WebUI/Label/Styles.aspx?FormID=LB_Style', '標籤款式資料設定', 'tab_2_4');
                    return false;
                });
                $("#btnImageQuery").bind("click", function () {
                    window.showModalDialog('ImageQuery.aspx?BillID=' + $("#txtBillID").val() + '&Flag=3&ID=' + $("#txtStyleID").val(), null, 'DialogHeight:280px;DialogWidth:600px;help:no;scroll:no');
                    return false;
                });
                var obj = $("input[type=radio][id*=GridView1_cbsubSelect]");
                if (obj && obj.length > 0) {
                    obj.each(function () { $(this).attr("name", "cbsubSelect"); });
                    obj[0].checked = true;
                } 
            }
            $(document).ready(function () {
                 
            }            );
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
                        <asp:Button ID="btnExit" runat="server" Text="離開" OnClientClick="return Exit()" 
                            CssClass="ButtonExit" />
                            <asp:Button ID="btnBack" runat="server" Text="返回" OnClientClick="return Back()" 
                            CssClass="ButtonBack" />
                    </td>
                </tr>
            </table>
			<table class="maintable" cellspacing="0" cellpadding="0" width="100%" align="center" borderColor="#ffffff" border="1" >
					<tr>
						<td  valign="middle" align="left" colspan="6" height="22" class="title1">
							<p><asp:Literal ID="ltlTitle" Text="標籤生產單[ 單筆編輯畫面 ]" runat="server"></asp:Literal></p>
						</td>
					</tr>
				    <tr>
						<td class="musttitle" align="center" width="10%" ><asp:Literal ID="ltlBillDate" Text="生產日期"
                                runat="server"></asp:Literal>
                        </td>
						<td width="20%" align ="left" >&nbsp;
                        <asp:textbox id="txtBillDate" runat="server" CssClass="TextRead" Width="60%" 
                                MaxLength="20" ></asp:textbox>
                        </td>
						<td class="musttitle" align="center" width="10%"><asp:Literal 
                                ID="ltlBillID" Text="生產單號"
                                runat="server"></asp:Literal></td>
						<td  width="20%">&nbsp;
								<asp:textbox id="txtBillID" runat="server" CssClass="TextRead" Width="60%" 
                                MaxLength="20" ></asp:textbox>
                                
                                </td>
                        <td class="smalltitle" >
                                <asp:Literal ID="Literal10" Text="來源單號" runat="server"></asp:Literal>
                                </td>
                                 <td >
								<asp:textbox id="txtSourceBillID" runat="server" CssClass="TextRead" Width="77%" 
                                MaxLength="20" ></asp:textbox>
                                
                                </td>
                        <td></td>
					</tr>
                    <tr>
						<td class="musttitle" align="center" width="10%" ><asp:Literal ID="LitStyleID" Text="款式編號" runat="server"></asp:Literal>
                        </td>
						<td width="20%" align ="left" colspan="5" >&nbsp;
                        <asp:TextBox ID="txtStyleID" runat="server" CssClass="TextRead" Width="153px"></asp:TextBox>
                        <asp:TextBox ID="txtStyleName" runat="server" CssClass="TextRead" Width="75%"
                                MaxLength="20" ></asp:TextBox>
                        </td>
						
                                     <td>
								         &nbsp;</td>
					</tr>
                
                    <tr>
						<td class="musttitle" align="center" width="10%" >
                            <asp:Literal ID="Literal13" Text="生產張數"
                                runat="server"></asp:Literal>
                        </td>
						<td width="20%" align ="left" >&nbsp;
                            <cc1:DataText runat="server" DataValue="0" CssClass="TextRead" Width="86%" 
                                                            ID="txtProducePages">0</cc1:DataText>
                            </td>
						<td class="musttitle" align="center" width="10%">
                            <asp:Literal 
                                ID="Literal14" Text="每卷張數"
                                runat="server"></asp:Literal></td>
						<td  width="20%">&nbsp;
                               <cc1:DataText runat="server" DataValue="0" CssClass="TextRead" Width="86%" 
                                                            ID="txtStdPages">0</cc1:DataText>
                            </td>
                        <td  width="20%" class="smalltitle" >
                              <asp:Literal 
                                ID="Literal12" Text="生產張數"
                                runat="server"></asp:Literal>
                        </td>
                         <td  width="20%">
                             <cc1:DataText runat="server" DataValue="0" CssClass="TextRead" Width="77%" 
                                                            ID="txtVolumes">0</cc1:DataText>
                        </td>
                         <td><asp:button id="btnStyleQuery" runat="server" Text=" 款式查詢" 
                                            CssClass="but" Width="75px" > </asp:button>
                        </td>
					</tr>
					<tr>
						<td class="musttitle" align="center" width="10%" >
                            <asp:Literal ID="litStartLabelNo" Text="標籤起始序號"
                                runat="server" ViewStateMode="Enabled"></asp:Literal>
                        </td>
						<td width="20%" align ="left" >&nbsp;
                        <asp:TextBox ID="txtStartLabelNo" runat="server" CssClass="TextRead" width="60%"></asp:TextBox>
                        </td>
						<td class="musttitle" align="center" width="10%">
                            <asp:Literal ID="Literal17" Text="標籤結束序號"
                                runat="server"></asp:Literal>
                        </td>
						<td  width="20%">&nbsp;
								<asp:textbox id="txtEndLabelNo" runat="server" CssClass="TextRead" Width="60%" 
                                MaxLength="20" ></asp:textbox>
                                
                                </td>
                         <td  width="20%" class="smalltitle" >
                              <asp:Literal 
                                ID="Literal22" Text="單況"
                                runat="server"></asp:Literal>
                        </td>
                         <td  width="20%">
                             <asp:DropDownList ID="ddlBillState" runat="server" Enabled="false" Width="35%">
                                    <asp:ListItem Value="0">無效</asp:ListItem>                                    
                                    <asp:ListItem Value="1">有效</asp:ListItem>
                                    <asp:ListItem Value="2">已入庫</asp:ListItem>
                                </asp:DropDownList> 
                             <asp:button id="btnBillState" runat="server" 
                                 Text="單況切換"  CssClass="but" Width="75px" onclick="btnBillState_Click"></asp:button>
                                
                        </td>
                         <td><asp:button id="btnImageQuery" runat="server" Text=" 款式圖片" 
                                            CssClass="but" Width="75px">
                                    </asp:button>
                        </td>
					</tr>
                    <tr>
						<td class="musttitle" align="center" width="10%" >
                            <asp:Literal ID="Literal23" Text="生產單位"
                                runat="server"></asp:Literal>
                        </td>
						<td width="20%" align ="left" >&nbsp;
                                <asp:DropDownList ID="ddlProductionUnitID" runat="server" 
                                Width="85%" Height="16px" ></asp:DropDownList>
                        </td>
						<td  class="smalltitle"  align="center" width="10%">
                            <asp:Literal ID="Literal24" Text="入庫日期"
                                runat="server"></asp:Literal>
                        </td>
						<td  width="20%">
                                 <asp:textbox id="txtPreInStockDate" runat="server" CssClass="TextRead" Width="60%" 
                                MaxLength="20" ></asp:textbox>
                        </td>
                        <td class="smalltitle" >
                            <asp:Literal ID="Literal19" Text="排程單號"
                                runat="server"></asp:Literal>
                            </td>
                        <td >
                            <asp:textbox id="txtScheduleBillID" runat="server" CssClass="TextRead" Width="77%" 
                                MaxLength="20" ></asp:textbox>
                            </td>
                             <td><asp:button id="btnGet" runat="server" Text=" 取用序號" 
                                            CssClass="but" Width="75px" Enabled="False"></asp:button>
                                </td>
					</tr>
                   
                   
                    </table>
                    <div id='tabs'>
                        <div class='x-tab' title='明細'> 
                      <div style="overflow: auto; WIDTH: 100%;height:250px ">                    
                           
             <asp:HiddenField ID="HiddenField1" runat="server" />
             <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" SkinID="GridViewSkin"
                                Width="1200px" >
                                <Columns>
                                    <asp:TemplateField meta:resourcekey="TemplateFieldResource3">
                                        <HeaderTemplate>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:RadioButton ID="cbsubSelect" runat="server"></asp:RadioButton>
                                        </ItemTemplate>
                                        <HeaderStyle Width="40px"></HeaderStyle>
                                        <ItemStyle Width="40px"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="RowID" HeaderText="(序號)" SortExpression="RowID">
                                        <ItemStyle HorizontalAlign="Center" Width="8%" Wrap="False" />
                                        <HeaderStyle Wrap="False" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="VolumeNo" HeaderText="(標籤卷編號)" SortExpression="VolumeNo">
                                        <ItemStyle HorizontalAlign="Left" Width="20%" Wrap="False" />
                                        <HeaderStyle Wrap="False" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="StartLabelNo" HeaderText="(起始序號)" SortExpression="StartLabelNo">
                                        <ItemStyle HorizontalAlign="Left" Width="26%" Wrap="False" />
                                        <HeaderStyle Wrap="False" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="EndLabelNo" HeaderText="(結束序號)" SortExpression="EndLabelNo">
                                        <ItemStyle HorizontalAlign="Left" Width="26%" Wrap="False" />
                                        <HeaderStyle Wrap="False" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="(張數)" SortExpression="Pages">
                                      <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                                        <ItemTemplate>
                                            <%# DataBinder.Eval(Container.DataItem, "Pages", "{0:N0}")%>
                                        </ItemTemplate>
                                        <HeaderStyle Wrap="False" />
                                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" />
                                    </asp:TemplateField>
                                    
                                </Columns>
                                <PagerSettings Visible="False" />
                            </asp:GridView>
            </div>
                           
                   
                        </div>

                        <div class='x-tab' title='備註'>                              
                                
                                <table class="maintable" cellspacing="0" cellpadding="0" width="100%" height="100%" align="center" borderColor="#ffffff" border="1" >
                                <tr>
                                <td class="smalltitle" align="center" width="15%"><asp:Literal ID="Literal20" Text="備註"
                                            runat="server"></asp:Literal></td>
                                <td >&nbsp;
                                <asp:TextBox ID="txtMemo" runat="server" CssClass="TextRead" TextMode="MultiLine" 
                                        Width="98%" Height="100%" ReadOnly="True"></asp:TextBox></td>
                                </tr>
                                                                   
                                </table> 
                        </div>          
                    </div>
                    <table class="maintable" cellspacing="0" cellpadding="0" width="100%" align="center" borderColor="#ffffff" border="1" >
                             <tr>
						                <td class="smalltitle" align="center" width="15%" ><asp:Literal ID="ltlCreateUserName" Text="建檔人員"
                                                runat="server"></asp:Literal>
                                        </td>
						                <td width="35%">&nbsp;
								                <asp:textbox id="txtCreateUserName" runat="server" CssClass="TextRead" Width="90%" 
                                                MaxLength="20" ReadOnly="True"></asp:textbox>
                                         </td>
						                <td class="smalltitle" align="center" width="15%" >                            
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
						                <td class="smalltitle" align="center"><asp:Literal ID="Literal6" Text="營運覆核"
                                                runat="server"></asp:Literal>
                                        </td>
						                <td >&nbsp;
								                <asp:textbox id="txtCheckUserName" runat="server" CssClass="TextRead" Width="90%" MaxLength="20" ReadOnly="true" ></asp:textbox>
                                        </td>
						                <td class="smalltitle" align="center">							
						 
                                            <asp:Literal ID="Literal7" Text="營運覆核日期"
                                                runat="server"></asp:Literal>
						                </td>
						                <td >&nbsp;
                                        <asp:textbox id="txtCheckDate" runat="server" CssClass="TextRead" Width="60%" 
                                                MaxLength="20" ReadOnly="true"></asp:textbox>&nbsp;<asp:Button ID="btnCheck" 
                                                runat="server" CssClass="but" OnClick="btnCheck_Click" Text=" 營運覆核" 
                                                Width="75px" />
                                         </td>
					                </tr>
                                </table> 
					</div>
                    
    </ContentTemplate>
            </asp:UpdatePanel>
			<div id="msg" runat="server">
                    <asp:Literal ID="msg_choosesub" Text="請先指定明細內容!" Visible="false" runat="server"></asp:Literal> 
                         
                    </div>
                     <input id="HdnSubDetail1" type="hidden" runat="server" />
		</form>
	</body>
</html>
