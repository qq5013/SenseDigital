<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProductResumeCheck.aspx.cs" Inherits="WebUI_BusinessUnit_ProductResumeCheck" %>

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
                    if (gdview.rows(i).cells(18).innerText == "<%=Resources.Resource.Modify %>") {
                        haveModify = true;
                        break;
                    }
                }
                if (!haveModify) {
                    alert("<%=Resources.Resource.EP_NoModifyCompareRecord%>");
                    return false;
                }
                var TitleName = escape("<%=Resources.Resource.ProductResumeCompare %>");
                var page = "ProductResumeCompare.aspx";
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
       <div id="surdiv" style="overflow:auto">
            <table id="Table1" class="maintable" cellspacing="0" cellpadding="0" width="100%" align="center" bordercolor="#ffffff"
				border="1" runat="server">			
					<tr>
						<td  valign="middle" align="left" colspan="2" height="22" class="title1">
			 				<p><asp:Literal ID="ltlTitle" Text="營運單位產品生產履歷(未認證)[ 單筆明细畫面 ]" runat="server"></asp:Literal></p>
						</td>
					</tr>							
						
					<tr>
						<td class="musttitle" align="center" width="15%" ><asp:Literal ID="ltlUploadDate" Text="企業上傳日期"
                                runat="server"></asp:Literal>
                        </td>
						<td>&nbsp;								
                                <asp:textbox id="txtUploadDate" runat="server" CssClass="TextRead"  ReadOnly="true"  Width="30%" ></asp:textbox> 
                            &nbsp; 
                            <asp:Button ID="btnCheck" runat="server" CssClass="but" OnClientClick="return Check()"  
                                Text="營運覆核" Width="75px" onclick="btnCheck_Click" />  
                                &nbsp;<asp:Button ID="btnCompare" runat="server" OnClientClick="return Open()" CssClass="but" Text="比對記錄查詢" Width="92px" />                          
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
                    SkinID="GridViewSkin" Width="1000px" onrowdatabound="GridView1_RowDataBound">
            <Columns>                
               <asp:BoundField DataField="RowID" HeaderText="序號" 
                    SortExpression="RowID">
                    <ItemStyle HorizontalAlign="Center" Width="8%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="ResumeID" HeaderText="履歷編號" 
                    SortExpression="ResumeID">
                    <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
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
                <asp:BoundField DataField="ProduceDate" HeaderText="生產日期" HtmlEncode="false" DataFormatString="{0:yyyy/MM/dd HH:mm}"
                    SortExpression="ProduceDate">
                    <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                <asp:CheckBoxField DataField="HasGarantee" HeaderText="是否有保固期限" 
                    SortExpression="HasGarantee">
                    <ItemStyle HorizontalAlign="center" Width="8%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:CheckBoxField>
              <asp:BoundField DataField="GaranteeDate" HeaderText="保固日期" HtmlEncode="false" DataFormatString="{0:yyyy/MM/dd HH:mm}"
                    SortExpression="GaranteeDate">
                    <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="官方網址"  SortExpression="OfficalUrl">
                    <ItemTemplate>
                    <asp:HyperLink id="HyperLink2" runat="server" Target="_blank"                            
                            NavigateUrl='<%# "http://" + DataBinder.Eval(Container.DataItem, "OfficalUrl") %>'><%# DataBinder.Eval(Container.DataItem, "OfficalUrl")%></asp:HyperLink>                       
                    </ItemTemplate>
                  <ItemStyle Width="12%" Wrap="False" />
                  <HeaderStyle Width="12%" Wrap="False" />
               </asp:TemplateField>
                <asp:BoundField DataField="ServicePhone" HeaderText="客服電話" 
                    SortExpression="ServicePhone">
                    <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="Description" HeaderText="生產履歷描述" 
                    SortExpression="Description">
                    <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="生產履歷網址"  SortExpression="ResumeOfficalUrl">
                    <ItemTemplate>
                    <asp:HyperLink id="HyperLink3" runat="server" Target="_blank" NavigateUrl='<%# "http://" + DataBinder.Eval(Container.DataItem, "ResumeOfficalUrl") %>'>
                            <%# DataBinder.Eval(Container.DataItem, "ResumeOfficalUrl")%></asp:HyperLink>                       
                    </ItemTemplate>
                  <ItemStyle Width="12%" Wrap="False" />
                  <HeaderStyle Width="12%" Wrap="False" />
               </asp:TemplateField>
                <asp:BoundField DataField="EPResumeID" HeaderText="企業生產履歷編號" 
                    SortExpression="EPResumeID">
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
                <asp:BoundField DataField="BU_CheckDate" HeaderText="企業覆核日期" HtmlEncode="false" DataFormatString="{0:yyyy/MM/dd HH:mm}"
                    SortExpression="BU_CheckDate">
                    <ItemStyle HorizontalAlign="Left" Width="8%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
               <asp:BoundField DataField="Rows" HeaderText="新增/修改" 
                    SortExpression="Rows">
                    <ItemStyle HorizontalAlign="Center" Width="8%" Wrap="False" />
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>  
            </Columns>
            <PagerSettings Visible="False" />
        </asp:GridView>
        
            </div>
            
        </div>
     </div>
    </ContentTemplate>
            </asp:UpdatePanel> 
    </form>
</body>
</html>
