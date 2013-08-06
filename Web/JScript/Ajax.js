function IsExists(FormID, ID,cnKey) {
    var temp;
    if (cnKey == null)
        cnKey = "";
    $.ajax({
        async: false,
        type: "Post",
        url: getRootPath() + "/WebService/BaseService.asmx/IsExists",
        data: "{'FormID':'" + FormID + "','ID':'" + escape(ID) + "','cnKey':'" + cnKey + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            //debugger;
            //你可以 alert(data.d)看数据返回的格式
            temp = data.d;
        }
    });
    return temp;
}
function IsFlagExists(FormID, Flag, ID, cnKey) {
    var temp;
    if (cnKey == null)
        cnKey = "";
    $.ajax({
        async: false,
        type: "Post",
        url: getRootPath() + "/WebService/BaseService.asmx/IsFlagExists",
        data: "{'FormID':'" + FormID + "','Flag':'" + Flag + ",'ID':'" + escape(ID) + "','cnKey':'" + cnKey + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            //debugger;
            //你可以 alert(data.d)看数据返回的格式
            temp = data.d;
        }
    });
    return temp;
}
function IsExistsByFilter(FormID, EnterpriseID,ID, cnKey) {
    var temp;
    if (cnKey == null)
        cnKey = "";
    $.ajax({
        async: false,
        type: "Post",
        url: getRootPath() + "/WebService/BaseService.asmx/IsExistsByFilter",
        data: "{'FormID':'" + FormID + "','EnterpriseID':'" + escape(EnterpriseID) + "','ID':'" + escape(ID) + "','cnKey':'" + cnKey + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            //debugger;
            //你可以 alert(data.d)看数据返回的格式
            temp = data.d;
        }
    });
    return temp;
}
function ConfirmPwd(UserName, OldPwd, cnKey) {
    var temp;
    if (cnKey == null)
        cnKey = "";
    $.ajax({
        async: false,
        type: "Post",
        url: getRootPath() + "/WebService/BaseService.asmx/ConfirmPwd",
        data: "{'UserName':'" + UserName + "','Password':'" + escape(OldPwd) + "','cnKey':'" + cnKey + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            //debugger;
            //你可以 alert(data.d)看数据返回的格式
            temp = data.d;
        }
    });
    return temp;
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

var webserverUrl = getRootPath() + "/WebService/BaseService.asmx";
//var strCompanyID = "";
//區分大小
function jsAjax(code, strpara) {    
    //?建异步?象
    var xmlhttp = createxmlhttp();
    if (xmlhttp == null) {
        alert("你的??器不支持 XMLHttpRequest");
        return;
    }
    //同步
    xmlhttp.open("POST", webserverUrl, false); //如果是异步通信方式(true)，客?机就不等待服?器的??
    xmlhttp.setRequestHeader("Content-Type", "application/soap+xml");
    xmlhttp.send(getData(code, strpara));

    return getResult(xmlhttp);
}

function jsAjaxCallBack(code, strpara, callback) {
    var xmlhttp = createxmlhttp();
    if (xmlhttp == null) {
        alert("你的??器不支持 XMLHttpRequest");
        return;
    }
    //异步
    xmlhttp.open("POST", webserverUrl, true); //如果是异步通信方式(true)，客?机就不等待服?器的??
    xmlhttp.setRequestHeader("Content-Type", "application/soap+xml");
    xmlhttp.send(getData(code, strpara));

    xmlhttp.onreadystatechange = function() {
        if (xmlhttp.readyState == 4 && xmlhttp.status == 200) {
            if (callback != null) callback(getResult(xmlhttp));
        }
    }
}

function createxmlhttp() {
    //?建异步?象
    var xmlhttp = window.XMLHttpRequest ? new window.XMLHttpRequest() : new ActiveXObject("Microsoft.XMLHttp");
    return xmlhttp;
}

