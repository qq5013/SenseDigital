<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ErrorMsg.aspx.cs" Inherits="ErrorMsg" %>
<!--
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
-->
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table width="600" border="0" align="center" cellpadding="5" cellspacing="0">
					<tr>
						<td  class="table_bgcolor">
							<table width="100%" border="1" cellpadding="5" cellspacing="0" 
							
							class="table_bordercolor"
							
							>
								<tr bgcolor="#e4e4e4">
									<td height="22" class="table_titlebgcolor"><STRONG><FONT color="red">發生問題：</FONT></STRONG></td>
								</tr>
								<tr>
									<td height="22">
										<table cellSpacing="0" cellPadding="0" width="100%" border="0">
											<tr>
												<td height="22">
													<asp:Label id="lblMsg" runat="server" Width="100%"></asp:Label>
												</td>
											</tr>
										</table>
									</td>
								</tr>
								<tr>
									<td height="22">
										<div align="center"><input type="button" value="返  回" style="WIDTH: 120px" onclick="javascript:history.back();"></div>
									</td>
								</tr>
							</table>
						</td>
					</tr>
				</table>
    </div>
    </form>
</body>
</html>
