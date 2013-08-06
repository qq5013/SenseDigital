using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class WebUI_Label_StylePages : BasePage
{
    protected string FormID;
    protected string ID;
    protected string cnKey = "Label";

    protected void Page_Load(object sender, EventArgs e)
    {
        ID = Request.QueryString["ID"] + "";
        FormID = Request.QueryString["FormID"] + "";

        Js.BLL.Label.StyleDal dal = new Js.BLL.Label.StyleDal(cnKey);
        DataTable dt = dal.GetWarehousePages(ID);


        HdnSubDetail1.Value = Js.Com.JsonHelper.Dtb2Json(dt);

        //ID="sub1xxRowID" Text="(序號),  40,    label,1" 
        InitSubCols(FormID, cnKey, subColsName1.ID, "LB_WarehousePages");
        //InitSubCols(subColsName2.ID, "LB_OrderSub1");  
        writeJsvar(FormID, cnKey, ID);
    }
}