<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AnnounceMessageView.aspx.cs" Inherits="WebUI_System_AnnounceMessageView" %>

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
                        <asp:Button ID="btnAdd" runat="server" Text="新增" CssClass="ButtonCreate" OnClientClick="Edit(false);return false" />
                        <asp:Button ID="btnDelete" runat="server" Text="刪除" CssClass="ButtonDel" OnClientClick="return ViewDelete()"
                            onclick="btnDelete_Click" />
                        <asp:Button ID="btnExit" runat="server" Text="離開" OnClientClick="Exit()" CssClass="ButtonExit" />
                    </td>
                </tr>
            </table>
			<table class="maintable" cellspacing="0" cellpadding="0" width="100%" align="center" borderColor="#ffffff" border="1" >
					<tr>
						<td  valign="middle" align="left" colspan="4" height="22" class="title1">
							<p><asp:Literal ID="ltlTitle" Text="發佈公告[ 單筆明細畫面 ]" runat="server"></asp:Literal></p>
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
                                ID="ltlAnnounceUnitNo" Text="單位編號"
                                runat="server"></asp:Literal></td>
						<td  width="35%">&nbsp;
								<asp:textbox id="txtAnnounceUnitNo" runat="server" CssClass="TextRead" Width="90%" 
                                MaxLength="20" ReadOnly="true"></asp:textbox>
                        </td>
					</tr>
                    
					<tr>
						<td class="smalltitle" align="center" ><asp:Literal ID="ltlAnnounceID" Text="公告&nbsp;&nbsp;ID"
                                runat="server"></asp:Literal></td>
						<td width="35%" >&nbsp;
								<asp:textbox id="txtAnnounceID" runat="server" CssClass="TextRead" Width="90%" MaxLength="20" ReadOnly="true"></asp:textbox>
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
                    
			</table>
            <div id="table-container" style="overflow: auto; WIDTH: 100%; HEIGHT: 400px">                
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                    SkinID="GridViewSkin" Width="100%" 
                    onrowdatabound="GridView1_RowDataBound" >
            <Columns>                       
                <asp:BoundField DataField="ReceiverUserName" HeaderText="公告對象" >
                    <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField> 
                <asp:BoundField DataField="ReceiverFlag" HeaderText="公告單位" >
                    <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="ReceiveUnitNo" HeaderText="公告單位編號" >
                    <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>    
                <asp:BoundField DataField="Receiver" HeaderText="公告人員">
                    <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                <asp:CheckBoxField DataField="IsRead" HeaderText="是否閱讀" 
                    SortExpression="IsRead">
                    <ItemStyle HorizontalAlign="Center" Width="8%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:CheckBoxField>
                <asp:BoundField DataField="ReadDate" HeaderText="閱讀日期" HtmlEncode="false" DataFormatString="{0:yyyy/MM/dd HH:mm}"
                    meta:resourcekey="BoundFieldResource15">
                    <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="ReplyDate" HeaderText="回覆日期" HtmlEncode="false" DataFormatString="{0:yyyy/MM/dd HH:mm}"
                    meta:resourcekey="BoundFieldResource15">
                    <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="ReplyTitle" HeaderText="回覆標題">
                    <ItemStyle HorizontalAlign="Left" Width="10%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="ReplyContent" HeaderText="回覆內容">
                    <ItemStyle HorizontalAlign="Left" Width="15%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
            </Columns>
            <PagerSettings Visible="False" />
        </asp:GridView>
        </div>
			</div>
            </ContentTemplate>
            </asp:UpdatePanel> 
		</form>
	</body>
</html>

