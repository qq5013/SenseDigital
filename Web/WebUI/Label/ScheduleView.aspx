<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ScheduleView.aspx.cs" Inherits="WebUI_Label_ScheduleView" %>

<%@ Register Src="../../Controls/Calendar.ascx" TagName="Calendar" TagPrefix="uc2" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <title></title>
    <link href="~/Styles/Main.css" type="text/css" rel="stylesheet" />
    <link href="~/Styles/op.css" type="text/css" rel="stylesheet" />
    <link rel="stylesheet" type="text/css" href="~/ext-3.3.1/resources/css/ext-all.css" />
    <script type="text/javascript" src='<%=ResolveUrl("~/ext-3.3.1/ext-base.js") %>'></script>
    <script type="text/javascript" src='<%=ResolveUrl("~/ext-3.3.1/ext-all.js") %>'></script>
    <script type="text/javascript" src='<%=ResolveUrl("~/Scripts/JQuery/jquery-1.8.2.min.js") %>'></script>
    <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Commons.js") %>'></script>
    <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Resize.js") %>'></script>
    <script type="text/javascript">
        function initial() {
            var tabPanel = new Ext.TabPanel({
                height: 335,
                //width: "100%",
                autoWidth: true,
                defaults: { autoScroll: true, layout: 'fit' },
                autoTabs: true, //自动扫描页面中的div然后将其转换为标签页
                deferredRender: false, //不进行延时渲染
                activeTab: 0, //默认激活第一个tab页
                animScroll: true, //使用动画滚动效果
                enableTabScroll: true, //tab标签超宽时自动出现滚动按钮
                applyTo: 'tabsub'

            });

            $("#btnCancelOrdNo").bind("click", function () {
                if ($('#txtBU_CheckUserName').val().length != 0) {
                    alert(msg_cancelchk);
                    return false;
                }
                var strReturn = window.showModalDialog('CancelOrdNo.aspx?FormID=LB_Order&ID=' + $("#txtBillID").val(), null, 'DialogHeight:400px;DialogWidth:800px;help:no;scroll:no');
                $('#HdnSubDetail1').val("");
                if (strReturn != null) {
                    $('#HdnSubDetail1').val(strReturn);
                    return true;
                }
                return false;
            });
            $("#btnOrdPreNO").bind("click", function () {
                if ($('#txtBU_CheckUserName').val().length != 0) {
                    alert(msg_cancelchk);
                    return false;
                }
                var strReturn = window.showModalDialog('OrdPreNO.aspx?FormID=LB_Order&ID=' + $("#txtBillID").val(), null, 'DialogHeight:400px;DialogWidth:800px;help:no;scroll:no');
                $('#HdnSubDetail1').val("");
                if (strReturn != null) {
                    $('#HdnSubDetail1').val(strReturn);
                    return true;
                }
                return false;
            });
            $("#btnImageQuery").bind("click",
                function () {
                    var obj = $("input[type=radio][id*=GridView1_cbsubSelect][checked=checked]");
                    if (obj.length == 0) {
                        alert(msg_choosesub);
                        return false;
                    }
                    var strReturn = obj[0].parentNode.parentNode.cells[2].innerHTML;
                    window.showModalDialog('ImageQuery.aspx?BillID=' + $("#txtBillID").val() + '&Flag=2&ID=' + strReturn, null, 'DialogHeight:280px;DialogWidth:600px;help:no;scroll:no');
                    return false;
                });
            $("#btnEstimate").bind("click", function () {
                if ($('#txtBU_CheckUserName').val().length != 0) {
                    alert(msg_cancelchk);
                    return false;
                }
                var strReturn = window.showModalDialog('msgbox.aspx', null, 'DialogHeight:50px;DialogWidth:120px;help:no;scroll:no');
                if (strReturn == null) return false;
                return strReturn;
            });
            $("#btnBillState").bind("click", function () {
                if ($('#txtBU_CheckUserName').val().length != 0) {
                    alert(msg_cancelchk);
                    return false;
                }
                if ($('#ddlBillState').val() == "2") {
                    alert(msg_isclosed);
                    return false;
                }
                return true;
            });
            var obj = $("input[type=radio][id*=GridView1_cbsubSelect]");
            if (obj && obj.length > 0) {
                obj.each(function () { $(this).attr("name", "cbsubSelect"); });
                obj[0].checked = true;
            }
        }
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
                <img alt="Loading" src="../../images/main/loading.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="updatePanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table style="width: 100%; height: 20px;" class="OperationBar">
                <tr>
                    <td align="right" style="width: 60%">
                        <asp:Button ID="btnFirst" runat="server" Text="首筆" CssClass="ButtonFirst" OnClick="btnFirst_Click" />
                        <asp:Button ID="btnPre" runat="server" Text="上一筆" CssClass="ButtonPre" OnClick="btnPre_Click" />
                        <asp:Button ID="btnNext" runat="server" Text="下一筆" CssClass="ButtonNext" OnClick="btnNext_Click" />
                        <asp:Button ID="btnLast" runat="server" Text="尾筆" CssClass="ButtonLast" OnClick="btnLast_Click" />
                    </td>
                    <td align="right">
                        <asp:Button ID="btnPrint" runat="server" Text="列印" CssClass="ButtonPrint" OnClientClick="return print();" />
                        <asp:Button ID="btnAdd" runat="server" Text="新增" CssClass="ButtonCreate" OnClientClick="Edit(false);return false" />
                        <asp:Button ID="btnDelete" runat="server" Text="刪除" CssClass="ButtonDel" OnClientClick="return ViewDelete()"
                            OnClick="btnDelete_Click" />
                        <asp:Button ID="btnEdit" runat="server" Text="修改" CssClass="ButtonModify" OnClientClick="return BillEdit(true);"
                            OnClick="btnEdit_Click" />
                        <asp:Button ID="btnExit" runat="server" Text="離開" OnClientClick="return Exit()" CssClass="ButtonExit" />
                        <asp:Button ID="btnBack" runat="server" Text="返回" OnClientClick="return Back()" CssClass="ButtonBack" />
                    </td>
                </tr>
            </table>
            <div id="surdiv" style="overflow: auto">
                <table class="maintable" cellspacing="0" cellpadding="0" width="100%" align="center"
                    bordercolor="#ffffff" border="1">
                    <tr>
                        <td valign="middle" align="left" colspan="4" height="22" class="title1">
                            <p>
                                <asp:Literal ID="ltlTitle" Text="生產計劃單[ 單筆明細畫面 ]" runat="server"></asp:Literal></p>
                        </td>
                    </tr>
                    <tr>
                        <td class="musttitle" align="center" width="12%">
                            <asp:Literal ID="ltlBillDate" Text="計劃日期" runat="server"></asp:Literal>
                        </td>
                        <td width="18%">
                            &nbsp;
                            <uc2:Calendar ID="txtBillDate" runat="server" ReadOnly="true" />
                        </td>
                        <td class="musttitle" align="center" width="12%">
                            <asp:Literal ID="ltlBillID" Text="計劃單號" runat="server"></asp:Literal>
                        </td>
                        <td width="18%">
                            &nbsp;
                            <asp:TextBox ID="txtBillID" runat="server" CssClass="TextRead" ReadOnly="true" Width="90%"
                                MaxLength="20"></asp:TextBox>
                            &nbsp;
                        </td>
                       
                    </tr>
                     <tr>
                        <td align="center" class="smalltitle" width="12%">
                                <asp:Literal ID="Literal1" runat="server" Text="單&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;況"></asp:Literal>
                            </td>
                            <td>
                                &nbsp;&nbsp;<asp:DropDownList ID="ddlBillState" runat="server" Enabled="false" 
                                    Width="35%">
                                    <asp:ListItem Value="0">無效</asp:ListItem>
                                    <asp:ListItem Value="1">有效</asp:ListItem>                                    
                                    <asp:ListItem Value="2">結案</asp:ListItem>
                                </asp:DropDownList>
                                &nbsp;<asp:Button ID="btnBillState" runat="server" CssClass="but" 
                                    OnClick="btnBillState_Click" Text="單況切換" Width="91px" />
                            </td>
                        <td colspan="2">
                            <asp:Button ID="btnEstimate" runat="server" Text=" 估算切換" CssClass="but" 
                                Width="75px" onclick="btnEstimate_Click">
                            </asp:Button>
                           
                            &nbsp;
                            <asp:Button ID="btnImageQuery" runat="server" Text=" 款式圖片" CssClass="but" 
                                Width="75px" onclick="btnImageQuery_Click"> </asp:Button>
                        </td>
                    </tr>
                   
                </table>
                <div id='tabsub'>
                    <div class='x-tab' title='<%=tabName1.Text %>'>
                        <div style="overflow: auto; width: 100%; height: 305px">
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" SkinID="GridViewSkin"
                                Width="1200px" OnRowDataBound="GridView1_RowDataBound">
                                <Columns>
                                    <asp:TemplateField meta:resourcekey="TemplateFieldResource3">
                                        <HeaderTemplate>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:RadioButton ID="cbsubSelect" runat="server"></asp:RadioButton>
                                        </ItemTemplate>
                                        <HeaderStyle Width="40px"></HeaderStyle>
                                        <ItemStyle Width="40px"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="RowID" HeaderText="(序號)" SortExpression="RowID">
                                        <ItemStyle HorizontalAlign="Center" Width="8%" Wrap="False" />
                                        <HeaderStyle Wrap="False" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="StyleID" HeaderText="款式編號" SortExpression="StyleID">
                                        <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                                        <HeaderStyle Wrap="False" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="StyleName" HeaderText="(款式名稱)" SortExpression="LabelMode">
                                        <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                                        <HeaderStyle Wrap="False" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="生產張數" SortExpression="Pages">
                                        <ItemTemplate>
                                            <%# DataBinder.Eval(Container.DataItem, "Pages", "{0:N0}")%>
                                        </ItemTemplate>
                                        <HeaderStyle Wrap="False" />
                                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="預交日期" SortExpression="PreDeliverDate">
                                        <ItemTemplate>
                                            <%# ToYMD( DataBinder.Eval(Container.DataItem, "PreDeliverDate")) %>
                                        </ItemTemplate>
                                        <HeaderStyle Wrap="False" />
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                    </asp:TemplateField>
                                    <%--<asp:BoundField DataField="PreDeliverDate" HeaderText="預交日期" HtmlEncode="false" DataFormatString="{0:yyyy/MM/dd}"
                                        SortExpression="PreDeliverDate">
                                        <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                                        <HeaderStyle Wrap="False" />
                                    </asp:BoundField>--%>
                                    <asp:BoundField DataField="IsEstimate" HeaderText="(已估算)" SortExpression="IsEstimate">
                                        <ItemStyle HorizontalAlign="Center" Width="8%" Wrap="False" />
                                        <HeaderStyle Wrap="False" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Memo" HeaderText="備註" SortExpression="Memo">
                                        <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                                        <HeaderStyle Wrap="False" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="(建檔日期)" SortExpression="CreateDate">
                                        <ItemTemplate>
                                            <%# ToYMD(DataBinder.Eval(Container.DataItem, "CreateDate"))%>
                                        </ItemTemplate>
                                        <HeaderStyle Wrap="False" />
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                    </asp:TemplateField>                                 
                                    <asp:BoundField DataField="CreateUserName" HeaderText="(建檔人員)" SortExpression="CreateUserName">
                                        <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                                        <HeaderStyle Wrap="False" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="(異動日期)" SortExpression="LastModifyDate">
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
                                </Columns>
                                <PagerSettings Visible="False" />
                            </asp:GridView>
                        </div>
                    </div>
                    <div class='x-tab' title='<%=Literal16.Text %>'>
                        <table class="maintable" cellspacing="0" cellpadding="0" width="100%" align="center"
                            bordercolor="#ffffff" border="1">
                            <tr>
                                <td class="smalltitle" align="center" width="12%">
                                    <asp:Literal ID="Literal20" Text="備註" runat="server"></asp:Literal>
                                </td>
                                <td>
                                    &nbsp;
                                    <asp:TextBox ID="txtMemo" runat="server" CssClass="TextRead" ReadOnly="true" TextMode="MultiLine"
                                        Width="98%" Height="300"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <table class="maintable" cellspacing="0" cellpadding="0" width="100%" align="center"
                    bordercolor="#ffffff" border="1">
                    <tr>
                        <td class="smalltitle" align="center" width="12%">
                            <asp:Literal ID="Literal4" Text="張數合計" runat="server"></asp:Literal>
                        </td>
                        <td width="18%">
                            &nbsp;
                            <asp:TextBox ID="txtTotalPages" runat="server" CssClass="TextRead" Width="90%" MaxLength="20"
                                ReadOnly="True">0</asp:TextBox>
                        </td>
                        <td class="smalltitle" align="center" width="12%">
                            &nbsp;
                        </td>
                        <td width="18%">
                            &nbsp;
                        </td>
                        <td class="smalltitle" align="center" width="12%">
                        </td>
                        <td>
                            &nbsp;&nbsp;
                            <asp:Button ID="btnBUCheck" runat="server" CssClass="but" OnClick="btnBUCheck_Click"
                                Text="營運覆核" Width="75px" />
                        </td>
                    </tr>
                    <tr>
                        <td class="smalltitle" align="center">
                            <asp:Literal ID="ltlCreateUserName" Text="建檔人員" runat="server"></asp:Literal>
                        </td>
                        <td>
                            &nbsp;
                            <asp:TextBox ID="txtCreateUserName" runat="server" CssClass="TextRead" Width="90%"
                                MaxLength="20" ReadOnly="True"></asp:TextBox>
                        </td>
                        <td class="smalltitle" align="center">
                            <asp:Literal ID="ltlLastModifyUserName" Text="異動人員" runat="server"></asp:Literal>
                        </td>
                        <td>
                            &nbsp;
                            <asp:TextBox ID="txtLastModifyUserName" runat="server" CssClass="TextRead" Width="90%"
                                MaxLength="20" ReadOnly="true"></asp:TextBox>
                        </td>
                        <td class="smalltitle" align="center">
                            <asp:Literal ID="Literal6" runat="server" Text="營運覆核用戶"></asp:Literal>
                        </td>
                        <td>
                            &nbsp;
                            <asp:TextBox ID="txtBU_CheckUserName" runat="server" CssClass="TextRead" Width="90%"
                                MaxLength="20" ReadOnly="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="smalltitle" align="center">
                            <asp:Literal ID="ltlCreateDate" runat="server" Text="建檔日期"></asp:Literal>
                        </td>
                        <td>
                            &nbsp;
                            <asp:TextBox ID="txtCreateDate" runat="server" CssClass="TextRead" Width="90%" MaxLength="20"
                                ReadOnly="True"></asp:TextBox>
                        </td>
                        <td class="smalltitle" align="center">
                            <asp:Literal ID="ltlLastModifyDate" Text="異動日期" runat="server"></asp:Literal>
                        </td>
                        <td>
                            &nbsp;
                            <asp:TextBox ID="txtLastModifyDate" runat="server" CssClass="TextRead" Width="90%"
                                MaxLength="20" ReadOnly="true"></asp:TextBox>
                        </td>
                        <td class="smalltitle" align="center">
                            <asp:Literal ID="Literal7" runat="server" Text="營運覆核日期"></asp:Literal>
                        </td>
                        <td>
                            &nbsp;
                            <asp:TextBox ID="txtBU_CheckDate" runat="server" CssClass="TextRead" Width="90%"
                                MaxLength="20" ReadOnly="true"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:Literal ID="Literal10" runat="server" Visible="false" Text="是"></asp:Literal>
    <asp:Literal ID="Literal11" runat="server" Visible="false" Text="否"></asp:Literal>
    <div id="msg" runat="server">
        <asp:Literal ID="msg_choosesub" Text="請先指定明細內容!" Visible="false" runat="server"></asp:Literal>
        <asp:Literal ID="msg_cancelchk" Text="請先取消覆核!" Visible="false" runat="server"></asp:Literal>
        <asp:Literal ID="msg_isclosed" Text="此單已經結案, 不可變更單況!" Visible="false" runat="server"></asp:Literal>
    </div>
    <input id="HdnSubDetail1" type="hidden" runat="server" />
    <asp:Literal ID="tabName1" Text="生產計劃明細" Visible="false" runat="server"></asp:Literal>
    <asp:Literal ID="Literal16" Text="備註" Visible="false" runat="server"></asp:Literal>
    </form>
</body>
</html>
