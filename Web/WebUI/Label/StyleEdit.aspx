<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StyleEdit.aspx.cs" Inherits="WebUI_Label_StyleEdit" %>
<%@ Register Assembly="EditableDropDownList" Namespace="EditableControls" TagPrefix="editable" %>
<%@ Register Assembly="Js.PageControl" Namespace="Js.PageControl" TagPrefix="cc1" %>
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
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/DataProcess.js") %>'></script>
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Ajax.js") %>'></script>   
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Resize.js") %>'></script>  
        <script type="text/javascript">
            $(document).ready(function () {
                content_resize();
//                $('#txtEnterpriseID').bind('dblclick', function () { GetOtherJsonValue('BU_Enterprise', '','EnterpriseID,EnterpriseName'); });
//                $('#txtEnterpriseID').bind('change', function () { getBaseData('BU_Enterprise', '',this.value, 'EnterpriseID,EnterpriseName'); });
     
                var tabPanel = new Ext.TabPanel({
                    height: 370,
                    width: "100%",
                    autoTabs: true, //自动扫描页面中的div然后将其转换为标签页
                    deferredRender: false, //不进行延时渲染
                    activeTab: 0, //默认激活第一个tab页
                    animScroll: true, //使用动画滚动效果
                    enableTabScroll: true, //tab标签超宽时自动出现滚动按钮
                    applyTo: 'tabs'
                });
            });
            function Save() {
                $("#txtStyleID").val(trim($("#txtStyleID").val()));
                $("#txtStyleName").val(trim($("#txtStyleName").val()));
                $("#txtVolumes").val(trim($("#txtVolumes").val()));
                $("#EddlAntiCounterfeitType").val(trim($("#EddlAntiCounterfeitType").val()));

                if ($("#txtStyleID").val() == "") {
                    alert("<%=Resources.Resource.LB_StyleID_NotNull %>");
                    $("#txtStyleID").focus();
                    return false;
                }
                if ($("#txtStyleName").val() == "") {
                    alert("<%=Resources.Resource.LB_StyleName_NotNull %>");
                    $("#txtStyleName").focus();
                    return false;
                }
                if ($("#txtVolumes").val() == "") {
                    alert("<%=Resources.Resource.LB_Volumes_NotNull %>");
                    $("#txtVolumes").focus();
                    return false;
                }
                if ($("#txtStdPages").val() == "") {
                    alert("<%=Resources.Resource.LB_StdPages_NotNull %>");
                    $("#txtStdPages").focus();
                    return false;
                }
                if ($("#txtServiceYears").val() == "") {
                    alert("<%=Resources.Resource.LB_ServiceYears_NotNull %>");
                    $("#txtServiceYears").focus();
                    return false;
                }
                if ($("#ddlEnableMonths").val() == "") {
                    alert("<%=Resources.Resource.LB_EnableMonths_NotNull %>");
                    $("#ddlEnableMonths").focus();
                    return false;
                }
                if ("<%= ID%>" == "") {
                    var exist = IsExists("<%= FormID%>",$("#txtStyleID").val(), "<%= cnKey%>");
                    if (exist) {
                        alert("<%=ltlStyleID.Text %><%=Resources.Resource.Exists %>");
                        $("#txtStyleID").focus();
                        return false;
                    }
                }
                return true;
            }
            function Open(obj) {
                var formID = "<% =FormID%>";
                var styleID = $("#txtStyleID").val();
                var styleFlag = 0;
                if (obj.id == "btnOpen1")
                    styleFlag = 1;
                else if (obj.id == "btnOpen2")
                    styleFlag = 2;
                else if (obj.id == "btnOpen3")
                    styleFlag = 3;
                window.showModalDialog('StyleImage.aspx?FormID=' + formID + '&StyleID=' + styleID + '&StyleFlag=' + styleFlag, null, 'DialogHeight:280px;DialogWidth:600px;help:no;scroll:no'); //shj
                return false;
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
							<p><asp:Literal ID="ltlTitle" Text="標籤款式資料[ 單筆編輯畫面 ]" runat="server"></asp:Literal></p>
						</td>
					</tr>
					<tr>
						<td class="musttitle" align="center" width="15%" ><asp:Literal ID="ltlStyleID" Text="款式編號"
                                runat="server"></asp:Literal></td>
						<td width="35%" >&nbsp;
							<asp:textbox id="txtStyleID" runat="server" CssClass="TextBox" 
                                Width="90%" MaxLength="2" ></asp:textbox>
								<asp:textbox id="txtID" runat="server" CssClass="TextBox" 
                                Width="5%" MaxLength="20" Visible ="false" ReadOnly="true"></asp:textbox>
                        </td>
						<td class="smalltitle" align="center" width="15%" ><asp:Literal ID="ltlStopDate" Text="停用日期"
                                runat="server"></asp:Literal></td>
	                     <td width="35%" >&nbsp;
								<asp:textbox id="txtStopDate" runat="server" CssClass="TextRead" 
                                Width="90%" MaxLength="20" ReadOnly="true"></asp:textbox>
                            <input id ="Hidden1" type ="hidden" runat ="server" />
                        
                        </td>
					</tr>
					<tr>
						<td class="musttitle" align="center" ><asp:Literal ID="ltlStyleName" Text="款式名稱"
                                runat="server"></asp:Literal></td>
						<td width="85%" colspan="3" >&nbsp;
								<asp:textbox id="txtStyleName" runat="server" CssClass="TextBox" Width="97%" MaxLength="20" ></asp:textbox>
                            &nbsp;
                        </td>
					</tr>
                    <tr>
						<td class="musttitle" align="center" width="15%" height="22px"><asp:Literal ID="ltlVolumes" Text="大卷卷數"
                                runat="server"></asp:Literal></td>
						<td width="35%"  height="22px" style="vertical-align:top;padding:0px">
                         <table width="99%"  style="height:22px;margin:0px;padding:0px;">
                          <tr>
                           <td width="33%">&nbsp;<cc1:DataText runat="server" DataValue="0" CssClass="TextBox" Width="86%" 
                                                            ID="txtVolumes">0</cc1:DataText>
                           
                         
                           </td>
                            <td width="33%" class="musttitle" align="center" height="22px">
                           <asp:Literal ID="ltlStdPages" Text="每卷張數"
                                runat="server"></asp:Literal>
                           </td>
                            <td width="33%">&nbsp;<cc1:DataText runat="server" DataValue="0" CssClass="TextBox" Width="86%" 
                                                            ID="txtStdPages">0</cc1:DataText>
                          
                           </td>
                          </tr>
                        
                        </table>
								
                         </td>
						<td class="musttitle" align="center" width="15%" height="22px">                            
                            <asp:Literal ID="ltlServiceYears" runat="server" Text="標籤使用年限"></asp:Literal>						
						</td>
						<td width="35%" height="22px" style="vertical-align:top;padding:0px">
						<table width="99%"  style="height:22px;margin:0px;padding:0px;">
                          <tr>
                           <td width="30%">&nbsp;<cc1:DataText runat="server" DataValue="1" CssClass="TextBox" Width="95%" 
                                                            ID="txtServiceYears">1</cc1:DataText>
                        
                           </td>
                            <td width="4%" align="left" style=" font-size:12px">
                             	<asp:Literal ID="ltlYear" runat="server" Text="年"></asp:Literal>
                           </td>
                            <td width="25%" class="musttitle"  align="center" height="22px">
                           <asp:Literal ID="ltlEnableMonths" Text="啟用期限"
                                runat="server"></asp:Literal>
                           </td>
                            <td width="30%">
                            <asp:DropDownList ID="ddlEnableMonths" Width="95%" runat="server">
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
                              <td width="8%"  align="left" style=" font-size:12px">
                             	<asp:Literal ID="ltlMonth" runat="server" Text="月"></asp:Literal>
                           </td>
                          </tr>
                        
                        </table>
								</td>
					</tr>                 
                    
                    </table>
                    <div id='tabs'>
                        <div class='x-tab' title='基本資料'>  
                                         
                            <table class="maintable" cellspacing="0" cellpadding="0" width="100%" align="center" borderColor="#ffffff" border="1" >

						        <tr>
						            <td class="smalltitle" align="center" width="15%" ><asp:Literal ID="ltlImageLocation" Text="圖像定位"
                                            runat="server"></asp:Literal></td>
						            <td width="35%" >&nbsp;
								            <asp:DropDownList ID="ddlImageLocation" Width="90%" runat="server">
                            </asp:DropDownList>
                                    </td>
						            <td class="smalltitle" align="center" width="15%"><asp:Literal 
                                            ID="ltlSize" Text="尺寸(mm)"
                                            runat="server"></asp:Literal></td>
						            <td  width="35%">&nbsp;
								            <cc1:DataText runat="server" DataValue="0" CssClass="TextBox" Width="25%" 
                                                            ID="txtLength_R">0</cc1:DataText>
                                            &nbsp;<asp:Literal 
                                            ID="ltlLength" Text="(L)"
                                            runat="server"></asp:Literal>&nbsp<cc1:DataText runat="server" DataValue="0" CssClass="TextBox" Width="25%" 
                                                            ID="txtWidth_R">0</cc1:DataText>
                                            &nbsp;<asp:Literal 
                                            ID="ltlWidth" Text="(W)"
                                            runat="server"></asp:Literal>
                                            &nbsp;<cc1:DataText runat="server" DataValue="0" CssClass="TextBox" Width="25%" 
                                                            ID="txtHeight_R">0</cc1:DataText>
                                        &nbsp;<asp:Literal 
                                            ID="ltlHeight" Text="(H)"
                                            runat="server"></asp:Literal>
                                    </td>
					            </tr>
                                <tr>
						            <td class="smalltitle" align="center"><asp:Literal ID="ltlQRContent" Text="QR 內容"
                                            runat="server"></asp:Literal></td>
						            <td >&nbsp;
								            <asp:DropDownList ID="ddlQRContent" Width="90%" runat="server">
                                        </asp:DropDownList>
                                     </td>
						            <td class="smalltitle" align="center">                            
                                        <asp:Literal ID="ltlProductionNo" runat="server" Text="生產規格書編號"></asp:Literal>						
						            </td>
						            <td>&nbsp;
								            <asp:textbox id="txtProductionNo" runat="server" CssClass="TextBox" Width="95%" 
                                            MaxLength="20" ></asp:textbox>
								            </td>
					            </tr>
                                <tr>
						            <td  colspan="4">
                                      <table width="100%">
                                      <tr>
                                       <td width="15%"  class="smalltitle" align="center">
                                         <asp:Literal ID="ltlQR_X" runat="server" Text="QR_標籤範圍描述起點(X:"></asp:Literal>		
                                       </td>
                                         <td width="15%">&nbsp;<cc1:DataText runat="server" DataValue="0" CssClass="TextBox" Width="95%" 
                                                            ID="txtQR_X_R" DataFormat="N2">0</cc1:DataText>
                                         
                                       </td>
                                          <td width="5%" class="smalltitle" align="center">
                                          ,(Y:)
                                       </td>
                                        <td width="15%">&nbsp;<cc1:DataText runat="server" DataValue="0" CssClass="TextBox" Width="95%" 
                                                            ID="txtQR_Y_R">0</cc1:DataText>
                                       </td>
                                          <td width="6%" class="smalltitle" align="center">
                                          <asp:Literal ID="ltlQR_Length" runat="server" Text=",長"></asp:Literal>		
                                       </td>
                                        <td width="15%">&nbsp;<cc1:DataText runat="server" DataValue="0" CssClass="TextBox" Width="95%" 
                                                            ID="txtQR_Length_R">0</cc1:DataText>
                                        
                                       </td>
                                          <td width="5%" class="smalltitle" align="center">
                                        <asp:Literal ID="ltlQR_Width" runat="server" Text=",寬"></asp:Literal>		
                                       </td>
                                        <td width="20%">&nbsp;<cc1:DataText runat="server" DataValue="0" CssClass="TextBox" Width="95%" 
                                                            ID="txtQR_Width_R">0</cc1:DataText>
                                       </td>
                                          <td width="5%" align="center">
                                       )
                                       </td>
                                      </tr>
                                      </table>
								    </td>
					            </tr>
                                               
                                <tr>
						            <td class="smalltitle" align="center" ><asp:Literal ID="ltlImagePath" Text="款式圖片"
                                            runat="server"></asp:Literal></td>
						            <td width="85%" colspan="2" >&nbsp;
								            <asp:textbox id="txtImagePath" runat="server" CssClass="TextRead" Width="96%" MaxLength="20" ReadOnly="true"></asp:textbox>
                                       
                                    </td>
                                    <td>         
                                       <table width="100%">
                                         <tr>
                                          <td width="40%" class="smalltitle" align="center"><asp:Literal ID="ltlStopUserName" Text="停用人員"
                                            runat="server"></asp:Literal>                                           
                                          </td>
                                          <td width="60%">
                                            <asp:textbox id="txtStopUserName" runat="server" CssClass="TextRead" Width="95%" 
                                            MaxLength="20" ReadOnly="true"></asp:textbox>
                                          </td>
                                         </tr>
                                       </table>           
                                       				
						            </td>
					            </tr>
                                <tr>
						            <td class="smalltitle" align="center" ><asp:Literal ID="ltlDescription" Text="描述說明"
                                            runat="server"></asp:Literal></td>
						            <td width="85%" colspan="2" >&nbsp;
								            <asp:TextBox ID="txtDescription" runat="server" CssClass="TextBox" TextMode="MultiLine" 
                                        Width="96%" Height="100"></asp:TextBox>
                                    </td>
                                      <td >
                                        <div style="height:100px;width:90%">
                                              <a href="#"><img alt="圖片說明" id="Img1" src="" runat="server"/></a>
                                         </div>
                                    </td>
					            </tr>
                              <tr>
						            <td class="smalltitle" align="center"><asp:Literal ID="ltlNowVolumes" Text="現有卷數"
                                            runat="server"></asp:Literal></td>
						            <td >&nbsp;<cc1:DataText runat="server" DataValue="0" CssClass="TextRead" Width="90%" ReadOnly="True"
                                                            ID="txtNowVolumes">0</cc1:DataText>
								          
                                     </td>
						            <td class="smalltitle" align="center">                            
                                        <asp:Literal ID="ltlNowPages" runat="server" Text="現有張數"></asp:Literal>						
						            </td>
						            <td>&nbsp;<cc1:DataText runat="server" DataValue="0" CssClass="TextRead" Width="90%" ReadOnly="True"
                                                            ID="txtNowPages">0</cc1:DataText>
								           
								            </td>
					            </tr>
                                  <tr>
						                <td class="smalltitle" align="center"><asp:Literal ID="ltlCreateUserName" Text="建檔人員"
                                                runat="server"></asp:Literal>
                                        </td>
						                <td width="35%">&nbsp;
								                <asp:textbox id="txtCreateUserName" runat="server" CssClass="TextRead" Width="90%" 
                                                MaxLength="20" ReadOnly="True"></asp:textbox>
                                         </td>
						                <td class="smalltitle" align="center" width="15%">                            
                                            <asp:Literal ID="ltlCreateDate" runat="server" Text="建檔日期"></asp:Literal>							
						    
						                </td>
						                <td width="35%">&nbsp;
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
						                <td class="smalltitle" align="center" width="15%">	
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

                        <div class='x-tab' title='防偽識別描述'>
                               <table class="maintable" cellspacing="0" cellpadding="0" width="100%" align="center" borderColor="#ffffff" border="1" >
                                    <tr>
						                <td class="smalltitle" align="center" width="15%"><asp:Literal ID="ltlAntiFakeDesc1" Text="一級(營運用)"
                                                runat="server"></asp:Literal>
                                        </td>
						                <td width="85%">&nbsp;
								                <asp:textbox id="txtAntiFakeDesc1" runat="server" CssClass="TextBox" Width="95%" MaxLength="20" TextMode="MultiLine" Height="50"></asp:textbox>
                                        </td>
                                        </tr>
                                        <tr>
						                <td class="smalltitle" align="center" width="15%">	
                                            <asp:Literal ID="ltlImagePath1" Text="圖片說明檔"
                                                runat="server"></asp:Literal>
						                </td>
						                <td >&nbsp;
								                <asp:textbox id="txtImagePath1" runat="server" CssClass="TextRead" Width="85%" ReadOnly="true"
                                                MaxLength="20"></asp:textbox>&nbsp;<asp:button id="btnOpen1" runat="server" Text="開啟" OnClientClick="return Open(this)"
                                            CssClass="but" Width="90px" ></asp:button>
                                            </td>
					                </tr>                      
                                 <tr>
						                <td class="smalltitle" align="center" width="15%"><asp:Literal ID="ltlAntiFakeDesc2" Text="二級(企業用)"
                                                runat="server"></asp:Literal>
                                        </td>
						                <td width="85%">&nbsp;
								                <asp:textbox id="txtAntiFakeDesc2" runat="server" CssClass="TextBox" Width="95%" MaxLength="20"  TextMode="MultiLine" Height="50"></asp:textbox>
                                        </td>
                                        </tr>
                                        <tr>
						                <td class="smalltitle" align="center" width="15%">	
                                            <asp:Literal ID="ltlImagePath2" Text="圖片說明檔"
                                                runat="server"></asp:Literal>
						                </td>
						                <td >&nbsp;
								                <asp:textbox id="txtImagePath2" runat="server" CssClass="TextRead" Width="85%" ReadOnly="true"
                                                MaxLength="20"></asp:textbox>&nbsp;<asp:button id="btnOpen2" runat="server" Text="開啟" OnClientClick="return Open(this)"
                                            CssClass="but" Width="90px" ></asp:button>
                                            </td>
					                </tr>   
                                    <tr>
						                <td class="smalltitle" align="center" width="15%"><asp:Literal ID="ltlAntiFakeDesc3" Text="三級(消費者)"
                                                runat="server"></asp:Literal>
                                        </td>
						                <td width="85%">&nbsp;
								                <asp:textbox id="txtAntiFakeDesc3" runat="server" CssClass="TextBox" Width="95%" MaxLength="20"  TextMode="MultiLine" Height="50"></asp:textbox>
                                        </td>
                                        </tr>
                                        <tr>
						                <td class="smalltitle" align="center" width="15%">	
                                            <asp:Literal ID="ltlImagePath3" Text="圖片說明檔"
                                                runat="server"></asp:Literal>
						                </td>
						                <td >&nbsp;
								                <asp:textbox id="txtImagePath3" runat="server" CssClass="TextRead" Width="85%" ReadOnly="true"
                                                MaxLength="20"></asp:textbox>&nbsp;<asp:button id="btnOpen3" runat="server" Text="開啟" OnClientClick="return Open(this)"
                                            CssClass="but" Width="90px" ></asp:button>
                                            </td>
					                </tr>     
                             </table>
                        </div>          
                    </div>
                       
				</div>	
			
		
         </ContentTemplate>
            </asp:UpdatePanel> 
		</form>
	</body>
</html>