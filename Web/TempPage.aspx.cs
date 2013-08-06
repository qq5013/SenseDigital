using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class TempPage : System.Web.UI.Page
{
    protected string FormID;
    protected string Option;
    protected string TitleName;
    protected string strWhere;
    protected string cnKey;

    protected void Page_Load(object sender, EventArgs e)
    {
        FormID = Request.Params["FormID"] + "";
        Option = Request.Params["option"] + "";
        cnKey = Request.Params["cnKey"] + "";

        if (Request.Params["where"] != null)
            strWhere = Request.Params["where"] + "";
        else
            strWhere = "1=1";

        if (Option == "1")
            TitleName = "多選速查";
        else
            TitleName = "單項速查";
    }
}