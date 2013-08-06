<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BU_User.aspx.cs" Inherits="WebUI_Account_BU_User" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="~/Styles/Main.css" type="text/css" rel="stylesheet" /> 
    <link href="~/Styles/op.css" type="text/css" rel="stylesheet" /> 
    <script type="text/javascript" src='<%=ResolveUrl("~/Scripts/JQuery/jquery-1.8.3.min.js") %>'></script>
    <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Commons.js") %>'></script>
    <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Resize.js") %>'></script>

    <script type="text/javascript">
        function SelectAll(tempControl) {
            var theBox = tempControl;
            xState = theBox.checked;

            elem = theBox.form.elements;
            for (i = 0; i < elem.length; i++)
                if (elem[i].type == "checkbox" && elem[i].id != theBox.id && elem[i].id.substring(elem[i].id.length - 2, elem[i].id.length) == theBox.id.substring(theBox.id.length - 2, theBox.id.length)) {
                    if (elem[i].checked != xState)
                        elem[i].click();
                }
            }
            function selectDefault(colIndex) {
                var table = document.getElementById("<%=GridView1.ClientID %>");
                
                var temp = table.rows[0].cells[colIndex].getElementsByTagName("select")[0].value;
                for (var i = 1; i < table.rows.length; i++) {
                    var ddl = table.rows[i].cells[colIndex].getElementsByTagName("select")[0];
                    if (ddl != null) {
                        if (temp == "") {
                            ddl.value = temp;
                            //瀏覽如果為空，後面全清掉
                            if (colIndex == 3) {
                                if (table.rows[i].cells[3].getElementsByTagName("select")[0].value == "") {
                                    for (var k = 4; k < 12; k++) {
                                        table.rows[0].cells[k].getElementsByTagName("select")[0].value = "";
                                        
                                        var oddl = table.rows[i].cells[k].getElementsByTagName("select")[0];
                                        if (oddl != null)
                                            oddl.value = "";
                                    }
                                }
                            }
                        }
                        else {
                            if (ddl != null && ddl.value == "") {
                                for (var j = 0; j < ddl.length; j++) {
                                    //被disabled的選項不可選中
                                    if (ddl[j].value == temp && !ddl[j].disabled)
                                        ddl.value = temp;
                                }
                                //table.rows[i].cells[colIndex].getElementsByTagName("select")[0].value = temp;
                            }
                            //瀏覽自動勾選
                            if (colIndex > 3) {
                                if (table.rows[i].cells[3].getElementsByTagName("select")[0].value == "")
                                    table.rows[i].cells[3].getElementsByTagName("select")[0].value = temp;
                            }
                            
                        }
                    }
                }
            }
            function select(ctl) {
                var i = parseInt(ctl.id.substring(ctl.id.lastIndexOf('_') + 1)) + 1;
                //alert(row);
                var table = document.getElementById("<%=GridView1.ClientID %>");
                if (table.rows[i].cells[3].getElementsByTagName("select")[0].value == "")
                    table.rows[i].cells[3].getElementsByTagName("select")[0].value = ctl.value;
            }
            function del() {
                return confirm("<%=Resources.Resource.Question_Delete %>");
            }
            function Add() {
                location.href = "BU_UserEdit.aspx?FormID=<% =FormID %>&NodeUser=<% =NodeUser%>";
                return false;
            }
            function eEdit() {
                var user = $("#txtUserName").val();
                location.href = "BU_UserEdit.aspx?FormID=<% =FormID %>&UserName=" + user + "&NodeUser=" + user;
                return false;
            }
            function Cancel() {
                return confirm("<%=Resources.Resource.Question_Cancel %>");
            }
            function Copy() {
                var page = "BU_CopyUserPermission.aspx";
                var user = $("#txtUserName").val();
                var title = escape("<%=Resources.Resource.CopyPermissionLoading %>");
                //var strReturn = window.showModalDialog('TempPage.aspx?page=' + page + '&UserName=' + user + '&FormID=<% =FormID%>&TitleName=' + title, window, 'DialogHeight:200px;DialogWidth:800px;help:no;scroll:no');
                var strReturn = window.showModalDialog('BU_CopyUserPermission.aspx?UserName=' + user + '&FormID=<% =FormID%>', window, 'DialogHeight:200px;DialogWidth:400px;help:no;scroll:no');
                if (strReturn != null) {
                    if (confirm("<%=Resources.Resource.Question_CopyPermission %>")) {
                        $("#txtCopyUserID").val(strReturn);
                        return true;
                    }
                }
                return false;
            }
            function userToRole() {
                var userID = $("#txtUserID").val();
                location.href = "BU_UserToRoleEdit.aspx?UserID=" + userID + "&FormID=BU_UserToRole&IsUser=1";
                return false;
            }
    </script>
    <style type="text/css">    
        .tree td div    
        {      
            height: 20px !important;   
        }
    </style>
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
 
     <div >           
                <table style="width: 100%; height: 20px;" class="OperationBar">
                    <tr>
                        <td align="right">
                            <asp:Button ID="btnAdd" runat="server" Text="新增" OnClientClick="return Add();" CssClass="ButtonCreate" />
                            <asp:Button ID="btnEdit" runat="server" Text="修改" CssClass="ButtonModify" 
                            OnClientClick="return eEdit();" />
                            <asp:Button ID="btnPermission" runat="server" Text="授權" CssClass="ButtonCreate" 
                                onclick="btnPermission_Click" />
                            <asp:Button ID="btnDelete" runat="server" Text="刪除" CssClass="ButtonDel" OnClientClick="return del()" 
                                onclick="btnDelete_Click" />
                            <asp:Button ID="btnCopyPermission" runat="server" Text="複製權限" 
                                CssClass="ButtonCreate" OnClientClick="Copy()" 
                                onclick="btnCopyPermission_Click" />
                            <asp:Button ID="btnCancel" runat="server" Text="放棄" 
                                OnClientClick="return Cancel()" CssClass="ButtonCancel" 
                                onclick="btnCancel_Click" />
                            <asp:Button ID="btnSave" runat="server" Text="保存" CssClass="ButtonCreate" onclick="btnSave_Click" />
                            <asp:Button ID="btnExit" runat="server" Text="離開" CssClass="ButtonExit" OnClientClick="Exit()" />
                        </td>
                    </tr>
                </table>
    
    
        <table id="tree-container" style="width:100%">
            <tr>
                <td id="tdTree" valign="top" width="15%">
                    <div style="border:1px solid #7baed9;">
                        <asp:TreeView ID="TreeView1" runat="server" ShowLines="True" Height="500px" 
                            Width="100%"  ShowExpandCollapse="true" CssClass="tree" 
                            onselectednodechanged="TreeView1_SelectedNodeChanged" >
                            <SelectedNodeStyle BackColor="SkyBlue" BorderColor="#FF3300" BorderWidth="1px" />
                        </asp:TreeView>
                    </div>
                </td>
                <td valign="top" width="85%">
                    <table class="maintable" cellspacing="0" cellpadding="0" width="100%" align="center" bordercolor="#ffffff"	border="1" id="toptable" >
					<tr>
						<td class="musttitle" align="center" ><asp:Literal ID="ltlUserName" Text="登入帳號"
                                runat="server"></asp:Literal>
                        </td>
						<td width="25%" >&nbsp;
								<asp:textbox id="txtUserName" runat="server" CssClass="TextRead"  ReadOnly="true" Width="90%" MaxLength="20"></asp:textbox>
                                &nbsp;<asp:TextBox ID="txtUserID" CssClass="TextRead" runat="server" Width="0px"></asp:TextBox>
                            <asp:TextBox ID="txtCopyUserID" runat="server" CssClass="TextRead" Width="0px"></asp:TextBox>
                        </td>
						<td class="musttitle" align="center" width="8%"><asp:Literal 
                                ID="ltlTrueName" Text="真實姓名"
                                runat="server"></asp:Literal>
                        </td>
						<td  width="25%">&nbsp;
							<asp:TextBox ID="txtTrueName" runat="server" CssClass="TextRead" MaxLength="20" 
                                ReadOnly="true" Width="90%"></asp:TextBox>
                        </td>
                        <td class="smalltitle" align="center" width="8%"><asp:Literal 
                                ID="ltlState" Text="狀&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;態"
                                runat="server"></asp:Literal>
                        </td>
						<td  width="20%">&nbsp;
							<asp:TextBox ID="txtState" runat="server" CssClass="TextRead" MaxLength="20" 
                                ReadOnly="true" Width="90%"></asp:TextBox>
                            &nbsp;</td>
					</tr>                    
					<tr>
						<td class="musttitle" align="center" ><asp:Literal ID="ltlPersonID" Text="員工編號"
                                runat="server"></asp:Literal>
                        </td>
						<td width="25%" >&nbsp;
								<asp:textbox id="txtPersonID" runat="server" CssClass="TextRead"  ReadOnly="true" Width="30%" MaxLength="10"></asp:textbox>
                                <asp:textbox id="txtPersonName" runat="server" CssClass="TextRead"  ReadOnly="true" Width="60%" MaxLength="10"></asp:textbox>
                        </td>
						<td class="smalltitle" align="center" width="8%"><asp:Literal 
                                ID="ltlDepartment" Text="所屬部門"
                                runat="server"></asp:Literal>
                        </td>
						<td  width="25%">&nbsp;
							<asp:DropDownList runat="server" ID="ddlDepartmentID"  Width="90%" 
                                Enabled="false">                                
                            </asp:DropDownList>
                        </td>
                        <td class="smalltitle" align="center" width="8%"><asp:Literal 
                                ID="ltlSex" Text="性&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;別"
                                runat="server"></asp:Literal>
                        </td>
						<td  width="25%">&nbsp;
							<asp:DropDownList runat="server" ID="ddlSex"  Width="90%">
                                <asp:ListItem Value="0">男</asp:ListItem>
                                <asp:ListItem Value="1">女</asp:ListItem>
                            </asp:DropDownList>
                            &nbsp;</td>
					</tr>
                    <tr>
						<td class="smalltitle" align="center" ><asp:Literal 
                                ID="ltlUserLevel" Text="用戶等級"
                                runat="server"></asp:Literal>
                        </td>
						<td  width="25%">&nbsp;
								<asp:DropDownList ID="ddlUserLevel" runat="server" Width="90%">
                                    <asp:ListItem Text="一般用戶"></asp:ListItem>
                                    <asp:ListItem Text="管理用戶"></asp:ListItem>
                                    <asp:ListItem Text="超級用戶"></asp:ListItem>
                            </asp:DropDownList>
                         </td>
                         <td class="smalltitle" align="center" ><asp:Literal 
                                ID="ltlEnableDate" Text="啟用日期"
                                runat="server"></asp:Literal>
                        </td>
						<td  width="25%">&nbsp;
								<asp:textbox id="txtEnableDate" runat="server" CssClass="TextRead"  ReadOnly="true" Width="90%" MaxLength="30" ></asp:textbox>
                         </td>
                         <td class="smalltitle" align="center" ><asp:Literal 
                                ID="ltlStopDate" Text="停用日期"
                                runat="server"></asp:Literal>
                        </td>
						<td  width="25%">&nbsp;
								<asp:textbox id="txtStopDate" runat="server" CssClass="TextRead"  ReadOnly="true" Width="90%" MaxLength="30" ></asp:textbox>
                         </td>
					</tr>
					<tr>
						<td class="smalltitle" align="center" ><asp:Literal 
                                ID="ltlEMail" Text="電子郵件"
                                runat="server"></asp:Literal>
                        </td>
						<td  width="25%">&nbsp;
								<asp:textbox id="txtEMail" runat="server" CssClass="TextRead"  ReadOnly="true" Width="90%" MaxLength="30" ></asp:textbox>
                         </td>
                         <td class="smalltitle" align="center" ><asp:Literal 
                                ID="ltlPhone" Text="連絡電話"
                                runat="server"></asp:Literal>
                        </td>
						<td  width="25%">&nbsp;
								<asp:textbox id="txtPhone" runat="server" CssClass="TextRead"  ReadOnly="true" Width="90%" MaxLength="30" ></asp:textbox>
                         </td>
                         <td class="smalltitle" align="center" ><asp:Literal 
                                ID="ltlCellPhone" Text="行動電話"
                                runat="server"></asp:Literal>
                        </td>
						<td  width="25%">&nbsp;
								<asp:textbox id="txtCellPhone" runat="server" CssClass="TextRead"  ReadOnly="true" Width="90%" MaxLength="30" ></asp:textbox>
                         </td>
					</tr>
                    
                    <tr>
						<td class="smalltitle" align="center" ><asp:Literal 
                                ID="ltlCreateDate" Text="建檔日期"
                                runat="server"></asp:Literal>
                        </td>
						<td  width="25%">&nbsp;
								<asp:textbox id="txtCreateDate" runat="server" CssClass="TextRead"  ReadOnly="true" Width="90%" MaxLength="30" ></asp:textbox>
                         </td>
                         <td class="smalltitle" align="center" ><asp:Literal 
                                ID="ltlCreateUserName" Text="建檔人員"
                                runat="server"></asp:Literal>
                        </td>
						<td  width="25%">&nbsp;
								<asp:textbox id="txtCreateUserName" runat="server" CssClass="TextRead"  ReadOnly="true" Width="90%" MaxLength="30" ></asp:textbox>
                         </td>
                         <td colspan="2" rowspan="2">
                         
                             <asp:Button ID="btnRole" runat="server" CssClass="but" Text="所屬角色" OnClientClick="return userToRole();"
                                 Width="75px" />
                             &nbsp;<asp:Button ID="btnState" runat="server" CssClass="but" Text="狀態切換" 
                                 Width="70px" onclick="btnState_Click" />
                         
                         </td>
					</tr>
                    <tr>
						<td class="smalltitle" align="center" ><asp:Literal 
                                ID="ltlLastModifyDate" Text="異動日期"
                                runat="server"></asp:Literal>
                        </td>
						<td  width="25%">&nbsp;
								<asp:textbox id="txtLastModifyDate" runat="server" CssClass="TextRead"  ReadOnly="true" Width="90%" MaxLength="30" ></asp:textbox>
                         </td>
                         <td class="smalltitle" align="center" ><asp:Literal 
                                ID="ltlLastModifyUserName" Text="異動人員"
                                runat="server"></asp:Literal>
                        </td>
						<td  width="25%">&nbsp;
								<asp:textbox id="txtLastModifyUserName" runat="server" CssClass="TextRead"  ReadOnly="true" Width="90%" MaxLength="30" ></asp:textbox>
                         </td>
                         
					</tr>
			        </table>
                    <div id="table-container" style="overflow: auto; WIDTH: 100%; HEIGHT:400px;position:absolute">
                
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                                SkinID="GridViewSkin" Width="1000px"
                                OnRowDataBound="GridView1_RowDataBound">
                        <Columns>
                            <asp:BoundField DataField="RowID" HeaderText="序號" >
                                <ItemStyle HorizontalAlign="Center" Width="6%" Wrap="False" />
                                <HeaderStyle Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ModuleName" HeaderText="模組名稱" >
                                <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                                <HeaderStyle Wrap="False" />
                            </asp:BoundField>
                             <asp:BoundField DataField="Text" HeaderText="功能名稱" >
                                <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                                <HeaderStyle Wrap="False" />
                            </asp:BoundField>
                            <asp:TemplateField>                              
                              <ItemStyle HorizontalAlign="Center" Width="12%" Wrap="False" />
                              <HeaderTemplate>                                
                                  <asp:Literal ID="ltlHeadBrowse" Text="瀏覽" runat="server"></asp:Literal><br>
                                  <asp:DropDownList ID="ddlHeadBrowse" onchange="selectDefault(3)" runat="server">
                                      <asp:ListItem> </asp:ListItem>
                                      <asp:ListItem Value="1">限本用戶</asp:ListItem>
                                      <asp:ListItem Value="2">限企業群組</asp:ListItem>
                                      <asp:ListItem Value="3">不受限制</asp:ListItem>
                                  </asp:DropDownList>
                              </HeaderTemplate>
                              <ItemTemplate>
                                  <asp:DropDownList ID="ddlBrowse" runat="server">
                                      <asp:ListItem></asp:ListItem>
                                      <asp:ListItem Value="1">限本用戶</asp:ListItem>
                                      <asp:ListItem Value="2">限企業群組</asp:ListItem>
                                      <asp:ListItem Value="3">不受限制</asp:ListItem>
                                  </asp:DropDownList>
                              </ItemTemplate>
                              
                           </asp:TemplateField>                           
                            <asp:TemplateField>
                              <ItemStyle HorizontalAlign="Center" Width="12%" Wrap="False" />
                              <HeaderTemplate>                                
                                  <asp:Literal ID="ltlHeadDo" Text="執行" runat="server"></asp:Literal><br>
                                <asp:DropDownList ID="ddlHeadDo" onchange="selectDefault(4)" runat="server">
                                      <asp:ListItem> </asp:ListItem>
                                      <asp:ListItem Value="1">限本用戶</asp:ListItem>
                                      <asp:ListItem Value="2">限企業群組</asp:ListItem>
                                      <asp:ListItem Value="3">不受限制</asp:ListItem>
                                  </asp:DropDownList>
                              </HeaderTemplate>
                              <ItemTemplate>
                                <asp:DropDownList ID="ddlDo" onchange="select(this)" runat="server">
                                      <asp:ListItem> </asp:ListItem>
                                      <asp:ListItem Value="1">限本用戶</asp:ListItem>
                                      <asp:ListItem Value="2">限企業群組</asp:ListItem>
                                      <asp:ListItem Value="3">不受限制</asp:ListItem>
                                  </asp:DropDownList>
                              </ItemTemplate>
                           </asp:TemplateField>
                           <asp:TemplateField>
                              <ItemStyle HorizontalAlign="Center" Width="12%" Wrap="False" />
                              <HeaderTemplate>                                
                                  <asp:Literal ID="ltlHeadAdd" Text="新增" runat="server"></asp:Literal><br>
                                <asp:DropDownList ID="ddlHeadAdd" onchange="selectDefault(5)" runat="server">
                                      <asp:ListItem> </asp:ListItem>
                                      <asp:ListItem Value="1">限本用戶</asp:ListItem>
                                      <asp:ListItem Value="2">限企業群組</asp:ListItem>
                                      <asp:ListItem Value="3">不受限制</asp:ListItem>
                                  </asp:DropDownList>
                              </HeaderTemplate>
                              <ItemTemplate>
                                <asp:DropDownList ID="ddlAdd" onchange="select(this)" runat="server">
                                      <asp:ListItem> </asp:ListItem>
                                      <asp:ListItem Value="1">限本用戶</asp:ListItem>
                                      <asp:ListItem Value="2">限企業群組</asp:ListItem>
                                      <asp:ListItem Value="3">不受限制</asp:ListItem>
                                  </asp:DropDownList>
                              </ItemTemplate>
                           </asp:TemplateField>
                           <asp:TemplateField>
                              <ItemStyle HorizontalAlign="Center" Width="12%" Wrap="False" />
                              <HeaderTemplate>                                
                                  <asp:Literal ID="ltlHeadEdit" Text="修改" runat="server"></asp:Literal><br>
                                <asp:DropDownList ID="ddlHeadEdit" onchange="selectDefault(6)" runat="server">
                                      <asp:ListItem> </asp:ListItem>
                                      <asp:ListItem Value="1">限本用戶</asp:ListItem>
                                      <asp:ListItem Value="2">限企業群組</asp:ListItem>
                                      <asp:ListItem Value="3">不受限制</asp:ListItem>
                                  </asp:DropDownList>
                              </HeaderTemplate>
                              <ItemTemplate>
                                <asp:DropDownList ID="ddlEdit" onchange="select(this)" runat="server">
                                      <asp:ListItem> </asp:ListItem>
                                      <asp:ListItem Value="1">限本用戶</asp:ListItem>
                                      <asp:ListItem Value="2">限企業群組</asp:ListItem>
                                      <asp:ListItem Value="3">不受限制</asp:ListItem>
                                  </asp:DropDownList>
                              </ItemTemplate>
                           </asp:TemplateField>
                           <asp:TemplateField>
                              <ItemStyle HorizontalAlign="Center" Width="12%" Wrap="False" />
                              <HeaderTemplate>                                
                                  <asp:Literal ID="ltlHeadDelete" Text="刪除" runat="server"></asp:Literal><br>
                                <asp:DropDownList ID="ddlHeadDelete" onchange="selectDefault(7)" runat="server">
                                      <asp:ListItem> </asp:ListItem>
                                      <asp:ListItem Value="1">限本用戶</asp:ListItem>
                                      <asp:ListItem Value="2">限企業群組</asp:ListItem>
                                      <asp:ListItem Value="3">不受限制</asp:ListItem>
                                  </asp:DropDownList>
                              </HeaderTemplate>
                              <ItemTemplate>
                                <asp:DropDownList ID="ddlDelete" onchange="select(this)" runat="server">
                                      <asp:ListItem> </asp:ListItem>
                                      <asp:ListItem Value="1">限本用戶</asp:ListItem>
                                      <asp:ListItem Value="2">限企業群組</asp:ListItem>
                                      <asp:ListItem Value="3">不受限制</asp:ListItem>
                                  </asp:DropDownList>
                              </ItemTemplate>
                           </asp:TemplateField>
                           <asp:TemplateField>
                              <ItemStyle HorizontalAlign="Center" Width="12%" Wrap="False" />
                              <HeaderTemplate>                                
                                  <asp:Literal ID="ltlHeadPrint" Text="列印" runat="server"></asp:Literal><br>
                                <asp:DropDownList ID="ddlHeadPrint" onchange="selectDefault(8)" runat="server">
                                      <asp:ListItem> </asp:ListItem>
                                      <asp:ListItem Value="1">限本用戶</asp:ListItem>
                                      <asp:ListItem Value="2">限企業群組</asp:ListItem>
                                      <asp:ListItem Value="3">不受限制</asp:ListItem>
                                  </asp:DropDownList>
                              </HeaderTemplate>
                              <ItemTemplate>
                                <asp:DropDownList ID="ddlPrint" onchange="select(this)" runat="server">
                                      <asp:ListItem> </asp:ListItem>
                                      <asp:ListItem Value="1">限本用戶</asp:ListItem>
                                      <asp:ListItem Value="2">限企業群組</asp:ListItem>
                                      <asp:ListItem Value="3">不受限制</asp:ListItem>
                                  </asp:DropDownList>
                              </ItemTemplate>
                           </asp:TemplateField>
                           <asp:TemplateField>
                              <ItemStyle HorizontalAlign="Center" Width="12%" Wrap="False" />
                              <HeaderTemplate>                                
                                  <asp:Literal ID="ltlHeadStop" Text="停用" runat="server"></asp:Literal><br>
                                <asp:DropDownList ID="ddlHeadStop" onchange="selectDefault(9)" runat="server">
                                      <asp:ListItem> </asp:ListItem>
                                      <asp:ListItem Value="1">限本用戶</asp:ListItem>
                                      <asp:ListItem Value="2">限企業群組</asp:ListItem>
                                      <asp:ListItem Value="3">不受限制</asp:ListItem>
                                  </asp:DropDownList>
                              </HeaderTemplate>
                              <ItemTemplate>
                                <asp:DropDownList ID="ddlStop" onchange="select(this)" runat="server">
                                      <asp:ListItem> </asp:ListItem>
                                      <asp:ListItem Value="1">限本用戶</asp:ListItem>
                                      <asp:ListItem Value="2">限企業群組</asp:ListItem>
                                      <asp:ListItem Value="3">不受限制</asp:ListItem>
                                  </asp:DropDownList>
                              </ItemTemplate>
                           </asp:TemplateField>
                           <asp:TemplateField>
                              <ItemStyle HorizontalAlign="Center" Width="12%" Wrap="False" />
                              <HeaderTemplate>                                
                                  <asp:Literal ID="ltlHeadCheck" Text="覆核" runat="server"></asp:Literal><br>
                                <asp:DropDownList ID="ddlHeadCheck" onchange="selectDefault(10)" runat="server">
                                      <asp:ListItem> </asp:ListItem>
                                      <asp:ListItem Value="1">限本用戶</asp:ListItem>
                                      <asp:ListItem Value="2">限企業群組</asp:ListItem>
                                      <asp:ListItem Value="3">不受限制</asp:ListItem>
                                  </asp:DropDownList>
                              </HeaderTemplate>
                              <ItemTemplate>
                                <asp:DropDownList ID="ddlCheck" onchange="select(this)" runat="server">
                                      <asp:ListItem> </asp:ListItem>
                                      <asp:ListItem Value="1">限本用戶</asp:ListItem>
                                      <asp:ListItem Value="2">限企業群組</asp:ListItem>
                                      <asp:ListItem Value="3">不受限制</asp:ListItem>
                                  </asp:DropDownList>
                              </ItemTemplate>
                           </asp:TemplateField>
                           <asp:TemplateField>
                              <ItemStyle HorizontalAlign="Center" Width="12%" Wrap="False" />
                              <HeaderTemplate>                                
                                  <asp:Literal ID="ltlHeadUnCheck" Text="取消覆核" runat="server"></asp:Literal><br>
                                <asp:DropDownList ID="ddlHeadUnCheck" onchange="selectDefault(11)" runat="server">
                                      <asp:ListItem> </asp:ListItem>
                                      <asp:ListItem Value="1">限本用戶</asp:ListItem>
                                      <asp:ListItem Value="2">限企業群組</asp:ListItem>
                                      <asp:ListItem Value="3">不受限制</asp:ListItem>
                                  </asp:DropDownList>
                              </HeaderTemplate>
                              <ItemTemplate>
                                <asp:DropDownList ID="ddlUnCheck" onchange="select(this)" runat="server">
                                      <asp:ListItem> </asp:ListItem>
                                      <asp:ListItem Value="1">限本用戶</asp:ListItem>
                                      <asp:ListItem Value="2">限企業群組</asp:ListItem>
                                      <asp:ListItem Value="3">不受限制</asp:ListItem>
                                  </asp:DropDownList>
                              </ItemTemplate>
                           </asp:TemplateField>
                           <asp:BoundField DataField="Permission">
                                <ItemStyle HorizontalAlign="Left" Width="6%" Wrap="False" />
                                <HeaderStyle Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="SysID">
                                <ItemStyle HorizontalAlign="Left" Width="6%" Wrap="False" />
                                <HeaderStyle Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="PermissionID">
                                <ItemStyle HorizontalAlign="Left" Width="6%" Wrap="False" />
                                <HeaderStyle Wrap="False" />
                            </asp:BoundField>
                        </Columns>
                        <PagerSettings Visible="False" />
                    </asp:GridView>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    </ContentTemplate>
            </asp:UpdatePanel> 
    </form>
</body>
</html>
