using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class WebUI_Enterprise_QueryChecked : System.Web.UI.Page
{
    protected string FormID;
    protected string cnKey = "Enterprise";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["cnKey"] != null)
            cnKey = Session["cnKey"].ToString();
        FormID = Request.QueryString["FormID"];

        if (!Page.IsPostBack)
        {
            BindDropDownList();
            SetSession();
            ViewState["StrWhere"] = string.Format("EnterpriseID='{0}'", Session["EnterpriseID"].ToString());
            BindGrid();
        }
    }
    private void BindDropDownList()
    {
        Js.BLL.BaseDal dal = new Js.BLL.BaseDal("BU_Enterprise");
        string filter = "1=1";
        if (Session["UserType"].ToString() == "EP")
            filter = string.Format("EnterpriseID='{0}'", Session["EnterpriseID"].ToString());
        this.ddlEnterpriseID.DataSource = dal.GetIDNameList(filter);
        this.ddlEnterpriseID.DataTextField = "IDName";
        this.ddlEnterpriseID.DataValueField = "ID";
        this.ddlEnterpriseID.DataBind();

        if (Session["EnterpriseID"] != null)
            this.ddlEnterpriseID.SelectedValue = Session["EnterpriseID"].ToString();
    }
    private void BindGrid()
    {
        Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID, Session["EnterpriseID"].ToString());
        DataTable dt = dal.GetViewRecord(ViewState["StrWhere"].ToString());
        this.GridView1.DataSource = dt;
        this.GridView1.DataBind();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int i = e.Row.RowIndex + 1;
            e.Row.Cells[0].Text = i.ToString();
            switch (e.Row.Cells[1].Text.ToString())
            {
                case "1":
                    e.Row.Cells[1].Text = Resources.Resource.EP_ProductCheck;
                    break;
                case "2":
                    e.Row.Cells[1].Text = Resources.Resource.EP_ProductResumeCheck;
                    break;
                case "3":
                    e.Row.Cells[1].Text = Resources.Resource.EP_ProductLogisticsCheck;
                    break;
            }
        }
    }
    private void SetSession()
    {
        Session["cnKey"] = this.ddlEnterpriseID.SelectedValue.ToString();
        cnKey = Session["cnKey"].ToString();
        Session["EnterpriseID"] = this.ddlEnterpriseID.SelectedValue.ToString();
        Session["EnterpriseName"] = this.ddlEnterpriseID.SelectedItem.Text.Substring(this.ddlEnterpriseID.SelectedItem.Text.IndexOf("  ") + 1);
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {

    }
    protected void ddlEnterpriseID_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetSession();
        ViewState["StrWhere"] = string.Format("EnterpriseID='{0}'", Session["EnterpriseID"].ToString());
        BindGrid();
    }
}