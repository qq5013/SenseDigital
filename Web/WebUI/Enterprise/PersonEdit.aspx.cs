using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebUI_BusinessUnit_PersonEdit : BasePage
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
            ViewState["StrWhere"] = string.Format(" PersonID='{0}' and EnterpriseID='{1}'", ID, Session["EnterpriseID"].ToString());
            BindDropDownList();
            Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID, cnKey);
            if (ID != "")
            {
                DataTable dt = dal.GetRecord(ViewState["StrWhere"].ToString());
                BindData(dt);
                this.txtPersonID.ReadOnly = true;
            }
            else
            {
                this.txtPersonID.Text = dal.GetMaxID("EnterpriseID='" + Session["EnterpriseID"].ToString() + "'");
                this.txtEnterpriseID.Text = Session["EnterpriseID"].ToString();
                this.txtEnterpriseName.Text = Session["EnterpriseName"].ToString();
                this.txtLastModifyUserName.Text = Session["User"].ToString();
                this.txtLastModifyDate.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm");
                this.txtCreateUserName.Text = Session["User"].ToString();
                this.txtCreateDate.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm");
            }
        }
    }
    private void BindDropDownList()
    {
        Js.BLL.BaseDal dal = new Js.BLL.BaseDal("EP_Department",cnKey);
        DataTable dt = dal.GetIDNameList("");
        this.ddlDepartmentID.DataSource = dt;
        this.ddlDepartmentID.DataTextField = "IDName";
        this.ddlDepartmentID.DataValueField = "ID";
        this.ddlDepartmentID.DataBind();
    }
    private void BindData(DataTable dt)
    {

        if (dt.Rows.Count > 0)
        {
            this.txtEnterpriseID.Text = dt.Rows[0]["EnterpriseID"].ToString();
            this.txtEnterpriseName.Text = dt.Rows[0]["EnterpriseName"].ToString();
            this.txtPersonID.Text = dt.Rows[0]["PersonID"].ToString();
            this.ddlDepartmentID.SelectedValue = dt.Rows[0]["DepartmentID"].ToString();
            this.txtPersonName.Text = dt.Rows[0]["PersonName"].ToString();
            this.txtPersonEName.Text = dt.Rows[0]["PersonEName"].ToString();
            //this.txtIDNumber.Text = dt.Rows[0]["IDNumber"].ToString();
            //this.txtBirthday.Text = Js.Com.PageValidate.ParseDate(dt.Rows[0]["Birthday"].ToString());
            //this.txtOnJobDate.Text = Js.Com.PageValidate.ParseDate(dt.Rows[0]["OnJobDate"].ToString());
            //this.txtLeftJobDate.Text = Js.Com.PageValidate.ParseDate(dt.Rows[0]["LeftJobDate"].ToString());
            this.txtPost.Text = dt.Rows[0]["Post"].ToString();
            //this.txtHomePhone.Text = dt.Rows[0]["HomePhone"].ToString();
            this.txtCellPhone.Text = dt.Rows[0]["CellPhone"].ToString();
            this.txtEmail.Text = dt.Rows[0]["Email"].ToString();
            //this.txtLinkMan.Text = dt.Rows[0]["LinkMan"].ToString();
            //this.txtRelation.Text = dt.Rows[0]["Relation"].ToString();
            //this.txtLinkPhone.Text = dt.Rows[0]["LinkPhone"].ToString();
            this.txtContactAddress.Text = dt.Rows[0]["ContactAddress"].ToString();
            //this.txtHomeAddress.Text = dt.Rows[0]["HomeAddress"].ToString();
            this.txtMemo.Text = dt.Rows[0]["Memo"].ToString();
            this.txtCreateUserName.Text = dt.Rows[0]["CreateUserName"].ToString();
            this.txtCreateDate.Text = Js.Com.PageValidate.ParseDateTime(dt.Rows[0]["CreateDate"].ToString());
            this.txtLastModifyUserName.Text = dt.Rows[0]["LastModifyUserName"].ToString();
            this.txtLastModifyDate.Text = Js.Com.PageValidate.ParseDateTime(dt.Rows[0]["LastModifyDate"].ToString());
            this.txtCheckUserName.Text = dt.Rows[0]["CheckUserName"].ToString();
            this.txtCheckDate.Text = Js.Com.PageValidate.ParseDateTime(dt.Rows[0]["CheckDate"].ToString());
            this.txtStopUserName.Text = dt.Rows[0]["StopUserName"].ToString();
            this.txtStopDate.Text = Js.Com.PageValidate.ParseDateTime(dt.Rows[0]["StopDate"].ToString());
            ViewState["dt"] = dt;
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID, cnKey);
        DataTable dt = dal.GetRecord(ViewState["StrWhere"].ToString());
        DataRow dr = dt.NewRow();

        dr["EnterPriseID"] = Session["EnterPriseID"].ToString();
        dr["EnterpriseName"] = Session["EnterpriseName"].ToString();
        dr["PersonID"] = this.txtPersonID.Text;
        dr["DepartmentID"] = this.ddlDepartmentID.SelectedValue.ToString();
        dr["PersonName"] = this.txtPersonName.Text;
        dr["PersonEName"] = this.txtPersonEName.Text.Trim();
        //dr["IDNumber"] = this.txtIDNumber.Text.Trim();
        //if (this.txtBirthday.Text.Length > 0)
        //    dr["Birthday"] = this.txtBirthday.Text;
        dr["Post"] = this.txtPost.Text.Trim();
        //dr["HomePhone"] = this.txtHomePhone.Text.Trim();
        dr["CellPhone"] = this.txtCellPhone.Text.Trim();
        dr["Email"] = this.txtEmail.Text.Trim();
        //if (this.txtOnJobDate.Text.Length > 0)
        //    dr["OnJobDate"] = this.txtOnJobDate.Text;
        //if (this.txtLeftJobDate.Text.Length > 0)
        //    dr["LeftJobDate"] = this.txtLeftJobDate.Text;
        //dr["LinkMan"] = this.txtLinkMan.Text.Trim();
        //dr["Relation"] = this.txtRelation.Text.Trim();
        //dr["LinkPhone"] = this.txtLinkPhone.Text.Trim();
        dr["ContactAddress"] = this.txtContactAddress.Text.Trim();
        //dr["HomeAddress"] = this.txtHomeAddress.Text.Trim();
        dr["Memo"] = this.txtMemo.Text.Trim();
        dr["CreateUserName"] = this.txtCreateUserName.Text;
        dr["CreateDate"] = this.txtCreateDate.Text;
        dr["LastModifyUserName"] = Session["User"].ToString();
        dr["LastModifyDate"] = DateTime.Now.ToString(Js.Com.User.strDateFormat);
        dr["CheckUserName"] = this.txtCheckUserName.Text;
        if (this.txtCheckDate.Text.Length > 0)
            dr["CheckDate"] = this.txtCheckDate.Text;
        dr["StopUserName"] = this.txtStopUserName.Text;
        if (this.txtStopDate.Text.Length > 0)
            dr["StopDate"] = this.txtStopDate.Text;

        if (ID.Length > 0)
            dal.Update(dr, ID, "EnterpriseID='" + this.txtEnterpriseID.Text + "'");
        else
            dal.Add(dr);
        Response.Redirect("PersonView.aspx?FormID=" + Server.UrlEncode(FormID)+ "&ID=" +  Server.UrlEncode(this.txtPersonID.Text));
    }
}