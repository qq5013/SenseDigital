var fixedColsWidth = new Object();
fixedColsWidth.Detail1 = 0;
fixedColsWidth.Detail2 = 0;
fixedColsWidth.Detail3 = 0;
fixedColsWidth.Detail4 = 0;
fixedColsWidth.Detail5 = 0;
// author: never-online
// web: never-online.net
// 获取request
var calendarresize = new Array(); //日期控件定位
var request = {
    QueryString: function (val) {
        var uri = window.location.search;
        var re = new RegExp("" + val + "\=([^\&\?]*)", "ig");
        return ((uri.match(re)) ? (uri.match(re)[0].substr(val.length + 1)) : null);
    },
    QueryStrings: function () {
        var uri = window.location.search;
        var re = /\w*\=([^\&\?]*)/ig;
        var retval = [];
        while ((arr = re.exec(uri)) != null)
            retval.push(arr[0]);
        return retval;
    },
    setQuery: function (val1, val2) {
        var a = this.QueryStrings();
        var retval = "";
        var seted = false;
        var re = new RegExp("^" + val1 + "\=([^\&\?]*)$", "ig");
        for (var i = 0; i < a.length; i++) {
            if (re.test(a[i])) {
                seted = true;
                a[i] = val1 + "=" + val2;
            }
        }
        retval = a.join("&");
        return "?" + retval + (seted ? "" : (retval ? "&" : "") + val1 + "=" + val2);
    }
}

