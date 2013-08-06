using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.Security;
using System.Web.UI.WebControls;

public partial class WebUI_Start_Login_CT : BaseLanguage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        Session["UserType"] = "CT";
        Session["User"] = "administrator";
        FormsAuthentication.SetAuthCookie("admin", false);
        Response.Redirect("~/Start.aspx");
    }
}