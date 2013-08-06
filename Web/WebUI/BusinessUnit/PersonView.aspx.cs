using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebUI_BusinessUnit_PersonView : BasePage
{
    protected string FormID;
    protected string ID;

    protected void Page_Load(object sender, EventArgs e)
    {
        ID = Request.QueryString["ID"] + "";
        FormID = Request.QueryString["FormID"] + "";
        if (!IsPostBack)
        {
            ViewState["StrWhere"] = string.Format(" PersonID='{0}'", ID);
            Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID);
            DataTable dt = dal.GetRecord(ViewState["StrWhere"].ToString());
            ViewState["dt"] = dt;
            Js.BLL.Sys.SysComDal sdal = new Js.BLL.Sys.SysComDal();
            BindDropDownList();
            BindData(dt);
            if (sdal.GetBillCanBeEdit(FormID, ID) > 0)
                this.btnEdit.Enabled = false;

            this.ddlDepartmentID.Enabled = false;
        }
    }
    private void BindDropDownList()
    {
        Js.BLL.BaseDal dal = new Js.BLL.BaseDal("BU_Department");
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
            this.txtPersonID.Text = dt.Rows[0]["PersonID"].ToString();
            this.ddlDepartmentID.SelectedValue = dt.Rows[0]["DepartmentID"].ToString();
            this.txtPersonName.Text = dt.Rows[0]["PersonName"].ToString();
            this.txtPersonEName.Text = dt.Rows[0]["PersonEName"].ToString();
            this.txtIDNumber.Text = dt.Rows[0]["IDNumber"].ToString();
            this.txtBirthday.Text = Js.Com.PageValidate.ParseDate(dt.Rows[0]["Birthday"].ToString());
            this.txtOnJobDate.Text = Js.Com.PageValidate.ParseDate(dt.Rows[0]["OnJobDate"].ToString());
            this.txtLeftJobDate.Text = Js.Com.PageValidate.ParseDate(dt.Rows[0]["LeftJobDate"].ToString());
            this.txtPost.Text = dt.Rows[0]["Post"].ToString();
            this.txtHomePhone.Text = dt.Rows[0]["HomePhone"].ToString();
            this.txtCellPhone.Text = dt.Rows[0]["CellPhone"].ToString();
            this.txtEmail.Text = dt.Rows[0]["Email"].ToString();
            this.txtLinkMan.Text = dt.Rows[0]["LinkMan"].ToString();
            this.txtRelation.Text = dt.Rows[0]["Relation"].ToString();
            this.txtLinkPhone.Text = dt.Rows[0]["LinkPhone"].ToString();
            this.txtContactAddress.Text = dt.Rows[0]["ContactAddress"].ToString();
            this.txtHomeAddress.Text = dt.Rows[0]["HomeAddress"].ToString();
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
            this.txtStopUserName.Text = dt.Rows[0]["StopUserName"].ToString();
            if (this.txtStopUserName.Text.Length > 0)
                this.btnStop.Text = Resources.Resource.UnStop;
            else
                this.btnStop.Text = Resources.Resource.Stop;
            this.txtStopDate.Text = Js.Com.PageValidate.ParseDateTime(dt.Rows[0]["StopDate"].ToString());
        }
        ViewState["StrWhere"] = string.Format(" PersonID='{0}'", this.txtPersonID.Text);
        ViewState["dt"] = dt;
        ScriptManager.RegisterStartupScript(this.updatePanel, this.GetType(), "Resize", "content_resize();", true);
    }
    protected void btnFirst_Click(object sender, EventArgs e)
    {
        Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID);
        DataTable dt = dal.GetRecord("F", this.txtPersonID.Text);
        BindData(dt);
    }
    protected void btnPre_Click(object sender, EventArgs e)
    {
        Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID);
        DataTable dt = dal.GetRecord("P", this.txtPersonID.Text);
        if (dt != null)
            BindData(dt);
    }
    protected void btnNext_Click(object sender, EventArgs e)
    {
        Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID);
        DataTable dt = dal.GetRecord("N", this.txtPersonID.Text);
        if (dt != null)
            BindData(dt);
    }
    protected void btnLast_Click(object sender, EventArgs e)
    {
        Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID);
        DataTable dt = dal.GetRecord("L", this.txtPersonID.Text);
        BindData(dt);
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {

    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID);
        string strID = this.txtPersonID.Text;
        dal.Delete(strID);

        btnNext_Click(sender, e);
        if (this.txtPersonID.Text == strID)
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
        dal.Update(dr, ID);
        DataTable dt = dal.GetRecord(ViewState["StrWhere"].ToString());

        BindData(dt);
    }
    protected void btnStop_Click(object sender, EventArgs e)
    {
        DataRow dr = ((DataTable)ViewState["dt"]).Rows[0];
        Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID);

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
        dal.Update(dr, ID);
        DataTable dt = dal.GetRecord(ViewState["StrWhere"].ToString());

        BindData(dt);
    }
}