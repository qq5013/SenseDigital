<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReplyMessageEdit.aspx.cs" Inherits="WebUI_System_ReplyMessageEdit" %>

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
                $("#txtTitle").val(trim($("#txtTitle").val()));
                $("#txtContents").val(trim($("#txtContents").val()));
                if ($("#txtTitle").val() == "") {
                    alert("<%=Resources.Resource.Sys_Title_NotNull %>");
                    $("#txtTitle").focus();
                    return false;
                }
                if ($("#txtContents").val() == "") {
                    alert("<%=Resources.Resource.Sys_Content_NotNull %>");
                    $("#txtContents").focus();
                    return false;
                }
                return true;
            }
        </script>
	</head>
	<body >
		<form id="form1" runat="server">        
      
			<table id="Table1" class="maintable" cellspacing="0" cellpadding="0" width="100%" align="center" bordercolor="#ffffff"
				border="1" runat="server">			
					<tr>
						<td class="smalltitle" align="center" width="15%" ><asp:Literal ID="ltlReceiverFlag" Text="回覆單位"
                                runat="server"></asp:Literal></td>
						<td width="35%" >&nbsp;
								<asp:textbox id="txtReceiverFlag" runat="server" CssClass="TextRead" 
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
						<td class="smalltitle" align="center" width="15%"><asp:Literal 
                                ID="ltlReceiverUserName" Text="回覆使用者"
                                runat="server"></asp:Literal></td>
						<td  width="35%">&nbsp;
								<asp:textbox id="txtAnnouncerUserName" runat="server" CssClass="TextRead" Width="90%" 
                                MaxLength="20" ReadOnly="true"></asp:textbox>
                        </td>
                        <td class="smalltitle">
                        </td>
                        <td>
                        </td>
					</tr>
                    <tr>
						<td class="smalltitle" align="center" ><asp:Literal ID="ltlReplyTitle" Text="回覆標題"
                                runat="server"></asp:Literal></td>
						<td  width="85%" colspan="3">&nbsp;
								<asp:textbox id="txtReplyTitle" runat="server" CssClass="TextBox" Width="96%" MaxLength="100"
									ForeColor="Black"></asp:textbox>
                         </td>
					</tr>
                    <tr>
						<td class="smalltitle" align="center" ><asp:Literal 
                                ID="ltlReplyContent" Text="回覆內容"
                                runat="server"></asp:Literal></td>
						<td  width="85%" colspan="3" height="70">&nbsp;
								<asp:textbox id="txtReplyContent" runat="server" CssClass="TextBox" Width="96%" 
                                MaxLength="100" TextMode="MultiLine"
									Height="59px" ForeColor="Black"></asp:textbox>
                         </td>
					</tr>
                    <tr>
						<td class="smalltitle" align="center" colspan="4" >

                            <asp:Button ID="btnReply" OnClientClick="return Reply()" runat="server" 
                                CssClass="but" Text="回覆公告" onclick="btnReply_Click" />

                         </td>
					</tr>
			</table>
        
		</form>
	</body>
</html>

