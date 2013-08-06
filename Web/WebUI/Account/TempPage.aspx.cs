using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebUI_Account_TempPage : System.Web.UI.Page
{
    protected string page;
    protected string UserName;
    protected string FormID;
    protected string TitleName;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            page = Request.Params["page"] + "";
            UserName = Request.Params["UserName"] + "";
            FormID = Request.Params["FormID"] + "";
            TitleName = Microsoft.JScript.GlobalObject.unescape(Request.Params["TitleName"] + "");
        }
    }
}