using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class WebUI_Account_BU_RoleToUserView : BasePage
{
    protected string FormID;
    protected string RoleID;
    protected void Page_Load(object sender, EventArgs e)
    {
        RoleID = Request.QueryString["RoleID"] + "";
        FormID = Request.QueryString["FormID"] + "";
        if (!IsPostBack)
        {
            BindDropDownList();
            ViewState["StrWhere"] = string.Format(" RoleID={0}", RoleID);
            Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID);
            DataTable dt = dal.GetRecord(ViewState["StrWhere"].ToString());
            BindData(dt);
            this.ddlRoleID.Enabled = false;
            this.ddlUserLevel.Enabled = false;
        }
    }
    private void BindDropDownList()
    {
        Js.BLL.Account.RoleDal dal = new Js.BLL.Account.RoleDal();
        DataTable dt = dal.GetRecord("1=1");
        this.ddlRoleID.DataSource = dt;
        this.ddlRoleID.DataTextField = "RoleName";
        this.ddlRoleID.DataValueField = "RoleID";
        this.ddlRoleID.DataBind();
    }
    private void BindData(DataTable dt)
    {
        if (dt.Rows.Count > 0)
        {
            this.ddlRoleID.SelectedValue = dt.Rows[0]["RoleID"].ToString();
            this.ddlUserLevel.SelectedIndex = int.Parse(dt.Rows[0]["RoleUserLevel"].ToString());
            this.txtLastModifyUserName.Text = dt.Rows[0]["LastModifyUserName"].ToString();
            this.txtLastModifyDate.Text = Js.Com.PageValidate.ParseDateTime(dt.Rows[0]["LastModifyDate"].ToString());

            Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID);
            DataTable dtSub = dal.GetSubDetail(this.ddlRoleID.SelectedValue).Tables[0];
            this.GridView1.DataSource = dtSub.DefaultView;
            this.GridView1.DataBind();
        }

        ViewState["StrWhere"] = string.Format(" RoleID={0}", this.ddlRoleID.SelectedValue);
        ViewState["dt"] = dt;
    }
    protected void btnFirst_Click(object sender, EventArgs e)
    {
        Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID);
        DataTable dt = dal.GetRecord("F", this.ddlRoleID.SelectedValue);
        BindData(dt);
    }
    protected void btnPre_Click(object sender, EventArgs e)
    {
        Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID);
        DataTable dt = dal.GetRecord("P", this.ddlRoleID.SelectedValue);
        if (dt != null)
            BindData(dt);
    }
    protected void btnNext_Click(object sender, EventArgs e)
    {
        Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID);
        DataTable dt = dal.GetRecord("N", this.ddlRoleID.SelectedValue);
        if (dt != null)
            BindData(dt);
    }
    protected void btnLast_Click(object sender, EventArgs e)
    {
        Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID);
        DataTable dt = dal.GetRecord("L", this.ddlRoleID.SelectedValue);
        BindData(dt);
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {

    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID);
        string strID = this.ddlRoleID.SelectedValue;
        dal.Delete(strID);

        btnNext_Click(sender, e);
        if (this.ddlRoleID.SelectedValue == strID)
            btnPre_Click(sender, e);
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {

    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            switch (int.Parse(e.Row.Cells[2].Text)%1000)
            {
                case 0:
                    e.Row.Cells[2].Text = Resources.Resource.UserLevel1;
                    break;
                default:
                    e.Row.Cells[2].Text = Resources.Resource.UserLevel2;
                    break;
            }
        }
    }
}