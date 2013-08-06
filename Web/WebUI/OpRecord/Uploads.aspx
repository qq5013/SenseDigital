<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Uploads.aspx.cs" Inherits="WebUI_OpRecord_Uploads" %>

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

        <script type='text/javascript'>
            function pEdit() {
                location.href = "ProductEdit.aspx?FormID=EP_ProductNoCheck&ID=";
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
        <div id="processMessage"> Loading...<br />
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
                            onclick="btnPrint_Click" />
                        <asp:Button ID="btnExit" runat="server" Text="離開" OnClientClick="Exit()" CssClass="ButtonExit" />
                    </td>
                </tr>
            </table>
         <div id="surdiv" style="overflow:auto">
            <table class="maintable" cellspacing="0" cellpadding="0" width="100%" align="center" border="1" >
					    <tr> 
                            <td class="smalltitle" align="center" width="6%">
                                <asp:Literal ID="Literal1" Text="企業編號" runat="server"></asp:Literal>
                            </td>
                            <td width="15%">
                                &nbsp;
                                <asp:dropdownlist id="ddlEnterpriseID" runat="server" Width="90%" 
                                    AutoPostBack="True" 
                                    onselectedindexchanged="ddlEnterpriseID_SelectedIndexChanged" ></asp:dropdownlist>
                            </td>
						    <td class="smalltitle" align="center" width="7%" ><asp:Literal ID="ltlField" Text="查詢欄位"
                                    runat="server"></asp:Literal></td>
						    <td  width="15%" height="20">&nbsp;
							    <asp:dropdownlist id="ddlFieldName" runat="server" Width="90%" 
                                    meta:resourcekey="ddlFieldNameResource2"></asp:dropdownlist></td>
						    <td class="smalltitle" align="center" width="7%"><asp:Literal ID="ltlContent" Text="查詢內容"
                                    runat="server"></asp:Literal>
                            </td>
						    <td  width="50%" height="20" valign="middle">&nbsp;<asp:textbox id="txtContent" 
                                    tabIndex="1" runat="server" Width="70%" CssClass="TextBox" MaxLength="100" 
                                    heigth="16px" meta:resourcekey="txtContentResource2"></asp:textbox>
                                &nbsp;<asp:button id="btnSearch" tabIndex="2" runat="server" Width="60px" 
                                    CssClass="but" Text="立即查詢" OnClientClick="Search()" meta:resourcekey="btnSearchResource2" 
                                    onclick="btnSearch_Click"></asp:button>
                                &nbsp;<asp:Button ID="btnMultiSearch" runat="server" CssClass="but" tabIndex="3" 
                                    Text="多條件查詢" OnClientClick="SearchDialog(300)" Width="80px" 
                                    meta:resourcekey="btnMultiSearchResource2" onclick="btnMultiSearch_Click" />
                            </td>
					    </tr>
			    </table>
          <div id="table-container" style="overflow: auto; WIDTH: 100%; HEIGHT: 400px">
          <asp:HiddenField ID="HiddenField1" runat="server" />
          <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                    SkinID="GridViewSkin" Width="100%" onrowdatabound="GridView1_RowDataBound">
                <Columns>
                    
                    <asp:BoundField DataField="RowID" HeaderText="序號" 
                        SortExpression="RowID">
                        <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                        <HeaderStyle Wrap="False" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="入庫單號"  SortExpression="ProductID" >
                        <ItemTemplate>
                        <asp:HyperLink id="HyperLink1" runat="server"                             
                                NavigateUrl='<%# "Upload.aspx?FormID=" + FormID + "&BillID="+DataBinder.Eval(Container.DataItem, "BillID") %>'><%# DataBinder.Eval(Container.DataItem, "BillID")%></asp:HyperLink>                       
                        </ItemTemplate>
                      <ItemStyle Width="12%" Wrap="False" />
                      <HeaderStyle Width="12%" Wrap="False" />
                   </asp:TemplateField>
                   <asp:BoundField DataField="BillDate" HeaderText="入庫日期" HtmlEncode="false" DataFormatString="{0:yyyy/MM/dd HH:mm}"
                        SortExpression="BillDate">
                        <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                        <HeaderStyle Wrap="False" />
                    </asp:BoundField>
                    <asp:BoundField DataField="EnterpriseID" HeaderText="企業編號" 
                    SortExpression="EnterpriseID" >
                    <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="EnterpriseName" HeaderText="企業名稱" 
                    SortExpression="EnterpriseName">
                    <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                     <asp:BoundField DataField="StyleID" HeaderText="款式編號" 
                        SortExpression="StyleID">
                        <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                        <HeaderStyle Wrap="False" />
                    </asp:BoundField>               
                        <asp:BoundField DataField="LabelMode" HeaderText="標籤模式" 
                        SortExpression="LabelMode">
                        <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                        <HeaderStyle Wrap="False" />
                    </asp:BoundField>
                      <asp:BoundField DataField="Memo" HeaderText="備註" 
                        SortExpression="Memo">
                        <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                        <HeaderStyle Wrap="False" />
                    </asp:BoundField>
                   <asp:BoundField DataField="CreateUserName" HeaderText="建檔人員" 
                        SortExpression="CreateUserName">
                        <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                        <HeaderStyle Wrap="False" />
                    </asp:BoundField>
                    <asp:BoundField DataField="CreateDate" HeaderText="建檔日期" HtmlEncode="false" DataFormatString="{0:yyyy/MM/dd HH:mm}"
                        SortExpression="CreateDate">
                        <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                        <HeaderStyle Wrap="False" />
                    </asp:BoundField>
                   <asp:BoundField DataField="LastModifyUserName" HeaderText="異動人員" 
                        SortExpression="LastModifyUserName">
                        <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                        <HeaderStyle Wrap="False" />
                    </asp:BoundField>              
                    <asp:BoundField DataField="LastModifyDate" HeaderText="異動日期" HtmlEncode="false" DataFormatString="{0:yyyy/MM/dd HH:mm}"
                        SortExpression="LastModifyDate">
                        <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                        <HeaderStyle Wrap="False" />
                    </asp:BoundField>
                     <asp:BoundField DataField="EP_CheckUserName" HeaderText="企業覆核人員" 
                        SortExpression="EP_CheckUserName">
                        <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                        <HeaderStyle Wrap="False" />
                    </asp:BoundField>              
                    <asp:BoundField DataField="EP_CheckDate" HeaderText="企業覆核日期" HtmlEncode="false" DataFormatString="{0:yyyy/MM/dd HH:mm}"
                        SortExpression="BU_CheckDate">
                        <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                        <HeaderStyle Wrap="False" />
                    </asp:BoundField>
                   <asp:BoundField DataField="BU_CheckUserName" HeaderText="營運覆核人員" 
                        SortExpression="BU_CheckUserName">
                        <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                        <HeaderStyle Wrap="False" />
                    </asp:BoundField>              
                    <asp:BoundField DataField="BU_CheckDate" HeaderText="營運覆核日期" HtmlEncode="false" DataFormatString="{0:yyyy/MM/dd HH:mm}"
                        SortExpression="BU_CheckDate">
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
            </div>             
     </div>
    </ContentTemplate>
            </asp:UpdatePanel> 
    </form>
</body>
</html>
