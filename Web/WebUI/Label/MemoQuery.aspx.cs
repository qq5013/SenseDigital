using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class WebUI_Label_MemoQuery : BasePage
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
        this.txtDescription.Text = dt.Rows[0]["Description"].ToString();

        //HdnSubDetail1.Value = Js.Com.JsonHelper.Dtb2Json(new Js.DAO.Label.StyleDao(FormID, cnKey).GetLimitedProduct(ID));
    }
}