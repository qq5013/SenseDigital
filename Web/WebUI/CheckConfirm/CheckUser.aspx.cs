using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Js.BLL.Account;

public partial class WebUI_CheckConfirm_CheckUser : BasePage
{
    string cnKey = "Enterprise";
    protected void Page_Load(object sender, EventArgs e)
    {
        cnKey = Request.QueryString["cnKey"] + "";
    }
    protected void btnOK_Click(object sender, EventArgs e)
    {
        string Password = Js.Com.PageValidate.InputText(this.txtUserPwd.Text.Trim(), 30);
        byte[] buffer1 = UserPrincipal.EncryptPassword(Password);

        Js.BLL.Account.UserDal dal = new UserDal(cnKey);
        if(dal.UserPwdConfirm(Session["User"].ToString(), buffer1))
            ClientScript.RegisterStartupScript(this.GetType(), "Submit", "<script type=\"text/javascript\">window.parent.returnValue ='1';window.parent.close();</script>");
        else
            ClientScript.RegisterStartupScript(this.GetType(), "Message", "<script type=\"text/javascript\">alert('密碼不符');</script>");
    }
}