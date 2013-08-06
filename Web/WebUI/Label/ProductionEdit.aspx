<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProductionEdit.aspx.cs" Inherits="WebUI_Label_ProductionEdit" %>
<%@ Register Assembly="Js.PageControl" Namespace="Js.PageControl" TagPrefix="cc1" %>
<%@ Register src="../../Controls/Calendar.ascx" tagname="Calendar" tagprefix="uc2" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head id="Head1" runat="server">
		<meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
  
    <title></title> 
           <link href="~/Styles/Main.css" type="text/css" rel="stylesheet" /> 
        <link href="~/Styles/op.css" type="text/css" rel="stylesheet" /> 
        <link href="~/Styles/Detail.css" type="text/css" rel="stylesheet" /> 
        <link href="~/Styles/superTables.css" type="text/css" rel="stylesheet" /> 
        <link rel="stylesheet" type="text/css" href="~/ext-3.3.1/resources/css/ext-all.css" />
 	    <script type="text/javascript" src='<%=ResolveUrl("~/ext-3.3.1/ext-base.js") %>'></script>
        <script type="text/javascript" src='<%=ResolveUrl("~/ext-3.3.1/ext-all.js") %>'></script>

        <script type="text/javascript" src='<%=ResolveUrl("~/Scripts/JQuery/jquery-1.8.2.min.js") %>'></script> 
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Resize.js") %>'></script>  
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Detail.js") %>'></script>
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/superTables.js") %>'></script>
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Scrolltable.js") %>'></script>
        <script type="text/javascript" src="Js/Production.js"></script>

        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/EditDropDownList/Js/jquery.ui.core.js") %>'></script> 
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/EditDropDownList/Js/jquery.ui.widget.js") %>'></script> 
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/EditDropDownList/Js/jquery.ui.button.js") %>'></script> 
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/EditDropDownList/Js/jquery.ui.position.js") %>'></script> 
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/EditDropDownList/Js/jquery.ui.autocomplete.js") %>'></script> 
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/EditDropDownList/Js/jquery.ui.combobox.js") %>'></script> 
        
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Commons.js") %>'></script>
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/DataProcess.js") %>'></script>
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Ajax.js") %>'></script>   
        <script type="text/javascript">
