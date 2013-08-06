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
    
    string cnKey = "Enterprise";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["cnKey"] != null)
            cnKey = Session["cnKey"].ToString();
        FormID = Request.QueryString["FormID"] + "";
        if (!IsPostBack)
        {
            BindDropDownList();
            SetSession();
            this.ddlCategoryID.Enabled = false;
            this.ddlEnableMonths.Enabled = false;
            ViewState["StrWhere"] = string.Format(" EnterpriseID='{0}'", this.ddlEnterpriseID.SelectedValue.ToString());
            Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID);
            DataTable dt = dal.GetRecord(ViewState["StrWhere"].ToString());
            
            BindData(dt);            
        }
    }
    private void BindDropDownList()
    {
        Js.BLL.BaseDal bdal = new Js.BLL.BaseDal("BU_Enterprise");
        string filter = "1=1";
        if (Session["UserType"].ToString() == "EP")
            filter = string.Format("EnterpriseID='{0}'", Session["EnterpriseID"].ToString());
        this.ddlEnterpriseID.DataSource = bdal.GetIDNameList(filter);
        this.ddlEnterpriseID.DataTextField = "IDName";
        this.ddlEnterpriseID.DataValueField = "ID";
        this.ddlEnterpriseID.DataBind();

        if (Session["EnterpriseID"] != null)
            this.ddlEnterpriseID.SelectedValue = Session["EnterpriseID"].ToString();

        Js.BLL.BaseDal dal = new Js.BLL.BaseDal("BU_EnterpriseCategory");
        DataTable dt = dal.GetIDNameList("");
        this.ddlCategoryID.DataSource = dt;
        this.ddlCategoryID.DataTextField = "IDName";
        this.ddlCategoryID.DataValueField = "ID";
        this.ddlCategoryID.DataBind();
    }
    private void SetSession()
    {
        Session["cnKey"] = this.ddlEnterpriseID.SelectedValue.ToString();
        cnKey = Session["cnKey"].ToString();
        Session["EnterpriseID"] = this.ddlEnterpriseID.SelectedValue.ToString();
        Session["EnterpriseName"] = this.ddlEnterpriseID.SelectedItem.Text.Substring(this.ddlEnterpriseID.SelectedItem.Text.IndexOf("  ") + 1);
    }
    private void BindData(DataTable dt)
    {
        if (dt.Rows.Count > 0)
        {
            //this.ddlEnterpriseID.SelectedValue = dt.Rows[0]["EnterpriseID"].ToString();
            this.ddlCategoryID.SelectedValue = dt.Rows[0]["CategoryID"].ToString();
            this.txtEnterpriseName.Text = dt.Rows[0]["EnterpriseName"].ToString();
            this.txtEnterpriseEName.Text = dt.Rows[0]["EnterpriseEName"].ToString();
            this.txtEnterpriseSName.Text = dt.Rows[0]["EnterpriseSName"].ToString();
            this.txtUnionID.Text = dt.Rows[0]["UnionID"].ToString();
            //this.rbtLabelFromYes.Checked = bool.Parse(dt.Rows[0]["LabelFrom"].ToString());
            this.txtPresident.Text = dt.Rows[0]["President"].ToString();
            //this.txtPresidentPost.Text = dt.Rows[0]["PresidentPost"].ToString();
            this.txtPhone.Text = dt.Rows[0]["Phone"].ToString();
            this.txtFax.Text = dt.Rows[0]["Fax"].ToString();
            //this.txtContact.Text = dt.Rows[0]["Contact"].ToString();
            //this.txtContactPost.Text = dt.Rows[0]["ContactPost"].ToString();
            //this.txtContactPhone.Text = dt.Rows[0]["ContactPhone"].ToString();
            //this.txtCellPhone.Text = dt.Rows[0]["CellPhone"].ToString();
            //this.txtEmail.Text = dt.Rows[0]["Email"].ToString();
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
            this.txtCheckDate.Text = Js.Com.PageValidate.ParseDateTime(dt.Rows[0]["CheckDate"].ToString());

            Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID);
            DataTable dtSub = dal.GetSubDetail(this.ddlEnterpriseID.SelectedValue.ToString()).Tables[0];
            this.GridView1.DataSource = dtSub.DefaultView;
            this.GridView1.DataBind();
        }
        ScriptManager.RegisterStartupScript(this.updatePanel, this.GetType(), "Resize", "content_resize();initial();", true);
    }    
    protected void btnPrint_Click(object sender, EventArgs e)
    {

    }
    protected void ddlEnterpriseID_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["StrWhere"] = string.Format(" EnterpriseID='{0}'", this.ddlEnterpriseID.SelectedValue.ToString());
        Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID);
        DataTable dt = dal.GetRecord(ViewState["StrWhere"].ToString());        
        
        BindData(dt);
        SetSession();
    }
}