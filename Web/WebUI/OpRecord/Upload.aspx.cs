using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebUI_OpRecord_Upload : System.Web.UI.Page
{
    protected string FormID;
    protected string BillID;
    string cnKey = "Label";
    protected void Page_Load(object sender, EventArgs e)
    {
        FormID = Request.QueryString["FormID"];
        BillID = Request.QueryString["BillID"];
        if (!IsPostBack)
        {
            ViewState["StrWhere"] = string.Format(" BillID='{0}'", BillID);
            Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID, cnKey);
            DataTable dt = dal.GetRecord(ViewState["StrWhere"].ToString());
            ViewState["dt"] = dt;
            
            BindData(dt);
        }
    }
    private void BindData(DataTable dt)
    {

        if (dt.Rows.Count > 0)
        {
            this.txtEnterpriseID.Text = dt.Rows[0]["EnterPriseID"].ToString();
            this.txtEnterpriseName.Text = dt.Rows[0]["EnterpriseName"].ToString();
            this.txtStyleID.Text = dt.Rows[0]["StyleID"].ToString();
            this.txtLabelMode.Text = dt.Rows[0]["LabelMode"].ToString();
            this.txtQtyTotal.Text = dt.Rows[0]["QtyTotal"].ToString();
            
        }
        ScriptManager.RegisterStartupScript(this.updatePanel, this.GetType(), "Resize", "content_resize();", true);
    }
}