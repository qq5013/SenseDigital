<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InvalidLoad.aspx.cs" Inherits="WebUI_Label_InvaildLoad" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>入庫壞品清單轉入作廢</title>
      <link href="~/Styles/Main.css" type="text/css" rel="stylesheet" /> 
        <link href="~/Styles/op.css" type="text/css" rel="stylesheet" />      
        <link href="~/Styles/Detail.css" type="text/css" rel="stylesheet" /> 
        <link href="~/Styles/superTables.css" type="text/css" rel="stylesheet" /> 
        <script type="text/javascript" src='<%=ResolveUrl("~/Scripts/JQuery/jquery-1.8.2.min.js") %>'></script> 	  
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Resize.js") %>'></script>  
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Commons.js") %>'></script>
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Ajax.js") %>'></script>
            <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Detail.js") %>'></script>
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/superTables.js") %>'></script>
         <script type="text/javascript" src="Js/InvalidLoad.js"></script>
             <script type="text/javascript">                
                 $(document).ready(function () {                    
                     subData[0] = jQuery.parseJSON($('#HdnSubDetail1').val());
                     initialTable();
                     AddToDetail();
                 });
               
        </script>
    <style type="text/css">
        .style1
        {
            height: 31px;
        }
        .style2
        {
            height: 21px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
     <asp:ScriptManager ID="ScriptManager1" runat="server" />  
      <div>
         <table width="100%">
          <tr>
           <td width="90%" height="70px" align="center">
             <fieldset style="height:66px;width:95%">
              <legend>載入條件區間</legend>
               <table width="100%">
                
                  <tr>
                  <td width="10%" align="center" class="style1">
                    <asp:Literal ID="Literal1" Text="入庫單號" runat="server"></asp:Literal>
                  </td>
                  <td width="20%" class="style1">
                      <asp:textbox id="Textbox2" runat="server" CssClass="Textbox"   Width="90%" MaxLength="20"></asp:textbox>
                  </td>
                   <td width="3%" class="style1">
                    ~
                  </td>
                   <td width="20%" class="style1">
                     <asp:textbox id="Textbox3" runat="server" CssClass="Textbox"   Width="90%" MaxLength="20"></asp:textbox>
                  </td>
                   <td width="5%" class="style1">
                        <asp:button id="Button1" runat="server" Text="指定"  CssClass="but" Width="70px" ></asp:button>
                  </td>
                 </tr>
                  <tr>
                  <td width="10%" align="center" class="style2">
                    <asp:Literal ID="Literal2" Text="款式編號" runat="server"></asp:Literal>
                  </td>
                  <td width="20%" class="style2">
                      <asp:textbox id="Textbox4" runat="server" CssClass="Textbox"   Width="90%" MaxLength="20"></asp:textbox>
                  </td>
                   <td width="3%" class="style2">
                    ~
                  </td>
                   <td width="20%" class="style2">
                     <asp:textbox id="Textbox5" runat="server" CssClass="Textbox"   Width="90%" MaxLength="20"></asp:textbox>
                  </td>
                   <td width="5%" class="style2">
                        <asp:button id="Button2" runat="server" Text="指定"  CssClass="but" Width="70px" ></asp:button>
                  </td>
                 </tr>
               </table>
             </fieldset>
           </td>
           <td>
             <table width="100%">
               <tr>
                 <td height="26px">
                      <asp:button id="Button3" runat="server" Text="納入"  CssClass="but" Width="70px" ></asp:button>
                 </td>
               </tr>
                 <tr>
                 <td height="26px">
                     <asp:button id="Button4" runat="server" Text="取回"  CssClass="but" Width="70px" ></asp:button>
                 </td>
               </tr>
                 <tr>
                 <td height="26px">
                     <asp:button id="Button5" runat="server" Text="放棄"  CssClass="but" Width="70px" ></asp:button>
                 </td>
               </tr>
             </table>
           </td>
          </tr>
          <tr>
           <td colspan="2">
              <div class="fakeContainer" id="div_Detail1" style=" margin:5px;height:205px;">
               </div>
          <%--<div id="table-container" style="overflow: auto; WIDTH: 100%; HEIGHT: 400px">
                <asp:HiddenField ID="HiddenField1" runat="server" />
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                    SkinID="GridViewSkin" Width="1200px" AllowSorting="True"  
                  >
            <Columns>
                <asp:TemplateField meta:resourcekey="TemplateFieldResource3">
                    <HeaderTemplate>
                    <input type="checkbox" onclick="javascript:selectAll('GridView1',this.checked);" />                    
                    </HeaderTemplate>
                    <ItemTemplate>
                    <asp:CheckBox id="cbSelect" runat="server"></asp:CheckBox>                   
                    </ItemTemplate>
                  <HeaderStyle Width="30px"></HeaderStyle>
                 <ItemStyle Width="30px"></ItemStyle>
               </asp:TemplateField>             
                <asp:BoundField DataField="SubID" HeaderText="(序號)" 
                    SortExpression="SubID">
                    <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="BillID" HeaderText="(入庫單號)" 
                    SortExpression="BillID">
                    <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="OrderBillID" HeaderText="(訂單單號)" 
                    SortExpression="OrderBillID">
                    <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                 <asp:BoundField DataField="StyleID" HeaderText="(款式編號)" 
                    SortExpression="StyleID">
                    <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                 <asp:BoundField DataField="QtyTotal" HeaderText="(標籤模式)" 
                    SortExpression="QtyTotal">
                    <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                 <asp:BoundField DataField="LabelReelNo" HeaderText="(標籤卷編號)" 
                    SortExpression="LabelReelNo">
                    <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                   <asp:BoundField DataField="StartNo" HeaderText="(起始序號)" 
                    SortExpression="StartNo">
                    <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                   <asp:BoundField DataField="EndNo" HeaderText="(終止序號)" 
                    SortExpression="EndNo">
                    <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                  <asp:BoundField DataField="Quantity" HeaderText="(壞品張數)" 
                    SortExpression="Quantity">
                    <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>                           
              
            </Columns>
            <PagerSettings Visible="False" />
        </asp:GridView>
        </div>--%> 
           </td>
          </tr>
         </table>
            
        </div>

    </form>
</body>
</html>