function getResult(xmlhttp) {
    var result = ""; //result.toUpperCase
    result = xmlhttp.responseText;
    //        result.indexOf("<" + code + "Result>");
    //        result.indexOf("</" + code + "Result>");
    var type = result.substring(result.indexOf("<type>") + 6, result.indexOf("</type>"))
    if (result.indexOf("<data>") == -1) return "";
    result = result.substring(result.indexOf("<data>") + 6, result.indexOf("</data>"))
    result = $.trim(result).replace(/&amp;/g, '&');
    //    if (xmlhttp.responseXML.documentElement.nodeTypedValue == null) {
    //        //alert("11");
    //        //Chrome
    //        result = xmlhttp.responseText;
    ////        result.indexOf("<" + code + "Result>");
    //        //        result.indexOf("</" + code + "Result>");
    //        var type = result.substring(result.indexOf("<type>") + 6, result.indexOf("</type>"))
    //        result = result.substring(result.indexOf("<data>") + 6, result.indexOf("</data>"))
    //        
    //    } else {
    //        result = xmlhttp.responseXML.documentElement.nodeTypedValue;
    //    }

    //    System.Int16;
    //    System.Int32;
    //    System.Int64;
    //    System.DateTime;
    //    System.Decimal;
    //    System.Boolean;
    //    System.Byte;

    switch (type) {
        case "System.Byte":
        case "System.Int16":
        case "System.Int32":
        case "System.Int64":
            return parseInt(result);
        case "System.Decimal":
            return parseFloat(result);
        case "System.DateTime":
            return new Date(result);
        case "System.Boolean":
            return result == "True";
        case "ErrMsg":
            alert(result);
            return null;
        case "null":
            return null; 
    }
    return result;
}

function getData(code, strpara) {
    //在??我?拼接
    var data;
    data = '<?xml version="1.0" encoding="utf-8"?>';
    data = data + '<soap12:Envelope xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:soap12="http://www.w3.org/2003/05/soap-envelope">';
    data = data + '<soap12:Body>';
    data = data + '<' + code + ' xmlns="http://tempuri.org/" >';
    if (strpara != null) {
        data = data + strpara;
        //strpara 例
        //無參
        //code() 
        //null
        //有參
        //code(str1,str2)
        //<str1>你好1</str1>
        //<str2>你好2</str2>        
    }
    data = data + '</' + code + '>'
    data = data + '</soap12:Body>';
    data = data + '</soap12:Envelope>';
    return data;
}

function getPrintPrefix() {
    //window.location.pathname.split('/')[4].split('_')[0]
    var arr = window.location.pathname.split('/');
    var str = arr[arr.length - 1];
    if ($("#form1").attr("PrintPrefix") != null)
        return $("#form1").attr("PrintPrefix");
    return str.split('_')[0];
}
//轉換Xml for post to webserver
function parseXmlPara(para) {
    if (para == null) return "";
//    para._CompanyID = strCompanyID;
    //    var strpara = "";
    //    for (key in para) {
    //        //alert(key);
    //        if (strpara.length == 0)
    //            strpara = strpara + "\"" + key + "\":\"" + para[key] + "\"";
    //        else
    //            strpara = strpara + ",\"" + key + "\":\"" + para[key] + "\"";
    //    }
    return "<xmlpara>" + parseJosn(para) + "</xmlpara>";
}

function parseXmldtPara(para) {
    if (para == null) return "";
    var strpara = "";
    for (var i = 0; i < para.length; i++) {
        //for (key in para) {
            //alert(key);
            if (strpara.length == 0)
                strpara = strpara + "{" + getJosnRowStr(para[i]) + "}";
            else
                strpara = strpara + ",{" + getJosnRowStr(para[i]) + "}";
        //} 
    }
    return "<xmlpara>[" + strpara + "]</xmlpara>";
}

function parseJosnArray(para) {
    if (para == null) return null;
    var v = Array();
    for (var i = 0; i < para.length; i++) {
        for (key in para[i]) {
            //alert(key);
            v.push(para[i][key]);
        }
    }
    return v;
}

function parseJosn(para) {
//    if (para == null) return "";
//    var strpara = "";
//    for (key in para) {
//        //alert(key);
//        if (strpara.length == 0)
//            strpara = strpara + "\"" + key + "\":\"" + para[key] + "\"";
//        else
//            strpara = strpara + ",\"" + key + "\":\"" + para[key] + "\"";
//    }
    return "[{" + getJosnRowStr(para) + "}]";
}

function parsedttoJosn(para) {
    if (para == null) return "";
    var strpara = "";
    for (var i = 0; i < para.length; i++) {
        //for (key in para) {
        //alert(key);
        if (strpara.length == 0)
            strpara = strpara + "{" + getJosnRowStr(para[i]) + "}";
        else
            strpara = strpara + ",{" + getJosnRowStr(para[i]) + "}";
        //} 
    }
    return "[" + strpara + "]";
}

function getJosnRowStr(para) {
    if (para == null) return "";
    var strpara = "";
    for (key in para) {
        //alert(key);
        if (strpara.length == 0)
            strpara = strpara + "\"" + key + "\":\"" + para[key] + "\"";
        else
            strpara = strpara + ",\"" + key + "\":\"" + para[key] + "\"";
    }
    return strpara;
}
