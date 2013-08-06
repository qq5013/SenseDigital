<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Download.aspx.cs" Inherits="WebUI_System_Download" %>

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

    <div>
       
            <div id="surdiv" style="overflow:auto">
            <table class="maintable" cellspacing="0" cellpadding="0" width="100%" align="center" bordercolor="#ffffff" border="1" >			
                    <tr>
						<td class="musttitle" align="center" width="15%" ><asp:Literal ID="Literal1" Text="檔案類型"
                                runat="server"></asp:Literal>
                        </td>
						<td width="85%"  colspan="3">
                            <asp:RadioButton runat="server" ID="rbFileType1" Text="一般檔案" 
                                GroupName="rbFileType" AutoPostBack="True" 
                                oncheckedchanged="rbFileType1_CheckedChanged"/>&nbsp;&nbsp;
                             <asp:RadioButton runat="server" ID="rbFileType2" Text="匯入格式檔" Checked="true" 
                                GroupName="rbFileType" oncheckedchanged="rbFileType1_CheckedChanged" 
                                AutoPostBack="True"/>
                        </td>						
					</tr>					
			</table>
           
 
                                         
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="RecordID"
                    SkinID="GridViewSkin" Width="100%" AllowSorting="True"  
                    onrowdatabound="GridView1_RowDataBound" OnSorting="GridView1_Sorting"
                    onrowcommand="GridView1_RowCommand">
            <Columns>                
                <asp:BoundField DataField="RecordID" HeaderText="序號" 
                    SortExpression="RecordID">
                    <ItemStyle HorizontalAlign="Center" Width="6%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="FileType" HeaderText="檔案類型" 
                    SortExpression="FileType">
                    <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="UseUnit" HeaderText="使用單位" 
                    SortExpression="UseUnit">
                    <ItemStyle HorizontalAlign="Left" Width="6%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="FileDesc" HeaderText="檔案說明" 
                    SortExpression="FileDesc">
                    <ItemStyle HorizontalAlign="Left" Width="15%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="FileName" HeaderText="檔案名稱" 
                    SortExpression="FileName">
                    <ItemStyle HorizontalAlign="Left" Width="10%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="UserName" HeaderText="上傳用戶" 
                    SortExpression="UserName">
                    <ItemStyle HorizontalAlign="Left" Width="10%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="UploadDate" HeaderText="上傳日期"  HtmlEncode="false" DataFormatString="{0:yyyy/MM/dd HH:mm}"
                    SortExpression="UploadDate">
                    <ItemStyle HorizontalAlign="Left" Width="10%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>                
               
                <asp:BoundField DataField="Memo" HeaderText="備註" 
                    SortExpression="Memo">
                    <ItemStyle HorizontalAlign="Left" Width="30%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>         
               <asp:TemplateField HeaderText="下載" >
                    <ItemTemplate>
                    <asp:Button ID="btnDownload" Text="Download" CommandName="DownLoad" CommandArgument='<%# Eval("RecordID") %>' runat="server" />
                    </ItemTemplate>
                  <ItemStyle Width="12%" Wrap="False" />
                  <HeaderStyle Width="12%" Wrap="False" />
               </asp:TemplateField>
               <asp:TemplateField HeaderText="刪除" >
                    <ItemTemplate>
                    <asp:Button ID="btnDelete" Text="Delete" CommandName="Del" CommandArgument='<%# Eval("RecordID") %>' runat="server" />
                    </ItemTemplate>
                  <ItemStyle Width="12%" Wrap="False" />
                  <HeaderStyle Width="12%" Wrap="False" />
               </asp:TemplateField>
            </Columns>
            <PagerSettings Visible="False" />
        </asp:GridView>
            


        </div>
    </div>

    </form>
</body>
</html>