function mykeyDown() {
    if (event.keyCode == 13) {
        var e = document.activeElement;
        if (e.id == "txtMemo" ||  e.id == "txtReplyContent" || e.id == "txtCardNo" || e.id=="txtDescription")
            return true;
        else {
            event.keyCode = 9;
        }
    }
}
document.onkeydown = mykeyDown;
//鼠標移動 改變顏色
function HandleMouseEvent(evt) {
    var el = event.srcElement;
    var c;
    if (el.tagName == "TD") {
        switch (evt) {
            case "over":
                if (el.parentElement.className != "bottomtable") {
                    c = el.parentElement.className;
                    el.parentElement.className = "table_titlebgcolor";
                }
                break;
            case "out":
                if (el.parentElement.className != "bottomtable") {
                    el.parentElement.className = "table_bordercolor";
                }
                break;
            case "dblclick":
                break;
        }
    }
}
function Exit() {
    var message = getResource("Question_Exit");
    if (confirm(message))
        window.parent.delTab();
    return false;
}
var FormID = request.QueryString("FormID");
//新增修改時調用
function Edit(op) {
    var ID = "";
    var pageID = "";

    if (FormID.substring(0,3) == "Sys")
        pageID = FormID.substring(4);
    else
        pageID = FormID.substring(3);
    var ctlName = "txt" + pageID + "ID";
    if (FormID.indexOf("EnterpriseCategory") >= 0)
        ctlName = "txt" + FormID.substring(13) + "ID";
    else if(FormID=="EP_Enterprise")
        ctlName = "ddlEnterpriseID";

    if (op)
        ID = document.getElementById(ctlName).value;    

    location.href = pageID + "Edit.aspx?FormID=" + FormID + "&ID=" + ID;
    return false;
}
//新增修改時調用
function BillEdit(op) {
    var BillID = "";
    var pageID = "";

    if (FormID.substring(0, 3) == "Sys")
        pageID = FormID.substring(4);
    else
        pageID = FormID.substring(3);
    var ctlName = "txtBillID";    

    if (op)
        BillID = document.getElementById(ctlName).value;

    location.href = pageID + "Edit.aspx?FormID=" + FormID + "&BillID=" + BillID;
    return false;
}
function print() {
    //    var ID = "";
    //    var pageID = "";

    //    if (FormID.substring(0, 3) == "Sys")
    //        pageID = FormID.substring(4);
    //    else
    //        pageID = FormID.substring(3);
    //    var ctlName = "txt" + pageID + "ID";
    //    if (FormID.indexOf("EnterpriseCategory") >= 0)
    //        ctlName = "txt" + FormID.substring(13) + "ID";

    //    if (op)
    //        ID = document.getElementById(ctlName).value;
    //要先運行 writeJsvar
    location.href = "../rpt/frmRpt.aspx?FormID=" + FormID + "&cnKey=" + cnKey + "&ID=" + oldID + "&W=" + document.body.clientWidth + "&H=" + document.body.clientHeight;
    return false;
}
function Delete() {
    var allInput = document.getElementsByTagName("input");

    for (var i = 0; i < allInput.length; i++) {

        if (allInput[i].type == "checkbox") {
            if (allInput[i].checked) {
                var message = getResource("Question_Delete");
                return confirm(message);
                break;
            }
        }
    }
    alert(getResource("Select_Delete"));
    return false;

}
function ViewDelete() {
    var message = getResource("Question_Delete");
    return confirm(message);
}
function Cancel() {
    var message = getResource("Question_Cancel");
    if (confirm(message)) {
        var url = location.href;
        if (url.substring(url.length - 1, url.length) != "=")
            location.href = url.replace("Edit", "View");
        else {
            if (FormID.indexOf("EnterpriseCategory") >= 0)
                location.href = url.replace("yEdit", "ies");
            else
                location.href = url.replace("Edit", "s");
        }
        //history.go(-1);

    }
    return false;
}
//返回
function Back() {
    //    var message = getResource("Question_Return");
    //    if (confirm(message)) {
    var url = location.href;
    if (FormID.indexOf("EnterpriseCategory") >= 0)
        location.href = url.replace("yView", "ies");
    else if (FormID.indexOf("ProductCheck") >= 0 || FormID.indexOf("ProductResumeCheck") >= 0 || FormID.indexOf("ProductLogisticsCheck") >= 0)
        location.href = url.replace("Check", "Checks");
    else if (FormID.indexOf("EP_ProductNoCheck") >= 0 || FormID.indexOf("EP_ProductResumeNoCheck") >= 0)
        location.href = url.replace("View", "sNoCheck");
    else if (FormID.indexOf("EP_ProductLogisticsNoCheck") >= 0)
        location.href = url.replace("View", "NoCheck");
    else if (FormID.indexOf("EP_ProductLogistics") >= 0)
        location.href = url.replace("sView", "s");
    else if(FormID.indexOf("BU_UserToRole")>=0)
        location.href = url.replace("View", "s");
    else if (FormID.indexOf("BU_User") >= 0 || FormID.indexOf("EP_User") >= 0)
        location.href = url.replace("Edit", "");
    else
        location.href = url.replace("View", "s");
    //    }
    return false;
}
function getResource(keyName) {
    var temp;
    $.ajax({
        async: false,
        type: "Post",
        url: getRootPath() + "/WebService/BaseService.asmx/strResource",
        data: "{'keyName':'" + keyName + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            //debugger;
            //你可以 alert(data.d)看数据返回的格式
            data = data.d;
            if (data.length > 0) {
                temp = data;
            }
        }
    });
    return temp;
}
function selectAll(ctlName, bool) {
    var ctl = document.getElementById(ctlName);
    var checkbox = ctl.getElementsByTagName('input'); 
    
    for (var i = 0; i < checkbox.length; i++) {
        if (checkbox[i].type == 'checkbox') {
            checkbox[i].checked = bool;
        }
    }
}
function Search() {
    if(trim(document.getElementById("txtContent").value)=="") {
        var message = getResource("SearchContent_NotNull");
        alert(message);
    	document.getElementById("txtContent").focus();
    	return false;
    }
    document.getElementById("HiddenField1").value = document.getElementById("ddlFieldName").value + " like N'%" + trim(document.getElementById("txtContent").value).replace("'", "") + "%'";

}
function SearchDialog(height) {
    var strReturn = window.showModalDialog('../../SearchTemp.aspx?FormID=' + FormID, window, 'dialogHeight:' + height + 'px;dialogwidth:590px;help:no;scroll:no;resizable:yes');
    if (strReturn != null && strReturn != "false")
        document.getElementById("HiddenField1").value = strReturn;
    else
        return false;
}

