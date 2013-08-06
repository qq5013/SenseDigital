<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StockIns.aspx.cs" Inherits="WebUI_Label_InStocks" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
  
    <title></title> 
    <link href="~/Styles/Main.css" type="text/css" rel="stylesheet" /> 
    <link href="~/Styles/op.css" type="text/css" rel="stylesheet" /> 
    <script type="text/javascript" src='<%=ResolveUrl("~/Scripts/JQuery/jquery-1.8.2.min.js") %>'></script>
    <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Commons.js") %>'></script>
    <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Resize.js") %>'></script>  
 
    <script type='text/javascript'>
        
     
    </script>
</head>
<body >
    <form id="form1" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server" />  
    <asp:UpdateProgress ID="updateprogress" runat="server" AssociatedUpdatePanelID="updatePanel">
    <ProgressTemplate>            
             <div id="progressBackgroundFilter" style="display:none"></div>
        <div id="processMessage"> Loading...<br /><br />
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
                            <asp:Button ID="btnQuery" runat="server" Text="列印" CssClass="ButtonPrint" 
                                meta:resourcekey="btnQueryResource2" />
                            <asp:Button ID="btnAdd" runat="server" Text="新增" OnClientClick="Edit(false);" CssClass="ButtonCreate"                            
                                meta:resourcekey="btnCreateResource2" />
                            <asp:Button ID="btnDelete" runat="server" Text="刪除" CssClass="ButtonDel" 
                                onclick="btnDeletet_Click" meta:resourcekey="btnDeleteResource2"/>
                            <asp:Button ID="btnExit" runat="server" Text="離開" CssClass="ButtonExit" OnClientClick="Exit()" 
                                meta:resourcekey="btnExitResource2" />
                        </td>
                    </tr>
                </table>
                <table class="maintable" cellspacing="0" cellpadding="0" width="100%" align="center" border="1" >
					    <tr> 
                            <td class="smalltitle" align="center" width="6%">
                                <asp:Literal ID="Literal1" Text="企業編號" runat="server"></asp:Literal>
                            </td>
                            <td width="15%">
                                &nbsp;
                                <asp:dropdownlist id="ddlEnterpriseID" runat="server" Width="90%" 
                                    AutoPostBack="True" 
                                    onselectedindexchanged="ddlEnterpriseID_SelectedIndexChanged" ></asp:dropdownlist>
                            </td>
						    <td class="smalltitle" align="center" width="7%" ><asp:Literal ID="ltlField" Text="查詢欄位"
                                    runat="server"></asp:Literal></td>
						    <td  width="15%" height="20">&nbsp;
							    <asp:dropdownlist id="ddlFieldName" runat="server" Width="90%" 
                                    meta:resourcekey="ddlFieldNameResource2"></asp:dropdownlist></td>
						    <td class="smalltitle" align="center" width="7%"><asp:Literal ID="ltlContent" Text="查詢內容"
                                    runat="server"></asp:Literal>
                            </td>
						    <td  width="50%" height="20" valign="middle">&nbsp;<asp:textbox id="txtContent" 
                                    tabIndex="1" runat="server" Width="70%" CssClass="TextBox" MaxLength="100" 
                                    heigth="16px" meta:resourcekey="txtContentResource2"></asp:textbox>
                                &nbsp;<asp:button id="btnSearch" tabIndex="2" runat="server" Width="60px" 
                                    CssClass="but" Text="立即查詢" OnClientClick="Search()" meta:resourcekey="btnSearchResource2" 
                                    onclick="btnSearch_Click"></asp:button>
                                &nbsp;<asp:Button ID="btnMultiSearch" runat="server" CssClass="but" tabIndex="3" 
                                    Text="多條件查詢" OnClientClick="SearchDialog(300)" Width="80px" 
                                    meta:resourcekey="btnMultiSearchResource2" onclick="btnMultiSearch_Click" />
                            </td>
					    </tr>
			    </table>
                </div>

            <div id="table-container" style="overflow: auto; WIDTH: 100%; HEIGHT: 400px">
                <asp:HiddenField ID="HiddenField1" runat="server" />
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                    SkinID="GridViewSkin" Width="1200px" AllowSorting="True"  
                    OnSorting="GridView1_Sorting" 
                    meta:resourcekey="GridView1Resource2" 
                    onrowdatabound="GridView1_RowDataBound">
            <Columns>
                <asp:TemplateField meta:resourcekey="TemplateFieldResource3">
                    <HeaderTemplate>
                    <input type="checkbox" onclick="javascript:selectAll('GridView1',this.checked);" />                    
                    </HeaderTemplate>
                    <ItemTemplate>
                    <asp:CheckBox id="cbSelect" runat="server" meta:resourcekey="cbSelectResource2"></asp:CheckBox>                   
                    </ItemTemplate>
                  <HeaderStyle Width="60px"></HeaderStyle>
                 <ItemStyle  HorizontalAlign="Center" Width="60px"></ItemStyle>
               </asp:TemplateField>
                <asp:TemplateField HeaderText="入庫單號"  SortExpression="BillID" 
                    meta:resourcekey="TemplateFieldResource4">
                    <ItemTemplate>
                    <asp:HyperLink id="HyperLink1" runat="server"                             
                            NavigateUrl='<%# "StockInView.aspx?FormID=" + FormID + "&ID="+DataBinder.Eval(Container.DataItem, "BillID") %>' Text='<%# DataBinder.Eval(Container.DataItem, "BillID")%>'></asp:HyperLink>                       
                    </ItemTemplate>
                  <ItemStyle Width="12%" Wrap="False" />
                  <HeaderStyle Width="12%" Wrap="False" />
               </asp:TemplateField>            

