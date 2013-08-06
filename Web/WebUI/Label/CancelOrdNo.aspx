<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CancelOrdNo.aspx.cs" Inherits="WebUI_Label_CancelOrdNo" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <title>取消訂購數量作業</title>
    <link href="~/Styles/Main.css" type="text/css" rel="stylesheet" />
    <link href="~/Styles/op.css" type="text/css" rel="stylesheet" />
    <link href="~/Styles/Detail.css" type="text/css" rel="stylesheet" />
    <link href="~/Styles/superTables.css" type="text/css" rel="stylesheet" />
    <link rel="stylesheet" type="text/css" href="~/ext-3.3.1/resources/css/ext-all.css" />
    <link href="../../JScript/Date/lyz.calendar.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src='<%=ResolveUrl("~/ext-3.3.1/ext-base.js") %>'></script>
    <script type="text/javascript" src='<%=ResolveUrl("~/ext-3.3.1/ext-all.js") %>'></script>
    <script type="text/javascript" src='<%=ResolveUrl("~/Scripts/JQuery/jquery-1.8.2.min.js") %>'></script>
    <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Resize.js") %>'></script>
    <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Detail.js") %>'></script>
    <script type="text/javascript" src='<%=ResolveUrl("~/JScript/superTables.js") %>'></script>
    <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Scrolltable.js") %>'></script>
    <script type="text/javascript" src="Js/CancelOrdNo.js"></script>
    <script src="../../JScript/Date/lyz.calendar.min.js" type="text/javascript"></script>
    <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Commons.js") %>'></script>
    <script type="text/javascript" src='<%=ResolveUrl("~/JScript/DataProcess.js") %>'></script>
    <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Ajax.js") %>'></script>
    <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Date/lyz.calendar.min.js") %>'></script>
    <script type="text/javascript">
        $(document).ready(function () {
            initialTable();           
        });

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table class="maintable" cellspacing="0" cellpadding="0" width="100%" align="center"
            bordercolor="#ffffff" border="1">
            <tr>
                <td class="musttitle" align="center" width="15%">
                    <asp:Literal ID="ltlBillID" Text="訂購單號" runat="server"></asp:Literal>
                </td>
                <td width="35%">
                    &nbsp;<asp:TextBox ID="txtBillID" runat="server" CssClass="TextRead" ReadOnly="true"
                        Width="72%" MaxLength="20"></asp:TextBox>
                    &nbsp;
                </td>
                <td colspan="2" align="right">
                    <asp:Button ID="btnOK" runat="server" Text=" 確定" CssClass="but" Width="75px"></asp:Button>&nbsp;
                    <asp:Button ID="btnCancel" runat="server" Text="放棄" CssClass="but" Width="75px">
                    </asp:Button>&nbsp;&nbsp;
                </td>
            </tr>
            <tr>
                <td class="musttitle" align="center">
                    <asp:Literal ID="ltlStyleName" Text="企業編號" runat="server"></asp:Literal>
                </td>
                <td colspan="3">
                    &nbsp;<asp:TextBox ID="txtEnterpriseID" runat="server" CssClass="TextRead" ReadOnly="true"
                        Width="30%" MaxLength="20"> </asp:TextBox>
                    &nbsp;<asp:TextBox ID="txtEnterpriseName" runat="server" CssClass="TextRead" Width="65%"
                        MaxLength="20" ReadOnly="true"> </asp:TextBox>
                </td>
            </tr>
        </table>
        <div id="table-container" style="width: 100%; height: 320px;">
            <asp:RadioButton ID="RadioButton1" Text="整張取消" runat="server" Checked="True" GroupName="qq" />
            <asp:RadioButton ID="RadioButton2" Text="部份取消" runat="server" GroupName="qq" />
            <br />
            <div id="div_Detail1" class="fakeContainer" style="margin: 5px; height: 290px;">
            </div>
        </div>
    </div>
    <input id="HdnSubDetail1" type="hidden" runat="server" />
    <div id="subColsName1" runat="server">
        <asp:Literal ID="sub1xxRowID" Text="(序號),40,label,1" Visible="false" runat="server"></asp:Literal>
        <asp:Literal ID="sub1xxStyleID" Text="(款式編號),100,text,1" Visible="false" runat="server"></asp:Literal>
        <asp:Literal ID="sub1xxStyleName" Text="(款式名稱),100,text,1" Visible="false" runat="server"></asp:Literal>
        <asp:Literal ID="sub1xxPages" Text="(訂購張數),100,numeric_q,1" Visible="false" runat="server"></asp:Literal>
        <asp:Literal ID="sub1xxCancelPages" Text="取消張數,100,numeric_q,0" Visible="false" runat="server"></asp:Literal>
        <asp:Literal ID="sub1xxNoDeliverPages" Text="(未交張數),100,numeric_q,1" Visible="false"
            runat="server"></asp:Literal>
        <asp:Literal ID="sub1xxIsClose" Text="(結案),40,dropdown,1" Visible="false" runat="server"></asp:Literal>
        <asp:Literal ID="sub1xxIsEstimate" Text="(已估算),60,dropdown,1" Visible="false" runat="server"></asp:Literal>
        <asp:Literal ID="sub1xxMemo" Text="備註,100,text,0" Visible="false" runat="server"></asp:Literal>
        <asp:Literal ID="sub1xxLastModifyDate" Text="(異動日期),100,text,1" Visible="false" runat="server"></asp:Literal>
        <asp:Literal ID="sub1xxLastModifyUserName" Text="(異動人員),100,text,1" Visible="false"
            runat="server"></asp:Literal>
    </div>
    <%--                    <div id="subColsName2" runat="server">
                    <asp:Literal ID="sub2Literal3" Text="(序號),40,text,1,-1" Visible="false" runat="server"></asp:Literal>  
                    </div>--%>
    <div id="subddlstruct" runat="server">
        <asp:DropDownList ID="ddlIsClose" Visible="false" runat="server">
            <asp:ListItem Value="false">否</asp:ListItem>
            <asp:ListItem Value="true">是</asp:ListItem>
        </asp:DropDownList>
        <asp:DropDownList ID="ddlIsEstimate" Visible="false" runat="server">
            <asp:ListItem Value="false">否</asp:ListItem>
            <asp:ListItem Value="true">是</asp:ListItem>
        </asp:DropDownList>
    </div>
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    </form>
</body>
</html>
