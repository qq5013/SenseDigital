<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AnnounceMessageEdit.aspx.cs" Inherits="WebUI_System_AnnounceMessageEdit" %>

<%@ Register src="../../Controls/Calendar.ascx" tagname="Calendar" tagprefix="uc2" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head id="Head1" runat="server">
		<meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />  
        <title></title> 
        <link href="~/Styles/Main.css" type="text/css" rel="stylesheet" /> 
        <link href="~/Styles/op.css" type="text/css" rel="stylesheet" /> 
        <script type="text/javascript" src='<%=ResolveUrl("~/Scripts/JQuery/jquery-1.8.3.min.js") %>'></script>        
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Commons.js") %>'></script>       
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Resize.js") %>'></script>  
        <script type="text/javascript">
            $(document).ready(function () {
                content_resize();
            });
            function Save() {
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
                var ctl = document.getElementById("GridView1");
                var checkbox = ctl.getElementsByTagName('input');

                for (var i = 0; i < checkbox.length; i++) {
                    if (checkbox[i].type == 'checkbox') {
                        if (checkbox[i].checked)
                            return true;
                    }
                }
                alert("<%=Resources.Resource.Sys_Receiver_NotNull %>"); 
                return false;
            }
        </script>
	</head>
	<body >
		<form id="form1" runat="server">
        <table style="width: 100%; height: 20px;" class="OperationBar">
                <tr>
                    <td align="right">
                        <asp:Button ID="btnCancel" runat="server" Text="放棄" OnClientClick="return Cancel()" CssClass="ButtonCancel" />
                        <asp:Button ID="btnSave" runat="server" Text="保存" OnClientClick="return Save()" 
                            CssClass="ButtonSave" onclick="btnSave_Click" />
                        <asp:Button ID="btnExit" runat="server" Text="離開" OnClientClick="return Exit()" CssClass="ButtonExit" />
                    </td>
                </tr>
            </table>
             <div id="surdiv" style="overflow:auto">
			<table id="Table1" class="maintable" cellspacing="0" cellpadding="0" width="100%" align="center" bordercolor="#ffffff"
				border="1" runat="server">			
					<tr>
						<td  valign="middle" align="left" colspan="4" height="22" class="title1">
							<p><asp:Literal ID="ltlTitle" Text="發佈公告[ 單筆編輯畫面 ]" runat="server"></asp:Literal></p>
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
						<td class="smalltitle" align="center" ><asp:Literal ID="ltlAnnounceID" Text="公告&nbsp;&nbsp;&nbsp;ID"
                                runat="server"></asp:Literal></td>
						<td >&nbsp;
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
								<asp:textbox id="txtTitle" runat="server" CssClass="TextBox" Width="96%" MaxLength="100"
									ForeColor="Black"></asp:textbox>
                         </td>
					</tr>
                    <tr>
						<td class="smalltitle" align="center" ><asp:Literal 
                                ID="ltlContents" Text="發佈內容"
                                runat="server"></asp:Literal></td>
						<td  width="85%" colspan="3" height="70">&nbsp;
								<asp:textbox id="txtContents" runat="server" CssClass="TextBox" Width="96%" 
                                MaxLength="100" TextMode="MultiLine"
									Height="59px" ForeColor="Black"></asp:textbox>
                         </td>
					</tr>
			</table>
			<div id="table-container" style="overflow: auto; WIDTH: 100%; HEIGHT: 400px">                
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                    SkinID="GridViewSkin" Width="800px"                                 
                    onrowdatabound="GridView1_RowDataBound">
            <Columns>
                <asp:TemplateField meta:resourcekey="TemplateFieldResource3">
                    <HeaderTemplate>
                    <input type="checkbox" onclick="javascript:selectAll('GridView1',this.checked);" />                    
                    </HeaderTemplate>
                    <ItemTemplate>
                    <asp:CheckBox id="cbSelect" runat="server"></asp:CheckBox>                   
                    </ItemTemplate>
                  <HeaderStyle HorizontalAlign="Center" Width="60px"></HeaderStyle>
                 <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
               </asp:TemplateField>              
                <asp:BoundField DataField="ReceiverUserName" HeaderText="公告對象" >
                    <ItemStyle HorizontalAlign="Left" Width="20%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField> 
                <asp:BoundField DataField="ReceiverFlag" HeaderText="公告單位" >
                    <ItemStyle HorizontalAlign="Left" Width="25%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="ReceiveUnitNo" HeaderText="公告單位編號" >
                    <ItemStyle HorizontalAlign="Left" Width="25%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>    
                <asp:BoundField DataField="Receiver" HeaderText="公告人員">
                    <ItemStyle HorizontalAlign="Left" Width="20%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
            </Columns>
            <PagerSettings Visible="False" />
        </asp:GridView>
        </div>
        </div>
		</form>
	</body>
</html>