<asp:BoundField DataField="BillDate" HeaderText="入庫日期" HtmlEncode="false" DataFormatString="{0:yyyy/MM/dd}" SortExpression="BillDate" > 
<ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" /> <HeaderStyle Wrap="False" /> 
</asp:BoundField> 
<asp:BoundField DataField="EnterpriseID" HeaderText="企業編號" SortExpression="EnterpriseID" > 
<ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" /> <HeaderStyle Wrap="False" /> 
</asp:BoundField>  
<asp:BoundField DataField="EnterpriseName" HeaderText="企業名稱" SortExpression="EnterpriseName" > 
<ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" /> <HeaderStyle Wrap="False" /> 
</asp:BoundField>  
<asp:BoundField DataField="LabelFrom" HeaderText="生產方式" SortExpression="LabelFrom"> 
<ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" /> <HeaderStyle Wrap="False" /> 
</asp:BoundField> 
<asp:BoundField DataField="StyleID" HeaderText="生產款式" SortExpression="StyleID" > 
<ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" /> <HeaderStyle Wrap="False" /> 
</asp:BoundField>  
<asp:BoundField DataField="LabelMode" HeaderText="標籤模式" SortExpression="LabelMode" > 
<ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" /> <HeaderStyle Wrap="False" /> 
</asp:BoundField> 
<%--<asp:BoundField DataField="RemainQty" HeaderText="預計生產量" SortExpression="RemainQty" > 
<ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" /> <HeaderStyle Wrap="False" /> 
</asp:BoundField>  --%>
 <asp:TemplateField HeaderText="預計生產量" SortExpression="RemainQty" >
                    <ItemTemplate> 
                        <%# DataBinder.Eval(Container.DataItem, "RemainQty", "{0:N0}")%>
                    </ItemTemplate> 
                    <HeaderStyle Wrap="False" />
                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" />
                </asp:TemplateField>
<%--<asp:BoundField DataField="RemainQty" HeaderText="保留數量" SortExpression="RemainQty" > 
<ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" /> <HeaderStyle Wrap="False" /> 
</asp:BoundField>  --%>
<asp:BoundField DataField="RemainStartNo" HeaderText="保留起始號" SortExpression="RemainStartNo" > 
<ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" /> <HeaderStyle Wrap="False" /> 
</asp:BoundField>  
<asp:BoundField DataField="RemainEndNo" HeaderText="保留終止號" SortExpression="RemainEndNo" > 
<ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" /> <HeaderStyle Wrap="False" /> 
</asp:BoundField>  
<asp:BoundField DataField="BillState" HeaderText="單況" SortExpression="BillState" > 
<ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" /> <HeaderStyle Wrap="False" /> 
</asp:BoundField> 
<%--<asp:BoundField DataField="QtyTotal" HeaderText="入庫總數量合計" SortExpression="QtyTotal" > 
<ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" /> <HeaderStyle Wrap="False" /> 
</asp:BoundField>  --%>
 <asp:TemplateField HeaderText="入庫總數量合計" SortExpression="QtyTotal" >
                    <ItemTemplate> 
                        <%# DataBinder.Eval(Container.DataItem, "QtyTotal", "{0:N0}")%>
                    </ItemTemplate> 
                    <HeaderStyle Wrap="False" />
                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" />
                </asp:TemplateField>
