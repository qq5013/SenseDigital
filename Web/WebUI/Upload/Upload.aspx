<%@ Page Language="C#" EnableSessionState="ReadOnly" Async="true" AutoEventWireup="true" CodeFile="Upload.aspx.cs" Inherits="WebUI_Upload_Upload" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <link href="~/Styles/Main.css" type="text/css" rel="stylesheet" /> 
        <link href="~/Styles/op.css" type="text/css" rel="stylesheet" /> 
    <script type="text/javascript">
        
        function openfile() {

            var fd = new ActiveXObject("MSComDlg.CommonDialog");
            fd.Filter = "All Excel Files (*.xls)";
            fd.FilterIndex = 2;             // 必须设置MaxFileSize. 否则出错        
            fd.MaxFileSize = 128;
            fd.ShowOpen();
            document.getElementById("txtFileName").value = fd.Filename;
            document.getElementById("fileUpload").value = fd.Filename; ;
            return false;
        }
        function ck(obj) {
            if (obj.value.length > 0) {
                var af = "jpg,gif,png,zip,rar,txt,htm";
                if (eval("with(obj.value)if(!/" + af.split(",").join("|") + "/ig.test(substring(lastIndexOf('.')+1,length)))1;")) {
                    alert("允许上传的文件类型:\n" + af);
                    obj.createTextRange().execCommand('delete')
                };
            }
        }
        </script>
</head>
<body>
    <form id="form" runat="server" enctype="multipart/form-data">
    <asp:ScriptManager ID="scriptManager" runat="server" />
    <script type="text/javascript">
        function pageLoad(sender, args) {
            window.parent.register(
                $get('<%= this.form.ClientID %>'),
                $get('<%= this.fileUpload.ClientID %>'),
                $get('<%= this.txtFileName.ClientID %>')
            );
        }
    </script>
    <div>
        <asp:FileUpload ID="fileUpload" runat="server" Width="100%" />
            <table class="maintable" cellspacing="0" cellpadding="0" width="100%" align="center" bordercolor="#ffffff" border="1" >
                    <tr>						
						<td width="80%">&nbsp;
                            <asp:TextBox ID="txtFileName" runat="server" CssClass="TextRead" MaxLength="10" Width="80%"></asp:TextBox>
                            &nbsp;<asp:Button ID="btnBrowse" runat="server" CssClass="but" Text="瀏覽" OnClientClick="return openfile()"
                                Width="52px" />
                        </td>
					</tr>
			</table>
    </div>
    </form>
</body>
</html>
