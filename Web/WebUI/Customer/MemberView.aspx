<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MemberView.aspx.cs" Inherits="WebUI_Customer_MemberView" %>

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
                        <asp:Button ID="btnAdd" runat="server" Text="新增" CssClass="ButtonCreate" OnClientClick="Edit(false);return false" />
                        <asp:Button ID="btnDelete" runat="server" Text="刪除" CssClass="ButtonDel" 
                            onclick="btnDelete_Click" />
                        <asp:Button ID="btnEdit" runat="server" Text="修改" CssClass="ButtonModify" 
                            OnClientClick="return Edit(true);" onclick="btnEdit_Click" />
                        <asp:Button ID="btnExit" runat="server" Text="離開" OnClientClick="Exit()" CssClass="ButtonExit" />
                    </td>
                </tr>
            </table>
		<table class="maintable" cellspacing="0" cellpadding="0" width="100%" align="center" bordercolor="#ffffff"
				border="1" >			
					<tr>
						<td  valign="middle" align="left" colspan="4" height="22" class="title1">
							<p><asp:Literal ID="ltlTitle" Text="會員資料[ 單筆明細畫面 ]" runat="server"></asp:Literal></p>
						</td>
					</tr>				
						
					<tr>
						<td class="musttitle" align="center" width="15%" ><asp:Literal ID="ltlMemberID" Text="會員ID"
                                runat="server"></asp:Literal>
                        </td>
						<td width="35%" >&nbsp;
								<asp:textbox id="txtMemberID" runat="server" CssClass="TextRead"  ReadOnly="true" Width="90%" MaxLength="20"></asp:textbox>
                        </td>
						<td class="musttitle" align="center" width="15%"><asp:Literal 
                                ID="ltlCountry" Text="國&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;別"
                                runat="server"></asp:Literal>
                        </td>
						<td  width="35%">&nbsp;
							<asp:DropDownList runat="server" ID="ddlCountry"  Width="90%" Enabled="false"></asp:DropDownList>
                        </td>
					</tr>
                    <tr>
						<td class="musttitle" align="center" width="15%" ><asp:Literal ID="ltlPassword" Text="密&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;碼"
                                runat="server"></asp:Literal>
                        </td>
						<td width="35%" >&nbsp;
								<asp:textbox id="txtPassword" runat="server" CssClass="TextRead"  
                                ReadOnly="true" Width="90%" MaxLength="15" TextMode="Password"></asp:textbox>
                        </td>
						<td class="musttitle" align="center" width="15%"><asp:Literal 
                                ID="ltlPasswordHint" Text="密碼提示"
                                runat="server"></asp:Literal>
                        </td>
						<td  width="35%">&nbsp;
							<asp:textbox id="txtPasswordHint" runat="server" CssClass="TextRead"  ReadOnly="true" Width="90%" MaxLength="20"></asp:textbox>
                        </td>
					</tr>
					<tr>
						<td class="musttitle" align="center" width="15%" ><asp:Literal ID="ltlName" Text="姓&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;名"
                                runat="server"></asp:Literal>
                        </td>
						<td width="35%" >&nbsp;
								<asp:textbox id="txtName" runat="server" CssClass="TextRead"  ReadOnly="true" Width="90%" MaxLength="20"></asp:textbox>
                        </td>
						<td class="smalltitle" align="center" width="15%"><asp:Literal 
                                ID="ltlSex" Text="性&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;別"
                                runat="server"></asp:Literal>
                        </td>
						<td  width="35%">&nbsp;
							<asp:DropDownList runat="server" ID="ddlSex"  Width="90%" Enabled="false">
                                <asp:ListItem Value="0">男</asp:ListItem>
                                <asp:ListItem Value="1">女</asp:ListItem>
                            </asp:DropDownList>
                        </td>
					</tr>
					<tr>
						<td class="musttitle" align="center" ><asp:Literal 
                                ID="ltlEMail" Text="電子郵件"
                                runat="server"></asp:Literal>
                        </td>
						<td  width="85%" colspan="3" >&nbsp;
								<asp:textbox id="txtEMail" runat="server" CssClass="TextRead"  ReadOnly="true" Width="96%" MaxLength="30" ></asp:textbox>
                         </td>
					</tr>
                    <tr>
						<td class="smalltitle" align="center" ><asp:Literal 
                                ID="ltlSpareEMail" Text="備用郵件"
                                runat="server"></asp:Literal>
                        </td>
						<td  width="85%" colspan="3" >&nbsp;
								<asp:textbox id="txtSpareEMail" runat="server" CssClass="TextRead"  ReadOnly="true" Width="96%" MaxLength="30" ></asp:textbox>
                         </td>
					</tr>
                     <tr>
						<td class="smalltitle" align="center" ><asp:Literal 
                                ID="ltlBirthday" Text="出生日期"
                                runat="server"></asp:Literal>
                        </td>
						<td  width="85%" colspan="3" >&nbsp;
							<asp:textbox id="txtBirthday" runat="server" CssClass="TextRead"  ReadOnly="true" Width="35%" MaxLength="100" ></asp:textbox>
                         </td>
					</tr>
                       <tr>
						<td class="smalltitle" align="center" ><asp:Literal 
                                ID="ltlCellPhone" Text="行動電話"
                                runat="server"></asp:Literal>
                        </td>
						<td  width="85%" colspan="3" >&nbsp;
								<asp:textbox id="txtCellPhone" runat="server" CssClass="TextRead"  ReadOnly="true" Width="96%" MaxLength="16" ></asp:textbox>
                         </td>
					</tr>
                       <tr>
						<td class="smalltitle" align="center" ><asp:Literal 
                                ID="ltlLinkPhone" Text="聯絡電話"
                                runat="server"></asp:Literal>
                        </td>
						<td  width="85%" colspan="3" >&nbsp;
								<asp:textbox id="txtLinkPhone" runat="server" CssClass="TextRead"  ReadOnly="true" Width="96%" MaxLength="16" ></asp:textbox>
                         </td>
					</tr>
                    <tr>
						<td class="smalltitle" align="center" ><asp:Literal 
                                ID="ltlAddress1" Text="通訊地址1"
                                runat="server"></asp:Literal>
                        </td>
						<td  width="85%" colspan="3">&nbsp; <asp:textbox id="txtZipCode1" runat="server" CssClass="TextRead" Width="20%" MaxLength="100"
									ForeColor="Black"></asp:textbox>
								&nbsp;<asp:textbox id="txtAddress1" runat="server" CssClass="TextRead" Width="75%" MaxLength="100"
									ForeColor="Black"></asp:textbox>
                         </td>
					</tr>
                      <tr>
						<td class="smalltitle" align="center" ><asp:Literal 
                                ID="ltlAddress2" Text="通訊地址2"
                                runat="server"></asp:Literal>
                        </td>
						<td  width="85%" colspan="3">&nbsp; <asp:textbox id="txtZipCode2" runat="server" CssClass="TextRead" Width="20%" MaxLength="100"
									ForeColor="Black"></asp:textbox>
								&nbsp;<asp:textbox id="txtAddress2" runat="server" CssClass="TextRead" Width="75%" MaxLength="100"
									ForeColor="Black"></asp:textbox>
                         </td>
					</tr>
                    <tr>
						<td class="smalltitle" align="center" ><asp:Literal 
                                ID="ltlWebSite" Text="個人網站"
                                runat="server"></asp:Literal>
                        </td>
						<td  width="85%" colspan="3" >&nbsp;
								<asp:textbox id="txtWebSite" runat="server" CssClass="TextRead"  ReadOnly="true" Width="92%" MaxLength="50" ></asp:textbox>
                                &nbsp;
                            <asp:HyperLink ID="lnkWebSite" Target="_blank" runat="server">Go</asp:HyperLink>
                         </td>
					</tr>
                    <tr>
						<td class="smalltitle" align="center" width="15%" ><asp:Literal ID="ltlCreateDate" Text="創建日期"
                                runat="server"></asp:Literal>
                        </td>
						<td width="35%" >&nbsp;
								<asp:textbox id="txtCreateDate" runat="server" CssClass="TextRead"  
                                ReadOnly="true" Width="90%" MaxLength="20"></asp:textbox>
                        </td>
						<td class="smalltitle" align="center" width="15%"><asp:Literal 
                                ID="ltlLastModifyDate" Text="異動日期"
                                runat="server"></asp:Literal>
                        </td>
						<td  width="35%">&nbsp;
							<asp:textbox id="txtLastModifyDate" runat="server" CssClass="TextRead"  ReadOnly="true" Width="90%" MaxLength="20"></asp:textbox>
                        </td>
					</tr>
			</table>
			
	</div>
       </ContentTemplate>
         </asp:UpdatePanel> 
		</form>
	</body>
</html>
