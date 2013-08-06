<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BU_UserToRoleEdit.aspx.cs" Inherits="WebUI_Account_BU_UserToRoleEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="~/Styles/Main.css" type="text/css" rel="stylesheet" /> 
    <link href="~/Styles/op.css" type="text/css" rel="stylesheet" /> 

    <script type="text/javascript" src='<%=ResolveUrl("~/Scripts/JQuery/jquery-1.8.3.min.js") %>'></script>
    <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Commons.js") %>'></script>
     
    <script type="text/javascript">

        function eExit() {
            if (confirm("<%=Resources.Resource.Question_Exit %>"))
                Exit();
        }
        function Save() {
//            var allInput = document.getElementsByTagName("input");
//            var haveCheck = false;
//            for (var i = 0; i < allInput.length; i++) {

//                if (allInput[i].type == "checkbox") {
//                    if (allInput[i].checked) {
//                        haveCheck = true;
//                        break;
//                    }
//                }
//            }
//            if (!haveCheck)
//                alert("<%=Resources.Resource.UserToRole_NotRole %>");
//            return haveCheck
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
                    <td align="right">                        
                        <asp:Button ID="btnSave" runat="server" Text="保存" CssClass="ButtonSave" 
                            OnClientClick="return Save()" onclick="btnSave_Click" />
                            <asp:Button ID="btnCancel" runat="server" Text="放棄" CssClass="ButtonCancel" 
                            OnClientClick="return Cancel()"  />
                        <asp:Button ID="btnExit" runat="server" Text="離開" OnClientClick="eExit()" CssClass="ButtonExit" />
                    </td>
                </tr>
       </table>
        <table class="maintable" cellspacing="0" cellpadding="0" width="100%" align="center" bordercolor="#ffffff" border="1" >			

					<tr>						
                        <td class="musttitle" width="10%"  align="center" >
                            <asp:Literal 
                                ID="ltlUserName" Text="使用者編號"
                                runat="server"></asp:Literal>
                        </td>
						<td  width="35%">&nbsp;
								<asp:DropDownList ID="ddlUserID" runat="server" Width="90%" 
                                onselectedindexchanged="ddlUserID_SelectedIndexChanged" 
                                AutoPostBack="True">                                    
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
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="RoleID"
                    SkinID="GridViewSkin" Width="600px" OnRowDataBound="GridView1_RowDataBound">
            <Columns>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <input type="checkbox" onclick="javascript:selectAll('GridView1',this.checked);" />                    
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox id="cbSelect" runat="server"></asp:CheckBox>                   
                    </ItemTemplate>
                  <HeaderStyle Width="60px" HorizontalAlign="Center"></HeaderStyle>
                 <ItemStyle Width="60px" HorizontalAlign="Center"></ItemStyle>
               </asp:TemplateField>
                <asp:BoundField DataField="RoleID" HeaderText="角色ID" >
                    <ItemStyle HorizontalAlign="Left" Width="20%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField> 
                <asp:BoundField DataField="RoleName" HeaderText="角色名稱">
                    <ItemStyle HorizontalAlign="Left" Width="40%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                  <asp:BoundField DataField="UserLevel" HeaderText="用戶等級" >
                    <ItemStyle HorizontalAlign="Left" Width="30%" Wrap="False" />
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
