using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class WebUI_Label_ProductionEdit : BasePage
{
    protected string FormID;
    protected string ID;
    protected string cnKey = "Label";

    byte Flag = 1;
    protected void Page_Load(object sender, EventArgs e)
    {
        ID = Request.QueryString["BillID"] + "";
        FormID = Request.QueryString["FormID"] + "";
        if (!IsPostBack)
        {
            ViewState["StrWhere"] = string.Format(" BillID='{0}'", ID);
            Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID, cnKey);
            BindEdll();
            if (ID != "")
            {
                DataTable dt = dal.GetViewRecord(ViewState["StrWhere"].ToString());
                BindData(dt);
                this.txtBillID.ReadOnly = true;
                this.txtBillID.CssClass = "TextRead";
                this.txtStyleID.CssClass = "TextRead";
                this.txtProducePages.CssClass = "TextRead";
                this.txtStdPages.CssClass = "TextRead";
                this.btnImageQuery.Enabled = true;
                this.btnStyleQuery.Enabled = true;
            }
            else
            {
                this.txtBillID.Text = dal.GetAutoCode(DateTime.Now);
                this.txtBillDate.DateValue = DateTime.Now;
                this.txtLastModifyUserName.Text = Session["User"].ToString();
                this.txtLastModifyDate.Text = ToYMDHM(DateTime.Now);
                this.txtCreateUserName.Text = Session["User"].ToString();
                this.txtCreateDate.Text = ToYMDHM(DateTime.Now);
                this.ddlBillState.SelectedIndex = 1;
            }
            
           
            BindOther();
        }
    }
    private void BindEdll()
    {
        Js.BLL.BaseDal dal = new Js.BLL.BaseDal("LB_ProductionUnit",cnKey);
        DataTable dt = dal.GetIDNameList("");
        this.ddlProductionUnitID.DataSource = dt;
        this.ddlProductionUnitID.DataTextField = "IDName";
        this.ddlProductionUnitID.DataValueField = "ID";
        this.ddlProductionUnitID.DataBind();
    }
    /// <summary>
    /// 事件，最大長度，樣式等
    /// </summary>
    private void BindOther()
    {
        if (ID == "")
            txtBillDate.changed = "$('#txtBillID').val(autoCode('" + FormID + "','" + cnKey + "','txtBillDate'));";

        txtBillID.MaxLength = 20;//單號 
        txtMemo.MaxLength = 1000;//備註 

        //一個明細調用一次
        //                       欄位名稱,顯示寬,type ,只讀標記
        //ID="sub1xxRowID" Text="(序號),  40,    label,1" 
        InitSubCols(FormID, cnKey, subColsName1.ID, "VLB_ProduceSub");

        //InitSubCols(subColsName2.ID, "LB_OrderSub1");  
        writeJsvar(FormID, cnKey, ID);
    }
    private void BindData(DataTable dt)
    {

        if (dt.Rows.Count > 0)
        {
            this.txtBillID.Text = dt.Rows[0]["BillID"].ToString(); //單號 
            this.txtBillDate.DateValue = (DateTime)dt.Rows[0]["BillDate"];
            this.txtStyleID.Text = dt.Rows[0]["StyleID"].ToString();


            this.txtSourceBillID.Text = dt.Rows[0]["SourceBillID"].ToString();

           this.txtProducePages.Text=  dt.Rows[0]["ProducePages"].ToString()  ;
           this.txtStdPages.Text=  dt.Rows[0]["StdPages"].ToString();
           this.txtVolumes.Text=  dt.Rows[0]["Volumes"].ToString() ;
           this.txtStartLabelNo.Text=   dt.Rows[0]["StartLabelNo"].ToString();
            this.txtEndLabelNo.Text=  dt.Rows[0]["EndLabelNo"].ToString();
          this.ddlProductionUnitID.SelectedValue=   dt.Rows[0]["ProductionUnitID"].ToString();
           this.txtScheduleBillID.Text=   dt.Rows[0]["ScheduleBillID"].ToString() ;
           if (dt.Rows[0]["PreInStockDate"].ToString() != "")
               txtPreInStockDate.DateValue = (DateTime)dt.Rows[0]["PreInStockDate"];

           this.ddlBillState.SelectedIndex = int.Parse(dt.Rows[0]["BillState"].ToString());
          
            this.txtCreateDate.Text = ToYMDHM(dt.Rows[0]["CreateDate"]); //建檔日期 
            this.txtCreateUserName.Text = dt.Rows[0]["CreateUserName"].ToString(); //建檔人員 
            this.txtLastModifyDate.Text = ToYMDHM(dt.Rows[0]["LastModifyDate"]); //異動日期 
            this.txtLastModifyUserName.Text = dt.Rows[0]["LastModifyUserName"].ToString(); //異動人員 
            this.txtCheckDate.Text = ToYMDHM(dt.Rows[0]["CheckDate"]); //營運覆核日期 
            this.txtCheckUserName.Text = dt.Rows[0]["CheckUserName"].ToString(); //營運覆核用戶 
            this.txtMemo.Text = dt.Rows[0]["Memo"].ToString();

            Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID, cnKey);
            DataSet ds = dal.GetSubDetail(dt.Rows[0]["BillID"].ToString());
            HdnSubDetail1.Value = Js.Com.JsonHelper.Dtb2Json(ds.Tables[0]);
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID, cnKey);
        DataTable dt = dal.GetRecord(ViewState["StrWhere"].ToString());
        DataRow dr = dt.NewRow();
        dr["BillID"] = this.txtBillID.Text;//單號 
        if (this.txtBillDate.Text.Length > 0) dr["BillDate"] = this.txtBillDate.Text;//日期 
        dr["StyleID"] = this.txtStyleID1.Value;
        dr["SourceBillID"] = this.txtSourceBillID.Text;

        dr["ProducePages"] = this.txtProducePages.Text;
        dr["StdPages"] = this.txtStdPages.Text;
        dr["Volumes"] = this.txtVolumes.Text;
        dr["StartLabelNo"] = this.txtStartLabelNo.Text;
        dr["EndLabelNo"] = this.txtEndLabelNo.Text;
        dr["ProductionUnitID"] = this.ddlProductionUnitID.SelectedValue;
        dr["ScheduleBillID"] = this.txtScheduleBillID.Text;
        if (this.txtPreInStockDate.Text.Length > 0) dr["PreInStockDate"] = this.txtPreInStockDate.Text;
        dr["BillState"] = this.ddlBillState.SelectedIndex;
    
        if (this.txtCreateDate.Text.Length > 0) dr["CreateDate"] = this.txtCreateDate.Text;//建檔日期 
        dr["CreateUserName"] = this.txtCreateUserName.Text;//建檔人員 
        dr["LastModifyDate"] = DateTime.Now;// this.txtLastModifyDate.Text;//異動日期 
        dr["LastModifyUserName"] = Session["User"].ToString();// this.txtLastModifyUserName.Text;//異動人員 
        if (this.txtCheckDate.Text.Length > 0) dr["CheckDate"] = this.txtCheckDate.Text;//營運覆核日期 
        dr["CheckUserName"] = this.txtCheckUserName.Text;//營運覆核用戶 
        dr["Memo"] = this.txtMemo.Text;

        if (ID.Length > 0)
            dal.Update(dr, ID);
        else
            dal.Add(dr);

        DataTable dtSub1 = dal.GetSubDetail("").Tables[0];
        DataTable newdtb = Js.Com.JsonHelper.Json2Dtb(HdnSubDetail1.Value);
        for (int i = 0; i < newdtb.Rows.Count; i++)
        {
            DataRow dr1 = dtSub1.NewRow();
            dr1["BillID"] = this.txtBillID.Text;
            //dr1["EnterpriseID"] = this.txtEnterpriseID.Text;
            dr1["BillDate"] = this.txtBillDate.DateValue;
            for (int j = 0; j < newdtb.Columns.Count; j++)
            {
                dr1[newdtb.Columns[j].ColumnName] = newdtb.Rows[i][j];
            }
            dtSub1.Rows.Add(dr1);
        }
        dtSub1.TableName = "LB_ProductionSub";
        dal.SaveDetail(dtSub1, ID);

        Response.Redirect("ProductionView.aspx?FormID=" + Server.UrlEncode(FormID) + "&ID=" + Server.UrlEncode(this.txtBillID.Text));
    }
    protected void txtStyleID1_TextChanged(object sender, EventArgs e)
    {
            }
    protected void Textbox3_TextChanged(object sender, EventArgs e)
    {

    }
    protected void Textbox9_TextChanged(object sender, EventArgs e)
    {

    }
    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btnLoad_Click(object sender, EventArgs e)
    {

    }
    protected void Unnamed1_TextChanged(object sender, EventArgs e)
    {

    }
}