using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebUI_Customer_QueryLabelRecord : BasePage
{
    protected string FormID;
    protected string ID;
    protected string cnKey = "Customer";
    protected void Page_Load(object sender, EventArgs e)
    {
        ID = Request.QueryString["ID"] + "";
        FormID = Request.QueryString["FormID"] + "";
        if (!IsPostBack)
        {
            //ViewState["StrWhere"] = string.Format(" MemberID='{0}'", ID);
            //Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID, cnKey);
            //DataTable dt = dal.GetRecord(ViewState["StrWhere"].ToString());
            //ViewState["dt"] = dt;
            //Js.BLL.Sys.SysComDal sdal = new Js.BLL.Sys.SysComDal();
            //BindData(dt);
        }
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID, cnKey);
        //DataTable dtSub = dal.GetSubDetail(this.txtMemberID.Text).Tables[0];
        DataTable dtSub = dal.GetRecord(string.Format("MemberID ='{0}'", this.txtMemberID.Text.Trim())); 
        this.GridView1.DataSource = dtSub.DefaultView;
        this.GridView1.DataBind();
    }
}