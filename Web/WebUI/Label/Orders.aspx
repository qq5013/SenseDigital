<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Orders.aspx.cs" Inherits="WebUI_Label_Orders" %>

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
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <asp:UpdateProgress ID="updateprogress" runat="server" AssociatedUpdatePanelID="updatePanel">
        <ProgressTemplate>
            <div id="progressBackgroundFilter" style="display: none">
            </div>
            <div id="processMessage">
                Loading...<br />
                <br />
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
                            <asp:Button ID="btnQuery" runat="server" Text="列印" CssClass="ButtonPrint" meta:resourcekey="btnQueryResource2"
                                OnClientClick="return print();" />
                            <asp:Button ID="btnAdd" runat="server" Text="新增" OnClientClick="Edit(false);" CssClass="ButtonCreate"
                                meta:resourcekey="btnCreateResource2" />
                            <asp:Button ID="btnDelete" runat="server" Text="刪除" CssClass="ButtonDel" OnClick="btnDeletet_Click"
                                meta:resourcekey="btnDeleteResource2" />
                            <asp:Button ID="btnExit" runat="server" Text="離開" CssClass="ButtonExit" OnClientClick="return Exit()"
                                meta:resourcekey="btnExitResource2" />
                        </td>
                    </tr>
                </table>
                <table class="maintable" cellspacing="0" cellpadding="0" width="100%" align="center"
                    border="1">
                    <tr>
                        <td class="smalltitle" align="center" width="6%">
                            <asp:Literal ID="Literal1" Text="企業編號" runat="server"></asp:Literal>
                        </td>
                        <td width="15%">
                            &nbsp;
                            <asp:DropDownList ID="ddlEnterpriseID" runat="server" Width="90%" AutoPostBack="True"
                                OnSelectedIndexChanged="ddlEnterpriseID_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td class="smalltitle" align="center" width="7%">
                            <asp:Literal ID="ltlField" Text="查詢欄位" runat="server"></asp:Literal>
                        </td>
                        <td width="15%" height="20">
                            &nbsp;
                            <asp:DropDownList ID="ddlFieldName" runat="server" Width="90%" meta:resourcekey="ddlFieldNameResource2">
                            </asp:DropDownList>
                        </td>
                        <td class="smalltitle" align="center" width="7%">
                            <asp:Literal ID="ltlContent" Text="查詢內容" runat="server"></asp:Literal>
                        </td>
                        <td width="50%" height="20" valign="middle">
                            &nbsp;<asp:TextBox ID="txtContent" TabIndex="1" runat="server" Width="70%" CssClass="TextBox"
                                MaxLength="100" heigth="16px" meta:resourcekey="txtContentResource2"></asp:TextBox>
                            &nbsp;<asp:Button ID="btnSearch" TabIndex="2" runat="server" Width="60px" CssClass="but"
                                Text="立即查詢" OnClientClick="Search()" meta:resourcekey="btnSearchResource2" OnClick="btnSearch_Click">
                            </asp:Button>
                            &nbsp;<asp:Button ID="btnMultiSearch" runat="server" CssClass="but" TabIndex="3"
                                Text="多條件查詢" OnClientClick="SearchDialog(300)" Width="80px" meta:resourcekey="btnMultiSearchResource2"
                                OnClick="btnMultiSearch_Click" />
                        </td>
                    </tr>
                </table>
            </div>
            <div id="table-container" style="overflow: auto; width: 100%; height: 400px">
                <asp:HiddenField ID="HiddenField1" runat="server" />
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" SkinID="GridViewSkin"
                    Width="1200px" AllowSorting="True" OnSorting="GridView1_Sorting" meta:resourcekey="GridView1Resource2"
                    OnRowDataBound="GridView1_RowDataBound">
                    <Columns>
                        <asp:TemplateField meta:resourcekey="TemplateFieldResource3">
                            <HeaderTemplate>
                                <input type="checkbox" onclick="javascript:selectAll('GridView1',this.checked);" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="cbSelect" runat="server" meta:resourcekey="cbSelectResource2">
                                </asp:CheckBox>
                            </ItemTemplate>
                            <HeaderStyle Width="60px"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="單號" SortExpression="BillID">
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# "OrderView.aspx?FormID=" + FormID + "&ID="+DataBinder.Eval(Container.DataItem, "BillID") %>'
                                    Text='<%# DataBinder.Eval(Container.DataItem, "BillID")%>'></asp:HyperLink>
                            </ItemTemplate>
                            <ItemStyle Width="12%" Wrap="False" />
                            <HeaderStyle Width="12%" Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="日期" SortExpression="BillDate">
                            <ItemTemplate>
                                <%# ToYMD(DataBinder.Eval(Container.DataItem, "BillDate"))%>
                            </ItemTemplate>
                            <HeaderStyle Wrap="False" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="CustomerPO" HeaderText="客戶PO" SortExpression="CustomerPO">
                            <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                            <HeaderStyle Wrap="False" />
                        </asp:BoundField>
                        <asp:BoundField DataField="EnterpriseID" HeaderText="企業編號" SortExpression="EnterpriseID">
                            <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                            <HeaderStyle Wrap="False" />
                        </asp:BoundField>
                        <asp:BoundField DataField="EnterpriseName" HeaderText="企業名稱" SortExpression="EnterpriseName">
                            <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                            <HeaderStyle Wrap="False" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Contact" HeaderText="連絡人員" SortExpression="Contact">
                            <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                            <HeaderStyle Wrap="False" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ContactPhone" HeaderText="聯絡電話" SortExpression="ContactPhone">
                            <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                            <HeaderStyle Wrap="False" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Fax" HeaderText="傳真號碼" SortExpression="Fax">
                            <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                            <HeaderStyle Wrap="False" />
                        </asp:BoundField>
                        <asp:BoundField DataField="DeliverCountry" HeaderText="送貨地址國別" SortExpression="DeliverCountry">
                            <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                            <HeaderStyle Wrap="False" />
                        </asp:BoundField>
                        <asp:BoundField DataField="DeliverAddress" HeaderText="送貨地址" SortExpression="DeliverAddress">
                            <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                            <HeaderStyle Wrap="False" />
                        </asp:BoundField>
                        <asp:BoundField DataField="DeliverMehtod" HeaderText="交貨方式" SortExpression="DeliverMehtod">
                            <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                            <HeaderStyle Wrap="False" />
                        </asp:BoundField>
                        <asp:BoundField DataField="BusPersonID" HeaderText="業務員編號" SortExpression="BusPersonID">
                            <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                            <HeaderStyle Wrap="False" />
                        </asp:BoundField>
                        <%--<asp:BoundField DataField="BusPersonName" HeaderText="業務員姓名" SortExpression="BusPersonName">
                            <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                            <HeaderStyle Wrap="False" />
                        </asp:BoundField>--%>
                        <asp:BoundField DataField="BillState" HeaderText="單況" SortExpression="BillState">
                            <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                            <HeaderStyle Wrap="False" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="張數合計" SortExpression="TotalPages">
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "TotalPages", "{0:N0}")%>
                            </ItemTemplate>
                            <HeaderStyle Wrap="False" />
                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="建檔日期" SortExpression="CreateDate">
                            <ItemTemplate>
                                <%# ToYMD(DataBinder.Eval(Container.DataItem, "CreateDate"))%>
                            </ItemTemplate>
                            <HeaderStyle Wrap="False" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="CreateUserName" HeaderText="建檔人員" SortExpression="CreateUserName">
                            <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                            <HeaderStyle Wrap="False" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="異動日期" SortExpression="LastModifyDate">
                            <ItemTemplate>
                                <%# ToYMD(DataBinder.Eval(Container.DataItem, "LastModifyDate"))%>
                            </ItemTemplate>
                            <HeaderStyle Wrap="False" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="LastModifyUserName" HeaderText="(異動人員)" SortExpression="LastModifyUserName">
                            <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                            <HeaderStyle Wrap="False" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="營運覆核日期" SortExpression="CheckDate">
                            <ItemTemplate>
                                <%# ToYMD(DataBinder.Eval(Container.DataItem, "CheckDate"))%>
                            </ItemTemplate>
                            <HeaderStyle Wrap="False" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="CheckUserName" HeaderText="營運覆核人員" SortExpression="BU_CheckUserName">
                            <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                            <HeaderStyle Wrap="False" />
                        </asp:BoundField>
                    </Columns>
                    <PagerSettings Visible="False" />
                </asp:GridView>
            </div>
            <div>
                <asp:LinkButton ID="btnFirst" runat="server" OnClick="btnFirst_Click" meta:resourcekey="btnFirstResource2"
                    Text="首頁"></asp:LinkButton>
                &nbsp;<asp:LinkButton ID="btnPre" runat="server" OnClick="btnPre_Click" meta:resourcekey="btnPreResource2"
                    Text="上一頁"></asp:LinkButton>
                &nbsp;<asp:Label ID="lblCurrentPage" runat="server" meta:resourcekey="lblCurrentPageResource2"></asp:Label>
                &nbsp;<asp:LinkButton ID="btnNext" runat="server" OnClick="btnNext_Click" meta:resourcekey="btnNextResource2"
                    Text="下一頁"></asp:LinkButton>
                &nbsp;<asp:LinkButton ID="btnLast" runat="server" OnClick="btnLast_Click" meta:resourcekey="btnLastResource2"
                    Text="尾页"></asp:LinkButton>
                &nbsp;<asp:TextBox ID="txtPageNo" onkeypress="return regInput(this,/^\d+$/,String.fromCharCode(event.keyCode))"
                    onpaste="return regInput(this,/^\d+$/,window.clipboardData.getData('Text'))"
                    ondrop="return regInput(this,/^\d+$/,event.dataTransfer.getData('Text'))" runat="server"
                    Width="56px" CssClass="Textread" ReadOnly="true" meta:resourcekey="txtPageNoResource2"></asp:TextBox>&nbsp;<asp:LinkButton
                        ID="btnToPage" runat="server" OnClick="btnToPage_Click" meta:resourcekey="btnToPageResource2"
                        Text="跳轉"></asp:LinkButton>
                &nbsp;
                <asp:DropDownList ID="ddlPageSize" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged"
                    meta:resourcekey="ddlPageSizeResource1">
                </asp:DropDownList>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:Literal ID="Literal3" runat="server" Visible="false" Text="有效"></asp:Literal>
    <asp:Literal ID="Literal4" runat="server" Visible="false" Text="結案"></asp:Literal>
    <asp:Literal ID="Literal2" runat="server" Visible="false" Text="無效"></asp:Literal>
    </form>
</body>
</html>
