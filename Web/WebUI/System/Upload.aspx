<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Upload.aspx.cs" Inherits="WebUI_System_Upload"  EnableEventValidation = "false"  %>
<%@ Register assembly="Js.PageControl" namespace="Js.PageControl" tagprefix="cc1" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />  
    <title></title>
    <link href="~/Styles/Main.css" type="text/css" rel="stylesheet" /> 
    <link href="~/Styles/op.css" type="text/css" rel="stylesheet" /> 
    <link href="~/JScript/uploadify/uploadify.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Commons.js") %>'></script>
    <script src='<%=ResolveUrl("~/Scripts/JQuery/jquery-1.8.3.min.js") %>' type="text/javascript"></script>
    <script src='<%=ResolveUrl("~/JScript/uploadify/jquery.uploadify.min.js") %>' type="text/javascript"></script>
    <script type="text/javascript" src='<%=ResolveUrl("~/JScript/DDL/JsDDL.js") %>'></script>
     <script type="text/javascript">
         var fileName = "";
         var fileType = 'Product.xls;Prod_resume.xls;Prod_Logistics.xls';
         var multi = false;
         var queueSizeLimit = 1;
         var obj;

         jQuery(document).ready(function () {
             if ($("input[name='rbFileType']:checked").val() == "rbFileType1")
                 fileType = '*.*';
             var UseUnit = 0;
             if ($("#chk1").attr("checked") == "checked")
                 UseUnit += 1;
             if ($("#chk2").attr("checked") == "checked")
                 UseUnit += 2;

             jQuery("#file_upload").uploadify({
                 'buttonImage': '../../JScript/uploadify/browsebtn2.jpg',
                 'auto': false,
                 'multi': multi,
                 'successTimeout': 99999,
                 'swf': '../../JScript/uploadify/uploadify.swf',
                 'queueID': 'uploadfileQueue',
                 'uploader': '../Upload/OtherUploadHandler.ashx?UseUnit=' + UseUnit + '&FileType=' + fileType,
                 'fileSizeLimit': '0',
                 'queueSizeLimit': queueSizeLimit,
                 'progressData': 'speed',
                 'overrideEvents': ['onDialogClose'],
                 //'fileTypeExts': '*.rar;*.zip;*.7z;*.jpg;*.jpge;*.gif;*.png',
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
                     InsertUploadRecord(fileName);
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
                 url: getRootPath() + "/WebService/BaseService.asmx/InsertUploadRecord",
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
         function IsFileExist() {

             fileName = uploadfileQueue.outerText.substring(4, uploadfileQueue.outerText.lastIndexOf("(") - 1);

             var UseUnit = 0;
             if ($("#chk1").attr("checked") == "checked")
                 UseUnit += 1;
             if ($("#chk2").attr("checked") == "checked")
                 UseUnit += 2;

             var fileType = 0;
             var fileDesc = $("#EddlFileDesc").val();
             if ($("input[name='rbFileType']:checked").val() == "rbFileType1") {
                 fileType = 0;
             }
             else {
                 fileType = 1;
             }

             var temp = false;
             $.ajax({
                 async: false,
                 type: "Post",
                 url: getRootPath() + "/WebService/BaseService.asmx/IsFileExists",
                 data: "{'UseUnit':'" + UseUnit + "','FileType':'" + fileType + "','FileName':'" + escape(fileName) + "'}",
                 contentType: "application/json; charset=utf-8",
                 dataType: "json",
                 success: function (data) {
                     //debugger;
                     //你可以 alert(data.d)看数据返回的格式
                     temp = data.d;
                 }
             });
             if (temp)
                 return confirm("<%=Resources.Resource.Upload_FileExists %>")
             return true;
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
                width: 500px;
                height:50px;
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
   
			<table class="maintable" cellspacing="0" cellpadding="0" width="100%" align="center" bordercolor="#ffffff"
				border="1" >			
					
					<tr>
						<td class="smalltitle" align="center" width="15%" ><asp:Literal ID="ltlUserName" Text="上傳人員"
                                runat="server"></asp:Literal>
                        </td>
						<td width="35%" >&nbsp;
								<asp:textbox id="txtUserName" runat="server" CssClass="TextRead"  ReadOnly="true" Width="90%" MaxLength="20"></asp:textbox>
                        </td>
						<td class="smalltitle" align="center" width="15%"><asp:Literal 
                                ID="ltlMemo" Text="備註說明"
                                runat="server"></asp:Literal>
                        </td>
						<td  width="35%" rowspan="3">&nbsp;
								<asp:textbox id="txtMemo" runat="server" CssClass="TextBox" Width="90%" TextMode="MultiLine"
									Height="70px" ForeColor="Black"
                                MaxLength="100" ></asp:textbox>
                        </td>
					</tr>
					
                     <tr>
						<td class="musttitle" align="center"><asp:Literal ID="ltlCreateUserName" Text="檔案類型"
                                runat="server"></asp:Literal></td>
						<td  colspan="2">&nbsp;&nbsp;&nbsp;&nbsp;
							 <asp:RadioButton runat="server" ID="rbFileType1" Text="一般檔案" 
                                GroupName="rbFileType" AutoPostBack="True" 
                                oncheckedchanged="rbFileType1_CheckedChanged"/>&nbsp;&nbsp;
                             <asp:RadioButton runat="server" ID="rbFileType2" Text="匯入格式檔" Checked="true" 
                                GroupName="rbFileType" oncheckedchanged="rbFileType1_CheckedChanged" 
                                AutoPostBack="True"/>
                         </td>
						
					
					</tr>
                    <tr>
						<td class="musttitle" align="center"><asp:Literal ID="ltlFileDesc" Text="檔案說明"
                                runat="server"></asp:Literal></td>
						<td colspan="2">&nbsp;
								<cc1:DDL ID="txtFileDesc" runat="server" Width = "90%" />
                             
                        </td>
						
					</tr>
                    <tr>
						<td class="musttitle" align="center"><asp:Literal ID="ltlUseUnit" Text="使用單位"
                                runat="server"></asp:Literal></td>
						<td  colspan="2">&nbsp;&nbsp;&nbsp;&nbsp;
						  <asp:CheckBox runat="server" ID="chk1" Text="營運單位" Checked="true" 
                                AutoPostBack="True" Enabled="False" />&nbsp;&nbsp;
						  <asp:CheckBox runat="server" ID="chk2" Text="企業用戶"  Checked="true" 
                                AutoPostBack="True" Enabled="False"/>
                        </td>	
                        <td>



        <asp:Button ID="btnUploadRecord" runat="server" Text="上傳記錄" OnClientClick="return UploadRecord()" CssClass="but" Width="70px" />
                        </td>					
					</tr>
                  
			</table>

        <input type="file" id="file_upload" name="file_upload" onchange="IsFileExist()" />
        <div id="uploadfileQueue"></div>



        <asp:Button ID="btnUpload" runat="server" Text="上傳"  CssClass="but" Width="70px"
            OnClientClick="if(IsFileExist()) javascript:jQuery('#file_upload').uploadify('upload','*');return false" 
            meta:resourcekey="btnUploadResource1" />
        &nbsp;<asp:Button ID="btnCancel" runat="server" Text="取消上傳" CssClass="but" Width="70px"
            OnClientClick="javascript:jQuery('#file_upload').uploadify('cancel','*');return false;;" 
            meta:resourcekey="btnCancelResource1" />

		</form>
	</body>
</html>
