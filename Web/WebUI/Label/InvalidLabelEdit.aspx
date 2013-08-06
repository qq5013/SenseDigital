<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InvalidLabelEdit.aspx.cs" Inherits="WebUI_Label_InvalidLabelEdit" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
        <script type="text/javascript" src="Js/InvalidLabel.js"></script>

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
                $("#btnload").bind("click", function () {
                    window.showModalDialog('InvalidLabelLoad.aspx?FormID=LB_Order&ID=' + $('#txtOrderID').val(), $('#txtOrderID').val() + ',' + $('#txtEnterpriseID').val() + ',' + $('#txtEnterpriseName').val(), 'dialogHeight:300px;dialogWidth:820px;toolbar=no,status=yes,resizable=no'); return false;
                });
                content_resize();
                subData[0] = jQuery.parseJSON($('#HdnSubDetail1').val());
                initialTable();
                AddToDetail();
            });
            function Save() {
            }
        </script>
</head>
<body>
    <form id="form1" runat="server">
   <asp:ScriptManager ID="ScriptManager1" runat="server" />  
    <div>
           <table style="width: 100%; height: 20px;" class="OperationBar">
                <tr>
                      <td align="right">
                        <asp:Button ID="btnCancel" runat="server" Text="放棄" OnClientClick="history.go(-1);return false" CssClass="ButtonCancel" />
                        <asp:Button ID="btnSave" runat="server" Text="保存" OnClientClick="return Save()" 
                            CssClass="ButtonCreate" onclick="btnSave_Click" />
                        <asp:Button ID="btnExit" runat="server" Text="離開" OnClientClick="Exit()" CssClass="ButtonExit" />
                    </td>
                </tr>
            </table>
            <div id="surdiv" style="overflow:auto">
			<table id="Table1" class="maintable" cellspacing="0" cellpadding="0" width="100%" align="center" bordercolor="#ffffff"
				border="1" runat="server">			
					<tr>
						<td  valign="middle" align="left" colspan="2" height="22" class="title1">
							<p><asp:Literal ID="ltlTitle" Text="登錄標籤生產作廢清單[ 單筆編輯畫面 ]" runat="server"></asp:Literal></p>
						</td>
					</tr>							
						
					<tr>
						<td class="musttitle" align="center" width="15%" ><asp:Literal ID="ltlBillID" Text="作廢單號"
                                runat="server"></asp:Literal>
                        </td>
						<td width="85%" >&nbsp;
								<asp:textbox id="txtBillID" runat="server" CssClass="TextRead"  ReadOnly="true" Width="30%" MaxLength="20"></asp:textbox>
                     &nbsp;&nbsp;
								  </td>
					</tr>
                    		
					<tr>
						<td class="musttitle" align="center" width="15%" ><asp:Literal ID="ltlEEnterpriseID" Text="企業編號"
                                runat="server"></asp:Literal>
                        </td>
						<td width="85%" >&nbsp;
								<asp:textbox id="txtEnterpriseID" runat="server" CssClass="TextBox" Width="30%" MaxLength="6"></asp:textbox>
                                <asp:textbox id="txtEnterpriseName" runat="server" CssClass="TextRead"  ReadOnly="true" Width="50%" MaxLength="100"></asp:textbox>
                        &nbsp;
								  <asp:button id="btnload" runat="server" Text="載入資料"  CssClass="but" Width="80px"></asp:button>
                        </td>
                       						
					</tr>                 
					
			</table>           
            <div class="fakeContainer" id="div_Detail1" style=" margin:5px;height:205px;">
               </div>   
             <%--<div id="table-container" style="overflow: auto; WIDTH: 100%; HEIGHT: 200px">
          <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                    SkinID="GridViewSkin" Width="100%" 
                  >
            <Columns>      
                <asp:BoundField DataField="SubID" HeaderText="(序號)" 
                    SortExpression="SubID">
                    <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="LabellNo" HeaderText="標籤序號" 
                    SortExpression="LabellNo">
                    <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="Memo" HeaderText="備註" 
                    SortExpression="Memo">
                    <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                  <asp:BoundField DataField="State" HeaderText="(現行狀態)" 
                    SortExpression="State" >
                    <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
               
            </Columns>
            <PagerSettings Visible="False" />
        </asp:GridView>
        
          </div>     --%> 
            <table width="100%" class="maintable" bordercolor="#ffffff"
				border="1">     
                  <tr>
						<td class="smalltitle" align="center" width="15%"><asp:Literal ID="ltlBillState" Text="狀&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;態"
                                runat="server"></asp:Literal>
                        </td>
						<td width="30%">&nbsp;
								<asp:textbox id="txtBillState" runat="server" CssClass="TextRead" Width="90%" 
                                MaxLength="20" ReadOnly="True"></asp:textbox>
                         </td>
						<td class="smalltitle" align="center" width="15%">                            
                            <asp:Literal ID="ltlQtyTotal" runat="server" Text="數量合計"></asp:Literal>							
						    
						</td>
						<td width="40%">&nbsp;
								<asp:textbox id="txtQtyTotal" runat="server" CssClass="TextRead" Width="90%" 
                                MaxLength="20" ReadOnly="True"></asp:textbox>&nbsp;
                                 </td>
					</tr>                   
                 <tr>
						<td class="smalltitle" align="center" width="15%"><asp:Literal ID="ltlCreateUserName" Text="建檔人員"
                                runat="server"></asp:Literal>
                        </td>
						<td width="25%">&nbsp;
								<asp:textbox id="txtCreateUserName" runat="server" CssClass="TextRead" Width="90%" 
                                MaxLength="20" ReadOnly="True"></asp:textbox>
                         </td>
						<td class="smalltitle" align="center" width="15%">                            
                            <asp:Literal ID="ltlCreateDate" runat="server" Text="建檔日期"></asp:Literal>							
						    
						</td>
						<td width="40%">&nbsp;
								<asp:textbox id="txtCreateDate" runat="server" CssClass="TextRead" Width="90%" 
                                MaxLength="20" ReadOnly="True"></asp:textbox>&nbsp;
                                 </td>
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
                                MaxLength="20" ReadOnly="true"></asp:textbox>&nbsp;
                                 </td>
					</tr>
                       <tr>
						<td class="smalltitle" align="center"><asp:Literal ID="ltlEP_CheckUserName" Text="企業覆核"
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
                                MaxLength="20"></asp:textbox>&nbsp;
                                </td>
					</tr>
                    <tr>
						<td class="smalltitle" align="center"><asp:Literal ID="ltlBU_CheckUserName" Text="營運覆核"
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
								<asp:textbox id="txtBU_CheckDate" runat="server" CssClass="TextRead" Width="90%" ReadOnly="true"
                                MaxLength="20"></asp:textbox>&nbsp;
                                </td>
					</tr>
                    </table>  
            </div>
        </div>       
    </div>
   
    </form>
</body>
</html>
