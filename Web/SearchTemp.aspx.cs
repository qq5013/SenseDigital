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

public partial class SearchTemp : System.Web.UI.Page
{
    protected string FormID = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Params["FormID"] != null)
            FormID = Request.Params["FormID"].ToString();

    }
}
