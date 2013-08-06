<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OtherBatchActionView.aspx.cs" Inherits="WebUI_Label_OtherBatchActionView" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
        <link href="~/Styles/Main.css" type="text/css" rel="stylesheet" /> 
        <link href="~/Styles/op.css" type="text/css" rel="stylesheet" /> 
        <script type="text/javascript" src='<%=ResolveUrl("~/Scripts/JQuery/jquery-1.8.2.min.js") %>'></script> 
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Commons.js") %>'></script>
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Resize.js") %>'></script>  
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
						<td  valign="middle" align="left" colspan="4" height="22" class="title1">
							<p><asp:Literal ID="ltlTitle" Text="標籤其他異動批次作業[ 單筆明細畫面 ]" runat="server"></asp:Literal></p>
						</td>
					</tr>							
						
					<tr>
						<td class="musttitle" align="center" width="15%" ><asp:Literal ID="ltlBillID" Text="異動單號"
                                runat="server"></asp:Literal>
                        </td>
						<td width="25%" >&nbsp;
								<asp:textbox id="txtBillID" runat="server" CssClass="TextRead"  ReadOnly="true" Width="90%" MaxLength="20"></asp:textbox>
                        </td>
                        <td class="musttitle" align="center" width="15%" ><asp:Literal ID="ltlApplyBillID" Text="申請單號"
                                runat="server"></asp:Literal>
                        </td>
						<td width="25%"  colspan="2">&nbsp;
								<asp:textbox id="txtApplyBillID" runat="server" CssClass="TextRead"  ReadOnly="true" Width="90%" MaxLength="20"></asp:textbox>
                        </td>
						<td  align="center" width="35%">
                                 &nbsp;</td>
					
					</tr>
                    		
					<tr>
						<td class="musttitle" align="center" width="15%" ><asp:Literal ID="ltlEnterpriseID" Text="企業編號"
                                runat="server"></asp:Literal>
                        </td>
						<td width="50%">&nbsp;
								<asp:textbox id="txtEnterpriseID" runat="server" CssClass="TextRead"  ReadOnly="true" Width="30%" MaxLength="6"></asp:textbox>
                                &nbsp;<asp:textbox id="txtEnterpriseName" runat="server" CssClass="TextRead"  ReadOnly="true" Width="60%" MaxLength="100"></asp:textbox>
                        </td>
                        <td align="center" width="10%" class="musttitle">
                            <asp:Literal ID="ltlApplyUnit" Text="申請單位"
                                runat="server"></asp:Literal>
                        </td>	
                          <td>&nbsp;
                          <asp:DropDownList runat="server" ID="ddlApplyUnit"  Width="90%"  Enabled="false"></asp:DropDownList>
                        </td>						
					</tr>    
                            
			</table>            
           <div style="overflow: auto; WIDTH: 100%; HEIGHT: 200px">
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                    SkinID="GridViewSkin" Width="100%">
            <Columns>                
                <asp:BoundField DataField="RowID" HeaderText="(序號)" 
                    SortExpression="RowID">
                    <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="LabelReelNo" HeaderText="標籤卷編號" 
                    SortExpression="LabelReelNo">
                    <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="StyleID" HeaderText="(款式編號)" 
                    SortExpression="StyleID">
                    <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                  <asp:BoundField DataField="LabelMode" HeaderText="(標籤模式)" 
                    SortExpression="LabelMode" >
                    <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                 <asp:BoundField DataField="StartNo" HeaderText="起始序號" 
                    SortExpression="StartNo">
                    <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                 <asp:BoundField DataField="EndNo" HeaderText="結束序號" 
                    SortExpression="EndNo">
                    <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                   <asp:BoundField DataField="Quantity" HeaderText="數量" 
                    SortExpression="Quantity">
                    <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>

                   <asp:BoundField DataField="StateID" HeaderText="(檢查狀態)" 
                    SortExpression="StateID">
                    <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                 <asp:BoundField DataField="NewStateID" HeaderText="新狀態" 
                    SortExpression="NewStateID">
                    <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>            
                <asp:BoundField DataField="OrderBillID" HeaderText="異動原因" 
                    SortExpression="OrderBillID">
                    <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
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
                                 <asp:button id="Button3" runat="server" Text="狀態檢查"  CssClass="but" Width="80px" ></asp:button>
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
                                 <asp:button id="Button4" runat="server" Text="執行異動"  CssClass="but" Width="80px" ></asp:button>
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
                                MaxLength="20" ReadOnly="true"></asp:textbox></td>
					</tr>
                    <tr>
						<td class="smalltitle" align="center"><asp:Literal ID="ltlCheckUserName" Text="營運覆核"
                                runat="server"></asp:Literal>
                        </td>
						<td >&nbsp;
								<asp:textbox id="txtCheckUserName" runat="server" CssClass="TextRead" Width="90%" MaxLength="20" ReadOnly="true"></asp:textbox>
                        </td>
						<td class="smalltitle" align="center">	
                            <asp:Literal ID="ltlCheckDate" Text="營運覆核日期"
                                runat="server"></asp:Literal>
						</td>
						<td >&nbsp;
								<asp:textbox id="txtCheckDate" runat="server" CssClass="TextRead" Width="65%" ReadOnly="true"
                                MaxLength="20"></asp:textbox>&nbsp;
                                <asp:button id="btnCheck" runat="server" Text="營運覆核"  CssClass="but" 
                                Width="80px" onclick="btnCheck_Click" ></asp:button>                          
                        </td>
					</tr>
                    </table>  
    </div>
    </ContentTemplate>
            </asp:UpdatePanel> 
    </form>
</body>
</html>

