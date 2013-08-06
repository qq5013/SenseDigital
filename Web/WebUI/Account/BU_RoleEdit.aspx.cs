using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Js.BLL.Account;

public partial class WebUI_Account_BU_RoleEdit : BasePage
{
    protected string FormID;
    protected string RoleID;

    protected void Page_Load(object sender, EventArgs e)
    {
        FormID = Request.QueryString["FormID"] + "";
        RoleID = Request.QueryString["NodeUser"] + "";
        if (!IsPostBack)
        {
            ViewState["StrWhere"] = string.Format(" RoleID={0}", RoleID);
            Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID);
            if (RoleID != "")
            {
                DataTable dt = dal.GetRecord(ViewState["StrWhere"].ToString());
                BindData(dt);
            }
            else
            {
                this.txtLastModifyUserName.Text = Session["User"].ToString();
                this.txtLastModifyDate.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm");
                this.txtCreateUserName.Text = Session["User"].ToString();
                this.txtCreateDate.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm");
            }
        }
    }
    
    private void BindData(DataTable dt)
    {
        if (dt.Rows.Count > 0)
        {
            this.txtRoleID.Text = dt.Rows[0]["RoleID"].ToString();
            this.txtRoleName.Text = dt.Rows[0]["RoleName"].ToString();
            this.ddlUserLevel.SelectedIndex = int.Parse(dt.Rows[0]["UserLevel"].ToString());
            this.txtCreateUserName.Text = dt.Rows[0]["CreateUserName"].ToString();
            this.txtCreateDate.Text = Js.Com.PageValidate.ParseDateTime(dt.Rows[0]["CreateDate"].ToString());
            this.txtLastModifyUserName.Text = dt.Rows[0]["LastModifyUserName"].ToString();
            this.txtLastModifyDate.Text = Js.Com.PageValidate.ParseDateTime(dt.Rows[0]["LastModifyDate"].ToString());
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        Js.BLL.Account.RoleDal dal = new Js.BLL.Account.RoleDal(FormID);
        Js.Model.Account.RoleInfo model = new Js.Model.Account.RoleInfo();
        if(RoleID.Length>0)
            model.RoleID = int.Parse(RoleID);
        model.RoleName = this.txtRoleName.Text.Trim();
        model.UserLevel = this.ddlUserLevel.SelectedIndex;
        model.CreateUserName = this.txtCreateUserName.Text;
        if(this.txtCreateDate.Text.Length>0)
            model.CreateDate = DateTime.Parse(this.txtCreateDate.Text);
        else
            model.CreateDate = DateTime.Now;
        model.LastModifyUserName = Session["User"].ToString();
        model.LastModifyDate = DateTime.Now;

        if (RoleID.Length > 0)
            dal.Update(model);
        else
            dal.Add(model);

        Response.Redirect("BU_Role.aspx?FormID=" + Server.UrlEncode(FormID));
    }
}