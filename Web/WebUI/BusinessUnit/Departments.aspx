<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Departments.aspx.cs" Inherits="WebUI_BusinessUnit_Departments" culture="auto" meta:resourcekey="PageResource3" uiculture="auto" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
  
    <title></title> 
    <link href="~/Styles/Main.css" type="text/css" rel="stylesheet" /> 
    <link href="~/Styles/op.css" type="text/css" rel="stylesheet" /> 
    <script type="text/javascript" src='<%=ResolveUrl("~/Scripts/JQuery/jquery-1.8.3.min.js") %>'></script>
    <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Commons.js") %>'></script>
    <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Resize.js") %>'></script>  
 
    <script type='text/javascript'>
        
     
    </script>
</head>
<body >
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
                            <asp:Button ID="btnPrint" runat="server" Text="列印" CssClass="ButtonPrint" 
                                meta:resourcekey="btnQueryResource2" />
                            <asp:Button ID="btnAdd" runat="server" Text="新增" OnClientClick="Edit(false);" CssClass="ButtonCreate"                            
                                meta:resourcekey="btnCreateResource2" />
                            <asp:Button ID="btnDelete" runat="server" Text="刪除" CssClass="ButtonDel" 
                                onclick="btnDeletet_Click" OnClientClick="return Delete()" meta:resourcekey="btnDeleteResource2"/>
                            <asp:Button ID="btnExit" runat="server" Text="離開" CssClass="ButtonExit" OnClientClick="return Exit()" 
                                meta:resourcekey="btnExitResource2" />
                        </td>
                    </tr>
                </table>
                <table class="maintable" cellspacing="0" cellpadding="0" width="100%" align="center" border="1" >
					    <tr> 
						    <td class="smalltitle" align="center" width="10%" >
                                <asp:Literal ID="ltlField" Text="查詢欄位"
                                    runat="server" meta:resourcekey="ltlFieldResource1"></asp:Literal></td>
						    <td  width="20%" height="20">&nbsp;
							    <asp:dropdownlist id="ddlFieldName" runat="server" Width="90%" 
                                    meta:resourcekey="ddlFieldNameResource2"></asp:dropdownlist></td>
						    <td class="smalltitle" align="center" width="10%">
                                <asp:Literal ID="ltlContent" Text="查詢內容"
                                    runat="server" meta:resourcekey="ltlContentResource1"></asp:Literal>
                            </td>
						    <td  width="60%" height="20" valign="middle">&nbsp;<asp:textbox id="txtContent" 
                                    tabIndex="1" runat="server" Width="70%" CssClass="TextBox" MaxLength="100" 
                                    heigth="16px" meta:resourcekey="txtContentResource2"></asp:textbox>
                                &nbsp;<asp:button id="btnSearch" tabIndex="2" runat="server" Width="60px" 
                                    CssClass="but" Text="立即查詢" OnClientClick="Search()" meta:resourcekey="btnSearchResource2" 
                                    onclick="btnSearch_Click"></asp:button>
                                &nbsp;<asp:Button ID="btnMultiSearch" runat="server" CssClass="but" tabIndex="3" 
                                    Text="多條件查詢" OnClientClick="return SearchDialog(300)" Width="80px" 
                                    meta:resourcekey="btnMultiSearchResource2" onclick="btnMultiSearch_Click" />
                            </td>
					    </tr>
				    
			    </table>
                </div>

            <div id="table-container" style="overflow: auto; WIDTH: 100%; HEIGHT: 400px">
                <asp:HiddenField ID="HiddenField1" runat="server" />
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                    SkinID="GridViewSkin" Width="1200px" AllowSorting="True"  
                    OnSorting="GridView1_Sorting" OnRowDataBound="GridView1_RowDataBound" 
                    meta:resourcekey="GridView1Resource3">
            <Columns>
                <asp:TemplateField meta:resourcekey="TemplateFieldResource3">
                    <HeaderTemplate>
                    <input type="checkbox" onclick="javascript:selectAll('GridView1',this.checked);" />                    
                    </HeaderTemplate>
                    <ItemTemplate>
                    <asp:CheckBox id="cbSelect" runat="server" meta:resourcekey="cbSelectResource3"></asp:CheckBox>                   
                    </ItemTemplate>
                  <HeaderStyle Width="60px"></HeaderStyle>
                 <ItemStyle Width="60px" HorizontalAlign="Center"></ItemStyle>
               </asp:TemplateField>
                <asp:TemplateField HeaderText="部門編號"  SortExpression="DepartmentID" 
                    meta:resourcekey="TemplateFieldResource5">
                    <ItemTemplate>
                    <asp:HyperLink id="HyperLink1" runat="server"                             
                            
                            NavigateUrl='<%# "DepartmentView.aspx?FormID=" + FormID + "&ID="+DataBinder.Eval(Container.DataItem, "DepartmentID") %>' 
                            Text='<%# DataBinder.Eval(Container.DataItem, "DepartmentID") %>' 
                            meta:resourcekey="HyperLink1Resource4"></asp:HyperLink>                       
                    </ItemTemplate>
                  <ItemStyle Width="8%" Wrap="False" HorizontalAlign="Left"/>
                  <HeaderStyle Width="8%" Wrap="False" />
               </asp:TemplateField>
                <asp:BoundField DataField="DepartmentName" HeaderText="部門名稱" 
                    SortExpression="DepartmentName" meta:resourcekey="BoundFieldResource23">
                    <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="DepartmentEName" HeaderText="英文名稱" 
                    SortExpression="DepartmentEName" meta:resourcekey="BoundFieldResource24">
                    <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="Memo" HeaderText="備註" 
                    SortExpression="Memo" meta:resourcekey="BoundFieldResource25">
                    <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="CreateDate" HeaderText="建檔日期" HtmlEncode="False" DataFormatString="{0:yyyy/MM/dd HH:mm}"
                    SortExpression="CreateDate" meta:resourcekey="BoundFieldResource26">
                    <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="CreateUserName" HeaderText="建檔人員" 
                    SortExpression="CreateUserName" meta:resourcekey="BoundFieldResource27">
                    <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="LastModifyDate" HeaderText="異動日期" HtmlEncode="False" DataFormatString="{0:yyyy/MM/dd HH:mm}"
                    SortExpression="LastModifyDate" meta:resourcekey="BoundFieldResource28">
                    <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="LastModifyUserName" HeaderText="異動人員" 
                    SortExpression="LastModifyUserName" 
                    meta:resourcekey="BoundFieldResource29">
                    <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                 <asp:BoundField DataField="CheckUserName" HeaderText="覆核人員" 
                    SortExpression="CheckUserName" meta:resourcekey="BoundFieldResource30">
                    <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="CheckDate" HeaderText="覆核日期" HtmlEncode="False" DataFormatString="{0:yyyy/MM/dd HH:mm}"
                    SortExpression="CheckDate" meta:resourcekey="BoundFieldResource31">
                    <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
               
                <asp:BoundField DataField="StopUserName" HeaderText="停用人員" 
                    SortExpression="StopUserName" meta:resourcekey="BoundFieldResource32">
                    <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="StopDate" HeaderText="停用日期" HtmlEncode="False" DataFormatString="{0:yyyy/MM/dd HH:mm}"
                    SortExpression="StopDate" meta:resourcekey="BoundFieldResource33">
                    <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                
                
            </Columns>
            <PagerSettings Visible="False" />
        </asp:GridView>
        </div>
            <div>
            <asp:LinkButton ID="btnFirst" runat="server" OnClick="btnFirst_Click" 
                    meta:resourcekey="btnFirstResource2" Text="首頁"></asp:LinkButton> 
            &nbsp;<asp:LinkButton ID="btnPre" runat="server" OnClick="btnPre_Click" 
                    meta:resourcekey="btnPreResource2" Text="上一頁"></asp:LinkButton> 
            &nbsp;<asp:Label ID="lblCurrentPage" runat="server" 
                    meta:resourcekey="lblCurrentPageResource2"></asp:Label> 
            &nbsp;<asp:LinkButton ID="btnNext" runat="server" OnClick="btnNext_Click" 
                    meta:resourcekey="btnNextResource2" Text="下一頁"></asp:LinkButton> 
            &nbsp;<asp:LinkButton ID="btnLast" runat="server" OnClick="btnLast_Click" 
                    meta:resourcekey="btnLastResource2" Text="尾页"></asp:LinkButton> 
   &nbsp;<asp:textbox id="txtPageNo" onkeypress="return regInput(this,/^\d+$/,String.fromCharCode(event.keyCode))"
					onpaste="return regInput(this,/^\d+$/,window.clipboardData.getData('Text'))" ondrop="return regInput(this,/^\d+$/,event.dataTransfer.getData('Text'))"
					runat="server" Width="56px" CssClass="TextBox" meta:resourcekey="txtPageNoResource2"></asp:textbox>&nbsp;<asp:linkbutton 
                    id="btnToPage" runat="server" onclick="btnToPage_Click" 
                    meta:resourcekey="btnToPageResource2" Text="跳轉"></asp:linkbutton>
                &nbsp;
                <asp:DropDownList ID="ddlPageSize" runat="server" AutoPostBack="True" 
                    onselectedindexchanged="ddlPageSize_SelectedIndexChanged" 
                    meta:resourcekey="ddlPageSizeResource1">
                    
                </asp:DropDownList>
        </div>
       </ContentTemplate>
            </asp:UpdatePanel> 
    </form>
</body>
</html>
