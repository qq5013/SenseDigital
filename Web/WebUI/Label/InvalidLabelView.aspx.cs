using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebUI_Label_InvalidLabelView : BasePage
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
            ViewState["StrWhere"] = string.Format(" BillID='{0}'", ID);
            Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID, cnKey);
            DataTable dt = dal.GetRecord(ViewState["StrWhere"].ToString());
            ViewState["dt"] = dt;
            Js.BLL.Sys.SysComDal sdal = new Js.BLL.Sys.SysComDal(cnKey);

            BindData(dt);
            if (sdal.GetBillCanBeEdit(FormID, ID) > 0)
                this.btnEdit.Enabled = false;

            if (Session["UserType"] == "BU")
            {
                this.btnEPCheck.Visible = false;
                //this.txtEP_CheckUserName.Width = "90%";
            }
            else
            {
                this.btnInvalid.Visible = false;
                this.btnBUCheck.Visible = false;
            }
        }
    }

    private void BindData(DataTable dt)
    {
        if (dt.Rows.Count > 0)
        {
            this.txtBillID.Text = dt.Rows[0]["BillID"].ToString();
            this.txtEnterpriseID.Text = dt.Rows[0]["EnterpriseID"].ToString();
            this.txtEnterpriseName.Text = dt.Rows[0]["EnterpriseName"].ToString();
            if(bool.Parse(dt.Rows[0]["BillState"].ToString()))
                this.txtBillState.Text = "已執行";
            else
                this.txtBillState.Text = "未執行";
            this.txtQtyTotal.Text = dt.Rows[0]["QtyTotal"].ToString();
            this.txtCreateUserName.Text = dt.Rows[0]["CreateUserName"].ToString();
            this.txtCreateDate.Text = dt.Rows[0]["CreateDate"].ToString();
            this.txtLastModifyUserName.Text = dt.Rows[0]["LastModifyUserName"].ToString();
            this.txtLastModifyDate.Text = dt.Rows[0]["LastModifyDate"].ToString();
            this.txtBU_CheckUserName.Text = dt.Rows[0]["BU_CheckUserName"].ToString();
            if (this.txtBU_CheckUserName.Text.Length > 0)
            {
                this.btnEdit.Enabled = false;
                this.btnBUCheck.Text = Resources.Resource.BU_UnCheck;
            }
            else
            {
                this.btnEdit.Enabled = true;
                this.btnBUCheck.Text = Resources.Resource.BU_Check;
            }
            this.txtBU_CheckDate.Text = dt.Rows[0]["BU_CheckDate"].ToString();
            this.txtEP_CheckUserName.Text = dt.Rows[0]["EP_CheckUserName"].ToString();
            this.txtEP_CheckDate.Text = dt.Rows[0]["EP_CheckDate"].ToString();

            Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID, cnKey);
            DataTable dtSub = dal.GetSubDetail(this.txtBillID.Text).Tables[0];
            this.GridView1.DataSource = dtSub.DefaultView;
            this.GridView1.DataBind();
        }
        ScriptManager.RegisterStartupScript(this.updatePanel, this.GetType(), "Resize", "content_resize();", true);
    }
    protected void btnFirst_Click(object sender, EventArgs e)
    {
        Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID, cnKey);
        DataTable dt = dal.GetRecord("F", this.txtBillID.Text);
        BindData(dt);
    }
    protected void btnPre_Click(object sender, EventArgs e)
    {
        Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID, cnKey);
        DataTable dt = dal.GetRecord("P", this.txtBillID.Text);
        if (dt != null)
            BindData(dt);
    }
    protected void btnNext_Click(object sender, EventArgs e)
    {
        Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID, cnKey);
        DataTable dt = dal.GetRecord("N", this.txtBillID.Text);
        if (dt != null)
            BindData(dt);
    }
    protected void btnLast_Click(object sender, EventArgs e)
    {
        Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID, cnKey);
        DataTable dt = dal.GetRecord("L", this.txtBillID.Text);
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
    protected void btnBUCheck_Click(object sender, EventArgs e)
    {
        DataRow dr = ((DataTable)ViewState["dt"]).Rows[0];
        Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID, cnKey);

        if (this.txtBU_CheckUserName.Text.Length > 0)
        {
            dr["BU_CheckUserName"] = "";
            dr["BU_CheckDate"] = DBNull.Value;
        }
        else
        {
            dr["BU_CheckUserName"] = Session["User"].ToString();
            dr["BU_CheckDate"] = DateTime.Now;
        }
        dal.Update(dr, ID);
        DataTable dt = dal.GetRecord(ViewState["StrWhere"].ToString());

        BindData(dt);
    }


    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int i = e.Row.RowIndex + 1;
            e.Row.Cells[0].Text = i.ToString();
            switch (e.Row.Cells[3].Text.ToString())
            {
                case "0":
                    e.Row.Cells[3].Text = Resources.Resource.NoCheck;
                    break;
                case "1":
                    e.Row.Cells[3].Text = Resources.Resource.Normal;
                    break;                
            }
        }
    }
}