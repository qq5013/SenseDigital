<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Start.aspx.cs" Inherits="WebUI_Start_Start" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%--<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 strict//EN" >--%>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
<script type="text/javascript" src='<%=ResolveUrl("Js/jquery-1.8.3.min.js") %>'></script>
    <title></title>   
     <script type="text/javascript">
         $(document).ready(function () {
             var a = window.screen.height;
             var b = (a - 630) / 2;
             $("#div1").css("marginTop", b);
         });
         //         window.onload = Init;
         //         function Init() {            
         //             var a = window.screen.height;
         //             var b = (a - 630) / 2;           
         //             document.getElementById("div1").style.marginTop = "" + b + "";
         //         }

        function changeHref(index) {
            var language = document.getElementById("ddlLanguage").value

            var a;
            if (index == 1) {
                //a = document.getElementById('BU');
                location.href = 'Login_BU1.aspx?currentculture=' + language;
                return false;
            }
            else if (index == 2) {
               // a = document.getElementById('Ep');
                location.href = 'Login_Ep1.aspx?currentculture=' + language;
                return false;
            }
            else {
               // a = document.getElementById('Ct');
                location.href = 'Login_CT.aspx?currentculture=' + language;
                return false;
            }
        }
    </script>
    <style type="text/css">
    html 
    {
         overflow-y: hidden;
    }
    </style>

</head>
<body style="margin:0px;padding:0px; text-align:center">
    <form id="form1" runat="server" >
    
    <div id="div1" style=" text-align:center">    
    <table border="0" align="center" cellpadding="0" cellspacing="0" style="width:100%;">
     <tr>    
       <td align="center" width="40%"  style="background:url('../../Images/start/Main/web-table.jpg') repeat-x;">
       </td>
       <td>
      <table border="0" align="center" cellpadding="0" cellspacing="0" >
      <tr>       
        <td colspan="5" background="../../Images/start/Main/web_01.jpg" height="340px" width="865px" align="right" valign="top" style="background-repeat:no-repeat;">
     
         <p style="margin-top:18px">language</p>&nbsp;
       </td>
        <td background="../../Images/start/Main/web_02.jpg" height="340px" width="95px" valign="top"  style="background-repeat:no-repeat;" >
            
            <asp:DropDownList ID="ddlLanguage" runat="server"  style="margin-top:18px">
            </asp:DropDownList>
       </td>    
     </tr>
      <tr>
      <td style="height:31px">
         <img src="../../Images/start/Main/web_03.jpg"  alt="" width="310px" height="31px" style="display:block;"/>
      </td>
      <td style="padding:0px;margin:0px">       
           <asp:Button ID="btnEP" runat="server" Text="企業用戶" style=" background-image:url('../../Images/start/Main/web_06.jpg');border:0px;width:113px;height:31px;COLOR:#0c2c76; font-size:16px; font-weight:900;text-align:center;
                padding-left:8px;cursor:pointer;font-family:微軟正黑體;"  
          OnClientClick="return changeHref(2);"  />                  
         
       </td>
       <td >
          <img src="../../Images/start/Main/web_05.jpg"  alt="" height="31px" width="123px" style="display:block;"/>
       </td>
       <td>
           <asp:Button ID="btnBU" runat="server" Text="營運用戶" 
           style=" background-image:url('../../Images/start/Main/web_04.jpg');border:0px;width:112px;height:31px;COLOR:#0c2c76; font-size:16px; font-weight:900; text-align:center;padding-left:8px;padding-top:-5px; cursor:pointer; font-family:微軟正黑體"  
              OnClientClick="return changeHref(1);"  />            
            
       </td>
       <td>
           <img src="../../Images/start/Main/web_07.jpg"  alt=""  height="31px" width="207px" style="display:block;"/>
       </td>
      <%-- <td>
      
         <asp:Button ID="btnCT" runat="server" Text="消費會員" style=" background-image:url('../../Images/start/Main1/WebAll_08.jpg');border:0px;width:172px;height:71px;COLOR:Blue; font-size:20px; font-weight:900;cursor:hand;font-family:微軟正黑體" 
          />
          
       </td>--%>
       <td style="padding:0px;margin:0px">
           <img src="../../Images/start/Main/web_08.jpg"  alt=""  height="31px" width="95px" style="display:block;"/>
       </td>       
     </tr>
      <tr>
      <td colspan="6" style="padding:0px;margin:0px">
         <img src="../../Images/start/Main/web_09.jpg"  alt="" style="display:block;"/>
      </td>
     </tr>    
     </table>
      </td>  
       <td align="center" width="40%"  style="background:url('../../Images/start/Main/web-R.jpg') repeat-x;">
       </td>   
     </tr>    
    </table>
    
    </div>   
    </form>
</body>
</html>
