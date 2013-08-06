<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmRptView.aspx.cs" Inherits="WebUI_Rpt_frmRptView" %>
<%@ Register Assembly="FastReport.Web" Namespace="FastReport.Web" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title><asp:Literal ID="Literal1" Text="企業編號" runat="server"></asp:Literal></title>
    <base target ="_self" />
</head>
<body>
    <form id="form1" runat="server">
   
    <div style="width: 100%; height: 100%; ">
                    <span style="font-size: 10pt; font-family: Tahoma"><strong>
                        <span style="font-size: 4pt"> </span>
                        <cc1:WebReport ID="WebReport1" runat="server" BackColor="White" ButtonsPath="images\buttons1"
                            Font-Bold="False" Height="700px" OnStartReport="WebReport1_StartReport"
                            ToolbarColor="Lavender" Width="100%" Zoom="1" />
                    </strong></span>                
    </div>
    </form>
</body>
</html>
