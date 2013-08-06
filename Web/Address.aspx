<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Address.aspx.cs" Inherits="Address" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="~/Styles/Main.css" type="text/css" rel="stylesheet" /> 
    <link href="~/Styles/op.css" type="text/css" rel="stylesheet" /> 
    <script type="text/javascript" src='<%=ResolveUrl("~/Scripts/JQuery/jquery-1.8.3.min.js") %>'></script>
    <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Address.js") %>'></script>
    
    <script type="text/javascript">
        
    </script>


</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table class="gbtext" style="BORDER-COLLAPSE: collapse; HEIGHT: 32px" borderColor="#93bee2"
				cellSpacing="0" cellPadding="0" width="100%" align="center" border="1">
				<tr>
					<td style="height: 32px" valign="middle" align="left" height="24">&nbsp;
                        <asp:Button ID="btnGetBack" OnClientClick="return getBack()" Width="60px" runat="server" Text="取回" CssClass="but"/>
                        &nbsp;
                        <asp:Button ID="btnClear" OnClientClick="return clearAddress()" Width="60px" runat="server" Text="清除" CssClass="but"/>
                        &nbsp;
                        <asp:Button ID="btnClose" OnClientClick="return closeAddress()" Width="60px" runat="server" Text="關閉" CssClass="but"/>
                    </td>
				</tr>
				<tr>
					<td style="height: 28px" valign="middle" align="left" height="24">&nbsp;&nbsp;
						<asp:textbox id="txtAddress" runat="server" Width="452px" CssClass="TextBox"></asp:textbox></td>
				</tr>
				<tr>
					<td style="HEIGHT: 28px;font-family:新細明體;font-size: 9pt;" >&nbsp;&nbsp;
                    <asp:Literal ID="ltlTitle" Text="縣市" runat="server"></asp:Literal>
                   <asp:dropdownlist id="ddlCity" runat="server" Width="104px"></asp:dropdownlist>
                    <asp:Literal ID="Literal1" Text="區域" runat="server"></asp:Literal>
                   <asp:dropdownlist id="ddlDistrict" runat="server" Width="112px"></asp:dropdownlist><asp:textbox id="txtCodeTmp" runat="server" Width="64px" MaxLength="5" CssClass="TextBox"></asp:textbox>&nbsp;
						<asp:dropdownlist id="ddlTown" runat="server" Width="110px"></asp:dropdownlist>&nbsp;</td>
				</tr>
				<tr>
					<td><input style="WIDTH: 32px; HEIGHT: 32px" onclick="changeAddress('縣')" type="button" value="縣"  class="but"/>
                        <input style="WIDTH: 32px; HEIGHT: 32px" onclick="changeAddress('市')" type="button" value="市" class="but"/>
                        <input style="WIDTH: 32px; HEIGHT: 32px" onclick="changeAddress('0')" type="button" value="0" class="but"/>
                        <input style="WIDTH: 32px; HEIGHT: 32px" onclick="changeAddress('1')" type="button" value="1" class="but"/>
                        <input style="WIDTH: 32px; HEIGHT: 32px" onclick="changeAddress('2')" type="button" value="2" class="but"/>
                        <input style="WIDTH: 32px; HEIGHT: 32px" onclick="changeAddress('3')" type="button" value="3" class="but"/>
                        <input style="WIDTH: 32px; HEIGHT: 32px" onclick="changeAddress('4')" type="button" value="4" class="but"/>
                        <input style="WIDTH: 32px; HEIGHT: 32px" onclick="changeAddress('5')" type="button" value="5" class="but"/>
                        <input style="WIDTH: 32px; HEIGHT: 32px" onclick="changeAddress('6')" type="button" value="6" class="but"/>
                        <input style="WIDTH: 32px; HEIGHT: 32px" onclick="changeAddress('7')" type="button" value="7" class="but"/>
                        <input style="WIDTH: 32px; HEIGHT: 32px" onclick="changeAddress('8')" type="button" value="8" class="but"/>
                        <input style="WIDTH: 32px; HEIGHT: 32px" onclick="changeAddress('9')" type="button" value="9" class="but"/>
                        <input style="WIDTH: 32px; HEIGHT: 32px" onclick="changeAddress('10')" type="button" value="10" class="but"/>
                        <input style="WIDTH: 32px; HEIGHT: 32px" onclick="changeAddress('中')" type="button" value="中" class="but"/>
                    </td>
				</tr>
				<tr>
					<td><input style="WIDTH: 32px; HEIGHT: 32px" onclick="changeAddress('鄉')" type="button" value="鄉" class="but"/>
                        <input style="WIDTH: 32px; HEIGHT: 32px" onclick="changeAddress('鎮')" type="button" value="鎮" class="but"/>
                        <input style="WIDTH: 32px; HEIGHT: 32px" onclick="changeAddress('區')" type="button" value="區" class="but"/>
                        <input style="WIDTH: 32px; HEIGHT: 32px" onclick="changeAddress('村')" type="button" value="村" class="but" />
                        <input style="WIDTH: 32px; HEIGHT: 32px" onclick="changeAddress('里')" type="button" value="里" class="but"/>
                        <input style="WIDTH: 32px; HEIGHT: 32px" onclick="changeAddress('鄰')" type="button" value="鄰" class="but"/>
                        <input style="WIDTH: 32px; HEIGHT: 32px" onclick="changeAddress('路')" type="button" value="路" class="but"/>
                        <input style="WIDTH: 32px; HEIGHT: 32px" onclick="changeAddress('街')" type="button" value="街" class="but"/>
                        <input style="WIDTH: 32px; HEIGHT: 32px" onclick="changeAddress('段')" type="button" value="段" class="but"/>
                        <input style="WIDTH: 32px; HEIGHT: 32px" onclick="changeAddress('巷')" type="button" value="巷" class="but"/>
                        <input style="WIDTH: 32px; HEIGHT: 32px" onclick="changeAddress('弄')" type="button" value="弄" class="but"/>
                        <input style="WIDTH: 32px; HEIGHT: 32px" onclick="changeAddress('號')" type="button" value="號" class="but"/>
                        <input style="WIDTH: 32px; HEIGHT: 32px" onclick="changeAddress('之')" type="button" value="之" class="but"/>
                        <input style="WIDTH: 32px; HEIGHT: 32px" onclick="changeAddress('樓')" type="button" value="樓" class="but"/>
                    </td>
				</tr>
			</table>
    </div>
    </form>
</body>
</html>
