<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BU_UserToRoleView.aspx.cs" Inherits="WebUI_Account_BU_UserToRoleView" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="~/Styles/Main.css" type="text/css" rel="stylesheet" /> 
    <link href="~/Styles/op.css" type="text/css" rel="stylesheet" /> 

    <script type="text/javascript" src='<%=ResolveUrl("~/Scripts/JQuery/jquery-1.8.3.min.js") %>'></script>
    <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Commons.js") %>'></script>
     
    <script type="text/javascript">
        function AssignRole() {
            var userID = $("#ddlUserID").val();
            location.href = "BU_UserToRoleEdit.aspx?FormID=<%= FormID%>&UserID=" + userID
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
                        <asp:Button ID="btnDelete" runat="server" Text="刪除" CssClass="ButtonDel" OnClientClick="return ViewDelete()"
                            onclick="btnDelete_Click" />
                        <asp:Button ID="btnEdit" runat="server" Text="指派角色" CssClass="ButtonModify" 
                            OnClientClick="return AssignRole();" onclick="btnEdit_Click" />
                        <asp:Button ID="btnExit" runat="server" Text="離開" OnClientClick="Exit()" CssClass="ButtonExit" />
                        <asp:Button ID="btnBack" runat="server" Text="返回" OnClientClick="return Back()" 
                            CssClass="ButtonBack" />
                    </td>
                </tr>
            </table>
        <table class="maintable" cellspacing="0" cellpadding="0" width="100%" align="center" bordercolor="#ffffff" border="1" >			
        <tr>						
                        <td class="musttitle" width="10%"  align="center" >
                            <asp:Literal 
                                ID="ltlUserName" Text="用戶帳號"
                                runat="server"></asp:Literal>
                        </td>
						<td  width="35%">&nbsp;
								<asp:DropDownList ID="ddlUserID" runat="server" Width="90%">
                                   
                            </asp:DropDownList>
                         </td>    
                         <td class="smalltitle" align="center" ><asp:Literal 
                                ID="ltlUserLevel" Text="用戶等級"
                                runat="server"></asp:Literal>
                        </td>
						<td  width="35%">&nbsp;
							<asp:DropDownList runat="server" ID="ddlUserLevel"  Width="90%" >
                                <asp:ListItem Text="一般用戶"></asp:ListItem>
                                <asp:ListItem Text="管理用戶"></asp:ListItem>
                                <asp:ListItem Text="超級用戶"></asp:ListItem>
                            </asp:DropDownList>
                         </td>                     
					</tr>
                    
                    <tr>
						<td class="smalltitle" align="center"><asp:Literal ID="ltlLastModifyUserName" Text="異動人員"
                                runat="server"></asp:Literal></td>
						<td >&nbsp;
								<asp:textbox id="txtLastModifyUserName" runat="server" CssClass="TextRead" Width="90%" MaxLength="20" ReadOnly="true" ></asp:textbox>
                        </td>
						<td class="smalltitle" align="center"> <asp:Literal ID="ltlLastModifyDate" Text="異動日期"
                                runat="server"></asp:Literal>							
						</td>
						<td >&nbsp;
                        <asp:textbox id="txtLastModifyDate" runat="server" CssClass="TextRead" Width="90%" 
                                MaxLength="20" ReadOnly="true"></asp:textbox></td>
					</tr>
			</table>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                    SkinID="GridViewSkin" Width="600px" 
            onrowdatabound="GridView1_RowDataBound">
            <Columns>
                <asp:BoundField DataField="RoleID" HeaderText="角色ID" >
                    <ItemStyle HorizontalAlign="Left" Width="18%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                 <asp:BoundField DataField="RoleName" HeaderText="角色名稱" >
                    <ItemStyle HorizontalAlign="Left" Width="18%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="RoleUserLevel" HeaderText="用戶等級" >
                    <ItemStyle HorizontalAlign="Left" Width="18%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>    
            </Columns>
            <PagerSettings Visible="False" />
        </asp:GridView>
    </div>
    </ContentTemplate>
            </asp:UpdatePanel>
    </form>
</body>
</html>


