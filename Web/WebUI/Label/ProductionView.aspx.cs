using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class WebUI_Label_ProductionView : BasePage
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
          
        }
        writeJsvar(FormID, cnKey, "");
    }
    /// <summary>
    /// 綁定查詢
    /// </summary>
    private void BindDropDownList()
    {
        Js.BLL.BaseDal dal = new Js.BLL.BaseDal("LB_ProductionUnit", cnKey);
        DataTable dt = dal.GetIDNameList("");
        this.ddlProductionUnitID.DataSource = dt;
        this.ddlProductionUnitID.DataTextField = "IDName";
        this.ddlProductionUnitID.DataValueField = "ID";
        this.ddlProductionUnitID.DataBind();
    }
    private void BindData(DataTable dt)
    {
        ViewState["dt"] = dt;//移動，開始記錄 (更新所用)
        if (dt.Rows.Count > 0)
        {
            this.txtBillID.Text = dt.Rows[0]["BillID"].ToString(); //單號 
            this.txtBillDate.Text = ToYMDHM(dt.Rows[0]["BillDate"]);
            this.txtStyleID.Text = dt.Rows[0]["StyleID"].ToString();


            this.txtSourceBillID.Text = dt.Rows[0]["SourceBillID"].ToString();

            this.txtProducePages.Text = dt.Rows[0]["ProducePages"].ToString();
            this.txtStdPages.Text = dt.Rows[0]["StdPages"].ToString();
            this.txtVolumes.Text = dt.Rows[0]["Volumes"].ToString();
            this.txtStartLabelNo.Text = dt.Rows[0]["StartLabelNo"].ToString();
            this.txtEndLabelNo.Text = dt.Rows[0]["EndLabelNo"].ToString();
            this.ddlProductionUnitID.SelectedValue = dt.Rows[0]["ProductionUnitID"].ToString();
            this.txtScheduleBillID.Text = dt.Rows[0]["ScheduleBillID"].ToString();
            txtPreInStockDate.Text = ToYMDHM(dt.Rows[0]["PreInStockDate"]);

            this.ddlBillState.SelectedIndex = int.Parse(dt.Rows[0]["BillState"].ToString());

            this.txtCreateDate.Text = ToYMDHM(dt.Rows[0]["CreateDate"]); //建檔日期 
            this.txtCreateUserName.Text = dt.Rows[0]["CreateUserName"].ToString(); //建檔人員 
            this.txtLastModifyDate.Text = ToYMDHM(dt.Rows[0]["LastModifyDate"]); //異動日期 
            this.txtLastModifyUserName.Text = dt.Rows[0]["LastModifyUserName"].ToString(); //異動人員 
            this.txtCheckDate.Text = ToYMDHM(dt.Rows[0]["CheckDate"]); //營運覆核日期 
            this.txtCheckUserName.Text = dt.Rows[0]["CheckUserName"].ToString(); //營運覆核用戶 
            this.txtMemo.Text = dt.Rows[0]["Memo"].ToString();

            if (dt.Rows[0]["BillState"].ToString() == "2")
            {
                this.btnBillState.Enabled = false;
                this.btnEdit.Enabled = false;
                this.btnDelete.Enabled = false;
               
            }
            if (this.txtCheckUserName.Text.Length > 0)
            {
                this.btnEdit.Enabled = false;
                this.btnDelete.Enabled = false;
                this.btnCheck.Text = Resources.Resource.BU_UnCheck;
                this.btnBillState.Enabled = false;
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
                    this.btnEdit.Enabled = true;
                    this.btnDelete.Enabled = true;
                    this.btnBillState.Enabled = true;
                }
                 
                this.btnCheck.Text = Resources.Resource.BU_Check;
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
            Response.Redirect("Productions.aspx?FormID=" + Server.UrlEncode(FormID));
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
        dal.Update(dr, this.txtBillID.Text);
        DataTable dt = dal.GetViewRecord(string.Format(_strWhere, this.txtBillID.Text));

        BindData(dt);
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {

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
        if (((Button)sender).ClientID == "btnCancelOrdNo") new Js.DAO.Label.OrderDao(FormID, cnKey).updateBillState(this.txtBillID.Text);

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
   
 
}