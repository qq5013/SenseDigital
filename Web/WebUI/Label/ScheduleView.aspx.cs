using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class WebUI_Label_ScheduleView : BasePage
{
    protected string FormID;
    protected string ID;
    string cnKey = "Label";
    string _strWhere = " BillID='{0}'";

    protected void Page_Load(object sender, EventArgs e)
    {
        ID = Request.QueryString["ID"] + "";
        FormID = Request.QueryString["FormID"] + "";
        if (!IsPostBack)
        {
            BindDropDownList();
            //ViewState["StrWhere"] = string.Format(" BillID='{0}'", ID);
            Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID, cnKey);
            DataTable dt = dal.GetViewRecord(string.Format(_strWhere, ID));
            Js.BLL.Sys.SysComDal sdal = new Js.BLL.Sys.SysComDal(cnKey);

            BindData(dt);
            if (sdal.GetBillCanBeEdit(FormID, ID) > 0)
                this.btnEdit.Enabled = false;

            if ("" + Session["UserType"] == "EP")
            {
                this.btnBUCheck.Visible = false;
                //    this.GridView1.Columns[7].Visible = false;
                //    this.GridView1.Columns[15].Visible = false;
                //    this.GridView1.Columns[19].Visible = false;
                //    this.GridView1.Columns[24].Visible = false;
                //    this.GridView1.Columns[25].Visible = false;
            }
            //else
            //{
            //    this.btnEPCheck.Visible = false;
            //}
            this.txtTotalPages.Attributes.Add("style", "text-align:right");

            writeJsvar(FormID, cnKey, "");
        }
    }
    /// <summary>
    /// 綁定查詢
    /// </summary>
    private void BindDropDownList()
    {

    }
    private void BindData(DataTable dt)
    {
        ViewState["dt"] = dt;//移動，開始記錄 (更新所用)
        if (dt.Rows.Count > 0)
        {
            //this.txtFlag.SelectedIndex = int.Parse(dt.Rows[0]["Flag"].ToString());//旗標 Flag=1 訂單 Flag=2 生產計劃單 
            this.txtBillID.Text = dt.Rows[0]["BillID"].ToString(); //單號 
            this.txtBillDate.DateValue = (DateTime)dt.Rows[0]["BillDate"]; //日期 
            //this.txtCustomerPO.Text = dt.Rows[0]["CustomerPO"].ToString(); //客戶PO 
            //this.txtEnterpriseID.Text = dt.Rows[0]["EnterpriseID"].ToString(); //企業用戶編號 
            //this.txtEnterpriseName.Text = dt.Rows[0]["EnterpriseName"].ToString(); //企業用戶名稱 
            //this.txtContact.Text = dt.Rows[0]["Contact"].ToString(); //連絡人員 
            //this.txtContactPhone.Text = dt.Rows[0]["ContactPhone"].ToString(); //聯絡電話 
            //this.txtFax.Text = dt.Rows[0]["Fax"].ToString(); //公司傳真 
            //this.txtBusPersonID.Text = dt.Rows[0]["BusPersonID"].ToString(); //業務員編號 
            //this.txtBusPersonName.Text = dt.Rows[0]["BusPersonName"].ToString(); //業務員姓名 
            this.ddlBillState.SelectedIndex = int.Parse(dt.Rows[0]["BillState"].ToString());//單況  0:有效 1:結案 
            if (dt.Rows[0]["BillState"].ToString() == "2")
            {
                this.btnBillState.Enabled = false;
                this.btnEstimate.Enabled = false;
                this.btnEdit.Enabled = false;
                this.btnDelete.Enabled = false;
            }
            //this.txtDeliverCountry.Text = dt.Rows[0]["DeliverCountry"].ToString(); //送貨地址國別 
            //this.txtDeliverAddress.Text = dt.Rows[0]["DeliverAddress"].ToString(); //送貨地址 
            //this.txtDeliverMehtod.Text = dt.Rows[0]["DeliverMehtod"].ToString(); //交貨方式 
            this.txtMemo.Text = dt.Rows[0]["Memo"].ToString(); //備註 
            this.txtTotalPages.Text = string.Format("{0:N0}", dt.Rows[0]["TotalPages"]);//張數合計 
            this.txtCreateDate.Text = ToYMDHM(dt.Rows[0]["CreateDate"]); //建檔日期 
            this.txtCreateUserName.Text = dt.Rows[0]["CreateUserName"].ToString(); //建檔人員 
            this.txtLastModifyDate.Text = ToYMDHM(dt.Rows[0]["LastModifyDate"]); //異動日期 
            this.txtLastModifyUserName.Text = dt.Rows[0]["LastModifyUserName"].ToString(); //異動人員 
            this.txtBU_CheckDate.Text = ToYMDHM(dt.Rows[0]["CheckDate"]); //營運覆核日期 
            this.txtBU_CheckUserName.Text = dt.Rows[0]["CheckUserName"].ToString(); //營運覆核人員  

            if (this.txtBU_CheckUserName.Text.Length > 0)
            {
                this.btnEdit.Enabled = false;
                this.btnDelete.Enabled = false;
                this.btnBillState.Enabled = false;
                this.btnEstimate.Enabled = false;
                this.btnBUCheck.Text = Resources.Resource.BU_UnCheck;
            }
            else
            {
                if (dt.Rows[0]["BillState"].ToString() == "2")
                {
                    this.btnEdit.Enabled = false;
                    this.btnDelete.Enabled = false;
                }
                else
                {
                    this.btnBillState.Enabled = true;
                    this.btnEstimate.Enabled = true;
                    this.btnEdit.Enabled = true;
                    this.btnDelete.Enabled = true;
                    this.btnBUCheck.Text = Resources.Resource.BU_Check;
                }
            }

            Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID, cnKey);
            DataSet ds = dal.GetSubDetail(dt.Rows[0]["BillID"].ToString());
            GridView1.DataSource = ds.Tables[0];
            GridView1.DataBind();
        }

        ScriptManager.RegisterStartupScript(this.updatePanel, this.GetType(), "Resize", "content_resize();initial();", true);
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
        Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID, cnKey);
        //Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID);
        string strID = this.txtBillID.Text;
        dal.Delete(strID);//, "EnterpriseID='" + this.txtEnterpriseID.Text + "'");
        dal.DeleteDetail(strID);
        btnNext_Click(sender, e);
        if (this.txtBillID.Text == strID)
            btnPre_Click(sender, e);
        if (this.txtBillID.Text == strID)
            Response.Redirect("Orders.aspx?FormID=" + Server.UrlEncode(FormID));
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
            dr["CheckUserName"] = "";
            dr["CheckDate"] = DBNull.Value;
        }
        else
        {
            dr["CheckUserName"] = Session["User"].ToString();
            dr["CheckDate"] = DateTime.Now;
        }
        dal.Update(dr, this.txtBillID.Text);
        DataTable dt = dal.GetViewRecord(string.Format(_strWhere, this.txtBillID.Text));

        BindData(dt);
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {

    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //((RadioButton)e.Row.Cells[0].Controls[1]).Attributes.Add("name", "cbsubSelect");//.GroupName = "cbsubSelect";
            switch (e.Row.Cells[6].Text.Trim().ToLower())
            {
                case "false":
                    e.Row.Cells[6].Text = Literal11.Text;
                    break;
                case "true":
                    e.Row.Cells[6].Text = Literal10.Text;
                    break;
            }
        }
    }
    protected void btnOrdPreNO_Click(object sender, EventArgs e)
    {
        Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID, cnKey);
        DataTable dtSub1 = dal.GetSubDetail(this.txtBillID.Text).Tables[0];
        DataTable newdtb = Js.Com.JsonHelper.Json2Dtb(HdnSubDetail1.Value);
        for (int i = 0; i < newdtb.Rows.Count; i++)
        {
            DataRow dr1 = dtSub1.Rows[i];
            for (int j = 0; j < newdtb.Columns.Count; j++)
            {
                dr1[newdtb.Columns[j].ColumnName] = newdtb.Rows[i][j];
            }
        }
        dtSub1.TableName = "LB_OrderSub";
        dal.SaveDetail(dtSub1, this.txtBillID.Text);
        DataTable dt = dal.GetViewRecord(string.Format(_strWhere, this.txtBillID.Text));
        BindData(dt);
        //Response.Redirect("OrderView.aspx?FormID=" + Server.UrlEncode(FormID) + "&ID=" + Server.UrlEncode(this.txtBillID.Text));
    }
    protected void btnBillState_Click(object sender, EventArgs e)
    {
        DataRow dr = ((DataTable)ViewState["dt"]).Rows[0];
        Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID, cnKey);

        if (this.ddlBillState.SelectedIndex == 0)
        {
            dr["BillState"] = 1;
        }
        else
        {
            dr["BillState"] = 0;
        }
        dr["LastModifyDate"] = DateTime.Now;
        dr["LastModifyUserName"] = Session["User"].ToString();

        dal.Update(dr, this.txtBillID.Text);
        DataTable dt = dal.GetViewRecord(string.Format(_strWhere, this.txtBillID.Text));

        BindData(dt);
    }
    protected void btnEstimate_Click(object sender, EventArgs e)
    {
        Js.DAO.Label.OrderDao orddao = new Js.DAO.Label.OrderDao(FormID, cnKey);
        orddao.ChangeScheduleEstimate(this.txtBillID.Text);
        Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID, cnKey);
        DataTable dt = dal.GetViewRecord(string.Format(_strWhere, this.txtBillID.Text));
        BindData(dt);
    }
    protected void btnImageQuery_Click(object sender, EventArgs e)
    {

    }
}