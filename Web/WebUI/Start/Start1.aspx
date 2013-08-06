<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Start1.aspx.cs" Inherits="WebUI_Start_Start1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
<script type="text/javascript" src='<%=ResolveUrl("Js/jquery-1.8.3.min.js") %>'></script>
    <title></title>   
     <script type="text/javascript">
         $(document).ready(function () {
             var a = window.screen.height;
             var b = (a - 700) / 2;
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
       <td align="center" width="40%"  style="background:url('../../Images/start/Main1/web_L.jpg') repeat-x;">
       </td>
       <td>
      <table border="0" align="center" cellpadding="0" cellspacing="0" >
      <tr>       
        <td colspan="5" background="../../Images/start/Main1/web1_01.jpg" height="375px" width="866px" align="right" valign="top" style="background-repeat:no-repeat;">
     
         <p style="margin-top:70px">language</p>&nbsp;
       </td>
        <td background="../../Images/start/Main1/web1_02.jpg" height="375px" width="94px" valign="top"  style="background-repeat:no-repeat;" >
            
            <asp:DropDownList ID="ddlLanguage" runat="server"  style="margin-top:70px" 
                onselectedindexchanged="ddlLanguage_SelectedIndexChanged" AutoPostBack="true">
            </asp:DropDownList>
       </td>    
     </tr>
      <tr>
      <td style="height:33px">
         <img src="../../Images/start/Main1/web1_03.jpg"  alt="" height="33px" 
              style="display:block; width: 308px;"/>
      </td>
      <td style="padding:0px;margin:0px" valign="top">       
           <asp:Button ID="btnEP" runat="server" Text="企業用戶" style=" background-image:url('../../Images/start/Main1/web1_04.jpg');border:0px;width:113px;height:33px;COLOR:#fff; font-size:16px; font-weight:900;text-align:center;
                cursor:pointer;font-family:微軟正黑體;"  
          OnClientClick="return changeHref(2);"  />                  
         
       </td>
       <td >
          <img src="../../Images/start/Main1/web1_05.jpg"  alt="" height="33px" width="149px" style="display:block;"/>
       </td>
       <td>
           <asp:Button ID="btnBU" runat="server" Text="營運用戶" 
           style=" background-image:url('../../Images/start/Main1/web1_06.jpg');border:0px;width:114px;height:33px;COLOR:#fff; font-size:16px; font-weight:900; text-align:center;cursor:pointer; font-family:微軟正黑體"  
              OnClientClick="return changeHref(1);"  />            
            
       </td>
       <td>
           <img src="../../Images/start/Main1/web1_07.jpg"  alt=""  height="33px" width="182px" style="display:block;"/>
       </td>
      <%-- <td>
      
         <asp:Button ID="btnCT" runat="server" Text="消費會員" style=" background-image:url('../../Images/start/Main11/WebAll_08.jpg');border:0px;width:172px;height:71px;COLOR:Blue; font-size:20px; font-weight:900;cursor:hand;font-family:微軟正黑體" 
          />
          
       </td>--%>
       <td style="padding:0px;margin:0px">
           <img src="../../Images/start/Main1/web1_08.jpg"  alt=""  height="33px" width="94px" style="display:block;"/>
       </td>       
     </tr>
      <tr>
      <td colspan="6" style="padding:0px;margin:0px">
         <img src="../../Images/start/Main1/web1_09.jpg"  alt="" style="display:block;"/>
      </td>
      </tr>
      <tr>
      
       <td colspan="6" background="../../Images/start/Main1/web1_10.jpg" height="40px" width="960px" align="right" valign="middle" style="background-repeat:no-repeat;margin-top:3px" >
         <font style=" font-weight:bold;font-size:12px;font-family:微軟正黑體;color:#fff;">
          <asp:Literal ID="ltlAdddress" Text="地址: 106 台北市信義路三段 170 之 1 號 10 樓"  runat="server" ></asp:Literal></font><br />
          <font style=" font-weight:bold;font-size:12px;font-family:微軟正黑體;color:#fff">
          <asp:Literal ID="ltlWeb" Text="本網站為信實數碼股份有限公司版權所有"  runat="server" ></asp:Literal></font>
      </td>
      </tr>
      <tr>
        <td colspan="6" style="padding:0px;margin:0px">
         <img src="../../Images/start/Main1/web1_11.jpg"  alt="" style="display:block;"/>
      </td>
     </tr>    
     </table>
      </td>  
       <td align="center" width="40%"  style="background:url('../../Images/start/Main1/web_R.jpg') repeat-x;">
       </td>   
     </tr>    
    </table>
    
    </div>   
    </form>
</body>
</html>

