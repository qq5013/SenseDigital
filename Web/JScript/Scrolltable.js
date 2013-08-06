////function CreateDataGrid(element)
//{
//	var selectBgColor = '#A3A3A3';//被選擇行的背景色
//	var headTdColor = 'menu';//表頭背景色
//	var trOverColor = '#D3D3D3';
//	function setTdWidth(tdIndex)//設置表格的列寬
//	{
//	
//	
//	//if(LIST.rows[0].cells[tdIndex].className!=)
//	//	alert(LIST.rows[0].cells[tdIndex].className);
//	
//		var td = document.getElementById(stableID).rows[0].cells[tdIndex];
//		var tab = td;
//		
//		while(tab.tagName!='TABLE')tab=tab.parentElement;//顯示表格內容的Table
//		tab=tab.parentElement;
//		while(tab.tagName!='TABLE')tab=tab.parentElement;//最外層的Table
//		var hTab = tab.rows[0].firstChild;//頭Table
//		while(hTab.tagName!='TABLE')hTab=hTab.firstChild;
//		var headTd = hTab.rows[0].cells[td.cellIndex];
//		//alert(headTd.width);
//		
//	if(headTd.className!="hidden"){
//		if(document.getElementById(stableID).rows[0].cells[tdIndex].offsetWidth){
//	//	td.width =LIST.rows[0].cells[tdIndex].offsetWidth;
//	  //  td.style.width = LIST.rows[0].cells[tdIndex].offsetWidth;
//		
//		td.width = headTd.offsetWidth;
//		td.style.width = headTd.offsetWidth;
//		td.innerHTML = "<nobr>" + td.innerHTML + "</nobr>";
//		}

//	}
//	else
//	{
//	//alert("hidden");
//	}


//		
//		//return headTd.offsetWidth;
//	}
//	
//	
//	function setHeadDivLeft(div)//
//	{
//		var tab = div;
//		while(tab.tagName!='TABLE')tab=tab.parentElement;
//		var headDiv = tab.rows[0].firstChild;
//		while(headDiv.tagName!='DIV')headDiv=headDiv.firstChild;
//		headDiv.scrollLeft = div.scrollLeft;
//	}
//	function initHeadTdWidth()
//	{
//	
//		var tab = document.getElementById(stableID);
//		tab=tab.parentElement;
//		while(tab.tagName!='TABLE')tab=tab.parentElement;//最外層的Table
//		var hTab = tab.rows[0].firstChild;//頭Table
//		while(hTab.tagName!='TABLE')hTab=hTab.firstChild;//找到真正的列頭Table
//		
//		for(var i=0;i<hTab.rows[0].cells.length;i++)
//		{ 
//		
//				
//		
//			var td = hTab.rows[0].cells[i],tmpWidth=0;
//			
//				td.bgColor = headTdColor;
//				td.style.borderLeftColor = '#eeeeee';
//				td.style.borderTopColor = '#eeeeee';
//				td.style.borderRightColor = '#666666';
//				td.style.borderBottomColor = '#666666';
//				td.onmousedown = beginResizeTd;
//				td.onmousemove = setTdCursor;
//				//td.style.display = element.rows[0].cells[i].style.display;
//				td.innerHTML = "<nobr>" + td.innerHTML + "</nobr>";
//				
//				
//				
//				if(document.getElementById(stableID).rows[0].cells[i].width)
//				{
//					td.width = document.getElementById(stableID).rows[0].cells[i].width;
//					td.style.width = document.getElementById(stableID).rows[0].cells[i].width;
//				}
//				else if(document.getElementById(stableID).rows[0].cells[i].style.width)
//				{
//					td.width = document.getElementById(stableID).rows[0].cells[i].style.width;
//					td.style.width = document.getElementById(stableID).rows[0].cells[i].style.width;
//				}
//				else
//				{
//					//默認寬為100
//					
//						td.width =100  //LIST.rows[0].cells[i].offsetWidth;
//						td.style.width =100 //LIST.rows[0].cells[i].offsetWidth;
//					
//				}
//		
//		}
//	
//		
//		
//	}
//	function initMainTdWidth()
//	{
//		if(document.getElementById(stableID).rows.length<=0) return;
//		for(var i=0;i<document.getElementById(stableID).rows[0].cells.length;i++)
//			//element.rows[0].cells[i].width = setTdWidth(element.rows[0].cells[i]);
//			setTdWidth(i);
//	}
//	function setMainTable()
//	{
//		var tab = document.getElementById(stableID);
//		tab=tab.parentElement;
//		while(tab.tagName!='TABLE')tab=tab.parentElement;//最外層的Table
//		var hTab = tab.rows[0].firstChild;//頭Table
//		while(hTab.tagName!='TABLE')hTab=hTab.firstChild;
//		document.getElementById(stableID).style.position = 'relative';
//		document.getElementById(stableID).style.top = -hTab.offsetHeight;
//		document.getElementById(stableID).width = hTab.offsetWidth;
//		document.getElementById(stableID).style.width = hTab.offsetWidth;
//	}
//	function initElement()
//	{
//		var html = "<table style='table-layout:fixed;height:300px;width:98%;'  border=0  cellspacing=1 cellpadding=0>";
//		html += "<tr><td style='height:expression(firstChild.offsetHeight+1);layout:fixed;'>";
//		html += "<div style='overflow:hidden;width:expression(parentElement.offsetWidth-18);position:relative;'>";
//		html += "<table border=0   cellspacing=1 cellpadding=0 style='border-collapse:collapse;table-layout:fixed'>";
//		html += document.getElementById(stableID).rows[0].outerHTML;	// + head
//		html += "</table>";
//		html += "</div>";
//		html += "</tr></td><tr><td style='height:*;layout:fixed;'>";
//		html += "<div id='main' style='overflow:scroll;width:expression(parentElement.offsetWidth);height:100%;' onscroll='setHeadDivLeft(this)'>";
//		html += document.getElementById(stableID).outerHTML;// + element
//		html += "</div>";
//		html += "</td></tr></table>";
//		
//		document.getElementById(stableID).outerHTML = html;
//		//element.document.close();

