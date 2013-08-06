<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ScheduleEdit.aspx.cs" Inherits="WebUI_Label_ScheduleEdit" %>

<%@ Register src="../../Controls/Calendar.ascx" tagname="Calendar" tagprefix="uc2" %>
<%@ Register Assembly="EditableDropDownList" Namespace="EditableControls" TagPrefix="editable" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head id="Head1" runat="server">
		<meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
  
    <title></title> 
           <link href="~/Styles/Main.css" type="text/css" rel="stylesheet" /> 
        <link href="~/Styles/op.css" type="text/css" rel="stylesheet" /> 
        <link href="~/Styles/Detail.css" type="text/css" rel="stylesheet" /> 
        <link href="~/Styles/superTables.css" type="text/css" rel="stylesheet" /> 
        <link href="~/JScript/EditDropDownList/css/jquery-ui.css" rel="stylesheet" type="text/css" />
        <link rel="stylesheet" type="text/css" href="~/ext-3.3.1/resources/css/ext-all.css" />
 	    <script type="text/javascript" src='<%=ResolveUrl("~/ext-3.3.1/ext-base.js") %>'></script>
        <script type="text/javascript" src='<%=ResolveUrl("~/ext-3.3.1/ext-all.js") %>'></script>

        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/EditDropDownList/Js/jquery-1.6.4.min.js") %>'></script> 
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/EditDropDownList/Js/jquery.ui.core.js") %>'></script> 
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/EditDropDownList/Js/jquery.ui.widget.js") %>'></script> 
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/EditDropDownList/Js/jquery.ui.button.js") %>'></script> 
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/EditDropDownList/Js/jquery.ui.position.js") %>'></script> 
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/EditDropDownList/Js/jquery.ui.autocomplete.js") %>'></script> 
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/EditDropDownList/Js/jquery.ui.combobox.js") %>'></script> 


        <%--<script type="text/javascript" src='<%=ResolveUrl("~/Scripts/JQuery/jquery-1.8.2.min.js") %>'></script> --%>
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Resize.js") %>'></script>  
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Detail.js") %>'></script>
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/superTables.js") %>'></script>
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Scrolltable.js") %>'></script>
        
        <script type="text/javascript" src="Js/Schedule.js"></script>
        
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Commons.js") %>'></script>
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/DataProcess.js") %>'></script>
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Ajax.js") %>'></script>   
        <script type="text/javascript">
            $(document).ready(function () {
                //                $("#txtDeliverCountry").combobox({ source: ["Address1", "Address1"], refreshsource: true });
                content_resize();

                //明細 
                initialTable();

                var tabPanel = new Ext.TabPanel({
                    height: 335,
                    //width: "100%",

                    autoTabs: true, //自动扫描页面中的div然后将其转换为标签页
                    deferredRender: false, //不进行延时渲染
                    activeTab: 0, //默认激活第一个tab页
                    animScroll: true, //使用动画滚动效果
                    enableTabScroll: true, //tab标签超宽时自动出现滚动按钮
                    applyTo: 'tabs'
                });
            });
            function Save() {
                $("#txtBillID").val(trim($("#txtBillID").val()));
               

                if ($("#txtBillID").val() == "") {
                    alert("<%=ltlBillID.Text %>" + "<%=Resources.Resource.NotNull %>");
                    $("#txtBillID").focus();
                    return false;
                }
                if ($("#txtBillDate").val() == "") {
                    alert("<%=ltlBillDate.Text %>" + "<%=Resources.Resource.NotNull %>");
                    $("#txtBillDate").focus();
                    return false;
                }

                if (!isEditState) {
                    var exist = IsExists(FormID, $("#txtBillID").val(), cnKey);
                    if (exist) {
                        alert("<%=ltlBillID.Text %>" + "<%=Resources.Resource.Exists %>");
                        $("#txtBillID").focus();
                        return false;
                    }
                }
                ////子檔驗證提示
                var table = $("#" + tbTableId[0] + "_sDataTable");
                var rowCount = table.find('tr').length - 1;
                if (rowCount == 0) { alert("<%=tabName1.Text %>" + "<%=Resources.Resource.NotNull %>!"); return false; }
                for (var i = 0; i < rowCount; i++) {
                    var rowIndex = i + 1;
                    if ($.trim($("#" + tbRowName[0] + "_" + rowIndex + "_StyleID").val()) == "") {
                        alert("<%=sub1xxStyleID.Text.Split(',')[0] %>" + "<%=Resources.Resource.NotNull %>!");
                        $("#" + tbRowName[0] + "_" + rowIndex + "_StyleID").focus();
                        return false;
                    }
                }
                var data = GenerateDetailToJson(0);
                $('#HdnSubDetail1').val(data);
                return true;
            }
        </script>
	</head>
	<body>
		<form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" />  
            <table style="width: 100%; height: 20px;" class="OperationBar">
                <tr>
                    <td align="right">
                        
                        <asp:Button ID="btnCancel" runat="server" Text="放棄" OnClientClick="history.go(-1);return false" CssClass="ButtonCancel" />
                        <asp:Button ID="btnSave" runat="server" Text="保存" OnClientClick="return Save()" 
                            CssClass="ButtonCreate" onclick="btnSave_Click" />
                        <asp:Button ID="btnExit" runat="server" Text="離開" OnClientClick="return Exit()" CssClass="ButtonDel" />
                    </td>
                </tr>
            </table>
			 <table class="maintable" cellspacing="0" cellpadding="0" width="100%" align="center"
                    bordercolor="#ffffff" border="1">
                    <tr>
                        <td valign="middle" align="left" colspan="6" height="22" class="title1">
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
                            <uc2:Calendar ID="txtBillDate" runat="server"  />
                        </td>
                        <td class="musttitle" align="center" width="12%">
                            <asp:Literal ID="ltlBillID" Text="計劃單號" runat="server"></asp:Literal>
                        </td>
                        <td width="18%">
                            &nbsp;
                            <asp:TextBox ID="txtBillID" runat="server" CssClass="Textbox" Width="90%"
                                MaxLength="20"></asp:TextBox>
                            &nbsp;
                        </td>
                        <td align="center" class="smalltitle" width="12%">
                                <asp:Literal ID="Literal1" runat="server" Text="單況"></asp:Literal>
                            </td>
                            <td>
                                &nbsp;&nbsp;<asp:DropDownList ID="ddlBillState" runat="server" Enabled="false" 
                                    Width="35%">
                                    <asp:ListItem Value="0">無效</asp:ListItem>
                                    <asp:ListItem Value="1">有效</asp:ListItem>                                    
                                    <asp:ListItem Value="2">結案</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                    </tr>
                    <%-- <tr>
                        <td class="musttitle" align="center">
                            <asp:Literal ID="ltlEnterpriseID" Text="企業編號" runat="server"></asp:Literal>
                        </td>
                        <td colspan="3">
                            &nbsp;
                            <asp:TextBox ID="txtEnterpriseID" runat="server" CssClass="Textbox"  
                                Width="30%"  >
                            </asp:TextBox>
                            &nbsp;<asp:TextBox ID="txtEnterpriseName" runat="server" CssClass="TextRead" Width="65%"
                                MaxLength="20" >
                            </asp:TextBox>
                            <td align="center" class="smalltitle">
                                <asp:Literal ID="Literal17" runat="server" Text="單況"></asp:Literal>
                            </td>
                            <td>
                                &nbsp;&nbsp;<asp:DropDownList ID="ddlBillState" runat="server" Enabled="false" Width="35%">
                                    <asp:ListItem Value="0">有效</asp:ListItem>                                    
                                    <asp:ListItem Value="1">無效</asp:ListItem>
                                    <asp:ListItem Value="2">結案</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                    </tr>
                    <tr>
                        <td class="smalltitle" align="center">
                            <asp:Literal ID="Literal21" runat="server" Text="聯絡人員"></asp:Literal>
                        </td>
                        <td>
                            &nbsp;&nbsp;<asp:TextBox ID="txtContact" runat="server" CssClass="Textbox" 
                                Width="90%"  ></asp:TextBox>
                        </td>
                        <td class="smalltitle" align="center">
                            <asp:Literal ID="Literal22" runat="server" Text="聯絡電話"></asp:Literal>
                        </td>
                        <td>
                            &nbsp;&nbsp;
                            <asp:TextBox ID="txtContactPhone" runat="server" CssClass="Textbox" 
                                  Width="90%"></asp:TextBox>
                        </td>
                        <td align="center" class="smalltitle">
                            <asp:Literal ID="Literal23" runat="server" Text="傳真號碼"></asp:Literal>
                        </td>
                        <td>
                            &nbsp;&nbsp;<asp:TextBox ID="txtFax" runat="server" CssClass="Textbox"  Width="90%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="smalltitle" align="center">
                            <asp:Literal ID="Literal25" runat="server" Text="送貨地址"></asp:Literal>
                        </td>
                        <td colspan="3">
                            &nbsp;
                            <asp:TextBox ID="txtDeliverCountry" runat="server" CssClass="Textbox"  
                                Width="26%"></asp:TextBox>
                            &nbsp;<asp:TextBox ID="txtDeliverAddress" runat="server" CssClass="Textbox"  
                                Width="61%"></asp:TextBox>
                        </td>
                        <td class="smalltitle" align="center">
                            <asp:Literal ID="Literal3" runat="server" Text="交貨方式"></asp:Literal>
                        </td>
                        <td>
                            &nbsp;
                            <asp:TextBox ID="txtDeliverMehtod" runat="server" CssClass="Textbox"   
                                Width="82%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="musttitle" align="center">
                            <asp:Literal ID="ltlBusPersonID" runat="server" Text="業務人員"></asp:Literal>
                        </td>
                        <td colspan="3">
                            &nbsp;
                            <asp:TextBox ID="txtBusPersonID" runat="server" CssClass="Textbox"  Width="30%">
                            </asp:TextBox>
                            &nbsp;<asp:TextBox ID="txtBusPersonName" runat="server" CssClass="TextRead" MaxLength="20"
                                Width="65%">
                            </asp:TextBox>
                        </td>
                        <td colspan="2">
                            &nbsp;
                            <asp:Button ID="btnImageQuery" runat="server" Text=" 款式圖片" CssClass="but" Width="75px">
                            </asp:Button>
                            &nbsp;</td>
                    </tr>--%>
                </table>

                    <div id='tabs'>
                        <div class='x-tab' title='<%=tabName1.Text %>'> 
                         <table style="width:100%">
                            <tr>
                                <td class="table_titlebgcolor" height="25px">
                                    <input id="btnAddDetail" class="ButtonCss" type="button" value="新增明細" style="width:60px;"  />
                                    <input id="btnDelDetail" class="ButtonCss" type="button" value="刪除明細" style="width:60px;" />
                                    <input id="btnInsDetail" class="ButtonCss" type="button" value="插入明細"  style="width:60px;" />
                                    <input id="btnBelowSameValue" class="ButtonCss" type="button" value="以下同值"  
                                        style="width:60px;" onclick="BelowSameValue();" /></td>
                            </tr>
                          </table> 

                           <div class="fakeContainer" id="div_Detail1" style=" overflow:auto  margin:2px;height:300px; width:1200px">
                           </div> 
            			  
                        </div>

                        <div class='x-tab' title='<%=Literal20.Text %>'>                              
                                
                                <table class="maintable" cellspacing="0" cellpadding="0" width="100%" align="center" borderColor="#ffffff" border="1" >
                                <tr>
                                <td class="smalltitle" align="center" width="12%"><asp:Literal ID="Literal20" Text="備註"
                                            runat="server"></asp:Literal></td>
                                <td colspan ="3" >&nbsp;
                                <asp:TextBox ID="txtMemo" runat="server" CssClass="Text" TextMode="MultiLine" 
                                        Width="98%" Height="300" ></asp:TextBox></td>
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
                                 >0</asp:TextBox>
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
                            &nbsp;</td>
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
					<input id="HdnSubDetail1" type="hidden" runat="server" />
                    
                    <div id="subColsName1" runat="server">
                        <%--//      colname,colwidth,type,readonly 
                        //Text="(序號) ,40      ,text,1       "--%>
                    <asp:Literal ID="sub1xxRowID" Text="(序號),40,label,1" Visible="false" runat="server"></asp:Literal>  
                        <%--//sub1__SubID 隱藏 --%>
                    <asp:Literal ID="sub1__SubID" Text=",40,label,1" Visible="false" runat="server"></asp:Literal>  
                    <asp:Literal ID="sub1xxStyleID" Text="款式編號,100,text,0" Visible="false" runat="server"></asp:Literal>  
                    <asp:Literal ID="sub1xxStyleName" Text="(款式名稱),160,text,1" Visible="false" runat="server"></asp:Literal>   
                    <asp:Literal ID="sub1xxPages" Text="生產張數,100,numeric_q,0" Visible="false" runat="server"></asp:Literal>   
                    <asp:Literal ID="sub1xxPreDeliverDate" Text="預交日期,100,date,0" Visible="false" runat="server"></asp:Literal>
                    <asp:Literal ID="sub1__InitialPreDeliverDate" Text="預交日期,100,date,0" Visible="false" runat="server"></asp:Literal>
                        <%-- <asp:Literal ID="sub1xxIsGift" Text="贈品,40,dropdown,0" Visible="false" runat="server"></asp:Literal> --%>                 <%--   <asp:Literal ID="sub1xxCancelPages" Text="(取消張數),100,numeric_q,1" Visible="false" runat="server"></asp:Literal>  
                    <asp:Literal ID="sub1xxNoDeliverPages" Text="(未交張數),100,numeric_q,1" Visible="false" runat="server"></asp:Literal>--%>                   <%-- <asp:Literal ID="sub1xxIsClose" Text="(結案),40,dropdown,1" Visible="false" runat="server"></asp:Literal> --%>
                    <asp:Literal ID="sub1xxIsEstimate" Text="(已估算),60,dropdown,1" Visible="false" runat="server"></asp:Literal> 
                    <asp:Literal ID="sub1xxMemo" Text="備註,180,text,0" Visible="false" runat="server"></asp:Literal>   
                    <asp:Literal ID="sub1BUCreateDate" Text="(建檔日期),100,text,1" Visible="false" runat="server"></asp:Literal>   
                    <asp:Literal ID="sub1BUCreateUserName" Text="(建檔人員),100,text,1" Visible="false" runat="server"></asp:Literal>   
                    <asp:Literal ID="sub1xxLastModifyDate" Text="(異動日期),100,text,1" Visible="false" runat="server"></asp:Literal>   
                    <asp:Literal ID="sub1xxLastModifyUserName" Text="(異動人員),100,text,1" Visible="false" runat="server"></asp:Literal>                         
                    </div>
        <%--                    <div id="subColsName2" runat="server">
                    <asp:Literal ID="sub2Literal3" Text="(序號),40,text,1,-1" Visible="false" runat="server"></asp:Literal>  
                    </div>--%>
                    <div id="subddlstruct" runat="server">
                        <%--  <asp:DropDownList ID="ddlIsGift" Visible ="false" 
                        runat="server">
                        <asp:ListItem Value="false">否</asp:ListItem>
                        <asp:ListItem Value="true">是</asp:ListItem>
                    </asp:DropDownList>
                     <asp:DropDownList ID="ddlIsClose" Visible ="false" 
                        runat="server">
                        <asp:ListItem Value="false">否</asp:ListItem>
                        <asp:ListItem Value="true">是</asp:ListItem>
                    </asp:DropDownList>--%>
                   <asp:DropDownList ID="ddlIsEstimate" Visible ="false" 
                        runat="server">
                        <asp:ListItem Value="false">否</asp:ListItem>
                        <asp:ListItem Value="true">是</asp:ListItem>
                    </asp:DropDownList>
                    </div>
        <%-- <div id="msg" runat="server">
                    <asp:Literal ID="msg_firstKeyinEnterpriseID" Text="請先輸入[企業編號]" Visible="false" runat="server"></asp:Literal>   
                    <asp:Literal ID="msg_SubisExists" Text="明細已輸入,若要修改[企業編號],請先刪除所有明細!" Visible="false" runat="server"></asp:Literal>    
                    </div>--%>
                    <asp:Literal ID="tabName1" Text="生產計劃明細" Visible="false" runat="server"></asp:Literal>    
		</form>
	</body>
</html>
