using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Js.BLL.Account;

public partial class WebUI_Account_BU_UserPwdUpdate : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        UserDal dal = new UserDal();
        dal.SetPassword(Session["User"].ToString(), this.txtPassword.Text);

        ScriptManager.RegisterStartupScript(this.updatePanel, this.GetType(), "Message", "<script type=\"text/javascript\">alert('" + Resources.Resource.UserPwdUpdate_Success + "!');</script>", true);
    }
}