<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProductLogisticsNoCheck.aspx.cs" Inherits="WebUI_Enterprise_ProductLogisticsNoCheck" %>

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
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Ajax.js") %>'></script>

        <script type='text/javascript'>
            function pEdit(op) {
                var formId = "EP_ProductLogisticsNoCheck";
                var id = "";            
                var exist = IsExistsByFilter(formId, $("#txtEnterpriseID").val(), id, "<%= cnKey%>");

                if (exist == "2") {
                    alert("<%=Resources.Resource.EP_ProductLogisticsEnterpriseChecked%>");
                    return false;
                }
               
                    location.href = "ProductLogisticsEdit.aspx?FormID=" + formId + "&ID=";
                    return false;
            }
            function Upload() {
                //var EnterpriseID = $("#ddlEnterpriseID").val();
                if ($("#txtState0").val() == "3") {
                    alert("<%=Resources.Resource.EP_CheckNoUploadImport%>");
                    return false;
                }
                location.href = "Upload.aspx?FormID=<% =FormID%>";

                //                var strReturn = window.showModalDialog('TempPage.aspx?page=' + page + '&EnterpriseID=' + EnterpriseID + '&FormID=<% =FormID%>&TitleName=' + escape('企業用戶產品上傳作業'), window, 'DialogHeight:200px;DialogWidth:800px;help:no;scroll:no');
                //                location.href = "Default.aspx";
                return false;
            }
            function Open() {
                var gdview = document.getElementById("<%=GridView1.ClientID %>");
                if (gdview == null) {
                    alert("<%=Resources.Resource.EP_NoRecordNotCompare%>");
                    return false;
                }

                var TitleName = escape("<%=Resources.Resource.ProductLogisticsCompare %>");
                var page = "ProductLogisticsCompare.aspx";
                var EnterpriseID = $("#ddlEnterpriseID").val();
                var strReturn = window.showModalDialog('TempPage.aspx?page=' + page + '&EnterpriseID=' + EnterpriseID + '&FormID=<% =FormID%>&TitleName=' + TitleName, window, 'DialogHeight:350px;DialogWidth:800px;help:no;scroll:no');
                //return false;
            }
            function check() {
                var gdview = document.getElementById("<%=GridView1.ClientID %>");
                if (gdview == null) {
                    alert("<%=Resources.Resource.EP_NoRecordNotCheck%>");
                    return false;
                }
                var haveModify = false;
                for (var i = 1; i < gdview.rows.length; i++) {
                    if (gdview.rows(i).cells(2).innerText == "<%=Resources.Resource.Modify %>") {
                        haveModify = true;
                        break;
                    }
                }
                if (haveModify) {
                    if (!confirm("<%=Resources.Resource.EP_CheckModifyMessage%>"))
                        return false;
                }

                var TitleName = escape('<%=Resources.Resource.EP_CheckUser %>');
                var page = "../CheckConfirm/CheckUser.aspx";
                var strReturn = window.showModalDialog('../CheckConfirm/TempPage.aspx?page=' + page + '&TitleName=' + TitleName + '&cnKey=<%= cnKey%>', window, 'DialogHeight:80px;DialogWidth:400px;help:no;scroll:no');
                if (strReturn == "1")
                    return true;
                else
                    return false;
            }
            function uncheck() {
                var uploadDate = $("#txtUploadDate").val();
                if (uploadDate.length > 0) {
                    if (!confirm("<%=Resources.Resource.EP_UploadUnCheck%>"))
                        return false;
                }
                var TitleName = escape('<%=Resources.Resource.EP_CheckUser %>');
                var page = "../CheckConfirm/CheckUser.aspx";
                var strReturn = window.showModalDialog('../CheckConfirm/TempPage.aspx?page=' + page + '&TitleName=' + TitleName + '&cnKey=<%= cnKey%>', window, 'DialogHeight:80px;DialogWidth:400px;help:no;scroll:no');
                if (strReturn == "1")
                    return true;
                else
                    return false;
            }
            function Import() {
                if ($("#txtState0").val() == "3") {
                    alert("<%=Resources.Resource.EP_CheckNoUploadImport%>");
                    return false;
                }
                var formId = "EP_ProductLogisticsNoCheck";
                var exist = IsExistsByFilter(formId, $("#ddlEnterpriseID").val(), "", "<%= cnKey%>");

                if (exist == "2") {
                    alert("<%=Resources.Resource.EP_ProductEnterpriseChecked%>");
                    return false;
                }
            }
            function UploadBU() {
                var gdview = document.getElementById("<%=GridView1.ClientID %>");
                if (gdview == null) {
                    alert("<%=Resources.Resource.EP_NoRecordNotUpload%>");
                    return false;
                }
                if (gdview.rows.length > 0) {
                    if ($.trim(getCellValueByRow(14)).length == 0) {
                        alert("<%=Resources.Resource.EP_NoCheckNotUpload%>");
                        return false;
                    }
                }
                else {
                    alert("<%=Resources.Resource.EP_NoRecordNotUpload%>");
                    return false;
                }
            }
            //1 读取指定行单元格的值 如果是0则读出其表头的值
            //rowIndex=1就是第一行的值
            function getCellValueByRow(rowIndex) {
                var gdview = document.getElementById("<%=GridView1.ClientID %>");

                if (gdview.rows.length > 0)
                    return gdview.rows(1).cells(12).innerText;
                else
                    return "";
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
                        <asp:Button ID="btnAdd" runat="server" Text="新增" CssClass="ButtonCreate" OnClientClick="return pEdit()" />
                        <asp:Button ID="btnDelete" runat="server" Text="刪除" CssClass="ButtonDel" 
                            onclick="btnDelete_Click" OnClientClick="return Delete()" meta:resourcekey="btnDeleteResource2"/>
                        <asp:Button ID="btnExit" runat="server" Text="離開" OnClientClick="return Exit()" CssClass="ButtonExit" />
                    </td>
                </tr>
            </table>
       <div id="surdiv" style="overflow:auto">
            <table class="maintable" cellspacing="0" cellpadding="0" width="100%" align="center" bordercolor="#ffffff" border="1">			
					<tr>
						<td  valign="middle" align="left" colspan="5" height="22" class="title1">
			 				<p><asp:Literal ID="ltlTitle" Text="企業用戶物流資訊設定(未認證)" runat="server"></asp:Literal></p>
						</td>
					</tr>
					<tr>
						<td class="musttitle" align="center" width="10%" ><asp:Literal ID="ltlUploadDate" Text="企業上傳日期"
                                runat="server"></asp:Literal>
                        </td>
						<td width="20%">&nbsp;								
                                <asp:textbox id="txtUploadDate" runat="server" CssClass="TextRead"  ReadOnly="true"  Width="90%" ></asp:textbox> 
                            &nbsp;                         
                        </td >
                        <td width="10%" class="musttitle" align="center">
                         <asp:Literal ID="Literal1" Text="狀&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;態"
                                runat="server"></asp:Literal>
                        </td>
                        <td width="20%">
                            &nbsp;&nbsp;<asp:textbox id="txtState" runat="server" CssClass="TextRead"  ReadOnly="true"  Width="90%" ></asp:textbox>                             
                            <asp:TextBox ID="txtState0" runat="server" CssClass="TextRead" ReadOnly="true" 
                                Width="0px"></asp:TextBox>
                        </td>
                        <td width="25%">
                            &nbsp;&nbsp;&nbsp;<asp:Button ID="btnUploadExcel" runat="server" 
                                OnClientClick="return Upload()" CssClass="but" Text="上傳檔案" Width="75px" />
                            &nbsp;<asp:Button ID="btnImport" runat="server" CssClass="but" Text="資料匯入" 
                                Width="75px" OnClientClick="return Import()" onclick="btnImport_Click" />
                            &nbsp;<asp:Button ID="btnCompare" runat="server" CssClass="but" 
                                onclick="btnCompare_Click" OnClientClick="return Open()" Text="比對檢查" 
                                Width="75px" />
                        </td>
					</tr>
                    <tr>
						<td class="musttitle" align="center" width="10%" ><asp:Literal ID="ltlEnterpriseID" Text="企業編號"
                                runat="server"></asp:Literal>
                        </td>

						<td colspan="3">&nbsp;
                            <asp:DropDownList ID="ddlEnterpriseID" Width="96%" runat="server" 
                                AutoPostBack="True" 
                                onselectedindexchanged="ddlEnterpriseID_SelectedIndexChanged">
                            </asp:DropDownList>
                            &nbsp;                            
                                
                         </td> 

                         <td width="25%" >
                             &nbsp;&nbsp;&nbsp;<asp:Button ID="btnCheck" runat="server" CssClass="but" Text="企業覆核" 
                                 Width="75px" OnClientClick="return check()" onclick="btnCheck_Click" />
                             &nbsp;<asp:Button ID="btnUnCheck" runat="server" CssClass="but" 
                                 onclick="btnUnCheck_Click" OnClientClick="return uncheck()" Text="取消覆核" 
                                 Width="75px" />
                             &nbsp;<asp:Button ID="btnUpload" runat="server" CssClass="but" Text="上傳營運單位" 
                                 Width="75px" OnClientClick="return UploadBU()" onclick="btnUpload_Click" />
                         </td>                     				
					</tr>
                  </table>
          <div id="table-container" style="overflow: auto; WIDTH: 100%; HEIGHT: 400px">
          <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                    SkinID="GridViewSkin" Width="100%" onrowdatabound="GridView1_RowDataBound">
                <Columns>
                <asp:TemplateField meta:resourcekey="TemplateFieldResource3">
                    <HeaderTemplate>
                    <input type="checkbox" onclick="javascript:selectAll('GridView1',this.checked);" />                    
                    </HeaderTemplate>
                    <ItemTemplate>
                    <asp:CheckBox id="cbSelect" runat="server" ></asp:CheckBox>                   
                    </ItemTemplate>
                  <HeaderStyle Width="60px"></HeaderStyle>
                 <ItemStyle Width="60px"  HorizontalAlign="Center"></ItemStyle>
               </asp:TemplateField>
                <asp:BoundField DataField="RowID" HeaderText="序號" 
                        SortExpression="RowID">
                        <ItemStyle HorizontalAlign="Center" Width="8%" Wrap="False" />
                        <HeaderStyle Wrap="False" />
                    </asp:BoundField>
                     <asp:BoundField DataField="Rows" HeaderText="新增/修改" 
                    SortExpression="Rows">
                    <ItemStyle HorizontalAlign="Center" Width="8%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>  
                <asp:TemplateField HeaderText="物流資訊編號"  SortExpression="LogisticsID" 
                    >
                    <ItemTemplate>
                    <asp:HyperLink id="HyperLink1" runat="server"                             
                            NavigateUrl='<%# "ProductLogisticsView.aspx?FormID=" + FormID + "&ID="+DataBinder.Eval(Container.DataItem, "LogisticsID") %>' Text='<%# DataBinder.Eval(Container.DataItem, "LogisticsID")%>'></asp:HyperLink>                      
                    </ItemTemplate>
                  <ItemStyle Width="12%" Wrap="False" HorizontalAlign="Left"/>
                  <HeaderStyle Width="12%" Wrap="False" />
               </asp:TemplateField>
                <asp:BoundField DataField="ProductID" HeaderText="產品編號" 
                    SortExpression="ProductID">
                    <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="ProductName" HeaderText="產品名稱" 
                    SortExpression="ProductName">
                    <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="Area" HeaderText="地區別" 
                    SortExpression="Area">
                    <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="Description" HeaderText="物流資訊描述" 
                    SortExpression="Description">
                    <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="EPLogisticsID" HeaderText="企業物流編號" 
                    SortExpression="EPLogisticsID">
                    <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
               <asp:TemplateField HeaderText="企業物流網址"  SortExpression="LogisticsOfficalUrl" >
                    <ItemTemplate>
                         <asp:HyperLink id="HyperLink2" runat="server" Target="_blank" NavigateUrl='<%# DataBinder.Eval(Container.DataItem, "LogisticsOfficalUrl") %>' >
                            <%# DataBinder.Eval(Container.DataItem, "LogisticsOfficalUrl")%>
                         </asp:HyperLink>                       
                    </ItemTemplate>
                  <ItemStyle Width="12%" Wrap="False" />
                  <HeaderStyle Width="12%" Wrap="False" />
               </asp:TemplateField>             
                <asp:BoundField DataField="CreateDate" HeaderText="建檔日期" HtmlEncode="false" DataFormatString="{0:yyyy/MM/dd HH:mm}"
                    SortExpression="CreateDate">
                    <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="CreateUserName" HeaderText="建檔人員" 
                    SortExpression="CreateUserName">
                    <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="LastModifyDate" HeaderText="異動日期" HtmlEncode="false" DataFormatString="{0:yyyy/MM/dd HH:mm}"
                    SortExpression="LastModifyDate">
                    <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="LastModifyUserName" HeaderText="異動人員" 
                    SortExpression="LastModifyUserName">
                    <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                 <asp:BoundField DataField="EP_CheckUserName" HeaderText="企業覆核人員" 
                    SortExpression="EP_CheckUserName">
                    <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="EP_CheckDate" HeaderText="企業覆核日期" HtmlEncode="false" DataFormatString="{0:yyyy/MM/dd HH:mm}"
                    SortExpression="EP_CheckDate">
                    <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
               
                 <asp:BoundField DataField="BU_CheckUserName" HeaderText="營運覆核人員" 
                    SortExpression="BU_CheckUserName" >
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
