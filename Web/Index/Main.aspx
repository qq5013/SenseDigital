﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Main.aspx.cs" Inherits="Index_Main" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server"> 
<style type="text/css">
</style>
    <title>主页</title>
    <link href="Css/css.css?t=88" type="text/css" rel="stylesheet" />
    <style type="text/css">
      .ButtonMessage
      {
        font-family: "Tahoma", "宋体"; 
        font-size:9pt;  
    /*  border: 0px #ff0000 solid; 
        background-color:Transparent;
         BORDER-BOTTOM: #ffffff 0px solid;  
        BORDER-LEFT: #93bee2 0px solid;  
        BORDER-RIGHT: #93bee2 0px solid;  
        BORDER-TOP: #93bee2 0px solid; */
        background-image:url(images/op/light.gif); 
        background-repeat:no-repeat;
        CURSOR: hand; 
        padding-left:13px;
        font-style: normal ; 
        height:24px;
      }
    </style>
<script type="text/javascript" language="javascript">
  function SetNewColor(source)
  {
            _oldColor=source.style.backgroundColor;
            source.style.backgroundColor='#C0E4EE';
          
  }
  function SetOldColor(source)
  {
         source.style.backgroundColor=_oldColor;
  }
</script>

<script language="JavaScript" type="text/javascript">

</head>
<body bgcolor="#F8FCFF" style="margin-top:30px;">
    <form id="form1" runat="server">
    </form>
        <DIV ID="oDialog"  style="position:absolute; bottom:0; right:1px;display:none;">
            <table style="width:202px;" cellpadding="0" cellspacing="0">
        <asp:Panel ID="pnlRemind" runat="server" style="position:absolute; right:0; bottom:0;" Visible="false">
           <span style="font-size:13px; cursor:pointer;  width:68px; height:23px; padding:5 3 3 28; background-image:url(images/desk/msg_button.gif);" onclick="ShowMessage()">
             消息</span>
        </asp:Panel>
</body>
</html>
<script>
   if(document.getElementById('pnlRemind')!=null)
   {
//      ShowMessage();
//      window.setInterval(ShowMessage(),10)
     document.getElementById('pnlRemind').style.display='none';
     showMsg('oDialog');
   }
   
function ShowMessage()
     showMsg('oDialog');


//设置透明度
function setOpacity(obj, value){
    if(document.all){
        if(value == 100){
            obj.style.filter = "";
        }else{
           //alert(value);
            obj.style.filter = "alpha(opacity=" + value + ")";    
        }
    }else{
        obj.style.opacity =value / 100 ;
       
    }
}
//用setTimeout循环减少透明度
function changeOpacity(obj, startValue, endValue, step, speed){
    if(startValue >= endValue){
         //document.body.removeChild(obj);
         document.getElementById('oDialog').style.display='block';
        return;
    }
    if(!obj)
    {
      return;
    }
    if(startValue>=100)
    {
      //document.body.removeChild(obj);
      document.getElementById('oDialog').style.display='block';
      return;
    }
   // alert(startValue);
    setOpacity(obj, startValue);
    setTimeout(function(){changeOpacity(obj, startValue+step, endValue, step, speed);}, speed);
}
//设置隐藏速度和id
function showMsg(id){
    var msg =document.getElementById(id);
    var step = 5, speed = 80;
//    if(msg.style.display=="none")
//    {
//      msg.style.display="";
//    }
    msg.style.display='block';
    changeOpacity(msg, 0, 100, step, speed);
}


</script>