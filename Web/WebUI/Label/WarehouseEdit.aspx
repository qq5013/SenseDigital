<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WarehouseEdit.aspx.cs" Inherits="WebUI_Label_WarehouseEdit" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
      <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />  
        <title></title> 
        <link href="~/Styles/Main.css" type="text/css" rel="stylesheet" /> 
        <link href="~/Styles/op.css" type="text/css" rel="stylesheet" /> 
          <script type="text/javascript" src='<%=ResolveUrl("~/Scripts/JQuery/jquery-1.8.3.min.js") %>'></script>
           <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Commons.js") %>'></script>
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Ajax.js") %>'></script>        

        <script type="text/javascript">

            function Save() {
                $("#txtWarehouseID").val(trim($("#txtWarehouseID").val()));
                $("#txtWarehouseName").val(trim($("#txtWarehouseName").val()));
                if ($("#txtWarehouseID").val() == "") {
                    alert("<%=Resources.Resource.LB_WarehouseID_NotNull %>");
                    $("#txtWarehouseID").focus();
                    return false;
                }
                if ($("#txtWarehouseName").val() == "") {
                    alert("<%=Resources.Resource.LB_WarehouseName_NotNull %>");
                    $("#txtWarehouseName").focus();
                    return false;
                }
                if ("<%= ID%>" == "") {
                    var exist = IsExists("<%= FormID%>", $("#txtWarehouseID").val(),"<%= cnKey%>");
                    if (exist) {
                        alert("<%=Resources.Resource.LB_WarehouseID_Exists %>");
                        $("#txtWarehouseID").focus();
                        return false;
                    }
                }
                return true;
            }
        </script>
