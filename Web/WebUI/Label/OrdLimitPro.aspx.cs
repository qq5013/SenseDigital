using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class WebUI_Label_OrdLimitPro : BasePage
{
    protected string FormID = "LB_Style";
    protected string ID;
    protected string cnKey = "Label";

    protected void Page_Load(object sender, EventArgs e)
    {
        ID = Request.QueryString["ID"] + "";
        txtBillID.Text = Request.QueryString["BillID"] + "";
        Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID, cnKey);

        DataTable dt = dal.GetRecord(string.Format(" ID='{0}'", ID));
        //this.txtEnterpriseID.Text = dt.Rows[0]["EnterPriseID"].ToString();
        //this.txtEnterpriseName.Text = dt.Rows[0]["EnterpriseName"].ToString();
        this.txtStyleID.Text = dt.Rows[0]["StyleID"].ToString();
        this.txtLabelMode.Text = dt.Rows[0]["LabelMode"].ToString();

        HdnSubDetail1.Value = Js.Com.JsonHelper.Dtb2Json(new Js.DAO.Label.StyleDao(cnKey).GetLimitedProduct(ID));

        //一個明細調用一次
        //                       欄位名稱,顯示寬,type ,只讀標記
        //ID="sub1xxRowID" Text="(序號),  40,    label,1" 
        InitSubCols(FormID, cnKey, subColsName1.ID, "LB_StyleSub");
    }
}