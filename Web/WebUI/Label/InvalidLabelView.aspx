<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InvalidLabelView.aspx.cs" Inherits="WebUI_Label_InvalidLabelView" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link href="~/Styles/Main.css" type="text/css" rel="stylesheet" /> 
        <link href="~/Styles/op.css" type="text/css" rel="stylesheet" /> 
        <link href="~/Styles/superTables.css" type="text/css" rel="stylesheet" /> 
        <script type="text/javascript" src='<%=ResolveUrl("~/Scripts/JQuery/jquery-1.8.2.min.js") %>'></script> 	  
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Resize.js") %>'></script>  
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Commons.js") %>'></script>
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Ajax.js") %>'></script>
        <script type="text/javascript">
            function Save() {
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
                        <asp:Button ID="btnAdd" runat="server" Text="新增" CssClass="ButtonCreate" OnClientClick="Edit(false);return false" />
                        <asp:Button ID="btnDelete" runat="server" Text="刪除" CssClass="ButtonDel" 
                            onclick="btnDelete_Click" />
                        <asp:Button ID="btnEdit" runat="server" Text="修改" CssClass="ButtonModify" 
                            OnClientClick="return BillEdit(true);" onclick="btnEdit_Click" />
                        <asp:Button ID="btnExit" runat="server" Text="離開" OnClientClick="Exit()" CssClass="ButtonExit" />
                    </td>
                </tr>
            </table>
            <div id="surdiv" style="overflow:auto">
			<table id="Table1" class="maintable" cellspacing="0" cellpadding="0" width="100%" align="center" bordercolor="#ffffff"
				border="1" runat="server">			
					<tr>
						<td  valign="middle" align="left" colspan="2" height="22" class="title1">
							<p><asp:Literal ID="ltlTitle" Text="登錄標籤生產作廢清單[ 單筆明細畫面 ]" runat="server"></asp:Literal></p>
						</td>
					</tr>							
						
					<tr>
						<td class="musttitle" align="center" width="15%" ><asp:Literal ID="ltlBillID" Text="作廢單號"
                                runat="server"></asp:Literal>
                        </td>
						<td width="70%" >&nbsp;
								<asp:textbox id="txtBillID" runat="server" CssClass="TextRead"  ReadOnly="true" Width="90%" MaxLength="20"></asp:textbox>
                       &nbsp;&nbsp;
								  </td>
					</tr>
                    		
					<tr>
						<td class="musttitle" align="center" width="15%" ><asp:Literal ID="ltlEnterpriseID" Text="企業編號"
                                runat="server"></asp:Literal>
                        </td>
						<td width="85%">&nbsp;
								<asp:textbox id="txtEnterpriseID" runat="server" CssClass="TextRead"  ReadOnly="true" Width="30%" MaxLength="6"></asp:textbox>
                                <asp:textbox id="txtEnterpriseName" runat="server" CssClass="TextRead"  ReadOnly="true" Width="60%" MaxLength="100"></asp:textbox>
                        </td>
                       						
					</tr>                 
					
			</table>           
            
             <div  style=" HEIGHT: 200px">
          <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                    SkinID="GridViewSkin" Width="100%" onrowdatabound="GridView1_RowDataBound" 
                  >
            <Columns>      
                <asp:BoundField DataField="SubID" HeaderText="(序號)" 
                    SortExpression="SubID">
                    <ItemStyle HorizontalAlign="Left" Width="5%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="LabellNo" HeaderText="標籤序號" 
                    SortExpression="LabellNo">
                    <ItemStyle HorizontalAlign="Left" Width="15%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="Memo" HeaderText="備註" 
                    SortExpression="Memo">
                    <ItemStyle HorizontalAlign="Left" Width="75%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                  <asp:BoundField DataField="StockInBillID" HeaderText="(入庫單號)" 
                    SortExpression="StockInBillID" >
                    <ItemStyle HorizontalAlign="Left" Width="5%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
               
            </Columns>
            <PagerSettings Visible="False" />
        </asp:GridView>
        
          </div>      
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
								<asp:textbox id="txtQtyTotal" runat="server" CssClass="TextRead" Width="65%" 
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
								<asp:textbox id="txtCreateDate" runat="server" CssClass="TextRead" Width="65%" 
                                MaxLength="20" ReadOnly="True"></asp:textbox>&nbsp;
                                <asp:button id="btnInvalid" runat="server" Text="執行作廢"  CssClass="but" Width="80px"  ></asp:button>
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
                        <asp:textbox id="txtLastModifyDate" runat="server" CssClass="TextRead" Width="65%" 
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
								<asp:textbox id="txtEP_CheckDate" runat="server" CssClass="TextRead" Width="65%" ReadOnly="true"
                                MaxLength="20"></asp:textbox>&nbsp;
                                <asp:Button ID="btnEPCheck" runat="server" CssClass="but" 
                                Text="企業覆核" Width="80px" />
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
								<asp:textbox id="txtBU_CheckDate" runat="server" CssClass="TextRead" Width="65%" ReadOnly="true"
                                MaxLength="20"></asp:textbox>&nbsp;
                                <asp:button id="btnBUCheck" runat="server" Text="營運覆核"  CssClass="but" 
                                Width="80px" onclick="btnBUCheck_Click" ></asp:button>                          
                        </td>
					</tr>
                    </table>  
            </div>
        </div>       
    </div>
    </ContentTemplate>
            </asp:UpdatePanel> 
    </form>
</body>
</html>
