using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebUI_BusinessUnit_ProductCheck : BasePage
{
    protected string FormID;
    protected string ID;
    protected void Page_Load(object sender, EventArgs e)
    {
        ID = Request.QueryString["ID"] + "";
        FormID = Request.QueryString["FormID"] + "";
        if (!IsPostBack)
        {            
            ViewState["StrWhere"] = string.Format(" EnterpriseID='{0}'", ID);
            Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID);
            DataTable dt = dal.GetRecord(ViewState["StrWhere"].ToString());
            ViewState["dt"] = dt;
            BindData(dt);
        }
    }
  
    private void BindData(DataTable dt)
    {  
        if (dt.Rows.Count > 0)
        {
            this.txtEnterpriseID.Text = dt.Rows[0]["EnterpriseID"].ToString();          
            this.txtEnterpriseName.Text = dt.Rows[0]["EnterpriseName"].ToString();
       
            Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID);           
            DataTable dtSub = dal.GetSubDetail(this.txtEnterpriseID.Text).Tables[0];

            if (dtSub.Rows.Count > 0)
                this.txtUploadDate.Text = Js.Com.PageValidate.ParseDateTime(dtSub.Rows[0]["UploadDate"].ToString());
            else
                this.txtUploadDate.Text = "";

            this.GridView1.DataSource = dtSub.DefaultView;
            this.GridView1.DataBind();
        }
        ScriptManager.RegisterStartupScript(this.updatePanel, this.GetType(), "Resize", "content_resize2();", true);
    }
    protected void btnFirst_Click(object sender, EventArgs e)
    {
        Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID);
        DataTable dt = dal.GetRecord("F", this.txtEnterpriseID.Text);
        BindData(dt);
    }
    protected void btnPre_Click(object sender, EventArgs e)
    {
        Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID);
        DataTable dt = dal.GetRecord("P", this.txtEnterpriseID.Text);
        if (dt != null)
            BindData(dt);
    }
    protected void btnNext_Click(object sender, EventArgs e)
    {
        Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID);
        DataTable dt = dal.GetRecord("N", this.txtEnterpriseID.Text);
        if (dt != null)
            BindData(dt);
    }
    protected void btnLast_Click(object sender, EventArgs e)
    {
        Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID);
        DataTable dt = dal.GetRecord("L", this.txtEnterpriseID.Text);
        BindData(dt);
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {

    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int i = e.Row.RowIndex +1;
            e.Row.Cells[0].Text = i.ToString();
            switch (e.Row.Cells[17].Text.ToString())
            {
                case "0":
                    e.Row.Cells[17].Text = Resources.Resource.Add;
                    break;
                case "1":
                    e.Row.Cells[17].Text = Resources.Resource.Modify;
                    break;
            }
        }
    }
    protected void btnCheck_Click(object sender, EventArgs e)
    {
        Js.BLL.BusinessUnit.CheckDal dal = new Js.BLL.BusinessUnit.CheckDal(this.txtEnterpriseID.Text);
        dal.ProductCheck(this.txtEnterpriseID.Text,Session["User"].ToString());

        //自動產生公告
        string[] ToUserName = new string[1];
        ToUserName[0] = "supervisor";
        int ReceiverFlag = 1;
        string[] ReceiveUnitNo = new string[1];
        ReceiveUnitNo[0] = this.txtEnterpriseID.Text;
        string Title = Resources.Resource.EP_ProductCheckMessageTitle;
        string Content = Resources.Resource.EP_ProductCheckMessageContent;

        Common.SystemMessage(ToUserName, ReceiverFlag, ReceiveUnitNo, Title, Content);
        //Js.BLL.BaseDal bdal = new Js.BLL.BaseDal(FormID);
        //DataTable dtSub = bdal.GetSubDetail(this.txtEnterpriseID.Text).Tables[0];
        //this.GridView1.DataSource = dtSub.DefaultView;
        //this.GridView1.DataBind();
        Response.Redirect("ProductChecks.aspx?FormID=" + Server.UrlEncode(FormID));
    }
}