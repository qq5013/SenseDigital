<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AutoCode.aspx.cs" Inherits="WebUI_System_AutoCode" %>


<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />  
    <title>編碼原則</title>
   <link href="~/Styles/Main.css" type="text/css" rel="stylesheet" /> 
        <link href="~/Styles/op.css" type="text/css" rel="stylesheet" /> 
        <link href="~/Styles/Detail.css" type="text/css" rel="stylesheet" /> 
        <link href="~/Styles/superTables.css" type="text/css" rel="stylesheet" /> 
        <script type="text/javascript" src='<%=ResolveUrl("~/Scripts/JQuery/jquery-1.8.2.min.js") %>'></script> 	  

        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Commons.js") %>'></script>
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Ajax.js") %>'></script>
         <script type="text/javascript" language="javascript">
             var klen = 0;
             $(document).ready(function () {
                 $('#Button1').bind('click', function () { window.close(); });
                 $('#btnUp').bind('click', function () { changeLength(1); });
                 $('#btnDown').bind('click', function () { changeLength(-1); });
                 $('#chkUseK').bind('change', function () {
                     if ($("#chkUseK").attr("checked"))
                         setCode($("#txtK").val() + $("#lblCode").val());
                     else
                         setCode($("#lblCode").val().substring(klen));
                     changeLength(0);
                 });
                 $('#txtK').bind('change', function () {
                     if ($("#chkUseK").attr("checked")) {
                         setCode($("#txtK").val() + $("#lblCode").val().substring(klen)); klen = $('#txtK').val().length; changeLength(0);
                     }
                 });
                 $('#chkUseYear').bind('change', function () {
                     if ($("#lblCode").val().substring(klen).indexOf("YYYY") != -1) {
                         setCode(GetCode().substring(0, $("#lblCode").val().length - 4));
                     }
                     else {
                         setCode(GetCode().substring(0, $("#lblCode").val().length + 4));
                     }
                     changeLength(0);
                 });
                 $('#chkUseMonth').bind('change', function () {
                     if ($("#lblCode").val().substring(klen).indexOf("MM") != -1) {
                         setCode(GetCode().substring(0, $("#lblCode").val().length - 2));
                     }
                     else {
                         setCode(GetCode().substring(0, $("#lblCode").val().length + 2));
                     }
                     changeLength(0);
                 });
                 $('#chkUseDate').bind('change', function () {
                     if ($("#lblCode").val().substring(klen).indexOf("DD") != -1) {
                         setCode(GetCode().substring(0, $("#lblCode").val().length - 2));
                     }
                     else {
                         setCode(GetCode().substring(0, $("#lblCode").val().length + 2));
                     }
                     changeLength(0);
                 });

                 $('#chkUseSplite').bind('change', function () {
                     if ($("#lblCode").val().substring(klen).indexOf("-") != -1) {
                         setCode(GetCode().substring(0, $("#lblCode").val().length - 1));
                     }
                     else {
                         setCode(GetCode().substring(0, $("#lblCode").val().length + 1));
                     }
                     changeLength(0);
                 });
                 $('#btnOK').bind('click', function () {
                     var obj = new Object();
                     obj.Code = $("#lblCode").val() + " " + $("#txtK").val().length;
                     window.returnValue = obj;
                     window.close();
                 });
                 init();
                 SetSample();
             });
             function init() {
                 try {
                     var obj = window.dialogArguments;
                     if (obj.val.length > 0) {
                         var Code = obj.val.split(' ')[0];
                         klen = parseInt(obj.val.split(' ')[1]);
                         $("#lblCode").val(Code);
                         $("#lblLength").val($("#lblCode").val().length);
                         $("#txtK").val(Code.substring(0, klen));
                         if (klen != 0) {
                             $("#chkUseK").attr("checked", true);
                         }
                         if (Code.substring(klen).indexOf("YYYY") != -1) {
                             $("#chkUseYear").attr("checked", true);
                         }
                         if (Code.substring(klen).indexOf("MM") != -1) {
                             $("#chkUseMonth").attr("checked", true);
                         }
                         if (Code.substring(klen).indexOf("DD") != -1)
                             $("#chkUseDate").attr("checked", true);
                         if (Code.substring(klen).indexOf("-") != -1)
                             $("#chkUseSplite").attr("checked", true);
                     }
                     else {
                         $("#lblLength").val("6");
                     }
                 }
                 catch (e) { }
             }
             function SetSample() {
                 var str1 = "" + $("#lblCode").val();
                 var str2 = str1;
                 str1 = str1.replace("YYYY", "2013"); 
                 str2 = str2.replace("YYYY", "2013");
                 str1 = str1.replace("MM", "01");
                 str2 = str2.replace("MM", "10");                 
                 str1 = str1.replace("DD", "01");
                 str2 = str2.replace("DD", "31");
                 if (str1.indexOf("X") != -1) {
                     var str4 = str1.substring(str1.indexOf("X"));
                     var str5 = "00000000000000000000000001"
                     str5 = str5.substring(str5.length - str4.length);
                     str1 = str1.replace(str4, str5);
                     str2 = str2.replace(str4, str5);
                 }
                 $("#lblSample1").val("2013/01/01 => " + str1);
                 $("#lblSample2").val("2013/10/31 => " + str2);
             }
             function changeLength(i) {
                 if ($("#lblCode").val().indexOf("X") == -1 && i == -1) return;
                 var len = (parseInt($("#lblLength").val()) + i);

                 if (len > 20 || len < 6) return;
                 var Code = $("#lblCode").val() + "XXXXXXXXXXXXXXXXXXXXXXXXXX";
                 $("#lblCode").val(Code.substring(0, len));
                 $("#lblLength").val(len);
                 SetSample();
             }
             function GetCode() {
                 var str = "";
                 if ($("#chkUseK").attr("checked")) str = str + $("#txtK").val();
                 if ($("#chkUseSplite").attr("checked")) str = str + "-";
                 if ($("#chkUseYear").attr("checked")) str = str + "YYYY";
                 if ($("#chkUseMonth").attr("checked")) str = str + "MM";
                 if ($("#chkUseDate").attr("checked")) str = str + "DD";
                 str = str + "XXXXXXXXXXXXXXXXXXXXXXXXXX";
                 return str;
             }
             function setCode(v) {
                 if (v.length > 20)
                     $("#lblCode").val(v.substring(0, 20));
                 else
                     $("#lblCode").val(v);
             }
   </script>
