<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BU_Role.aspx.cs" Inherits="WebUI_Account_BU_Role" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
                if (temp == "") {
                    ddl.value = temp;
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
            location.href = "BU_RoleEdit.aspx?FormID=<% =FormID %>";
            return false;
        }
        function eEdit() {
            var user = $("#HiddenField1").val();
            location.href = "BU_RoleEdit.aspx?FormID=<% =FormID %>&NodeUser=" + user;
            return false;
        }
        function Cancel() {
            return confirm("<%=Resources.Resource.Question_Cancel %>");
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
                            <asp:Button ID="btnCopyPermission" runat="server" Text="複製權限" CssClass="ButtonCreate" />
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
                    <asp:HiddenField ID="HiddenField1" runat="server" />
                </td>
                <td valign="top" width="85%">
                    
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

