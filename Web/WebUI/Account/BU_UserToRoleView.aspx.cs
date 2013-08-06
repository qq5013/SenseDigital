using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class WebUI_Account_BU_UserToRoleView : BasePage
{
    protected string FormID;
    protected string UserID;
    protected void Page_Load(object sender, EventArgs e)
    {
        UserID = Request.QueryString["UserID"] + "";
        FormID = Request.QueryString["FormID"] + "";
        if (!IsPostBack)
        {
            BindDropDownList();
            ViewState["StrWhere"] = string.Format(" UserID={0}", UserID);
            Js.BLL.Account.UserDal dal = new Js.BLL.Account.UserDal();
            DataTable dt = dal.GetUserRole(int.Parse(UserID));
            BindData(dt);
            this.ddlUserID.Enabled = false;
            this.ddlUserLevel.Enabled = false;
        }
    }
    private void BindDropDownList()
    {
        Js.BLL.Account.UserDal dal = new Js.BLL.Account.UserDal();
        DataTable dt = dal.GetRecord("1=1");
        this.ddlUserID.DataSource = dt;
        this.ddlUserID.DataTextField = "UserName";
        this.ddlUserID.DataValueField = "UserID";
        this.ddlUserID.DataBind();
    }
    private void BindData(DataTable dt)
    {
        if (dt.Rows.Count > 0)
        {
            this.ddlUserID.SelectedValue = dt.Rows[0]["UserID"].ToString();
            if (dt.Rows[0]["UserLevel"].ToString() == "1000")
                this.ddlUserLevel.SelectedIndex = 2;
            else if (int.Parse(dt.Rows[0]["UserLevel"].ToString()) % 1000 == 0)
                this.ddlUserLevel.SelectedIndex = 1;
            else
                this.ddlUserLevel.SelectedIndex = 0;
            this.txtLastModifyUserName.Text = dt.Rows[0]["LastModifyUserName"].ToString();
            this.txtLastModifyDate.Text = Js.Com.PageValidate.ParseDateTime(dt.Rows[0]["LastModifyDate"].ToString());

            Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID);
            DataTable dtSub = dal.GetSubDetail(this.ddlUserID.SelectedValue).Tables[0];
            this.GridView1.DataSource = dtSub.DefaultView;
            this.GridView1.DataBind();
        }

        ViewState["StrWhere"] = string.Format(" UserID={0}", this.ddlUserID.SelectedValue);
        ViewState["dt"] = dt;
    }
    protected void btnFirst_Click(object sender, EventArgs e)
    {
        Js.BLL.Account.UserDal dal = new Js.BLL.Account.UserDal();
        DataTable dt = dal.GetRecord("F", int.Parse(this.ddlUserID.SelectedValue.ToString()));
        BindData(dt);
    }
    protected void btnPre_Click(object sender, EventArgs e)
    {
        Js.BLL.Account.UserDal dal = new Js.BLL.Account.UserDal();
        DataTable dt = dal.GetRecord("P", int.Parse(this.ddlUserID.SelectedValue.ToString()));
        if (dt != null)
            BindData(dt);
    }
    protected void btnNext_Click(object sender, EventArgs e)
    {
        Js.BLL.Account.UserDal dal = new Js.BLL.Account.UserDal();
        DataTable dt = dal.GetRecord("N", int.Parse(this.ddlUserID.SelectedValue.ToString()));
        if (dt != null)
            BindData(dt);
    }
    protected void btnLast_Click(object sender, EventArgs e)
    {
        Js.BLL.Account.UserDal dal = new Js.BLL.Account.UserDal();
        DataTable dt = dal.GetRecord("L", int.Parse(this.ddlUserID.SelectedValue.ToString()));
        BindData(dt);
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {

    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID);
        string strID = this.ddlUserID.SelectedValue;
        dal.Delete(strID);

        btnNext_Click(sender, e);
        if (this.ddlUserID.SelectedValue == strID)
            btnPre_Click(sender, e);
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {

    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            switch (e.Row.Cells[2].Text)
            {
                case "0":
                    e.Row.Cells[2].Text = Resources.Resource.UserLevel2;
                    break;
                case "1":
                    e.Row.Cells[2].Text = Resources.Resource.UserLevel1;
                    break;
            }   
        }
    }
}