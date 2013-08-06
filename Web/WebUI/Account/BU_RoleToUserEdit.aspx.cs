using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;

public partial class WebUI_Account_BU_RoleToUserEdit : BasePage
{
    protected string FormID;
    protected string RoleID;
    DataTable dtSub;

    protected void Page_Load(object sender, EventArgs e)
    {
        RoleID = Request.QueryString["RoleID"] + "";
        FormID = Request.QueryString["FormID"] + "";
        if (!IsPostBack)
        {
            BindDropDownList();
            this.ddlUserLevel.Enabled = false;
            BindUsers();
            if (RoleID.Length > 0)
            {
                ViewState["StrWhere"] = string.Format(" RoleID={0}", RoleID);
                Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID);

                DataTable dt = dal.GetRecord(ViewState["StrWhere"].ToString());
                this.ddlRoleID.SelectedValue = RoleID;
                BindData(dt);
            }
            GetUserLevel();
            CheckDefaultUser();
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
    private void BindUsers()
    {
        Js.BLL.BaseDal dal = new Js.BLL.BaseDal("BU_User");
        DataTable dtUser = dal.GetRecord("UserName<>'administrator'");

        this.GridView1.DataSource = dtUser.DefaultView;
        this.GridView1.DataBind();

    }
    private void BindData(DataTable dt)
    {
        if (dt.Rows.Count > 0)
        {
            //this.ddlRoleID.SelectedValue = dt.Rows[0]["RoleID"].ToString();
            //this.ddlUserLevel.SelectedIndex = int.Parse(dt.Rows[0]["RoleUserLevel"].ToString());
            this.txtLastModifyUserName.Text = dt.Rows[0]["LastModifyUserName"].ToString();
            this.txtLastModifyDate.Text = Js.Com.PageValidate.ParseDateTime(dt.Rows[0]["LastModifyDate"].ToString());
        }        
    }
    private void CheckDefaultUser()
    {
        for (int j = 0; j < this.GridView1.Rows.Count; j++)
        {
            CheckBox oChk = (CheckBox)this.GridView1.Rows[j].FindControl("cbSelect");
            oChk.Checked = false;              
        }

        Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID);
        dtSub = dal.GetSubDetail(this.ddlRoleID.SelectedValue).Tables[0];

        for (int i = 0; i < dtSub.Rows.Count; i++)
        {
            for (int j = 0; j < this.GridView1.Rows.Count; j++)
            {
                CheckBox oChk = (CheckBox)this.GridView1.Rows[j].FindControl("cbSelect");
                if (dtSub.Rows[i]["UserID"].Equals(this.GridView1.DataKeys[j].Value))
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
        ArrayList UserList = new ArrayList();
        for (int i = 0; i < this.GridView1.Rows.Count; i++)
        {
            CheckBox oChk = (CheckBox)this.GridView1.Rows[i].FindControl("cbSelect");
            if(oChk.Checked)
                UserList.Add(this.GridView1.DataKeys[i].Value);                
        }
        dal.RoleToUser(int.Parse(this.ddlRoleID.SelectedValue), UserList, Session["User"].ToString());

        Response.Redirect("BU_RoleToUsers.aspx?FormID=" + Server.UrlEncode(FormID));
    }
    private void GetUserLevel()
    {
        Js.BLL.BaseDal dal = new Js.BLL.BaseDal("BU_Role");
        ViewState["StrWhere"] = string.Format(" RoleID={0}", this.ddlRoleID.SelectedValue);
        DataTable dt = dal.GetRecord(ViewState["StrWhere"].ToString());
        if (dt.Rows.Count > 0)
            this.ddlUserLevel.SelectedIndex = int.Parse(dt.Rows[0]["UserLevel"].ToString());            
    }
    protected void ddlRoleID_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["StrWhere"] = string.Format(" RoleID={0}", this.ddlRoleID.SelectedValue);
        Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID);

        DataTable dt = dal.GetRecord(ViewState["StrWhere"].ToString());
        BindData(dt);
        GetUserLevel();
        CheckDefaultUser();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
}