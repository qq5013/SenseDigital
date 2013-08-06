using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class WebUI_Label_OrderEdit : BasePage
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
            //後台ddl綁定
            //DataTable dt1 = new DataTable();
            //DataColumn dc = new DataColumn("id", typeof(string));
            //dt1.Columns.Add(dc);
            //DataRow dr;
            //dr = dt1.NewRow();
            //dr[0] = "";
            //dt1.Rows.Add(dr);
            //dr = dt1.NewRow();
            //dr[0] = "1";
            //dt1.Rows.Add(dr);
            //dr = dt1.NewRow();
            //dr[0] = "2";
            //dt1.Rows.Add(dr);
            //txtDeliverCountry.DataSource = dt1;
            //txtDeliverCountry.Text = "2";

            ViewState["StrWhere"] = string.Format(" BillID='{0}'", ID);
            Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID, cnKey);
            if (ID != "")
            {
                DataTable dt = dal.GetViewRecord(ViewState["StrWhere"].ToString());
                BindData(dt);
                this.txtBillID.ReadOnly = true;
                this.txtBillID.CssClass = "TextRead";
            }
            else
            {                
                this.txtBillID.Text = dal.GetAutoCode(DateTime.Now);
                this.txtBillDate.DateValue = DateTime.Now;
                this.txtLastModifyUserName.Text = Session["User"].ToString();
                this.txtLastModifyDate.Text = ToYMDHM(DateTime.Now);
                this.txtCreateUserName.Text = Session["User"].ToString();
                this.txtCreateDate.Text = ToYMDHM(DateTime.Now);
            }

            BindEdll();

            BindOther();
        }
    }
    /// <summary>
    /// 事件，最大長度，樣式等
    /// </summary>
    private void BindOther()
    {
        if (ID == "")
        {
            txtBillDate.changed = "$('#txtBillID').val(autoCode('" + FormID + "','" + cnKey + "','txtBillDate'));";
            if (Session["UserType"].ToString() == "EP")
            {
                txtEnterpriseID.Attributes.Add("ReadOnly", "true");
                txtEnterpriseID.CssClass = "TextRead"; txtEnterpriseID.Text = Session["EnterpriseID"].ToString();
                txtEnterpriseName.Text = Session["EnterpriseName"].ToString();
            }
        }

        //txtFlag.MaxLength = 1;//旗標 Flag=1 訂單 Flag=2 生產計劃單 
        txtBillID.MaxLength = 20;//單號 
        //txtBillDate.MaxLength = 8;//日期 
        txtCustomerPO.MaxLength = 20;//客戶PO 
        txtEnterpriseID.MaxLength = 6;//企業用戶編號 
        txtEnterpriseName.MaxLength = 100;//企業用戶名稱 
        txtContact.MaxLength = 20;//連絡人員 
        txtContactPhone.MaxLength = 20;//聯絡電話 
        txtFax.MaxLength = 20;//公司傳真 
        txtBusPersonID.MaxLength = 10;//業務員編號 
        txtBusPersonName.MaxLength = 10;//業務員姓名 
        //txtBillState.MaxLength = 1;//單況  0:有效 1:結案 
        txtDeliverCountry.MaxLength = 10;//送貨地址國別 
        txtDeliverAddress.MaxLength = 200;//送貨地址 
        txtDeliverMehtod.MaxLength = 20;//交貨方式 
        txtMemo.MaxLength = 1000;//備註 
        txtTotalPages.MaxLength = 4;//張數合計 
        txtCreateDate.MaxLength = 8;//建檔日期 
        txtCreateUserName.MaxLength = 20;//建檔人員 
        txtLastModifyDate.MaxLength = 8;//異動日期 
        txtLastModifyUserName.MaxLength = 20;//異動人員 
        //txtCheckDate.MaxLength = 8;//營運覆核日期 
        //txtCheckUserName.MaxLength = 20;//營運覆核人員 


        this.txtTotalPages.Attributes.Add("style", "text-align:right");
        //this.txtRemainQtyTotal.Attributes.Add("style", "text-align:right");
        txtEnterpriseName.Attributes.Add("ReadOnly", "true");
        txtBusPersonName.Attributes.Add("ReadOnly", "true");
        txtTotalPages.Attributes.Add("ReadOnly", "true");
        //txtRemainQtyTotal.Attributes.Add("ReadOnly", "true");

        //txtBillID.ReadOnly = true;
        //txtBillID.CssClass = "TextRead";
        //一個明細調用一次
        //                       欄位名稱,顯示寬,type ,只讀標記
        //ID="sub1xxRowID" Text="(序號),  40,    label,1" 
        InitSubCols(FormID, cnKey, subColsName1.ID, "LB_OrderSub");

        //InitSubCols(subColsName2.ID, "LB_OrderSub1");  
        writeJsvar(FormID, cnKey, ID);
    }

    private void BindEdll()
    {

    }
    private void BindData(DataTable dt)
    {

        if (dt.Rows.Count > 0)
        {
            //this.txtFlag.SelectedIndex = int.Parse(dt.Rows[0]["Flag"].ToString());//旗標 Flag=1 訂單 Flag=2 生產計劃單 
            this.txtBillID.Text = dt.Rows[0]["BillID"].ToString(); //單號 
            this.txtBillDate.DateValue = (DateTime)dt.Rows[0]["BillDate"];  //日期 
            this.txtCustomerPO.Text = dt.Rows[0]["CustomerPO"].ToString(); //客戶PO 
            this.txtEnterpriseID.Text = dt.Rows[0]["EnterpriseID"].ToString(); //企業用戶編號 
            this.txtEnterpriseName.Text = dt.Rows[0]["EnterpriseName"].ToString(); //企業用戶名稱 
            this.txtContact.Text = dt.Rows[0]["Contact"].ToString(); //連絡人員 
            this.txtContactPhone.Text = dt.Rows[0]["ContactPhone"].ToString(); //聯絡電話 
            this.txtFax.Text = dt.Rows[0]["Fax"].ToString(); //公司傳真 
            this.txtBusPersonID.Text = dt.Rows[0]["BusPersonID"].ToString(); //業務員編號 
            this.txtBusPersonName.Text = dt.Rows[0]["BusPersonName"].ToString(); //業務員姓名 
            this.ddlBillState.SelectedIndex = int.Parse(dt.Rows[0]["BillState"].ToString());//單況  0:有效 1:結案 
            this.txtDeliverCountry.Text = dt.Rows[0]["DeliverCountry"].ToString(); //送貨地址國別 
            this.txtDeliverAddress.Text = dt.Rows[0]["DeliverAddress"].ToString(); //送貨地址 
            this.txtDeliverMehtod.Text = dt.Rows[0]["DeliverMehtod"].ToString(); //交貨方式 
            this.txtMemo.Text = dt.Rows[0]["Memo"].ToString(); //備註 
            this.txtTotalPages.Text = string.Format("{0:N0}", dt.Rows[0]["TotalPages"]);//張數合計 
            this.txtCreateDate.Text = ToYMDHM(dt.Rows[0]["CreateDate"]); //建檔日期 
            this.txtCreateUserName.Text = dt.Rows[0]["CreateUserName"].ToString(); //建檔人員 
            this.txtLastModifyDate.Text = ToYMDHM(dt.Rows[0]["LastModifyDate"]); //異動日期 
            this.txtLastModifyUserName.Text = dt.Rows[0]["LastModifyUserName"].ToString(); //異動人員 
            this.txtBU_CheckDate.Text = ToYMDHM(dt.Rows[0]["CheckDate"]); //營運覆核日期 
            this.txtBU_CheckUserName.Text = dt.Rows[0]["CheckUserName"].ToString(); //營運覆核人員  
           
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

        dr["Flag"] = Flag;//旗標 Flag=1 訂單 Flag=2 生產計劃單 
        dr["BillID"] = this.txtBillID.Text;//單號 
        if (this.txtBillDate.Text.Length > 0) dr["BillDate"] = this.txtBillDate.Text;//日期 
        dr["CustomerPO"] = this.txtCustomerPO.Text;//客戶PO 
        dr["EnterpriseID"] = this.txtEnterpriseID.Text;//企業用戶編號 
        dr["EnterpriseName"] = this.txtEnterpriseName.Text;//企業用戶名稱 
        dr["Contact"] = this.txtContact.Text;//連絡人員 
        dr["ContactPhone"] = this.txtContactPhone.Text;//聯絡電話 
        dr["Fax"] = this.txtFax.Text;//公司傳真 
        dr["BusPersonID"] = this.txtBusPersonID.Text;//業務員編號 
        dr["BusPersonName"] = this.txtBusPersonName.Text;//業務員姓名 
        dr["BillState"] = ddlBillState.SelectedIndex;//單況  0:有效 1:結案 
        dr["DeliverCountry"] = this.txtDeliverCountry.Text;//送貨地址國別 
        dr["DeliverAddress"] = this.txtDeliverAddress.Text;//送貨地址 
        dr["DeliverMehtod"] = this.txtDeliverMehtod.Text;//交貨方式 
        dr["Memo"] = this.txtMemo.Text;//備註 
        //dr["TotalPages"] = int.Parse(this.txtTotalPages.Text);//張數合計 
        if (this.txtCreateDate.Text.Length > 0) dr["CreateDate"] = this.txtCreateDate.Text;//建檔日期 
        dr["CreateUserName"] = this.txtCreateUserName.Text;//建檔人員 
        if (this.txtLastModifyDate.Text.Length > 0) dr["LastModifyDate"] = this.txtLastModifyDate.Text;//異動日期 
        dr["LastModifyUserName"] = this.txtLastModifyUserName.Text;//異動人員 
        if (this.txtBU_CheckDate.Text.Length > 0) dr["CheckDate"] = this.txtBU_CheckDate.Text;//營運覆核日期 
        dr["CheckUserName"] = this.txtBU_CheckUserName.Text;//營運覆核人員  

        if (ID.Length > 0)
            dal.Update(dr, ID);
        else
            dal.Add(dr);

        DataTable dtSub1 = dal.GetSubDetail("").Tables[0];
        DataTable newdtb = Js.Com.JsonHelper.Json2Dtb(HdnSubDetail1.Value);
        for (int i = 0; i < newdtb.Rows.Count; i++)
        {
            DataRow dr1 = dtSub1.NewRow();
            dr1["Flag"] = Flag;
            dr1["BillID"] = this.txtBillID.Text;
            dr1["EnterpriseID"] = this.txtEnterpriseID.Text;
            dr1["BillDate"] = this.txtBillDate.DateValue;
            for (int j = 0; j < newdtb.Columns.Count; j++)
            {
                dr1[newdtb.Columns[j].ColumnName] = newdtb.Rows[i][j];
            }
            dtSub1.Rows.Add(dr1);
        }
        dtSub1.TableName = "LB_OrderSub";
        dal.SaveDetail(dtSub1, ID);
        Response.Redirect("OrderView.aspx?FormID=" + Server.UrlEncode(FormID) + "&ID=" + Server.UrlEncode(this.txtBillID.Text));
    }
    
}