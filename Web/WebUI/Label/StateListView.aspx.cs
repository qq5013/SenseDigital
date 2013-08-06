using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class WebUI_Label_StateListView : BasePage
{
    protected string FormID;
    protected string ID;
    protected string cnKey = "Label";
    protected void Page_Load(object sender, EventArgs e)
    {
        ID = Request.QueryString["ID"] + "";
        FormID = Request.QueryString["FormID"] + "";
        if (!IsPostBack)
        {
            BindDropDownList();
            ViewState["StrWhere"] = string.Format(" StateID='{0}'", ID);
            Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID, cnKey);
            DataTable dt = dal.GetRecord(ViewState["StrWhere"].ToString());
            ViewState["dt"] = dt;
            Js.BLL.Sys.SysComDal sdal = new Js.BLL.Sys.SysComDal(cnKey);

            BindData(dt);
            if (sdal.GetBillCanBeEdit(FormID, ID) > 0)
                this.btnEdit.Enabled = false;
        }
    }
    private void BindDropDownList()
    {
        //Js.BLL.BaseDal dal = new Js.BLL.BaseDal("BU_EnterpriseCategory");
        //DataTable dt = dal.GetIDNameList("");
        //this.ddlCategoryID.DataSource = dt;
        //this.ddlCategoryID.DataTextField = "IDName";
        //this.ddlCategoryID.DataValueField = "ID";
        //this.ddlCategoryID.DataBind();
    }
    private void BindData(DataTable dt)
    {
        if (dt.Rows.Count > 0)
        {
            this.txtStateListID.Text = "" + dt.Rows[0]["StateID"];
            this.txtStateName.Text = "" + dt.Rows[0]["StateName"];
            this.ddlStyle.SelectedIndex = int.Parse("" + dt.Rows[0]["Style"]);
            this.txtDescription.Text = "" + dt.Rows[0]["Description"];
            this.chkImageProvider.Checked = bool.Parse(dt.Rows[0]["ImageProvider"].ToString());
            this.txtStopDate.Text =ToYMDHM( dt.Rows[0]["StopDate"]);
            this.txtStopUserName.Text = "" + dt.Rows[0]["StopUserName"];
            this.txtCreateUserName.Text = dt.Rows[0]["CreateUserName"].ToString();
            this.txtCreateDate.Text =ToYMDHM( dt.Rows[0]["CreateDate"]);
            this.txtLastModifyUserName.Text = dt.Rows[0]["LastModifyUserName"].ToString();
            this.txtLastModifyDate.Text = ToYMDHM( dt.Rows[0]["LastModifyDate"]);
            this.txtCheckDate.Text =ToYMDHM( dt.Rows[0]["CheckDate"]);
            this.txtCheckUserName.Text = "" + dt.Rows[0]["CheckUserName"];
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
            //if (this.txtStopUserName.Text.Length > 0)
            //{
            //    this.btnEdit.Enabled = false;
            //    this.btnCheck.Text = Resources.Resource.;
            //}
            //else
            //{
            //    this.btnEdit.Enabled = true;
            //    this.btnCheck.Text = Resources.Resource.Check;
            //}
            this.txtCheckDate.Text =ToYMDHM( dt.Rows[0]["CheckDate"]);
            ViewState["dt"] = dt;

        }
        ScriptManager.RegisterStartupScript(this.updatePanel, this.GetType(), "Resize", "content_resize();initial();", true);
    }
    protected void btnFirst_Click(object sender, EventArgs e)
    {
        Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID, cnKey);
        DataTable dt = dal.GetRecord("F", this.txtStateListID.Text);
        BindData(dt);
    }
    protected void btnPre_Click(object sender, EventArgs e)
    {
        Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID, cnKey);
        DataTable dt = dal.GetRecord("P", this.txtStateListID.Text);
        if (dt != null)
            BindData(dt);
    }
    protected void btnNext_Click(object sender, EventArgs e)
    {
        Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID, cnKey);
        DataTable dt = dal.GetRecord("N", this.txtStateListID.Text);
        if (dt != null)
            BindData(dt);
    }
    protected void btnLast_Click(object sender, EventArgs e)
    {
        Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID, cnKey);
        DataTable dt = dal.GetRecord("L", this.txtStateListID.Text);
        BindData(dt);
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {

    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID, cnKey);
        //Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID);
        string strID = this.txtStateListID.Text;
        dal.Delete(strID);

        btnNext_Click(sender, e);
        if (this.txtStateListID.Text == strID)
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
        dal.Update(dr, this.txtStateListID.Text.Trim());
        ViewState["StrWhere"] = string.Format(" StateID='{0}'", this.txtStateListID.Text.Trim());
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
        dal.Update(dr, this.txtStateListID.Text.Trim());
        ViewState["StrWhere"] = string.Format(" StateID='{0}'", this.txtStateListID.Text.Trim());
        DataTable dt = dal.GetRecord(ViewState["StrWhere"].ToString());       

        BindData(dt);
    }
   
}