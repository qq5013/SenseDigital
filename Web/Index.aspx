<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Index.aspx.cs" Inherits="Index" %>
<html xmlns="http://www.w3.org/1999/xhtml" onunload="GoReset();">
<head id="Head1" runat="server">
    <title>信實中華防偽雲雲端平台管理系統</title>
<script type="text/javascript" language="javascript">
   window.moveTo(0,0);
   window.resizeTo(screen.availWidth,screen.availHeight);

    function avoid(obj)
    {
       alert(obj.src);
   }
   function hrefTab(url, titleName,id) {
       mainFrame.addTab(url, titleName,id);
   }
   function delTab() {
       mainFrame.delTab();
   }

</script>
</head>
<frameset id="frameMain" rows="71,*" cols="*" frameborder="no" border="0" framespacing="0">
  <frame src="Index/Top.aspx" name="topFrame" scrolling="No" noresize="noresize" frameborder="0" id="topFrame" title="topFrame" />
  <frameset id="main" rows="*" cols="170,8,100%" framespacing="0" frameborder="no" border="0">
    <frame src="Index/Left.aspx" name="leftFrame" scrolling="auto" noresize="noresize" frameborder="0" id="leftFrame" title="leftFrame"/>
    <frame id="handle" name="handle" src="Index/Spliter.aspx" frameborder=0 scrolling="no" noresize="noresize"/>
    <frameset rows="8,*" cols="*" frameborder="no" border="0" framespacing="0">
        <frame id="handle" name="handle" src="Index/topButton.aspx" frameborder=0 scrolling="no" noresize="noresize"/>
        <!--frame id="Navigation" name="Navigation" frameborder=0 scrolling=no noresize=noresize src="NavigationPage.aspx" /-->
        <frame name="mainFrame" id="mainFrame" frameborder="0" title="mainFrame" src="Index/Default.aspx"/>
    </frameset>
  </frameset>
</frameset>
</html>
