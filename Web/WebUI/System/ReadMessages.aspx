<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReadMessages.aspx.cs" Inherits="WebUI_System_ReadMessages" %>

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
                            <asp:Button ID="btnQuery" runat="server" Text="列印" CssClass="ButtonPrint" 
                                meta:resourcekey="btnQueryResource2" />                            
                            <asp:Button ID="btnDelete" runat="server" Text="刪除" CssClass="ButtonDel" 
                                onclick="btnDeletet_Click" meta:resourcekey="btnDeleteResource2"/>
                            <asp:Button ID="btnExit" runat="server" Text="離開" CssClass="ButtonExit" OnClientClick="Exit()" 
                                meta:resourcekey="btnExitResource2" />
                        </td>
                    </tr>
                </table>
                <table class="maintable" cellspacing="0" cellpadding="0" width="100%" align="center" border="1" >
					    <tr> 
						    <td class="smalltitle" align="center" width="10%" ><asp:Literal ID="ltlField" Text="查詢欄位"
                                    runat="server"></asp:Literal></td>
						    <td  width="20%" height="20">&nbsp;
							    <asp:dropdownlist id="ddlFieldName" runat="server" Width="90%" 
                                    meta:resourcekey="ddlFieldNameResource2"></asp:dropdownlist></td>
						    <td class="smalltitle" align="center" width="10%"><asp:Literal ID="ltlContent" Text="查詢內容"
                                    runat="server"></asp:Literal>
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
                    OnSorting="GridView1_Sorting"                    
                    onrowdatabound="GridView1_RowDataBound">
            <Columns>
                <asp:TemplateField meta:resourcekey="TemplateFieldResource3">
                    <HeaderTemplate>
                    <input type="checkbox" onclick="javascript:selectAll('GridView1',this.checked);" />                    
                    </HeaderTemplate>
                    <ItemTemplate>
                    <asp:CheckBox id="cbSelect" runat="server"></asp:CheckBox>                   
                    </ItemTemplate>
                  <HeaderStyle Width="60px"></HeaderStyle>
                 <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
               </asp:TemplateField>
                <asp:TemplateField HeaderText="公告ID"  SortExpression="AnnounceID">
                    <ItemTemplate>
                    <asp:HyperLink id="HyperLink1" runat="server"                             
                            NavigateUrl='<%# "ReadMessageView.aspx?FormID=" + FormID + "&ID="+DataBinder.Eval(Container.DataItem, "AnnounceID") %>' Text='<%# DataBinder.Eval(Container.DataItem, "AnnounceID")%>'></asp:HyperLink>                       
                    </ItemTemplate>
                  <ItemStyle Width="12%" Wrap="False" />
                  <HeaderStyle Width="12%" Wrap="False" />
               </asp:TemplateField>
               <asp:BoundField DataField="AnnounceDate" HeaderText="發佈日期" HtmlEncode="false" DataFormatString="{0:yyyy/MM/dd HH:mm}"
                    SortExpression="AnnounceDate">
                    <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="AnnounceFlag" HeaderText="發佈單位" 
                    SortExpression="AnnounceFlag">
                    <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="Announcer" HeaderText="發佈人員" 
                    SortExpression="Announcer">
                    <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="AnnouncerUserName" HeaderText="發佈使用者" 
                    SortExpression="AnnouncerUserName">
                    <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="Source" HeaderText="來源" 
                    SortExpression="Source">
                    <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="Title" HeaderText="發佈標題" 
                    SortExpression="Title">
                    <ItemStyle HorizontalAlign="Left" Width="10%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="Contents" HeaderText="發佈內容" 
                    SortExpression="Contents">
                    <ItemStyle HorizontalAlign="Left" Width="30%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                
                <asp:CheckBoxField DataField="IsRead" HeaderText="是否閱讀" 
                    SortExpression="IsRead">
                    <ItemStyle HorizontalAlign="Center" Width="10%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:CheckBoxField>
                 <asp:BoundField DataField="ReadDate" HeaderText="閱讀日期" HtmlEncode="false" DataFormatString="{0:yyyy/MM/dd HH:mm}"
                    SortExpression="ReadDate">
                    <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>   
                <asp:BoundField DataField="ReceiverFlag" HeaderText="回覆單位" 
                    SortExpression="ReceiverFlag">
                    <ItemStyle HorizontalAlign="Left" Width="10%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>    
                <asp:BoundField DataField="ReceiverUserName" HeaderText="回覆使用者" 
                    SortExpression="ReceiverUserName">
                    <ItemStyle HorizontalAlign="Left" Width="10%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField> 
                <asp:BoundField DataField="ReplyDate" HeaderText="回覆日期" HtmlEncode="false" DataFormatString="{0:yyyy/MM/dd HH:mm}"
                    SortExpression="ReplyDate">
                    <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>   
                <asp:BoundField DataField="ReplyTitle" HeaderText="回覆標題" 
                    SortExpression="ReplyTitle">
                    <ItemStyle HorizontalAlign="Left" Width="10%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="ReplyContent" HeaderText="回覆內容" 
                    SortExpression="ReplyContent">
                    <ItemStyle HorizontalAlign="Left" Width="30%" Wrap="False" />
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