<asp:BoundField DataField="OrderBillID" HeaderText="訂單單號" SortExpression="OrderBillID" > 
<ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" /> <HeaderStyle Wrap="False" /> 
</asp:BoundField>  
 <asp:BoundField DataField="ProduceBillID" HeaderText="生產單號" SortExpression="ProduceBillID" > 
<ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" /> <HeaderStyle Wrap="False" /> 
</asp:BoundField>  
 
<%--<asp:BoundField DataField="RemainQty" HeaderText="訂購數量" SortExpression="RemainQty" > 
<ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" /> <HeaderStyle Wrap="False" /> 
</asp:BoundField>  
<asp:BoundField DataField="RemainQty" HeaderText="備品數量" SortExpression="RemainQty" > 
<ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" /> <HeaderStyle Wrap="False" /> 
</asp:BoundField>  

<asp:BoundField DataField="QtyTotal" HeaderText="尾數備品調減" SortExpression="QtyTotal" > 
<ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" /> <HeaderStyle Wrap="False" /> 
</asp:BoundField>  
<asp:BoundField DataField="QtyTotal" HeaderText="差異數量" SortExpression="QtyTotal" > 
<ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" /> <HeaderStyle Wrap="False" /> 
</asp:BoundField>  
<asp:BoundField DataField="QtyTotal" HeaderText="壞品數量合計" SortExpression="QtyTotal" > 
<ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" /> <HeaderStyle Wrap="False" /> 
</asp:BoundField>  
<asp:BoundField DataField="LabelReels" HeaderText="匯入圖檔數量" SortExpression="LabelReels" > 
<ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" /> <HeaderStyle Wrap="False" /> 
</asp:BoundField>  
<asp:CheckBoxField DataField="IsNoDone" HeaderText="序號產生" SortExpression="IsNoDone"> 
<ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" /> <HeaderStyle Wrap="False" /> 
</asp:CheckBoxField> 
<asp:CheckBoxField DataField="IsCheckImage" HeaderText="圖檔檢查" SortExpression="IsCheckImage"> 
<ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" /> <HeaderStyle Wrap="False" /> 
</asp:CheckBoxField> 
<asp:CheckBoxField DataField="IsConfirmBad" HeaderText="壞品確認" SortExpression="IsConfirmBad"> 
<ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" /> <HeaderStyle Wrap="False" /> 
</asp:CheckBoxField> 
<asp:CheckBoxField DataField="IsImportImage" HeaderText="圖檔匯入" SortExpression="IsImportImage"> 
<ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" /> <HeaderStyle Wrap="False" /> 
</asp:CheckBoxField> 
<asp:CheckBoxField DataField="IsInvalidBad" HeaderText="壞品作廢" SortExpression="IsInvalidBad"> 
<ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" /> <HeaderStyle Wrap="False" /> 
</asp:CheckBoxField> 
<asp:BoundField DataField="InvalidBillID" HeaderText="作廢單號" SortExpression="InvalidBillID" > 
<ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" /> <HeaderStyle Wrap="False" /> 
</asp:BoundField>  --%>
 
