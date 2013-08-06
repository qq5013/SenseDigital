using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Text;
using System.Configuration;

/// <summary>
/// Summary description for GetBaseData
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[System.ComponentModel.ToolboxItem(false)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class BaseService : System.Web.Services.WebService {

    public BaseService () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public bool IsExists(string FormID, string ID,string cnKey)
    {
        ID = Server.UrlDecode(ID);
        Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID, cnKey);
        return dal.Exists(ID);
    }
    [WebMethod]
    public bool IsFlagExists(string FormID, string ID, byte Flag, string cnKey)
    {
        Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID, cnKey);
        return dal.Exists(ID,"Flag=" + Flag);
    }
    [WebMethod]
    public string IsExistsByFilter(string FormID, string EnterpriseID, string ID, string cnKey)
    {
        ID = Server.UrlDecode(ID);
        EnterpriseID = Server.UrlDecode(EnterpriseID);
        string filter = string.Format("EnterpriseID='{0}'", EnterpriseID);
        
        Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID, cnKey);
        Js.BLL.Enterprise.CheckDal cdal = new Js.BLL.Enterprise.CheckDal(FormID, cnKey);

        byte strTemp = 0;
        if (ID.Length > 0)
        {
            if (dal.Exists(ID, filter))
                strTemp = 1;
            else
                strTemp = 0;
        }
        if (FormID == "EP_Department" || FormID == "EP_Person")
        { }
        else
        {
            if (strTemp == 0)
            {
                if (cdal.IsEnterpriseChecked(EnterpriseID))
                    strTemp = 2;
            }
        }
        return strTemp.ToString();
    }

    [WebMethod]
    public string strBaseData(string FormID, string strIdValue, string strFieldName, string strWhere,string cnKey)
    {
        if (strFieldName == "")
            strFieldName = "*";

        if (strWhere.Length != 0)
            strWhere = Microsoft.JScript.GlobalObject.unescape(strWhere);

        strIdValue = Microsoft.JScript.GlobalObject.unescape(strIdValue);
        strFieldName = Microsoft.JScript.GlobalObject.unescape(strFieldName);

        Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID, cnKey);

        string filter = strWhere;
        if (strWhere.Trim().Length > 0)
            filter += " and " + strWhere;

        DataTable dt = dal.GetRecord(strIdValue, strFieldName,filter);
        
        DataTable dtc = dt.Clone();
        foreach (DataColumn dr in dtc.Columns)
        {
            if (dr.DataType.ToString() == "System.DateTime")
                dr.DataType = typeof(string);
        }

        foreach (DataRow dr in dt.Rows)
        {
            DataRow drNew = dtc.NewRow();
            for (int i = 0; i < dr.ItemArray.Length; i++)
            {
                if (dr[i].GetType().ToString() != "System.DateTime")
                    drNew[i] = dr[i];
                else
                    drNew[i] = Js.Com.PageValidate.GetDateTimeString((DateTime)dr[i], "yyyy/MM/dd", false);
            }
            dtc.Rows.Add(drNew);
        }
        return Js.Com.JsonHelper.Dtb2Json(dtc);
    }
    [WebMethod(EnableSession = true)]
    public string strAnnounceID()
    {
        if (Session["User"] == null)
            return "";
        Js.BLL.BaseDal dal = new Js.BLL.BaseDal("Sys_ReadMessage");
        //Sys_AnnounceSub.ReceiverUserName, dbo.Sys_AnnounceSub.ReceiverFlag
        string filter = "1=1";
        if (Session["UserType"].ToString() == "BU")
            filter = string.Format("(ReceiverFlag=0 and ReceiverUserName='{0}' And IsRead=0) Or (AnnounceFlag=0 and AnnouncerUserName='{0}' and ReplyTitle<>'' and IsRead1=0)", Session["User"].ToString());
        else
            filter = string.Format("(ReceiveUnitNo='{0}' and ReceiverUserName='{1}' And IsRead=0) Or (AnnounceUnitNo='{0}' and AnnouncerUserName='{1}' and ReplyTitle<>'' and IsRead1=0)", Session["EnterpriseID"].ToString(),Session["User"].ToString());

        DataTable dt = dal.GetRecord(filter);

        if (dt.Rows.Count > 0)
            return dt.Rows[0]["AnnounceID"].ToString();
        else
            return "";
    }
    [WebMethod]
    public void GetAddress(int level, int strNo)
    {
        Js.BLL.Sys.SysComDal dal = new Js.BLL.Sys.SysComDal();
        StringBuilder sb = new StringBuilder();
        DataTable dt;
        string TableName = "Sys_Address1";
        if (level == 1)
            TableName = "Sys_Address2";
        else
            TableName = "Sys_Address3";
        dt = dal.GetAddress(TableName, strNo).Tables[0];

        sb.Append("[");
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            sb.Append("{");
            sb.AppendFormat(@"""c_name"":""{0}"",", dt.Rows[i]["Name"]);
            if(level==1)
                sb.AppendFormat(@"""c_zipno"":""{0}"",", dt.Rows[i]["ZipNo"]);
            sb.AppendFormat(@"""c_code"":""{0}""", dt.Rows[i]["SerNo"]);
            sb.Append("}");
            if (i < dt.Rows.Count - 1)
            {
                sb.Append(",");
            }
        }

        sb.Append("]");
        System.Web.HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");
        System.Web.HttpContext.Current.Response.Write(sb.ToString());

    }
    [WebMethod]
    public string strResource(string keyName)
    {
        return Resources.Resource.ResourceManager.GetString(keyName);
    }
    [WebMethod(EnableSession = true)]
    public bool IsFileExists(byte UseUnit, byte FileType, string FileName)
    {
        string FileFullName = "";
        string documentName = "Sample";
        if (FileType == 0)
            documentName = "Other";
        FileName = Microsoft.JScript.GlobalObject.unescape(FileName);

        if (UseUnit == 1)
            FileFullName = ConfigurationManager.AppSettings["UploadPath"] + @"\COM\" + documentName + @"\BU\" + FileName;
        else if (UseUnit == 2)
            FileFullName = ConfigurationManager.AppSettings["UploadPath"] + @"\COM\" + documentName + @"\EP\" + FileName;
        else
            FileFullName = ConfigurationManager.AppSettings["UploadPath"] + @"\COM\" + documentName + @"\ALL\" + FileName;

        return System.IO.File.Exists(FileFullName);
    }
    [WebMethod]
    public string LoadAddress(int level, int strNo)
    {
        Js.BLL.Sys.SysComDal dal = new Js.BLL.Sys.SysComDal();

        DataTable dt;
        string TableName = "Sys_Address1";
        if (level == 1)
            TableName = "Sys_Address2";
        else if (level == 2)
            TableName = "Sys_Address3";
        dt = dal.GetAddress(TableName, strNo).Tables[0];

        return Js.Com.JsonHelper.Dtb2Json(dt);
    }

    [WebMethod]
    public string autoCode(string FormID, string cnKey, string strdate)
    {
        Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID, cnKey);
        return dal.GetAutoCode(DateTime.Parse(strdate));
    }

    [WebMethod]
    public string historyAddress(string FormID, string cnKey, string filter)
    {
        Js.DAO.Label.OrderDao dal = new Js.DAO.Label.OrderDao(FormID, cnKey);
        return Js.Com.JsonHelper.Dtb2Json(dal.historyAddress(filter)); 
    }
    [WebMethod]
    public bool ConfirmPwd(string UserName,string Password,string cnKey)
    {
        Password = Microsoft.JScript.GlobalObject.unescape(Password);
        Password = Js.Com.PageValidate.InputText(Password, 30);
        byte[] buffer1 = Js.BLL.Account.UserPrincipal.EncryptPassword(Password);

        Js.BLL.Account.UserDal dal = new Js.BLL.Account.UserDal(cnKey);
        if (dal.UserPwdConfirm(UserName, buffer1))
            return true;
        else
            return false;
    }
    [WebMethod]
    public void InsertUploadRecord(string UserName, byte UseUnit, byte FileType, string FileName, string FileDesc, string Memo)
    {
        UserName = Microsoft.JScript.GlobalObject.unescape(UserName);
        FileName = Microsoft.JScript.GlobalObject.unescape(FileName);
        FileDesc = Microsoft.JScript.GlobalObject.unescape(FileDesc);
        Memo = Microsoft.JScript.GlobalObject.unescape(Memo);

        Js.BLL.BusinessUnit.CheckDal dal = new Js.BLL.BusinessUnit.CheckDal();
        dal.InsertIntoUploadRecord(UserName, UseUnit, FileType, FileName, FileDesc, Memo);
    }
    //[WebMethod]
    //public string RunCS(string clsName, string strMode, string FormID, string cnKey, string filter)
    //{
    //    System.Reflection.Assembly aa = System.Reflection.Assembly.Load("Js.DAO");
    //    Type t =  aa.GetType(clsName);
    //    //System.Reflection.BindingFlags flags = (System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public |
    //    //   System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.DeclaredOnly);

    //     System.Reflection.MethodInfo m = t.GetMethod(strMode);
    //     Object obj = Activator.CreateInstance(t, new object[] { FormID, cnKey });
    //     return Js.Com.JsonHelper.Dtb2Json((DataTable )m.Invoke(obj, new object[] { filter }));
    //        //foreach (MethodInfo m in mi)
    //        //{
    //        //    // Instead of invoking the methods,
    //        //    m.Invoke(obj, new object[] { });
    //    //Js.DAO.Label.OrderDao dal = new Js.DAO.Label.OrderDao(FormID, cnKey);
    //    //return Js.Com.JsonHelper.Dtb2Json(dal.historyAddress(filter));
    //    //return "";
    //}

    /// <summary>
    /// 動態調用dll裏的後台方法
    /// </summary>
    /// <param name="xmlpara">para</param>
    /// var row = new Object(); 
    /// row.dll_className = "Js.DAO.Label.OrderDao";//注意大小寫
    /// row.dll_ModeName = "historyAddress";//注意大小寫
    /// row.FormID=FormID;//參數
    /// row.cnKey=cnKey;//參數
    /// row.filter="";//參數
    /// jsAjax("doJsDAOMode", parseXmlPara(row)) ;
    /// <returns></returns>
    [WebMethod(EnableSession = true)]
    [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Xml)]
    public ReturnData doJsDAOMode(string xmlpara)
    {
        ReturnData rr = new ReturnData();
        try
        {
            DataTable dt = Js.Com.JsonHelper.Json2Dtb(xmlpara);
            // Get the constructor and create an instance of MagicClass
            object[] arr = new object[] { };

            System.Reflection.Assembly aa = System.Reflection.Assembly.Load("Js.DAO");
            Type magicType = aa.GetType("" + dt.Rows[0]["dll_className"]);


            object magicClassObject = Activator.CreateInstance(magicType, new object[] {dt.Columns.IndexOf("FormID") != -1 ? "" + dt.Rows[0]["FormID"] :"", dt.Columns.IndexOf("cnKey") != -1 ? "" + dt.Rows[0]["cnKey"]:"" });

            System.Reflection.MethodInfo magicMethod = magicType.GetMethod("" + dt.Rows[0]["dll_ModeName"]);

            System.Reflection.ParameterInfo[] parainfo = magicMethod.GetParameters();
            arr = new object[parainfo.Length];
            //參數
            for (int i = 0; i < parainfo.Length; i++)
            {
                if (parainfo[i].ParameterType == typeof(byte))
                {
                    arr[i] = byte.Parse("" + dt.Rows[0][parainfo[i].Name]);
                }
                else if (parainfo[i].ParameterType == typeof(bool))
                {
                    arr[i] = bool.Parse("" + dt.Rows[0][parainfo[i].Name]);
                }
                else if (parainfo[i].ParameterType == typeof(DateTime))
                {
                    arr[i] = DateTime.Parse("" + dt.Rows[0][parainfo[i].Name]);
                }
                else if (parainfo[i].ParameterType == typeof(int))
                {
                    arr[i] = int.Parse("" + dt.Rows[0][parainfo[i].Name]);
                }
                else if (parainfo[i].ParameterType == typeof(decimal))
                {
                    arr[i] = decimal.Parse("" + dt.Rows[0][parainfo[i].Name]);
                }
                else
                    arr[i] = "" + dt.Rows[0][parainfo[i].Name];

            }

            if (magicMethod.ReturnType == typeof(void))
            {
                magicMethod.Invoke(magicClassObject, arr); return null;
            }

            object magicValue = magicMethod.Invoke(magicClassObject, arr);
            if (magicValue.GetType() == typeof(DataTable))
            {
                dt = (DataTable)magicValue;
                rr.data = Js.Com.JsonHelper.Dtb2Json(dt);
                rr.type = "" + dt.GetType();
            }
            else
            {
                rr.data = "" + magicValue;
                rr.type = "" + magicValue.GetType();
            }
            return rr;
        }
        catch (Exception ex)
        {
            rr.data = "doJsDAOMode ErrMsg:" + ex.Message;
            rr.type = "ErrMsg";
            return rr;
        }

    }
}

/// <summary>
/// 自定義 反回值
/// </summary>
public class ReturnData
{
    public string data = "";
    public string type = "";
}

