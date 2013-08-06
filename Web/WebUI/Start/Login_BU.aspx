<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login_BU.aspx.cs" Inherits="WebUI_Start_Login_BU" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>營運登錄</title>
    <link href="~/Styles/Main.css" type="text/css" rel="stylesheet" /> 
    
    <script type="text/javascript">
        function checkwd_reload() {
            var ob = document.getElementById("chk_img");
            ob.src = "CheckImage.aspx?" + new Date();
        }
        function Initial() {
            document.getElementById("txtVerifyCode").value = '<%= Session["CheckCode"]%>';
        }
        function CheckCode() {
            if ('<%= Session["CheckCode"]%>' == null) {
                location.href = "SessionTimeOut.aspx";
                return false;
            }
            return true;
        }

        </script>
    <style type="text/css">
     .login{    
        background:url(../../Images/start/BU/BU_13.jpg) no-repeat;     
        border:0px;
        width:134px;
        height:58px;
        cursor:pointer;
        font-size:large;
        font-weight:700;
         text-align:left;
         padding-left:30px;
    }
      .forgot{    
        background:url(../../Images/start/BU/BU_15.jpg) no-repeat;     
        border:0px;
        width:128px;
        height:58px;
        cursor:pointer;
        font-size:large;
        font-weight:700;
         text-align:right;
         padding-left:30px;
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
         <img src="../../Images/start/BU/BU_01.jpg"  alt="" height="258px" width="1024px" style="display:block;"/>
       </td>       
     </tr>
     <tr>
      <td  colspan="2" background="../../Images/start/BU/BU_02.jpg" align="right">
         <font style=" font-weight:bold">
          <asp:Literal ID="ltlUserName" Text="使用者代號" 
                                runat="server" ></asp:Literal></font>&nbsp;&nbsp;
      </td>
        <td  colspan="2"  background="../../Images/start/BU/BU_03.jpg">
           <asp:TextBox ID="txtUserName" runat="server" ></asp:TextBox>  
      </td>
       <td >
         <img src="../../Images/start/BU/BU_04.jpg"  alt="" style="display:block;"/>
      </td>     
     </tr>
      <tr>
      <td  colspan="2" background="../../Images/start/BU/BU_05.jpg" align="right">
         <font style=" font-weight:bold">
          <asp:Literal ID="ltlUserPwd" Text="使用者密碼" 
                                runat="server"></asp:Literal></font>&nbsp;&nbsp;
      </td>
        <td  colspan="2" background="../../Images/start/BU/BU_06.jpg">
           <asp:TextBox ID="txtUserPwd" runat="server" TextMode="Password"></asp:TextBox>  
      </td>
       <td >
         <img src="../../Images/start/BU/BU_07.jpg"  alt="" style="display:block;"/>
      </td>     
     </tr>
      <tr>
      <td  colspan="2" background="../../Images/start/BU/BU_08.jpg" align="right">
         <font style=" font-weight:bold">
          <asp:Literal ID="ltlVerifyCode" Text="驗證碼" 
                                runat="server" ></asp:Literal></font>&nbsp;&nbsp;
      </td>
        <td  colspan="2"  background="../../Images/start/BU/BU_09.jpg">
           <asp:TextBox ID="txtVerifyCode" runat="server" Width="80px" ></asp:TextBox> 
           <img id="chk_img" onclick="checkwd_reload()" style="cursor: pointer;" align="bottom" src="CheckImage.aspx" alt="驗證碼" /> 
      </td>
       <td >
         <img src="../../Images/start/BU/BU_10.jpg"  alt="" style="display:block;"/>
      </td>     
     </tr>
     <tr>
      <td colspan="5">
         <img src="../../Images/start/BU/BU_11.jpg"  alt="" style="display:block;"/>
      </td>
     </tr>
      <tr>
      <td>
         <img src="../../Images/start/BU/BU_12.jpg"  alt="" style="display:block;"/>         
      </td>
      <td>      
         <asp:Button ID="btnLogin" runat="server" Text="登入" onclick="Login_Click" OnClientClick="return CheckCode();"  CssClass="login" />
      </td>
       <td>
         <img src="../../Images/start/BU/BU_14.jpg"  alt="" style="display:block;"/>         
      </td>
      <td>
        <asp:Button ID="btnForgotPwd" runat="server" Text="忘記密碼"  CssClass="forgot" />
      </td>
       <td>
         <img src="../../Images/start/BU/BU_16.jpg"  alt="" style="display:block;"/>         
      </td>
     </tr>
     <tr>
       <td colspan="5">
          <img src="../../Images/start/BU/BU_17.jpg"  alt="" width="1024px" height="180px" style="display:block;"/>    
       </td>
     </tr>
     </table>
      </div>
      </ContentTemplate>
            </asp:UpdatePanel> 
    </form>

</body>
</html>