</head>
<body>
    <form id="form1" runat="server">
    <table style="width: 100%; height: 20px;" class="OperationBar">
                <tr>
                    <td align="right">
                        <asp:Button ID="btnCancel" runat="server" Text="放棄" 
                            OnClientClick="return Cancel()" CssClass="ButtonCancel" 
                            meta:resourcekey="btnCancelResource1" />
                        <asp:Button ID="btnSave" runat="server" Text="保存" OnClientClick="return Save()" 
                            CssClass="ButtonSave" onclick="btnSave_Click" 
                            meta:resourcekey="btnSaveResource1" />
                        <asp:Button ID="btnExit" runat="server" Text="離開" OnClientClick="return Exit()" 
                            CssClass="ButtonExit" meta:resourcekey="btnExitResource1" />
                    </td>
                </tr>
            </table>
			<table id="Table1" class="maintable" cellspacing="0" cellpadding="0" width="100%" align="center" bordercolor="#ffffff"
				border="1" runat="server">			
					<tr>
						<td  valign="middle" align="left" colspan="4" height="22" class="title1">
							<p><asp:Literal ID="ltlTitle" Text="倉庫資料設定[ 單筆編輯畫面 ]" runat="server"  ></asp:Literal></p>
						</td>
					</tr>				
						
					<tr>
						<td class="musttitle" align="center" width="15%" >
                            <asp:Literal ID="ltlWarehouseID" Text="倉庫編號"
                                runat="server" ></asp:Literal>
                        </td>
						<td width="35%" >&nbsp;
								<asp:textbox id="txtWarehouseID" runat="server" CssClass="TextBox" 
                                Width="90%" MaxLength="10" ></asp:textbox>
                        </td>
						<td class="musttitle" align="center" width="15%">
                            <asp:Literal 
                                ID="ltlWarehouseName" Text="倉庫名稱"
                                runat="server" ></asp:Literal>
                        </td>
						<td  width="35%">&nbsp;
								<asp:textbox id="txtWarehouseName" runat="server" CssClass="TextBox" Width="90%" 
                                MaxLength="20" ></asp:textbox>
                        </td>
					</tr>
                    	<tr>
						<td class="smalltitle" align="center" width="15%" >
                            <asp:Literal ID="ltlContact" Text="連絡人"
                                runat="server" ></asp:Literal>
                        </td>
						<td width="35%" >&nbsp;
								<asp:textbox id="txtContact" runat="server" CssClass="TextBox" 
                                Width="90%" MaxLength="10" ></asp:textbox>
                        </td>
						<td class="smalltitle" align="center" width="15%">
                            <asp:Literal 
                                ID="ltlContactPhone" Text="連絡電話"
                                runat="server" ></asp:Literal>
                        </td>
						<td  width="35%">&nbsp;
								<asp:textbox id="txtContactPhone" runat="server" CssClass="TextBox" Width="90%" 
                                MaxLength="20" ></asp:textbox>
                        </td>
					</tr>
					<tr>
						<td class="smalltitle" align="center" >
                            <asp:Literal ID="ltlAddress" Text="倉庫地址"
                                runat="server" ></asp:Literal>
                        </td>
						<td width="85%" colspan="3" >&nbsp;
								<asp:textbox id="txtAddress" runat="server" CssClass="TextBox" 
                                Width="96%" MaxLength="30" ></asp:textbox>
                        </td>
					</tr>
					<tr>
						<td class="smalltitle" align="center" >
                            <asp:Literal 
                                ID="ltlMemo" Text="備&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;註"
                                runat="server" ></asp:Literal>
                        </td>
						<td  width="85%" colspan="3" height="70">&nbsp;
								<asp:textbox id="txtMemo" runat="server" CssClass="TextBox" Width="96%" 
                                MaxLength="2000" TextMode="MultiLine"
									Height="59px" ForeColor="Black"></asp:textbox>
                         </td>
					</tr>
                     <tr>
						<td class="smalltitle" align="center">
                            <asp:Literal ID="ltlCreateUserName" Text="建檔人員"
                                runat="server"></asp:Literal></td>
						<td >&nbsp;
								<asp:textbox id="txtCreateUserName" runat="server" CssClass="TextRead" Width="90%" 
                                MaxLength="20" ReadOnly="True" ></asp:textbox>
                         </td>
						<td class="smalltitle" align="center">                            
                            <asp:Literal ID="ltlLastModifyUserName" runat="server" Text="異動人員" ></asp:Literal>						
						</td>
						<td>&nbsp;
								<asp:textbox id="txtLastModifyUserName" runat="server" CssClass="TextRead" Width="90%" 
                                MaxLength="20" ReadOnly="True" ></asp:textbox></td>
					</tr>
                    <tr>
						<td class="smalltitle" align="center">
                            <asp:Literal ID="ltlCreateDate" Text="建檔日期"
                                runat="server" ></asp:Literal></td>
						<td >&nbsp;
								<asp:textbox id="txtCreateDate" runat="server" CssClass="TextRead" 
                                Width="90%" MaxLength="20" ReadOnly="True" ></asp:textbox>
                        </td>
						<td class="smalltitle" align="center"> 
                            <asp:Literal ID="ltlLastModifyDate" Text="異動日期"
                                runat="server" ></asp:Literal>							
						</td>
						<td >&nbsp;
                        <asp:textbox id="txtLastModifyDate" runat="server" CssClass="TextRead" Width="90%" 
                                MaxLength="20" ReadOnly="True" ></asp:textbox></td>
					</tr>
                    <tr>
						<td class="smalltitle" align="center">
                            <asp:Literal ID="ltlCheckUserName" Text="營運覆核"
                                runat="server"></asp:Literal></td>
						<td >&nbsp;
								<asp:textbox id="txtCheckUserName" runat="server" CssClass="TextRead" 
                                Width="90%" MaxLength="20"></asp:textbox>
                        </td>
						<td class="smalltitle" align="center">
                            <asp:Literal ID="ltlStopUserName" Text="停用人員"
                                runat="server" ></asp:Literal>
						
						</td>
						<td >&nbsp;
								<asp:textbox id="txtStopUserName" runat="server" CssClass="TextRead" Width="90%" 
                                MaxLength="20" ></asp:textbox>
                        </td>
					</tr>
                    <tr>
						<td class="smalltitle" align="center">
                            <asp:Literal ID="ltlCheckDate" Text="營運覆核日期"
                                runat="server" ></asp:Literal></td>
						<td >&nbsp;
								<asp:textbox id="txtCheckDate" runat="server" CssClass="TextRead" 
                                Width="90%" MaxLength="20" ReadOnly="True" ></asp:textbox>
                        </td>
						<td class="smalltitle" align="center">
                            <asp:Literal ID="ltlStopDate" Text="停用日期"
                                runat="server" ></asp:Literal>
						</td>
						<td >&nbsp;
								<asp:textbox id="txtStopDate" runat="server" CssClass="TextRead" Width="90%" 
                                MaxLength="20" ReadOnly="True" ></asp:textbox>
                        </td>
					</tr>				
			</table>
			
		</form>
</body>
</html>
