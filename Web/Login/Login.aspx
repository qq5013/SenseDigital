<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" EnableTheming="true" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>
<%@ Register src="../Controls/CopyRight.ascx" tagname="CopyRight" tagprefix="uc1" %>

<html xmlns="http://www.w3.org/1999/xhtml"  >
<head id="Head1" runat="server">
    <title>信實防偽系統-用戶登錄</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link href="~/Styles/Main.css" type="text/css" rel="stylesheet" /> 
    <script language="javascript" type="text/javascript">
        function SelectDataBase() {
            if (document.getElementById("txtUsername").value == "") {
                alert("請輸入用戶名!");
                document.getElementById("txtUsername").focus();
                return false;
            }
            if (document.getElementById("ddlDataBase").value == "") {
                alert("請選擇公司!");
                document.getElementById("ddlDataBase").focus();
                return false;
            }
        }
        
        function checkwd_reload() {
		    var ob = document.getElementById("chk_img");
		    ob.src = "../WebUI/Start/CheckImage.aspx?" + new Date();
		}

	</script> 
</head>
<body >
   <form id="form1" runat="server" defaultfocus="txtUsername" defaultbutton="btnLogin">
   
      <table width="100%" border="0"  cellpadding="0" cellspacing="0" id="TABLE1" 
          style="background:url(../Images/Login/center_bg.gif); height: 100%;">
        <tr>          
		  <td align="center" style="height: 447px" >
		  
		  <table width="620" border="0" cellpadding="0" cellspacing="0">
            <tbody>
              <tr>
                <td style="width:620px;height: 11px"><img  alt="" height="11" src="../Images/Login/login_p_img02.gif" width="650" /></td>
              </tr>
              <tr>
                <td align="center" style="background:url(../Images/Login/login_p_img03.gif)"><br />
                    <table width="570" border="0" cellspacing="0" cellpadding="0">
                      <tr>
                        <td><table cellspacing="0" cellpadding="0" width="570" border="0">
                            <tbody>
                              <tr>
                                <td width="400" height="80" align="center" valign="top"><img  alt="" height="67" src="../Images/Login/member_t04.png" width="400" /></td>
                               
                              </tr>
                            </tbody>
                        </table>
                      </td>
                      </tr>
                      <tr>
                        <td>&nbsp;</td>
                      </tr>
                      <tr>
                        <td><img src="../Images/Login/a_te01.gif" width="570" height="3" alt="" /></td>
                      </tr>
                      <tr>
                        <td align="center" style="background:url(../Images/Login/a_te02.gif)">
                        <table width="520" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                              <td width="123" height="120"><img alt="" height="95" src="../Images/Login/login_p_img05.gif" width="123" border="0" /></td>
                              <td align="center">
                              <table cellspacing="0" cellpadding="0" border="0">
                                  <tbody>
                                    <tr>
                                      <td height="25" align="center" style="width: 76px"> <asp:Literal 
                                              ID="ltlUserName" runat="server" Text="用戶名：" 
                                              meta:resourcekey="ltlUserNameResource1"></asp:Literal>
                                        </td>
                                      <td align="center" height="25"  style="width: 128px"><input class="TextBox" tabindex="1" maxlength="22" name="user" id="txtUsername"
																		runat="server" style="width: 90%;height:18px;"/></td>
                                      <td width="80" rowspan="2" align="right" valign="middle">
                                          <asp:ImageButton id="btnLogin" runat="server" 
                                              ImageUrl="../Images/Login/login_p_img11.gif" OnClick="btnLogin_Click" 
                                              meta:resourcekey="btnLoginResource1"></asp:ImageButton></td>
                                    </tr>
                                    <tr>
                                      <td valign="middle" height="25"  align="center" style="width: 76px"> <asp:Literal 
                                              ID="ltlPassword" runat="server" Text="密&nbsp;&nbsp;&nbsp;&nbsp;碼：" 
                                              meta:resourcekey="ltlPasswordResource1"></asp:Literal>
                                      </td>
                                      <td align="center" height="25" valign="middle" style="width: 128px"><input class="TextBox" name="user2" type="password" tabindex="2" maxlength="22"
																		id="txtPass" runat="server" style="width: 90%;height:18px;"  /></td>
                                    </tr>
                                    <tr>
                                      <td align="center" height="25" valign="middle" style="width: 76px"> 
                                          <asp:Literal 
                                              ID="ltlLanguage" runat="server" Text="語&nbsp;&nbsp;&nbsp;&nbsp;言：" 
                                              meta:resourcekey="ltlLanguageResource1"></asp:Literal>
                                        </td>
                                      <td align="center" height="25" valign="middle" style="width: 128px">
                                          <asp:DropDownList ID="ddlLanguage" runat="server" Width="90%" 
                                              meta:resourcekey="ddlLanguageResource1"> </asp:DropDownList></td>
                                      <td align="right" rowspan="1" valign="middle" >
                                          <asp:HiddenField ID="HiddenField1" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center"  style="height: 19px; width: 76px;">
                                            <asp:Literal 
                                              ID="ltlCheckCode" runat="server" Text="驗證碼：" 
                                                meta:resourcekey="ltlCheckCodeResource1"></asp:Literal>
                                        </td><td>
                                &nbsp;
                                <asp:TextBox ID="txtCheckCode" runat="server" Width="90%" 
                                                meta:resourcekey="txtCheckCodeResource1"></asp:TextBox></td>
                                 <td>
                                <img id="chk_img" onclick="checkwd_reload()" style="cursor: pointer;" align="bottom" src="../WebUI/Start/CheckImage.aspx" alt="校验码" />
                               
                                <asp:RequiredFieldValidator 
                                                ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtCheckCode"
                                        ErrorMessage="驗證碼必須輸入" ForeColor="Red" 
                                         meta:resourcekey="RequiredFieldValidator3Resource1"></asp:RequiredFieldValidator></td>
        </tr>

                                  </tbody>
                                </table>
                                  <asp:Label id="lblMsg" runat="server" BackColor="Transparent" 
                                      meta:resourcekey="lblMsgResource1"></asp:Label>
                              </td>
                            </tr>
                        </table>
                        </td>
                      </tr>
                      <tr>
                        <td bgcolor="#d5d5d5"></td>
                      </tr>
                      <tr>
                        <td><table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                              <td height="70" align="center"> 
                                  <uc1:CopyRight ID="CopyRight1" runat="server" />
                                </td>
                            </tr>
                        </table></td>
                      </tr>
                  </table>
                </td>
              </tr>
              <tr>
                <td><img height="11" src="../Images/Login/login_p_img04.gif" width="650" alt="" /></td>
              </tr>
            </tbody>
          </table>
          
          </td>
        </tr>
      </table>
    </form>
</body>
</html>