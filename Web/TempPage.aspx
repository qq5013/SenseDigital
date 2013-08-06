<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TempPage.aspx.cs" Inherits="TempPage" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title><%=TitleName%></title>
</head>
<frameset rows="0,*">
	<frame src="about:blank">
	<frame src="Select.aspx?FormID=<%= FormID%>&Option=<%= Option%>&strWhere=<%= strWhere%>&cnKey=<%= cnKey%>" style="overflow-x: no;overflow-y:hidden;">
</frameset>

</html>
