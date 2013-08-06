using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;

public partial class WebUI_System_Parameter : BasePage
{
    protected string FormID;

    protected void Page_Load(object sender, EventArgs e)
    {
        FormID = Request.QueryString["FormID"] + "";
        if (!IsPostBack)
        {
            BindData();

            this.btnEdit.Enabled = true;
            this.btnSave.Enabled = false;
            this.btnCancel.Enabled = false;
            InitialCtl();

            this.btnCopy.OnClientClick = "return copyCode();";
            this.btnOrderCode.OnClientClick = "return autoCode();";
            this.btnScheduleCode.OnClientClick = "return autoCode();";
            this.btnProductionCode.OnClientClick = "return autoCode();";
            this.btnInStockCode.OnClientClick = "return autoCode();";
            this.btnInvalidLabelCode.OnClientClick = "return autoCode();";
            this.btnDeliverCode.OnClientClick = "return autoCode();";
            this.btnReturnLabelCode.OnClientClick = "return autoCode();";
            this.btnTransferCode.OnClientClick = "return autoCode();";
            this.btnEnableLabelNoCode.OnClientClick = "return autoCode();";
            this.btnLabelNoActionCode.OnClientClick = "return autoCode();";
            this.btnBatchActionCode.OnClientClick = "return autoCode();";
            this.btnLabelRegisterCode.OnClientClick = "return autoCode();";
            this.btnBatchRegisterCode.OnClientClick = "return autoCode();";

             
            this.txtOrderCodeView.Attributes.Add("readonly", "true");
            this.txtScheduleCodeView.Attributes.Add("readonly", "true");
            this.txtProductionCodeView.Attributes.Add("readonly", "true");
            this.txtInStockCodeView.Attributes.Add("readonly", "true");
            this.txtInvalidLabelCodeView.Attributes.Add("readonly", "true");
            this.txtDeliverCodeView.Attributes.Add("readonly", "true");
            this.txtReturnLabelCodeView.Attributes.Add("readonly", "true");
            this.txtTransferCodeView.Attributes.Add("readonly", "true");            
            this.txtEnableLabelNoCodeView.Attributes.Add("readonly", "true");
            this.txtLabelNoActionCodeView.Attributes.Add("readonly", "true");
            this.txtBatchActionCodeView.Attributes.Add("readonly", "true");
            this.txtLabelRegisterCodeView.Attributes.Add("readonly", "true");
            this.txtBatchRegisterCodeView.Attributes.Add("readonly", "true");
        }
    }

    private void BindData()
    {
        Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID);
        DataTable dt = dal.GetRecord("1=1");

        this.txtOrderCode.Value = dt.Rows[0]["OrderCode"].ToString();
        this.txtScheduleCode.Value = dt.Rows[0]["ScheduleCode"].ToString();
        this.txtProductionCode.Value = dt.Rows[0]["ProductionCode"].ToString();
        this.txtInStockCode.Value = dt.Rows[0]["InStockCode"].ToString();
        this.txtInvalidLabelCode.Value = dt.Rows[0]["InvalidLabelCode"].ToString();
        this.txtDeliverCode.Value = dt.Rows[0]["DeliverCode"].ToString();
        this.txtReturnLabelCode.Value = dt.Rows[0]["ReturnLabelCode"].ToString();
        this.txtTransferCode.Value = dt.Rows[0]["TransferCode"].ToString();
        this.txtEnableLabelNoCode.Value = dt.Rows[0]["EnableLabelNoCode"].ToString();
        this.txtLabelNoActionCode.Value = dt.Rows[0]["LabelNoActionCode"].ToString();
        this.txtBatchActionCode.Value = dt.Rows[0]["BatchActionCode"].ToString();
        this.txtLabelRegisterCode.Value = dt.Rows[0]["LabelRegisterCode"].ToString();
        this.txtBatchRegisterCode.Value = dt.Rows[0]["BatchRegisterCode"].ToString();

        this.txtOrderCodeView.Text = dt.Rows[0]["OrderCode"].ToString().Split (' ')[0];
        this.txtScheduleCodeView.Text = dt.Rows[0]["ScheduleCode"].ToString().Split(' ')[0];
        this.txtProductionCodeView.Text = dt.Rows[0]["ProductionCode"].ToString().Split(' ')[0];
        this.txtInStockCodeView.Text = dt.Rows[0]["InStockCode"].ToString().Split(' ')[0];
        this.txtInvalidLabelCodeView.Text = dt.Rows[0]["InvalidLabelCode"].ToString().Split(' ')[0];
        this.txtDeliverCodeView.Text = dt.Rows[0]["DeliverCode"].ToString().Split(' ')[0];
        this.txtReturnLabelCodeView.Text = dt.Rows[0]["ReturnLabelCode"].ToString().Split(' ')[0];
        this.txtTransferCodeView.Text = dt.Rows[0]["TransferCode"].ToString().Split(' ')[0];
        this.txtEnableLabelNoCodeView.Text = dt.Rows[0]["EnableLabelNoCode"].ToString().Split(' ')[0];
        this.txtLabelNoActionCodeView.Text = dt.Rows[0]["LabelNoActionCode"].ToString().Split(' ')[0];
        this.txtBatchActionCodeView.Text = dt.Rows[0]["BatchActionCode"].ToString().Split(' ')[0];
        this.txtLabelRegisterCodeView.Text = dt.Rows[0]["LabelRegisterCode"].ToString().Split(' ')[0];
        this.txtBatchRegisterCodeView.Text = dt.Rows[0]["BatchRegisterCode"].ToString().Split(' ')[0];

