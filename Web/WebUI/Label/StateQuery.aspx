<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StateQuery.aspx.cs" Inherits="WebUI_Label_StateQuery" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <link href="~/Styles/Main.css" type="text/css" rel="stylesheet" /> 
        <link href="~/Styles/op.css" type="text/css" rel="stylesheet" /> 
    <style type="text/css">
        #img1
        {
            height: 317px;
        }
    </style>
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
         <div id="surdiv" style="overflow:auto">
			<table id="Table1" class="maintable" cellspacing="0" cellpadding="0" width="100%" align="center" bordercolor="#ffffff"
				border="1" runat="server">			
					<tr>
						<td  valign="middle" align="left" colspan="4" height="22" class="title1">
							<p><asp:Literal ID="ltlTitle" Text="標籤狀態查詢" runat="server"></asp:Literal></p>
						</td>
					</tr>							
						
					<tr>
						<td class="musttitle" align="center" width="15%" ><asp:Literal ID="ltlEnterpriseID" Text="標籤序號"
                                runat="server"></asp:Literal>
                        </td>
						<td width="35%" >&nbsp;
								<asp:textbox id="txtEnterpriseID" runat="server" CssClass="TextBox" Width="72%" MaxLength="6">Q01A0001000105</asp:textbox>
                            &nbsp;<asp:Button ID="btnSearch" runat="server" CssClass="but" Text="查詢" 
                                Width="51px" />
                        </td>
						<td class="musttitle" align="center" width="15%"><asp:Literal 
                                ID="ltlCategoryID" Text="標籤狀態"
                                runat="server"></asp:Literal>
                        </td>
						<td  width="35%">&nbsp;
								<asp:textbox id="Textbox2" runat="server" CssClass="TextRead" ReadOnly="true"  Width="85%" MaxLength="10">服役中</asp:textbox>
                        </td>
					</tr>
                    		
					<tr>
						<td class="musttitle" align="center" width="15%" ><asp:Literal ID="ltlEnterpriseName" Text="產品編號"
                                runat="server"></asp:Literal>
                        </td>
						<td width="85%"  colspan="3">&nbsp;
								<asp:textbox id="txtEnterpriseName" runat="server" CssClass="TextRead" ReadOnly="true" Width="30%" MaxLength="10">0001</asp:textbox>
                                &nbsp;
                                <asp:textbox id="Textbox1" runat="server" CssClass="TextRead" ReadOnly="true" Width="62%" MaxLength="10">魷魚</asp:textbox>
                        </td>
						
					</tr>
                    <tr>
						<td class="musttitle" align="center" width="15%" ><asp:Literal ID="ltlEnterpriseEName" Text="生產履歷"
                                runat="server"></asp:Literal>
                        </td>
                        <td width="35%" >&nbsp;
								<asp:textbox id="Textbox4" runat="server" CssClass="TextRead" ReadOnly="true" Width="90%" MaxLength="6">R0001</asp:textbox>
                        </td>
						<td class="musttitle" align="center" width="15%"><asp:Literal 
                                ID="Literal1" Text="製造日期"
                                runat="server"></asp:Literal>
                        </td>
						<td  width="25%">&nbsp;
								<asp:textbox id="Textbox3" runat="server" CssClass="TextRead" ReadOnly="true"  Width="85%" MaxLength="10">2012/12/31</asp:textbox>
                        </td>
						
					</tr>
                     <tr>
						<td  colspan="2">
                        </td>
						<td class="musttitle" align="center" width="15%"><asp:Literal 
                                ID="Literal3" Text="保固日期"
                                runat="server"></asp:Literal>
                        </td>
						<td  width="25%">&nbsp;
								<asp:textbox id="Textbox6" runat="server" CssClass="TextRead" ReadOnly="true"  Width="85%" MaxLength="10">2015/12/31</asp:textbox>
                        </td>
						
					</tr>
                    <tr>
						<td class="musttitle" align="center" width="15%" ><asp:Literal ID="Literal2" Text="官方網址"
                                runat="server"></asp:Literal>
                        </td>
                        <td width="35%" >&nbsp;
								<asp:textbox id="Textbox5" runat="server" CssClass="TextRead" ReadOnly="true" Width="90%" MaxLength="6">www.google.com</asp:textbox>
                        </td>
						<td class="musttitle" align="center" width="15%"><asp:Literal 
                                ID="Literal4" Text="客服電話"
                                runat="server"></asp:Literal>
                        </td>
						<td  width="35%">&nbsp;
								<asp:textbox id="Textbox7" runat="server" CssClass="TextRead" ReadOnly="true"  Width="85%" MaxLength="10"></asp:textbox>
                        </td>
						
					</tr>
                      <tr>
						<td class="musttitle" align="center" width="15%" ><asp:Literal ID="Literal5" Text="產品物流資訊"
                                runat="server"></asp:Literal>
                        </td>
                        <td width="35%" >&nbsp;
								<asp:textbox id="Textbox8" runat="server" CssClass="TextRead" ReadOnly="true" Width="90%" MaxLength="6">L0001</asp:textbox>
                        </td>
						<td class="musttitle" align="center" width="15%"><asp:Literal 
                                ID="Literal6" Text="預計退役日期"
                                runat="server"></asp:Literal>
                        </td>
						<td  width="25%">&nbsp;
								<asp:textbox id="Textbox9" runat="server" CssClass="TextRead" ReadOnly="true"  Width="85%" MaxLength="10">2015/12/31</asp:textbox>
                        </td>
						
					</tr>
                    <tr>
                     <td height="100px" colspan="4" align="center">
                       <div style="height:80%;width:90%">
                         <img alt="標籤" id="img1" runat="server" src="../../Images/main/pic.jpg"  width="500"/>
                       </div>                      
                     </td>
                    </tr>
                  
			</table>      
    </div>
    </ContentTemplate>
            </asp:UpdatePanel>
    </form>
</body>
</html>
