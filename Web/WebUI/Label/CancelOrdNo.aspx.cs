using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class WebUI_Label_CancelOrdNo : BasePage
{
    protected string FormID;
    protected string ID;
    protected string cnKey = "Label";

    protected void Page_Load(object sender, EventArgs e)
    {
        ID = Request.QueryString["ID"] + "";
        FormID = Request.QueryString["FormID"] + "";

        Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID, cnKey);
        DataTable dt = dal.GetRecord(string.Format(" BillID='{0}'", ID));
        this.txtBillID.Text = ID;
        this.txtEnterpriseID.Text = "" + dt.Rows[0]["EnterpriseID"];
        this.txtEnterpriseName.Text = "" + dt.Rows[0]["EnterpriseName"];

        DataSet ds = dal.GetSubDetail(ID);
        //GridView1.DataSource = ds.Tables[0];
        //GridView1.DataBind();
        HdnSubDetail1.Value = Js.Com.JsonHelper.Dtb2Json(ds.Tables[0]);

        //ID="sub1xxRowID" Text="(序號),  40,    label,1" 
        InitSubCols(FormID, cnKey, subColsName1.ID, "LB_OrderSub");
        //InitSubCols(subColsName2.ID, "LB_OrderSub1");  
        writeJsvar(FormID, cnKey, ID);
    }
}