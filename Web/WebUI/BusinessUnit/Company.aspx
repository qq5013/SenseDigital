<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Company.aspx.cs" Inherits="WebUI_BusinessUnit_Company" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />  
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
                    returnvalue = window.showModalDialog('../../TempAddress.aspx', window, 'dialogWidth:520px;dialogHeight:200px;help:no;status:no;scroll:auto;Resizable:yes;');
                    if (returnvalue != null) {
                        var part = returnvalue.split("@#$");
                        obj.value = part[0];
                    }
                //}
            }
            function Cancel() {
                return confirm("<%=Resources.Resource.Question_Cancel %>");
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
            function openfile() {
                try {
                    var fd = new ActiveXObject("MSComDlg.CommonDialog");
                    fd.Filter = "上傳文件 (*.jpg;*.jpeg;*.gif)|*.jpg;*.jpeg;*.gif";
                    fd.FilterIndex = 2;
                    // 必须设置MaxFileSize. 否则出错 
                    fd.MaxFileSize = 128;
                    fd.ShowOpen();
                    document.getElementById("txtFilePath").value = fd.Filename;
                }
                catch (e) {
                    document.getElementById("txtFileName").value = "";
                }
            }

        </script>
</head>
<body>
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
                        &nbsp;&nbsp;<asp:Button ID="btnCancel" runat="server" Text="放棄" 
                            CssClass="ButtonCancel" OnClientClick="return Cancel()"
                            onclick="btnCancel_Click" meta:resourcekey="btnCancelResource1" />
                        <asp:Button ID="btnEdit" runat="server" Text="修改" CssClass="ButtonModify" 
                            onclick="btnEdit_Click" meta:resourcekey="btnEditResource1" />
                        <asp:Button ID="btnSave" runat="server" Text="保存" CssClass="ButtonSave" OnClientClick="return Save()"
                            onclick="btnSave_Click" meta:resourcekey="btnSaveResource1" />
                        <asp:Button ID="btnExit" runat="server" Text="離開" OnClientClick=" return Exit()" 
                            CssClass="ButtonExit" meta:resourcekey="btnExitResource1" />
                    </td>
                </tr>
       </table>
      <table id="Table1" class="maintable" cellspacing="0" cellpadding="0" width="100%" align="center" bordercolor="#ffffff"
				border="1" runat="server">			
					<tr runat="server">
						<td style="HEIGHT: 29px" valign="middle" align="center" colspan="4" height="22" 
                            runat="server">
							<table cellspacing="0" cellpadding="0" width="100%" border="0">
								<tr>
									<td class="titline1">
										<table cellspacing="0" cellpadding="0" width="100%" border="0">
											<tr>
												<td class="title1"><p><asp:Literal ID="ltlTitle" Text="營運單位公司資料設定" runat="server"></asp:Literal></p></td>
											</tr>
											
										</table>
									</td>
									<td class="titline4" width="85%" align="right">                                     
                                    </td>
									
								</tr>
							</table>
						</td>
					</tr>							
                    <tr runat="server">
						<td class="musttitle" align="center" width="15%" runat="server" ><asp:Literal ID="ltlCompanyNo" Text="公司代號"
                                runat="server"></asp:Literal>
                        </td>
						<td width="35%" runat="server" >&nbsp;
								<asp:textbox id="txtCompanyNo" runat="server" CssClass="TextBox" Width="90%" MaxLength="6"></asp:textbox>
                        </td>
						<td class="musttitle" align="center" width="15%" runat="server"><asp:Literal  ID="ltlCompanyName" Text="公司全稱"
                                runat="server"></asp:Literal>
                        </td>
						<td  width="35%" runat="server">&nbsp;
								<asp:textbox id="txtCompanyName" runat="server" CssClass="TextBox" Width="90%" MaxLength="100"></asp:textbox>
                        </td>
					</tr>				
                    <tr runat="server">
						<td class="musttitle" align="center" width="15%" runat="server" ><asp:Literal ID="ltlUnionID" Text="官方編號"
                                runat="server"></asp:Literal>
                        </td>
						<td width="35%" runat="server" >&nbsp;
								<asp:textbox id="txtUnionID" runat="server" CssClass="TextBox" Width="90%" MaxLength="20"></asp:textbox>
                        </td>
						<td class="smalltitle" align="center" width="15%" runat="server"><asp:Literal  ID="ltlPresident" Text="負責人員"
                                runat="server"></asp:Literal>
                        </td>
						<td  width="35%" runat="server">&nbsp;
								<asp:textbox id="txtPresident" runat="server" CssClass="TextBox" Width="90%" MaxLength="20"></asp:textbox>
                        </td>
					</tr>
					 <tr runat="server">
						<td class="smalltitle" align="center" width="15%" runat="server" ><asp:Literal ID="ltlPhone" Text="公司電話"
                                runat="server"></asp:Literal>
                        </td>
						<td width="35%" runat="server" >&nbsp;
								<asp:textbox id="txtPhone" runat="server" CssClass="TextBox" Width="90%" MaxLength="20"></asp:textbox>
                        </td>
						<td class="smalltitle" align="center" width="15%" runat="server"><asp:Literal  ID="ltlFax" Text="公司傳真"
                                runat="server"></asp:Literal>
                        </td>
						<td  width="35%" runat="server">&nbsp;
								<asp:textbox id="txtFax" runat="server" CssClass="TextBox" Width="90%" MaxLength="20"></asp:textbox>
                        </td>
					</tr>
                    <tr runat="server">
						<td class="smalltitle" align="center" runat="server" ><asp:Literal ID="ltlRegisterAddress" Text="登記地址"  runat="server"></asp:Literal>
                        </td>
						<td width="85%" colspan="3" runat="server" >&nbsp;
								<asp:textbox id="txtRegisterAddress" runat="server" CssClass="TextBox" Width="96%" MaxLength="100"></asp:textbox>
                        </td>
					</tr>
                     <tr runat="server">
						<td class="smalltitle" align="center" runat="server" ><asp:Literal ID="ltlWebUrl" Text="公司網址"  runat="server"></asp:Literal>
                        </td>
						<td width="85%" colspan="3" runat="server" >&nbsp;
								<asp:textbox id="txtWebUrl" runat="server" CssClass="TextBox" Width="96%" MaxLength="100"></asp:textbox>
                        </td>
					</tr>
					
                    <tr runat="server">
						<td class="smalltitle" align="center" runat="server" ><asp:Literal ID="ltlEnglishName" Text="英文名稱"  runat="server"></asp:Literal>
                        </td>
						<td width="85%" colspan="3" runat="server" >&nbsp;
								<asp:textbox id="txtEnglishName" runat="server" CssClass="TextBox" Width="96%" MaxLength="100"></asp:textbox>
                        </td>
					</tr>
                    <tr runat="server">
						<td class="smalltitle" align="center" runat="server" ><asp:Literal ID="ltlEnglishAddress" Text="英文地址"  runat="server"></asp:Literal>
                        </td>
						<td width="85%" colspan="3" runat="server" >&nbsp;
								<asp:textbox id="txtEnglishAddress" runat="server" CssClass="TextBox" Width="96%" MaxLength="20"></asp:textbox>
                        </td>
					</tr>
                    <tr runat="server">
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
						 
                            <asp:Literal ID="ltlLastModifyDate" Text="異動日期"  runat="server"></asp:Literal>
						</td>
						<td runat="server" >&nbsp;
                        <asp:textbox id="txtLastModifyDate" runat="server" CssClass="TextRead" Width="90%" 
                                MaxLength="20" ReadOnly="True"></asp:textbox></td>
					</tr>
                    
                   <tr style="display:none" runat="server">
						<td class="smalltitle" align="center" colspan="4" runat="server" >                  
                            <input id="InputFile" style="width: 221px" onclick="openfile()" type="file" runat="server" />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="Button4" runat="server" onclick="Button4_Click" 
                                Text="Upload1" />
                            <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                            <asp:FileUpload ID="FileUpload1" runat="server" 
                                onOnClientClickclick="openfile()" />
                            <asp:Button ID="Button3" runat="server" onclick="Button3_Click" Text="Upload" />
                            <asp:Button ID="Button2" runat="server" onclick="Button2_Click" Text="Import" />
                            <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Export" />
                        </td>
					</tr>
			</table>
    </div>
    </ContentTemplate>
    <Triggers>
            <asp:PostBackTrigger ControlID="Button3" />
            <asp:PostBackTrigger ControlID="Button4" />
    </Triggers>

            </asp:UpdatePanel> 
    </form>
</body>
</html>
