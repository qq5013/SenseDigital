using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class WebUI_Label_StockInView : BasePage
{
    protected string FormID;
    protected string ID;
    string cnKey = "Label";
    protected void Page_Load(object sender, EventArgs e)
    {
        ID = Request.QueryString["ID"] + "";
        FormID = Request.QueryString["FormID"] + "";
        if (!IsPostBack)
        {
            BindDropDownList();
            ViewState["StrWhere"] = string.Format(" BillID='{0}'", ID);
            Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID, cnKey);
            DataTable dt = dal.GetRecord(ViewState["StrWhere"].ToString());
            ViewState["dt"] = dt;
            Js.BLL.Sys.SysComDal sdal = new Js.BLL.Sys.SysComDal(cnKey);

            BindData(dt);
            if (sdal.GetBillCanBeEdit(FormID, ID) > 0)
                this.btnEdit.Enabled = false;

            //this.ddlAntiCounterfeitType.Enabled = false;
            //this.ddlImageLocation.Enabled = false;
            //this.ddlQRContent.Enabled = false;
            //this.ddlBaseMaterial.Enabled = false;
            //this.ddlBeAffixedMaterial.Enabled = false;
            //this.ddlTextureMaterial.Enabled = false;
            //this.ddlGlue.Enabled = false;
        }
    }
    /// <summary>
    /// 綁定查詢
    /// </summary>
    private void BindDropDownList()
    {
        //Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID, cnKey);
        //DataTable dt = dal.GetRecord("1=1");
        //DataView dv = dt.DefaultView;
        //DataTable ddt = dv.ToTable(true, "AntiCounterfeitType");
        //this.ddlAntiCounterfeitType.DataSource = ddt;
        //this.ddlAntiCounterfeitType.DataTextField = "AntiCounterfeitType";
        //this.ddlAntiCounterfeitType.DataValueField = "AntiCounterfeitType";
        //this.ddlAntiCounterfeitType.DataBind();

        //ddt = dv.ToTable(true, "ImageLocation");
        //this.ddlImageLocation.DataSource = ddt;
        //this.ddlImageLocation.DataTextField = "ImageLocation";
        //this.ddlImageLocation.DataValueField = "ImageLocation";
        //this.ddlImageLocation.DataBind();
        ////this.ddlImageLocation.Items.Insert(0, new ListItem("A. 透過QR碼定位", "A. 透過QR碼定位"));
        ////this.ddlImageLocation.Items.Insert(0, new ListItem("A. 透過QR碼定位", "A. 透過QR碼定位"));
        ////this.ddlImageLocation.Items.Insert(0, new ListItem("A. 透過QR碼定位", "A. 透過QR碼定位"));
        ////this.ddlImageLocation.Items.Insert(0, new ListItem("A. 透過QR碼定位", "A. 透過QR碼定位"));
        //ddt = dv.ToTable(true, "QRContent");
        //this.ddlQRContent.DataSource = ddt;
        //this.ddlQRContent.DataTextField = "QRContent";
        //this.ddlQRContent.DataValueField = "QRContent";
        //this.ddlQRContent.DataBind();

        //ddt = dv.ToTable(true, "BaseMaterial");
        //this.ddlBaseMaterial.DataSource = ddt;
        //this.ddlBaseMaterial.DataTextField = "BaseMaterial";
        //this.ddlBaseMaterial.DataValueField = "BaseMaterial";
        //this.ddlBaseMaterial.DataBind();

        //ddt = dv.ToTable(true, "BeAffixedMaterial");
        //this.ddlBeAffixedMaterial.DataSource = ddt;
        //this.ddlBeAffixedMaterial.DataTextField = "BeAffixedMaterial";
        //this.ddlBeAffixedMaterial.DataValueField = "BeAffixedMaterial";
        //this.ddlBeAffixedMaterial.DataBind();

        //ddt = dv.ToTable(true, "TextureMaterial");
        //this.ddlTextureMaterial.DataSource = ddt;
        //this.ddlTextureMaterial.DataTextField = "TextureMaterial";
        //this.ddlTextureMaterial.DataValueField = "TextureMaterial";
        //this.ddlTextureMaterial.DataBind();

        //ddt = dv.ToTable(true, "Glue");
        //this.ddlGlue.DataSource = ddt;
        //this.ddlGlue.DataTextField = "Glue";
        //this.ddlGlue.DataValueField = "Glue";
        //this.ddlGlue.DataBind();
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
            //this.ckbIsConfirmBad.Checked = bool.Parse(dt.Rows[0]["IsConfirmBad"].ToString());//壞品確認 
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

            if (this.txtBU_CheckUserName.Text.Length > 0)
            {
                this.btnEdit.Enabled = false;
                this.btnDelete.Enabled = false;
                this.btnCheck.Text = Resources.Resource.BU_UnCheck;
            }
            else
            {
                this.btnEdit.Enabled = true;
                this.btnDelete.Enabled = true;
                this.btnCheck.Text = Resources.Resource.BU_Check;
            }
        }
        Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID, cnKey);
        DataSet ds = dal.GetSubDetail(dt.Rows[0]["BillID"].ToString());
        GridView1.DataSource = ds.Tables[0];
        GridView1.DataBind();

        ScriptManager.RegisterStartupScript(this.updatePanel, this.GetType(), "Resize", "content_resize();initial();", true);
    }
    protected void btnFirst_Click(object sender, EventArgs e)
    {
        Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID, cnKey);
        DataTable dt = dal.GetRecord("F", this.txtBillID.Text);
        BindData(dt);
    }
    protected void btnPre_Click(object sender, EventArgs e)
    {
        Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID, cnKey);
        DataTable dt = dal.GetRecord("P", this.txtBillID.Text);
        if (dt != null)
            BindData(dt);
    }
    protected void btnNext_Click(object sender, EventArgs e)
    {
        Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID, cnKey);
        DataTable dt = dal.GetRecord("N", this.txtBillID.Text);
        if (dt != null)
            BindData(dt);
    }
    protected void btnLast_Click(object sender, EventArgs e)
    {
        Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID, cnKey);
        DataTable dt = dal.GetRecord("L", this.txtBillID.Text);
        BindData(dt);
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {

    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID, cnKey);
        //Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID);
        string strID = this.txtBillID.Text;
        dal.Delete(strID);//, "EnterpriseID='" + this.txtEnterpriseID.Text + "'");
        dal.DeleteDetail(strID);
        btnNext_Click(sender, e);
        if (this.txtBillID.Text == strID)
            btnPre_Click(sender, e);
        if (this.txtBillID.Text == strID)
            Response.Redirect("StockIns.aspx?FormID=" + Server.UrlEncode(FormID));
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {

    }
    protected void btnCheck_Click(object sender, EventArgs e)
    {
        DataRow dr = ((DataTable)ViewState["dt"]).Rows[0];
        Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID, cnKey);

        if (this.txtBU_CheckUserName.Text.Length > 0)
        {
            dr["BU_CheckUserName"] = "";
            dr["BU_CheckDate"] = DBNull.Value;
        }
        else
        {
            dr["BU_CheckUserName"] = Session["User"].ToString();
            dr["BU_CheckDate"] = DateTime.Now;
        }
        dal.Update(dr, ID);
        DataTable dt = dal.GetRecord(ViewState["StrWhere"].ToString());

        BindData(dt);
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {

    }
}