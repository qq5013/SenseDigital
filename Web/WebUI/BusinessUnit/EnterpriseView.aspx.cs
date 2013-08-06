using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebUI_BusinessUnit_EnterpriseView : BasePage
{
    protected string FormID;
    protected string ID;
    protected void Page_Load(object sender, EventArgs e)
    {
        ID = Request.QueryString["ID"] + "";
        FormID = Request.QueryString["FormID"] + "";
        if (!IsPostBack)
        {
            BindDropDownList();
            this.ddlCategoryID.Enabled = false;
            this.ddlEnableMonths.Enabled = false;
            ViewState["StrWhere"] = string.Format(" EnterpriseID='{0}'", ID);
            Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID);
            DataTable dt = dal.GetRecord(ViewState["StrWhere"].ToString());
            ViewState["dt"] = dt;
            Js.BLL.Sys.SysComDal sdal = new Js.BLL.Sys.SysComDal();

            BindData(dt);
            if (sdal.GetBillCanBeEdit(FormID, ID) > 0)
                this.btnEdit.Enabled = false;
        }
    }
    private void BindDropDownList()
    {
        Js.BLL.BaseDal dal = new Js.BLL.BaseDal("BU_EnterpriseCategory");
        DataTable dt = dal.GetIDNameList("");
        this.ddlCategoryID.DataSource = dt;
        this.ddlCategoryID.DataTextField = "IDName";
        this.ddlCategoryID.DataValueField = "ID";
        this.ddlCategoryID.DataBind();
    }
    private void BindData(DataTable dt)
    {
        if (dt.Rows.Count > 0)
        {
            this.txtEnterpriseID.Text = dt.Rows[0]["EnterpriseID"].ToString();
            this.ddlCategoryID.SelectedValue = dt.Rows[0]["CategoryID"].ToString();
            this.txtEnterpriseName.Text = dt.Rows[0]["EnterpriseName"].ToString();
            this.txtEnterpriseEName.Text = dt.Rows[0]["EnterpriseEName"].ToString();
            this.txtEnterpriseSName.Text = dt.Rows[0]["EnterpriseSName"].ToString();
            this.txtUnionID.Text = dt.Rows[0]["UnionID"].ToString();
            this.txtPresident.Text = dt.Rows[0]["President"].ToString();
            this.txtPhone.Text = dt.Rows[0]["Phone"].ToString();
            this.txtFax.Text = dt.Rows[0]["Fax"].ToString();
            this.txtWebUrl.Text = dt.Rows[0]["WebUrl"].ToString();
            if (this.txtWebUrl.Text.Trim().Length > 0)
            {
                if (this.txtWebUrl.Text.ToLower().Trim().IndexOf("http") < 0)
                    this.lnkWebUrl.NavigateUrl = "http://" + dt.Rows[0]["WebUrl"].ToString();
                else
                    this.lnkWebUrl.NavigateUrl = dt.Rows[0]["WebUrl"].ToString();
            }
            else
                this.lnkWebUrl.Visible = false;
            this.txtServiceYears.Text = dt.Rows[0]["ServiceYears"].ToString();
            this.ddlEnableMonths.Text = dt.Rows[0]["EnableMonths"].ToString();

            this.txtAddress.Text = dt.Rows[0]["Address"].ToString();
            this.txtZipNo.Text = dt.Rows[0]["ZipNo"].ToString();
            this.txtMemo.Text = dt.Rows[0]["Memo"].ToString();
            this.txtCreateUserName.Text = dt.Rows[0]["CreateUserName"].ToString();
            this.txtCreateDate.Text = Js.Com.PageValidate.ParseDateTime(dt.Rows[0]["CreateDate"].ToString());
            this.txtLastModifyUserName.Text = dt.Rows[0]["LastModifyUserName"].ToString();
            this.txtLastModifyDate.Text = Js.Com.PageValidate.ParseDateTime(dt.Rows[0]["LastModifyDate"].ToString());
            this.txtCheckUserName.Text = dt.Rows[0]["CheckUserName"].ToString();

            if (this.txtCheckUserName.Text.Length > 0)
            {
                this.btnEdit.Enabled = false;
                this.btnDelete.Enabled = false;
                this.btnCheck.Text = Resources.Resource.UnCheck;
            }
            else
            {
                this.btnEdit.Enabled = true;
                this.btnDelete.Enabled = true;
                this.btnCheck.Text = Resources.Resource.Check;
            }
            this.txtCheckDate.Text = Js.Com.PageValidate.ParseDateTime(dt.Rows[0]["CheckDate"].ToString());

            Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID);
            DataTable dtSub = dal.GetSubDetail(this.txtEnterpriseID.Text).Tables[0];
            this.GridView1.DataSource = dtSub.DefaultView;
            this.GridView1.DataBind();
        }
        ScriptManager.RegisterStartupScript(this.updatePanel, this.GetType(), "Resize", "content_resize();initial();", true);

        ViewState["StrWhere"] = string.Format(" EnterpriseID='{0}'", this.txtEnterpriseID.Text);
        ViewState["dt"] = dt;
    }
    protected void btnFirst_Click(object sender, EventArgs e)
    {
        Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID);
        DataTable dt = dal.GetRecord("F", this.txtEnterpriseID.Text);
        BindData(dt);
    }
    protected void btnPre_Click(object sender, EventArgs e)
    {
        Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID);
        DataTable dt = dal.GetRecord("P", this.txtEnterpriseID.Text);
        if (dt != null)
            BindData(dt);
    }
    protected void btnNext_Click(object sender, EventArgs e)
    {
        Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID);
        DataTable dt = dal.GetRecord("N", this.txtEnterpriseID.Text);
        if (dt != null)
            BindData(dt);
    }
    protected void btnLast_Click(object sender, EventArgs e)
    {
        Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID);
        DataTable dt = dal.GetRecord("L", this.txtEnterpriseID.Text);
        BindData(dt);
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {

    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID);
        string strID = this.txtEnterpriseID.Text;
        dal.Delete(strID);

        btnNext_Click(sender, e);
        if (this.txtEnterpriseID.Text == strID)
            btnPre_Click(sender, e);
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {

    }
    protected void btnCheck_Click(object sender, EventArgs e)
    {
        DataRow dr = ((DataTable)ViewState["dt"]).Rows[0];
        Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID);

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
        dal.Update(dr,this.txtEnterpriseID.Text);
        DataTable dt = dal.GetRecord(ViewState["StrWhere"].ToString());

        BindData(dt);
    }   
}