<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OtherActionUpload.aspx.cs" Inherits="WebUI_Label_OtherActionUpload" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <title>標籤其他異動作業</title>
        <link href="~/Styles/Main.css" type="text/css" rel="stylesheet" /> 
        <link href="~/Styles/op.css" type="text/css" rel="stylesheet" /> 
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table class="maintable" cellspacing="0" cellpadding="0" width="100%" align="center" bordercolor="#ffffff"
				border="1" >			
				
						
					<tr>
						<td class="smalltitle" align="center" width="20%" ><asp:Literal ID="ltlEnterpriseID" Text="上傳人員"
                                runat="server"></asp:Literal>
                        </td>
						<td width="80%" >&nbsp;
								<asp:textbox id="txtEnterpriseID" runat="server" CssClass="TextRead"  ReadOnly="true" Width="50%" MaxLength="6"></asp:textbox>
                        </td>                      
					</tr>
                    <tr>
						<td class="smalltitle" align="center" width="20%" ><asp:Literal ID="Literal1" Text="企業編號"
                                runat="server"></asp:Literal>
                        </td>
						<td width="80%" >&nbsp;
								<asp:textbox id="Textbox1" runat="server" CssClass="TextBox"  Width="30%" MaxLength="6"></asp:textbox>
                                <asp:textbox id="Textbox2" runat="server" CssClass="TextRead"  ReadOnly="true" Width="60%" MaxLength="6"></asp:textbox>
                        </td>                      
					</tr>
                     <tr>
						<td class="smalltitle" align="center" width="20%" ><asp:Literal ID="Literal2" Text="文檔路徑"
                                runat="server"></asp:Literal>
                        </td>
						<td width="80%" >&nbsp;
								<asp:textbox id="Textbox3" runat="server" CssClass="TextRead"  ReadOnly="true" Width="90%" MaxLength="6"></asp:textbox>
                               
                        </td>                      
					</tr>
                     <tr>
						<td class="smalltitle" align="center" width="20%" ><asp:Literal ID="Literal3" Text="上傳資料"
                                runat="server"></asp:Literal>
                        </td>
						<td width="80%" >&nbsp;
								<asp:textbox id="Textbox4" runat="server" CssClass="TextBox"  Width="65%" MaxLength="6"></asp:textbox>
                                &nbsp;<asp:button id="Button5" runat="server" Text="瀏覽"  CssClass="but" Width="75px" ></asp:button>
                                  &nbsp;<asp:button id="Button1" runat="server" Text="上傳"  CssClass="but" Width="75px" ></asp:button>
                        </td>                      
					</tr>
                  </table>  		
    </div>
    </form>
</body>
</html>
