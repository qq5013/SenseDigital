<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProductResumeCompare.aspx.cs" Inherits="WebUI_BusinessUnit_ProductResumeCompare" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>產品生產履歷比對記錄查詢</title>
    <link href="~/Styles/Main.css" type="text/css" rel="stylesheet" /> 
        <link href="~/Styles/op.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="100%">
            <tr>
                <td width="20%" valign="top">
                <table width="100%">
                            <tr>
                                <td style=" font-size:14px;">
                                      <div style="width:100%;height:324px;position:absolute;top:9.5px">
                                        <asp:Literal ID="Literal1" Text="生產履歷編號" runat="server"></asp:Literal>
                                        <div style="width:100%;height:304px;overflow:auto;position:absolute;left:0px;top:20px">
                                    <asp:GridView ID="GridView1" SkinID="GridViewSkin" DataKeyNames="ResumeID" 
                                            runat="server" AutoGenerateColumns="false" 
                                            onselectedindexchanging="GridView1_SelectedIndexChanging" 
                                            onrowdatabound="GridView1_RowDataBound">
                                    <Columns> 
                                        <asp:CommandField HeaderText="選取" ShowSelectButton="True" /> 
                                       <asp:BoundField DataField="RowID" HeaderText="序號" 
                                            SortExpression="RowID">
                                            <ItemStyle HorizontalAlign="Left" Width="4%" Wrap="False" />
                                            <HeaderStyle Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ResumeID" HeaderText="履歷編號" 
                                            SortExpression="ResumeID">
                                            <ItemStyle HorizontalAlign="Left" Width="80%" Wrap="False" />
                                            <HeaderStyle Wrap="False" />
                                        </asp:BoundField>               
                                    </Columns>
                                    </asp:GridView>
                                    </div>
                                    </div>
                                    </td>
                         </tr>
                    </table>
                </td>
                <td >
                  <div style="width:100%;height:327px;overflow:auto;position:absolute;">
                    <table width="100%">
                        <tr>
                            <td style=" font-size:14px;">                           
                                  <asp:Literal ID="Literal2" Text="原始資料" runat="server"></asp:Literal>
                                   <div style="width:100%;height:130px;overflow:auto;">
                                    <asp:GridView ID="GridView2" SkinID="GridViewSkin" runat="server" 
                                          AutoGenerateColumns="false">
                                    <Columns>
                                        <asp:BoundField DataField="ResumeID" HeaderText="履歷編號" 
                                            SortExpression="ResumeID">
                                            <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                                            <HeaderStyle Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ProductID" HeaderText="產品編號" 
                                            SortExpression="ProductID">
                                            <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                                            <HeaderStyle Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ProductName" HeaderText="產品品名" 
                                            SortExpression="ProductName">
                                            <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                                            <HeaderStyle Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ProduceDate" HeaderText="生產日期" HtmlEncode="false" DataFormatString="{0:yyyy/MM/dd HH:mm}"
                                            SortExpression="ProduceDate">
                                            <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                                            <HeaderStyle Wrap="False" />
                                        </asp:BoundField>
                                         <asp:BoundField DataField="HasGarantee" HeaderText="保固期限" 
                                            SortExpression="HasGarantee">
                                            <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                                            <HeaderStyle Wrap="False" />
                                        </asp:BoundField>
                                      <asp:BoundField DataField="GaranteeDate" HeaderText="保固日期" HtmlEncode="false" DataFormatString="{0:yyyy/MM/dd HH:mm}"
                                            SortExpression="GaranteeDate">
                                            <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                                            <HeaderStyle Wrap="False" />
                                        </asp:BoundField>
                                         <asp:BoundField DataField="Description" HeaderText="生產履歷描述" 
                                            SortExpression="Description">
                                            <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                                            <HeaderStyle Wrap="False" />
                                        </asp:BoundField>
                                            <asp:BoundField DataField="ResumeOfficalUrl" HeaderText="生產履歷網址" 
                                            SortExpression="ResumeOfficalUrl">
                                            <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                                            <HeaderStyle Wrap="False" />
                                        </asp:BoundField>
                                            <asp:BoundField DataField="EPResumeID" HeaderText="企業生產履歷編號" 
                                            SortExpression="EPResumeID">
                                            <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                                            <HeaderStyle Wrap="False" />
                                        </asp:BoundField>                  
                                       <asp:BoundField DataField="CreateUserName" HeaderText="建檔人員" 
                                            SortExpression="CreateUserName">
                                            <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                                            <HeaderStyle Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CreateDate" HeaderText="建檔日期" HtmlEncode="false" DataFormatString="{0:yyyy/MM/dd HH:mm}"
                                            SortExpression="CreateDate">
                                            <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                                            <HeaderStyle Wrap="False" />
                                        </asp:BoundField>
                                       <asp:BoundField DataField="LastModifyUserName" HeaderText="異動人員" 
                                            SortExpression="LastModifyUserName">
                                            <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                                            <HeaderStyle Wrap="False" />
                                        </asp:BoundField>              
                                        <asp:BoundField DataField="LastModifyDate" HeaderText="異動日期" HtmlEncode="false" DataFormatString="{0:yyyy/MM/dd HH:mm}"
                                            SortExpression="LastModifyDate">
                                            <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                                            <HeaderStyle Wrap="False" />
                                        </asp:BoundField>
                                         <asp:BoundField DataField="EP_CheckUserName" HeaderText="企業覆核人員" 
                                            SortExpression="EP_CheckUserName">
                                            <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                                            <HeaderStyle Wrap="False" />
                                        </asp:BoundField>              
                                        <asp:BoundField DataField="BU_CheckDate" HeaderText="企業覆核日期" HtmlEncode="false" DataFormatString="{0:yyyy/MM/dd HH:mm}"
                                            SortExpression="BU_CheckDate">
                                            <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                                            <HeaderStyle Wrap="False" />
                                        </asp:BoundField>
               
                                    </Columns>
                                    </asp:GridView>
                                   
                                    </div>
                            </td>
                        </tr>
                        <tr>
                            <td valign="bottom" style=" font-size:14px;">
                            
                                <asp:Literal ID="Literal3" Text="修改後資料" runat="server"></asp:Literal>
                                <div style="width:100%;height:130px;overflow:auto;">
                                <asp:GridView ID="GridView3" SkinID="GridViewSkin" runat="server" AutoGenerateColumns="false">
                                <Columns>
                                    <asp:BoundField DataField="ResumeID" HeaderText="履歷編號" 
                                        SortExpression="ResumeID">
                                        <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                                        <HeaderStyle Wrap="False" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ProductID" HeaderText="產品編號" 
                                        SortExpression="ProductID">
                                        <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                                        <HeaderStyle Wrap="False" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ProductName" HeaderText="產品品名" 
                                        SortExpression="ProductName">
                                        <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                                        <HeaderStyle Wrap="False" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ProduceDate" HeaderText="生產日期" HtmlEncode="false" DataFormatString="{0:yyyy/MM/dd HH:mm}"
                                        SortExpression="ProduceDate">
                                        <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                                        <HeaderStyle Wrap="False" />
                                    </asp:BoundField>
                                     <asp:BoundField DataField="HasGarantee" HeaderText="保固期限" 
                                        SortExpression="HasGarantee">
                                        <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                                        <HeaderStyle Wrap="False" />
                                    </asp:BoundField>
                                  <asp:BoundField DataField="GaranteeDate" HeaderText="保固日期" HtmlEncode="false" DataFormatString="{0:yyyy/MM/dd HH:mm}"
                                        SortExpression="GaranteeDate">
                                        <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                                        <HeaderStyle Wrap="False" />
                                    </asp:BoundField>
                                     <asp:BoundField DataField="Description" HeaderText="生產履歷描述" 
                                        SortExpression="Description">
                                        <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                                        <HeaderStyle Wrap="False" />
                                    </asp:BoundField>
                                        <asp:BoundField DataField="ResumeOfficalUrl" HeaderText="生產履歷網址" 
                                        SortExpression="ResumeOfficalUrl">
                                        <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                                        <HeaderStyle Wrap="False" />
                                    </asp:BoundField>
                                        <asp:BoundField DataField="EPResumeID" HeaderText="企業生產履歷編號" 
                                        SortExpression="EPResumeID">
                                        <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                                        <HeaderStyle Wrap="False" />
                                    </asp:BoundField>                  
                                   <asp:BoundField DataField="CreateUserName" HeaderText="建檔人員" 
                                        SortExpression="CreateUserName">
                                        <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                                        <HeaderStyle Wrap="False" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CreateDate" HeaderText="建檔日期" HtmlEncode="false" DataFormatString="{0:yyyy/MM/dd HH:mm}"
                                        SortExpression="CreateDate">
                                        <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                                        <HeaderStyle Wrap="False" />
                                    </asp:BoundField>
                                   <asp:BoundField DataField="LastModifyUserName" HeaderText="異動人員" 
                                        SortExpression="LastModifyUserName">
                                        <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                                        <HeaderStyle Wrap="False" />
                                    </asp:BoundField>              
                                    <asp:BoundField DataField="LastModifyDate" HeaderText="異動日期" HtmlEncode="false" DataFormatString="{0:yyyy/MM/dd HH:mm}"
                                        SortExpression="LastModifyDate">
                                        <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                                        <HeaderStyle Wrap="False" />
                                    </asp:BoundField>
                                     <asp:BoundField DataField="EP_CheckUserName" HeaderText="企業覆核人員" 
                                        SortExpression="EP_CheckUserName">
                                        <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                                        <HeaderStyle Wrap="False" />
                                    </asp:BoundField>              
                                    <asp:BoundField DataField="BU_CheckDate" HeaderText="企業覆核日期" HtmlEncode="false" DataFormatString="{0:yyyy/MM/dd HH:mm}"
                                        SortExpression="BU_CheckDate">
                                        <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                                        <HeaderStyle Wrap="False" />
                                    </asp:BoundField>
               
                                </Columns>
                                </asp:GridView>
                                
                                </div>
                            </td>
                        </tr>
                    </table>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
