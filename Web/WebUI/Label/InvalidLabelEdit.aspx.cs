using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebUI_Label_InvalidLabelEdit : BasePage
{
    protected string FormID;
    protected string BillID;
    protected string cnKey = "Label";
    protected void Page_Load(object sender, EventArgs e)
    {
        BillID = Request.QueryString["BillID"] + "";
        FormID = Request.QueryString["FormID"] + "";
        if (!IsPostBack)
        {
            ViewState["StrWhere"] = string.Format(" BillID='{0}'", BillID);
            Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID, cnKey);
            if (BillID != "")
            {
                DataTable dt = dal.GetRecord(ViewState["StrWhere"].ToString());
                BindData(dt);
                this.txtBillID.ReadOnly = true;
            }
            else
            {
                this.txtBillID.Text = dal.GetMaxID();
                this.txtLastModifyUserName.Text = Session["User"].ToString();
                this.txtLastModifyDate.Text = DateTime.Now.ToString(Js.Com.User.strDateFormat);
                this.txtCreateUserName.Text = Session["User"].ToString();
                this.txtCreateDate.Text = DateTime.Now.ToString(Js.Com.User.strDateFormat);
            }

        }
    }

    private void BindData(DataTable dt)
    {

        if (dt.Rows.Count > 0)
        {
            this.txtEnterpriseID.Text = dt.Rows[0]["EnterPriseID"].ToString();
            this.txtEnterpriseName.Text = dt.Rows[0]["EnterpriseName"].ToString();
            this.txtBillID.Text = dt.Rows[0]["BillID"].ToString();
            this.txtQtyTotal.Text = dt.Rows[0]["QtyTotal"].ToString();
            if (bool.Parse(dt.Rows[0]["BillState"].ToString()))
                this.txtBillState.Text = "已執行";
            else
                this.txtBillState.Text = "未執行";
            this.txtCreateUserName.Text = dt.Rows[0]["CreateUserName"].ToString();
            this.txtCreateDate.Text = dt.Rows[0]["CreateDate"].ToString();
            this.txtLastModifyUserName.Text = dt.Rows[0]["LastModifyUserName"].ToString();
            this.txtLastModifyDate.Text = dt.Rows[0]["LastModifyDate"].ToString();
            this.txtEP_CheckDate.Text = dt.Rows[0]["EP_CheckDate"].ToString();
            this.txtEP_CheckUserName.Text = dt.Rows[0]["EP_CheckUserName"].ToString();
            this.txtBU_CheckDate.Text = dt.Rows[0]["BU_CheckDate"].ToString();
            this.txtBU_CheckUserName.Text = dt.Rows[0]["BU_CheckUserName"].ToString();

            //Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID, cnKey);
            //DataTable dtSub = dal.GetSubDetail(this.txtBillID.Text).Tables[0];
            //this.GridView1.DataSource = dtSub.DefaultView;
            //this.GridView1.DataBind();
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID, cnKey);
        DataTable dt = dal.GetRecord(ViewState["StrWhere"].ToString());
        DataRow dr = dt.NewRow();
        dr["EnterpriseID"] = this.txtEnterpriseID.Text;
        dr["EnterpriseName"] = this.txtEnterpriseName.Text;
        dr["BillID"] = this.txtBillID.Text.Trim();
        dr["QtyTotal"] = this.txtQtyTotal.Text.Trim();
        dr["BillState"] = this.txtBillState.Text.Trim();
        dr["CreateUserName"] = this.txtCreateUserName.Text;
        dr["CreateDate"] = this.txtCreateDate.Text;
        dr["LastModifyUserName"] = Session["User"].ToString();
        dr["LastModifyDate"] = DateTime.Now.ToString(Js.Com.User.strDateFormat);
        dr["EP_CheckUserName"] = this.txtEP_CheckUserName.Text;
        if (this.txtEP_CheckDate.Text.Length > 0)
            dr["EP_CheckDate"] = this.txtEP_CheckDate.Text;
        dr["BU_CheckUserName"] = this.txtBU_CheckUserName.Text;
        if (this.txtBU_CheckDate.Text.Length > 0)
            dr["BU_CheckDate"] = this.txtBU_CheckDate.Text;
        if (ID.Length > 0)
            dal.Update(dr, ID);
        else
            dal.Add(dr);

        Response.Redirect("InvalidLabelView.aspx?FormID=" + Server.UrlEncode(FormID) + "&ID=" + Server.UrlEncode(this.txtBillID.Text));
    }
}