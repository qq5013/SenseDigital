using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Js.BLL.Account;

public partial class WebUI_Account_EP_UserEdit : BasePage
{
    protected string FormID;
    protected string UserName;
    protected string NodeUser;
    protected string cnKey = "Enterprise";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["cnKey"] != null)
            cnKey = Session["cnKey"].ToString();

        UserName = Request.QueryString["UserName"] + "";
        FormID = Request.QueryString["FormID"] + "";
        NodeUser = Request.QueryString["NodeUser"] + "";
        if (!IsPostBack)
        {
            ViewState["StrWhere"] = string.Format(" UserName='{0}'", NodeUser);
            Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID, cnKey);
            this.txtPersonName.Attributes.Add("readonly", "true");
            this.ddlUserLevel.Enabled = false;
            if (UserName != "")
            {
                DataTable dt = dal.GetRecord(ViewState["StrWhere"].ToString());
                BindData(dt);
                this.txtUserName.ReadOnly = true;
                if (this.txtCreateDate.Text.Trim().Length == 0)
                {
                    this.txtCreateUserName.Text = Session["User"].ToString();
                    this.txtCreateDate.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm");
                }
            }
            else
            {
                //if (NodeUser.ToLower() == "supervisor")
                if (Session["User"].ToString().ToLower() == "supervisor")
                    this.ddlUserLevel.SelectedIndex = 1;
                else
                    this.ddlUserLevel.SelectedIndex = 2;
                this.txtUserName.Text = dal.GetMaxID();
                this.txtLastModifyUserName.Text = Session["User"].ToString();
                this.txtLastModifyDate.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm");
                this.txtCreateUserName.Text = Session["User"].ToString();
                this.txtCreateDate.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm");
            }
            BindDropDownList();
            this.ddlEnterpriseID.Enabled = false;
        }
    }

    private void BindDropDownList()
    {
        Js.BLL.BaseDal bdal = new Js.BLL.BaseDal("BU_Enterprise");

        string filter = "1=1";
        if (Session["UserType"].ToString() == "EP")
            filter = string.Format("EnterpriseID='{0}'", Session["EnterpriseID"].ToString());
        this.ddlEnterpriseID.DataSource = bdal.GetIDNameList(filter);
        this.ddlEnterpriseID.DataTextField = "IDName";
        this.ddlEnterpriseID.DataValueField = "ID";
        this.ddlEnterpriseID.DataBind();
        if (Session["EnterpriseID"] != null)
            this.ddlEnterpriseID.SelectedValue = Session["EnterpriseID"].ToString();

        Js.BLL.BaseDal dal = new Js.BLL.BaseDal("EP_Department", cnKey);

        filter = string.Format("EnterpriseID='{0}'", this.ddlEnterpriseID.SelectedValue);
        DataTable dt = dal.GetIDNameList(filter);

        try
        {
            this.ddlDepartmentID.DataSource = dt;
            this.ddlDepartmentID.DataTextField = "IDName";
            this.ddlDepartmentID.DataValueField = "ID";
            this.ddlDepartmentID.DataBind();
        }
        catch { }
    }

    private void BindData(DataTable dt)
    {
        if (dt.Rows.Count > 0)
        {
            this.txtUserID.Text = dt.Rows[0]["UserID"].ToString();
            this.txtUserName.Text = dt.Rows[0]["UserName"].ToString();
            this.txtTrueName.Text = dt.Rows[0]["TrueName"].ToString();
            this.txtPassword.Text = dt.Rows[0]["Password"].ToString();
            this.txtPassword.Attributes.Add("value", this.txtPassword.Text);
            this.txtConfirmPwd.Attributes.Add("value", this.txtPassword.Text);
            this.txtPersonID.Text = dt.Rows[0]["PersonID"].ToString();
            this.txtPersonName.Text = dt.Rows[0]["PersonName"].ToString();
            this.ddlDepartmentID.SelectedValue = dt.Rows[0]["DepartmentID"].ToString();
            this.txtEMail.Text = dt.Rows[0]["EMail"].ToString();
            if (bool.Parse(dt.Rows[0]["Sex"].ToString()))
                this.ddlSex.SelectedIndex = 1;
            else
                this.ddlSex.SelectedIndex = 0;
            this.txtPhone.Text = dt.Rows[0]["Phone"].ToString();
            this.txtCellPhone.Text = dt.Rows[0]["CellPhone"].ToString();
            this.hfdUserLevel.Value = dt.Rows[0]["UserLevel"].ToString();
            this.hfdParentLevel.Value = dt.Rows[0]["ParentLevel"].ToString();
            if (dt.Rows[0]["UserLevel"].ToString() == "1000")
                this.ddlUserLevel.SelectedIndex = 2;
            else if (int.Parse(dt.Rows[0]["UserLevel"].ToString()) % 1000 == 0)
                this.ddlUserLevel.SelectedIndex = 1;
            else
                this.ddlUserLevel.SelectedIndex = 0;
            if (dt.Rows[0]["State"].ToString() == "0")
                this.txtState.Text = Resources.Resource.User_State0;
            else if (dt.Rows[0]["State"].ToString() == "1")
                this.txtState.Text = Resources.Resource.User_State1;
            else
                this.txtState.Text = Resources.Resource.User_State2;

            this.txtEnableDate.Text = Js.Com.PageValidate.ParseDateTime(dt.Rows[0]["EnableDate"].ToString());
            this.txtStopDate.Text = Js.Com.PageValidate.ParseDateTime(dt.Rows[0]["StopDate"].ToString());
            this.txtCreateUserName.Text = dt.Rows[0]["CreateUserName"].ToString();
            this.txtCreateDate.Text = Js.Com.PageValidate.ParseDateTime(dt.Rows[0]["CreateDate"].ToString());
            this.txtLastModifyUserName.Text = dt.Rows[0]["LastModifyUserName"].ToString();
            this.txtLastModifyDate.Text = Js.Com.PageValidate.ParseDateTime(dt.Rows[0]["LastModifyDate"].ToString());
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        Js.BLL.Account.UserDal dal = new Js.BLL.Account.UserDal(cnKey);
        Js.Model.Account.UsersInfo model = new Js.Model.Account.UsersInfo();

        if (UserName.Length > 0)
            model.UserID = int.Parse(this.txtUserID.Text);
        model.UserName = this.txtUserName.Text.Trim();
        model.TrueName = this.txtTrueName.Text.Trim();
        model.Password = UserPrincipal.EncryptPassword(this.txtPassword.Text);
        model.PersonID = this.txtPersonID.Text;
        model.PersonName = this.txtPersonName.Text;
        model.Email = this.txtEMail.Text.Trim();
        model.Phone = this.txtPhone.Text.Trim();
        model.CellPhone = this.txtCellPhone.Text.Trim();
        model.DepartmentID = this.ddlDepartmentID.SelectedValue;

        if (this.hfdUserLevel.Value != "")
        {
            model.UserLevel = int.Parse(this.hfdUserLevel.Value);
            model.ParentLevel = int.Parse(this.hfdParentLevel.Value);
        }
        else
        {
            //DataRow dr = dal.GetUserLevel(NodeUser);
            DataRow dr = dal.GetUserLevel(Session["User"].ToString());
            model.UserLevel = int.Parse(dr[0].ToString());
            model.ParentLevel = int.Parse(dr[1].ToString());
        }
        if (this.ddlSex.SelectedIndex == 0)
            model.Sex = false;
        else
            model.Sex = true;
        model.State = 0;
        if (this.txtEnableDate.Text.Length > 0)
            model.EnableDate = DateTime.Parse(this.txtEnableDate.Text);
        else
            model.EnableDate = null;

        if (this.txtStopDate.Text.Length > 0)
            model.StopDate = DateTime.Parse(this.txtStopDate.Text);
        else
            model.StopDate = null;
        model.CreateUserName = this.txtCreateUserName.Text;
        model.CreateDate = DateTime.Parse(this.txtCreateDate.Text);
        model.LastModifyUserName = Session["User"].ToString();
        model.LastModifyDate = DateTime.Now;

        if (UserName.Length > 0)
            dal.Update(model);
        else
            dal.Add(model);

        Response.Redirect("EP_User.aspx?FormID=" + Server.UrlEncode(FormID));
    }
}