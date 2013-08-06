<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProductCheck.aspx.cs" Inherits="WebUI_BusinessUnit_ProductCheck" %>

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
            function Open() {
                var gdview = document.getElementById("<%=GridView1.ClientID %>");
                if (gdview == null) {
                    alert("<%=Resources.Resource.EP_NoRecordNotCheck%>");
                    return false;
                }
                var haveModify = false;
                for (var i = 1; i < gdview.rows.length; i++) {
                    if (gdview.rows(i).cells(17).innerText == "<%=Resources.Resource.Modify %>") {
                        haveModify = true;
                        break;
                    }
                }
                if (!haveModify) {
                    alert("<%=Resources.Resource.EP_NoModifyCompareRecord%>");
                    return false;
                }
                var TitleName = escape("<%=Resources.Resource.ProductCompare %>");
                var page = "ProductCompare.aspx";
                var EnterpriseID = $("#txtEnterpriseID").val();
                var strReturn = window.showModalDialog('TempPage.aspx?page=' + page + '&EnterpriseID=' + EnterpriseID + '&FormID=<% =FormID%>&TitleName=' + TitleName, window, 'DialogHeight:350px;DialogWidth:800px;help:no;scroll:no');
                return false;
            }
            function Check() {
                var TitleName = escape('<%=Resources.Resource.BU_CheckUser %>');
                var page = "../CheckConfirm/CheckUser.aspx";
                var strReturn = window.showModalDialog('../CheckConfirm/TempPage.aspx?page=' + page + '&TitleName=' + TitleName, window, 'DialogHeight:80px;DialogWidth:400px;help:no;scroll:no');
                if (strReturn == "1")
                    return true;
                else
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
                        <asp:Button ID="btnExit" runat="server" Text="離開" OnClientClick="return Exit()" CssClass="ButtonExit" />
                         <asp:Button ID="btnBack" runat="server" Text="返回" OnClientClick="return Back()" 
                            CssClass="ButtonBack" />
                    </td>
                </tr>
            </table>
            
            <table class="maintable" cellspacing="0" cellpadding="0" width="100%" align="center" bordercolor="#ffffff"
				border="1">			
					<tr>
						<td  valign="middle" align="left" colspan="2" height="22" class="title1">
			 				<p><asp:Literal ID="ltlTitle" Text="營運單位產品設定(未認證)[ 單筆明细畫面 ]" runat="server"></asp:Literal></p>
						</td>
					</tr>							
						
					<tr>
						<td class="musttitle" align="center" width="15%" ><asp:Literal ID="ltlUploadDate" Text="企業上傳日期"
                                runat="server"></asp:Literal>
                        </td>
						<td>&nbsp;								
                                <asp:textbox id="txtUploadDate" runat="server" CssClass="TextRead"  ReadOnly="true"  Width="30%" ></asp:textbox> 
                            &nbsp; 
                            <asp:Button ID="btnCheck" runat="server" CssClass="but" 
                                Text="營運覆核" Width="75px" OnClientClick="return Check()" onclick="btnCheck_Click" />                           
                            &nbsp;<asp:Button ID="btnCompare" runat="server" CssClass="but" 
                                OnClientClick="return Open();" Text="比對記錄查詢" Width="92px" />

                        </td>
					</tr>
                    <tr>
						<td class="musttitle" align="center" width="10%" ><asp:Literal ID="ltlEnterpriseID" Text="企業編號"
                                runat="server"></asp:Literal>
                        </td>
						<td>&nbsp;
								<asp:textbox id="txtEnterpriseID" runat="server" CssClass="TextRead"  ReadOnly="true"  Width="30%" MaxLength="6"></asp:textbox>&nbsp;
                                <asp:textbox id="txtEnterpriseName" runat="server" CssClass="TextRead"  ReadOnly="true"  Width="60%" MaxLength="20"></asp:textbox>
                        </td>						
						
					</tr>
                  </table>
                    <div id="table-container" style="overflow: auto; WIDTH: 100%; HEIGHT: 400px">
                                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                                onrowdatabound="GridView1_RowDataBound" SkinID="GridViewSkin" Width="100%">
                                <Columns>
                                    <asp:BoundField DataField="RowID" HeaderText="序號" SortExpression="RowID">
                                    <ItemStyle HorizontalAlign="Center" Width="8%" Wrap="False" />
                                    <HeaderStyle Wrap="False" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ProductID" HeaderText="產品編號" 
                                        SortExpression="ProductID">
                                    <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                                    <HeaderStyle Wrap="False" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ProductName" HeaderText="產品品名" 
                                        SortExpression="ProductName">
                                    <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                                    <HeaderStyle Wrap="False" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="GroupName" HeaderText="大類" 
                                        SortExpression="GroupName">
                                    <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                                    <HeaderStyle Wrap="False" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ClassName" HeaderText="小類" 
                                        SortExpression="ClassName">
                                    <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                                    <HeaderStyle Wrap="False" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ProductEName" HeaderText="英文品名" 
                                        SortExpression="ProductEName">
                                    <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                                    <HeaderStyle Wrap="False" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Barcode1" HeaderText="條碼1" SortExpression="Barcode1">
                                    <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                                    <HeaderStyle Wrap="False" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Barcode2" HeaderText="條碼2" SortExpression="Barcode2">
                                    <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                                    <HeaderStyle Wrap="False" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Description" HeaderText="描述" 
                                        SortExpression="Description">
                                    <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                                    <HeaderStyle Wrap="False" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="官方網址" SortExpression="OfficalUrl">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="HyperLink2" runat="server" 
                                                NavigateUrl='<%# "http://" + DataBinder.Eval(Container.DataItem, "OfficalUrl") %>' 
                                                Target="_blank"><%# DataBinder.Eval(Container.DataItem, "OfficalUrl")%>
                                            </asp:HyperLink>
                                        </ItemTemplate>
                                        <ItemStyle Width="12%" Wrap="False" />
                                        <HeaderStyle Width="12%" Wrap="False" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="ServicePhone" HeaderText="服務電話" 
                                        SortExpression="ServicePhone">
                                    <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                                    <HeaderStyle Wrap="False" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CreateUserName" HeaderText="建檔人員" 
                                        SortExpression="CreateUserName">
                                    <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                                    <HeaderStyle Wrap="False" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CreateDate" DataFormatString="{0:yyyy/MM/dd HH:mm}" 
                                        HeaderText="建檔日期" HtmlEncode="false" SortExpression="CreateDate">
                                    <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                                    <HeaderStyle Wrap="False" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="LastModifyUserName" HeaderText="異動人員" 
                                        SortExpression="LastModifyUserName">
                                    <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                                    <HeaderStyle Wrap="False" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="LastModifyDate" 
                                        DataFormatString="{0:yyyy/MM/dd HH:mm}" HeaderText="異動日期" HtmlEncode="false" 
                                        SortExpression="LastModifyDate">
                                    <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                                    <HeaderStyle Wrap="False" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="EP_CheckUserName" HeaderText="企業覆核人員" 
                                        SortExpression="EP_CheckUserName">
                                    <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                                    <HeaderStyle Wrap="False" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="EP_CheckDate" 
                                        DataFormatString="{0:yyyy/MM/dd HH:mm}" HeaderText="企業覆核日期" HtmlEncode="false" 
                                        SortExpression="BU_CheckDate">
                                    <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                                    <HeaderStyle Wrap="False" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Rows" HeaderText="新增/修改" SortExpression="Rows">
                                    <ItemStyle HorizontalAlign="Center" Width="8%" Wrap="False" />
                                    <HeaderStyle Wrap="False" />
                                    </asp:BoundField>
                                </Columns>
                                <PagerSettings Visible="False" />
                            </asp:GridView>
                    </div> 
             </div>
    </ContentTemplate>
            </asp:UpdatePanel> 
    </form>
</body>
</html>
