using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Js.BLL.Account;

public partial class WebUI_Account_EP_UserPwdUpdate : BasePage
{
    protected string FormID;
    protected string cnKey = "Enterprise";

    protected void Page_Load(object sender, EventArgs e)
    {
        FormID = Request.QueryString["FormID"];
        if (Session["cnKey"] != null)
            cnKey = Session["cnKey"].ToString();
        if (!IsPostBack)
        {
            BindDropDownList();
            SetSession();
        }
    }
    private void SetSession()
    {
        Session["cnKey"] = this.ddlEnterpriseID.SelectedValue.ToString();
        cnKey = Session["cnKey"].ToString();
        Session["EnterpriseID"] = this.ddlEnterpriseID.SelectedValue.ToString();
        Session["EnterpriseName"] = this.ddlEnterpriseID.SelectedItem.Text.Substring(this.ddlEnterpriseID.SelectedItem.Text.IndexOf("  ") + 1);
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
    protected void ddlEnterpriseID_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetSession();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        UserDal dal = new UserDal();
        dal.SetPassword(Session["User"].ToString(), this.txtPassword.Text);

        ScriptManager.RegisterStartupScript(this.updatePanel, this.GetType(), "Message", "<script type=\"text/javascript\">alert('" + Resources.Resource.UserPwdUpdate_Success + "!');</script>", true);
    }
}