<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OrdPreNO.aspx.cs" Inherits="WebUI_Label_OrdPreNO" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <title>交期異動作業</title>
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
    <script type="text/javascript" src="Js/OrdPreNO.js"></script>
    <script src="../../JScript/Date/lyz.calendar.min.js" type="text/javascript"></script>
    <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Commons.js") %>'></script>
    <script type="text/javascript" src='<%=ResolveUrl("~/JScript/DataProcess.js") %>'></script>
    <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Ajax.js") %>'></script>
    <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Date/lyz.calendar.min.js") %>'></script>
    <script type="text/javascript">
        $(document).ready(function () {

            initialTable();
            $("#btnOK").bind('click', function () {

                window.returnValue = GenerateDetailToJson(0);
                window.close(); return false;
            });
            $("#btnCancel").bind('click', function () { window.close(); return false; });
        });

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
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
                        Width="30%" MaxLength="20">
                    </asp:TextBox>
                    &nbsp;<asp:TextBox ID="txtEnterpriseName" runat="server" CssClass="TextRead" Width="65%"
                        MaxLength="20" ReadOnly="true">
                    </asp:TextBox>
                </td>
            </tr>
        </table>
        <br />
        <div id="div_Detail1" class="fakeContainer" style="margin: 5px; height: 305px;">
        </div>
    </div>
    <input id="HdnSubDetail1" type="hidden" runat="server" />
    <div id="subColsName1" runat="server">
        <asp:Literal ID="sub1xxRowID" Text="(序號),40,label,1" Visible="false" runat="server"></asp:Literal>
        <asp:Literal ID="sub1xxStyleID" Text="(款式編號,100,text,0" Visible="false" runat="server"></asp:Literal>
        <asp:Literal ID="sub1xxStyleName" Text="(款式名稱),100,text,1" Visible="false" runat="server"></asp:Literal>
        <asp:Literal ID="sub1xxPages" Text="(訂購張數),100,numeric_q,1" Visible="false" runat="server"></asp:Literal>
        <asp:Literal ID="sub1xxPreDeliverDate" Text="預交日期,100,date,0" Visible="false" runat="server"></asp:Literal>
        <asp:Literal ID="sub1xxInitialPreDeliverDate" Text="(原預交日期),100,text,1" Visible="false"
            runat="server"></asp:Literal>
        <asp:Literal ID="sub1xxCancelPages" Text="(取消張數),100,numeric_q,1" Visible="false"
            runat="server"></asp:Literal>
        <asp:Literal ID="sub1xxNoDeliverPages" Text="(未交張數),100,numeric_q,1" Visible="false"
            runat="server"></asp:Literal>
        <asp:Literal ID="sub1xxIsClose" Text="(結案),40,dropdown,1" Visible="false" runat="server"></asp:Literal>
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
    </div>
    </form>
</body>
</html>
