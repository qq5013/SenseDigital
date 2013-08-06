<%@ WebHandler Language="C#" Class="OtherUploadHandler" %>

using System;
using System.Collections.Generic;
using System.Web;
using System.Text;
using System.IO;
using System.Data;
using System.Configuration;
using System.Web.SessionState;

public class OtherUploadHandler : IHttpHandler, IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        context.Response.Charset = "utf-8";

        HttpPostedFile file = context.Request.Files["Filedata"];

        string FormID = context.Request["FormID"];
        //string EnterpriseID = context.Request["EnterpriseID"]; 
        string EnterpriseID = context.Session["EnterpriseID"].ToString(); 
        
        string uploadPath = HttpContext.Current.Server.MapPath(@"~/Uploads/");
        uploadPath = ConfigurationManager.AppSettings["UploadPath"] + @"\" + EnterpriseID + Common.GetPath(FormID) + @"\";
        
        if (file != null)
        {
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }
            //file.SaveAs(uploadPath + System.DateTime.Now.ToString("yyyyMMddHHmmss") + file.FileName.Substring(file.FileName.LastIndexOf('.')));
            file.SaveAs(uploadPath + Common.GetFileName(FormID));
            
            //記錄上傳日誌
            Js.BLL.Enterprise.CheckDal dal = new Js.BLL.Enterprise.CheckDal(FormID, context.Session["EnterpriseID"].ToString());
            DataTable dt = dal.GetUploadEmptyRecord();
            DataRow dr = dt.NewRow();

            dr["EnterpriseID"] = context.Session["EnterpriseID"].ToString();
            dr["EnterpriseName"] = context.Session["EnterpriseName"].ToString();
            dr["UserName"] = context.Session["User"].ToString();
            dr["UploadPath"] = uploadPath + file.FileName;
            dal.InsertUploadRrecord(dr);
            
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