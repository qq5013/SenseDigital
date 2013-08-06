<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StateListEdit.aspx.cs" Inherits="WebUI_Label_StateListEdit" %>
<%@ Register src="../../Controls/Calendar.ascx" tagname="Calendar" tagprefix="uc2" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head id="Head1" runat="server">
		<meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
  
    <title></title> 
        <link href="~/Styles/Main.css" type="text/css" rel="stylesheet" /> 
        <link href="~/Styles/op.css" type="text/css" rel="stylesheet" /> 
        <link href="~/JScript/EditDropDownList/css/jquery-ui.css" rel="stylesheet" type="text/css" />
         <link href="~/ext-3.3.1/resources/css/ext-all.css" rel="stylesheet" type="text/css" />
 	    <script type="text/javascript" src='<%=ResolveUrl("~/ext-3.3.1/ext-base.js") %>'></script>
        <script type="text/javascript" src='<%=ResolveUrl("~/ext-3.3.1/ext-all.js") %>'></script>

        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/EditDropDownList/Js/jquery-1.6.4.min.js") %>'></script> 
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/EditDropDownList/Js/jquery.ui.core.js") %>'></script> 
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/EditDropDownList/Js/jquery.ui.widget.js") %>'></script> 
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/EditDropDownList/Js/jquery.ui.button.js") %>'></script> 
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/EditDropDownList/Js/jquery.ui.position.js") %>'></script> 
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/EditDropDownList/Js/jquery.ui.autocomplete.js") %>'></script> 
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/EditDropDownList/Js/jquery.ui.combobox.js") %>'></script> 
        
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Commons.js") %>'></script>
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Ajax.js") %>'></script>   
        <script type="text/javascript">
//            $(document).ready(function () {

