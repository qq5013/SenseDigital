using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebUI_BusinessUnit_DepartmentView : BasePage
{
    protected string FormID;
    protected string ID;
    string cnKey = "Enterprise";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["cnKey"] != null)
            cnKey = Session["cnKey"].ToString();
        ID = Request.QueryString["ID"] + "";
        FormID = Request.QueryString["FormID"] + "";
        if (!IsPostBack)
        {
            ViewState["StrWhere"] = string.Format(" DepartmentID='{0}' and EnterpriseID='{1}'", ID, Session["EnterpriseID"].ToString());
            Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID, cnKey);
            DataTable dt = dal.GetRecord(ViewState["StrWhere"].ToString());
           
            ViewState["dt"] = dt;
            Js.BLL.Sys.SysComDal sdal = new Js.BLL.Sys.SysComDal(cnKey);
            
            BindData(dt);
            if (sdal.GetBillCanBeEdit(FormID, ID, "EnterpriseID='" + Session["EnterpriseID"].ToString() + "'") > 0)
                this.btnEdit.Enabled = false;
        }
    }
    private void BindData(DataTable dt)
    {
        
        if (dt.Rows.Count > 0)
        {
            this.txtEnterpriseID.Text = dt.Rows[0]["EnterpriseID"].ToString();
            this.txtEnterpriseName.Text = dt.Rows[0]["EnterpriseName"].ToString();
            this.txtDepartmentID.Text = dt.Rows[0]["DepartmentID"].ToString();
            this.txtDepartmentName.Text = dt.Rows[0]["DepartmentName"].ToString();
            this.txtDepartmentEName.Text = dt.Rows[0]["DepartmentEName"].ToString();
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
            ViewState["dt"] = dt;

        }
    }
    protected void btnFirst_Click(object sender, EventArgs e)
    {
        Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID,cnKey);       
        DataTable dt = dal.GetEnterpriseRecord("F", this.txtEnterpriseID.Text, this.txtDepartmentID.Text);
        BindData(dt);
    }
    protected void btnPre_Click(object sender, EventArgs e)
    {
        Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID, cnKey);
        DataTable dt = dal.GetEnterpriseRecord("P", this.txtEnterpriseID.Text, this.txtDepartmentID.Text);
        if (dt != null)
            BindData(dt);
    }
    protected void btnNext_Click(object sender, EventArgs e)
    {
        Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID, cnKey);
        DataTable dt = dal.GetEnterpriseRecord("N", this.txtEnterpriseID.Text, this.txtDepartmentID.Text);
        if(dt!=null)
            BindData(dt);
    }
    protected void btnLast_Click(object sender, EventArgs e)
    {
        Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID, cnKey);
        DataTable dt = dal.GetEnterpriseRecord("L", this.txtEnterpriseID.Text, this.txtDepartmentID.Text);
        BindData(dt);
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {

    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID, cnKey);
        //Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID);
        string strID = this.txtDepartmentID.Text;
        dal.Delete(strID, "EnterpriseID='" + this.txtEnterpriseID.Text + "'");

        btnNext_Click(sender, e);
        if (this.txtDepartmentID.Text == strID)
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
        dal.Update(dr, this.txtDepartmentID.Text, "EnterpriseID='" + this.txtEnterpriseID.Text + "'");
        ViewState["StrWhere"] = string.Format(" DepartmentID='{0}' and EnterpriseID='{1}'", this.txtDepartmentID.Text, this.txtEnterpriseID.Text);
        DataTable dt = dal.GetRecord(ViewState["StrWhere"].ToString());       

        BindData(dt);
      
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
        dal.Update(dr, this.txtDepartmentID.Text, "EnterpriseID='" + this.txtEnterpriseID.Text + "'");
        ViewState["StrWhere"] = string.Format(" DepartmentID='{0}' and EnterpriseID='{1}'", this.txtDepartmentID.Text, this.txtEnterpriseID.Text);
        DataTable dt = dal.GetRecord(ViewState["StrWhere"].ToString());

        BindData(dt);
    }
}