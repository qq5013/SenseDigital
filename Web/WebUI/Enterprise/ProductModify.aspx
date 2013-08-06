<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProductModify.aspx.cs" Inherits="WebUI_Enterprise_ProductModify" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
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
                               <div style="width:100%;height:130px;overflow:auto;">
                                <%--<asp:GridView ID="GridView1" SkinID="GridViewSkin" DataKeyNames="ProductID" runat="server" AutoGenerateColumns="false">
                                <Columns>    
                                                                 
                                   <asp:BoundField DataField="LastModifyUserName" HeaderText="修改人員" 
                                        SortExpression="LastModifyUserName">
                                        <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                                        <HeaderStyle Wrap="False" />
                                    </asp:BoundField>              
                                    <asp:BoundField DataField="LastModifyDate" HeaderText="修改日期" HtmlEncode="false" DataFormatString="{0:yyyy/MM/dd HH:mm}"
                                        SortExpression="LastModifyDate">
                                        <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                                        <HeaderStyle Wrap="False" />
                                    </asp:BoundField>
                                       <asp:BoundField DataField="" HeaderText="修改欄位名稱" 
                                        SortExpression="">
                                        <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                                        <HeaderStyle Wrap="False" />
                                    </asp:BoundField> 
                                </Columns>
                                </asp:GridView>--%>
               <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                    SkinID="GridViewSkin" Width="1200px" AllowSorting="True"     >
            <Columns>              
             
                  <asp:BoundField DataField="ProductID" HeaderText="產品編號" 
                    SortExpression="ProductID" >
                    <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="ProductName" HeaderText="產品名稱" 
                    SortExpression="ProductName" >
                    <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="ProductEName" HeaderText="英文品名" 
                    SortExpression="ProductEName">
                    <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="GroupName" HeaderText="大類" 
                    SortExpression="GroupName" >
                    <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="ClassName" HeaderText="小類" 
                    SortExpression="ClassName" >
                    <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="Barcode1" HeaderText="條碼編號1" 
                    SortExpression="Barcode1">
                    <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="Barcode2" HeaderText="條碼編號2" 
                    SortExpression="Barcode2">
                    <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="Description" HeaderText="產品描述" 
                    SortExpression="Description" >
                    <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                  <asp:BoundField DataField="Memo" HeaderText="備註" 
                    SortExpression="Memo" >
                    <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                 <asp:BoundField DataField="OfficalUrl" HeaderText="產品官方網址" 
                    SortExpression="OfficalUrl" >
                    <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
              
                <asp:BoundField DataField="ServicePhone" HeaderText="客服電話" 
                    SortExpression="ServicePhone">
                    <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="CreateDate" HeaderText="建檔日期" HtmlEncode="false" DataFormatString="{0:yyyy/MM/dd HH:mm}"
                    SortExpression="CreateDate" >
                    <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="CreateUserName" HeaderText="建檔人員" 
                    SortExpression="CreateUserName" >
                    <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="LastModifyDate" HeaderText="異動日期" HtmlEncode="false" DataFormatString="{0:yyyy/MM/dd HH:mm}"
                    SortExpression="LastModifyDate" >
                    <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="LastModifyUserName" HeaderText="異動人員" 
                    SortExpression="LastModifyUserName" >
                    <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                 <asp:BoundField DataField="EP_CheckUserName" HeaderText="企業覆核人員" 
                    SortExpression="EP_CheckUserName" >
                    <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="EP_CheckDate" HeaderText="企業覆核日期" HtmlEncode="false" DataFormatString="{0:yyyy/MM/dd HH:mm}"
                    SortExpression="EP_CheckDate" >
                    <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
               <asp:BoundField DataField="BU_CheckUserName" HeaderText="營運覆核人員" 
                    SortExpression="BU_CheckUserName" >
                    <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>  
                <asp:BoundField DataField="BU_CheckDate" HeaderText="營運覆核日期" HtmlEncode="false" DataFormatString="{0:yyyy/MM/dd HH:mm}"
                    SortExpression="BU_CheckDate">
                    <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
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
