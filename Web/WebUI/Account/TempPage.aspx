<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TempPage.aspx.cs" Inherits="WebUI_Account_TempPage" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title><% =TitleName%></title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR" />
	<meta content="C#" name="CODE_LANGUAGE" />
	<meta content="JavaScript" name="vs_defaultClientScript" />
	<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />
</head>
<frameset rows="0,*">
	<frame src="about:blank">
	<FRAME src="<% =page%>?UserName=<%=UserName%>&FormID=<% =FormID%>"
</frameset>
</html>