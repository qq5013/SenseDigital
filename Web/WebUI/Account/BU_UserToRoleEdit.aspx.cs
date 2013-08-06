using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class WebUI_Account_BU_UserToRoleEdit : BasePage
{
    protected string FormID;
    protected string UserID;
    protected string IsUser;
    DataTable dtSub;

    protected void Page_Load(object sender, EventArgs e)
    {
        UserID = Request.QueryString["UserID"] + "";
        FormID = Request.QueryString["FormID"] + "";
        IsUser = Request.QueryString["IsUser"] + "";
        if (!IsPostBack)
        {
            BindDropDownList();
            this.ddlUserLevel.Enabled = false;
            BindRoles();
            if (UserID.Length > 0)
            {
                ViewState["StrWhere"] = string.Format(" UserID={0}", UserID);
                Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID);

                DataTable dt = dal.GetRecord(ViewState["StrWhere"].ToString());
                this.ddlUserID.SelectedValue = UserID;
                BindData(dt);
            }
            CheckDefaultRole();
            if (IsUser == "1")
                this.ddlUserID.Enabled = false;
        }
    }
    private void BindDropDownList()
    {
        Js.BLL.Account.UserDal dal = new Js.BLL.Account.UserDal();
        DataTable dt = dal.GetRecord("UserName<>'administrator'");
        this.ddlUserID.DataSource = dt;
        this.ddlUserID.DataTextField = "UserName";
        this.ddlUserID.DataValueField = "UserID";
        this.ddlUserID.DataBind();
    }
    private void BindRoles()
    {
        Js.BLL.BaseDal dal = new Js.BLL.BaseDal("BU_Role");
        DataTable dtRole = dal.GetRecord("1=1");

        this.GridView1.DataSource = dtRole.DefaultView;
        this.GridView1.DataBind();

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
        }
    }
    private void CheckDefaultRole()
    {
        for (int j = 0; j < this.GridView1.Rows.Count; j++)
        {
            CheckBox oChk = (CheckBox)this.GridView1.Rows[j].FindControl("cbSelect");
            oChk.Checked = false;
        }

        Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID);
        dtSub = dal.GetSubDetail(this.ddlUserID.SelectedValue).Tables[0];

        for (int i = 0; i < dtSub.Rows.Count; i++)
        {
            for (int j = 0; j < this.GridView1.Rows.Count; j++)
            {
                CheckBox oChk = (CheckBox)this.GridView1.Rows[j].FindControl("cbSelect");
                if (dtSub.Rows[i]["RoleID"].Equals(this.GridView1.DataKeys[j].Value))
                {
                    oChk.Checked = true;
                    break;
                }
            }
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        Js.BLL.Account.RoleDal dal = new Js.BLL.Account.RoleDal();
        ArrayList RoleList = new ArrayList();
        for (int i = 0; i < this.GridView1.Rows.Count; i++)
        {
            CheckBox oChk = (CheckBox)this.GridView1.Rows[i].FindControl("cbSelect");
            if (oChk.Checked)
                RoleList.Add(this.GridView1.DataKeys[i].Value);
        }
        dal.UserToRole(int.Parse(this.ddlUserID.SelectedValue), RoleList, Session["User"].ToString());
        if (IsUser == "1")
            Response.Redirect("BU_User.aspx?FormID=" + Server.UrlEncode("BU_User"));
        else
            Response.Redirect("BU_UserToRoles.aspx?FormID=" + Server.UrlEncode(FormID));
    }

    protected void ddlUserID_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["StrWhere"] = string.Format(" UserID={0}", this.ddlUserID.SelectedValue);
        Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID);

        DataTable dt = dal.GetRecord(ViewState["StrWhere"].ToString());
        BindData(dt);

        CheckDefaultRole();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            switch (e.Row.Cells[3].Text)
            {
                case "0":
                    e.Row.Cells[3].Text = Resources.Resource.UserLevel2;
                    break;
                case "1":
                    e.Row.Cells[3].Text = Resources.Resource.UserLevel1;
                    break;
            }  
        }
    }
}