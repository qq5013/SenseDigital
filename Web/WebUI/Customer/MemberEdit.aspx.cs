using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebUI_Customer_MemberEdit : BasePage
{
    protected string FormID;
    protected string ID;
    protected string cnKey = "Customer";
    protected void Page_Load(object sender, EventArgs e)
    {       
        ID = Request.QueryString["ID"] + "";
        FormID = Request.QueryString["FormID"] + "";
        if (!IsPostBack)
        {
            BindEdll();
            ViewState["StrWhere"] = string.Format(" MemberID='{0}'", ID);
            Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID, cnKey);
            if (ID != "")
            {
                DataTable dt = dal.GetRecord(ViewState["StrWhere"].ToString());
                BindData(dt);
                this.txtMemberID.ReadOnly = true;
            }
            else
            {
                this.txtMemberID.Text = dal.GetMaxID();              
            }

        }
    }
    private void BindEdll()
    {
        Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID, cnKey);
        DataTable dt = dal.GetDistinctRecord("Country");

        this.EddlCountry.DataSource = dt;
        this.EddlCountry.DataTextField = "Country";
        this.EddlCountry.DataValueField = "Country";
        this.EddlCountry.DataBind();

    }
    private void BindData(DataTable dt)
    {

        if (dt.Rows.Count > 0)
        {
            this.txtMemberID.Text = dt.Rows[0]["MemberID"].ToString();
            this.EddlCountry.Text = dt.Rows[0]["Country"].ToString();
            this.txtPassword.Text = dt.Rows[0]["Password"].ToString();
            this.txtPassword.Attributes.Add("value", this.txtPassword.Text);
            this.txtConfirmPwd.Attributes.Add("value", this.txtPassword.Text);
            this.txtPasswordHint.Text = dt.Rows[0]["PasswordHint"].ToString();
            this.txtName.Text = dt.Rows[0]["Name"].ToString();
            if (bool.Parse(dt.Rows[0]["Sex"].ToString()))
                this.ddlSex.SelectedIndex = 1;
            else
                this.ddlSex.SelectedIndex = 0;
            this.txtEMail.Text = dt.Rows[0]["EMail"].ToString();
            this.txtSpareEMail.Text = dt.Rows[0]["SpareEMail"].ToString();
            this.txtBirthday.Text = dt.Rows[0]["Birthday"].ToString();
            this.txtCellPhone.Text = dt.Rows[0]["CellPhone"].ToString();
            this.txtLinkPhone.Text = dt.Rows[0]["LinkPhone"].ToString();
            this.txtZipCode1.Text = dt.Rows[0]["ZipCode1"].ToString();
            this.txtAddress1.Text = dt.Rows[0]["Address1"].ToString();
            this.txtZipCode2.Text = dt.Rows[0]["ZipCode2"].ToString();
            this.txtAddress2.Text = dt.Rows[0]["Address2"].ToString();
            this.txtWebSite.Text = dt.Rows[0]["WebSite"].ToString();
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID, cnKey);
        DataTable dt = dal.GetRecord(ViewState["StrWhere"].ToString());
        DataRow dr = dt.NewRow();
        dr["MemberID"] = this.txtMemberID.Text;
        dr["Country"] = this.EddlCountry.Text;
        dr["Password"] = this.txtPassword.Text.Trim();
        dr["PasswordHint"] = this.txtPasswordHint.Text.Trim();
        dr["Name"] = this.txtName.Text.Trim();
        if (this.ddlSex.SelectedIndex > 0)
            dr["Sex"] = 1;
        else
            dr["Sex"] = 0;
        dr["EMail"] = this.txtEMail.Text;
        dr["SpareEMail"] = this.txtSpareEMail.Text;
        if(this.txtBirthday.Text.Trim().Length>0)
            dr["Birthday"] = this.txtBirthday.Text;
        dr["CellPhone"] = this.txtCellPhone.Text;
        dr["LinkPhone"] = this.txtLinkPhone.Text;
        dr["ZipCode1"] = this.txtZipCode1.Text;
        dr["Address1"] = this.txtAddress1.Text;
        dr["ZipCode2"] = this.txtZipCode2.Text;
        dr["Address2"] = this.txtAddress2.Text;
        dr["WebSite"] = this.txtWebSite.Text;

        if (ID.Length > 0)
            dal.Update(dr, ID);
        else
            dal.Add(dr);

        Response.Redirect("MemberView.aspx?FormID=" + Server.UrlEncode(FormID) + "&ID=" + Server.UrlEncode(this.txtMemberID.Text));
    }
}