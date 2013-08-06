<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EnterpriseEdit.aspx.cs" Inherits="WebUI_BusinessUnit_EnterpriseEdit" culture="auto" meta:resourcekey="PageResource3" uiculture="auto"  %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
        <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />  
        <title></title> 
        <link href="~/Styles/Main.css" type="text/css" rel="stylesheet" /> 
        <link href="~/Styles/op.css" type="text/css" rel="stylesheet" /> 
        <link href="~/Styles/Detail.css" type="text/css" rel="stylesheet" /> 
        <link href="~/Styles/superTables.css" type="text/css" rel="stylesheet" /> 
        <link href="~/ext-3.3.1/resources/css/ext-all.css" rel="stylesheet" type="text/css" />
        <script type="text/javascript" src='<%=ResolveUrl("~/Scripts/JQuery/jquery-1.8.3.min.js") %>'></script>
 	    <script type="text/javascript" src='<%=ResolveUrl("~/ext-3.3.1/ext-base.js") %>'></script>
        <script type="text/javascript" src='<%=ResolveUrl("~/ext-3.3.1/ext-all.js") %>'></script>
           
        
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Resize.js") %>'></script>  
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Detail.js") %>'></script>
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/superTables.js") %>'></script>
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Scrolltable.js") %>'></script>

        <script type="text/javascript" src="Js/Enterprise.js"></script>
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Commons.js") %>'></script>
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Ajax.js") %>'></script>
        <script type="text/javascript">
            $(document).ready(function () {
                content_resize();
                $("#txtAddress").dblclick(function () { load(this); });
                subData[0] = jQuery.parseJSON($('#HdnSubDetail1').val());
                initialTable("<%=Session["language_session"] %>");
                AddToDetail();
                isShowColumn("Detail1_Col_SubID", false);

                $('#btnAddDetail').bind('click', function () { doOption('Add', 1); });
                $('#btnDelDetail').bind('click', function () { doOption('Del'); });
                $('#btnInsDetail').bind('click', function () { doOption('Ins', 1); });
                var tabPanel = new Ext.TabPanel({
                    height: 400,
                    width: "100%",
                    autoTabs: true, //自动扫描页面中的div然后将其转换为标签页
                    deferredRender: false, //不进行延时渲染
                    activeTab: 0, //默认激活第一个tab页
                    animScroll: true, //使用动画滚动效果
                    enableTabScroll: true, //tab标签超宽时自动出现滚动按钮
                    applyTo: 'tabs'
                });
            });
            function load(obj) {
                returnvalue = window.showModalDialog('../../TempAddress.aspx', window, 'dialogWidth:500px;dialogHeight:200px;help:no;status:no;scroll:auto;Resizable:yes;');
                if (returnvalue != null) {
                    var part = returnvalue.split("@#$");
                    if (obj.id == "txtAddress") {
                        document.getElementById("txtAddress").value = part[0];
                        document.getElementById("txtZipNo").value = part[1];
                    }
                    else {
                        document.getElementById("txtHomeAddress").value = part[0];
                        //document.getElementById("txtZipNo").value = part[1];
                    }
                }
            }
            function Save() {
                //主檔驗證提示
                if ($.trim($("input[id=txtEnterpriseID]").val()) == "") {
                    "<%=Resources.Resource.BU_EnterpriseID_NotNull %>"
                    $("input[id=txtEnterpriseID]").focus();
                    return false;
                }
                if ($.trim($("#ddlCategoryID").val()) == "") {
                    alert("<%=Resources.Resource.BU_CategoryID_NoNull %>");
                    $("#ddlCategoryID").focus();
                    return false;
                }
                if ($.trim($("input[id=txtEnterpriseName]").val()) == "") {
                    alert("<%=Resources.Resource.BU_EnterpriseName_NotNull %>");
                    $("input[id=txtEnterpriseName]").focus();
                    return false;
                }
                if ($.trim($("input[id=txtEnterpriseEName]").val()) == "") {
                    alert("<%=Resources.Resource.BU_EnterpriseEName_NotNull %>");
                    $("input[id=txtEnterpriseEName]").focus();
                    return false;
                }
                if ($.trim($("input[id=txtEnterpriseSName]").val()) == "") {
                    alert("<%=Resources.Resource.BU_EnterpriseSName_NotNull %>");
                    $("input[id=txtEnterpriseSName]").focus();
                    return false;
                }
                if ($.trim($("input[id=txtUnionID]").val()) == "") {
                    alert("<%=Resources.Resource.BU_UnionID_NotNull %>");
                    $("input[id=txtUnionID]").focus();
                    return false;
                }

                if ("<%= ID%>" == "") {
                    var exist = IsExists("<%= FormID%>", $("#txtEnterpriseID").val());
                    if (exist) {
                        alert("<%=Resources.Resource.BU_EnterpriseID_Exists %>");
                        $("#txtEnterpriseID").focus();
                        return false;
                    }
                }
                //子檔驗證提示
                var table = $("#" + tbTableId[0] + "_sDataTable");
                var rowCount = table.find('tr').length - 1;
                
                for (var i = 0; i < rowCount; i++) {
                    var rowIndex = i + 1;
                    if ($.trim($("#" + tbRowName[0] + "_" + rowIndex + "_LinkMan").val()) == "") {
                        alert("<%=Resources.Resource.BU_Detail_LinkMan_NoNull %>");
                        $("#" + tbRowName[0] + "_" + rowIndex + "_LinkMan").focus();
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
    <asp:UpdateProgress ID="updateprogress" runat="server" AssociatedUpdatePanelID="updatePanel">
    <ProgressTemplate>            
             <div id="progressBackgroundFilter" style="display:none"></div>
        <div id="processMessage"> Loading...<br />
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
                        <asp:Button ID="btnCancel" runat="server" Text="放棄" 
                              OnClientClick="return Cancel()" CssClass="ButtonCancel" 
                              meta:resourcekey="btnCancelResource2" />
                        <asp:Button ID="btnSave" runat="server" Text="保存" OnClientClick="return Save()" 
                            CssClass="ButtonSave" onclick="btnSave_Click" 
                              meta:resourcekey="btnSaveResource2" />
                        <asp:Button ID="btnExit" runat="server" Text="離開" OnClientClick="return Exit()" 
                              CssClass="ButtonExit" meta:resourcekey="btnExitResource2" />
                    </td>
                </tr>
            </table>
            <div id="surdiv" style="overflow:auto">
			<table class="maintable" cellspacing="0" cellpadding="0" width="100%" align="center" bordercolor="#ffffff"	border="1" >			
					<tr>
						<td  valign="middle" align="left" height="22" class="title1" 
                           >
							<p><asp:Literal ID="ltlTitle" Text="企業用戶资料[ 單筆編輯畫面 ]" runat="server" 
                                    meta:resourcekey="ltlTitleResource1"></asp:Literal></p>
						</td>
					</tr>							
			</table>
            <div id='tabs'>
                <div class='x-tab' title='基本資料'> 
            <table class="maintable" cellspacing="0" cellpadding="0" width="100%" align="center" bordercolor="#ffffff"	border="1" >			
					<tr>
						<td class="musttitle" align="center" width="15%" >
                            <asp:Literal ID="ltlEnterpriseID" Text="企業編號"
                                runat="server" meta:resourcekey="ltlEnterpriseIDResource1"></asp:Literal>
                        </td>
						<td width="35%" >&nbsp;
								<asp:textbox id="txtEnterpriseID" runat="server" CssClass="TextBox" 
                                Width="90%" MaxLength="6" meta:resourcekey="txtEnterpriseIDResource1"></asp:textbox>
                        </td>
						<td class="musttitle" align="center" width="15%">
                            <asp:Literal 
                                ID="ltlCategoryID" Text="企業類別"
                                runat="server" meta:resourcekey="ltlCategoryIDResource1"></asp:Literal>
                        </td>
						<td  width="35%">&nbsp;
								<asp:DropDownList ID="ddlCategoryID" Width="90%" runat="server" 
                                meta:resourcekey="ddlCategoryIDResource1">
                            </asp:DropDownList>
                        </td>
					</tr>
                    		
					<tr>
						<td class="musttitle" align="center" width="15%" >
                            <asp:Literal ID="ltlEnterpriseName" Text="企業名稱"
                                runat="server" meta:resourcekey="ltlEnterpriseNameResource1"></asp:Literal>
                        </td>
						<td width="85%"  colspan="3">&nbsp;
								<asp:textbox id="txtEnterpriseName" runat="server" CssClass="TextBox" 
                                Width="96%" MaxLength="100" meta:resourcekey="txtEnterpriseNameResource1"></asp:textbox>
                        </td>
						
					</tr>
                    <tr>
						<td class="musttitle" align="center" width="15%" >
                            <asp:Literal ID="ltlEnterpriseEName" Text="企業英文名稱"
                                runat="server" meta:resourcekey="ltlEnterpriseENameResource1"></asp:Literal>
                        </td>
						<td width="85%"  colspan="3">&nbsp;
								<asp:textbox id="txtEnterpriseEName" runat="server" CssClass="TextBox" 
                                Width="96%" MaxLength="100" meta:resourcekey="txtEnterpriseENameResource1"></asp:textbox>
                        </td>
						
					</tr>
                     <tr>
						<td class="musttitle" align="center" width="15%" >
                            <asp:Literal ID="ltlEnterpriseSName" Text="企業簡稱"
                                runat="server" meta:resourcekey="ltlEnterpriseSNameResource1"></asp:Literal>
                        </td>
						<td width="85%"  colspan="3">&nbsp;
								<asp:textbox id="txtEnterpriseSName" runat="server" CssClass="TextBox" 
                                Width="96%" MaxLength="40" meta:resourcekey="txtEnterpriseSNameResource1"></asp:textbox>
                        </td>
						
					</tr>
                    <tr>
						<td class="musttitle" align="center" width="15%" >
                            <asp:Literal ID="ltlUnionID" Text="官方編號"
                                runat="server" meta:resourcekey="ltlUnionIDResource1"></asp:Literal>
                        </td>
						<td width="35%" >&nbsp;
								<asp:textbox id="txtUnionID" runat="server" CssClass="TextBox" Width="90%" 
                                MaxLength="20" meta:resourcekey="txtUnionIDResource1"></asp:textbox>
                        </td>
						<td class="smalltitle" align="center" width="15%">
                            <asp:Literal ID="ltlPhone" runat="server" meta:resourcekey="ltlPhoneResource1" 
                                Text="公司電話"></asp:Literal>
                        </td>
						<td  width="35%">&nbsp;
                          		<asp:TextBox ID="txtPhone" runat="server" CssClass="TextBox" MaxLength="20" 
                                meta:resourcekey="txtPhoneResource1" Width="90%"></asp:TextBox>
                          		</td>
					</tr>
                    <tr>
						<td class="smalltitle" align="center" width="15%" >
                            <asp:Literal ID="ltlPresident" Text="公司負責人"
                                runat="server" meta:resourcekey="ltlPresidentResource1"></asp:Literal>
                        </td>
						<td width="35%" >&nbsp;
								<asp:textbox id="txtPresident" runat="server" CssClass="TextBox" 
                                Width="90%" MaxLength="20" meta:resourcekey="txtPresidentResource1"></asp:textbox>

                        </td>
						<td class="smalltitle" align="center" width="15%">
                            <asp:Literal ID="ltlFax" runat="server" meta:resourcekey="ltlFaxResource1" 
                                Text="公司傳真"></asp:Literal>
                        </td>
						<td  width="35%">&nbsp;
								<asp:TextBox ID="txtFax" runat="server" CssClass="TextBox" MaxLength="20" 
                                meta:resourcekey="txtFaxResource1" Width="90%"></asp:TextBox>
								</td>
					</tr>
                    

                    <tr>
						<td class="smalltitle" align="center">
                            <asp:Literal ID="ltlWebUrl" Text="公司網址"
                                runat="server" meta:resourcekey="ltlWebUrlResource1"></asp:Literal>
                        </td>
						<td width="85%" colspan="3" >&nbsp;
								<asp:textbox id="txtWebUrl" runat="server" CssClass="TextBox" Width="96%" 
                                MaxLength="100" meta:resourcekey="txtWebUrlResource1"></asp:textbox>
                        </td>
					</tr>
                    <tr>
						<td class="smalltitle" align="center">
                            <asp:Literal ID="ltlAddress" Text="公司地址"
                                runat="server" meta:resourcekey="ltlAddressResource1"></asp:Literal>
                        </td>
						<td width="85%" colspan="3">&nbsp;
								<asp:textbox id="txtAddress" runat="server" CssClass="TextBox" Width="80%" 
                                MaxLength="100" meta:resourcekey="txtAddressResource1"></asp:textbox>
                            &nbsp;<asp:TextBox ID="txtZipNo" runat="server" CssClass="TextBox" 
                                MaxLength="5" meta:resourcekey="txtAddressResource1" Width="15%"></asp:TextBox>
                        </td>
					</tr>
					
                      <tr>
						<td class="smalltitle" align="center" width="15%" >
                            <asp:Literal ID="ltlServiceYears" runat="server" Text="標籤使用年限"></asp:Literal>
                          </td>
						<td width="35%" >&nbsp;
								<asp:TextBox ID="txtServiceYears" runat="server" CssClass="TextBox" 
                                MaxLength="20" Width="90%"></asp:TextBox>
								</td>
						<td class="smalltitle" align="center" width="15%">
                            <asp:Literal ID="ltlEnableMonths" runat="server" Text="標籤啟用期限"></asp:Literal>
                          </td>
						<td  width="35%">&nbsp;
								<asp:DropDownList ID="ddlEnableMonths" runat="server" Width="88%">
                                    <asp:ListItem>1</asp:ListItem>
                                    <asp:ListItem>2</asp:ListItem>
                                    <asp:ListItem>3</asp:ListItem>
                                    <asp:ListItem>4</asp:ListItem>
                                    <asp:ListItem>5</asp:ListItem>
                                    <asp:ListItem>6</asp:ListItem>
                                    <asp:ListItem>7</asp:ListItem>
                                    <asp:ListItem>8</asp:ListItem>
                                    <asp:ListItem>9</asp:ListItem>
                                    <asp:ListItem>10</asp:ListItem>
                                    <asp:ListItem>11</asp:ListItem>
                                    <asp:ListItem>12</asp:ListItem>
                            </asp:DropDownList>
								</td>
					</tr>
                    <tr>
						<td class="smalltitle" align="center">
                            <asp:Literal ID="ltlCreateUserName" Text="建檔人員"
                                runat="server" meta:resourcekey="ltlCreateUserNameResource1"></asp:Literal>
                        </td>
						<td >&nbsp;
								<asp:textbox id="txtCreateUserName" runat="server" CssClass="TextRead" Width="90%" 
                                MaxLength="20" ReadOnly="True" 
                                meta:resourcekey="txtCreateUserNameResource1"></asp:textbox>
                         </td>
						<td class="smalltitle" align="center">                            
                            <asp:Literal ID="ltlCreateDate" runat="server" Text="建檔日期" 
                                meta:resourcekey="ltlCreateDateResource1"></asp:Literal>							
						    
						</td>
						<td>&nbsp;
								<asp:textbox id="txtCreateDate" runat="server" CssClass="TextRead" Width="90%" 
                                MaxLength="20" ReadOnly="True" meta:resourcekey="txtCreateDateResource1"></asp:textbox></td>
					</tr>
                    <tr>
						<td class="smalltitle" align="center">
                            <asp:Literal ID="ltlLastModifyUserName" Text="異動人員"
                                runat="server" meta:resourcekey="ltlLastModifyUserNameResource1"></asp:Literal>
                        </td>
						<td >&nbsp;
								<asp:textbox id="txtLastModifyUserName" runat="server" CssClass="TextRead" 
                                Width="90%" MaxLength="20" ReadOnly="True" 
                                meta:resourcekey="txtLastModifyUserNameResource1" ></asp:textbox>
                        </td>
						<td class="smalltitle" align="center">							
						 
                            <asp:Literal ID="ltlLastModifyDate" Text="異動日期"
                                runat="server" meta:resourcekey="ltlLastModifyDateResource1"></asp:Literal>
						</td>
						<td >&nbsp;
                        <asp:textbox id="txtLastModifyDate" runat="server" CssClass="TextRead" Width="90%" 
                                MaxLength="20" ReadOnly="True" 
                                meta:resourcekey="txtLastModifyDateResource1"></asp:textbox></td>
					</tr>
                    <tr>
						<td class="smalltitle" align="center">
                            <asp:Literal ID="ltlCheckUserName" Text="覆核人員"
                                runat="server" meta:resourcekey="ltlCheckUserNameResource1"></asp:Literal>
                        </td>
						<td >&nbsp;
								<asp:textbox id="txtCheckUserName" runat="server" CssClass="TextRead" 
                                Width="90%" MaxLength="20" ReadOnly="True" 
                                meta:resourcekey="txtCheckUserNameResource1"></asp:textbox>
                        </td>
						<td class="smalltitle" align="center">	
                            <asp:Literal ID="ltlCheckDate" Text="覆核日期"
                                runat="server" meta:resourcekey="ltlCheckDateResource1"></asp:Literal>
						</td>
						<td >&nbsp;
								<asp:textbox id="txtCheckDate" runat="server" CssClass="TextRead" 
                                Width="90%" ReadOnly="True"
                                MaxLength="20" meta:resourcekey="txtCheckDateResource1"></asp:textbox>                           
                        </td>
					</tr>
                   
			</table>
                <div class='x-tab' title='連絡人'> 
                    <table style="width:100%">
                        <tr>
                            <td class="table_titlebgcolor" height="25px">
                                <input id="btnAddDetail" class="ButtonCss" type="button" value="新增明細" style="width:60px;"  />
                                <input id="btnDelDetail" class="ButtonCss" type="button" value="刪除明細" style="width:60px;" />
                                <input id="btnInsDetail" class="ButtonCss" type="button" value="插入明細"  style="width:60px;" />
                            </td>
                        </tr>
                    </table> 

                    <div class="fakeContainer" id="div_Detail1" style=" margin:5px;height:300px;">
                    </div>
                </div>

                <div class='x-tab' title='備註'>   
                <asp:TextBox ID="txtMemo" runat="server" CssClass="Text" TextMode="MultiLine" MaxLength="1000"
                        Width="98%" Height="300px"></asp:TextBox>
                </div>          
            </div>
        </div>
        <input id="HdnSubDetail1" type="hidden" runat="server" />
          </input>
          </input>
          </input>
          </input>
          </input>
          </input>
          </input>
          </input>
          </input>
          </input>
    </div>
    </ContentTemplate>
            </asp:UpdatePanel> 
    </form>
</body>
</html>
