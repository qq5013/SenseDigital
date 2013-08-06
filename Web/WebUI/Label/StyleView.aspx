<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StyleView.aspx.cs" Inherits="WebUI_Label_StyleView"  %>
<%@ Register Assembly="Js.PageControl" Namespace="Js.PageControl" TagPrefix="cc1" %>
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
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/DataProcess.js") %>'></script>
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Resize.js") %>'></script>  
        <script type="text/javascript">
            function initial() {
                var tabPanel = new Ext.TabPanel({
                    height: 370,
                    width: "100%",
                    autoTabs: true, //自动扫描页面中的div然后将其转换为标签页
                    deferredRender: false, //不进行延时渲染
                    activeTab: 0, //默认激活第一个tab页
                    animScroll: true, //使用动画滚动效果
                    enableTabScroll: true, //tab标签超宽时自动出现滚动按钮
                    applyTo: 'tabs'
                })
            }
            $(document).ready(function () {
                $("#btnStylePages").bind("click", function () {
                    var strReturn = window.showModalDialog('StylePages.aspx?FormID=<% =FormID%>&ID=' + $("#txtStyleID").val(), null, 'DialogHeight:240px;DialogWidth:800px;help:no;scroll:no');
                    
                    $('#HdnSubDetail1').val("");
                    if (strReturn != null) {
                        $('#HdnSubDetail1').val(strReturn);
                        return true;
                    }
                    return false;
                });
            });

            
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
                //window.showModalDialog('StyleImage.aspx?FormID=' + formID + '&StyleID=' + styleID + '&StyleFlag=' + styleFlag, null, 'DialogHeight:280px;DialogWidth:600px;help:no;scroll:no'); //shj
                parent.addTab('../WebUI/System/ReadMessageView.aspx?FormID=Sys_ReadMessage&ID=11', '<%=Resources.Resource.Sys_ReadMessage %>', 'tab_0_3');
                return false;
            }
            function Upload(obj) {
                var formID = "<% =FormID%>";
                var styleFlag = 0;
                if (obj.id == "btnUpload1")
                    styleFlag = 1;
                else if (obj.id == "btnUpload2")
                    styleFlag = 2;
                else if (obj.id == "btnUpload3")
                    styleFlag = 3;
                
                var styleID = $("#txtStyleID").val();
                var styleName = $("#txtStyleName").val();
                location.href = "Upload.aspx?FormID=" + formID + "&StyleFlag=" + styleFlag + "&StyleID=" + styleID + "&StyleName=" + styleName;
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
      
            <table style="width: 100%; height: 20px;" class="OperationBar">
                <tr>
                    <td align="right" style="width:60%">
                        <asp:Button ID="btnFirst" runat="server" Text="首筆" CssClass="ButtonFirst" 
                            onclick="btnFirst_Click" />
                        <asp:Button ID="btnPre" runat="server" Text="上一筆" CssClass="ButtonPre" 
                            onclick="btnPre_Click" />
                        <asp:Button ID="btnNext" runat="server" Text="下一筆" CssClass="ButtonNext" 
                            onclick="btnNext_Click" />
                        <asp:Button ID="btnLast" runat="server" Text="尾筆" CssClass="ButtonLast" 
                            onclick="btnLast_Click" />
                    </td>
                    <td align="right">
                        <asp:Button ID="btnPrint" runat="server" Text="列印" CssClass="ButtonPrint" 
                            onclick="btnPrint_Click" />
                        <asp:Button ID="btnAdd" runat="server" Text="新增" CssClass="ButtonCreate" 
                            OnClientClick="Edit(false);return false" onclick="btnAdd_Click" />
                        <asp:Button ID="btnDelete" runat="server" Text="刪除" CssClass="ButtonDel"  OnClientClick="return ViewDelete()"
                            onclick="btnDelete_Click" />
                        <asp:Button ID="btnEdit" runat="server" Text="修改" CssClass="ButtonModify" 
                            OnClientClick="return Edit(true);" onclick="btnEdit_Click" />
                        <asp:Button ID="btnExit" runat="server" Text="離開" OnClientClick="return Exit()" 
                            CssClass="ButtonExit" />
                        <asp:Button ID="btnBack" runat="server" Text="返回" OnClientClick="return Back()" 
                            CssClass="ButtonBack" />
                    </td>
                </tr>
            </table>
             <div id="surdiv" style="overflow:auto">
			<table class="maintable" cellspacing="0" cellpadding="0" width="100%" align="center" borderColor="#ffffff" border="1" >
					<tr>
						<td  valign="middle" align="left" colspan="4" height="22" class="title1">
							<p><asp:Literal ID="ltlTitle" Text="標籤款式資料[ 單筆明細畫面 ]" runat="server"></asp:Literal></p>
						</td>
					</tr>
					<tr>
						<td class="musttitle" align="center" width="15%" ><asp:Literal ID="ltlStyleID" Text="款式編號"
                                runat="server"></asp:Literal></td>
						<td width="35%" >&nbsp;
								<asp:textbox id="txtStyleID" runat="server" CssClass="TextRead" 
                                Width="70%" MaxLength="20" ReadOnly="true"></asp:textbox>                            
                            <asp:button id="btnStop" runat="server" Text="停用" 
                                            CssClass="but" Width="90px"  onclick="btnStop_Click" >
                                    </asp:button>
                        </td>
						<td class="smalltitle" align="center" width="15%" ><asp:Literal ID="ltlStopDate" Text="停用日期"
                                runat="server"></asp:Literal></td>
	                     <td width="35%" >&nbsp;
								<asp:textbox id="txtStopDate" runat="server" CssClass="TextRead" 
                                Width="60%" MaxLength="20" ReadOnly="true"></asp:textbox>
                            <input id ="Hidden1" type ="hidden" runat ="server" />
                             &nbsp;<asp:button id="btnCheck" runat="server" Text="營運覆核" 
                                            CssClass="but" Width="90px" onclick="btnCheck_Click">
                                    </asp:button>
                        </td>
					</tr>
					<tr>
						<td class="musttitle" align="center" ><asp:Literal ID="ltlStyleName" Text="款式名稱"
                                runat="server"></asp:Literal></td>
						<td width="85%" colspan="3" >&nbsp;
								<asp:textbox id="txtStyleName" runat="server" CssClass="TextRead" Width="97%" MaxLength="20" ReadOnly="true"></asp:textbox>
                            &nbsp;
                        </td>
					</tr>
                    <tr>
						<td class="musttitle" align="center" width="15%" height="22px"><asp:Literal ID="ltlVolumes" Text="大卷卷數"
                                runat="server"></asp:Literal></td>
						<td width="35%"  height="22px" style="vertical-align:top;padding:0px">
                         <table width="99%"  style="height:22px;margin:0px;padding:0px;">
                          <tr>
                           <td width="33%">&nbsp;<cc1:DataText runat="server" DataValue="0" CssClass="TextRead" Width="86%" ReadOnly="true"
                                                            ID="txtVolumes">0</cc1:DataText>
                           </td>
                            <td width="33%" class="musttitle" align="center" height="22px">
                           <asp:Literal ID="ltlStdPages" Text="每卷張數"
                                runat="server"></asp:Literal>
                           </td>
                            <td width="33%">&nbsp;<cc1:DataText runat="server" DataValue="0" CssClass="TextRead" Width="70%" ReadOnly="true"
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
                           <td width="25%">&nbsp;<cc1:DataText runat="server" DataValue="1" CssClass="TextRead" Width="90%" ReadOnly="true"
                                                            ID="txtServiceYears">1</cc1:DataText>
                           </td>
                            <td width="4%" align="left" style=" font-size:12px">
                             	<asp:Literal ID="ltlYear" runat="server" Text="年"></asp:Literal>
                           </td>
                            <td width="25%" class="musttitle"  align="center" height="22px">
                           <asp:Literal ID="ltlEnableMonths" Text="啟用期限"
                                runat="server"></asp:Literal>
                           </td>
                            <td width="30%">&nbsp;<cc1:DataText runat="server" DataValue="1" CssClass="TextRead" Width="95%" ReadOnly="true"
                                                            ID="txtEnableMonths">1</cc1:DataText>

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
								            <cc1:DataText runat="server" DataValue="0" CssClass="TextRead" Width="25%"  ReadOnly="true"
                                                            ID="txtLength">0</cc1:DataText>
                                            &nbsp;<asp:Literal 
                                            ID="ltlLength" Text="(L)"
                                            runat="server"></asp:Literal>&nbsp;<cc1:DataText runat="server" DataValue="0" CssClass="TextRead" Width="25%"  ReadOnly="true"
                                                            ID="txtWidth">0</cc1:DataText>
                                            &nbsp;<asp:Literal 
                                            ID="ltlWidth" Text="(W)"
                                            runat="server"></asp:Literal>
                                            &nbsp;<cc1:DataText runat="server" DataValue="0" CssClass="TextRead" Width="25%"  ReadOnly="true"
                                                            ID="txtHeight">0</cc1:DataText>
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
								            <asp:textbox id="txtProductionNo" runat="server" CssClass="TextRead" Width="95%" 
                                            MaxLength="20" ReadOnly="true"></asp:textbox>
								            </td>
					            </tr>
                                <tr>
						            <td  colspan="4">
                                      <table width="100%">
                                      <tr>
                                       <td width="15%"  class="smalltitle" align="center">
                                         <asp:Literal ID="ltlQR_X" runat="server" Text="QR_標籤範圍描述起點(X:"></asp:Literal>		
                                       </td>
                                         <td width="15%">&nbsp;<cc1:DataText runat="server" DataValue="0" 
                                                 CssClass="TextRead" Width="95%"  ReadOnly="true"
                                                            ID="txtQR_X" DataFormat="N2">0</cc1:DataText>
                                       </td>
                                          <td width="5%" class="smalltitle" align="center">
                                          ,(Y:)
                                       </td>
                                        <td width="15%">&nbsp;<cc1:DataText runat="server" DataValue="0" 
                                                CssClass="TextRead" Width="95%"  ReadOnly="true"
                                                            ID="txtQR_Y" DataFormat="N2">0</cc1:DataText>
                                       </td>
                                          <td width="6%" class="smalltitle" align="center">
                                          <asp:Literal ID="ltlQR_Length" runat="server" Text=",長"></asp:Literal>		
                                       </td>
                                        <td width="15%">&nbsp;<cc1:DataText runat="server" DataValue="0" 
                                                CssClass="TextRead" Width="95%"  ReadOnly="true"
                                                            ID="txtQR_Length" DataFormat="N2">0</cc1:DataText>
                                       </td>
                                          <td width="5%" class="smalltitle" align="center">
                                        <asp:Literal ID="ltlQR_Width" runat="server" Text=",寬"></asp:Literal>		
                                       </td>
                                        <td width="20%">&nbsp;<cc1:DataText runat="server" DataValue="0" CssClass="TextRead" Width="95%"  ReadOnly="true"
                                                            ID="txtQR_Width">0</cc1:DataText>
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
								            <asp:textbox id="txtImagePath" runat="server" CssClass="TextRead" Width="80%" MaxLength="20" ReadOnly="true"></asp:textbox>
                                        &nbsp;<asp:Button ID="btnUpload" runat="server" CssClass="but" OnClientClick="return Upload(this)" Text="上傳圖檔" Width="75px" />
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
								            <asp:TextBox ID="txtDescription" runat="server" CssClass="TextRead" TextMode="MultiLine" 
                                        Width="96%" Height="100" ReadOnly="True"></asp:TextBox>
                                    </td>
                                      <td >
                                        <div style="height:100px;width:90%">
                                              <a href="#"><img alt="圖片說明" id="Img1" src="" runat="server" width="400" height="100"/></a>
                                         </div>
                                    </td>
					            </tr>
                              <tr>
						            <td class="smalltitle" align="center"><asp:Literal ID="ltlNowVolumes" Text="現有卷數"
                                            runat="server"></asp:Literal></td>
						            <td >&nbsp;<cc1:DataText runat="server" DataValue="0" CssClass="TextRead" Width="90%"  ReadOnly="true"
                                                            ID="txtNowVolumes">0</cc1:DataText>
                                     </td>
						            <td class="smalltitle" align="center">                            
                                        <asp:Literal ID="ltlNowPages" runat="server" Text="現有張數"></asp:Literal>						
						            </td>
						            <td>&nbsp;<cc1:DataText runat="server" DataValue="0" CssClass="TextRead" Width="68%"  ReadOnly="true"
                                                            ID="txtNowPages">0</cc1:DataText>
                                            &nbsp;<asp:button id="btnStylePages" runat="server" Text="倉庫數量查詢" 
                                            CssClass="but" Width="90px" onclick="btnStylePages_Click" >
                                    </asp:button>

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
								                <asp:textbox id="txtAntiFakeDesc1" runat="server" CssClass="TextRead" Width="95%" MaxLength="20" ReadOnly="true" TextMode="MultiLine" Height="80"></asp:textbox>
                                        </td>
                                        </tr>
                                        <tr>
						                <td class="smalltitle" align="center" width="15%">	
                                            <asp:Literal ID="ltlImagePath1" Text="圖片說明檔"
                                                runat="server"></asp:Literal>
						                </td>
						                <td >&nbsp;
								                <asp:textbox id="txtImagePath1" runat="server" CssClass="TextRead" Width="82%" ReadOnly="true"
                                                MaxLength="20"></asp:textbox>&nbsp;<asp:button id="btnUpload1" OnClientClick="return Upload(this)" 
                                                runat="server" Text="上傳" 
                                            CssClass="but" Width="60px" ></asp:button>
                                            &nbsp;<asp:Button ID="btnOpen1" runat="server" CssClass="but" Text="開啟" OnClientClick="return Open(this)" 
                                                Width="60px" />
                                            </td>
					                </tr>                      
                                 <tr>
						                <td class="smalltitle" align="center" width="15%"><asp:Literal ID="ltlAntiFakeDesc2" Text="二級(企業用)"
                                                runat="server"></asp:Literal>
                                        </td>
						                <td width="85%">&nbsp;
								                <asp:textbox id="txtAntiFakeDesc2" runat="server" CssClass="TextRead" Width="95%" MaxLength="20" ReadOnly="true" TextMode="MultiLine" Height="80"></asp:textbox>
                                        </td>
                                        </tr>
                                        <tr>
						                <td class="smalltitle" align="center" width="15%">	
                                            <asp:Literal ID="ltlImagePath2" Text="圖片說明檔"
                                                runat="server"></asp:Literal>
						                </td>
						                <td >&nbsp;
								                <asp:textbox id="txtImagePath2" runat="server" CssClass="TextRead" Width="82%" ReadOnly="true"
                                                MaxLength="20"></asp:textbox>&nbsp;<asp:Button ID="btnUpload2" runat="server" OnClientClick="return Upload(this)" 
                                                CssClass="but" Text="上傳" Width="60px" />
                                            &nbsp;<asp:button id="btnOpen2" runat="server" Text="開啟" OnClientClick="return Open(this)" 
                                            CssClass="but" Width="60px" ></asp:button>
                                            </td>
					                </tr>   
                                    <tr>
						                <td class="smalltitle" align="center" width="15%"><asp:Literal ID="ltlAntiFakeDesc3" Text="三級(消費者)"
                                                runat="server"></asp:Literal>
                                        </td>
						                <td width="85%">&nbsp;
								                <asp:textbox id="txtAntiFakeDesc3" runat="server" CssClass="TextRead" Width="95%" MaxLength="20" ReadOnly="true" TextMode="MultiLine" Height="80"></asp:textbox>
                                        </td>
                                        </tr>
                                        <tr>
						                <td class="smalltitle" align="center" width="15%">	
                                            <asp:Literal ID="ltlImagePath3" Text="圖片說明檔"
                                                runat="server"></asp:Literal>
						                </td>
						                <td >&nbsp;
								                <asp:textbox id="txtImagePath3" runat="server" CssClass="TextRead" Width="82%" ReadOnly="true"
                                                MaxLength="20"></asp:textbox>&nbsp;<asp:Button ID="btnUpload3" runat="server" OnClientClick="return Upload(this)" 
                                                CssClass="but" Text="上傳" Width="60px" />
                                            &nbsp;<asp:button id="btnOpen3" runat="server" Text="開啟" OnClientClick="return Open(this)" 
                                            CssClass="but" Width="60px" ></asp:button>
                                            </td>
					                </tr>     
                             </table>
                        </div>          
                    </div>
					
			
		</div>
    </ContentTemplate>
            </asp:UpdatePanel> 	
            <input id="HdnSubDetail1" type="hidden" runat="server" />
		</form>
	</body>
</html>

