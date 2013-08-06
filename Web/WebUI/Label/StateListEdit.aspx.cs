using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class WebUI_Label_StateListEdit : BasePage
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
            ViewState["StrWhere"] = string.Format(" StateID='{0}'", ID);
            Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID, cnKey);
            if (ID != "")
            {
                DataTable dt = dal.GetRecord(ViewState["StrWhere"].ToString());
                BindData(dt);
                this.txtStateListID.ReadOnly = true;
                this.txtStateListID.CssClass = "TextRead";
            }
            else
            {
                this.txtStateListID.Text = dal.GetMaxID();
                this.ddlStyle.SelectedIndex = 1;
                this.txtLastModifyUserName.Text = Session["User"].ToString();
                this.txtLastModifyDate.Text =ToYMDHM( DateTime.Now);
                this.txtCreateUserName.Text = Session["User"].ToString();
                this.txtCreateDate.Text =ToYMDHM( DateTime.Now);
            }
            BindEdll();

            txtStateListID.MaxLength = 2;//代碼 
            txtStateName.MaxLength = 8;//企業用戶名稱 
            txtMemo.MaxLength = 300;//狀態說明          

            if (this.ddlStyle.SelectedIndex == 0)
            {
                txtStateListID.ReadOnly = true;
                txtStateListID.CssClass = "TextRead";
                txtStateName.ReadOnly = true;
                txtStateName.CssClass = "TextRead";
                ddlStyle.Enabled = false;
                chkImageProvider.Enabled = false;
            }        
        }
    }
    private void BindEdll()
    {

    }
    private void BindData(DataTable dt)
    {

        if (dt.Rows.Count > 0)
        {
            this.txtStateListID.Text = "" + dt.Rows[0]["StateID"];
            this.txtStateName.Text = "" + dt.Rows[0]["StateName"];
            this.ddlStyle.SelectedIndex = int.Parse("" + dt.Rows[0]["Style"]);
            this.txtMemo.Text = "" + dt.Rows[0]["Description"];
            this.chkImageProvider.Checked = bool.Parse(dt.Rows[0]["ImageProvider"].ToString());
            this.txtStopDate.Text =ToYMDHM( dt.Rows[0]["StopDate"]);
            this.txtStopUserName.Text = "" + dt.Rows[0]["StopUserName"];
            this.txtCreateUserName.Text = dt.Rows[0]["CreateUserName"].ToString();
            this.txtCreateDate.Text =ToYMDHM( dt.Rows[0]["CreateDate"]);
            this.txtLastModifyUserName.Text = dt.Rows[0]["LastModifyUserName"].ToString();
            this.txtLastModifyDate.Text =ToYMDHM( dt.Rows[0]["LastModifyDate"]);
            this.txtCheckDate.Text = ToYMDHM(dt.Rows[0]["CheckDate"]);
            this.txtCheckUserName.Text = "" + dt.Rows[0]["CheckUserName"];
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID, cnKey);
        DataTable dt = dal.GetRecord(ViewState["StrWhere"].ToString());
        DataRow dr = dt.NewRow();

        dr["StateID"] = this.txtStateListID.Text;
        dr["StateName"] = this.txtStateName.Text;
        dr["Style"] = this.ddlStyle.SelectedIndex;
        dr["Description"] = this.txtMemo.Text;
        dr["ImageProvider"] = this.chkImageProvider.Checked;
        if (this.txtStopDate.Text.Length > 0)
            dr["StopDate"] = this.txtStopDate.Text;
        dr["StopUserName"] = this.txtStopUserName.Text;
        if (this.txtCreateDate.Text.Length > 0)
        dr["CreateDate"] = this.txtCreateDate.Text;
        dr["CreateUserName"] = this.txtCreateUserName.Text;
        dr["LastModifyDate"] = DateTime.Now;// this.txtLastModifyDate.Text;//異動日期 
        dr["LastModifyUserName"] = Session["User"].ToString();// this.txtLastModifyUserName.Text;//異動人員 
        if (this.txtCheckDate.Text.Length > 0)
        dr["CheckDate"] = this.txtCheckDate.Text;
        dr["CheckUserName"] = this.txtCheckUserName.Text;

        if (ID.Length > 0)
            dal.Update(dr, ID);
        else
            dal.Add(dr);

        Response.Redirect("StateListView.aspx?FormID=" + Server.UrlEncode(FormID) + "&ID=" + Server.UrlEncode(this.txtStateListID.Text));
    }
}