</head>
<body  style="overflow :hidden;">
    <form id="form1" runat="server" >
         <asp:ScriptManager ID="ScriptManager1" runat="server" />  
         
			<table class="maintable" cellspacing="0" cellpadding="0" width="100%" height="100%" align="center" bordercolor="#ffffff"
				border="1" >
					<tr>
						<td  align="center" width="15%" ><asp:Literal ID="ltlEnterpriseName" Text="格式編碼"
                                runat="server"></asp:Literal>
                        </td>
						<td width="70%" >&nbsp;
								<asp:textbox id="lblCode" runat="server" CssClass="TextRead" Width="90%"  ReadOnly ="true">XXXXXX</asp:textbox>
                                &nbsp;</td>
                       		<td><asp:button id="btnOK" runat="server" Text="確定" OnClientClick ="return false;"  CssClass="but" Width="80px"></asp:button></td>				
					</tr>
                    <tr>
						<td  align="center" width="10%" ><asp:Literal ID="Literal1" Text="編碼長度"
                                runat="server"></asp:Literal>
                        </td>
						<td width="70%" >&nbsp;
								<asp:textbox id="lblLength" runat="server" CssClass="TextRead" Width="30%" 
                                MaxLength="10">14</asp:textbox>
                                &nbsp;
								<asp:button id="btnDown" runat="server" Text="<"  OnClientClick =" return false;"
                                CssClass="but" Width="20px"></asp:button>&nbsp;<asp:button id="btnUp" 
                                runat="server" Text=">"   OnClientClick =" return false;" CssClass="but" Width="20px"></asp:button>&nbsp;</td>
                       			<td><asp:button id="Button1" runat="server" Text="取消"  CssClass="but" Width="80px" OnClientClick ="return false;" 
                                        ></asp:button></td>				
					</tr>  
                     <tr>
						<td  align="center" width="10%" ><asp:Literal ID="Literal2" Text="代碼設定"
                                runat="server"></asp:Literal>
                        </td>
						<td width="70%" >&nbsp;
								<asp:textbox id="txtK" runat="server" CssClass="TextBox" Width="30%" 
                                MaxLength="4"></asp:textbox>
                                &nbsp;</td>
                       			
					</tr>                 
					<tr>
                    <td   colspan ="3">
                        <asp:CheckBox ID="chkUseK" runat="server" Text ="代碼使用" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:CheckBox ID="chkUseSplite" runat="server" Text ="分格符號" />
                        </td>
                    </tr>
                    <tr>
                    <td   colspan ="3">
                        <asp:CheckBox ID="chkUseYear" runat="server" Text ="西元四碼" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:CheckBox ID="chkUseMonth" runat="server" Text ="月份使用" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:CheckBox ID="chkUseDate" runat="server" Text ="日期使用" />
                        </td>
                    </tr>
                    <tr>
                    <td  colspan ="3">
                    <br />
                       <fieldset style="width: 98%; height: 80px">
      <legend class="xuanxiangka" align ="left"><asp:Literal ID="Literal3" Text="範例"
                                runat="server"></asp:Literal></legend>
      <div align= "center">
      <table width="90%" align="center" >

       <tr height ="30px">
      <td><asp:textbox id="lblSample1" runat="server" CssClass="TextRead"  
              ReadOnly="true" Width="85%" >紋理防偽標籤15mm*30mm</asp:textbox>
           </td>
      </tr>
       <tr>
      <td><asp:textbox id="lblSample2" runat="server" 
              CssClass="TextRead"  ReadOnly="true" Width="85%" >紋理防偽標籤15mm*30mm</asp:textbox>
           </td>
      </tr>
      
      </table>
      </div>
     </fieldset>
                    </td>
                    </tr>
			</table>           
            
    </form>
</body>
</html>


