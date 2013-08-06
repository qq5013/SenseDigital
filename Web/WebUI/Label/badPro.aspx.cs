using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebUI_Label_badPro : BasePage
{
    protected string FormID;
    protected string ID;
    protected string cnKey = "Label";

    protected void Page_Load(object sender, EventArgs e)
    {
        //ID = Request.QueryString["ID"] + "";
        //FormID = Request.QueryString["FormID"] + "";

        //Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID, cnKey);
        //DataSet ds = dal.GetSubDetail(ID);
        //HdnSubDetail1.Value = Js.Com.JsonHelper.Dtb2Json(ds.Tables[0]);
    }
}