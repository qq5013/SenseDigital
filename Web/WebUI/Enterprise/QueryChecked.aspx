<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QueryChecked.aspx.cs" Inherits="WebUI_Enterprise_QueryChecked" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head id="Head1" runat="server">
		<meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
  
    <title></title> 
        <link href="~/Styles/Main.css" type="text/css" rel="stylesheet" /> 
        <link href="~/Styles/op.css" type="text/css" rel="stylesheet" />

        <script type="text/javascript" src='<%=ResolveUrl("~/Scripts/JQuery/jquery-1.8.3.min.js") %>'></script> 
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Commons.js") %>'></script>
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Resize.js") %>'></script>  
        <script type="text/javascript">

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
                    <td align="right">
                        <asp:Button ID="btnPrint" runat="server" Text="列印" CssClass="ButtonPrint" 
                            onclick="btnPrint_Click" />
                        <asp:Button ID="btnExit" runat="server" Text="離開" OnClientClick="return Exit()" CssClass="ButtonExit" />
                    </td>
                </tr>
            </table>
            <div id="surdiv" style="overflow:auto">
            <table class="maintable" cellspacing="0" cellpadding="0" width="100%" align="center" bordercolor="#ffffff" border="1" >			
					<tr>
						<td  valign="middle" align="left" colspan="4" height="22" class="title1">
							<p><asp:Literal ID="ltlTitle" Text="認證後查詢" runat="server"></asp:Literal></p>
						</td>
					</tr>
					<tr>
						<td class="musttitle" align="center" width="15%" ><asp:Literal ID="ltlEnterpriseName" Text="企業用戶"
                                runat="server"></asp:Literal>
                        </td>
						<td width="85%"  colspan="3">&nbsp;
								<asp:dropdownlist id="ddlEnterpriseID" runat="server" Width="90%" 
                                AutoPostBack="True" 
                                onselectedindexchanged="ddlEnterpriseID_SelectedIndexChanged" ></asp:dropdownlist>
                        </td>						
					</tr>
			</table>
           
 
                                         
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                    SkinID="GridViewSkin" Width="1000px" 
                    onrowdatabound="GridView1_RowDataBound">
            <Columns>                
                <asp:BoundField DataField="RowID" HeaderText="序號" 
                    SortExpression="RowID">
                    <ItemStyle HorizontalAlign="Center" Width="8%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="CheckItem" HeaderText="認證覆核項目" 
                    SortExpression="CheckItem">
                    <ItemStyle HorizontalAlign="Left" Width="12%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="UploadDate" HeaderText="企業上傳日期"  HtmlEncode="false" DataFormatString="{0:yyyy/MM/dd HH:mm}"
                    SortExpression="UploadDate">
                    <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="EP_CheckDate" HeaderText="企業覆核日期" HtmlEncode="false" DataFormatString="{0:yyyy/MM/dd HH:mm}"
                    SortExpression="EP_CheckDate">
                    <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                  <asp:BoundField DataField="EP_CheckUserName" HeaderText="企業覆核人員" 
                    SortExpression="EP_CheckUserName" >
                    <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                 <asp:BoundField DataField="Rows" HeaderText="上傳筆數" 
                    SortExpression="Rows">
                    <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                 <asp:BoundField DataField="BU_CheckDate" HeaderText="營運覆核日期" HtmlEncode="false" DataFormatString="{0:yyyy/MM/dd HH:mm}"
                    SortExpression="BU_CheckDate">
                    <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                 <asp:BoundField DataField="BU_CheckUserName" HeaderText="營運覆核人員" 
                    SortExpression="BU_CheckUserName">
                    <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="Rows" HeaderText="覆核筆數" 
                    SortExpression="Rows">
                    <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>         
               
            </Columns>
            <PagerSettings Visible="False" />
        </asp:GridView>
            


        </div>
    </div>
    </ContentTemplate>
            </asp:UpdatePanel> 
    </form>
</body>
</html>
