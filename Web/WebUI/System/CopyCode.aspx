<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CopyCode.aspx.cs" Inherits="WebUI_System_CopyCode" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=UTF-8" /> 
    <title>複製編碼</title>
     <link href="~/Styles/Main.css" type="text/css" rel="stylesheet" /> 
        <link href="~/Styles/op.css" type="text/css" rel="stylesheet" /> 
        <link href="~/Styles/Detail.css" type="text/css" rel="stylesheet" /> 
        <link href="~/Styles/superTables.css" type="text/css" rel="stylesheet" /> 
        <script type="text/javascript" src='<%=ResolveUrl("~/Scripts/JQuery/jquery-1.8.3.min.js") %>'></script> 	  

        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Commons.js") %>'></script>
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Ajax.js") %>'></script>
        <script type="text/javascript" language="javascript">
            var curcel = null;
            $(document).ready(function () {

                $('#btnDownSame').bind('click', function () {
                    if (curcel == null) return false;
                    //curcel.parentNode.parentNode.parentNode.rows[2].cells[0].children[0].id
                    //curcel.parentNode.parentNode.parentNode.rows.each(function () { alert(1); })
                    for (var i = curcel.parentNode.parentNode.rowIndex + 1; i < curcel.parentNode.parentNode.parentNode.rows.length; i++) {
                        curcel.parentNode.parentNode.parentNode.rows[i].cells[0].children[0].checked = curcel.checked;
                    }
                    return false;
                });
                $("input[type=checkbox]").bind('change', function () {
                    //alert("click");
                    curcel = this;
                });
                $('#btnOk').bind('click', function () {
                    var obj = Array();
                    for (var i = 1; i < $("#table1")[0].rows.length; i++) {
                        if ($("#table1")[0].rows[i].cells[0].children[0].checked)
                            obj.push($("#table1")[0].rows[i].cells[0].children[0].id);
                    }
                    window.returnValue = obj;
                    window.close();
                    return false;
                });
                $('#Button4').bind('click', function () { window.close(); return false; });
            });
          </script>
</head>
<body style="overflow:hidden;">
    <form id="form1" runat="server">
    <div>
   	<table id = "table1" class="maintable" cellspacing="0" cellpadding="0" height="100%"  width ="100%"
            align="center" bordercolor="#ffffff"
				border="1" >
					<tr>
						<td  align="left" ><asp:button id="btnDownSame" runat="server" Text="以下同值"  CssClass="but" Width="80px"></asp:button>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:button id="btnOk" runat="server" Text="確定"  CssClass="but" Width="50px"></asp:button>
                        <asp:button id="Button4" runat="server" Text="取消"  CssClass="but" Width="50px"></asp:button>
                        </td>
								
					</tr>
                    <tr>
                    <td >
                        <asp:CheckBox ID="ProductionCode" runat="server" Text ="標籤生產單編碼原則" />
                        </td>
                    </tr>
                     <tr>
                    <td >
                        <asp:CheckBox ID="InStocCode" runat="server" Text ="標籤入庫單編碼原則" />
                        </td>
                    </tr>
                     <tr>
                    <td >
                        <asp:CheckBox ID="BadLabelCode" runat="server" Text ="壞品作廢單編碼原則" />
                        </td>
                    </tr>
                     <tr>
                    <td >
                        <asp:CheckBox ID="EnableLabelNoCode" runat="server" Text ="標籤啟用單編碼原則" />
                        </td>
                    </tr>
                     <tr>
                    <td >
                        <asp:CheckBox ID="OtherActionCode" runat="server" Text ="其他異動單編碼原則" />
                        </td>
                    </tr>
                     <tr>
                    <td >
                        <asp:CheckBox ID="OtherBatchActionCode" runat="server" Text ="批次異動單編碼原則" />
                        </td>
                    </tr>
                     <tr>
                    <td >
                        <asp:CheckBox ID="ReturnLabelCode" runat="server" Text ="退貨作廢單編碼原則" />
                        </td>
                    </tr>
                
			</table>        
    </div>
    </form>
</body>
</html>
