<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LoadOrder.aspx.cs" Inherits="WebUI_Label_LoadOrder" %>
<%@ Register src="../../Controls/Calendar.ascx" tagname="Calendar" tagprefix="uc2" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html >
<head id="Head1" runat="server">
   <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
  
    <title>訂單轉生產單</title> 
        <link href="~/Styles/Main.css" type="text/css" rel="stylesheet" /> 
        <link href="~/Styles/op.css" type="text/css" rel="stylesheet" /> 
        <script type="text/javascript" src='<%=ResolveUrl("~/Scripts/JQuery/jquery-1.8.2.min.js") %>'></script>
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Commons.js") %>'></script>
</head>
<body>
    <form id="form1" runat="server">
  <div style="height:300px; width :600px;">
     <table   class="maintable"  cellspacing="0" cellpadding="0" width="100%" align="center"   >
    
						<tr>
						<td class="smalltitle" align="center" width="10%" ><asp:Literal ID="ltlStyleID" Text="企業編號:"
                                runat="server"></asp:Literal></td>
						<td  Width="25%" >
								&nbsp;<asp:textbox id="txtStyleID" runat="server" CssClass="Textbox" 
                                Width="90%" MaxLength="20" ReadOnly="true"></asp:textbox>
								~</td><td  Width="25%"><asp:textbox id="txtStyleID3" runat="server" CssClass="Textbox" 
                                Width="90%" MaxLength="20" ReadOnly="true"></asp:textbox>
								</td>
                                <td width="25%">  &nbsp;
                                                    <asp:button id="btnCheck1" runat="server" Text=" 指定" 
                                            CssClass="but" Width="75px"  
                                        >
                                    </asp:button>
                                                    </td>
                        </tr>
                        <tr>
                     <td class="smalltitle" align="center"><asp:Literal ID="Literal2" Text="訂購日期:"
                                runat="server"></asp:Literal></td>
                                <td>
								    &nbsp;<uc2:Calendar ID="cldBillDate" runat="server" />
								~</td><td>
								<uc2:Calendar ID="Calendar1" runat="server" /></td><td>&nbsp; </td>
                                </tr>
                     <tr>
                     <td class="smalltitle" align="center"><asp:Literal ID="Literal1" Text="訂購單號:"
                                runat="server"></asp:Literal></td>
                                <td>
								    &nbsp;<asp:textbox id="Textbox1" runat="server" CssClass="Textbox" 
                                Width="90%" MaxLength="20" ReadOnly="true"></asp:textbox>
								~</td><td>
								<asp:textbox id="Textbox2" runat="server" CssClass="Textbox" 
                                Width="90%" MaxLength="20" ReadOnly="true"></asp:textbox></td><td>&nbsp; 
								<asp:button id="btnCheck2" runat="server" Text=" 指定" 
                                            CssClass="but" Width="75px"   >
                                    </asp:button>
                         </td>
                                </tr>
                    <tr>
                     <td class="smalltitle" align="center"><asp:Literal ID="Literal3" Text="預交日期:"
                                runat="server"></asp:Literal></td>
                                <td>
								    &nbsp;<uc2:Calendar ID="Calendar2" runat="server" />
								~</td><td>
								<uc2:Calendar ID="Calendar3" runat="server" /></td><td>&nbsp; 
                                    &nbsp;</td>
                                </tr>
                                   <tr>
                     <td align="center">&nbsp;</td>
                                <td>
								    &nbsp;</td><td>
								           &nbsp;</td><td>&nbsp; 
                            <asp:button id="Button1" runat="server" Text=" 取回" 
                                            CssClass="but" Width="75px"   >
                                    </asp:button>
                                    &nbsp;<asp:button id="Button2" runat="server" Text=" 放棄" 
                                            CssClass="but" Width="75px"   >
                                    </asp:button>
                        </td>
                                </tr>
                                <tr style ="height:150px;">
     <td></td>
     <td></td>
     <td></td>
     <td></td>
     </tr>
                        </table>
    </div>
    </form>
</body>
</html>