//                var tabPanel = new Ext.TabPanel({
//                    height: 300,
//                    width: "100%",
//                    autoTabs: true, //自动扫描页面中的div然后将其转换为标签页
//                    deferredRender: false, //不进行延时渲染
//                    activeTab: 0, //默认激活第一个tab页
//                    animScroll: true, //使用动画滚动效果
//                    enableTabScroll: true, //tab标签超宽时自动出现滚动按钮
//                    applyTo: 'tabs'
//                });
            //            });
            function Save() {
                $("#txtStateListID").val(trim($("#txtStateListID").val()));
                $("#txtStateName").val(trim($("#txtStateName").val()));

                if ($("#txtStateListID").val() == "") {
                    alert("<%=ltlStyleID.Text %>" + "<%=Resources.Resource.NotNull %>");
                    $("#txtStateListID").focus();
                    return false;
                }                

                if ("<%= ID%>" == "") {
                    var exist = IsExists("<%= FormID%>", $("#txtStateListID").val(), "<%= cnKey%>");
                    if (exist) {
                        alert("<%=ltlStyleID.Text %>" + "<%=Resources.Resource.Exists %>");
                        $("#txtStateListID").focus();
                        return false;
                    }
                }
                return true;
            }
        </script>
	</head>
	<body>
		<form id="form1" runat="server">
            <table style="width: 100%; height: 20px;" class="OperationBar">
                <tr>
                    <td align="right">
                        <asp:Button ID="btnCancel" runat="server" Text="放棄" OnClientClick="return Cancel()" CssClass="ButtonCancel" />
                        <asp:Button ID="btnSave" runat="server" Text="保存" OnClientClick="return Save()" 
                            CssClass="ButtonSave" onclick="btnSave_Click" />
                        <asp:Button ID="btnExit" runat="server" Text="離開" OnClientClick=" return Exit()" CssClass="ButtonExit" />
                    </td>
                </tr>
            </table>
             <div id="surdiv" style="overflow:auto">
			<table class="maintable" cellspacing="0" cellpadding="0" width="100%" align="center" borderColor="#ffffff" border="1" >
					<tr>
						<td  valign="middle" align="left" colspan="4" height="22" class="title1">
							<p><asp:Literal ID="ltlTitle" Text="標籤狀態清單[ 單筆編輯畫面 ]" runat="server"></asp:Literal></p>
						</td>
					</tr>
						<tr>
						<td class="musttitle" align="center" width="15%" ><asp:Literal ID="ltlStyleID" Text="代碼"
                                runat="server"></asp:Literal></td>
						<td width="35%" >&nbsp;
								<asp:textbox id="txtStateListID" runat="server" CssClass="TextBox" 
                                Width="90%" MaxLength="2"></asp:textbox>
                        </td>
						<td class="musttitle" align="center">&nbsp;</td>
						<td >&nbsp;
								<asp:CheckBox ID="chkImageProvider" runat="server" Text="認証圖片提供"/>
                         </td>
					</tr>
                    <tr>
                    <td class="musttitle" align="center">                            
                            <asp:Literal ID="Literal2" runat="server" Text="狀態"></asp:Literal>						
						</td>
						<td>&nbsp;
								<asp:textbox id="txtStateName" runat="server" CssClass="TextBox" 
                                Width="90%" MaxLength="2"></asp:textbox>
								</td>
						<td class="musttitle" align="center"><asp:Literal ID="Literal1" Text="類型"
                                runat="server"></asp:Literal></td>
						<td >&nbsp;
								<asp:DropDownList ID="ddlStyle" Width="90%" runat="server" Enabled ="false">
                                    <asp:ListItem Value="0">系統內建</asp:ListItem>
                                    <asp:ListItem Value="1">使用者自建</asp:ListItem>
                            </asp:DropDownList>
                         </td>
						
					</tr>
					<tr>
						
						<td class="smalltitle" align="center">                            
                            <asp:Literal ID="Literal4" runat="server" Text="停用人員"></asp:Literal>						
						</td>
						<td>&nbsp;
								                <asp:textbox id="txtStopUserName" runat="server" 
                                CssClass="TextRead" Width="90%" 
                                                MaxLength="20" ReadOnly="True"></asp:textbox>
								</td>
                                <td class="smalltitle" align="center" width="15%"><asp:Literal 
                                ID="ltlEnterpriseID" Text="停用日期"
                                runat="server"></asp:Literal></td>
						<td  width="35%">&nbsp;&nbsp;<asp:textbox id="txtStopDate" runat="server" 
                                CssClass="TextRead" Width="90%" 
                                                MaxLength="20" ReadOnly="True"></asp:textbox>
                                </td>
					</tr>
                    
                  
                    <tr>
						            <td class="smalltitle" align="center" ><asp:Literal ID="Literal17" Text="說明"
                                            runat="server"></asp:Literal></td>
						            <td width="85%" colspan="3" >&nbsp;
								            <asp:TextBox ID="txtMemo" runat="server" CssClass="Text" TextMode="MultiLine" 
                                        Width="98%" Height="100" MaxLength="300" ></asp:TextBox>
                                    </td>
					            </tr>
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
						                <td class="smalltitle" align="center"><asp:Literal ID="ltlCheckUserName" Text="營運覆核"
                                                runat="server"></asp:Literal>
                                        </td>
						                <td >&nbsp;
								                <asp:textbox id="txtCheckUserName" runat="server" CssClass="TextRead" Width="90%" MaxLength="20" ReadOnly="true"></asp:textbox>
                                        </td>
						                <td class="smalltitle" align="center">	
                                            <asp:Literal ID="ltlCheckDate" Text="營運覆核日期"
                                                runat="server"></asp:Literal>
						                </td>
						                <td >&nbsp;
								                <asp:textbox id="txtCheckDate" runat="server" CssClass="TextRead" Width="90%" ReadOnly="true"
                                                MaxLength="20"></asp:textbox>&nbsp;
                                            </td>
					                </tr>
                 
                   
                    </table>				
			
			</div>
		</form>
	</body>
</html>
