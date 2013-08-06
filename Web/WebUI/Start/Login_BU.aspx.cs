using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.Security;
using System.Web.UI.WebControls;
using Js.BLL.Account;

public partial class WebUI_Start_Login_BU : BaseLanguage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.txtUserName.Text = "administrator";
            this.txtUserPwd.Attributes.Add("value", "admin");
            //this.txtVerifyCode.Text = Session["CheckCode"] + "";            
        }
    }
    protected void Login_Click(object sender, EventArgs e)
    {

        string key = txtUserName.Text.ToLower();
        //Session["User"] = "Administrator";
        //FormsAuthentication.SetAuthCookie("Admin", false);
        //Response.Redirect("~/Start.aspx");
        
        if (Session["CheckCode"].ToString() != this.txtVerifyCode.Text.Trim())
        {
            JScript.Instance.ShowMessage(this.updatePanel, Resources.Resource.InvalidCheckCode);
            return;
        }
        string userName = Js.Com.PageValidate.InputText(this.txtUserName.Text.Trim(), 30);
        string Password = Js.Com.PageValidate.InputText(this.txtUserPwd.Text.Trim(), 30);

        string UserCache = Convert.ToString(Cache[userName]);

        if (UserCache == null || UserCache == string.Empty || Cache[userName].ToString() == Page.Request.UserHostAddress)
        {
            UserPrincipal newUser = UserPrincipal.ValidateLogin(userName, Password);
            if (newUser == null)
            {
                JScript.Instance.ShowMessage(this.updatePanel, Resources.Resource.LoginFailed + userName);
                ScriptManager.RegisterStartupScript(this.updatePanel, this.GetType(), "Reload", "checkwd_reload();", true);
                return;
            }
            else
            {
                UserDal currentUser = new UserDal(newUser);
                Js.Model.Account.UsersInfo model = currentUser.GetModel(userName);
                Context.User = newUser;
                if (((SiteIdentity)User.Identity).TestPassword(Password) == 0)
                {
                    JScript.Instance.ShowMessage(this.updatePanel, Resources.Resource.InvalidPassword);
                }
                else
                {
                    FormsAuthentication.SetAuthCookie(userName, false);

                    Session["UserInfo"] = currentUser;
                    Session["UserLevel"] = model.UserLevel;
                    Session["UserType"] = "BU";
                    Session["User"] = userName;

                    TimeSpan stLogin = new TimeSpan(0, 0, System.Web.HttpContext.Current.Session.Timeout, 0, 0);
                    //HttpContext.Current.Cache.Insert(key, Page.Request.UserHostAddress, null, DateTime.MaxValue, stLogin, System.Web.Caching.CacheItemPriority.NotRemovable, null);

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