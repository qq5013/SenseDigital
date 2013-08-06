using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class WebUI_Label_EnableLabelNoView : BasePage
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

            if (Session["UserType"] == "BU")
            {
                this.btnEPCheck.Visible = false;
                //this.txtEP_CheckUserName.Width = "90%";
            }
            else
            {
                this.btnOnShelf.Visible = false;
                this.btnBUCheck.Visible = false;
            }
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
            this.txtEnableLabelNoID.Text = dt.Rows[0]["BillID"].ToString(); //上架單號 
            this.txtEnableDate.DateValue = (DateTime)dt.Rows[0]["EnableDate"]; //日期 
            this.txtEnterpriseID.Text = dt.Rows[0]["EnterpriseID"].ToString(); //企業用戶編號 
            this.txtEnterpriseName.Text = dt.Rows[0]["EnterpriseName"].ToString(); //企業用戶名稱 
            this.ddlBillState.SelectedIndex = int.Parse(dt.Rows[0]["BillState"].ToString());//狀態 0未上架、1異常待修正、2檢查OK、3上架中、4已上架 
            this.txtQtyTotal.Text = dt.Rows[0]["QtyTotal"].ToString();//數量合計 
            this.txtCreateDate.Text = dt.Rows[0]["CreateDate"].ToString(); //建檔日期 
            this.txtCreateUserName.Text = dt.Rows[0]["CreateUserName"].ToString(); //建檔人員 
            this.txtLastModifyDate.Text = dt.Rows[0]["LastModifyDate"].ToString(); //異動日期 
            this.txtLastModifyUserName.Text = dt.Rows[0]["LastModifyUserName"].ToString(); //異動人員 
            this.txtEP_CheckDate.Text = dt.Rows[0]["EP_CheckDate"].ToString(); //建檔日期 
            this.txtEP_CheckUserName.Text = dt.Rows[0]["EP_CheckUserName"].ToString(); //營運覆核 
            this.txtBU_CheckDate.Text = dt.Rows[0]["BU_CheckDate"].ToString(); //營運覆核日期 
            this.txtBU_CheckUserName.Text = dt.Rows[0]["BU_CheckUserName"].ToString(); //營運覆核用戶 

            if (this.txtBU_CheckUserName.Text.Length > 0)
            {
                this.btnEdit.Enabled = false;
                this.btnDelete.Enabled = false;
                this.btnBUCheck.Text = Resources.Resource.BU_UnCheck;
            }
            else
            {
                this.btnEdit.Enabled = true;
                this.btnDelete.Enabled = false;
                this.btnBUCheck.Text = Resources.Resource.BU_Check;
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
        DataTable dt = dal.GetRecord("F", this.txtEnableLabelNoID.Text);
        BindData(dt);
    }
    protected void btnPre_Click(object sender, EventArgs e)
    {
        Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID, cnKey);
        DataTable dt = dal.GetRecord("P", this.txtEnableLabelNoID.Text);
        if (dt != null)
            BindData(dt);
    }
    protected void btnNext_Click(object sender, EventArgs e)
    {
        Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID, cnKey);
        DataTable dt = dal.GetRecord("N", this.txtEnableLabelNoID.Text);
        if (dt != null)
            BindData(dt);
    }
    protected void btnLast_Click(object sender, EventArgs e)
    {
        Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID, cnKey);
        DataTable dt = dal.GetRecord("L", this.txtEnableLabelNoID.Text);
        BindData(dt);
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {

    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {

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