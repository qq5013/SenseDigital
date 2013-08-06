<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StyleLimitPro.aspx.cs" Inherits="WebUI_Label_StyleLimitPro" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html >
<head runat="server">
   <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
  
    <title></title> 
        <link href="~/Styles/Main.css" type="text/css" rel="stylesheet" /> 
        <link href="~/Styles/op.css" type="text/css" rel="stylesheet" /> 
</head>
<body>
    <form id="form1" runat="server">
    <div>
     <table cellspacing="0" cellpadding="0" width="100%" align="center"   >

						<tr>
						<td align="center" width="10%" ><asp:Literal ID="ltlStyleID" Text="企業代號:"
                                runat="server"></asp:Literal></td>
						<td width="15%" >
								<asp:textbox id="txtStyleID" runat="server" CssClass="TextRead" 
                                Width="90%" MaxLength="20" ReadOnly="true"></asp:textbox>
								</td>
                                <td width="25%">
								<asp:textbox id="txtStyleID0" runat="server" CssClass="TextRead" 
                                Width="90%" MaxLength="20" ReadOnly="true"></asp:textbox>
                                </td>
                                <td width="15%">                            <asp:button id="btnCheck" runat="server" Text=" 確定" 
                                            CssClass="but" Width="75px"  >
                                    </asp:button>&nbsp;
                                                    <asp:button id="Button1" runat="server" Text=" 取消" 
                                            CssClass="but" Width="75px" >
                                    </asp:button>
                        </td>
                        </tr>
                     <tr>
                     <td align="center"><asp:Literal ID="Literal1" Text="款式編號:"
                                runat="server"></asp:Literal></td>
                                <td>
								<asp:textbox id="txtStyleID1" runat="server" CssClass="TextRead" 
                                Width="90%" MaxLength="20" ReadOnly="true"></asp:textbox>
                         </td><td>
								<asp:textbox id="txtStyleID2" runat="server" CssClass="TextRead" 
                                Width="90%" MaxLength="20" ReadOnly="true"></asp:textbox>
                         </td><td></td>
                                </tr>
                     <tr><td colspan ="4">
                     <br />
                     <div>

                         <div id="div_Detail1" class="fakeContainer" style=" margin:5px;height:205px;">
                         </div>
            
                     </div>
                     </td> </tr>
                        </table>
    </div>
                    <input id="HdnSubDetail1" type="hidden" runat="server" />
                    
                    <div id="subColsName1" runat="server">
                    <asp:Literal ID="sub1xxRowID" Text="(序號),40,label,1" Visible="false" runat="server"></asp:Literal>  
                    <asp:Literal ID="sub1xxProductID" Text="產品編號,100,text,1" Visible="false" runat="server"></asp:Literal>  
                    <asp:Literal ID="sub1xxProductName" Text="(品名規格),200,text,1" Visible="false" runat="server"></asp:Literal>    
                    <asp:Literal ID="sub1xxMemo" Text="(備註),200,text,1" Visible="false" runat="server"></asp:Literal>     
                       
                    </div>
    </form>
</body>
</html>
