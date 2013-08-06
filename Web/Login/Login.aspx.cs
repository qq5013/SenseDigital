using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using Js.BLL.Account;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            //this.HiddenField1.Value = Session["CheckCode"].ToString();

            //this.txtCheckCode.Text = Session["CheckCode"].ToString();
        }
    }
    protected void btnLogin_Click(object sender, ImageClickEventArgs e)
    {
        //string userName = Js.Com.PageValidate.InputText(txtUsername.Value.Trim(), 30);
        //string Password = Js.Com.PageValidate.InputText(txtPass.Value.Trim(), 30);

        //string companyID = ddlLanguage.SelectedValue.Substring(8);
        //AccountsPrincipal newUser = AccountsPrincipal.ValidateLogin(userName, Password, companyID);
        //if (newUser == null)
        //{
        //    this.lblMsg.Text = "登錄失敗： " + userName;
        //}
        //else
        //{
        //    User currentUser = new Js.BLL.Account.User(newUser);
        //    Context.User = newUser;
        //    if (((SiteIdentity)User.Identity).TestPassword(Password) == 0)
        //    {
        //        this.lblMsg.Text = "你的密碼無效！";
        //    }
        //    else
        //    {
        //        FormsAuthentication.SetAuthCookie(userName, false);
        //        Js.Com.User.UserID = userName;
        //        Session["UserInfo"] = currentUser;
        //        Session["Style"] = currentUser.Style;
        //        Js.Com.User.CompanyID = ddlLanguage.SelectedValue.ToLower().Replace(Js.Com.User.DataBasePrefix, "");
        //        Session["SysID"] = int.Parse(this.ddlSystem.SelectedValue.ToString());
        //        Session["CompanyID"] = Js.Com.User.CompanyID;
        //        //Js.BLL.Companys.Company bll = new Js.BLL.Companys.Company();
        //        //Js.Com.User.strDataFormat = bll.GetDataFormat(companyID, out Js.Com.User.NumberGigit);
        //        //Js.Com.User.strDateFormat = bll.GetDateFormat(companyID);
        //        //string Format = Js.Com.User.strDateFormat;

        //        //Format = Format.Replace("yyyy", "y").Replace("MM", "mm");
        //        //Format = Format.Replace("yy", "y");
        //        //Js.Com.User.strWebDateFormat = Format;

        //        if (Session["returnPage"] != null)
        //        {
        //            string returnpage = Session["returnPage"].ToString();
        //            Session["returnPage"] = null;
        //            Response.Redirect(returnpage);
        //        }
        //        else
        //        {
        //            //Response.Redirect("Default.aspx");
        //            Response.Write("<script>window.parent.location.href='Start.aspx';</script>");
        //        }
        //    }
        //}
        //if (this.txtCheckCode.Text != Session["CheckCode"])
        //{
        //    this.lblMsg.Text = "驗證碼輸入不正確";
        //    return;
        //}
        FormsAuthentication.SetAuthCookie("admin", false);
        Response.Redirect("~/Index.aspx");
    }

  
}
