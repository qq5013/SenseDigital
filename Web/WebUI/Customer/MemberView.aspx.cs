using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebUI_Customer_MemberView : BasePage
{
    protected string FormID;
    protected string ID;
    string cnKey = "Customer";
    protected void Page_Load(object sender, EventArgs e)
    {
        ID = Request.QueryString["ID"] + "";
        FormID = Request.QueryString["FormID"] + "";
        if (!IsPostBack)
        {
            BindDropDownList();
            ViewState["StrWhere"] = string.Format(" MemberID='{0}'", ID);
            Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID, cnKey);
            DataTable dt = dal.GetRecord(ViewState["StrWhere"].ToString());
            ViewState["dt"] = dt;
            Js.BLL.Sys.SysComDal sdal = new Js.BLL.Sys.SysComDal(cnKey);

            BindData(dt);
            if (sdal.GetBillCanBeEdit(FormID, ID) > 0)
                this.btnEdit.Enabled = false;
        }
    }
    /// <summary>
    /// 綁定查詢
    /// </summary>
    private void BindDropDownList()
    {
        Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID, cnKey);
        DataTable dt = dal.GetDistinctRecord("Country");
        
        this.ddlCountry.DataSource = dt;
        this.ddlCountry.DataTextField = "Country";
        this.ddlCountry.DataValueField = "Country";
        this.ddlCountry.DataBind();
    }
    private void BindData(DataTable dt)
    {

        if (dt.Rows.Count > 0)
        {
            this.txtMemberID.Text = dt.Rows[0]["MemberID"].ToString();
            this.ddlCountry.SelectedValue = dt.Rows[0]["Country"].ToString();
            this.txtPassword.Text = dt.Rows[0]["Password"].ToString();
            this.txtPassword.Attributes.Add("value", this.txtPassword.Text);
            this.txtPasswordHint.Text = dt.Rows[0]["PasswordHint"].ToString();
            this.txtName.Text = dt.Rows[0]["Name"].ToString();
            if (bool.Parse(dt.Rows[0]["Sex"].ToString()))
                this.ddlSex.SelectedIndex = 1;
            else
                this.ddlSex.SelectedIndex = 0;
            this.txtEMail.Text = dt.Rows[0]["EMail"].ToString();
            this.txtSpareEMail.Text = dt.Rows[0]["SpareEMail"].ToString();
            this.txtBirthday.Text = Js.Com.PageValidate.ParseDateTime(dt.Rows[0]["Birthday"].ToString());
            this.txtCellPhone.Text = dt.Rows[0]["CellPhone"].ToString();
            this.txtLinkPhone.Text = dt.Rows[0]["LinkPhone"].ToString();
            this.txtZipCode1.Text = dt.Rows[0]["ZipCode1"].ToString();
            this.txtAddress1.Text = dt.Rows[0]["Address1"].ToString();
            this.txtZipCode2.Text = dt.Rows[0]["ZipCode2"].ToString();
            this.txtAddress2.Text = dt.Rows[0]["Address2"].ToString();
            this.txtWebSite.Text = dt.Rows[0]["WebSite"].ToString();
            if (this.txtWebSite.Text.Trim().Length > 0)
            {
                if (this.txtWebSite.Text.ToLower().Trim().IndexOf("http") >= 0)
                    this.lnkWebSite.NavigateUrl = "http://" + dt.Rows[0]["WebSite"].ToString();
                else
                    this.lnkWebSite.NavigateUrl = dt.Rows[0]["WebSite"].ToString();
            }
            else
                this.lnkWebSite.Visible = false;
            this.txtCreateDate.Text = Js.Com.PageValidate.ParseDateTime(dt.Rows[0]["CreateDate"].ToString());
            this.txtLastModifyDate.Text = Js.Com.PageValidate.ParseDateTime(dt.Rows[0]["LastModifyDate"].ToString());
        }
        ScriptManager.RegisterStartupScript(this.updatePanel, this.GetType(), "Resize", "content_resize();", true);
    }
    protected void btnFirst_Click(object sender, EventArgs e)
    {
        Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID, cnKey);
        DataTable dt = dal.GetRecord("F", this.txtMemberID.Text);
        BindData(dt);
    }
    protected void btnPre_Click(object sender, EventArgs e)
    {
        Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID, cnKey);
        DataTable dt = dal.GetRecord("P", this.txtMemberID.Text);
        if (dt != null)
            BindData(dt);
    }
    protected void btnNext_Click(object sender, EventArgs e)
    {
        Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID, cnKey);
        DataTable dt = dal.GetRecord("N", this.txtMemberID.Text);
        if (dt != null)
            BindData(dt);
    }
    protected void btnLast_Click(object sender, EventArgs e)
    {
        Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID, cnKey);
        DataTable dt = dal.GetRecord("L", this.txtMemberID.Text);
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
   
}