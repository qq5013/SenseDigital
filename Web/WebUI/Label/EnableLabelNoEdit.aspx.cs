using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class WebUI_Label_EnableLabelNoEdit : BasePage
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
            //this.txtFlag.SelectedIndex = int.Parse(dt.Rows[0]["Flag"].ToString());//旗標 
            this.txtBillID.Text = dt.Rows[0]["BillID"].ToString(); //上架單號 
            this.txtEnableDate.DateValue = (DateTime)dt.Rows[0]["EnableDate"]; //日期 
            this.txtEnterpriseID.Text = dt.Rows[0]["EnterpriseID"].ToString(); //企業用戶編號 
            this.txtEnterpriseName.Text = dt.Rows[0]["EnterpriseName"].ToString(); //企業用戶名稱 
            this.ddlBillState.SelectedIndex = int.Parse(dt.Rows[0]["BillState"].ToString());//狀態 0未上架、1異常待修正、2檢查OK、3上架中、4已上架 
            this.txtQtyTotal.Text =  dt.Rows[0]["QtyTotal"].ToString( );//數量合計 
            this.txtCreateDate.Text = dt.Rows[0]["CreateDate"].ToString(); //建檔日期 
            this.txtCreateUserName.Text = dt.Rows[0]["CreateUserName"].ToString(); //建檔人員 
            this.txtLastModifyDate.Text = dt.Rows[0]["LastModifyDate"].ToString(); //異動日期 
            this.txtLastModifyUserName.Text = dt.Rows[0]["LastModifyUserName"].ToString(); //異動人員 
            this.txtEP_CheckDate.Text = dt.Rows[0]["EP_CheckDate"].ToString(); //建檔日期 
            this.txtEP_CheckUserName.Text = dt.Rows[0]["EP_CheckUserName"].ToString(); //營運覆核 
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
        dr["BillID"] = this.txtBillID.Text;//上架單號 
        if (this.txtEnableDate.Text.Length > 0) dr["EnableDate"] = this.txtEnableDate.DateValue;//日期 
        dr["EnterpriseID"] = this.txtEnterpriseID.Text;//企業用戶編號 
        dr["EnterpriseName"] = this.txtEnterpriseName.Text;//企業用戶名稱 
        dr["BillState"] = this.ddlBillState.SelectedIndex;//狀態 0未上架、1異常待修正、2檢查OK、3上架中、4已上架 
        dr["QtyTotal"] = int.Parse(this.txtQtyTotal.Text);//數量合計 
        if (this.txtCreateDate.Text.Length > 0) dr["CreateDate"] = this.txtCreateDate.Text;//建檔日期 
        dr["CreateUserName"] = this.txtCreateUserName.Text;//建檔人員 
        if (this.txtLastModifyDate.Text.Length > 0) dr["LastModifyDate"] = this.txtLastModifyDate.Text;//異動日期 
        dr["LastModifyUserName"] = this.txtLastModifyUserName.Text;//異動人員 
        if (this.txtEP_CheckDate.Text.Length > 0) dr["EP_CheckDate"] = this.txtEP_CheckDate.Text;//建檔日期 
        dr["EP_CheckUserName"] = this.txtEP_CheckUserName.Text;//營運覆核 
        if (this.txtBU_CheckDate.Text.Length > 0) dr["BU_CheckDate"] = this.txtBU_CheckDate.Text;//營運覆核日期 
        dr["BU_CheckUserName"] = this.txtBU_CheckUserName.Text;//營運覆核用戶 

        if (ID.Length > 0)
            dal.Update(dr, ID);
        else
            dal.Add(dr);

        Response.Redirect("EnableLabelNoView.aspx?FormID=" + Server.UrlEncode(FormID) + "&ID=" + Server.UrlEncode(this.txtBillID.Text));
    }
}