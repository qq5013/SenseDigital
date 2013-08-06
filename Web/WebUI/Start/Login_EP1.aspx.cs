using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.Security;
using System.Web.UI.WebControls;
using Js.BLL.Account;


public partial class WebUI_Start_Login_EP1 : BaseLanguage
{
    string cnKey = "Enterprise";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.txtEnterpriseID.Text = "A0001";
            this.txtUserName.Text = "supervisor";
            this.txtUserPwd.Attributes.Add("value", "admin");
            //this.txtVerifyCode.Text = Session["CheckCode"] + ""; 
            if (Request.QueryString["currentculture"] == "zh-cn")
            {
                this.ltlEpNo.Text = "企业代码";
                this.ltlUserName.Text = "用户登录名";
                this.ltlUserPwd.Text = "用户密码";
                this.ltlVerifyCode.Text = "验证码";
                this.btnLogin.Text = "登录";
                this.btnForgotPwd.Text = "忘了密码";
            }
            else if (Request.QueryString["currentculture"] == "en-us")
            {
                this.ltlEpNo.Text = "Company code";
                this.ltlUserName.Text = "User account";
                this.ltlUserPwd.Text = "Password";
                this.ltlVerifyCode.Text = "Validation code";
                this.btnLogin.Text = "Log in";
                this.btnForgotPwd.Text = "Login help";
                this.btnLogin.Style["font-size"] = "15px";
                this.btnForgotPwd.Style["font-size"] = "15px";
            }
        }
    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        if (Session["CheckCode"].ToString() != this.txtVerifyCode.Text.Trim())
        {
            JScript.Instance.ShowMessage(this.updatePanel, Resources.Resource.InvalidCheckCode);
            return;
        }
        Js.BLL.BusinessUnit.EnterpriseDal dal = new Js.BLL.BusinessUnit.EnterpriseDal();
        Js.Model.BusinessUnit.EnterpriseInfo model = dal.GetModel(this.txtEnterpriseID.Text.Trim());

        if (model.EnterpriseName.Trim().Length <= 0)
        {
            JScript.Instance.ShowMessage(this.updatePanel, Resources.Resource.Login_Enterprise_NotExist);
            return;
        }
        cnKey = model.EnterpriseID;
        string userName = Js.Com.PageValidate.InputText(this.txtUserName.Text.Trim(), 30);
        string Password = Js.Com.PageValidate.InputText(this.txtUserPwd.Text.Trim(), 30);

        string UserCache = Convert.ToString(Cache[userName]);

        if (UserCache == null || UserCache == string.Empty || Cache[userName].ToString() == Page.Request.UserHostAddress)
        {
            UserPrincipal newUser = UserPrincipal.ValidateLogin(userName, Password, cnKey);
            if (newUser == null)
            {
                JScript.Instance.ShowMessage(this.updatePanel, Resources.Resource.LoginFailed + userName);
                ScriptManager.RegisterStartupScript(this.updatePanel, this.GetType(), "Reload", "checkwd_reload();", true);
                return;
            }
            else
            {
                UserDal currentUser = new UserDal(newUser, cnKey);
                Js.Model.Account.UsersInfo userModel = currentUser.GetModel(userName);

                Context.User = newUser;
                if (((SiteIdentity)User.Identity).TestPassword(Password, cnKey) == 0)
                {
                    JScript.Instance.ShowMessage(this.updatePanel, Resources.Resource.InvalidPassword);
                }
                else
                {
                    FormsAuthentication.SetAuthCookie(userName, false);

                    Session["UserInfo"] = currentUser;
                    Session["UserLevel"] = userModel.UserLevel;
                    Session["UserType"] = "EP";
                    Session["User"] = userName;
                    Session["EnterpriseID"] = model.EnterpriseID;
                    Session["EnterpriseName"] = model.EnterpriseName;
                    Session["cnKey"] = model.EnterpriseID;
                    //Session["cnKey"] = "Enterprise";

                    if (Session["returnPage"] != null)
                    {
                        string returnpage = Session["returnPage"].ToString();
                        Session["returnPage"] = null;
                        Response.Redirect(returnpage);
                    }
                    else
                    {
                        Response.Redirect("~/Start.aspx");
                        //Response.Write("<script>window.parent.location.href='Start.aspx';</script>");
                    }
                }
            }

        }
        else
        {
            JScript.Instance.ShowMessage(this.updatePanel, Resources.Resource.TheUserLogined);
            return;
        }
    }


}