<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CheckUser.aspx.cs" Inherits="WebUI_CheckConfirm_CheckUser" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="~/Styles/Main.css" type="text/css" rel="stylesheet" /> 
        <link href="~/Styles/op.css" type="text/css" rel="stylesheet" /> 
        <script type="text/javascript" src='<%=ResolveUrl("~/Scripts/JQuery/jquery-1.8.3.min.js") %>'></script>
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Commons.js") %>'></script>
        <script type="text/javascript">
            $(document).ready(function () {
                //$("#txtRegisterAddress").dblclick(function () { load(this); });
            });
            function load(obj) {
                //if ($("#btnEdit").attr("disabled") != null) {
                returnvalue = window.showModalDialog('../../TempAddress.aspx', window, 'dialogWidth:500px;dialogHeight:200px;help:no;status:no;scroll:auto;Resizable:yes;');
                if (returnvalue != null) {
                    var part = returnvalue.split("@#$");
                    obj.value = part[0];
                }
                //}
            }
            function Cancel() {
                return confirm("<%=Resources.Resource.Question_Cancel %>");
            }
            function eExit() {
                if (confirm("<%=Resources.Resource.Question_Exit %>"))
                    Exit();
            }
            function Save() {
                $("#txtCompanyNo").val(trim($("#txtCompanyNo").val()));
                $("#txtCompanyName").val(trim($("#txtCompanyName").val()));
                if ($("#txtCompanyNo").val() == "") {
                    alert("<%=Resources.Resource.BU_DepartmentID_NotNull %>");
                    $("#txtCompanyNo").focus();
                    return false;
                }
                if ($("#txtCompanyName").val() == "") {
                    alert("<%=Resources.Resource.BU_CompanyName_NotNull %>");
                    $("#txtCompanyName").focus();
                    return false;
                }
                if ($("#txtUnionID").val() == "") {
                    alert("<%=Resources.Resource.BU_UnionID_NotNull %>");
                    $("#txtUnionID").focus();
                    return false;
                }
                return true;
            }            

        </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table class="maintable" cellspacing="0" cellpadding="0" width="100%" align="center" bordercolor="#ffffff" border="1">			
                    <tr>
						<td class="smalltitle" align="center" width="15%" ><asp:Literal ID="ltlUserPwd" Text="請輸入使用者密碼"
                                runat="server"></asp:Literal>
                        </td>
						<td width="35%" height="40px">&nbsp;
								<asp:textbox id="txtUserPwd" runat="server" CssClass="TextBox" Width="90%" 
                                MaxLength="6" TextMode="Password"></asp:textbox>
                        </td>
						
					</tr>
                    <tr>
						<td align="center" colspan="2" height="40px">
                            <asp:button id="btnOK" runat="server" Text="確定" 
                                            CssClass="but" Width="75px" onclick="btnOK_Click">
                                    </asp:button>                          
                        </td>
						
						
					</tr>
			</table>
    </div>
    </form>
</body>
</html>
