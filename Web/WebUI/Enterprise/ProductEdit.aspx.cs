using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class WebUI_Enterprise_ProductEdit : BasePage
{
    protected string FormID;
    protected string ID;    
    protected string cnKey = "Enterprise";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["cnKey"] != null)
            cnKey = Session["cnKey"].ToString();
        ID = Request.QueryString["ID"] + "";
        FormID = Request.QueryString["FormID"] + "";
        
        if (!IsPostBack)
        {
           // ViewState["StrWhere"] = string.Format(" ProductID='{0}'", ID);
            ViewState["StrWhere"] = string.Format(" ProductID='{0}' and EnterpriseID='{1}'", ID, Session["EnterpriseID"].ToString());
            BindEdll();
            Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID, cnKey);
            if (ID != "")
            {
                DataTable dt = dal.GetViewRecord(ViewState["StrWhere"].ToString());
                BindData(dt);
                this.txtProductID.ReadOnly = true;
            }
            else
            {
                this.txtProductID.Text = dal.GetMaxID("EnterpriseID='" + Session["EnterpriseID"].ToString() + "' and flag=2 ");
                this.txtLastModifyUserName.Text = Session["User"].ToString();
                this.txtLastModifyDate.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm");
                this.txtCreateUserName.Text = Session["User"].ToString();
                this.txtCreateDate.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm");
            }
            this.txtEnterpriseID.Text = Session["EnterPriseID"].ToString();
            this.txtEnterpriseName.Text = Session["EnterpriseName"].ToString();
        }
    }
    private void BindEdll()
    {
        Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID, cnKey);
        DataTable dt = dal.GetDistinctRecord("GroupName");
        this.txtGroupName.DataSource = dt;        

        dt = dal.GetDistinctRecord("ClassName");
        this.txtClassName.DataSource = dt;        
    }
    private void BindData(DataTable dt)
    {
        if (dt.Rows.Count > 0)
        {
            this.txtEnterpriseID.Text = dt.Rows[0]["EnterpriseID"].ToString();
            this.txtEnterpriseName.Text = dt.Rows[0]["EnterpriseName"].ToString();
            this.txtProductID.Text = dt.Rows[0]["ProductID"].ToString();
            this.txtProductName.Text = dt.Rows[0]["ProductName"].ToString();
            this.txtProductEName.Text = dt.Rows[0]["ProductEName"].ToString();
            this.txtGroupName.Text = dt.Rows[0]["GroupName"].ToString();
            this.txtClassName.Text = dt.Rows[0]["ClassName"].ToString();
            this.txtBarcode1.Text = dt.Rows[0]["Barcode1"].ToString();
            this.txtBarcode2.Text = dt.Rows[0]["Barcode2"].ToString();
            this.txtOfficalUrl.Text = dt.Rows[0]["OfficalUrl"].ToString();
            this.txtServicePhone.Text = dt.Rows[0]["ServicePhone"].ToString();
            this.txtDescription.Text = dt.Rows[0]["Description"].ToString();
            this.txtMemo.Text = dt.Rows[0]["Memo"].ToString();
            this.txtCreateUserName.Text = dt.Rows[0]["CreateUserName"].ToString();
            this.txtCreateDate.Text = Js.Com.PageValidate.ParseDateTime(dt.Rows[0]["CreateDate"].ToString());
            this.txtLastModifyUserName.Text = dt.Rows[0]["LastModifyUserName"].ToString();
            this.txtLastModifyDate.Text = Js.Com.PageValidate.ParseDateTime(dt.Rows[0]["LastModifyDate"].ToString());
            this.txtBU_CheckUserName.Text = dt.Rows[0]["BU_CheckUserName"].ToString();
            this.txtBU_CheckDate.Text = Js.Com.PageValidate.ParseDateTime(dt.Rows[0]["BU_CheckDate"].ToString());
            this.txtEP_CheckUserName.Text = dt.Rows[0]["EP_CheckUserName"].ToString();
            this.txtEP_CheckDate.Text = Js.Com.PageValidate.ParseDateTime(dt.Rows[0]["EP_CheckDate"].ToString());
            this.txtStopUserName.Text = dt.Rows[0]["StopUserName"].ToString();
            this.txtStopDate.Text = Js.Com.PageValidate.ParseDateTime(dt.Rows[0]["StopDate"].ToString());
            ViewState["dt"] = dt;
        }        
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID, cnKey);
        DataTable dt = dal.GetRecord(ViewState["StrWhere"].ToString());
        DataRow dr = dt.NewRow();

        dr["Flag"] = 2;
        dr["EnterPriseID"] = Session["EnterPriseID"].ToString();
        dr["EnterpriseName"] = Session["EnterpriseName"].ToString();
        dr["ProductID"] = this.txtProductID.Text;
        dr["ProductName"] = this.txtProductName.Text;
        dr["ProductEName"] = this.txtProductEName.Text.Trim();
        dr["GroupName"] = this.txtGroupName.Text.Trim();
        dr["ClassName"] = this.txtClassName.Text.Trim();
        dr["Barcode1"] = this.txtBarcode1.Text.Trim();
        dr["Barcode2"] = this.txtBarcode2.Text.Trim();
        dr["OfficalUrl"] = this.txtOfficalUrl.Text.Trim();
        dr["ServicePhone"] = this.txtServicePhone.Text.Trim();
        dr["Description"] = this.txtDescription.Text.Trim();
        dr["Memo"] = this.txtMemo.Text.Trim();
        dr["CreateUserName"] = this.txtCreateUserName.Text;
        dr["CreateDate"] = this.txtCreateDate.Text;
        dr["LastModifyUserName"] = Session["User"].ToString();
        dr["LastModifyDate"] = DateTime.Now.ToString(Js.Com.User.strDateFormat);
        dr["BU_CheckUserName"] = "";
        dr["EP_CheckUserName"] = "";
        //dr["BU_CheckUserName"] = this.txtBU_CheckUserName.Text;
        //if (this.txtBU_CheckDate.Text.Length > 0)
        //    dr["BU_CheckDate"] = this.txtBU_CheckDate.Text;
        //dr["EP_CheckUserName"] = this.txtEP_CheckUserName.Text;
        //if (this.txtEP_CheckDate.Text.Length > 0)
        //    dr["EP_CheckDate"] = this.txtEP_CheckDate.Text;
        dr["StopUserName"] = this.txtStopUserName.Text;
        if (this.txtStopDate.Text.Length > 0)
            dr["StopDate"] = this.txtStopDate.Text;

        dr["UploadUserName"] = "";
        dr["State"] = 0;
        
        if (FormID == "EP_ProductNoCheck" && ID.Length > 0)
            //dal.Update(dr, this.txtEnterpriseID.Text, string.Format(" ProductID='{0}'", this.txtProductID.Text));
            dal.Update(dr, this.txtProductID.Text, string.Format(" EnterpriseID='{0}' and flag=2", this.txtEnterpriseID.Text));
        else
            dal.Add(dr);

        Response.Redirect("ProductView.aspx?FormID=" + Server.UrlEncode(FormID) + "&ID=" + Server.UrlEncode(this.txtProductID.Text));
    }
}