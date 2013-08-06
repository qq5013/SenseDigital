using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebUI_Label_StyleImage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string cnKey = "Label";
        string FormID = Request.QueryString["FormID"] + "";
        string StyleFlag = Request.QueryString["StyleFlag"] + "";
        string StyleID = Request.QueryString["StyleID"] + "";
        if (StyleFlag == "1")
            this.Title = Resources.Resource.LB_StyleImageTitle1;
        else if(StyleFlag == "2")
            this.Title = Resources.Resource.LB_StyleImageTitle2;
        else
            this.Title = Resources.Resource.LB_StyleImageTitle3;

        Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID,cnKey);
        string filter = string.Format("StyleID='{0}'",StyleID);
        DataTable dt = dal.GetRecord(filter);

        Image1.ImageUrl = dt.Rows[0]["ImagePath" + StyleFlag].ToString();
    }
}