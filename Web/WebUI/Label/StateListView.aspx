<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StateListView.aspx.cs" Inherits="WebUI_Label_StateListView" %>
<%@ Register src="../../Controls/Calendar.ascx" tagname="Calendar" tagprefix="uc2" %>
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

            function initial() { }
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
                        <asp:Button ID="btnAdd" runat="server" Text="新增" CssClass="ButtonCreate" OnClientClick="Edit(false);return false" onclick="btnAdd_Click"/>
                        <asp:Button ID="btnDelete" runat="server" Text="刪除" CssClass="ButtonDel" OnClientClick="return ViewDelete()"
                            onclick="btnDelete_Click" />
                        <asp:Button ID="btnEdit" runat="server" Text="修改" CssClass="ButtonModify" 
                            OnClientClick="return Edit(true);" onclick="btnEdit_Click" />
                        <asp:Button ID="btnExit" runat="server" Text="離開" OnClientClick="return Exit()" CssClass="ButtonExit" />
                         <asp:Button ID="btnBack" runat="server" Text="返回" OnClientClick="return Back()" 
                            CssClass="ButtonBack" />
                    </td>
                </tr>
            </table>
             <div id="surdiv" style="overflow:auto">
				<table class="maintable" cellspacing="0" cellpadding="0" width="100%" align="center" borderColor="#ffffff" border="1" >
					<tr>
						<td  valign="middle" align="left" colspan="4" height="22" class="title1">
							<p><asp:Literal ID="ltlTitle" Text="標籤狀態清單[ 單筆明細畫面 ]" runat="server"></asp:Literal></p>
						</td>
					</tr>
						<tr>
						<td class="musttitle" align="center" width="15%" ><asp:Literal ID="ltlStyleID" Text="代碼"
                                runat="server"></asp:Literal></td>
						<td width="35%" >&nbsp;
								<asp:textbox id="txtStateListID" runat="server" CssClass="TextRead" ReadOnly="false" 
                                Width="90%" MaxLength="2" ></asp:textbox>
                        </td>
						<td class="musttitle" align="center">&nbsp;</td>
						<td >&nbsp;
								<asp:CheckBox ID="chkImageProvider" runat="server" Text="認証圖片提供"  Enabled="false"/>
                         </td>
                                
					</tr>
                    <tr><td class="musttitle" align="center">                            
                            <asp:Literal ID="Literal2" runat="server" Text="狀態"></asp:Literal>						
						</td>
						<td>&nbsp;
								<asp:TextBox ID="txtStateName" runat="server" CssClass="TextRead" 
                                MaxLength="2" Width="90%"></asp:TextBox>
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
                                CssClass="TextRead" Width="60%" 
                                                MaxLength="20" ReadOnly="True"></asp:textbox>
                                                &nbsp;
                            <asp:button id="btnStop" runat="server" Text="停用" 
                                            CssClass="but" Width="75px" onclick="btnStop_Click">
                                    </asp:button>
                                </td>
					</tr>
                    
                  
                    <tr>
						            <td class="smalltitle" align="center" ><asp:Literal ID="Literal17" Text="說明"
                                            runat="server"></asp:Literal></td>
						            <td width="85%" colspan="3" >&nbsp;
								            <asp:TextBox ID="txtDescription" runat="server"  CssClass="TextRead" 
                                            ReadOnly="True"  TextMode="MultiLine" 
                                        Width="98%" Height="100" MaxLength="300"  ></asp:TextBox>
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
								                <asp:textbox id="txtCheckDate" runat="server" CssClass="TextRead" Width="60%" ReadOnly="true"
                                                MaxLength="20"></asp:textbox>&nbsp;
                                            <asp:button id="btnCheck" runat="server" Text="營運覆核" 
                                            CssClass="but" Width="75px" onclick="btnCheck_Click">
                                    </asp:button>
                                            </td>
					                </tr>                 
                   
                    </table>	
                    </div>			
				    </ContentTemplate>
            </asp:UpdatePanel> 
		</form>
	</body>
</html>