function GoPageNo()
{
	if(trim(document.getElementById("txtPageNo").value)=="") {
	    var message = getResource("KeyIn_Pages");
	    alert(message);
		//alert("請輸入跳轉頁數!");
		document.getElementById("txtPageNo").focus();
		return false;
	}
	else if(parseInt(document.getElementById("txtPageNo").value)==NaN || parseInt(document.getElementById("txtPageNo").value)==0) {
	    var message = getResource("KeyIn_RightPage");
	    alert(message);
		//alert("請輸入正確的頁碼!");
		document.getElementById("txtPageNo").value = "";
		document.getElementById("txtPageNo").focus();
		return false;
	}
	document.getElementById('btnToPage').click();
}
function trim(theData)
{
	var checkStr = theData;
	if (checkStr==null)
		return ;
	if (checkStr=="")
		return "";
	var theStrLength=0;
	while (checkStr.charAt(0)==" ")
		checkStr=checkStr.substring(1,checkStr.length);
	theStrLength=checkStr.length;
	while (checkStr.charAt(theStrLength-1)==" ")
	{
		checkStr=checkStr.substring(0,checkStr.length-1);
		theStrLength=checkStr.length
	}	
	return checkStr;
}
//顯示(單/多)選對話框 //返回值包含﹒欄位名稱+內容;傳回所有值
//sysid.length>=2;  第一位表示(0:單選／1:多選);第二位表示(系統sysid);
//PermissionID:系統表單id
//'@|$' 表示 <<|>>; @,# 表示 <<,>> @^&  表示 <<'>>
function showdialog(FormID, strWhere, strPath) {
    var returnvalue;
    var Where;

    if (strWhere == null)
        Where = '';
    else
        Where = '&Where=' + strWhere;
    if (strPath == null)
        returnvalue = window.showModalDialog('~/TempPage.aspx?FormID=' + FormID + Where, window, 'DialogHeight:560px;DialogWidth:600px;help:no;scroll:auto;Resizable:yes');
    else
        returnvalue = window.showModalDialog(strPath + 'TempPage.aspx?Sysid=' + Sysid + '&PermissionID=' + PermissionID + Where, window, 'DialogHeight:560px;DialogWidth:600px;help:no;scroll:auto;Resizable:yes');
    //alert(returnvalue);
    if (returnvalue != "undefined" && returnvalue != null) {
        if (returnvalue != "") {
            var t1 = returnvalue.replace(/&nbsp;/g, "");
            var t2 = t1.replace(/&quot;/g, '"');
            var t3 = t2.replace(/&lt;/g, '<');
            var t4 = t3.replace(/&gt;/g, '>');
            var t5 = t4.replace("@^&", "'");
            var t6 = t5.replace(/&amp;/g, '&');
            return t6.split("@,#");
        }
        else
            return null;
    }
    else {
        return null;
    }
}
function isNumber(theData)
{
  var checkOK = "0123456789-.";
  var checkStr = theData;
  var allValid = true;
  var decPoints = 0;
  var allNum = "";
  if (checkStr=="")
  	return true;
  for (i = 0;  i < checkStr.length;  i++)
  {
    ch = checkStr.charAt(i);
    for (j = 0;  j < checkOK.length;  j++)
      if (ch == checkOK.charAt(j))
        break;
    if (j == checkOK.length)
    {
      allValid = false;
      break;
    }
    if (ch == ".")
    {
      allNum += ".";
      decPoints++;
    }
    else if (ch != ",")
      allNum += ch;
  }
  if (!allValid)
    return false;
  if (decPoints > 1) 
    return false;
    
  return true;
}

