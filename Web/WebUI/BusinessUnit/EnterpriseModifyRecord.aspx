<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EnterpriseModifyRecord.aspx.cs" Inherits="WebUI_BusinessUnit_EnterpriseModifyRecord" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>標籤異動記錄</title>
    <link href="~/Styles/Main.css" type="text/css" rel="stylesheet" />      
        
</head>
<body>
    <form id="form1" runat="server">
    <div>      
        <table width="100%">                    
                        <tr> 
                            <td valign="bottom" style=" font-size:14px;">                            
                               <div style="width:100%;height:200px;overflow:auto;">

               <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                    SkinID="GridViewSkin" Width="100%" AllowSorting="True"     >
            <Columns>              
             
                <asp:BoundField DataField="ModifyDate" HeaderText="異動日期" HtmlEncode="false" DataFormatString="{0:yyyy/MM/dd HH:mm}" >
                    <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="ServiceYears" HeaderText="使用年限" >
                    <ItemStyle HorizontalAlign="right" Width="8%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                 <asp:BoundField DataField="EnableMonths" HeaderText="啟用期限" >
                    <ItemStyle HorizontalAlign="right" Width="8%" Wrap="False" />
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