        this.ddlPercentDecimalDigits.SelectedValue = dt.Rows[0]["PercentDecimalDigits"].ToString();        
        this.txtServiceYears.Text = dt.Rows[0]["ServiceYears"].ToString();
        this.ddlEnableMonths.Text = dt.Rows[0]["EnableMonths"].ToString();
        this.txtClearDate.Text = Js.Com.PageValidate.ParseDate(dt.Rows[0]["ClearDate"].ToString());
        this.txtCloseDate.Text = Js.Com.PageValidate.ParseDate(dt.Rows[0]["CloseDate"].ToString());
        this.txtLastModifyUserName.Text = dt.Rows[0]["LastModifyUserName"].ToString();
        this.txtLastModifyDate.Text = Js.Com.PageValidate.ParseDateTime(dt.Rows[0]["LastModifyDate"].ToString());
    }
    private void InitialCtl()
    {
        this.txtCloseDate.ReadOnly = this.btnEdit.Enabled;
        this.ddlPercentDecimalDigits.Enabled = !this.btnEdit.Enabled;
        this.txtServiceYears.ReadOnly = this.btnEdit.Enabled;
        this.ddlEnableMonths.Enabled = !this.btnEdit.Enabled;
        this.txtServiceYears.BackColor = this.btnEdit.Enabled ? Color.Gainsboro : Color.White;
        
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        this.btnEdit.Enabled = false;
        this.btnSave.Enabled = true;
        this.btnCancel.Enabled = true;
        InitialCtl();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        //BaseDal
        Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID);
        DataTable dt = dal.GetRecord("1=1");
        string OrderCode = dt.Rows[0]["OrderCode"].ToString();

        DataRow dr = dt.NewRow();
        dr["OrderCode"] = this.txtOrderCode.Value;
        dr["ScheduleCode"] = this.txtScheduleCode.Value;        
        dr["ProductionCode"] = this.txtProductionCode.Value;
        dr["InStockCode"] = this.txtInStockCode.Value;
        dr["InvalidLabelCode"] = this.txtInvalidLabelCode.Value;
        dr["OutStockCode"] = this.txtDeliverCode.Value;
        dr["ReturnLabelCode"] = this.txtReturnLabelCode.Value;
        dr["TransferCode"] = this.txtTransferCode.Value;
        dr["EnableLabelNoCode"] = this.txtEnableLabelNoCode.Value;
        dr["LabelNoActionCode"] = this.txtLabelNoActionCode.Value;
        dr["BatchActionCode"] = this.txtBatchActionCode.Value;
        dr["LabelRegisterCode"] = this.txtLabelRegisterCode.Value;
        dr["BatchRegisterCode"] = this.txtBatchRegisterCode.Value;
     
        dr["PercentDecimalDigits"] = this.ddlPercentDecimalDigits.SelectedValue;
        int ServiceYears;
        int.TryParse(this.txtServiceYears.Text.Trim(), out ServiceYears);
        this.txtServiceYears.Text = ServiceYears.ToString();
        dr["ServiceYears"] = ServiceYears;
        dr["EnableMonths"] = this.ddlEnableMonths.Text;
        if (this.txtClearDate.Text.Length > 0)
            dr["ClearDate"] = this.txtClearDate.Text;
        if (this.txtCloseDate.Text.Length > 0)
            dr["CloseDate"] = this.txtCloseDate.Text;
        dr["LastModifyUserName"] = Session["User"].ToString();
        if (this.txtLastModifyDate.Text.Length > 0)
            dr["LastModifyDate"] = DateTime.Now;

        if (OrderCode.Length > 0)
            dal.Update(dr, OrderCode);
        else
            dal.Add(dr);
        //BindData();
        this.btnEdit.Enabled = true;
        this.btnSave.Enabled = false;
        this.btnCancel.Enabled = false;
        InitialCtl();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        this.btnEdit.Enabled = true;
        this.btnSave.Enabled = false;
        this.btnCancel.Enabled = false;

        BindData();
        InitialCtl();
    }

    protected void btnModify_Click(object sender, EventArgs e)
    {
        BindData();
    }
}