function replaceChar(theData,theOrginChar,theReplaceChar)
 {
 	if (theData=="" || theData==null)
 		return theData;
 	while (theData.indexOf(theOrginChar)!=-1)
 	{
 		theData=theData.replace(theOrginChar,theReplaceChar); 
 	}
 
 	return theData;
 	
 }
 /*
 document.attachEvent("onselectstart", function()
{
	var thetag=event.srcElement.tagName;
	if (thetag=="INPUT" || thetag=="TEXTAREA")
		return true;
	else
		return false;
});
document.attachEvent("oncontextmenu", function()
{
	var thetag=event.srcElement.tagName;
	if (thetag=="INPUT" || thetag=="TEXTAREA")
		return true;
	else
		return false;
});
*/
function setItemBackground(obj)
{
    var curTR = document.all.tags("tr");
    
    for(var i=1;i<curTR.length;i++)
    {
        if (curTR[i].style.backgroundColor == "yellow")
        {
            curTR[i].style.backgroundColor = "white";
        }
    }
    obj.style.backgroundColor = "yellow";
    
}
function GetItem(FormId,SelectOption,strWhere,objId,objIdName)
{
	var strReturn = window.showModalDialog('../TempGetItem.aspx?FormId=' + FormId + '&SelectOption='+SelectOption+'&strWhere='+strWhere,window,'DialogHeight:465px;DialogWidth:600px;help:no;scroll:no');			
	if(strReturn!=null)
	{				
		var parts = strReturn.split("]");
		document.getElementById(objId).value = parts[0].substring(parts[0].indexOf(":")+1);
		document.getElementById(objIdName).value = parts[1].substring(parts[1].indexOf(":")+1);
	}
}
function regInput(obj, reg, inputStr)
{
	var docSel= document.selection.createRange()
	if (docSel.parentElement().tagName != "INPUT" || docSel.parentElement().readOnly == true)
		return false
	oSel = docSel.duplicate()
	oSel.text = ""
	var srcRange= obj.createTextRange()
	oSel.setEndPoint("StartToStart", srcRange)
	var str = oSel.text + inputStr + srcRange.text.substr(oSel.text.length)
	return reg.test(str)
}
function tofloat(f,dec) 
{ 
	if(dec<0) 
		return "Error:dec<0!"; 
	result = parseInt(f)+(dec==0?"":"."); 
	f-=parseInt(f); 
	if(f==0) 
		for(i=0;i<dec;i++) 
			result+='0'; 
	else 
	{ 
		for(i=0;i<dec;i++) 
			f*=10; 
		result+=parseInt(Math.round(f)); 
	} 
	return result; 
} 
function HandleMouseEvent(evt)
{
	var el=event.srcElement;
	if(el.tagName=="TD" && el.parentElement.style.backgroundColor!="#ccccff")
	{
		switch(evt)
		{
			case "over":
			el.parentElement.style.backgroundColor="#c1d2ee";
			el.parentElement.style.color="#003399";
			break;
			case "out":					
			el.parentElement.style.backgroundColor="white";
			el.parentElement.style.color="#003399";
			break;
			case "dblclick":
			el.parentElement.style.backgroundColor="red";
			break;
		}
	}
}
function SetInputDisable(id,Y)
{
	document.getElementById(id).disabled = Y;
}
function document_onkeydown() 
{
	if( window.event.keyCode == 13  ) 
	{
		window.event.keyCode=9;
	}
}

function china_id_check(id,id_f)
{
	if((id.length < 15) || (id.length > 18))
	{
		return "";
		alert( "您所輸入的身份證字號必須在15碼～18碼之間！" ) ;
		id_f.focus();
			return(false);
	}

	for(var i = 0; i < id.length; i++)
	{
		var c = id.charAt(i);
		if(!((c >= "a" && c <= "z")||(c>="A" && c<="Z")||(c >= "0" && c <= "9")))
		{
			alert( "身份證字號輸入不正確！" ) ;
			id_f.focus();
			return(false);
		}
  	}
	return true;
}														

