using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.SessionState;
using System.IO;

/// <summary>
/// Summary description for Common
/// </summary>
public class Common
{
    public Common()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static string GetFileName(string FormID)
    {
        string strFile = "";
        if (FormID == "EP_ProductNoCheck")
            strFile = "Product.xls";
        else if (FormID == "EP_ProductResumeNoCheck")
            strFile = "Prod_resume.xls";
        else if (FormID == "EP_ProductLogisticsNoCheck")
            strFile = "Prod_logistics.xls";
        else
            strFile = "Product.xls";

        return strFile;
    }
    public static string GetPath(string FormID)
    {
        string strPath = "";
        if (FormID == "EP_ProductNoCheck")
            strPath = @"\Product";
        else if (FormID == "EP_ProductResumeNoCheck")
            strPath = @"\Resume";
        else if (FormID == "EP_ProductLogisticsNoCheck")
            strPath = @"\Logistics";
        else
            strPath = @"\Product";

        return strPath;
    }
    public static void SystemMessage(string[] ToUserName, int ReceiverFlag, string[] ReceiveUnitNo, string Title, string Content)
    {
        Js.BLL.BaseDal dal = new Js.BLL.BaseDal("Sys_AnnounceMessage");

        string strAnnounceUnitNo = "";
        string strAnnounceID = "";
        string strAnnouncer = "";

        string strWhere = " AnnounceID like '" + strAnnounceUnitNo + DateTime.Now.ToString("yyyyMMdd") + "%'";
        dal.GetMaxID(strWhere);

        strAnnounceID = dal.GetMaxID(strWhere);
        if (strAnnounceID.Length <= 0)
            strAnnounceID = strAnnounceUnitNo + DateTime.Now.ToString("yyyyMMdd") + "0001";

        if (HttpContext.Current.Session["UserType"].ToString() == "BU")
        {
            Js.BLL.BusinessUnit.CompanyDal cdal = new Js.BLL.BusinessUnit.CompanyDal();
            Js.Model.BusinessUnit.CompanyInfo model = cdal.GetModel();
            strAnnounceUnitNo = model.CompanyNo;
            Js.BLL.Account.UserDal udal = new Js.BLL.Account.UserDal();
            Js.Model.Account.UsersInfo umodel = udal.GetModel(HttpContext.Current.Session["User"].ToString());
            strAnnouncer = umodel.PersonName;
        }
        else
        {
            strAnnounceUnitNo = HttpContext.Current.Session["EnterpriseID"].ToString();

            Js.BLL.Account.UserDal udal = new Js.BLL.Account.UserDal(HttpContext.Current.Session["EnterpriseID"].ToString());
            Js.Model.Account.UsersInfo umodel = udal.GetModel(HttpContext.Current.Session["User"].ToString());
            strAnnouncer = umodel.PersonName;
        }

        DataTable dt = dal.GetRecord("1=2");
        DataRow dr = dt.NewRow();
        dr["AnnounceID"] = strAnnounceID;
        dr["Announcer"] = strAnnouncer;
        if (HttpContext.Current.Session["UserType"].ToString() == "BU")
            dr["AnnounceFlag"] = 0;
        else
            dr["AnnounceFlag"] = 1;
        dr["AnnounceUnitNo"] = strAnnounceUnitNo;
        dr["AnnouncerUserName"] = HttpContext.Current.Session["User"].ToString();

        dr["Source"] = 1;
        dr["AnnounceDate"] = DateTime.Now;
        dr["Title"] = Title;
        dr["Contents"] = Content;

        dal.Add(dr);

        DataTable dtSub = dal.GetSubDetail("").Tables[0];

        for (int i = 0; i < ToUserName.Length; i++)
        {

            DataRow subdr = dtSub.NewRow();
            subdr["AnnounceID"] = strAnnounceID;
            subdr["ReceiverUserName"] = ToUserName[i];
            subdr["ReceiverFlag"] = ReceiverFlag;

            subdr["ReceiveUnitNo"] = ReceiveUnitNo[i];
            Js.BLL.Account.UserDal udal = new Js.BLL.Account.UserDal(ReceiveUnitNo[i]);
            Js.Model.Account.UsersInfo umodel = udal.GetModel(ToUserName[i]);
            subdr["Receiver"] = umodel.PersonName;

            dtSub.Rows.Add(subdr);

        }
        dal.SaveDetail(dtSub, "");
    }
    public static string ClientIP
    {
        get
        {
            string result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (null == result || result == String.Empty)
            { result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]; }
            if (null == result || result == String.Empty)
            {
                result = HttpContext.Current.Request.UserHostAddress;
            }
            return result;
        }
    }
    public static string getIp()
    {
        if (System.Web.HttpContext.Current.Request.ServerVariables["HTTP_VIA"] != null)
            return System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].Split(new char[] { ',' })[0];
        else
            return System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
    }
    public static void DirectDownLoad(string strFile)
    {
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ClearHeaders();
        HttpContext.Current.Response.Charset = "utf-8";
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
        HttpContext.Current.Response.ContentType = "application/octet-stream";
        //----------------------------------------------------
        FileInfo fi = new FileInfo(strFile);
        HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(fi.Name, System.Text.Encoding.UTF8));
        HttpContext.Current.Response.AddHeader("Content-Length", fi.Length.ToString());
        byte[] tmpbyte = new byte[1024 * 8];
        FileStream fs = fi.OpenRead();
        int count;
        while ((count = fs.Read(tmpbyte, 0, tmpbyte.Length)) > 0)
        {
            HttpContext.Current.Response.BinaryWrite(tmpbyte);
            HttpContext.Current.Response.Flush();
        }
        fs.Close();

        HttpContext.Current.Response.End();			
    }
    public static void DeleteFile(string strFile)
    {
        if (System.IO.File.Exists(strFile))
            System.IO.File.Delete(strFile);
    }
}