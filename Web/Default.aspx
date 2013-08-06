<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link rel="stylesheet" href="Scripts/zTree/css/zTreeStyle/zTreeStyle.css" type="text/css" />
    
    <link rel="stylesheet" type="text/css" href="Scripts/easyui/themes/default/easyui.css" />
	<link rel="stylesheet" type="text/css" href="Scripts/easyui/themes/icon.css" />
	<script type="text/javascript" src="Scripts/JQuery/jquery-1.8.3.min.js"></script>
	<script type="text/javascript" src="Scripts/easyui/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="Scripts/easyui/locale/easyui-lang-zh_CN.js"></script>
    
    <script type="text/javascript" src="Scripts/zTree/js/jquery.ztree.core-3.4.js"></script>
	<script type="text/javascript" src="Scripts/zTree/js/jquery.ztree.excheck-3.4.js"></script>
	<script type="text/javascript" src="Scripts/zTree/js/jquery.ztree.exedit-3.4.js"></script>
	<script type="text/javascript" src="Scripts/js/tabs.js"></script>
    <script type="text/javascript">

        
    </script>
</head>
<body class="easyui-layout">
    <form id="form1" runat="server">
    <div data-options="region:'north',split:true" title="Top畫面" style="height:100px;padding:10px;">
		<p>The north content.<asp:Button ID="Button1" runat="server" Text="Button" OnClientClick="addTab('客戶多筆資料','WebUI/BusinessUnit/Persons.aspx');return false;" />
            <asp:Button ID="Button2" runat="server" Text="Button" 
                OnClientClick="setUrl();return false;" />
            <asp:Button ID="Button3" runat="server" Text="客戶多筆" 
                OnClientClick="addTab('客戶多筆資料','WebUI/BusinessUnit/Departments.aspx');return false;" />
                 <asp:Button ID="Button4" runat="server" Text="客戶單筆編輯"
                OnClientClick="addTab('客戶單筆資料','WebUI/BusinessUnit/DepartmentEdit.aspx');return false;" />
                 
        </p>
	</div>
	<div data-options="region:'south',split:true" title="Bottom畫面" style="height:50px;padding:10px;background:#efefef;">
		<div class="easyui-layout" data-options="fit:true" style="background:#ccc;">
			<div data-options="region:'center'">sub center</div>
			
		</div>
	</div>
<%--	<div data-options="region:'east',iconCls:'icon-reload',split:true" title="Tree Menu" style="width:180px;">
		<ul class="easyui-tree" data-options="url:'Default.ashx'"></ul>
	</div>--%>
	<div data-options="region:'west',split:true" title="防偽系統" style="width:200px;padding:1px;overflow:hidden;">
		<div class="easyui-accordion" data-options="selected:true,fit:true,border:false">
			<div title="營運單位作業模組" style="padding:10px;overflow:auto;">
				<%--<p>營運單位基本資料</p>
				<p>營運單位作業人員</p>
				<p>廠商資料</p>
				<p>廠商作業人員</p>
				<p>廠商產品資料</p>
				<p>廠商產品生產履歷</p>
				<p>廠商產品物流資訊</p>$('#myul').tree({
				url: "readdb.ashx",
				onClick:function(node){
				opentb(node.text);	
				}				
			});  
				<p>會員資料</p>--%>
				<ul class="easyui-tree" data-options="checkbox:true,url:'Default.ashx',onClick:function(node){addTab(node.text,'Tab2.aspx');} "></ul>
			</div>
			<div title="標籤作業模組" style="padding:10px;">
				<ul id="treeDemo" class="ztree"></ul>
			</div>
			<div title="標籤使用單位模組" style="padding:10px">
				content3
			</div>
			<div title="會員作業模組" style="padding:10px">
				content4
			</div>
			<div title="密碼權限模組" style="padding:10px">
				content5
			</div>
		</div>
	</div>
	<div data-options="region:'center'" title="主操作畫面" style="overflow:hidden;">
		<div id="tabs" class="easyui-tabs" data-options="fit:true,border:false" >
			<div id="t1" title="桌面" style="padding:20px;overflow:hidden;"> 
				<div style="margin-top:20px;">
					<h3>營運單位作業桌面</h3>
					<ul>
						<li>產品資料</li> 
						<li>人員資料</li> 
						<li>部門資料</li> 
					</ul>
				</div>
			</div>
			<div id="t2"  title="廠商資料" data-options="closable:true" style="padding:0px;">
			    <input type="button" value="add tab" onclick="addSubTab();" />
                <input type="button" value="add tab" onclick="addTab('客戶多筆資料','WebUI/BusinessUnit/Persons.aspx');" />
			    <div id="dtt"  class="easyui-tabs" data-options="fit:true,border:true">
			        <div title="Tab1" style="padding:5px;overflow:hidden;"> 
				        
			        </div>
			    </div>
			</div>
			<div id="t3" title="會員資料" data-options="iconCls:'icon-reload',closable:true,href: 'Tab2.aspx'" style="overflow:hidden;padding:5px;">
				
			</div>
			<div id="Div1" title="GridView" data-options="iconCls:'icon-reload',closable:true,href: 'Tab1.aspx'">
				
			</div>
		</div>
	</div>
	<div id="mm" class="easyui-menu" style="width:150px;"> 
        <div id="refresh">刷新</div> 
        <div class="menu-sep"></div> 
        <div id="close">关闭</div> 
        <div id="closeall">全部关闭</div> 
        <div id="closeother">除此之外全部关闭</div> 
        <div class="menu-sep"></div> 
        <div id="closeright">当前页右侧全部关闭</div> 
        <div id="closeleft">当前页左侧全部关闭</div> 
        <div class="menu-sep"></div> 
        <div id="exit">退出</div> 
</div> 

    </form>
</body>
</html>

