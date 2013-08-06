<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PersonEdit.aspx.cs" Inherits="WebUI_BusinessUnit_PersonEdit" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>
<%@ Register src="../../Controls/Calendar.ascx" tagname="Calendar" tagprefix="uc2" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
        <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />  
        <title></title> 
        <link href="~/Styles/Main.css" type="text/css" rel="stylesheet" /> 
        <link href="~/Styles/op.css" type="text/css" rel="stylesheet" /> 
         <link href="../../JScript/Date/lyz.calendar.css" type="text/css" rel="stylesheet" /> 

        <script type="text/javascript" src='<%=ResolveUrl("~/Scripts/JQuery/jquery-1.8.3.min.js") %>'></script>
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Commons.js") %>'></script>
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Resize.js") %>'></script>  
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Ajax.js") %>'></script>
        <script type="text/javascript" src="../../JScript/Date/lyz.calendar.min.js"></script>

        <script type="text/javascript">
            $(document).ready(function () {
                content_resize();               
            });
            function Save() {
                $("#txtPersonID").val(trim($("#txtPersonID").val()));
                $("#txtPersonName").val(trim($("#txtPersonName").val()));
                if ($("#txtPersonID").val() == "") {
                    alert("<%=Resources.Resource.BU_PersonID_NotNull %>");
                    $("#txtPersonID").focus();
                    return false;
                }
                if ($("#ddlDepartmentID").val() == "") {
                    alert("<%=Resources.Resource.BU_DepartmentID_NotNull %>");
                    $("#ddlDepartmentID").focus();
                    return false;
                }
                if ($("#txtPersonName").val() == "") {
                    alert("<%=Resources.Resource.BU_PersonName_NotNull %>");
                    $("#txtPersonName").focus();
                    return false;
                }
                if ($("#txtPost").val() == "") {
                    alert("<%=Resources.Resource.BU_Post_NotNull %>");
                    $("#txtPost").focus();
                    return false;
                }
                if ("<%= ID%>" == "") {
                    // var exist = IsExists("<%= FormID%>", $("#txtPersonID").val(), "<%= cnKey%>");
                    var exist = IsExistsByFilter("<%= FormID%>", $("#txtEnterpriseID").val(), $("#txtPersonID").val(), "<%= cnKey%>");
                    if (exist=="1") {
                        alert("<%=Resources.Resource.BU_PersonID_Exists %>");
                        $("#txtPersonID").focus();
                        return false;
                    }
                }
                return true;
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
                        <asp:Button ID="btnCancel" runat="server" Text="放棄" 
                              OnClientClick="return Cancel()" CssClass="ButtonCancel" 
                              meta:resourcekey="btnCancelResource1" />
                        <asp:Button ID="btnSave" runat="server" Text="保存" OnClientClick="return Save()" 
                            CssClass="ButtonSave" onclick="btnSave_Click" 
                              meta:resourcekey="btnSaveResource1" />
                        <asp:Button ID="btnExit" runat="server" Text="離開" OnClientClick="return Exit()" 
                              CssClass="ButtonExit" meta:resourcekey="btnExitResource1" />
                    </td>
                </tr>
            </table>
             <div id="surdiv" style="overflow:auto">
			<table id="Table1" class="maintable" cellspacing="0" cellpadding="0" width="100%" align="center" bordercolor="#ffffff"
				border="1" runat="server">			
					<tr runat="server">
						<td  valign="middle" align="left" colspan="4" height="22" class="title1" 
                            runat="server">
							<p><asp:Literal ID="ltlTitle" Text="企業用戶人員資料[ 單筆編輯畫面 ]" runat="server"></asp:Literal></p>
						</td>
					</tr>							
						<tr runat="server">
						<td class="smalltitle" align="center" width="15%" runat="server" ><asp:Literal ID="ltlEnterpriseID" Text="企業編號"
                                runat="server"></asp:Literal></td>
						<td width="35%" runat="server" >&nbsp;
								<asp:textbox id="txtEnterpriseID" runat="server" CssClass="TextRead" 
                                Width="90%" MaxLength="20" ReadOnly="True"></asp:textbox>
                        </td>
						<td class="smalltitle" align="center" width="15%" runat="server"><asp:Literal 
                                ID="ltlEnterpriseName" Text="企業名稱"
                                runat="server"></asp:Literal></td>
						<td  width="35%" runat="server">&nbsp;
								<asp:textbox id="txtEnterpriseName" runat="server" CssClass="TextRead" Width="90%" 
                                MaxLength="20" ReadOnly="True"></asp:textbox>
                        </td>
					</tr>	
					<tr runat="server">
						<td class="musttitle" align="center" width="15%" runat="server" ><asp:Literal ID="ltlPersonID" Text="人員編號"
                                runat="server"></asp:Literal>
                        </td>
						<td width="35%" runat="server" >&nbsp;
								<asp:textbox id="txtPersonID" runat="server" CssClass="TextBox" Width="90%" MaxLength="10"></asp:textbox>
                        </td>
						<td class="musttitle" align="center" width="15%" runat="server">
                            <asp:Literal 
                                ID="ltlDepartmentID" Text="所屬單位"
                                runat="server"></asp:Literal>
                        </td>
						<td  width="35%" runat="server">&nbsp;
								<asp:DropDownList ID="ddlDepartmentID" Width="90%" runat="server"></asp:DropDownList></td>
					</tr>
                    		
					<tr runat="server">
						<td class="musttitle" align="center" width="15%" runat="server" ><asp:Literal ID="ltlPersonName" Text="人員姓名"
                                runat="server"></asp:Literal>
                        </td>
						<td width="35%" runat="server" >&nbsp;
								<asp:textbox id="txtPersonName" runat="server" CssClass="TextBox" Width="90%" MaxLength="10"></asp:textbox>
                        </td>
						<td class="smalltitle" align="center" width="15%" runat="server"><asp:Literal 
                                ID="ltlPersonEName" Text="英文姓名"
                                runat="server"></asp:Literal>
                        </td>
						<td  width="35%" runat="server">&nbsp;
								<asp:textbox id="txtPersonEName" runat="server" CssClass="TextBox" Width="90%" 
                                MaxLength="20"></asp:textbox>
                        </td>
					</tr>
                    <tr runat="server">
						<td class="musttitle" align="center" width="15%" runat="server" ><asp:Literal ID="ltlPost" Text="職務名稱"
                                runat="server"></asp:Literal>
                        </td>
						<td width="35%" runat="server" >&nbsp;
								<asp:textbox id="txtPost" runat="server" CssClass="TextBox" Width="90%" MaxLength="20"></asp:textbox>
                        </td>
						<td class="smalltitle" align="center" width="15%" runat="server">
                            <asp:Literal ID="ltlCellPhone" runat="server" Text="行動電話"></asp:Literal>
                        </td>
						<td  width="35%" runat="server">&nbsp;
								<asp:TextBox ID="txtCellPhone" runat="server" CssClass="TextBox" 
                                MaxLength="16" Width="90%"></asp:TextBox>
                        </td>
					</tr>
					<tr runat="server">
						<td class="smalltitle" align="center" runat="server" ><asp:Literal ID="ltlEmail" Text="E-MAIL"
                                runat="server"></asp:Literal>
                        </td>
						<td width="85%" colspan="3" runat="server" >&nbsp;
								<asp:textbox id="txtEmail" runat="server" CssClass="TextBox" Width="96%" MaxLength="30"></asp:textbox>
                        </td>
					</tr>
                    <tr runat="server">
						<td class="smalltitle" align="center" runat="server" ><asp:Literal ID="ltlContactAddress" Text="連絡地址"
                                runat="server"></asp:Literal>
                        </td>
						<td width="85%" colspan="3" runat="server" >&nbsp;
								<asp:textbox id="txtContactAddress" runat="server" CssClass="TextBox" Width="96%" MaxLength="100"></asp:textbox>
                        </td>
					</tr>
					
                    <tr>
						<td class="smalltitle" align="center" runat="server" ><asp:Literal 
                                ID="ltlMemo" Text="備&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;註"
                                runat="server"></asp:Literal>
                        </td>
						<td  width="85%" colspan="3" height="70" runat="server">&nbsp;
								<asp:textbox id="txtMemo" runat="server" CssClass="TextBox" Width="96%" MaxLength="2000" TextMode="MultiLine"
									Height="59px" ForeColor="Black"></asp:textbox>
                         </td>
					</tr>
                    <tr>
						<td class="smalltitle" align="center" runat="server"><asp:Literal ID="ltlCreateUserName" Text="建檔人員"
                                runat="server"></asp:Literal>
                        </td>
						<td runat="server" >&nbsp;
								<asp:textbox id="txtCreateUserName" runat="server" CssClass="TextRead" Width="90%" 
                                MaxLength="20" ReadOnly="True"></asp:textbox>
                         </td>
						<td class="smalltitle" align="center" runat="server">                            
                            <asp:Literal ID="ltlCreateDate" runat="server" Text="建檔日期"></asp:Literal>							
						    
						</td>
						<td runat="server">&nbsp;
								<asp:textbox id="txtCreateDate" runat="server" CssClass="TextRead" Width="90%" 
                                MaxLength="20" ReadOnly="True"></asp:textbox></td>
					</tr>
                    <tr runat="server">
						<td class="smalltitle" align="center" runat="server"><asp:Literal ID="ltlLastModifyUserName" Text="異動人員"
                                runat="server"></asp:Literal>
                        </td>
						<td runat="server" >&nbsp;
								<asp:textbox id="txtLastModifyUserName" runat="server" CssClass="TextRead" 
                                Width="90%" MaxLength="20" ReadOnly="True" ></asp:textbox>
                        </td>
						<td class="smalltitle" align="center" runat="server">							
						 
                            <asp:Literal ID="ltlLastModifyDate" Text="異動日期"
                                runat="server"></asp:Literal>
						</td>
						<td runat="server" >&nbsp;
                        <asp:textbox id="txtLastModifyDate" runat="server" CssClass="TextRead" Width="90%" 
                                MaxLength="20" ReadOnly="True"></asp:textbox></td>
					</tr>
                    <tr runat="server">
						<td class="smalltitle" align="center" runat="server"><asp:Literal ID="ltlCheckUserName" Text="企業覆核人員"
                                runat="server"></asp:Literal>
                        </td>
						<td runat="server" >&nbsp;
								<asp:textbox id="txtCheckUserName" runat="server" CssClass="TextRead" 
                                Width="90%" MaxLength="20" ReadOnly="True"></asp:textbox>
                        </td>
						<td class="smalltitle" align="center" runat="server">	
                            <asp:Literal ID="ltlCheckDate" Text="企業覆核日期"
                                runat="server"></asp:Literal>
						</td>
						<td runat="server" >&nbsp;
								<asp:textbox id="txtCheckDate" runat="server" CssClass="TextRead" 
                                Width="90%" ReadOnly="True"
                                MaxLength="20"></asp:textbox>                           
                        </td>
					</tr>
                    <tr runat="server">
						<td class="smalltitle" align="center" runat="server"><asp:Literal ID="ltlStopUserName" Text="停用人員"
                                runat="server"></asp:Literal>
                        </td>
						<td runat="server" >&nbsp;
								<asp:textbox id="txtStopUserName" runat="server" CssClass="TextRead" Width="90%" MaxLength="20"></asp:textbox>
                        </td>
						<td class="smalltitle" align="center" runat="server">
						 
                            <asp:Literal ID="ltlStopDate" Text="停用日期"
                                runat="server"></asp:Literal>
						</td>
						<td runat="server" >&nbsp;
								<asp:textbox id="txtStopDate" runat="server" CssClass="TextRead" Width="90%" 
                                MaxLength="20"></asp:textbox>
                       </td>
					</tr>
				
			</table>
            </div>
    </div>
     </ContentTemplate>
            </asp:UpdatePanel> 
    </form>
</body>
</html>
