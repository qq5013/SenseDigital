<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SockInLoad.aspx.cs" Inherits="WebUI_Label_SockInLoad" %>

<%@ Register src="../../Controls/Calendar.ascx" tagname="Calendar" tagprefix="uc2" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html >
<head id="Head1" runat="server">
   <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
  
    <title>生產轉入庫單</title> 
        <link href="~/Styles/Main.css" type="text/css" rel="stylesheet" /> 
        <link href="~/Styles/op.css" type="text/css" rel="stylesheet" /> 
        <script type="text/javascript" src='<%=ResolveUrl("~/Scripts/JQuery/jquery-1.8.2.min.js") %>'></script>
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Commons.js") %>'></script>
</head>
<body>
    <form id="form1" runat="server">
  

     <table cellspacing="0" cellpadding="0" width="100%" align="center"   >

						<tr>
						<td align="center" width="10%" ><asp:Literal ID="ltlStyleID" Text="訂購單號:"
                                runat="server"></asp:Literal></td>
						<td  Width="35%" >
								&nbsp;<asp:textbox id="txtStyleID" runat="server" CssClass="Textbox" 
                                Width="90%" MaxLength="20" ReadOnly="true"></asp:textbox>
								~</td><td  Width="35%"><asp:textbox id="txtStyleID3" runat="server" CssClass="Textbox" 
                                Width="90%" MaxLength="20" ReadOnly="true"></asp:textbox>
								</td>
                                <td width="20%">  &nbsp;
                                                    <asp:button id="btnCheck1" runat="server" Text=" 指定" 
                                            CssClass="but" Width="50px"  
                                         >
                                    </asp:button>
                                                    &nbsp;<asp:button id="btnCheck4" runat="server" Text=" 納入" 
                                            CssClass="but" Width="50px"  
                                        >
                                    </asp:button>
                                                    </td>
                        </tr>
                        <tr>
                     <td align="center"><asp:Literal ID="Literal2" Text="生產單號:"
                                runat="server"></asp:Literal></td>
                                <td>
								    &nbsp;
								<asp:textbox id="Textbox4" runat="server" CssClass="Textbox" 
                                Width="90%" MaxLength="20" ReadOnly="true"></asp:textbox>&nbsp;~</td><td>
								<asp:textbox id="Textbox3" runat="server" CssClass="Textbox" 
                                Width="90%" MaxLength="20" ReadOnly="true"></asp:textbox></td><td>&nbsp; 
                                                    <asp:button id="btnCheck3" runat="server" Text=" 指定" 
                                            CssClass="but" Width="50px"  
                                        >
                                    </asp:button>
                                                    &nbsp;<asp:button id="btnCheck" runat="server" Text=" 取回" >
                                    </asp:button>
                            </td>
                                </tr>
                     <tr>
                     <td align="center"><asp:Literal ID="Literal1" Text="款式編號:"
                                runat="server"></asp:Literal></td>
                                <td>
								    &nbsp;<asp:textbox id="Textbox1" runat="server" CssClass="Textbox" 
                                Width="90%" MaxLength="20" ReadOnly="true"></asp:textbox>
								~</td><td>
								<asp:textbox id="Textbox2" runat="server" CssClass="Textbox" 
                                Width="90%" MaxLength="20" ReadOnly="true"></asp:textbox></td><td>&nbsp; 
								<asp:button id="btnCheck2" runat="server" Text=" 指定" 
                                            CssClass="but" Width="50px"  >
                                    </asp:button>
                         &nbsp;<asp:button id="btnCheck0" runat="server" Text=" 放棄" 
                                            CssClass="but" Width="50px"   >
                                    </asp:button>
                         </td>
                                </tr>
                        <tr><td colspan ="4"
                   > 
                            <div id="div_Detail1" class="fakeContainer" style=" margin:5px;height:205px;">
                            </div>
                            </td></tr>
                        </table>
    
    </form>
</body>
</html>


