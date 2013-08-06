<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PersonEdit.aspx.cs" Inherits="WebUI_BusinessUnit_PersonEdit" %>
<%@ Register src="../../Controls/Calendar.ascx" tagname="Calendar" tagprefix="uc2" %>
<%@ Register assembly="Js.PageControl" namespace="Js.PageControl" tagprefix="cc1" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
        <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />  
        <title></title> 
        <link href="~/Styles/Main.css" type="text/css" rel="stylesheet" /> 
        <link href="~/Styles/op.css" type="text/css" rel="stylesheet" /> 

        <script src='<%=ResolveUrl("~/Scripts/JQuery/jquery-1.8.3.min.js") %>' type="text/javascript"></script>
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Commons.js") %>'></script>
         <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Resize.js") %>'></script>  
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Ajax.js") %>'></script> 
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/DDL/JsDDL.js") %>'></script>
        <script type="text/javascript">
            $(document).ready(function () {
                $("#txtContactAddress").dblclick(function () { load(this); });
                $("#txtHomeAddress").dblclick(function () { load(this); });
                content_resize();   
            });
            function load(obj) {
                returnvalue = window.showModalDialog('../../TempAddress.aspx', window, 'dialogWidth:500px;dialogHeight:200px;help:no;status:no;scroll:auto;Resizable:yes;');
                if (returnvalue != null) {
                    var part = returnvalue.split("@#$");
                    if (obj.id == "txtContactAddress") {
                        document.getElementById("txtContactAddress").value = part[0];
                        //document.getElementById("txtZipCode1").value = part[1];
                    }
                    else {
                        document.getElementById("txtHomeAddress").value = part[0];
                        //document.getElementById("txtZipCode2").value = part[1];
                    }
                }
            }
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
                    var exist = IsExists("<%= FormID%>", $("#txtPersonID").val());
                    if (exist) {
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
                        <asp:Button ID="btnCancel" runat="server" Text="放棄" OnClientClick="return Cancel()" CssClass="ButtonCancel" />
                        <asp:Button ID="btnSave" runat="server" Text="保存" OnClientClick="return Save()" 
                            CssClass="ButtonSave" onclick="btnSave_Click" />
                        <asp:Button ID="btnExit" runat="server" Text="離開" OnClientClick="return Exit()" CssClass="ButtonExit" />
                    </td>
                </tr>
            </table>
             <div id="surdiv" style="overflow:auto">
			<table id="Table1" class="maintable" cellspacing="0" cellpadding="0" width="100%" align="center" bordercolor="#ffffff"
				border="1" runat="server">			
					<tr>
						<td  valign="middle" align="left" colspan="4" height="22" class="title1">
							<p><asp:Literal ID="ltlTitle" Text="營運人員資料[ 單筆編輯畫面 ]" runat="server"></asp:Literal></p>
						</td>
					</tr>							
						
					<tr>
						<td class="musttitle" align="center" width="15%" ><asp:Literal ID="ltlPersonID" Text="人員編號"
                                runat="server"></asp:Literal>
                        </td>
						<td width="35%" >&nbsp;
								<asp:textbox id="txtPersonID" runat="server" CssClass="TextBox" Width="90%" MaxLength="10"></asp:textbox>
                        </td>
						<td class="musttitle" align="center" width="15%"><asp:Literal 
                                ID="ltlDepartmentID" Text="所屬部門"
                                runat="server"></asp:Literal>
                        </td>
						<td  width="35%">&nbsp;
								<asp:DropDownList ID="ddlDepartmentID" Width="90%" runat="server"></asp:DropDownList>
                        </td>
					</tr>
                    		
					<tr>
						<td class="musttitle" align="center" width="15%" ><asp:Literal ID="ltlPersonName" Text="人員姓名"
                                runat="server"></asp:Literal>
                        </td>
						<td width="35%" >&nbsp;
								<asp:textbox id="txtPersonName" runat="server" CssClass="TextBox" Width="90%" MaxLength="20"></asp:textbox>
                        </td>
						<td class="smalltitle" align="center" width="15%"><asp:Literal 
                                ID="ltlPersonEName" Text="英文姓名"
                                runat="server"></asp:Literal>
                        </td>
						<td  width="35%">&nbsp;
								<asp:textbox id="txtPersonEName" runat="server" CssClass="TextBox" Width="90%" 
                                MaxLength="20"></asp:textbox>
                        </td>
					</tr>
                    <tr>
						<td class="musttitle" align="center" width="15%" ><asp:Literal ID="ltlPost" Text="職務名稱"
                                runat="server"></asp:Literal>
                        </td>
						<td width="35%" >&nbsp;
								<asp:textbox id="txtPost" runat="server" CssClass="TextBox" Width="90%" MaxLength="20"></asp:textbox>
                        </td>
						<td class="smalltitle" align="center" width="15%"><asp:Literal 
                                ID="ltlIDNumber" Text="身份證號"
                                runat="server"></asp:Literal>
                        </td>
						<td  width="35%">&nbsp;
								<asp:textbox id="txtIDNumber" runat="server" CssClass="TextBox" Width="90%" 
                                MaxLength="20"></asp:textbox>
                        </td>
					</tr>
                    <tr>
						<td class="smalltitle" align="center" width="15%" ><asp:Literal ID="ltlBirthday" Text="出生日期"
                                runat="server"></asp:Literal>
                        </td>
						<td width="35%" >&nbsp;
                                <uc2:Calendar ID="txtBirthday"  runat="server" />
                        </td>
						<td class="smalltitle" align="center" width="15%"><asp:Literal 
                                ID="ltlOnJobDate" Text="到職日期"
                                runat="server"></asp:Literal>
                        </td>
						<td  width="35%">&nbsp;
                         
								<uc2:Calendar ID="txtOnJobDate"  runat="server" />                         
								
                        </td>
					</tr>
                      <tr>
						<td class="smalltitle" align="center" width="15%" ><asp:Literal ID="ltlCellPhone" Text="行動電話"
                                runat="server"></asp:Literal>
                        </td>
						<td width="35%" >&nbsp;
								<asp:textbox id="txtCellPhone" runat="server" CssClass="TextBox" Width="90%" MaxLength="16"></asp:textbox>
                        </td>
						<td class="smalltitle" align="center" width="15%"><asp:Literal 
                                ID="ltlLeftJobDate" Text="離職日期"
                                runat="server"></asp:Literal>
                        </td>
						<td  width="35%">&nbsp;
								<uc2:Calendar ID="txtLeftJobDate"  runat="server" />
								
                        </td>
					</tr>
                     <tr>
						<td class="smalltitle" align="center" width="15%" ><asp:Literal ID="ltlHomePhone" Text="住家電話"
                                runat="server"></asp:Literal>
                        </td>
						<td width="35%"  colspan="3">&nbsp;
								<asp:textbox id="txtHomePhone" runat="server" CssClass="TextBox" Width="37%" MaxLength="16"></asp:textbox>
                        </td>
						
					</tr>
					<tr>
						<td class="smalltitle" align="center" ><asp:Literal ID="ltlEmail" Text="E-MAIL"
                                runat="server"></asp:Literal>
                        </td>
						<td width="85%" colspan="3" >&nbsp;
								<asp:textbox id="txtEmail" runat="server" CssClass="TextBox" Width="96%" MaxLength="30"></asp:textbox>
                        </td>
					</tr>
                    <tr>
						<td class="smalltitle" align="center" ><asp:Literal ID="ltlContactAddress" Text="連絡地址"
                                runat="server"></asp:Literal>
                        </td>
						<td width="85%" colspan="3" >&nbsp;
								<asp:textbox id="txtContactAddress" runat="server" CssClass="TextBox" Width="96%" MaxLength="100"></asp:textbox>
                        </td>
					</tr>
                    <tr>
						<td class="smalltitle" align="center" ><asp:Literal ID="ltlHomeAddress" Text="住家地址"
                                runat="server"></asp:Literal>
                        </td>
						<td width="85%" colspan="3" >&nbsp;
								<asp:textbox id="txtHomeAddress" runat="server" CssClass="TextBox" Width="96%" MaxLength="100"></asp:textbox>
                        </td>
					</tr>
					 <tr>
						<td class="smalltitle" align="center" width="15%" ><asp:Literal ID="ltlLinkMan" Text="緊急聯絡人"
                                runat="server"></asp:Literal>
                        </td>
						<td width="35%"  height="22px" align="left">
                          <table width="92%" style="height:100%;margin:0px;padding:0px;">
                           <tr>
                           <td width="40%">&nbsp;<asp:textbox id="txtLinkMan" runat="server" CssClass="TextBox" Width="92%" MaxLength="15"></asp:textbox>
                           </td>
                            <td width="20%" class="smalltitle" align="center">
                              <asp:Literal ID="ltlRelation" Text="關係" runat="server"></asp:Literal>
                           </td>
                            <td width="38%">
                              
                                <cc1:DDL ID="txtRelation" runat="server" Width="85%" />
                        </td>
                        
                          </tr></table>
						</td>	
                            
						<td class="smalltitle" align="center" width="15%"><asp:Literal 
                                ID="ltlLinkPhone" Text="緊急聯絡電話"
                                runat="server"></asp:Literal>
                        </td>
						<td  width="35%">&nbsp;
								<asp:textbox id="txtLinkPhone" runat="server" CssClass="TextBox" Width="90%" 
                                MaxLength="16"></asp:textbox>
                        </td>
					</tr>
                    <tr>
						<td class="smalltitle" align="center" ><asp:Literal 
                                ID="ltlMemo" Text="備&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;註"
                                runat="server"></asp:Literal>
                        </td>
						<td  width="85%" colspan="3" height="70">&nbsp;
								<asp:textbox id="txtMemo" runat="server" CssClass="TextBox" Width="96%" MaxLength="2000" TextMode="MultiLine"
									Height="59px" ForeColor="Black"></asp:textbox>
                         </td>
					</tr>
                    <tr>
						<td class="smalltitle" align="center"><asp:Literal ID="ltlCreateUserName" Text="建檔人員"
                                runat="server"></asp:Literal>
                        </td>
						<td >&nbsp;
								<asp:textbox id="txtCreateUserName" runat="server" CssClass="TextRead" Width="90%" 
                                MaxLength="20" ReadOnly="True"></asp:textbox>
                         </td>
						<td class="smalltitle" align="center">                            
                            <asp:Literal ID="ltlCreateDate" runat="server" Text="建檔日期"></asp:Literal>							
						    
						</td>
						<td>&nbsp;
								<asp:textbox id="txtCreateDate" runat="server" CssClass="TextRead" Width="90%" 
                                MaxLength="20" ReadOnly="True"></asp:textbox></td>
					</tr>
                    <tr>
						<td class="smalltitle" align="center"><asp:Literal ID="ltlLastModifyUserName" Text="異動人員"
                                runat="server"></asp:Literal>
                        </td>
						<td >&nbsp;
								<asp:textbox id="txtLastModifyUserName" runat="server" CssClass="TextRead" Width="90%" MaxLength="20" ReadOnly="true" ></asp:textbox>
                        </td>
						<td class="smalltitle" align="center">							
						 
                            <asp:Literal ID="ltlLastModifyDate" Text="異動日期"
                                runat="server"></asp:Literal>
						</td>
						<td >&nbsp;
                        <asp:textbox id="txtLastModifyDate" runat="server" CssClass="TextRead" Width="90%" 
                                MaxLength="20" ReadOnly="true"></asp:textbox></td>
					</tr>
                    <tr>
						<td class="smalltitle" align="center"><asp:Literal ID="ltlCheckUserName" Text="覆核人員"
                                runat="server"></asp:Literal>
                        </td>
						<td >&nbsp;
								<asp:textbox id="txtCheckUserName" runat="server" CssClass="TextRead" Width="90%" MaxLength="20" ReadOnly="true"></asp:textbox>
                        </td>
						<td class="smalltitle" align="center">	
                            <asp:Literal ID="ltlCheckDate" Text="覆核日期"
                                runat="server"></asp:Literal>
						</td>
						<td >&nbsp;
								<asp:textbox id="txtCheckDate" runat="server" CssClass="TextRead" Width="90%" ReadOnly="true"
                                MaxLength="20"></asp:textbox>                           
                        </td>
					</tr>
                    <tr>
						<td class="smalltitle" align="center"><asp:Literal ID="ltlStopUserName" Text="停用人員"
                                runat="server"></asp:Literal>
                        </td>
						<td >&nbsp;
								<asp:textbox id="txtStopUserName" runat="server" CssClass="TextRead" Width="90%" MaxLength="20"></asp:textbox>
                        </td>
						<td class="smalltitle" align="center">
						 
                            <asp:Literal ID="ltlStopDate" Text="停用日期"
                                runat="server"></asp:Literal>
						</td>
						<td >&nbsp;
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
