<%@ WebHandler Language="C#" Class="UploadHandler" %>

using System;
using System.Collections.Generic;
using System.Web;
using System.Text;
using System.IO;
using System.Data;
using System.Configuration;
using System.Web.SessionState;

public class UploadHandler : IHttpHandler, IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        context.Response.Charset = "utf-8";

        HttpPostedFile file = context.Request.Files["Filedata"];
        
        byte UseUnit = byte.Parse(context.Request["UseUnit"]);
        string FileType = context.Request["FileType"] + "";
        string UserName = context.Session["User"].ToString(); 
        
        string uploadPath = HttpContext.Current.Server.MapPath(@"~/Uploads/");

        string documentName = "Sample";
        if(FileType=="*.*")
            documentName = "Other";

        if (UseUnit == 1)
            uploadPath = ConfigurationManager.AppSettings["UploadPath"] + @"\COM\" + documentName + @"\BU\";
        else if (UseUnit == 2)
            uploadPath = ConfigurationManager.AppSettings["UploadPath"] + @"\COM\" + documentName + @"\EP\";
        else
            uploadPath = ConfigurationManager.AppSettings["UploadPath"] + @"\COM\" + documentName + @"\ALL\";
        
        if (file != null)
        {
            if (System.IO.File.Exists(uploadPath + file.FileName))
            {
                //context.Response.Write("<script languge='javascript'>if(confirm('清單內相同檔案，是否上傳覆蓋?'))</script>");
            }
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }
            //file.SaveAs(uploadPath + System.DateTime.Now.ToString("yyyyMMddHHmmss") + file.FileName.Substring(file.FileName.LastIndexOf('.')));
            file.SaveAs(uploadPath + file.FileName);
            
            context.Response.Write("1");
        }
        else
        {
            context.Response.Write("0");
        }
        context.Response.End();
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
    private static void Upload(string ftpServer, string userName, string password, string filename)
    {
        using (System.Net.WebClient client = new System.Net.WebClient())
        {
            client.Credentials = new System.Net.NetworkCredential(userName, password);
            client.UploadFile(ftpServer + "/" + new FileInfo(filename).Name, "STOR", filename);
        }
    }
    
}