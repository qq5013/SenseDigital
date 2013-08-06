<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReadMessageView.aspx.cs" Inherits="WebUI_System_ReadMessageView" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head id="Head1" runat="server">
		<meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
  
    <title></title> 
        <link href="~/Styles/Main.css" type="text/css" rel="stylesheet" /> 
        <link href="~/Styles/op.css" type="text/css" rel="stylesheet" /> 
        <script type="text/javascript" src='<%=ResolveUrl("~/Scripts/JQuery/jquery-1.8.3.min.js") %>'></script> 
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Commons.js") %>'></script>
        <script type="text/javascript">
            function Reply() {
                var page = "ReplyMessageEdit.aspx";
                var AnnounceID = $("#txtAnnounceID").val();
                var strReturn = window.showModalDialog('TempPage.aspx?page=' + page + '&AnnounceID=' + AnnounceID + '&FormID=<% =FormID%>&TitleName=' + escape('回覆公告'), window, 'DialogHeight:200px;DialogWidth:600px;help:no;scroll:no');
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
        <div id="processMessage"> Loading...<br /><br />
             <img alt="Loading" src="../../images/main/loading.gif" />
        </div>            
 
        </ProgressTemplate>
 
    </asp:UpdateProgress>  
                <asp:UpdatePanel ID="updatePanel" runat="server" UpdateMode="Conditional">                
                <ContentTemplate>
                <div>
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
                        <asp:Button ID="btnReply" runat="server" Text="回覆公告" CssClass="ButtonCreate" OnClientClick="return Reply()" />
                        <asp:Button ID="btnExit" runat="server" Text="離開" OnClientClick="Exit()" CssClass="ButtonExit" />
                    </td>
                </tr>
            </table>
			<table class="maintable" cellspacing="0" cellpadding="0" width="100%" align="center" borderColor="#ffffff" border="1" >
					<tr>
						<td  valign="middle" align="left" colspan="4" height="22" class="title1">
							<p><asp:Literal ID="ltlTitle" Text="閱讀公告[ 單筆明細畫面 ]" runat="server"></asp:Literal></p>
						</td>
					</tr>
					<tr>
						<td class="smalltitle" align="center" width="15%" ><asp:Literal ID="ltlAnnounceFlag" Text="發佈單位"
                                runat="server"></asp:Literal></td>
						<td width="35%" >&nbsp;
								<asp:textbox id="txtAnnounceFlag" runat="server" CssClass="TextRead" 
                                Width="90%" MaxLength="20" ReadOnly="true"></asp:textbox>                            
                        </td>
						<td class="smalltitle" align="center" width="15%"><asp:Literal 
                                ID="ltlAnnouncer" Text="發佈人員"
                                runat="server"></asp:Literal></td>
						<td  width="35%">&nbsp;
								<asp:textbox id="txtAnnouncer" runat="server" CssClass="TextRead" Width="90%" 
                                MaxLength="20" ReadOnly="true"></asp:textbox>
                        </td>
					</tr>
                    
					<tr>
						<td class="smalltitle" align="center" ><asp:Literal ID="ltlAnnounceID" Text="公告&nbsp;&nbsp;ID"
                                runat="server"></asp:Literal></td>
						<td width="85%" colspan="3" >&nbsp;
								<asp:textbox id="txtAnnounceID" runat="server" CssClass="TextRead" Width="96%" MaxLength="20" ReadOnly="true"></asp:textbox>
                        </td>
					</tr>
					
                    <tr>
						<td class="smalltitle" align="center" width="15%" ><asp:Literal ID="ltlSource" Text="來&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;源"
                                runat="server"></asp:Literal></td>
						<td width="35%" >&nbsp;
								<asp:textbox id="txtSource" runat="server" CssClass="TextRead" 
                                Width="90%" MaxLength="20" ReadOnly="true"></asp:textbox>
                        </td>
						<td class="smalltitle" align="center" width="15%"><asp:Literal 
                                ID="ltlAnnouncerUserName" Text="發佈使用者"
                                runat="server"></asp:Literal></td>
						<td  width="35%">&nbsp;
								<asp:textbox id="txtAnnouncerUserName" runat="server" CssClass="TextRead" Width="90%" 
                                MaxLength="20" ReadOnly="true"></asp:textbox>
                        </td>
					</tr>
                    <tr>
						<td class="smalltitle" align="center" ><asp:Literal ID="ltlATitle" Text="發佈標題"
                                runat="server"></asp:Literal></td>
						<td  width="85%" colspan="3">&nbsp;
								<asp:textbox id="txtTitle" runat="server" CssClass="TextRead" Width="96%" MaxLength="100"
									ForeColor="Black" ReadOnly="true"></asp:textbox>
                         </td>
					</tr>
                    <tr>
						<td class="smalltitle" align="center" ><asp:Literal 
                                ID="ltlContents" Text="發佈內容"
                                runat="server"></asp:Literal></td>
						<td  width="85%" colspan="3" height="70">&nbsp;
								<asp:textbox id="txtContents" runat="server" CssClass="TextRead" Width="96%" MaxLength="100" TextMode="MultiLine"
									Height="59px" ForeColor="Black" ReadOnly="true"></asp:textbox>
                         </td>
					</tr>
                    <tr>
						<td class="smalltitle" align="center" width="15%" ><asp:Literal ID="ltlReceiverFlag" Text="回覆單位"
                                runat="server"></asp:Literal></td>
						<td width="35%" >&nbsp;
								<asp:textbox id="txtReceiverFlag" runat="server" CssClass="TextRead" 
                                Width="90%" MaxLength="20" ReadOnly="true"></asp:textbox>
                            <asp:TextBox ID="txtReceiverFlag0" runat="server" CssClass="TextRead" 
                                MaxLength="20" ReadOnly="true" Width="0px"></asp:TextBox>
                        </td>
						<td class="smalltitle" align="center" width="15%"><asp:Literal 
                                ID="ltlReceiveUnitNo" Text="回覆單位編號"
                                runat="server"></asp:Literal></td>
						<td  width="35%">&nbsp;
								<asp:textbox id="txtReceiveUnitNo" runat="server" CssClass="TextRead" Width="90%" 
                                MaxLength="20" ReadOnly="true"></asp:textbox>
                        </td>
					</tr>
                    <tr>
						<td class="smalltitle" align="center" width="15%" ><asp:Literal ID="ltlReceiverUserName" Text="回覆使用者"
                                runat="server"></asp:Literal></td>
						<td width="35%" >&nbsp;
								<asp:textbox id="txtReceiverUserName" runat="server" CssClass="TextRead" 
                                Width="90%" MaxLength="20" ReadOnly="true"></asp:textbox>
                        </td>
						<td class="smalltitle" align="center" width="15%"><asp:Literal 
                                ID="ltlReceiver" Text="回覆人員"
                                runat="server"></asp:Literal></td>
						<td  width="35%">&nbsp;
								<asp:textbox id="txtReceiver" runat="server" CssClass="TextRead" Width="90%" 
                                MaxLength="20" ReadOnly="true"></asp:textbox>
                        </td>
					</tr>
                    <tr>
						<td class="smalltitle" align="center" ><asp:Literal ID="ltlReplyTitle" Text="回覆標題"
                                runat="server"></asp:Literal></td>
						<td  width="85%" colspan="3">&nbsp;
								<asp:textbox id="txtReplyTitle" runat="server" CssClass="TextRead" Width="96%" MaxLength="100"
									ForeColor="Black" ReadOnly="true"></asp:textbox>
                         </td>
					</tr>
                    <tr>
						<td class="smalltitle" align="center" ><asp:Literal 
                                ID="ltlReplyContent" Text="回覆內容"
                                runat="server"></asp:Literal></td>
						<td  width="85%" colspan="3" height="70">&nbsp;
								<asp:textbox id="txtReplyContent" runat="server" CssClass="TextRead" Width="96%" MaxLength="100" TextMode="MultiLine"
									Height="59px" ForeColor="Black" ReadOnly="true"></asp:textbox>
                         </td>
					</tr>
			</table>
			</div>
            </ContentTemplate>
            </asp:UpdatePanel> 
		</form>
	</body>
</html>


