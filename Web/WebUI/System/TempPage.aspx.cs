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

public partial class Kc_Com_ProductData_TempPage : System.Web.UI.Page
{
    protected string page;
    protected string AnnounceID;
    protected string FormID;
    protected string TitleName;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            page = Request.Params["page"];
            AnnounceID = Request.Params["AnnounceID"];
            FormID = Request.Params["FormID"];
            TitleName = Microsoft.JScript.GlobalObject.unescape(Request.Params["TitleName"] + "");
        }

    }
}
