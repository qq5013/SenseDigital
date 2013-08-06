using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebUI_Enterprise_ProductResumeView : BasePage
{
    protected string FormID;
    protected string ID;
    protected string cnKey = "Enterprise";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["cnKey"] != null)
            cnKey = Session["cnKey"].ToString();
        ID = Request.QueryString["ID"] + "";
        FormID = Request.QueryString["FormID"] + "";

        if (!IsPostBack)
        {
            ViewState["StrWhere"] = string.Format(" ResumeID='{0}' and EnterpriseID='{1}'", ID, Session["EnterpriseID"].ToString());
            Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID,cnKey);
            DataTable dt = dal.GetRecord(ViewState["StrWhere"].ToString());
            ViewState["dt"] = dt;
            Js.BLL.Sys.SysComDal sdal = new Js.BLL.Sys.SysComDal(cnKey);

            BindData(dt);
            this.txtProductID.ReadOnly = true;
            this.txtProductName.ReadOnly = true;
            //if (sdal.GetBillCanBeEdit(FormID, ID, "EnterpriseID='" + Session["EnterpriseID"].ToString() + "'") > 0)
            //    this.btnEdit.Enabled = false;

            if (FormID == "EP_ProductResume")
            {
                this.btnSourceView.Enabled = false;
                this.btnDelete.Visible = false;
                this.btnAdd.Visible = false;
            }
            else
            {
                string filter = string.Format("EnterpriseID='{0}'", Session["EnterpriseID"].ToString());
                Js.BLL.BaseDal bdal = new Js.BLL.BaseDal("EP_ProductResume", Session["cnKey"].ToString());
                if (bdal.Exists(this.txtResumeID.Text, filter))
                    this.btnSourceView.Enabled = true;
                else
                    this.btnSourceView.Enabled = false;
                this.ltlTitle.Text = Resources.Resource.EP_ProductResumeNoCheckTitle;
                this.btnModify.Visible = false;
            }
        }
    }
    private void BindData(DataTable dt)
    {
        if (dt.Rows.Count > 0)
        {
            this.txtEnterpriseID.Text = dt.Rows[0]["EnterpriseID"].ToString();
            this.txtEnterpriseName.Text = dt.Rows[0]["EnterpriseName"].ToString();
            this.txtResumeID.Text = dt.Rows[0]["ResumeID"].ToString();
            this.txtProductID.Text = dt.Rows[0]["ProductID"].ToString();
            this.txtProductName.Text = dt.Rows[0]["ProductName"].ToString();
            this.txtProduceDate.Text = dt.Rows[0]["ProduceDate"].ToString();
            this.txtGaranteeDate.Text = dt.Rows[0]["GaranteeDate"].ToString();
            this.ckbHasGarantee.Checked = bool.Parse(dt.Rows[0]["HasGarantee"].ToString());
            this.txtDescription.Text = dt.Rows[0]["Description"].ToString();
            this.txtResumeOfficalUrl.Text = dt.Rows[0]["ResumeOfficalUrl"].ToString();
            //this.lnkResumeOfficalUrl.NavigateUrl = "http://" + dt.Rows[0]["ResumeOfficalUrl"].ToString();
            if (this.txtResumeOfficalUrl.Text.Trim().Length > 0)
            {
                if (this.txtResumeOfficalUrl.Text.ToLower().Trim().IndexOf("http") >= 0)
                    this.lnkResumeOfficalUrl.NavigateUrl = dt.Rows[0]["ResumeOfficalUrl"].ToString();
                else
                    this.lnkResumeOfficalUrl.NavigateUrl = "http://" + dt.Rows[0]["ResumeOfficalUrl"].ToString();
            }
            else
                this.lnkResumeOfficalUrl.Visible = false;
            this.txtOfficalUrl.Text = dt.Rows[0]["OfficalUrl"].ToString();
            if (this.txtOfficalUrl.Text.Trim().Length > 0)
            {
                if (this.txtOfficalUrl.Text.ToLower().Trim().IndexOf("http") >= 0)
                    this.lnkOfficalUrl.NavigateUrl = dt.Rows[0]["OfficalUrl"].ToString();
                else
                    this.lnkOfficalUrl.NavigateUrl = "http://" + dt.Rows[0]["OfficalUrl"].ToString();
            }
            else
                this.lnkOfficalUrl.Visible = false;
            
            this.txtServicePhone.Text = dt.Rows[0]["ServicePhone"].ToString();            
            this.txtCreateUserName.Text = dt.Rows[0]["CreateUserName"].ToString();           
            this.txtCreateDate.Text = Js.Com.PageValidate.ParseDateTime(dt.Rows[0]["CreateDate"].ToString());
            this.txtLastModifyUserName.Text = dt.Rows[0]["LastModifyUserName"].ToString();
            this.txtLastModifyDate.Text = Js.Com.PageValidate.ParseDateTime(dt.Rows[0]["LastModifyDate"].ToString());
            this.txtBU_CheckUserName.Text = dt.Rows[0]["BU_CheckUserName"].ToString();
            this.txtBU_CheckDate.Text = Js.Com.PageValidate.ParseDateTime(dt.Rows[0]["BU_CheckDate"].ToString());
            this.txtEP_CheckUserName.Text = dt.Rows[0]["EP_CheckUserName"].ToString();
            this.txtEP_CheckDate.Text = Js.Com.PageValidate.ParseDateTime(dt.Rows[0]["EP_CheckDate"].ToString());

            if (this.txtEP_CheckUserName.Text.Length > 0)
            {
                //this.btnEdit.Enabled = false;
                this.btnDelete.Enabled = false;
            }
            else
            {
                //this.btnEdit.Enabled = true;
                this.btnDelete.Enabled = true;
            }
            this.txtStopUserName.Text = dt.Rows[0]["StopUserName"].ToString();
            if (this.txtStopUserName.Text.Length > 0)
                this.btnStop.Text = Resources.Resource.UnStop;
            else
                this.btnStop.Text = Resources.Resource.Stop;
            this.txtStopDate.Text = Js.Com.PageValidate.ParseDateTime(dt.Rows[0]["StopDate"].ToString());
            ViewState["dt"] = dt;

        }
        ScriptManager.RegisterStartupScript(this.updatePanel, this.GetType(), "Resize", "content_resize();", true);
    }

    protected void btnFirst_Click(object sender, EventArgs e)
    {
        Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID, cnKey);
        DataTable dt = dal.GetEnterpriseRecord("F", this.txtEnterpriseID.Text, this.txtResumeID.Text);
        BindData(dt);
    }
    protected void btnPre_Click(object sender, EventArgs e)
    {
        Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID, cnKey);
        DataTable dt = dal.GetEnterpriseRecord("P", this.txtEnterpriseID.Text, this.txtResumeID.Text);
        if (dt != null)
            BindData(dt);
    }
    protected void btnNext_Click(object sender, EventArgs e)
    {
        Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID, cnKey);
        DataTable dt = dal.GetEnterpriseRecord("N", this.txtEnterpriseID.Text, this.txtResumeID.Text);
        if (dt != null)
            BindData(dt);
    }
    protected void btnLast_Click(object sender, EventArgs e)
    {
        Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID, cnKey);
        DataTable dt = dal.GetEnterpriseRecord("L", this.txtEnterpriseID.Text, this.txtResumeID.Text);
        BindData(dt);
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {

    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID, cnKey);
        //Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID);
        string strID = this.txtResumeID.Text;
        dal.Delete(strID, "EnterpriseID='" + this.txtEnterpriseID.Text + "'");

        btnNext_Click(sender, e);
        if (this.txtResumeID.Text == strID)
            btnPre_Click(sender, e);

    }
    protected void btnEdit_Click(object sender, EventArgs e)
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
        if (FormID == "EP_ProductResume")
        {
            dal.Update(dr, this.txtResumeID.Text, "EnterpriseID='" + this.txtEnterpriseID.Text + "' and flag=1 ");
            ViewState["StrWhere"] = string.Format(" ResumeID='{0}' and EnterpriseID='{1}' and flag=1", this.txtResumeID.Text, this.txtEnterpriseID.Text);
        }
        else {
            dal.Update(dr, this.txtResumeID.Text, "EnterpriseID='" + this.txtEnterpriseID.Text + "' and flag=2 ");
            ViewState["StrWhere"] = string.Format(" ResumeID='{0}' and EnterpriseID='{1}' and flag=2", this.txtResumeID.Text, this.txtEnterpriseID.Text);
        
        }
        DataTable dt = dal.GetRecord(ViewState["StrWhere"].ToString());

        BindData(dt);
    }
}
