using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class WebUI_Upload_Upload : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.IsPostBack)
        {
            string js;
            UploadInfo uploadInfo = Session["UploadInfo"] as UploadInfo;
            uploadInfo.IsReady = false;
            
            if (this.fileUpload.PostedFile != null && this.fileUpload.PostedFile.ContentLength > 0)
            {
                string path = this.Server.MapPath(@"~/Uploads");
                string fileName = Path.GetFileName(this.fileUpload.PostedFile.FileName);

                uploadInfo.ContentLength = this.fileUpload.PostedFile.ContentLength;
                uploadInfo.FileName = fileName;
                uploadInfo.UploadedLength = 0;

                uploadInfo.IsReady = true;
                
                int bufferSize = 1;
                byte[] buffer = new byte[bufferSize];

                using (FileStream fs = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                {
                    while (uploadInfo.UploadedLength < uploadInfo.ContentLength)
                    {
                        int bytes = this.fileUpload.PostedFile.InputStream.Read(buffer, 0, bufferSize);
                        fs.Write(buffer, 0, bytes);
                        uploadInfo.UploadedLength += bytes;
                    }
                }
                js = string.Format("<script type=\"text/javascript\">window.parent.onComplete('success', '{0} 已成功上傳');</script>", fileName);
                ClientScript.RegisterStartupScript(this.GetType(), "progress", js);
            }
            else
            {
                js = "<script type=\"text/javascript\">window.parent.onComplete('success', '上傳文件出錯');</script>";
                ClientScript.RegisterStartupScript(this.GetType(), "progress", js);
            }
            uploadInfo.IsReady = false;
        }
    }
    //限制大小 1M
    protected void Page_Load2(object sender, EventArgs e)
    {
        if (this.IsPostBack)
        {
            string js;
            UploadInfo uploadInfo = this.Session["UploadInfo"] as UploadInfo;
            if (uploadInfo == null)
            {
                // 让父页面知道无法处理上传
                js = "<script type=\"text/javascript\">window.parent.onComplete('error', '无法上传文件。请刷新页面，然后再试一次);</script>";
                ClientScript.RegisterStartupScript(this.GetType(), "progress", js);
            }
            else
            {
                //  让服务端知道我们还没有准备好..
                uploadInfo.IsReady = false;

                //  上传验证
                if (this.fileUpload.PostedFile != null && this.fileUpload.PostedFile.ContentLength > 0

                    && this.fileUpload.PostedFile.ContentLength < 1048576)//  限制1M
                {
                    //  设置路径
                    string path = this.Server.MapPath(@"Upload");
                    string fileName = Path.GetFileName(this.fileUpload.PostedFile.FileName);

                    // 上传信息
                    uploadInfo.ContentLength = this.fileUpload.PostedFile.ContentLength;
                    uploadInfo.FileName = fileName;
                    uploadInfo.UploadedLength = 0;

                    //文件存在 初始化...
                    uploadInfo.IsReady = true;

                    //缓存
                    int bufferSize = 1;
                    byte[] buffer = new byte[bufferSize];

                    // 保存字节
                    using (FileStream fs = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                    {
                        while (uploadInfo.UploadedLength < uploadInfo.ContentLength)
                        {
                            //从输入流放进缓冲区
                            int bytes = this.fileUpload.PostedFile.InputStream.Read(buffer, 0, bufferSize);
                            // 字节写入文件流
                            fs.Write(buffer, 0, bytes);
                            //  更新大小
                            uploadInfo.UploadedLength += bytes;

                            //  线程睡眠 上传就更慢 这样就可以看到进度条了
                            System.Threading.Thread.Sleep(100);
                        }
                    }

                    // 删除.
                    File.Delete(Path.Combine(path, fileName));

                    //   让父页面知道已经处理上传完毕
                    js = string.Format("<script type=\"text/javascript\">window.parent.onComplete('success', '{0} 已成功上傳');</script>", fileName);
                    ClientScript.RegisterStartupScript(this.GetType(), "progress", js);
                }
                else
                {
                    if (this.fileUpload.PostedFile.ContentLength >= 1048576)//1M
                    {
                        js = "<script type=\"text/javascript\">window.parent.onComplete('error', '超出上传文件限制大小，请重新选择');</script>";
                        ClientScript.RegisterStartupScript(this.GetType(), "progress", js);
                    }
                    else
                    {
                        js = "<script type=\"text/javascript\">window.parent.onComplete('error', '上傳文檔出錯');</script>";
                        ClientScript.RegisterStartupScript(this.GetType(), "progress", js);
                    }
                }
                uploadInfo.IsReady = false;
            }
        }
    }
}