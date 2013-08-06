﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Upload.aspx.cs" Inherits="WebUI_Label_Upload" %>
<%@ Register Src="~/Controls/Uploadify.ascx" TagPrefix="Upload" TagName="Uploadify" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="~/Styles/Main.css" type="text/css" rel="stylesheet" /> 
    <link href="~/Styles/op.css" type="text/css" rel="stylesheet" /> 
    <link href="~/JScript/uploadify/uploadify.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Commons.js") %>'></script>
    <script src='<%=ResolveUrl("~/Scripts/JQuery/jquery-1.8.3.min.js") %>' type="text/javascript"></script>
    <script src='<%=ResolveUrl("~/JScript/uploadify/jquery.uploadify.min.js") %>' type="text/javascript"></script>
    
     <script type="text/javascript">
         var formID = "<% =FormID%>";
         var styleFlag = "<% =StyleFlag%>";
         var styleID = "<% =StyleID%>";
         var fileType = '*.jpg';
         var queueSizeLimit = 1 ;
         var multi = false;         

         jQuery(document).ready(function () {
             jQuery("#file_upload").uploadify({
                 'buttonImage': '../../JScript/uploadify/browsebtn2.jpg',
                 'auto': false,
                 'multi': multi,
                 'successTimeout': 99999,
                 'swf': '../../JScript/uploadify/uploadify.swf',
                 'queueID': 'uploadfileQueue',
                 'uploader': '../Upload/StyleHandler.ashx?FormID=' + formID + '&StyleFlag=' + styleFlag + '&StyleID=' + styleID,
                 'fileSizeLimit': '0',
                 'queueSizeLimit': queueSizeLimit,
                 'progressData': 'speed',
                 'overrideEvents': ['onDialogClose'],
                 'fileTypeExts': '*.rar;*.zip;*.7z;;*.jpge;*.gif;*.png',
                 'fileTypeExts': fileType,
                 'onSelectError': function (file, errorCode, errorMsg) {
                     switch (errorCode) {
                         case -100:
                             alert("<%=Resources.Resource.Upload_FilesOver %>" + jQuery('#file_upload').uploadify('settings', 'queueSizeLimit') + "<%=Resources.Resource.Upload_Files %>！");
                             break;
                         case -110:
                             alert("<%=Resources.Resource.Upload_Document %> [" + file.name + "] <%=Resources.Resource.Upload_OverFileSize %>" + jQuery('#file_upload').uploadify('settings', 'fileSizeLimit') + "<%=Resources.Resource.Upload_FileSize %>！");
                             break;
                         case -120:
                             alert("<%=Resources.Resource.Upload_Document %> [" + file.name + "] <%=Resources.Resource.Upload_FileSizeError %>！");
                             break;
                         case -130:
                             alert("<%=Resources.Resource.Upload_Document %> [" + file.name + "] <%=Resources.Resource.Upload_FileTypeError %>！");
                             break;
                     }
                 },
                 'onClearQueue': function (queueItemCount) {
                     alert("<%=Resources.Resource.Upload_Cancel %>");
                     return;
                 },
                 'onQueueComplete': function (queueData) {
                     //debugger;
                     //InsertUploadRecord(queueData.files.SWFUpload_0_0.name);
                     //InsertUploadRecord(fileName);
                     alert("<%=Resources.Resource.Upload_Success %>！");
                     return;
                 }
             });
         });
         function InsertUploadRecord(fileName) {
             var UserName = '<%= Session["User"]%>';
             var UseUnit = 0;
             if ($("#chk1").attr("checked") == "checked")
                 UseUnit += 1;
             if ($("#chk2").attr("checked") == "checked")
                 UseUnit += 2;

             if (UseUnit == 0) {
                 alert("<%=Resources.Resource.Upload_CheckUseUnit %>");
                 return;
             }
             var fileType = 0;
             var fileDesc = $("#EddlFileDesc").val();
             if ($("input[name='rbFileType']:checked").val() == "rbFileType1") {
                 fileType = 0;
             }
             else {
                 fileType = 1;
             }

             var memo = $("#txtMemo").val();

             $.ajax({
                 async: false,
                 type: "Post",
                 url: getRootPath() + "/WebService/BaseService.asmx/UpdateImagePath",
                 data: "{'UserName':'" + escape(UserName) + "','UseUnit':'" + UseUnit + "','FileType':'" + fileType + "','FileName':'" + escape(fileName) + "','FileDesc':'" + escape(fileDesc) + "','Memo':'" + escape(memo) + "'}",
                 contentType: "application/json; charset=utf-8",
                 dataType: "json",
                 success: function (data) {
                     //debugger;
                     //你可以 alert(data.d)看数据返回的格式
                     temp = data.d;
                 }
             });
         }
         function UploadRecord() {

             window.showModalDialog('UploadRecord.aspx?FormID=<% =FormID%>', window, 'dialogHeight:320px;dialogWidth:800px;toolbar=no,status=yes,resizable=no');
             return false;
         }
         function Back() {
             //history.go(-1);
             location.href = "StyleView.aspx?FormID=" + formID + "&ID=" + styleID;
             return false;
         }
    </script>
   
    <style type="text/css">
        #uploadfileQueue {
                background-color: #FFF;
                border-radius: 3px;
                box-shadow: 0 1px 3px rgba(0,0,0,0.25);
                margin-bottom: 10px;
                overflow: auto;
                padding: 5px 10px;
                width: 300px;
                height:263px;
        }
        .uploadify-button {
            background-color: transparent;
            border: none;
            padding: 0;
        }
        .uploadify:hover .uploadify-button {
            background-color: transparent;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <table style="width: 100%; height: 20px;" class="OperationBar">
                <tr>

                    <td align="right">
                        <asp:Button ID="btnExit" runat="server" Text="離開" OnClientClick="return Exit()" CssClass="ButtonExit" />
                        <asp:Button ID="btnBack" runat="server" Text="返回" OnClientClick="return Back()" 
                            CssClass="ButtonBack" />
                    </td>
                </tr>
            </table>
        <table class="maintable" cellspacing="0" cellpadding="0" width="100%" align="center" bordercolor="#ffffff"
				border="1" >
                    <tr>
						<td class="smalltitle" align="center" width="10%" ><asp:Literal ID="lblBillID" Text="上傳人員"
                                runat="server"></asp:Literal>
                        </td>
						<td width="60%" >&nbsp;
								<asp:textbox id="txtUploadUserName" runat="server" CssClass="TextRead" 
                                Width="30%" MaxLength="10" ReadOnly="true"></asp:textbox>
                                &nbsp; 

                        		<asp:textbox id="txtStyleFlag" runat="server" CssClass="TextRead" 
                                Width="0" MaxLength="10" ReadOnly="true"></asp:textbox>   
                        </td>                        
					</tr>					
                    <tr>
						<td class="smalltitle" align="center" width="10%" >
                         <asp:Literal ID="ltlStyleID" Text="款式編號"
                                runat="server"></asp:Literal>
                        </td >
						<td>&nbsp;
							<asp:textbox id="txtStyleID" runat="server" CssClass="TextRead" Width="30%" MaxLength="20" ReadOnly="true"></asp:textbox>
                          &nbsp;<asp:textbox id="txtStyleName" runat="server" CssClass="TextRead" Width="63%" MaxLength="20" ReadOnly="true"></asp:textbox>
                        </td>
                        
					</tr>
                               
					
			</table>
        
        <input type="file" id="file_upload" name="file_upload" />
        <div id="uploadfileQueue"></div>
        
        <p>

        <asp:Button ID="btnUpload" runat="server" Text="上傳"  CssClass="but" Width="70px"
            OnClientClick="javascript:jQuery('#file_upload').uploadify('upload','*');return false" 
            meta:resourcekey="btnUploadResource1" />
        &nbsp;<asp:Button ID="btnCancel" runat="server" Text="取消上傳" CssClass="but" Width="70px"
            OnClientClick="javascript:jQuery('#file_upload').uploadify('cancel','*');return false" 
            meta:resourcekey="btnCancelResource1" />
    </p>
    </form>
</body>
</html>