//            var strDateFormat = "y/MM/dd";
            $(document).ready(function () {
                $('#txtStyleID').bind('dblclick', function () {
                    GetOtherJsonValue('LB_Style', cnKey, 'StyleID,StyleName,Volumes,StdPages', 'txtStyleID,txtStyleName,txtProducePages,txtStdPages');
                    var volumes = $('#txtProducePages').val();
                    var stdpages = $('#txtStdPages').val();
                    var minPages = volumes * stdpages;
                    $('#txtProducePages').val(minPages);
                    $('#txtMinProducePages').val(minPages);
                    $('#txtPages').val(stdpages);
                    $('#txtVolumes').val(volumes);
                    $('#btnStyleQuery').attr("disabled", false);
                    $('#btnImageQuery').attr("disabled", false);
                    $('#btnGet').attr("disabled", false);

                });
                $('#txtStyleID').bind('change', function () {
                    getBaseData('LB_Style', cnKey, this.value, 'StyleID,StyleName,Volumes,StdPages', 'txtStyleID,txtStyleName,txtProducePages,txtStdPages');
                    var volumes = $('#txtProducePages').val();
                    var stdpages = $('#txtStdPages').val();
                    var minPages = volumes * stdpages;
                    $('#txtProducePages').val(minPages);
                    $('#txtMinProducePages').val(minPages);
                    $('#txtPages').val(stdpages);
                    $('#txtVolumes').val(volumes);
                    $('#btnStyleQuery').attr("disabled", false);
                    $('#btnImageQuery').attr("disabled", false);
                    $('#btnGet').attr("disabled", false);
                });
                $("#btnStyleQuery").bind("click", function () {
                    parent.addTab('../WebUI/Label/Styles.aspx?FormID=LB_Style', '標籤款式資料設定', 'tab_2_4');
                    return false;
                });
                $("#btnStyleQuery").bind("click", function () {
                    addTab('/WebUI/Label/Styles.aspx', '標籤款式資料設定','tab_2_4');
                    return false;
                });
                function addTab(url, name, idName) {
                    //alert(tabs.items);
                    if (this.parent.tabs.findById(idName) != null) {
                        this.parent.tabs.setActiveTab(idName);
                        this.parent.window.frames[idName].location.reload();
                        return;
                    }
                    var closeable = true;

                    this.parent.tabs.add({
                        title: name,
                        id: idName,
                        layout: 'fit',
                        iconCls: 'tabs',
                        html: "<iframe id='" + idName + "' src='" + url + "' width='100%' height='100%' frameborder='1' style='overflow:hidden' scrolling='no' ></iframe>",
                        closable: closeable
                    }).show();
                }
                $('#btnGet').bind('click', function () {
                    var ProducePages = $('#txtProducePages').val();
                    var minpages = $('#txtMinProducePages').val();
                    var StdPages = $('#txtStdPages').val();
                    if (ProducePages < minpages) {
                        alert(msg_ProducePagesMin);
                        return false;
                    }
                    else {
                        if (ProducePages % StdPages != 0) {
                            alert(msg_ProducePages);
                            return false;
                        }
                    }
                    $('#txtStyleID1').val($('#txtStyleID').val());
                    $('#txtStyleID').attr("class", 'TextRead');
                    $('#txtProducePages').attr("class", 'TextRead');
                    $('#txtStdPages').attr("class", 'TextRead');
                    $('#txtStyleID').attr("disabled", "disabled");
                    var row = new Object();
                    row.dll_className = "Js.DAO.Label.StyleDao"; //注意大小寫
                    row.dll_ModeName = "GetLoadData"; //注意大小寫
                    row.cnKey = cnKey; //參數
                    row.filter = $('#txtBillDate_txtDate').val() + '$##$' + $('#txtStyleID').val() + '$##$' + $('#txtProducePages').val() + '$##$' + $('#txtStdPages').val() + '$##$' + $('#txtVolumes').val();

                    //載入數據
                    $('#HdnSubDetail1').val(jsAjax("doJsDAOMode", parseXmlPara(row)));
                    subData[0] = jQuery.parseJSON($('#HdnSubDetail1').val());

                    $('#txtStartLabelNo').val(subData[0][0].StarNo);
                    $('#txtEndLabelNo').val(subData[0][0].EndNo);
                    clearRow(0);
                    AddToDetail();
                    return false;
                });
                content_resize();
                //subData[0] = jQuery.parseJSON($('#HdnSubDetail1').val());
                initialTable();
                //AddToDetail();
                //doOption('Add', 1);
                //                $('#btnAddDetail').bind('click', function () { doOption('Add', 1); });
                //                $('#btnDelDetail').bind('click', function () { doOption('Del'); });
                //                $('#btnInsDetail').bind('click', function () { doOption('Ins', 1); });

                var tabPanel = new Ext.TabPanel({
                    height: 200,
                    width: "100%",
                    autoTabs: true, //自动扫描页面中的div然后将其转换为标签页
                    deferredRender: false, //不进行延时渲染
                    activeTab: 0, //默认激活第一个tab页
                    animScroll: true, //使用动画滚动效果
                    enableTabScroll: true, //tab标签超宽时自动出现滚动按钮
                    applyTo: 'tabs'
                });
            });
            function ProducePageBlur(obj) {
                formatCurrNumericControl(obj);
                var ProducePages = $('#txtProducePages').val();
                var minpages = $('#txtMinProducePages').val();
                var StdPages = $('#txtStdPages').val();
                if (ProducePages < minpages) {
                    alert(msg_ProducePagesMin);
                    return;
                }
                else {
                    if (ProducePages % StdPages != 0) {
                        alert(msg_ProducePages);
                        return;
                    }
                }
                $('#txtVolumes').val(ProducePages / StdPages)

            }
            function stdPageBlur(obj) {
                formatCurrNumericControl(obj);
                var ProducePages = $('#txtProducePages').val();
                var minpages = $('#txtMinProducePages').val();
                var StdPages = $('#txtStdPages').val();
                if (ProducePages % StdPages != 0) {
                    alert(msg_ProducePages);
                    return;
                }
                $('#txtVolumes').val(ProducePages / StdPages)
            }
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
                if ($("#txtStyleID").val() == "") {
                    alert("<%=LitStyleID.Text %>" + "<%=Resources.Resource.NotNull %>");
                    $("#txtStyleID").focus();
                    return false;
                }
                if ($("#txtStartLabelNo").val() == "") {
                    alert("<%=litStartLabelNo.Text %>" + "<%=Resources.Resource.NotNull %>");
                    $("#txtStyleID").focus();
                    return false;
                }
                ////子檔驗證提示
                var table = $("#" + tbTableId[0] + "_sDataTable");
                var rowCount = table.find('tr').length - 1;
                if (rowCount == 0) { alert(msg_subisnotNull); return false; }
                //判斷是起始序號及結束序號是否重複。
                var row = new Object();
                row.dll_className = "Js.DAO.Label.ProductionDao"; //注意大小寫
                row.dll_ModeName = "GetBetweenData"; //注意大小寫
                row.cnKey = cnKey; //參數
                row.strValue = $('#txtBillDate_txtDate').val() + '$##$' + $('#txtStyleID').val() + '$##$' + $('#txtStartLabelNo').val() + '$##$' + $('#txtEndLabelNo').val();

                //載入數據

                var obj = jsAjax("doJsDAOMode", parseXmlPara(row));
                if (!obj) {
                    alert(msg_LableNoBetween);
                    return false;
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
			<table class="maintable" cellspacing="0" cellpadding="0" width="100%" align="center" borderColor="#ffffff" border="1" >
					<tr>
						<td  valign="middle" align="left" colspan="6" height="22" class="title1">
							<p><asp:Literal ID="ltlTitle" Text="標籤生產單[ 單筆編輯畫面 ]" runat="server"></asp:Literal></p>
						</td>
					</tr>
				    <tr>
						<td class="musttitle" align="center" width="10%" ><asp:Literal ID="ltlBillDate" Text="生產日期"
                                runat="server"></asp:Literal>
                        </td>
						<td width="20%" align ="left" >&nbsp;
                        <uc2:Calendar ID="txtBillDate" runat="server" 
                                 />
                        </td>
						<td class="musttitle" align="center" width="10%"><asp:Literal 
                                ID="ltlBillID" Text="生產單號"
                                runat="server"></asp:Literal></td>
						<td  width="20%">&nbsp;
								<asp:textbox id="txtBillID" runat="server" CssClass="Textbox" Width="60%" 
                                MaxLength="20" ></asp:textbox>
                                
                                </td>
                        <td class="smalltitle" >
                                <asp:Literal ID="Literal10" Text="來源單號" runat="server"></asp:Literal>
                                </td>
                                 <td >
								<asp:textbox id="txtSourceBillID" runat="server" CssClass="Textbox" Width="60%" 
                                MaxLength="20" ></asp:textbox>
                                
                                </td>
                        <td></td>
					</tr>
                    <tr>
						<td class="musttitle" align="center" width="10%" ><asp:Literal ID="LitStyleID" Text="款式編號" runat="server"></asp:Literal>
                        </td>
						<td width="20%" align ="left" colspan="5" >&nbsp;
                        <asp:TextBox ID="txtStyleID" runat="server" CssClass="Textbox"></asp:TextBox>  <input id="txtStyleID1" type="hidden" runat="server" />
                        <asp:TextBox ID="txtStyleName" runat="server" CssClass="TextRead" Width="65%"
                                MaxLength="20" ></asp:TextBox>
                        </td>
						
                                     <td>
								         &nbsp;</td>
					</tr>
                
                    <tr>
						<td class="musttitle" align="center" width="10%" >
                            <asp:Literal ID="Literal13" Text="生產張數"
                                runat="server"></asp:Literal>
                        </td>
						<td width="20%" align ="left" >&nbsp;
                            <asp:textbox runat="server"  ID="txtProducePages" class="TextBox" 
                                onfocus="setFocus();" 
                                onkeypress="return regInput(this,/^\d*\.?\d{0,6}$/,String.fromCharCode(event.keyCode)) ;" 
                                ondrop="return regInput(this,/^\d*\.?\d{0,6}$/,event.dataTransfer.getData(&#39;Text&#39;));" 
                                onpaste="return regInput(this,/^\d*\.?\d{0,6}$/,window.clipboardData.getData(&#39;Text&#39;));" 
                                onblur="ProducePageBlur(this);" 
                                style="width:60%;text-align:right;padding-right:2px;" CssClass="Textbox"></asp:textbox>
                                <input  style="width:10%"id="txtMinProducePages" type="hidden" />
                        </td>
						<td class="musttitle" align="center" width="10%">
                            <asp:Literal 
                                ID="Literal14" Text="每卷張數"
                                runat="server"></asp:Literal></td>
						<td  width="20%">&nbsp;
                            <asp:textbox runat="server"  ID="txtStdPages" class="TextBox" 
                                onfocus="setFocus();" 
                                onkeypress="return regInput(this,/^\d*\.?\d{0,6}$/,String.fromCharCode(event.keyCode)) ;" 
                                ondrop="return regInput(this,/^\d*\.?\d{0,6}$/,event.dataTransfer.getData(&#39;Text&#39;));" 
                                onpaste="return regInput(this,/^\d*\.?\d{0,6}$/,window.clipboardData.getData(&#39;Text&#39;));" 
                                onblur="stdPageBlur(this);" 
                                style="width:60%;text-align:right;padding-right:2px;" CssClass="Textbox"></asp:textbox>
                            <input  style="width:10%"id="txtpages" type="hidden" />
                            </td>
                        <td  width="20%" class="smalltitle" >
                              <asp:Literal 
                                ID="Literal12" Text="生產張數"
                                runat="server"></asp:Literal>
                        </td>
                         <td  width="20%">
                             <cc1:DataText runat="server" DataValue="0" CssClass="TextRead" Width="86%" 
                                                            ID="txtVolumes">0</cc1:DataText>
                        </td>
                         <td><asp:button id="btnStyleQuery" runat="server" Text=" 款式查詢" 
                                            CssClass="but" Width="75px"  
                                
                                 
                                 OnClientClick ="window.showModalDialog('MemoQuery.aspx',null,'dialogHeight:300px;dialogWidth:600px;toolbar=no,status=yes,resizable=no');return false;" 
                                 Enabled="False" >
                                    </asp:button>
                        </td>
					</tr>
					<tr>
						<td class="musttitle" align="center" width="10%" >
                            <asp:Literal ID="litStartLabelNo" Text="標籤起始序號"
                                runat="server" ViewStateMode="Enabled"></asp:Literal>
                        </td>
						<td width="20%" align ="left" >&nbsp;
                        <asp:TextBox ID="txtStartLabelNo" runat="server" CssClass="TextRead" width="60%"></asp:TextBox>
                        </td>
						<td class="musttitle" align="center" width="10%">
                            <asp:Literal ID="Literal17" Text="標籤結束序號"
                                runat="server"></asp:Literal>
                        </td>
						<td  width="20%">&nbsp;
								<asp:textbox id="txtEndLabelNo" runat="server" CssClass="TextRead" Width="60%" 
                                MaxLength="20" ></asp:textbox>
                                
                                </td>
                         <td  width="20%" class="smalltitle" >
                              <asp:Literal 
                                ID="Literal22" Text="單況"
                                runat="server"></asp:Literal>
                        </td>
                         <td  width="20%">
                             <asp:DropDownList ID="ddlBillState" runat="server" Enabled="false" Width="35%">
                                    <asp:ListItem Value="0">無效</asp:ListItem>                                    
                                    <asp:ListItem Value="1">有效</asp:ListItem>
                                    <asp:ListItem Value="2">已入庫</asp:ListItem>
                                </asp:DropDownList>
                        </td>
                         <td><asp:button id="btnImageQuery" runat="server" Text=" 款式圖片" 
                                            CssClass="but" Width="75px" Enabled="False">
                                    </asp:button>
                        </td>
					</tr>
                    <tr>
						<td class="musttitle" align="center" width="10%" >
                            <asp:Literal ID="Literal23" Text="生產單位"
                                runat="server"></asp:Literal>
                        </td>
						<td width="20%" align ="left" >&nbsp;
                                <asp:DropDownList ID="ddlProductionUnitID" runat="server" 
                                Width="60%" ></asp:DropDownList>
                        </td>
						<td  class="smalltitle"  align="center" width="10%">
                            <asp:Literal ID="Literal24" Text="入庫日期"
                                runat="server"></asp:Literal>
                        </td>
						<td  width="20%">
                            <uc2:Calendar ID="txtPreInStockDate" runat="server" 
                                 />
                        </td>
                        <td class="smalltitle" >
                            <asp:Literal ID="Literal19" Text="排程單號"
                                runat="server"></asp:Literal>
                            </td>
                        <td >
                            <asp:textbox id="txtScheduleBillID" runat="server" CssClass="TextRead" Width="60%" 
                                MaxLength="20" ></asp:textbox>
                            </td>
                             <td><asp:button id="btnGet" runat="server" Text=" 取用序號" 
                                            CssClass="but" Width="75px" Enabled="False"></asp:button>
                                </td>
					</tr>
                   
                   
                    </table>
                    <div id='tabs'>
                       <div class='x-tab' title='明細'> 
                           <div class="fakeContainer" id="div_Detail1" style=" margin:2px;height:250px;">
                           </div> 
                       </div>

                       <div class='x-tab' title='備註'>                              
                                
                                <table class="maintable" cellspacing="0" cellpadding="0" width="100%" height= "100%" align="center" borderColor="#ffffff" border="1" >
                                 <tr>
                                <td class="smalltitle" align="center" width="15%"><asp:Literal ID="Literal20" Text="備註"
                                            runat="server"></asp:Literal></td>
                                <td colspan ="3" >&nbsp;
                                <asp:TextBox ID="txtMemo" runat="server" CssClass="Textbox" TextMode="MultiLine" 
                                        Width="98%" Height="100%" ReadOnly="True"></asp:TextBox></td>
                                </tr>

                                </table> 
                        </div>          
               </div>
			  <table class="maintable" cellspacing="0" cellpadding="0" width="100%" align="center" borderColor="#ffffff" border="1" >
                                    <tr>
						                <td class="smalltitle" align="center" width="15%" ><asp:Literal ID="ltlCreateUserName" Text="建檔人員"
                                                runat="server"></asp:Literal>
                                        </td>
						                <td width="35%">&nbsp;
								                <asp:textbox id="txtCreateUserName" runat="server" CssClass="TextRead" Width="90%" 
                                                MaxLength="20" ReadOnly="True"></asp:textbox>
                                         </td>
						                <td class="smalltitle" align="center" width="15%" >                            
                                            <asp:Literal ID="ltlCreateDate" runat="server" Text="建檔日期"></asp:Literal>							
						    
						                </td>
						                <td>&nbsp;
								                <asp:textbox id="txtCreateDate" runat="server" CssClass="TextRead" Width="90%" 
                                                MaxLength="20" ReadOnly="True"></asp:textbox></td>
					                </tr>
                                    <tr>
						                <td class="smalltitle" align="center"><asp:Literal ID="ltlLastModifyUserName" Text="異動人員"
                                                runat="server"></asp:Literal>
                                        </td>
						                <td >&nbsp;
								                <asp:textbox id="txtLastModifyUserName" runat="server" CssClass="TextRead" Width="90%" MaxLength="20" ReadOnly="true" ></asp:textbox>
                                        </td>
						                <td class="smalltitle" align="center">							
						 
                                            <asp:Literal ID="ltlLastModifyDate" Text="異動日期"
                                                runat="server"></asp:Literal>
						                </td>
						                <td >&nbsp;
                                        <asp:textbox id="txtLastModifyDate" runat="server" CssClass="TextRead" Width="90%" 
                                                MaxLength="20" ReadOnly="true"></asp:textbox></td>
					                </tr>
                                     <tr>
						                <td class="smalltitle" align="center"><asp:Literal ID="Literal6" Text="營運覆核"
                                                runat="server"></asp:Literal>
                                        </td>
						                <td >&nbsp;
								                <asp:textbox id="txtCheckUserName" runat="server" CssClass="TextRead" Width="90%" MaxLength="20" ReadOnly="true" ></asp:textbox>
                                        </td>
						                <td class="smalltitle" align="center">							
						 
                                            <asp:Literal ID="Literal7" Text="營運覆核日期"
                                                runat="server"></asp:Literal>
						                </td>
						                <td >&nbsp;
                                        <asp:textbox id="txtCheckDate" runat="server" CssClass="TextRead" Width="90%" 
                                                MaxLength="20" ReadOnly="true"></asp:textbox>&nbsp;</td>
					                </tr>
                                
                                </table> 
			<input id="HdnSubDetail1" type="hidden" runat="server" />
            <input id="Hidden1" type="hidden" runat="server" />
            <input id="Hidden2" type="hidden" runat="server" />
			          <div id="subColsName1" runat="server">
                          <%--//      colname,colwidth,type,readonly 
                        //Text="(序號) ,40      ,text,1       "--%>
                    <asp:Literal ID="sub1xxRowID" Text="(序號),60,label,1" Visible="false" runat="server"></asp:Literal>  
                    <asp:Literal ID="sub1xxVolumeNo" Text="(標籤卷編號),200,text,1" Visible="false" runat="server"></asp:Literal>  
                    <asp:Literal ID="sub1xxStartLabelNo" Text="(起始序號),150,text,1" Visible="false" runat="server"></asp:Literal>   
                    <asp:Literal ID="sub1xxEndLabelNo" Text="(結束序號),150,text,1" Visible="false" runat="server"></asp:Literal> 
                    <asp:Literal ID="sub1xxPages" Text="(張數),100,numeric_q,1" Visible="false" runat="server"></asp:Literal> 
                                               
                    </div>
         <%--                    <div id="subColsName2" runat="server">
                    <asp:Literal ID="sub2Literal3" Text="(序號),40,text,1,-1" Visible="false" runat="server"></asp:Literal>  
                    </div>--%><%--<div id="subddlstruct" runat="server">
                    <asp:DropDownList ID="ddlIsLimitedProduct" Visible ="false" 
                        runat="server">
                        <asp:ListItem Value="false">否</asp:ListItem>
                        <asp:ListItem Value="true">是</asp:ListItem>
                    </asp:DropDownList>
                     <asp:DropDownList ID="ddlIsOverOrder" Visible ="false" 
                        runat="server">
                        <asp:ListItem Value="false">不允許</asp:ListItem>
                        <asp:ListItem Value="true">允許</asp:ListItem>
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlState" Visible ="false" 
                        runat="server">
                        <asp:ListItem Value="0">未生產</asp:ListItem>
                        <asp:ListItem Value="1">已生產</asp:ListItem>
                        <asp:ListItem Value="2">結案</asp:ListItem>
                    </asp:DropDownList>
                    </div>--%>
                    <div id="msg" runat="server">
                    <asp:Literal ID="msg_subisnotNull" Text="明細不能為空!" Visible="false" runat="server"></asp:Literal> 
                    <asp:Literal ID="msg_ProducePagesMin" Text="生產張數小於最低生產量!" Visible="false" runat="server"></asp:Literal>
                     <asp:Literal ID="msg_ProducePages" Text="生產張數不為每卷張數之倍數!" Visible="false" runat="server"></asp:Literal>          
                    <asp:Literal ID="msg_choosesub" Text="請先指定明細內容!" Visible="false" runat="server"></asp:Literal>   
                    <asp:Literal ID="msg_LableNoBetween" Text="標籤序號區間段產生重複，請重新啟用序號!" Visible="false" runat="server"></asp:Literal>   
                    </div>
		</form>
	</body>
</html>

