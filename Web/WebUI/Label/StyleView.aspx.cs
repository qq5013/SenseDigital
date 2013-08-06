using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class WebUI_Label_StyleView : BasePage
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
            ViewState["StrWhere"] = string.Format(" StyleID='{0}'", ID);
            Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID, cnKey);
            DataTable dt = dal.GetRecord(ViewState["StrWhere"].ToString());
            ViewState["dt"] = dt;
            Js.BLL.Sys.SysComDal sdal = new Js.BLL.Sys.SysComDal(cnKey);

            BindData(dt);
            if (sdal.GetBillCanBeEdit(FormID, ID) > 0)
                this.btnEdit.Enabled = false;

            //this.ddlAntiCounterfeitType.Enabled = false;
            this.ddlImageLocation.Enabled = false;
            this.ddlQRContent.Enabled = false;
            //this.ddlBaseMaterial.Enabled = false;
            //this.ddlBeAffixedMaterial.Enabled = false;
            //this.ddlBeAffixedShape.Enabled = false;
            //this.ddlTextureMaterial.Enabled = false;
            //this.ddlGlue.Enabled = false;

            //this.txtLowQuantity.Attributes.Add("style", "text-align:right");
            //this.txtSparesPercent.Attributes.Add("style", "text-align:right");
            this.txtLength.Attributes.Add("style", "text-align:right");
            this.txtWidth.Attributes.Add("style", "text-align:right");
            this.txtHeight.Attributes.Add("style", "text-align:right");
            writeJsvar(FormID, cnKey, "");
        }
    }
    /// <summary>
    /// 綁定查詢
    /// </summary>
    private void BindDropDownList()
    {
        Js.BLL.Label.StyleDal sdal = new Js.BLL.Label.StyleDal(cnKey);
        DataTable dt = sdal.GetRecord("Flag=2");

        this.ddlImageLocation.DataSource = dt;
        this.ddlImageLocation.DataTextField = "DataText";
        this.ddlImageLocation.DataValueField = "DataValue";
        this.ddlImageLocation.DataBind();

        dt = sdal.GetRecord("Flag=3");
        this.ddlQRContent.DataSource = dt;
        this.ddlQRContent.DataTextField = "DataText";
        this.ddlQRContent.DataValueField = "DataValue";
        this.ddlQRContent.DataBind();
    }
    private void BindData(DataTable dt)
    {

        if (dt.Rows.Count > 0)
        {
            this.txtStyleName.Text = dt.Rows[0]["StyleName"].ToString();            
            this.txtStyleID.Text = dt.Rows[0]["StyleID"].ToString();
            this.txtVolumes.Text = dt.Rows[0]["Volumes"].ToString();
            this.txtStdPages.Text = dt.Rows[0]["StdPages"].ToString();
            this.txtServiceYears.Text = dt.Rows[0]["ServiceYears"].ToString();
            this.txtEnableMonths.Text = dt.Rows[0]["EnableMonths"].ToString();           
            this.ddlImageLocation.SelectedValue = dt.Rows[0]["ImageLocation"].ToString();
            this.txtLength.Text =string.Format("{0:N2}", dt.Rows[0]["Length"]);
            this.txtWidth.Text = string.Format("{0:N2}",dt.Rows[0]["Width"]);
            this.txtHeight.Text = string.Format("{0:N2}",dt.Rows[0]["Height"]);
            this.ddlQRContent.Text = dt.Rows[0]["QRContent"].ToString();
            this.txtProductionNo.Text = dt.Rows[0]["ProductionNo"].ToString();
            this.txtQR_X.Text = string.Format("{0:N2}",dt.Rows[0]["QR_X"]);
            this.txtQR_Y.Text = string.Format("{0:N2}",dt.Rows[0]["QR_Y"]);
            this.txtQR_Length.Text = string.Format("{0:N2}",dt.Rows[0]["QR_Length"]);
            this.txtQR_Width.Text = string.Format("{0:N2}",dt.Rows[0]["QR_Width"]);
            this.txtNowVolumes.Text = dt.Rows[0]["NowVolumes"].ToString();
            this.txtNowPages.Text = dt.Rows[0]["NowPages"].ToString();
            this.txtImagePath.Text = dt.Rows[0]["ImagePath"].ToString();
            this.Img1.Src = this.txtImagePath.Text;
            
            this.txtDescription.Text = dt.Rows[0]["Description"].ToString();
            this.txtAntiFakeDesc1.Text = dt.Rows[0]["AntiFakeDesc1"].ToString();
            this.txtImagePath1.Text = dt.Rows[0]["ImagePath1"].ToString();
            this.txtAntiFakeDesc2.Text = dt.Rows[0]["AntiFakeDesc2"].ToString();
            this.txtImagePath2.Text = dt.Rows[0]["ImagePath2"].ToString();
            this.txtAntiFakeDesc3.Text = dt.Rows[0]["AntiFakeDesc3"].ToString();
            this.txtImagePath3.Text = dt.Rows[0]["ImagePath3"].ToString();
            this.txtStopDate.Text = dt.Rows[0]["StopDate"].ToString();
            this.txtCreateUserName.Text = dt.Rows[0]["CreateUserName"].ToString();
            this.txtCreateDate.Text =ToYMDHM( dt.Rows[0]["CreateDate"]);
            this.txtLastModifyUserName.Text = dt.Rows[0]["LastModifyUserName"].ToString();
            this.txtLastModifyDate.Text = ToYMDHM(dt.Rows[0]["LastModifyDate"]);
            this.txtCheckUserName.Text = dt.Rows[0]["CheckUserName"].ToString();
            if (this.txtCheckUserName.Text.Length > 0)
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
            this.txtCheckDate.Text = ToYMDHM(dt.Rows[0]["CheckDate"]);
            this.txtStopUserName.Text = dt.Rows[0]["StopUserName"].ToString();
            if (this.txtStopUserName.Text.Length > 0)
                this.btnStop.Text = Resources.Resource.UnStop;
            else
                this.btnStop.Text = Resources.Resource.Stop;
            this.txtStopDate.Text =ToYMDHM( dt.Rows[0]["StopDate"]);
            ViewState["dt"] = dt;
        }
        ScriptManager.RegisterStartupScript(this.updatePanel, this.GetType(), "Resize", "content_resize();initial();", true);
    }
    protected void btnFirst_Click(object sender, EventArgs e)
    {
        Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID, cnKey);
        DataTable dt = dal.GetRecord("F", this.txtStyleID.Text);
        BindData(dt);
    }
    protected void btnPre_Click(object sender, EventArgs e)
    {
        Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID, cnKey);
        DataTable dt = dal.GetRecord("P", this.txtStyleID.Text);
        if (dt != null)
            BindData(dt);
    }
    protected void btnNext_Click(object sender, EventArgs e)
    {
        Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID, cnKey);
        DataTable dt = dal.GetRecord("N", this.txtStyleID.Text);
        if (dt != null)
            BindData(dt);
    }
    protected void btnLast_Click(object sender, EventArgs e)
    {
        Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID, cnKey);
        DataTable dt = dal.GetRecord("L", this.txtStyleID.Text);
        BindData(dt);
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {

    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID, cnKey);
        //Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID);
        string strID = this.txtStyleID.Text;
        dal.Delete(strID);
        Js.BLL.Label.StyleDal sdal = new Js.BLL.Label.StyleDal(cnKey);
        sdal.DeleteWarehousePagesByStyleID(strID);

        btnNext_Click(sender, e);
        if (this.txtStyleID.Text == strID)
            btnPre_Click(sender, e);

    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {

    }
    protected void btnCheck_Click(object sender, EventArgs e)
    {
        DataRow dr = ((DataTable)ViewState["dt"]).Rows[0];
        Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID, cnKey);

        if (this.txtCheckUserName.Text.Length > 0)
        {
            dr["CheckUserName"] = "";
            dr["CheckDate"] = DBNull.Value;
        }
        else
        {
            dr["CheckUserName"] = Session["User"].ToString();
            dr["CheckDate"] = DateTime.Now;
        }
        dal.Update(dr,this.txtStyleID.Text.Trim());
        ViewState["StrWhere"] = string.Format(" StyleID='{0}'", this.txtStyleID.Text.Trim());
        DataTable dt = dal.GetRecord(ViewState["StrWhere"].ToString());
        BindData(dt);
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {

    }
    protected void btnStop_Click(object sender, EventArgs e)
    {
        DataRow dr = ((DataTable)ViewState["dt"]).Rows[0];
        Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID, cnKey);

        if (this.txtStopUserName.Text.Length > 0)
        {
            dr["StopUserName"] = "";
            dr["StopDate"] = DBNull.Value;
        }
        else
        {
            dr["StopUserName"] = Session["User"].ToString();
            dr["StopDate"] = DateTime.Now;
        }
        //dal.Update(dr, this.txtEnterpriseID.Text.Trim() + "_" + this.txtStyleID1.Text.Trim());
        //ViewState["StrWhere"] = string.Format(" ID='{0}'", this.txtEnterpriseID.Text.Trim() + "_" + this.txtStyleID1.Text.Trim());
        dal.Update(dr,this.txtStyleID.Text.Trim());
        ViewState["StrWhere"] = string.Format(" StyleID='{0}'", this.txtStyleID.Text.Trim());
        DataTable dt = dal.GetRecord(ViewState["StrWhere"].ToString());
        BindData(dt);
    }
    //protected void btnLimitedProduct_Click(object sender, EventArgs e)
    //{
        
    //    //Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID, cnKey);
    //    //DataTable dtSub1 = dal.GetSubDetail("").Tables[0];
    //    DataTable newdtb = Js.Com.JsonHelper.Json2Dtb(HdnSubDetail1.Value);

    //    DataColumn dc;
    //    dc = new DataColumn("ID", typeof(string));
    //    newdtb.Columns.Add(dc);
    //    dc = new DataColumn("StyleID", typeof(string));
    //    newdtb.Columns.Add(dc);
    //    dc = new DataColumn("EnterpriseID", typeof(string));
    //    newdtb.Columns.Add(dc);

    //    for (int i = 0; i < newdtb.Rows.Count; i++)
    //    {
    //        newdtb.Rows[i]["ID"] = txtStyleID.Value;
    //        newdtb.Rows[i]["StyleID"] = this.txtStyleID1.Text;
    //       // newdtb.Rows[i]["EnterpriseID"] = this.txtEnterpriseID.Text;
          
    //    }
    //    newdtb.TableName = "LB_StyleSub";
    //    new Js.DAO.SubBaseDao(cnKey).Save(newdtb, " ID ='" + ID + "'");
    //    ScriptManager.RegisterStartupScript(this.updatePanel, this.GetType(), "Resize", "content_resize();initial();", true);
    //}
    protected void btnStylePages_Click(object sender, EventArgs e)
    {
        Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID, cnKey);
        DataTable dtSub1 = dal.GetSubDetail(this.txtStyleID.Text).Tables[0];
        DataTable newdtb = Js.Com.JsonHelper.Json2Dtb(HdnSubDetail1.Value);
        for (int i = 0; i < newdtb.Rows.Count; i++)
        {
            DataRow dr1 = dtSub1.Rows[i];
            for (int j = 0; j < newdtb.Columns.Count; j++)
            {
                dr1[newdtb.Columns[j].ColumnName] = newdtb.Rows[i][j];
            }
        }
        dtSub1.TableName = "LB_WarehousePages";
        dal.SaveDetail(dtSub1, this.txtStyleID.Text);

    }
}