//		initHeadTdWidth();		//設置表格頭的寬
//		//element.deleteRow(0);	//刪除顯示內容的第一行
//		initMainTdWidth();		//設置內容表格的寬
//		setMainTable(); 		//設置內容表格的位置
//	}
//	//element.width = '';
//	document.getElementById(stableID).style.wordBreak = "break-all";
//	document.getElementById(stableID).style.tableLayout = 'fixed';
//	initElement();
//	////////////////////////// 下面內容設置可以使用鼠標調整列寬 //////////////////////////////////////////////
//	document.getElementById(stableID).document.attachEvent('onmousemove',resizeTd);
//	document.getElementById(stableID).document.attachEvent('onmouseup',endResizeTd);
//	//function document.getElementById(stableID).onselectstart(){if(document.old)return false;}
//	
//	function setTdCursor()
//	{
//	
//		var td = event.srcElement;
//		if(event.offsetX>td.offsetWidth-10 || document.old)
//			td.style.cursor = "col-resize";
//		else
//			td.style.cursor = "auto";
//		
//			
//	}
//	
//	//function resizeTimeOut(){endResizeTd()}
//	function beginResizeTd()
//	{
//	
//		var td = event.srcElement;
//		//if(event.offsetX<td.offsetWidth-10) return;
//		document.tdDown = true;
//		var tab = td;while(tab.tagName!="TABLE")tab=tab.parentElement;
//		document.old=
//		{
//			"td":td,
//			"tdWidth":td.offsetWidth,
//			"downX":event.x,
//			"table":tab,
//			"tableWidth":tab.offsetWidth
//		}
//		
//		//setTimeout("resizeTimeOut()",10000);
//	}
//	function resizeTd()
//	{
//	
//		if(!document.old || document.old["td"].tagName!="TD") return;
//		if(document.tdDown)
//		{
//			document.body.style.cursor = "col-resize";
//			var offsetWidth = (event.x-document.old["downX"]);
//			var newWidth = document.old["tdWidth"] + offsetWidth;
//			if(newWidth<=5) return;
//			document.old["td"].width =  newWidth;
//			document.old["td"].style.width = newWidth;
//			document.old["table"].width = document.old["tableWidth"]+offsetWidth;
//			document.old["table"].style.width = document.old["tableWidth"]+offsetWidth;

//			setTdWidth(document.old["td"].cellIndex);
//			setMainTable();
//			//保存列寬
//			//var index = document.old["td"].cellIndex;
//			//divTdWidth.getElementsByTagName("INPUT")[index].value = newWidth;
//		}
//		else
//		{
//			document.body.style.cursor = "auto";
//			document.old = null;
//		}
//		
//	}
//	function endResizeTd()
//	
//	{
//	
//		document.old = null;
//		document.tdDown = false;
//		document.body.style.cursor = "auto";
//		
//	}
//	////////////////////// 下面設置點擊表格時改變行背景色的事件 ////////////////////////////
//	/*
//	LIST.attachEvent('onclick',doDataGridClick);
//	LIST.attachEvent('onmouseover',doDataGridMouseOver);
//	LIST.attachEvent('onmouseout',doDataGridMouseOut);
//	function doDataGridClick()
//	{
//		var td = event.srcElement;
//		if(td.tagName!="TD" && td.parentElement.tagName!="TD") return ;
//		var tr = td;
//		while(tr.tagName!="TR") tr = tr.parentElement;
//		if(LIST.oldTr!=null)
//			LIST.oldTr.bgColor = LIST.oldColor;
//		LIST.oldTr = tr;
//		LIST.oldColor = tr.oldColor;//tr.bgColor;
//		tr.bgColor = selectBgColor;
//		window.status = '選中行：第'+ (LIST.oldTr?LIST.oldTr.rowIndex:'-1') +'行--當前行：第'+ tr.rowIndex +'行';
//	}
//	function doDataGridMouseOver()
//	{
//		var td = event.srcElement;
//		if(td.tagName!="TD" && td.parentElement.tagName!="TD") return ;
//		var tr = td;
//		while(tr.tagName!="TR") tr = tr.parentElement;
//		if(tr == LIST.oldTr) return;
//		tr.oldColor = tr.bgColor;
//		tr.bgColor = trOverColor;
//		window.status = '選中行：第'+ (LIST.oldTr?LIST.oldTr.rowIndex:'-1') +'行--當前行：第'+ tr.rowIndex +'行';
//	}
//	function doDataGridMouseOut()
//	{
//		var td = event.srcElement;
//		if(td.tagName!="TD" && td.parentElement.tagName!="TD") return ;
//		var tr = td;
//		while(tr.tagName!="TR") tr = tr.parentElement;
//		if(tr == LIST.oldTr) return;
//		tr.bgColor = tr.oldColor;
//	}
//	*/
//}

