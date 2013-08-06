<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StockInEdit.aspx.cs" Inherits="WebUI_Label_StockInEdit" %>

<%@ Register src="../../Controls/Calendar.ascx" tagname="Calendar" tagprefix="uc2" %>
<%@ Register Assembly="EditableDropDownList" Namespace="EditableControls" TagPrefix="editable" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head id="Head1" runat="server">
		<meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
  
    <title></title> 
           <link href="~/Styles/Main.css" type="text/css" rel="stylesheet" /> 
        <link href="~/Styles/op.css" type="text/css" rel="stylesheet" /> 
        <link href="~/Styles/Detail.css" type="text/css" rel="stylesheet" /> 
        <link href="~/Styles/superTables.css" type="text/css" rel="stylesheet" /> 
        <link rel="stylesheet" type="text/css" href="~/ext-3.3.1/resources/css/ext-all.css" />
 	    <script type="text/javascript" src='<%=ResolveUrl("~/ext-3.3.1/ext-base.js") %>'></script>
        <script type="text/javascript" src='<%=ResolveUrl("~/ext-3.3.1/ext-all.js") %>'></script>

        <script type="text/javascript" src='<%=ResolveUrl("~/Scripts/JQuery/jquery-1.8.2.min.js") %>'></script> 
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Resize.js") %>'></script>  
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Detail.js") %>'></script>
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/superTables.js") %>'></script>
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Scrolltable.js") %>'></script>
        <script type="text/javascript" src="Js/StockIn.js"></script>

        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/EditDropDownList/Js/jquery.ui.core.js") %>'></script> 
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/EditDropDownList/Js/jquery.ui.widget.js") %>'></script> 
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/EditDropDownList/Js/jquery.ui.button.js") %>'></script> 
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/EditDropDownList/Js/jquery.ui.position.js") %>'></script> 
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/EditDropDownList/Js/jquery.ui.autocomplete.js") %>'></script> 
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/EditDropDownList/Js/jquery.ui.combobox.js") %>'></script> 
        
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Commons.js") %>'></script>
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Ajax.js") %>'></script>   
        <script type="text/javascript">
            var strDateFormat = "y/MM/dd";
            $(document).ready(function () {

                //content_resize();
                subData[0] = jQuery.parseJSON($('#HdnSubDetail1').val());
                initialTable();
                AddToDetail();
                doOption('Add', 1);
                $('#btnAddDetail').bind('click', function () { doOption('Add', 1); });
                $('#btnDelDetail').bind('click', function () { doOption('Del'); });
                $('#btnInsDetail').bind('click', function () { doOption('Ins', 1); });

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
                });
            });

            function Save() { }
        </script>
	</head>
	<body>
		<form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" />  
            <table style="width: 100%; height: 20px;" class="OperationBar">
                <tr>
                    <td align="right">
                        
                        <asp:Button ID="btnCancel" runat="server" Text="放棄" OnClientClick="history.go(-1);return false" CssClass="ButtonCancel" />
                        <asp:Button ID="btnSave" runat="server" Text="保存" OnClientClick="return Save()" 
                            CssClass="ButtonCreate" onclick="btnSave_Click" />
                        <asp:Button ID="btnExit" runat="server" Text="離開" OnClientClick="Exit()" CssClass="ButtonDel" />
                    </td>
                </tr>
            </table>
            <div id="surdiv" style="overflow:auto">
			<table class="maintable" cellspacing="0" cellpadding="0" width="100%" align="center" borderColor="#ffffff" border="1" >
					<tr>
						<td  valign="middle" align="left" colspan="4" height="22" class="title1">
							<p><asp:Literal ID="ltlTitle" Text="標籤入庫單[ 單筆編輯畫面 ]" runat="server"></asp:Literal></p>
						</td>
					</tr>
						<tr>
						<td class="musttitle" align="center" width="15%" ><asp:Literal ID="ltlStyleID" Text="入庫日期"
                                runat="server"></asp:Literal></td>
						<td width="35%" >&nbsp;
								<uc2:Calendar ID="txtBillDate" runat="server" />
                        </td>
						<td class="musttitle" align="center" width="15%"><asp:Literal 
                                ID="ltlEnterpriseID" Text="入庫單號"
                                runat="server"></asp:Literal></td>
						<td  width="35%">&nbsp;
								<asp:textbox id="txtBillID" runat="server" CssClass="Textbox" Width="90%" 
                                MaxLength="20" ></asp:textbox>
                                &nbsp;
                                </td>
					</tr>
					    <tr>
						<td class="musttitle" align="center" ><asp:Literal ID="ltlStyleName" Text="企業編號"
                                runat="server"></asp:Literal></td>
						<td colspan="3">&nbsp;
								
                        		<asp:textbox id="txtEnterpriseID" runat="server" CssClass="Textbox" Width="30%" 
                                MaxLength="20" ></asp:textbox>&nbsp;
								<asp:textbox id="txtEnterpriseName" runat="server" CssClass="TextRead" Width="65%" MaxLength="20" ></asp:textbox>
                        </td>
                       
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
                        <asp:TextBox ID="txtStyleID" runat="server" CssClass="Textbox"
                            MaxLength="20" Width="30%"></asp:TextBox>&nbsp;
                        <asp:textbox id="txtLabelMode" runat="server" CssClass="TextRead" Width="58%" 
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
                              &nbsp;<asp:TextBox ID="TextBox2" runat="server" CssClass="TextRead" ReadOnly="true"
                            MaxLength="20" Width="90%"></asp:TextBox>
                            </td>
                             <td class="smalltitle" align="center" width="15%"><asp:Literal ID="Literal13" runat="server" Text="預計生產量"></asp:Literal>    
                            
                            </td>
                             <td width="20%">
                              &nbsp;<asp:TextBox ID="TextBox3" runat="server" CssClass="TextRead" ReadOnly="true"
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
								&nbsp;<asp:textbox id="txtRemainEndNo" runat="server" CssClass="TextRead" Width="90%" 
                                MaxLength="20" ReadOnly="True"></asp:textbox>
								
                            </td>
                             <td class="smalltitle" align="center" width="15%">
                        <asp:Literal ID="Literal22" runat="server" 
                            Text="作廢單號"></asp:Literal>    
                            
                            </td>
                             <td width="20%">
								&nbsp;<asp:textbox id="txtInvalidBillID" runat="server" CssClass="TextRead" Width="85%" 
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
                                    
                                        &nbsp;<asp:button id="btnLoad" runat="server" Text=" 載入單據" 
                                            CssClass="but" Width="75px"  OnClientClick ="window.showModalDialog('StockInLoad.aspx',null,'dialogHeight:300px;dialogWidth:1000px;help:no;scroll:no');return false;" >
                                    </asp:button>
                        &nbsp;<asp:button id="btnCheck2" runat="server" Text=" 款式圖片" 
                                            CssClass="but" Width="75px"  
                                OnClientClick ="window.showModalDialog('ImageQuery.aspx',null,'dialogHeight:300px;dialogWidth:600px;help:no;scroll:no');return false;" >
                                    </asp:button>
								&nbsp;</td>   
                    </tr>

                   
                    </table>
                    <div id='tabs'>
                        <div class='x-tab' title='入庫明細'> 
       <table style="width:100%">
                        <tr>
                            <td class="table_titlebgcolor" height="25px">
                                <input id="btnAddDetail" class="ButtonCss" type="button" value="新增明細" style="width:60px;"  />
                                <input id="btnDelDetail" class="ButtonCss" type="button" value="刪除明細" style="width:60px;" />
                                <input id="btnInsDetail" class="ButtonCss" type="button" value="插入明細"  style="width:60px;" />
                            </td>
                        </tr>
              </table> 

               <div class="fakeContainer" id="div_Detail1" style=" margin:5px;height:110px;">
               </div> 
            
                        </div>

                        <div class='x-tab' title='備註'>                              
                                
                                <table class="maintable" cellspacing="0" cellpadding="0" width="100%" height="100%" align="center" borderColor="#ffffff" border="1" >
                                <tr>
                                <td class="smalltitle" align="center" width="15%"><asp:Literal ID="Literal20" Text="備註"
                                            runat="server"></asp:Literal></td>
                                <td colspan ="3" >&nbsp;
                                <asp:TextBox ID="txtMemo" runat="server" CssClass="Textread" ReadOnly="true" TextMode="MultiLine" 
                                        Width="98%" Height="100%" ></asp:TextBox></td>
                                </tr>
                                 
                                
                                </table> 
                        </div>          
                    </div>
			
			<table class="maintable" cellspacing="0" cellpadding="0" width="100%" align="center" borderColor="#ffffff" border="1" >
              
                <tr>
                       <td  class="smalltitle" align="center"><asp:Literal ID="Literal16" runat="server" Text="入庫總數量合計"></asp:Literal>
                       </td>
                      <td colspan="3" height="22px">
                        <table width="100%">
                          <tr>                             
                             <td width="20%">
                              &nbsp;
                              <asp:TextBox ID="TextBox6" runat="server" CssClass="TextRead" ReadOnly="true"
                            MaxLength="20" Width="90%"></asp:TextBox>
                            </td>
                             <td class="smalltitle" align="center" width="15%"><asp:Literal ID="Literal17" runat="server" Text="尾數備品調減"></asp:Literal>                             
                            </td>
                             <td width="20%">
                              <asp:TextBox ID="TextBox7" runat="server" CssClass="Textbox"
                            MaxLength="20" Width="90%"></asp:TextBox>
                            </td>
                             <td class="smalltitle" align="center" width="15%"><asp:Literal ID="Literal18" runat="server" Text="差異數量"></asp:Literal>    
                            
                            </td>
                             <td width="20%">
                              <asp:TextBox ID="TextBox8" runat="server" CssClass="TextRead" ReadOnly="true"
                            MaxLength="20" Width="85%"></asp:TextBox>
                            </td>
                          </tr>
                        </table>
                      </td>
                    </tr>
                 <tr>
                    <td  class="smalltitle" align="center"><asp:Literal ID="Literal2" runat="server" Text="壞品數量合計"></asp:Literal></td>
                    <td >&nbsp;<asp:textbox id="txtQtyTotal" runat="server" CssClass="TextRead" Width="90%" 
                                MaxLength="20" ReadOnly="True">0</asp:textbox></td>
                    <td  class="smalltitle" align="center"><asp:Literal ID="Literal7" runat="server" Text="匯入圖檔數量"></asp:Literal></td>
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
                      <td class="smalltitle" align="center"><asp:Literal ID="Literal14" runat="server" 
                            Text="企業覆核"></asp:Literal></td>
                    <td>&nbsp;<asp:textbox id="Textbox4" runat="server" CssClass="TextRead" Width="90%" 
                                MaxLength="20" ReadOnly="True"></asp:textbox>
								
                        </td>
                    <td class="smalltitle" align="center"><asp:Literal ID="Literal15" runat="server" Text="企業覆核日期"></asp:Literal></td>
                    <td>&nbsp;<asp:textbox id="Textbox5" runat="server" CssClass="TextRead" Width="90%" 
                                MaxLength="20" ReadOnly="True"></asp:textbox></td>
                  
                    </tr>
                     <tr>
                      <td class="smalltitle" align="center"><asp:Literal ID="Literal10" runat="server" 
                            Text="營運覆核"></asp:Literal></td>
                    <td>&nbsp;<asp:textbox id="txtBU_CheckUserName" runat="server" CssClass="TextRead" Width="90%" 
                                MaxLength="20" ReadOnly="True"></asp:textbox>
								
                        </td>
                    <td class="smalltitle" align="center"><asp:Literal ID="Literal1" runat="server" Text="營運覆核日期"></asp:Literal></td>
                    <td>&nbsp;<asp:textbox id="txtBU_CheckDate" runat="server" CssClass="TextRead" Width="90%" 
                                MaxLength="20" ReadOnly="True"></asp:textbox></td>
                  
                    </tr>
            
            </table>
            </div>
			<input id="HdnSubDetail1" type="hidden" runat="server" />
		</form>
	</body>
</html>
