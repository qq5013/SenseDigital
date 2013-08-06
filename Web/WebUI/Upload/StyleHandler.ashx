<%@ WebHandler Language="C#" Class="StyleHandler" %>

using System;
using System.Collections.Generic;
using System.Web;
using System.Text;
using System.IO;
using System.Data;
using System.Configuration;
using System.Web.SessionState;

public class StyleHandler : IHttpHandler, IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        context.Response.Charset = "utf-8";

        HttpPostedFile file = context.Request.Files["Filedata"];

        string FormID = context.Request["FormID"] + "";
        string cnKey = "Label";
        byte StyleFlag = byte.Parse(context.Request["StyleFlag"]);
        string StyleID = context.Request["StyleID"] + "";
        string UserName = context.Session["User"].ToString(); 
        
        string uploadPath = HttpContext.Current.Server.MapPath(@"~/Uploads/");
        string fileName = "";
        string documentName = "LabelStyle";
        string FieldName = "ImagePath";

        if (StyleFlag == 0)
        {
            uploadPath = ConfigurationManager.AppSettings["UploadPath"] + @"\COM\" + documentName + @"\";
            fileName = StyleID + ".jpg";
        }
        else if (StyleFlag == 1)
        {
            uploadPath = ConfigurationManager.AppSettings["UploadPath"] + @"\COM\" + documentName + @"\lblegend\";
            fileName = StyleID + "_ld1.jpg";
            FieldName = "ImagePath1";
        }
        else if (StyleFlag == 2)
        {
            uploadPath = ConfigurationManager.AppSettings["UploadPath"] + @"\COM\" + documentName + @"\lblegend\";
            fileName = StyleID + "_ld2.jpg";
            FieldName = "ImagePath2";
        }
        else
        {
            uploadPath = ConfigurationManager.AppSettings["UploadPath"] + @"\COM\" + documentName + @"\lblegend\";
            fileName = StyleID + "_ld3.jpg";
            FieldName = "ImagePath3";
        }
        
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
            file.SaveAs(uploadPath + fileName);
            //file.SaveAs(uploadPath + file.FileName);
            //更新路徑
            Js.BLL.Label.StyleDal dal = new Js.BLL.Label.StyleDal(cnKey);
            dal.UpdateImagePath(StyleID, FieldName, uploadPath + fileName);
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