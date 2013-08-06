<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EnableLabelNoEdit.aspx.cs" Inherits="WebUI_Label_EnableLabelNoEdit" %>
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
<script type="text/javascript" src="Js/EnableLabelNo.js"></script>

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
                $("#btnLoad").bind("click", function () {
                    window.showModalDialog('EnableLabelNoStockToID.aspx', null, 'dialogHeight:300px;dialogWidth:1000px;help:no;scroll:no'); return false;
                    return false;
                });
                content_resize();
                subData[0] = jQuery.parseJSON($('#HdnSubDetail1').val());
                initialTable();
                AddToDetail();
                $('#btnAddDetail').bind('click', function () { doOption('Add', 1); });
                $('#btnDelDetail').bind('click', function () { doOption('Del'); });
                $('#btnInsDetail').bind('click', function () { doOption('Ins', 1); });

                //                var tabPanel = new Ext.TabPanel({
                //                    height: 300,
                //                    width: "100%",
                //                    autoTabs: true, //自动扫描页面中的div然后将其转换为标签页
                //                    deferredRender: false, //不进行延时渲染
                //                    activeTab: 0, //默认激活第一个tab页
                //                    animScroll: true, //使用动画滚动效果
                //                    enableTabScroll: true, //tab标签超宽时自动出现滚动按钮
                //                    applyTo: 'tabs'
                //                });
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
            <table class="maintable" cellspacing="0" cellpadding="0" width="100%" align="center" bordercolor="#ffffff"
				border="1" >			
					<tr>
						<td  valign="middle" align="left" colspan="4" height="22" class="title1">
							<p><asp:Literal ID="ltlTitle" Text="登錄啟用標籤序號[ 單筆編輯畫面 ]" runat="server"></asp:Literal></p>
						</td>
					</tr>							
						
					<tr>
						<td class="musttitle" align="center" width="10%" ><asp:Literal ID="ltlEnterpriseID" Text="啟用日期"
                                runat="server"></asp:Literal>
                        </td>
						<td width="30%" >&nbsp;
							<uc2:Calendar runat="server"  ID="txtEnableDate" />
                     &nbsp;&nbsp;
								  </td>
                        <td class="musttitle" align="center" width="10%" ><asp:Literal ID="Literal3" Text="啟用單號"
                                runat="server"></asp:Literal>
                        </td>
						<td width="30%" >&nbsp;
								<asp:textbox id="txtBillID" runat="server"  CssClass="TextRead"  ReadOnly="true"
                                Width="70%" MaxLength="6"></asp:textbox>
                 
								  
                            &nbsp;
                 
								  
                            <asp:button id="btnLoad" runat="server" Text="載入入庫序號"  CssClass="but" Width="80px"></asp:button>
								  
                        </td>

					</tr>
                    		
					<tr>
						<td class="musttitle" align="center" width="10%" ><asp:Literal ID="ltlEnterpriseName" Text="企業編號"
                                runat="server"></asp:Literal>
                        </td>
						<td colspan="3">&nbsp;
								<asp:textbox id="txtEnterpriseID" runat="server" CssClass="TextBox" Width="30%" MaxLength="10"></asp:textbox>
                                <asp:textbox id="txtEnterpriseName" runat="server" CssClass="TextRead"  ReadOnly="true" Width="57%" MaxLength="10"></asp:textbox>
                        &nbsp;
                 
								  
                            </td>
                       				
					</tr>                 

			</table>              
            
            
            <table style="width:100%">
                        <tr>
                            <td class="table_titlebgcolor" height="25px">
                                <input id="btnAddDetail" class="ButtonCss" type="button" value="新增明細" style="width:60px;"  />
                                <input id="btnDelDetail" class="ButtonCss" type="button" value="刪除明細" style="width:60px;" />
                                <input id="btnInsDetail" class="ButtonCss" type="button" value="插入明細"  style="width:60px;" />
                                <input id="btnInsDetail0" class="ButtonCss" type="button" value="以下同值"  
                                    style="width:60px;" /></td>
                        </tr>
              </table> 

               <div class="fakeContainer" id="div_Detail1" style=" margin:5px;height:205px;">
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
								<asp:textbox id="txtEP_CheckDate" runat="server" CssClass="TextRead" Width="90%" ReadOnly="true"
                                MaxLength="20"></asp:textbox>&nbsp;
                                </td>
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
								<asp:textbox id="txtBU_CheckDate" runat="server" CssClass="TextRead" Width="90%" ReadOnly="true"
                                MaxLength="20"></asp:textbox></td>
					</tr>
			</table>              
           
          
    <input id="HdnSubDetail1" type="hidden" runat="server" />
   
    </form>
</body>
</html>
