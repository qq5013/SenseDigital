<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StylePages.aspx.cs" Inherits="WebUI_Label_StylePages" %>
<%@ Register src="../../Controls/Calendar.ascx" tagname="Calendar" tagprefix="uc2" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html >
<head id="Head1" runat="server">
<base target ="_self" />
   <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
  
    <title>倉庫數量查詢</title> 
   
        <link href="~/Styles/Main.css" type="text/css" rel="stylesheet" /> 
        <link href="~/Styles/op.css" type="text/css" rel="stylesheet" /> 
        <link href="~/Styles/Detail.css" type="text/css" rel="stylesheet" /> 
        <link href="~/Styles/superTables.css" type="text/css" rel="stylesheet" /> 

        <script type="text/javascript" src='<%=ResolveUrl("~/Scripts/JQuery/jquery-1.8.2.min.js") %>'></script> 
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Resize.js") %>'></script>  
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Detail.js") %>'></script>
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/superTables.js") %>'></script>
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Scrolltable.js") %>'></script>
        <script type="text/javascript" src="Js/StylePages.js"></script>

        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Commons.js") %>'></script>
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/DataProcess.js") %>'></script>
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Ajax.js") %>'></script>   
        <script type="text/javascript">
            $(document).ready(function () {
                subData[0] = jQuery.parseJSON($('#HdnSubDetail1').val());
                initialTable();
                AddToDetail();

                $("#btnOK").bind('click', function () {

                    window.returnValue = GenerateDetailToJson(0);
                    window.close(); return false;
                });
                $("#btnCancel").bind('click', function () { window.close(); return false; });
            });

        </script>
    </head>
<body>
    <form id="form1" runat="server" target="_self"><asp:ScriptManager ID="ScriptManager1" runat="server" />  

        <div>
        
        <uc2:Calendar ID="txtBillDate" runat="server" Visible="False"  />
            <div id="div_Detail1" class="fakeContainer" style=" margin:5px;height:205px;">
            </div>


            <table class="maintable" cellspacing="0" cellpadding="0" width="100%" align="center"
            bordercolor="#ffffff" border="1">
            <tr>
                <td align="right">
                    <asp:Button ID="btnOK" runat="server" Text=" 確定" CssClass="but" Width="75px"></asp:Button>&nbsp;
                    <asp:Button ID="btnCancel" runat="server" Text="放棄" CssClass="but" Width="75px">
                    </asp:Button>&nbsp;&nbsp;
                </td>
            </tr>
            
        </table>
        <br />
        </div>
                     
        <input id="HdnSubDetail1" type="hidden" runat="server" />
        <div id="subColsName1" runat="server">
                    <asp:Literal ID="sub1xxWarehouseID" Text="(倉庫編號),80,text,1" Visible="false" runat="server"></asp:Literal>  
                    <asp:Literal ID="sub1xxWarehouseName" Text="(倉庫名稱),120,text,1" Visible="false" runat="server"></asp:Literal>  
                    <asp:Literal ID="sub1xxSafePages" Text="安全存量,80,numeric_q,0" Visible="false" runat="server"></asp:Literal>    
                    <asp:Literal ID="sub1xxInitialVolumes" Text="(期初卷數),80,numeric_q,1" Visible="false" runat="server"></asp:Literal>
                    <asp:Literal ID="sub1xxInitialPages" Text="(期初張數),80,numeric_q,1" Visible="false" runat="server"></asp:Literal>  
                    <asp:Literal ID="sub1xxNowVolumes" Text="(現有卷數),80,numeric_q,1" Visible="false" runat="server"></asp:Literal>  
                    <asp:Literal ID="sub1xxNowPages" Text="(現有張數),80,numeric_q,1" Visible="false" runat="server"></asp:Literal>  
                    <asp:Literal ID="sub1xxLastModifyUserName" Text="(異動人員),100,text,1" Visible="false" runat="server"></asp:Literal>  
                    <asp:Literal ID="sub1xxLastModifyDate" Text="(異動日期),100,text,1" Visible="false" runat="server"></asp:Literal>  
                       
        </div>
    </form>
</body>
</html>