function Taiwan_id_check(id,id_f) 
{ 
	var LegalID = "0123456789" 
	var LegalUserName = "0123456789abcdefghijklmnopqrstuvwxyz_" 
	var value = 0; 
	var sId=id; 
	if (id_f.value=="a123456789"){
		alert("身份證字號不正確！");
		id_f.focus();
		return(false);
	}
	if(sId.length!=10) {
		alert( "身份證字號不正確！" ) ;
		id_f.focus();
		return( false ) ;
	}else { 
		if((sId.charAt(0)=='A') || (sId.charAt(0)=='a')) value=10 
		else if((sId.charAt(0)=='B') || (sId.charAt(0)=='b')) value=11 
		else if((sId.charAt(0)=='C') || (sId.charAt(0)=='c')) value=12 
		else if((sId.charAt(0)=='D') || (sId.charAt(0)=='d')) value=13 
		else if((sId.charAt(0)=='E') || (sId.charAt(0)=='e')) value=14 
		else if((sId.charAt(0)=='F') || (sId.charAt(0)=='f')) value=15 
		else if((sId.charAt(0)=='G') || (sId.charAt(0)=='g')) value=16 
		else if((sId.charAt(0)=='H') || (sId.charAt(0)=='h')) value=17 
		else if((sId.charAt(0)=='J') || (sId.charAt(0)=='j')) value=18 
		else if((sId.charAt(0)=='K') || (sId.charAt(0)=='k')) value=19 
		else if((sId.charAt(0)=='L') || (sId.charAt(0)=='l')) value=20 
		else if((sId.charAt(0)=='M') || (sId.charAt(0)=='m')) value=21 
		else if((sId.charAt(0)=='N') || (sId.charAt(0)=='n')) value=22 
		else if((sId.charAt(0)=='P') || (sId.charAt(0)=='p')) value=23 
		else if((sId.charAt(0)=='Q') || (sId.charAt(0)=='q')) value=24 
		else if((sId.charAt(0)=='R') || (sId.charAt(0)=='r')) value=25 
		else if((sId.charAt(0)=='S') || (sId.charAt(0)=='s')) value=26 
		else if((sId.charAt(0)=='T') || (sId.charAt(0)=='t')) value=27 
		else if((sId.charAt(0)=='U') || (sId.charAt(0)=='u')) value=28 
		else if((sId.charAt(0)=='V') || (sId.charAt(0)=='v')) value=29 
		else if((sId.charAt(0)=='X') || (sId.charAt(0)=='x')) value=30 
		else if((sId.charAt(0)=='Y') || (sId.charAt(0)=='y')) value=31 
		else if((sId.charAt(0)=='W') || (sId.charAt(0)=='w')) value=32 
		else if((sId.charAt(0)=='Z') || (sId.charAt(0)=='z')) value=33 
		else if((sId.charAt(0)=='I') || (sId.charAt(0)=='i')) value=34 
		else if((sId.charAt(0)=='O') || (sId.charAt(0)=='o')) value=35 
		else 
		{
			alert( "身份證字號不正確！" ) ;
			id_f.focus();
			return( false ) ;
		}
	} 
	
	value = Math.floor(value/10) + (value%10)*9 + 
	parseInt(sId.charAt(1))*8+ 
	parseInt(sId.charAt(2))*7+ 
	parseInt(sId.charAt(3))*6+ 
	parseInt(sId.charAt(4))*5+ 
	parseInt(sId.charAt(5))*4+ 
	parseInt(sId.charAt(6))*3+ 
	parseInt(sId.charAt(7))*2+ 
	parseInt(sId.charAt(8))+ 
	parseInt(sId.charAt(9)) ; 
	value = value % 10 ; 
	if(value!=0){
		alert( "身份證字號不正確！" ) ;
		id_f.focus();
		return( false ) ;				
	}

	var i; 
	var c; 
	for (i = 1; i < sId.length; i++) { 
		c = sId.charAt(i); 
		if (LegalID.indexOf(c) == -1){
			alert( "身份證字號不正確！" ) ;
			id_f.focus();
			return( false ) ;
		}
	} 
	return (true) ;
}
//js获取网站根路径(站点及虚拟目录)，获得网站的根目录或虚拟目录的根地址
function getRootPath() {
    var strFullPath = window.document.location.href;
    var strPath = window.document.location.pathname;
    var pos = strFullPath.indexOf(strPath);
    var prePath = strFullPath.substring(0, pos);
    var postPath = strPath.substring(0, strPath.substr(1).indexOf('/') + 1);
    return (prePath + postPath);
}

function toNumber(num) {
    var v = "" + num;
    return parseFloat(v.replace(/\,/g, ''));
}