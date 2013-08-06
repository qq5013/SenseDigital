using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class WebUI_Enterprise_ProductModify : BasePage
{
    string FormID = "EP_Product";
    string EnterpriseID = "";   
    string cnKey = "Enterprise";
    string ProductID = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["cnKey"] != null)
            cnKey = Session["cnKey"].ToString();
        EnterpriseID = Request.QueryString["EnterpriseID"] + "";
        FormID = Request.QueryString["FormID"] + "";
        ProductID = Request.QueryString["ID"] + "";

        if (!IsPostBack)
        {          
            Js.BLL.Enterprise.CheckDal cdal = new Js.BLL.Enterprise.CheckDal(FormID, cnKey);
            string filter = string.Format("EnterpriseID= '{0}' and ProductID='{1}'", EnterpriseID, ProductID);
            DataTable dt = cdal.GetModifyRecord(FormID,filter);
            this.GridView1.DataSource = dt.DefaultView;
            this.GridView1.DataBind();
        }
    }

}