<%--<asp:BoundField DataField="Memo" HeaderText="備註" SortExpression="Memo" > 
<ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" /> <HeaderStyle Wrap="False" /> 
</asp:BoundField>  
<asp:BoundField DataField="CreateDate" HeaderText="建檔日期" HtmlEncode="false" DataFormatString="{0:yyyy/MM/dd HH:mm}" SortExpression="CreateDate" > 
<ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" /> <HeaderStyle Wrap="False" /> 
</asp:BoundField> 
<asp:BoundField DataField="CreateUserName" HeaderText="建檔人員" SortExpression="CreateUserName" > 
<ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" /> <HeaderStyle Wrap="False" /> 
</asp:BoundField>  
<asp:BoundField DataField="LastModifyDate" HeaderText="異動日期" HtmlEncode="false" DataFormatString="{0:yyyy/MM/dd HH:mm}" SortExpression="LastModifyDate" > 
<ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" /> <HeaderStyle Wrap="False" /> 
</asp:BoundField> --%>
<asp:BoundField DataField="EP_CheckDate" HeaderText="企業覆核日期" HtmlEncode="false" DataFormatString="{0:yyyy/MM/dd HH:mm}" SortExpression="EP_CheckDate" > 
<ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" /> <HeaderStyle Wrap="False" /> 
</asp:BoundField> 
<asp:BoundField DataField="EP_CheckUserName" HeaderText="企業覆核" SortExpression="EP_CheckUserName" > 
<ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" /> <HeaderStyle Wrap="False" /> 
</asp:BoundField>  
<asp:BoundField DataField="BU_CheckDate" HeaderText="營運覆核日期" HtmlEncode="false" DataFormatString="{0:yyyy/MM/dd HH:mm}" SortExpression="BU_CheckDate" > 
<ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" /> <HeaderStyle Wrap="False" /> 
</asp:BoundField> 
<asp:BoundField DataField="BU_CheckUserName" HeaderText="營運覆核" SortExpression="BU_CheckUserName" > 
<ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" /> <HeaderStyle Wrap="False" /> 
      </asp:BoundField>       
            </Columns>
            <PagerSettings Visible="False" />
        </asp:GridView>
        </div>
            <div>
                <asp:LinkButton ID="btnFirst" runat="server" OnClick="btnFirst_Click" 
                        meta:resourcekey="btnFirstResource2" Text="首頁"></asp:LinkButton> 
                &nbsp;<asp:LinkButton ID="btnPre" runat="server" OnClick="btnPre_Click" 
                        meta:resourcekey="btnPreResource2" Text="上一頁"></asp:LinkButton> 
                &nbsp;<asp:Label ID="lblCurrentPage" runat="server" 
                        meta:resourcekey="lblCurrentPageResource2"></asp:Label> 
                &nbsp;<asp:LinkButton ID="btnNext" runat="server" OnClick="btnNext_Click" 
                        meta:resourcekey="btnNextResource2" Text="下一頁"></asp:LinkButton> 
                &nbsp;<asp:LinkButton ID="btnLast" runat="server" OnClick="btnLast_Click" 
                        meta:resourcekey="btnLastResource2" Text="尾页"></asp:LinkButton> 
       &nbsp;<asp:textbox id="txtPageNo" onkeypress="return regInput(this,/^\d+$/,String.fromCharCode(event.keyCode))"
					    onpaste="return regInput(this,/^\d+$/,window.clipboardData.getData('Text'))" ondrop="return regInput(this,/^\d+$/,event.dataTransfer.getData('Text'))"
					    runat="server" Width="56px" CssClass="TextBox" meta:resourcekey="txtPageNoResource2"></asp:textbox>&nbsp;<asp:linkbutton 
                        id="btnToPage" runat="server" onclick="btnToPage_Click" 
                        meta:resourcekey="btnToPageResource2" Text="跳轉"></asp:linkbutton>
                    &nbsp;
                    <asp:DropDownList ID="ddlPageSize" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="ddlPageSize_SelectedIndexChanged" 
                        meta:resourcekey="ddlPageSizeResource1">                    
                    </asp:DropDownList>
            </div>
       </ContentTemplate>
            </asp:UpdatePanel>
            <asp:Literal ID="Literal22" runat="server" Visible="false" Text="營運生產"></asp:Literal>
            <asp:Literal ID="Literal2" runat="server" Visible="false" Text="企業自製"></asp:Literal>
             <asp:Literal ID="Literal3" runat="server" Visible="false" Text="空白"></asp:Literal>
              <asp:Literal ID="Literal4" runat="server" Visible="false" Text="序號產出中"></asp:Literal>
               <asp:Literal ID="Literal5" runat="server" Visible="false" Text="序號已產出"></asp:Literal>
                <asp:Literal ID="Literal6" runat="server" Visible="false" Text="圖檔檢查中"></asp:Literal>
                 <asp:Literal ID="Literal7" runat="server" Visible="false" Text="圖檔已檢查"></asp:Literal>
                  <asp:Literal ID="Literal8" runat="server" Visible="false" Text="圖檔匯入中"></asp:Literal>
                   <asp:Literal ID="Literal9" runat="server" Visible="false" Text="圖檔已匯入"></asp:Literal>
                    <asp:Literal ID="Literal10" runat="server" Visible="false" Text="壞品未作廢"></asp:Literal>
                    <asp:Literal ID="Literal11" runat="server" Visible="false" Text="入庫結案"></asp:Literal>
    </form>
</body>
</html>

