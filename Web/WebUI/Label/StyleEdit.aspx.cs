using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class WebUI_Label_StyleEdit : BasePage
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
            ViewState["StrWhere"] = string.Format(" StyleID='{0}'", ID);
            Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID,cnKey);
            if (ID != "")
            {
                DataTable dt = dal.GetRecord(ViewState["StrWhere"].ToString());
                BindData(dt);
                this.txtStyleID.ReadOnly = true;
            }
            else
            {
                this.txtStyleID.Text = dal.GetMaxID();
                this.txtLastModifyUserName.Text = Session["User"].ToString();
                this.txtLastModifyDate.Text = ToYMDHM(DateTime.Now);
                this.txtCreateUserName.Text = Session["User"].ToString();
                this.txtCreateDate.Text = ToYMDHM(DateTime.Now);
            }

            BindOther();
        }
    }
    /// <summary>
    /// 事件，最大長度，樣式等
    /// </summary>
    private void BindOther()
    {      
        txtStyleID.MaxLength = 2;//款式編號        
        txtDescription.MaxLength = 1000;//描述說明
        writeJsvar(FormID, cnKey, ID);
    }
    /// <summary>
    /// 綁定查詢
    /// </summary>
    private void BindDropDownList()
    {
        Js.BLL.Label.StyleDal sdal = new Js.BLL.Label.StyleDal(cnKey);
        DataTable dt = sdal.GetRecord("Flag=2");

        this.ddlImageLocation.DataSource = dt;
        this.ddlImageLocation.DataTextField = "DataText";
        this.ddlImageLocation.DataValueField = "DataValue";
        this.ddlImageLocation.DataBind();

        dt = sdal.GetRecord("Flag=3");
        this.ddlQRContent.DataSource = dt;
        this.ddlQRContent.DataTextField = "DataText";
        this.ddlQRContent.DataValueField = "DataValue";
        this.ddlQRContent.DataBind();
    }
    private DataTable dtBindEdit(DataTable dt,DataRow[] dr)
    {
        DataTable dtt = dt.Clone();
        for (int i = 0; i < dr.Length; i++)
        {
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                if (dt.Rows[j][0].ToString() == dr[i]["DataValue"].ToString())
                    break;
                else
                {
                    DataRow drr = dtt.NewRow();
                    drr[0] = dr[i]["DataText"];
                    dtt.Rows.Add(drr);
                }
            }
        }
        return dtt;
    }
    private void BindData(DataTable dt)
    {
        if (dt.Rows.Count > 0)
        {
            this.txtStyleID.Text = dt.Rows[0]["StyleID"].ToString();
            this.txtStyleName.Text = dt.Rows[0]["StyleName"].ToString();
            this.txtVolumes.Text = dt.Rows[0]["Volumes"].ToString();
            this.txtStdPages.Text = dt.Rows[0]["StdPages"].ToString();
            this.txtServiceYears.Text = dt.Rows[0]["ServiceYears"].ToString();
            this.ddlEnableMonths.Text = dt.Rows[0]["EnableMonths"].ToString();
            this.ddlImageLocation.SelectedValue = dt.Rows[0]["ImageLocation"].ToString();
            this.txtLength_R.Text = string.Format("{0:N0}", dt.Rows[0]["Length"]);
            this.txtWidth_R.Text = string.Format("{0:N0}", dt.Rows[0]["Width"]);
            this.txtHeight_R.Text = string.Format("{0:N0}", dt.Rows[0]["Height"]);
            this.ddlQRContent.Text = dt.Rows[0]["QRContent"].ToString();
            this.txtProductionNo.Text = dt.Rows[0]["ProductionNo"].ToString();
            this.txtQR_X_R.Text = dt.Rows[0]["QR_X"].ToString();
            this.txtQR_Y_R.Text = dt.Rows[0]["QR_Y"].ToString();
            this.txtQR_Length_R.Text = dt.Rows[0]["QR_Length"].ToString();
            this.txtQR_Width_R.Text = dt.Rows[0]["QR_Width"].ToString();
            this.txtNowVolumes.Text = dt.Rows[0]["NowVolumes"].ToString();
            this.txtNowPages.Text = dt.Rows[0]["NowPages"].ToString();
            this.txtDescription.Text = dt.Rows[0]["Description"].ToString();
            this.txtAntiFakeDesc1.Text = dt.Rows[0]["AntiFakeDesc1"].ToString();
            this.txtImagePath1.Text = dt.Rows[0]["ImagePath1"].ToString();
            this.txtAntiFakeDesc2.Text = dt.Rows[0]["AntiFakeDesc2"].ToString();
            this.txtImagePath2.Text = dt.Rows[0]["ImagePath2"].ToString();
            this.txtAntiFakeDesc3.Text = dt.Rows[0]["AntiFakeDesc3"].ToString();
            this.txtImagePath3.Text = dt.Rows[0]["ImagePath3"].ToString();
            this.txtImagePath.Text = dt.Rows[0]["ImagePath"].ToString();
            this.txtCreateUserName.Text = dt.Rows[0]["CreateUserName"].ToString();
            this.txtCreateDate.Text =ToYMDHM( dt.Rows[0]["CreateDate"]);
            this.txtLastModifyUserName.Text = dt.Rows[0]["LastModifyUserName"].ToString();
            this.txtLastModifyDate.Text = ToYMDHM(dt.Rows[0]["LastModifyDate"]);
            this.txtCheckUserName.Text = dt.Rows[0]["CheckUserName"].ToString();
            this.txtCheckDate.Text = ToYMDHM(dt.Rows[0]["CheckDate"]);
            this.txtStopUserName.Text = dt.Rows[0]["StopUserName"].ToString();
            this.txtStopDate.Text = ToYMDHM(dt.Rows[0]["StopDate"]);
        }        
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID,cnKey);
        DataTable dt = dal.GetRecord(ViewState["StrWhere"].ToString());
        DataRow dr = dt.NewRow();
       
       // dr["ID"] = this.txtEnterpriseID.Text.Trim().ToUpper() + "_" + this.txtStyleID.Text.Trim().ToUpper();
       // dr["EnterpriseID"] = this.txtEnterpriseID.Text.ToUpper();
        dr["StyleName"] = this.txtStyleName.Text;
        dr["StyleID"] = this.txtStyleID.Text.Trim().ToUpper();
        dr["Volumes"] = this.txtVolumes.DataValue;
        dr["StdPages"] = this.txtStdPages.DataValue;
        dr["ImageLocation"] = this.ddlImageLocation.Text.Trim();
        dr["Length"] = this.txtLength_R.DataValue;
        dr["Width"] = this.txtWidth_R.DataValue;
        dr["Height"] = this.txtHeight_R.DataValue;
        dr["QRContent"] = this.ddlQRContent.Text.Trim();
        dr["ProductionNo"] = this.txtProductionNo.Text.Trim();
        dr["QR_X"] = this.txtQR_X_R.DataValue;
        dr["QR_Y"] = this.txtQR_Y_R.DataValue;
        dr["QR_Length"] = this.txtQR_Length_R.DataValue;
        dr["QR_Width"] = this.txtQR_Width_R.DataValue;
        dr["NowVolumes"] = this.txtNowVolumes.DataValue;
        dr["NowPages"] = this.txtNowPages.DataValue;      
        dr["ImagePath"] = this.txtImagePath.Text.Trim();
        dr["ServiceYears"] = byte.Parse(this.txtServiceYears.Text.Trim());
        dr["EnableMonths"] = byte.Parse(this.ddlEnableMonths.SelectedValue.ToString());
        dr["Description"] = this.txtDescription.Text.Trim();
        dr["AntiFakeDesc1"] = this.txtAntiFakeDesc1.Text.Trim();
        dr["ImagePath1"] = this.txtImagePath1.Text.Trim();
        dr["AntiFakeDesc2"] = this.txtAntiFakeDesc2.Text.Trim();
        dr["ImagePath2"] = this.txtImagePath2.Text.Trim();
        dr["AntiFakeDesc3"] = this.txtAntiFakeDesc3.Text.Trim();
        dr["ImagePath3"] = this.txtImagePath3.Text.Trim();
        dr["CreateUserName"] = this.txtCreateUserName.Text;
        dr["CreateDate"] = this.txtCreateDate.Text;
        dr["LastModifyUserName"] = Session["User"].ToString();
        dr["LastModifyDate"] = DateTime.Now;//.ToString(Js.Com.User.strDateFormat);
        dr["CheckUserName"] = this.txtCheckUserName.Text;
        if (this.txtCheckDate.Text.Length > 0)
            dr["CheckDate"] = this.txtCheckDate.Text;
        dr["StopUserName"] = this.txtStopUserName.Text;
        if (this.txtStopDate.Text.Length > 0)
            dr["StopDate"] = this.txtStopDate.Text;

        if (ID.Length > 0)
            dal.Update(dr, ID);
        else
        {
            dal.Add(dr);
            //插入標籤數量
            Js.BLL.Label.StyleDal sdal = new Js.BLL.Label.StyleDal(cnKey);
            sdal.InsertWarehousePagesByStyleID(this.txtStyleID.Text.Trim().ToUpper(), Session["User"].ToString());
        }

        Response.Redirect("StyleView.aspx?FormID=" + Server.UrlEncode(FormID) + "&ID=" + Server.UrlEncode(this.txtStyleID.Text));
    }
    private string UploadFile(FileUpload upFile)
    {
        string filepath = upFile.PostedFile.FileName;
        if (filepath.Length > 0)
        {
            string upfilename = filepath.Substring(filepath.LastIndexOf("\\") + 1);
            string attachName = upfilename.Substring(upfilename.IndexOf("."));
            string filename = this.txtStyleID.Text + "_" + upFile.ID.Substring(upFile.ID.Length - 1) + attachName;
            string serverpath = Server.MapPath("~/WebUI/Label/Images/") + filename;
            upFile.PostedFile.SaveAs(serverpath);
            return filename;
        }
        return "";
    }
}