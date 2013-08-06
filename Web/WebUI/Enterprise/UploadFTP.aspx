<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UploadFTP.aspx.cs" Inherits="WebUI_Enterprise_UploadFTP" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
        <link href="~/Styles/Main.css" type="text/css" rel="stylesheet" /> 
        <link href="~/Styles/op.css" type="text/css" rel="stylesheet" /> 
        <link href="~/Styles/progress.css" type="text/css" rel="Stylesheet"  />
        <link href="~/Styles/upload.css" type="text/css" rel="Stylesheet"  />

        <script type="text/javascript" src='<%#ResolveUrl("~/Scripts/JQuery/jquery-1.8.3.min.js") %>'></script>  
        <script type="text/javascript" src='<%#ResolveUrl("~/JScript/Commons.js") %>'></script>
        
        <script type="text/javascript">
            function Open() {
                //var page = "ProductResumeCompare.aspx";
                //var EnterpriseID = $("#txtEnterpriseID").val();
                var strReturn = window.showModalDialog('OtherActionUpload.aspx', window, 'DialogHeight:200px;DialogWidth:800px;help:no;scroll:no');
                return false;
            }
            function openfile() {

                var fd = new ActiveXObject("MSComDlg.CommonDialog");
                fd.Filter = "所有Excel檔案 (*.xls)";
                fd.FilterIndex = 2;             // 必须设置MaxFileSize. 否则出错        
                fd.MaxFileSize = 128;
                fd.ShowOpen();
                document.getElementById("txtFileName").value = fd.Filename;
                return false;
            }
     
        </script>
    <style type="text/css">
        #uploadFrame
        {
            height: 20px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="scriptManager" runat="server" EnablePageMethods="true" />
    
            <script type="text/javascript">
                var intervalID = 0;
                var progressBar;
                var fileUpload;
                var fileName;
                var form;
                // 进度条      
                function pageLoad() {
                    $addHandler($get('upload'), 'click', onUploadClick);
                    progressBar = $find('progress');
                }
                // 註冊表單       
                function register(form, fileUpload, fileName) {
                    this.form = form;
                    this.fileUpload = fileUpload;
                    this.fileName = fileName;
                }
                //上傳驗證
                function onUploadClick() {
                    var vaild = fileUpload.value.length > 0;
                    //vaild = fileName.value.length > 0;
                    if (vaild) {
                        $get('upload').disabled = 'disabled';
                        updateMessage('info', '初始化上传...');
                        //提交上传
                        form.submit();
                        // 隐藏frame
                        Sys.UI.DomElement.addCssClass($get('uploadFrame'), 'hidden');
                        // 0开始显示进度条
                        progressBar.set_percentage(0);
                        progressBar.show();
                        // 上传过程
                        intervalID = window.setInterval(function () {
                            PageMethods.GetUploadStatus(function (result) {
                                if (result) {
                                    //debugger;
                                    //  更新进度条为新值
                                    progressBar.set_percentage(result.percentComplete);
                                    //更新信息
                                    updateMessage('info', result.message);

                                    if (result == 100) {
                                        // 自动消失
                                        window.clearInterval(intervalID);
                                    }
                                }
                            });
                        }, 500);
                    }
                    else {
                        onComplete('error', '您必需选择一个文件');
                    }
                }

                function onComplete(type, msg) {
                    // 自动消失
                    window.clearInterval(intervalID);
                    // 显示消息
                    updateMessage(type, msg);
                    // 隐藏进度条
                    progressBar.hide();
                    progressBar.set_percentage(0);
                    // 重新启用按钮
                    $get('upload').disabled = '';
                    //  显示frame
                    Sys.UI.DomElement.removeCssClass($get('uploadFrame'), 'hidden');
                }
                function updateMessage(type, value) {
                    var status = $get('status');
                    status.innerHTML = value;
                    // 移除样式
                    status.className = '';
                    Sys.UI.DomElement.addCssClass(status, type);
                }
    
            </script>
        
			<table class="maintable" cellspacing="0" cellpadding="0" width="100%" align="center" bordercolor="#ffffff"
				border="1" >
                    <tr>
						<td class="smalltitle" align="center" width="10%" ><asp:Literal ID="lblBillID" Text="上傳人員"
                                runat="server"></asp:Literal>
                        </td>
						<td width="35%" >&nbsp;
								<asp:textbox id="txtBillID" runat="server" CssClass="TextRead" Width="30%" MaxLength="10">Administrator</asp:textbox>
                                &nbsp;
                        </td>			
					</tr>
					<tr>
						<td class="smalltitle" align="center" width="10%" ><asp:Literal ID="ltlEnterpriseID" Text="企業編號"
                                runat="server"></asp:Literal>
                        </td>
						<td width="35%">&nbsp;
								<asp:textbox id="txtEnterpriseID" runat="server" CssClass="TextRead" Width="30%" MaxLength="10">A0001</asp:textbox>
                                &nbsp;<asp:textbox id="txtEnterpriseName" runat="server" CssClass="TextRead"  ReadOnly="true" Width="60%" MaxLength="10">屏東漁會</asp:textbox>
                        </td>
                        	
					</tr>
                    <tr>
						<td class="smalltitle" align="center" width="10%" ><asp:Literal ID="Literal1" Text="文檔路徑"
                                runat="server"></asp:Literal>
                        </td >
						<td width="80%">&nbsp;
								<asp:textbox id="txtImagePath" runat="server" CssClass="TextRead" 
                                Width="90%" MaxLength="10"></asp:textbox>
                                &nbsp;</td>
                        
					</tr>
                    <tr>
						<td class="smalltitle" align="center" width="10%" >
                            <asp:Literal ID="Literal2" Text="上傳資料"
                                runat="server"></asp:Literal>
                        </td >
						<td width="80%">&nbsp;
                            <asp:TextBox ID="txtFileName" runat="server" CssClass="TextRead" 
                                MaxLength="10" Width="80%"></asp:TextBox>
                            &nbsp;<asp:Button ID="btnBrowse" runat="server" CssClass="but" Text="瀏覽" OnClientClick="return openfile()"
                                Width="52px" />
                            &nbsp;<asp:Button ID="btnUpload" runat="server" CssClass="but" Text="上傳" 
                                Width="52px" />
                        </td>
                        
					</tr>
                    
			</table>
    </form>
</body>
</html>
