using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebUI_BusinessUnit_EnterpriseModifyRecord : BasePage
{

    string EnterpriseID = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        EnterpriseID = Request.QueryString["EnterpriseID"] + "";        

        if (!IsPostBack)
        {
            Js.BLL.BusinessUnit.EnterpriseDal dal = new Js.BLL.BusinessUnit.EnterpriseDal();

            DataTable dt = dal.GetModifyRecord(EnterpriseID);
            this.GridView1.DataSource = dt.DefaultView;
            this.GridView1.DataBind();
        }
    }

}