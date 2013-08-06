<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login_BU1.aspx.cs" Inherits="WebUI_Start_Login_BU1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>營運登錄</title>
 <link href="~/Styles/Main.css" type="text/css" rel="stylesheet" /> 
    <script type="text/javascript" src='<%=ResolveUrl("Js/jquery-1.8.3.min.js") %>'></script>
    <script type="text/javascript">
        $(document).ready(function () {
//            var a = window.screen.height;
//            var b = (a - 670) / 2;
//            $("#div1").css("marginTop", b);
                      
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
        background:url(../../Images/start/BU1/BU_15.jpg) no-repeat;     
        border:0px;
        width:99px;
        height:50px;
        cursor:pointer;
        font-size:16px;
        font-weight:700;
         text-align:left;
         padding-left:30px;
         font-family:微軟正黑體;
    }
      .forgot{    
        background:url(../../Images/start/BU1/BU_17.jpg) no-repeat;     
        border:0px;
        width:101px;
        height:50px;
        cursor:pointer;
        font-size:16px;
        font-weight:700;
         text-align:center;
         padding-left:15px;
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
    {
        height:100%;
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
     <div id="div1" style=" text-align:center;width:100%" runat="server">    
    <table border="0" align="center" cellpadding="0" cellspacing="0" style="width:100%;">
     <tr>    
       <td align="center" width="40%"  style="background:url('../../Images/start/BU1/BU_L.jpg') repeat-x;">
       </td>
       <td>
          <table border="0" align="center" cellpadding="0" cellspacing="0" >
      <tr>       
        <td colspan="5" background="../../Images/start/BU1/BU_01.jpg" height="187px" width="960px" align="right" valign="top" style="background-repeat:no-repeat;display:block;" >
     
       </td>         
     </tr>
      <tr>
      <td style="height:51px">
         <img src="../../Images/start/BU1/BU_02.jpg"  alt="" width="350px" height="51px" style="display:block;"/>
      </td>
      <td style="padding:0px;margin:0px" background="../../Images/start/BU1/BU_03.jpg" align="right">       
           <font style=" font-weight:bold;font-size:14px;font-family:微軟正黑體;">
          <asp:Literal ID="ltlUserName" Text="使用者代號"  runat="server" ></asp:Literal></font>
       </td>
       <td  colspan="2" background="../../Images/start/BU1/BU_04.jpg" style="WIDTH:176px;display:block;">
         <asp:TextBox ID="txtUserName" runat="server"  Width="85%"></asp:TextBox>  
       </td>
       <td>
           <img src="../../Images/start/BU1/BU_05.jpg"  alt="" width="335px" height="51px" style="display:block;"/>         
            
       </td>
       </tr>       
      <tr>
      <td style="height:49px">
         <img src="../../Images/start/BU1/BU_06.jpg"  alt="" width="350px" height="49px" style="display:block;"/>
      </td>
      <td style="padding:0px;margin:0px;display:block;height:49px" background="../../Images/start/BU1/BU_07.jpg" align="right">      
       
         <font style=" font-weight:bold;font-size:14px;font-family:微軟正黑體;">
          <asp:Literal ID="ltlUserPwd" Text="使用者密碼" runat="server"></asp:Literal></font>
       </td>
       <td  colspan="2" background="../../Images/start/BU1/BU_08.jpg" style="WIDTH:176px;height:49px;display:block;">
          <asp:TextBox ID="txtUserPwd" runat="server" TextMode="Password" Width="85%"></asp:TextBox>    
       </td>
       <td>
           <img src="../../Images/start/BU1/BU_09.jpg"  alt="" width="335px" height="49px" style="display:block;"/>         
            
       </td>     
          
     </tr>    
      <tr>
      <td style="height:69px">
         <img src="../../Images/start/BU1/BU_10.jpg"  alt="" width="350px" height="69px" style="display:block;"/>
      </td>
      <td style="padding:0px;margin:0px;display:block;height:69px;word-break:break-all" background="../../Images/start/BU1/BU_11.jpg" align="right">      
       
         <font style=" font-weight:bold;font-size:14px;font-family:微軟正黑體;" id="font1" runat="server">
          <asp:Literal ID="ltlVerifyCode" Text="驗證碼"  runat="server"></asp:Literal></font>
       </td>
       <td  colspan="2" background="../../Images/start/BU1/BU_12.jpg" style="WIDTH:176px;height:69px;display:block;">
        <asp:TextBox ID="txtVerifyCode" runat="server" Width="90px" ></asp:TextBox> 
           <img id="chk_img" onclick="checkwd_reload()" style="cursor: pointer;" align="bottom" src="CheckImage.aspx" alt="驗證碼" /> 
       </td>
       <td>
           <img src="../../Images/start/BU1/BU_13.jpg"  alt="" width="335px" height="69px" style="display:block;"/>         
            
       </td>
      
     </tr>  
        <tr>
      <td style="height:50px">
         <img src="../../Images/start/BU1/BU_14.jpg"  alt="" width="350px" height="50px" style="display:block;"/>
      </td>
      <td >      
        <asp:Button ID="btnLogin" runat="server" Text="登入" onclick="Login_Click" OnClientClick="return CheckCode();"  CssClass="login" />
       </td>
       <td style="height:50px">
         <img src="../../Images/start/BU1/BU_16.jpg"  alt="" width="75px" height="50px" style="display:block;"/>
      </td>
       <td>
           <asp:Button ID="btnForgotPwd" runat="server" Text="忘記密碼"  CssClass="forgot" />             
       </td>
       <td style="height:50px">
         <img src="../../Images/start/BU1/BU_18.jpg"  alt="" width="335px" height="50px" style="display:block;"/>
      </td>
     </tr>  
      <tr>       
        <td colspan="5" background="../../Images/start/BU1/BU_19.jpg" height="127px" width="960px" align="right" valign="top" style="background-repeat:no-repeat;display:block;">
     
       </td>         
     </tr>
     </table>
        </td>  
       <td align="center" width="40%"  style="background:url('../../Images/start/BU1/BU_R.jpg') repeat-x;">
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