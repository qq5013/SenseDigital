<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OpRecord.aspx.cs" Inherits="WebUI_Customer_OpRecord" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <link href="~/Styles/Main.css" type="text/css" rel="stylesheet" /> 
        <link href="~/Styles/op.css" type="text/css" rel="stylesheet" /> 
         <script type="text/javascript" src='<%=ResolveUrl("~/Scripts/JQuery/jquery-1.8.3.min.js") %>'></script>
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Commons.js") %>'></script>
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Ajax.js") %>'></script>  
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
			<table class="maintable" cellspacing="0" cellpadding="0" width="100%" align="center" bordercolor="#ffffff"
				border="1" >			
					                    		
					<tr>
						<td class="musttitle" align="center" width="15%" ><asp:Literal ID="ltlMemberID" Text="會員ID"
                                runat="server"></asp:Literal>
                        </td>
						<td width="25%" >&nbsp;
								<asp:textbox id="txtMemberID" runat="server" CssClass="TextBox" Width="90%" MaxLength="20"></asp:textbox>
                        </td>
						<td class="musttitle" align="center" width="15%"><asp:Literal 
                                ID="ltlPersonName" Text="姓名"
                                runat="server"></asp:Literal>
                        </td>
						<td  width="30%">&nbsp;
		                    						<asp:textbox id="txtPersonName" runat="server" CssClass="TextRead" ReadOnly="true"  Width="70%" MaxLength="10">李大仁</asp:textbox>&nbsp;
                                  <asp:button id="btnQuery" runat="server" Text="查询"  CssClass="but" 
                                Width="70px" onclick="btnQuery_Click" ></asp:button>
                        </td>
					</tr>
                    <tr>
                     <td colspan="4">
                       <div style="height:520px; overflow:auto;">
             <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                    SkinID="GridViewSkin" Width="100%" AllowSorting="True"  
                    >
                 <Columns>
      <asp:BoundField DataField="RecordID" HeaderText="(序號)" SortExpression="RecordID" > 
<ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" /> <HeaderStyle Wrap="False" /> 
</asp:BoundField>  
<asp:BoundField DataField="OpDate" HeaderText="(日期)" HtmlEncode="false" DataFormatString="{0:yyyy/MM/dd HH:mm}" SortExpression="OpDate" > 
<ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" /> <HeaderStyle Wrap="False" /> 
</asp:BoundField> 
<asp:BoundField DataField="FromName" HeaderText="(功能名稱)" SortExpression="FromName" > 
<ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" /> <HeaderStyle Wrap="False" /> 
</asp:BoundField>  
<asp:BoundField DataField="ActionState" HeaderText="(操作狀態)" SortExpression="ActionState" > 
<ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" /> <HeaderStyle Wrap="False" /> 
</asp:BoundField>  
<asp:BoundField DataField="IP" HeaderText="(IP)" SortExpression="IP" > 
<ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" /> <HeaderStyle Wrap="False" /> 
</asp:BoundField>  

 </Columns>
            <PagerSettings Visible="False" />
        </asp:GridView>
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
