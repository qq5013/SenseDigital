<%@ WebHandler Language="C#" Class="Handler" %>

using System;
using System.Collections.Generic;
using System.Web;
using System.Text;
using System.IO;
using System.Net;

public class Handler : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        context.Response.Write("Hello World");
        string teststr = context.Request["tt"]; 

        HttpPostedFile file = context.Request.Files["Filedata"];
        
        if (file != null)
        {
            //Upload(file.FileName);
            FtpWebRequest reqFTP;

            // 根据uri创建FtpWebRequest对象 
            reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://203.67.36.61:21/home/" + file.FileName));
            // ftp用户名和密码
            reqFTP.Credentials = new NetworkCredential("haiping", "13959204915");
            // 默认为true，连接不会被关闭
            // 在一个命令之后被执行
            reqFTP.KeepAlive = false;
            // 指定执行什么命令
            reqFTP.Method = WebRequestMethods.Ftp.UploadFile;
            // 指定数据传输类型
            reqFTP.UseBinary = true;
            //reqFTP.Timeout = 36000;
            // 上传文件时通知服务器文件的大小
            reqFTP.ContentLength = file.ContentLength;
            // 缓冲大小设置为10kb
            int buffLength = 10240;//2048
            byte[] buff = new byte[buffLength];
            int contentLen;
            // 打开一个文件流 (System.IO.FileStream) 去读上传的文件
            //FileStream fs = fileInf.OpenRead();
            Stream fs = file.InputStream;
            try
            {
                // 把上传的文件写入流
                Stream strm = reqFTP.GetRequestStream();
                // 每次读文件流的2kb
                contentLen = fs.Read(buff, 0, buffLength);
                // 流内容没有结束
                while (contentLen != 0)
                {
                    // 把内容从file stream 写入 upload stream
                    strm.Write(buff, 0, contentLen);
                    contentLen = fs.Read(buff, 0, buffLength);
                }
                // 关闭两个流
                strm.Close();
                fs.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            context.Response.Write("1");
        }

        else
        {
            context.Response.Write("0");
        }
        context.Response.End();
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }
    private static void Upload(string filename)
    {
        using (System.Net.WebClient client = new System.Net.WebClient())
        {
            client.Credentials = new System.Net.NetworkCredential("haiping", "13959204915");
            client.UploadFile("ftp://203.67.36.61:21/home/" + filename, "STOR", filename);
        }
    }
}