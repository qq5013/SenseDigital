<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ParameterCloseDate.aspx.cs" Inherits="WebUI_System_ParameterCloseDate" %>

<%@ Register src="../../Controls/Calendar.ascx" tagname="Calendar" tagprefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="~/Styles/Main.css" type="text/css" rel="stylesheet" /> 
    <link href="~/Styles/op.css" type="text/css" rel="stylesheet" /> 

    <script type="text/javascript" src='<%=ResolveUrl("~/Scripts/JQuery/jquery-1.8.3.min.js") %>'></script>
    <script type="text/javascript" src='<%=ResolveUrl("~/JScript/DataProcess.js") %>'></script>
    <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Commons.js") %>'></script>
    <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Ajax.js") %>'></script>        
    
    <script type="text/javascript">
        function Cancel() {
            window.parent.returnValue = '0';
            window.parent.close();
        }
    </script>    
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table class="maintable" cellspacing="0" cellpadding="0" width="100%" align="center" bordercolor="#ffffff" border="1" >			

					<tr>
						<td class="musttitle" align="center" ><asp:Literal ID="ltlCloseDate" Text="關帳日期"
                                runat="server"></asp:Literal>
                        </td>
						<td width="60%" >&nbsp;
								<uc2:Calendar ID="txtCloseDate" runat="server" />
                        </td>
						
                                           
					</tr>
                    
                    <tr>
						<td align="center" colspan="2">
                         
                             <asp:Button ID="btnOK" runat="server" CssClass="but" Text="確定" 
                                 Width="75px" onclick="btnOK_Click" />
                             &nbsp;
                            <asp:Button ID="btnCancel" runat="server" CssClass="but" Text="取消" OnClientClick="Cancel()"
                                 Width="70px" />
                        </td>
						
					</tr>
                                      
			</table>
    </div>
    </form>
</body>
</html>


