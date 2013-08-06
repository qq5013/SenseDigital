using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class WebUI_Label_StockInEdit : BasePage
{
    protected string FormID;
    protected string ID;
    protected string cnKey = "Label";

    byte Flag = 1;
    protected void Page_Load(object sender, EventArgs e)
    {
        ID = Request.QueryString["ID"] + "";
        FormID = Request.QueryString["FormID"] + "";
        if (!IsPostBack)
        {
            ViewState["StrWhere"] = string.Format(" BillID='{0}'", ID);
            Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID, cnKey);
            if (ID != "")
            {
                DataTable dt = dal.GetRecord(ViewState["StrWhere"].ToString());
                BindData(dt);
                this.txtBillID.ReadOnly = true;
            }
            else
            {
                this.txtBillID.Text = dal.GetMaxID();
                this.txtLastModifyUserName.Text = Session["User"].ToString();
                this.txtLastModifyDate.Text = DateTime.Now.ToString(Js.Com.User.strDateFormat);
                this.txtCreateUserName.Text = Session["User"].ToString();
                this.txtCreateDate.Text = DateTime.Now.ToString(Js.Com.User.strDateFormat);
            }
            BindEdll();
        }
    }
    private void BindEdll()
    {
        //Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID, cnKey);
        //DataTable dt = dal.GetRecord("1=1");
        //DataView dv = dt.DefaultView;
        //DataTable ddt = dv.ToTable(true, "AntiCounterfeitType");
        //this.EddlAntiCounterfeitType.DataSource = ddt;
        //this.EddlAntiCounterfeitType.DataTextField = "AntiCounterfeitType";
        //this.EddlAntiCounterfeitType.DataValueField = "AntiCounterfeitType";
        //this.EddlAntiCounterfeitType.DataBind();

        //ddt = dv.ToTable(true, "ImageLocation");
        //this.EddlImageLocation.DataSource = ddt;
        //this.EddlImageLocation.DataTextField = "ImageLocation";
        //this.EddlImageLocation.DataValueField = "ImageLocation";
        //this.EddlImageLocation.DataBind();
        ////this.ddlImageLocation.Items.Insert(0, new ListItem("A. 透過QR碼定位", "A. 透過QR碼定位"));
        ////this.ddlImageLocation.Items.Insert(0, new ListItem("A. 透過QR碼定位", "A. 透過QR碼定位"));
        ////this.ddlImageLocation.Items.Insert(0, new ListItem("A. 透過QR碼定位", "A. 透過QR碼定位"));
        ////this.ddlImageLocation.Items.Insert(0, new ListItem("A. 透過QR碼定位", "A. 透過QR碼定位"));
        //ddt = dv.ToTable(true, "QRContent");
        //this.EddlQRContent.DataSource = ddt;
        //this.EddlQRContent.DataTextField = "QRContent";
        //this.EddlQRContent.DataValueField = "QRContent";
        //this.EddlQRContent.DataBind();

        //ddt = dv.ToTable(true, "BaseMaterial");
        //this.EddlBaseMaterial.DataSource = ddt;
        //this.EddlBaseMaterial.DataTextField = "BaseMaterial";
        //this.EddlBaseMaterial.DataValueField = "BaseMaterial";
        //this.EddlBaseMaterial.DataBind();

        //ddt = dv.ToTable(true, "BeAffixedMaterial");
        //this.EddlBeAffixedMaterial.DataSource = ddt;
        //this.EddlBeAffixedMaterial.DataTextField = "BeAffixedMaterial";
        //this.EddlBeAffixedMaterial.DataValueField = "BeAffixedMaterial";
        //this.EddlBeAffixedMaterial.DataBind();

        //ddt = dv.ToTable(true, "TextureMaterial");
        //this.EddlTextureMaterial.DataSource = ddt;
        //this.EddlTextureMaterial.DataTextField = "TextureMaterial";
        //this.EddlTextureMaterial.DataValueField = "TextureMaterial";
        //this.EddlTextureMaterial.DataBind();

        //ddt = dv.ToTable(true, "Glue");
        //this.EddlGlue.DataSource = ddt;
        //this.EddlGlue.DataTextField = "Glue";
        //this.EddlGlue.DataValueField = "Glue";
        //this.EddlGlue.DataBind();
    }
    private void BindData(DataTable dt)
    {

        if (dt.Rows.Count > 0)
        {
            this.txtBillID.Text = dt.Rows[0]["BillID"].ToString();
            this.txtBillDate.DateValue = (DateTime)dt.Rows[0]["BillDate"];
            this.txtEnterpriseID.Text = dt.Rows[0]["EnterpriseID"].ToString(); //企業用戶編號 
            this.txtEnterpriseName.Text = dt.Rows[0]["EnterpriseName"].ToString(); //企業用戶名稱 
            this.txtProduceBillID.Text = dt.Rows[0]["ProduceBillID"].ToString(); //單號 
            this.txtOrderBillID.Text = dt.Rows[0]["OrderBillID"].ToString(); //訂單單號 
            //this.txtOrderSubID.SelectedIndex = int.Parse(dt.Rows[0]["OrderSubID"].ToString());//訂單序號 
            this.txtStyleID.Text = dt.Rows[0]["StyleID"].ToString(); //款式編號 
            this.txtLabelMode.Text = dt.Rows[0]["LabelMode"].ToString(); //標籤模式 
            //this.txtRemainQty.Text = dt.Rows[0]["RemainQty"].ToString();//保留數量 
            this.txtRemainStartNo.Text = dt.Rows[0]["RemainStartNo"].ToString(); //保留起始序號 
            this.txtRemainEndNo.Text = dt.Rows[0]["RemainEndNo"].ToString(); //保留終止序號 
            this.txtQtyTotal.Text = dt.Rows[0]["QtyTotal"].ToString();//入庫張數合計 
            this.txtLabelReels.Text = dt.Rows[0]["LabelReels"].ToString();//標籤卷數合計 
            this.ckbIsNoDone.Checked = bool.Parse(dt.Rows[0]["IsNoDone"].ToString());//序號產生 
            this.ckbIsCheckImage.Checked = bool.Parse(dt.Rows[0]["IsCheckImage"].ToString());//圖檔檢查 
            //this.ckbIsConfirmBad.Checked =   bool.Parse(dt.Rows[0]["IsConfirmBad"].ToString());//壞品確認 
            this.ckbIsImportImage.Checked = bool.Parse(dt.Rows[0]["IsImportImage"].ToString());//圖檔匯入 
            this.ckbIsInvalidBad.Checked = bool.Parse(dt.Rows[0]["IsInvalidBad"].ToString());//壞品作廢 
            this.txtInvalidBillID.Text = dt.Rows[0]["InvalidBillID"].ToString(); //作廢單號 
            RadioButton3.Checked = !bool.Parse(dt.Rows[0]["LabelFrom"].ToString());
            RadioButton4.Checked = bool.Parse(dt.Rows[0]["LabelFrom"].ToString());
            //this.ckbBillState.Checked = bool.Parse(dt.Rows[0]["BillState"].ToString());//入庫狀況 
            this.txtMemo.Text = dt.Rows[0]["Memo"].ToString(); //備註 
            this.txtCreateDate.Text = dt.Rows[0]["CreateDate"].ToString(); //建檔日期 
            this.txtCreateUserName.Text = dt.Rows[0]["CreateUserName"].ToString(); //建檔人員 
            this.txtLastModifyDate.Text = dt.Rows[0]["LastModifyDate"].ToString(); //異動日期 
            this.txtLastModifyUserName.Text = dt.Rows[0]["LastModifyUserName"].ToString(); //異動人員 
            //this.txtEP_CheckDate.Text = dt.Rows[0]["EP_CheckDate"].ToString(); //建檔日期 
            //this.txtEP_CheckUserName.Text = dt.Rows[0]["EP_CheckUserName"].ToString(); //營運覆核 
            this.txtBU_CheckDate.Text = dt.Rows[0]["BU_CheckDate"].ToString(); //營運覆核日期 
            this.txtBU_CheckUserName.Text = dt.Rows[0]["BU_CheckUserName"].ToString(); //營運覆核用戶 

            Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID, cnKey);
            DataSet ds = dal.GetSubDetail(dt.Rows[0]["BillID"].ToString());
            HdnSubDetail1.Value = Js.Com.JsonHelper.Dtb2Json(ds.Tables[0]);
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID, cnKey);
        DataTable dt = dal.GetRecord(ViewState["StrWhere"].ToString());
        DataRow dr = dt.NewRow();
        dr["Flag"] = Flag;//旗標 
        dr["BillID"] = this.txtBillID.Text;//單號 
        if (this.txtBillDate.Text.Length > 0) dr["BillDate"] = this.txtBillDate.DateValue;//日期 
        dr["EnterpriseID"] = this.txtEnterpriseID.Text;//企業用戶編號 
        dr["EnterpriseName"] = this.txtEnterpriseName.Text;//企業用戶名稱 
        dr["ProduceBillID"] = this.txtProduceBillID.Text;//單號 
        dr["OrderBillID"] = this.txtOrderBillID.Text;//訂單單號 
        dr["OrderSubID"] = 0;// int.Parse(this.txtOrderSubID.Text);//訂單序號 
        dr["StyleID"] = this.txtStyleID.Text;//款式編號 
        dr["LabelMode"] = this.txtLabelMode.Text;//標籤模式 
        dr["RemainQty"] = 0;// int.Parse(this.txtRemainQty.Text);//保留數量 
        dr["RemainStartNo"] = this.txtRemainStartNo.Text;//保留起始序號 
        dr["RemainEndNo"] = this.txtRemainEndNo.Text;//保留終止序號 
        dr["QtyTotal"] = int.Parse(this.txtQtyTotal.Text);//入庫張數合計 
        dr["LabelReels"] = int.Parse(this.txtLabelReels.Text);//標籤卷數合計 
        dr["IsNoDone"] = this.ckbIsNoDone.Checked;//序號產生 
        dr["IsCheckImage"] = this.ckbIsCheckImage.Checked;//圖檔檢查 
        dr["IsConfirmBad"] = false;// this.ckbIsConfirmBad.Checked;//壞品確認 
        dr["IsImportImage"] = this.ckbIsImportImage.Checked;//圖檔匯入 
        dr["IsInvalidBad"] = this.ckbIsInvalidBad.Checked;//壞品作廢 
        dr["InvalidBillID"] = this.txtInvalidBillID.Text;//作廢單號 
        dr["LabelFrom"] = this.RadioButton4.Checked;//入庫狀況 
        dr["Memo"] = this.txtMemo.Text;//備註 
        if (this.txtCreateDate.Text.Length > 0) dr["CreateDate"] = this.txtCreateDate.Text;//建檔日期 
        dr["CreateUserName"] = this.txtCreateUserName.Text;//建檔人員 
        if (this.txtLastModifyDate.Text.Length > 0) dr["LastModifyDate"] = this.txtLastModifyDate.Text;//異動日期 
        dr["LastModifyUserName"] = this.txtLastModifyUserName.Text;//異動人員 
        //if (this.txtEP_CheckDate.Text.Length > 0) dr["EP_CheckDate"] = this.txtEP_CheckDate.Text;//建檔日期 
        //dr["EP_CheckUserName"] = this.txtEP_CheckUserName.Text;//營運覆核 
        if (this.txtBU_CheckDate.Text.Length > 0) dr["BU_CheckDate"] = this.txtBU_CheckDate.Text;//營運覆核日期 
        dr["BU_CheckUserName"] = this.txtBU_CheckUserName.Text;//營運覆核用戶 


        if (ID.Length > 0)
            dal.Update(dr, ID);
        else
            dal.Add(dr);

        Response.Redirect("StockInView.aspx?FormID=" + Server.UrlEncode(FormID) + "&ID=" + Server.UrlEncode(this.txtBillID.Text));
    }
}