<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login_EP1.aspx.cs" Inherits="WebUI_Start_Login_EP1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>企業登錄</title>
    <link href="~/Styles/Main.css" type="text/css" rel="stylesheet" /> 
    <script type="text/javascript" src='<%=ResolveUrl("Js/jquery-1.8.3.min.js") %>'></script>
 <script type="text/javascript">
     $(document).ready(function () {
//         var a = window.screen.height;
//         var b = (a - 670) / 2;
//         $("#div1").css("marginTop", b);
     });
     //         window.onload = Init;
     //         function Init() {            
     //             var a = window.screen.height;
     //             var b = (a - 630) / 2;           
     //             document.getElementById("div1").style.marginTop = "" + b + "";
     //         }
     function checkwd_reload() {
         var ob = document.getElementById("chk_img");
         ob.src = "CheckImage.aspx?" + new Date();
     }
     function Login() {
         if (document.getElementById("txtEnterpriseID").value == "") {
             alert("<%=Resources.Resource.Login_Enterprise_NotNull %>");
             document.getElementById("txtEnterpriseID").focus();
             return false;
         }
         if (document.getElementById("txtVerifyCode").value == "") {
             alert("<%=Resources.Resource.Login_VerifyCode_NotNull %>");
             document.getElementById("txtVerifyCode").focus();
             return false;
         }
         return true;
     }
        </script>
     <style type="text/css">
     .login{    
        background:url(../../Images/start/EP1/EP_19.jpg) no-repeat;     
        border:0px;
        width:109px;
        height:51px;
        cursor:pointer;
        font-size:16px;
        font-weight:700;
         text-align:left;
         padding-left:30px;
           font-family:微軟正黑體;
    }
      .forgot{    
        background:url(../../Images/start/EP1/EP_21.jpg) no-repeat;     
        border:0px;
        width:110px;
        height:51px;
        cursor:pointer;
        font-size:16px;
        font-weight:700;
         text-align:center;
         padding-left:20px;
           font-family:微軟正黑體;
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
      html 
    { height:100%;
         overflow-y: hidden;
    }
      body { height:100%; 
         }
    #div1
    { display:inline-block; zoom:1; vertical-align:middle;
      position:relative;
        }
      *{margin:0; padding:0;}     
     #outer{width:100%; height:100%;position:relative;}
     #outer[id]{display:table;}    
     #middle[id]{display:table-cell; position:static;vertical-align:middle;top:50%;position:absolute;}
     #inner{position:relative; top:-50%;}
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
                  <div id="outer">   
                   <div id="middle">    
                           <div id="inner">   
    <div id="div1" style=" text-align:center">    
    <table border="0" align="center" cellpadding="0" cellspacing="0" style="width:100%;">
     <tr>    
       <td align="center" width="40%"  style="background:url('../../Images/start/EP1/EP_L.jpg') repeat-x;">
       </td>
       <td>
          <table border="0" align="center" cellpadding="0" cellspacing="0" >
      <tr>       
        <td colspan="5" background="../../Images/start/EP1/EP_01.jpg" height="182px" width="960px" align="right" valign="top" style="background-repeat:no-repeat;display:block;" >
     
       </td>         
     </tr>
      <tr>
      <td style="height:42px">
         <img src="../../Images/start/EP1/EP_02.jpg"  alt="" width="350px" height="42px" style="display:block;"/>
      </td>
      <td style="padding:0px;margin:0px" background="../../Images/start/EP1/EP_03.jpg" align="right">       
           <font style=" font-weight:bold;font-size:14px;  font-family:微軟正黑體;">
        <asp:Literal ID="ltlEpNo" Text="企業代號" runat="server" ></asp:Literal></font>
       </td>
       <td  colspan="2" background="../../Images/start/EP1/EP_04.jpg" style="WIDTH:166px;display:block;">
          <asp:TextBox ID="txtEnterpriseID" runat="server" Width="83%"></asp:TextBox>  
       </td>
       <td>
           <img src="../../Images/start/EP1/EP_05.jpg"  alt="" width="335px" height="42px" style="display:block;"/>         
            
       </td>
       </tr>       
      <tr>
      <td style="height:40px">
         <img src="../../Images/start/EP1/EP_06.jpg"  alt="" width="350px" height="40px" style="display:block;"/>
      </td>
      <td style="padding:0px;margin:0px;display:block;height:40px" background="../../Images/start/EP1/EP_07.jpg" align="right">      
       
         <font style=" font-weight:bold;font-size:14px;  font-family:微軟正黑體;">
         <asp:Literal ID="ltlUserName" Text="使用者代號"  runat="server" ></asp:Literal></font>
       </td>
       <td  colspan="2" background="../../Images/start/EP1/EP_08.jpg" style="WIDTH:176px;height:40px;display:block;">
           <asp:TextBox ID="txtUserName" runat="server" Width="83%"></asp:TextBox>  
       </td>
       <td>
           <img src="../../Images/start/EP1/EP_09.jpg"  alt="" width="335px" height="40px" style="display:block;"/>         
            
       </td>     
          
     </tr>    
      <tr>
      <td style="height:45px">
         <img src="../../Images/start/EP1/EP_10.jpg"  alt="" width="350px" height="45px" style="display:block;"/>
      </td>
      <td style="padding:0px;margin:0px;display:block;height:45px" background="../../Images/start/EP1/EP_11.jpg" align="right">      
       
         <font style=" font-weight:bold;font-size:14px;  font-family:微軟正黑體;">
       <asp:Literal ID="ltlUserPwd" Text="使用者密碼" 
                                runat="server" ></asp:Literal></font>
       </td>
       <td  colspan="2" background="../../Images/start/EP1/EP_12.jpg" style="WIDTH:166px;height:45px;display:block;">
       <asp:TextBox ID="txtUserPwd" runat="server" TextMode="Password" Width="83%"></asp:TextBox>  
           
       </td>
       <td>
           <img src="../../Images/start/EP1/EP_13.jpg"  alt="" width="335px" height="45px" style="display:block;"/>         
            
       </td>
      
     </tr>  
      <tr>
      <td style="height:48px">
         <img src="../../Images/start/EP1/EP_14.jpg"  alt="" width="350px" height="48px" style="display:block;"/>
      </td>
      <td style="padding:0px;margin:0px;display:block;height:48px" background="../../Images/start/EP1/EP_15.jpg" align="right">      
       
         <font style=" font-weight:bold;font-size:14px;  font-family:微軟正黑體;">
     <asp:Literal ID="ltlVerifyCode" Text="驗證碼" 
                                runat="server" ></asp:Literal></font>
       </td>
       <td  colspan="2" background="../../Images/start/EP1/EP_16.jpg" style="WIDTH:166px;height:48px;display:block;">
      <asp:TextBox ID="txtVerifyCode" runat="server" Width="85px"></asp:TextBox>  
           <img id="chk_img" onclick="checkwd_reload()" style="cursor: pointer;" align="bottom" src="CheckImage.aspx" alt="驗證碼" />
           
       </td>
       <td>
           <img src="../../Images/start/EP1/EP_17.jpg"  alt="" width="335px" height="48px" style="display:block;"/>         
            
       </td>
      
     </tr>  
        <tr>
      <td style="height:51px">
         <img src="../../Images/start/EP1/EP_18.jpg"  alt="" width="350px" height="51px" style="display:block;"/>
      </td>
      <td >      
         <asp:Button ID="btnLogin" runat="server" Text="登入" onclick="btnLogin_Click"  OnClientClick="return Login()"
              CssClass="login"/>
       </td>
       <td style="height:51px">
         <img src="../../Images/start/EP1/EP_20.jpg"  alt="" width="56px" height="51px" style="display:block;"/>
      </td>
       <td>
            <asp:Button ID="btnForgotPwd" runat="server" Text="忘記密碼"  CssClass="forgot"/>           
       </td>
       <td style="height:51px">
         <img src="../../Images/start/EP1/EP_22.jpg"  alt="" width="335px" height="51px" style="display:block;"/>
      </td>
     </tr>  
      <tr>       
        <td colspan="5" background="../../Images/start/EP1/EP_23.jpg" height="125px" width="960px" align="right" valign="top" style="background-repeat:no-repeat;display:block;">
     
       </td>         
     </tr>
     </table>
        </td>  
       <td align="center" width="40%"  style="background:url('../../Images/start/EP1/EP_R.jpg') repeat-x;">
       </td>   
     </tr>    
    </table>
     </div>
                         </div>
                     </div>
                     </div>
     </ContentTemplate>
            </asp:UpdatePanel> 
    </form>
</body>
</html>