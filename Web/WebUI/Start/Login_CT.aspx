<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login_CT.aspx.cs" Inherits="WebUI_Start_Login_CT" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>消費者登錄</title>
     <style type="text/css">
     .login{    
        background:url(../../Images/start/Ct/Ct_17.jpg) no-repeat;     
        border:0px;
        width:119px;
        height:54px;
        cursor:pointer;
        font-size:large;
        font-weight:700;
         text-align:left;
         padding-left:30px;
    }
      .forgot{    
        background:url(../../Images/start/Ct/Ct_19.jpg) no-repeat;     
        border:0px;
        width:123px;
        height:54px;
        cursor:pointer;
        font-size:large;
        font-weight:700;
         text-align:right;
         padding-left:30px;
    }
      .logon{    
        background:url(../../Images/start/Ct/Ct_12.jpg) no-repeat;     
        border:0px;
        width:88px;
        height:32px;
        cursor:pointer;
        font-size:large;
        font-weight:700;
        text-align:center;         
    }
     #progressBackgroundFilter {
        top:0px;
        bottom:0px;
        left:0px;
        right:0px;
        overflow:hidden;
        padding:0;
        margin:0;
        background-color:#000;
        filter:alpha(opacity=50);
        opacity:0.5;
        z-index:1000;     
        position:absolute;      
        height: expression(document.body.clientHeight + "px");     
        width: expression(document.body.clientWidth + "px");
    } #processMessage {
        padding:10px;
        width:12%;
        z-index:1001;
        background-color:#fff;
        border:solid 1px #000;     
        position:absolute;left:43%;top:43%;     
    } 
  </style>
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
    <table border="0" align="center" cellpadding="0" cellspacing="0">
      <tr>
       <td colspan="5" >
         <img src="../../Images/start/Ct/Ct_01.jpg"  alt="" height="250px" width="1024px" style="display:block;"/>
       </td>       
     </tr>
     <tr>
      <td> 
        <img src="../../Images/start/Ct/Ct_02.jpg"  alt="" style="display:block;"/>
      </td>
      <td   background="../../Images/start/Ct/Ct_03.jpg" align="right">
         <font style=" font-weight:bold"> <asp:Literal ID="ltlUserName" Text="會員代號" 
                                runat="server" ></asp:Literal></font>&nbsp;&nbsp;
      </td>
        <td  colspan="2"  background="../../Images/start/Ct/Ct_04.jpg">
           <asp:TextBox ID="txtUserName" runat="server"></asp:TextBox>  
      </td>
       <td >
         <img src="../../Images/start/Ct/Ct_05.jpg"  alt="" style="display:block;"/>
      </td>     
     </tr>
      <tr>
       <td> 
        <img src="../../Images/start/Ct/Ct_06.jpg"  alt="" style="display:block;"/>
      </td>
      <td   background="../../Images/start/Ct/Ct_07.jpg" align="right">
         <font style=" font-weight:bold"> <asp:Literal ID="ltlUserPwd" Text="會員密碼" 
                                runat="server" ></asp:Literal></font>&nbsp;&nbsp;
      </td>
        <td  colspan="2"  background="../../Images/start/Ct/Ct_08.jpg">
           <asp:TextBox ID="txtUserPwd" runat="server" TextMode="Password"></asp:TextBox>  
      </td>
       <td >
         <img src="../../Images/start/Ct/Ct_09.jpg"  alt="" style="display:block;"/>
      </td>     
     </tr>
     <tr>
          <td colspan="5">
             <img src="../../Images/start/Ct/Ct_10.jpg"  alt="" style="display:block;"/>
          </td>
      </tr>    
      <tr>
        <td colspan="2"> 
          <img src="../../Images/start/Ct/Ct_11.jpg"  alt="" style="display:block;"/>
      </td>
        <td > 
         <asp:Button ID="btnRegister" runat="server" Text="註冊" CssClass="logon"/>
      </td>
      <td > 
          <img src="../../Images/start/Ct/Ct_13.jpg"  alt="" style="display:block;"/>
      </td>
      <td > 
          <img src="../../Images/start/Ct/Ct_14.jpg"  alt="" style="display:block;"/>
      </td>
      </tr>
      <tr>
      <td colspan="5">
         <img src="../../Images/start/Ct/Ct_15.jpg"  alt="" style="display:block;"/>         
      </td>
      </tr>
      <tr>
        <td >
         <img src="../../Images/start/Ct/Ct_16.jpg"  alt="" style="display:block;"/>         
      </td>
      <td>      
         <asp:Button ID="btnLogin" runat="server" Text="登入" onclick="btnLogin_Click"  CssClass="login"/>
      </td>
       <td>
         <img src="../../Images/start/Ct/Ct_18.jpg"  alt="" style="display:block;"/>         
      </td>
      <td>
        <asp:Button ID="btnForgotPwd" runat="server" Text="忘記密碼"  CssClass="forgot"/>
      </td>
       <td>
         <img src="../../Images/start/Ct/Ct_20.jpg"  alt="" style="display:block;"/>         
      </td>
     </tr>
     <tr>
       <td colspan="5">
          <img src="../../Images/start/Ct/Ct_21.jpg"  alt="" width="1024px" height="190px" style="display:block;"/>    
       </td>
     </tr>
     </table>
    </div>
     </ContentTemplate>
            </asp:UpdatePanel> 
    </form>
</body>
</html>