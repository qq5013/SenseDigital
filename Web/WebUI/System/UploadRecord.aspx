<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UploadRecord.aspx.cs" Inherits="WebUI_System_UploadRecord" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>上傳記錄</title>
    <link href="~/Styles/Main.css" type="text/css" rel="stylesheet" /> 
        <link href="~/Styles/op.css" type="text/css" rel="stylesheet" />
         <link rel="stylesheet" type="text/css" href="~/ext-3.3.1/resources/css/ext-all.css" /> 	   
        <script type="text/javascript" src='<%=ResolveUrl("~/Scripts/JQuery/jquery-1.8.3.min.js") %>'></script> 
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Commons.js") %>'></script>
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Resize.js") %>'></script>  
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Ajax.js") %>'></script>        
        
</head>
<body>
    <form id="form1" runat="server">
    <div>      
        <table width="100%">                    
                        <tr> 
                            <td valign="bottom" style=" font-size:14px;">                            
                               <div style="width:100%;height:300px;overflow:auto;">

                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                    SkinID="GridViewSkin" Width="100%" 
                                       onrowdatabound="GridView1_RowDataBound"    >
            <Columns>
               
                  <asp:BoundField DataField="RecordID" HeaderText="序號" >
                    <ItemStyle HorizontalAlign="Center" Width="6%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="FileType" HeaderText="檔案類型" 
                    SortExpression="FileType">
                    <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="FileName" HeaderText="檔案名稱" 
                    SortExpression="FileName">
                    <ItemStyle HorizontalAlign="Left" Width="10%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="FileDesc" HeaderText="檔案說明" 
                    SortExpression="FileDesc">
                    <ItemStyle HorizontalAlign="Left" Width="15%" Wrap="False" />
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
            </Columns>
            <PagerSettings Visible="False" />
        </asp:GridView>
                                </div>
                               
                </td>                
            </tr>           
        </table>
    </div>
    </form>
</body>
</html>
