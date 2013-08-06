using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebUI_Enterprise_Upload : BaseLanguage
{
    protected string FormID;    

    protected void Page_Load(object sender, EventArgs e)
    {
        FormID = Request.QueryString["FormID"];
        
        string strFile = Common.GetFileName(FormID);        

        this.txtUploadUserName.Text = Session["User"].ToString();
        this.txtEnterpriseID.Text = Session["EnterpriseID"].ToString();
        this.txtEnterpriseName.Text = Session["EnterpriseName"].ToString();
        this.txtImagePath.Text = ConfigurationManager.AppSettings["UploadPath"] + Session["EnterpriseID"].ToString() + @"\" + Common.GetPath(FormID) + @"\" + strFile;
    }
}