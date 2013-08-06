using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class WebUI_System_AnnounceMessageView : BasePage
{
    protected string FormID;
    protected string ID;
    protected void Page_Load(object sender, EventArgs e)
    {
        ID = Request.QueryString["ID"] + "";
        FormID = Request.QueryString["FormID"] + "";
        if (!IsPostBack)
        {
            ViewState["StrWhere"] = string.Format(" AnnounceID='{0}'", ID);
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
            this.txtAnnounceID.Text = dt.Rows[0]["AnnounceID"].ToString();
            this.txtAnnouncer.Text = dt.Rows[0]["Announcer"].ToString();
            this.txtAnnouncerUserName.Text = dt.Rows[0]["AnnouncerUserName"].ToString();
            if (dt.Rows[0]["AnnounceFlag"].ToString() == "0")
                this.txtAnnounceFlag.Text = Resources.Resource.AnnounceFlag1;
            else
                this.txtAnnounceFlag.Text = Resources.Resource.AnnounceFlag2;
            this.txtAnnounceUnitNo.Text = dt.Rows[0]["AnnounceUnitNo"].ToString();
            if (dt.Rows[0]["Source"].ToString() == "0")
                this.txtSource.Text = Resources.Resource.AnnounceSource1;
            else
                this.txtSource.Text = Resources.Resource.AnnounceSource2;
            this.txtTitle.Text = dt.Rows[0]["Title"].ToString();
            this.txtContents.Text = dt.Rows[0]["Contents"].ToString();

            Js.BLL.BaseDal bdal = new Js.BLL.BaseDal(FormID);
            DataTable dtSub = bdal.GetSubDetail(this.txtAnnounceID.Text).Tables[0];

            this.GridView1.DataSource = dtSub.DefaultView;
            this.GridView1.DataBind();
        }
    }
    protected void btnFirst_Click(object sender, EventArgs e)
    {
        Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID);
        DataTable dt = dal.GetRecord("F", this.txtAnnounceID.Text);
        BindData(dt);
    }
    protected void btnPre_Click(object sender, EventArgs e)
    {
        Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID);
        DataTable dt = dal.GetRecord("P", this.txtAnnounceID.Text);
        if (dt != null)
            BindData(dt);
    }
    protected void btnNext_Click(object sender, EventArgs e)
    {
        Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID);
        DataTable dt = dal.GetRecord("N", this.txtAnnounceID.Text);
        if (dt != null)
            BindData(dt);
    }
    protected void btnLast_Click(object sender, EventArgs e)
    {
        Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID);
        DataTable dt = dal.GetRecord("L", this.txtAnnounceID.Text);
        BindData(dt);
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {

    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID);
        string strID = this.txtAnnounceID.Text;
        dal.Delete(strID);

        btnNext_Click(sender, e);
        if (this.txtAnnounceID.Text == strID)
            btnPre_Click(sender, e);
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            switch (e.Row.Cells[1].Text.ToString())
            {
                case "0":
                    e.Row.Cells[1].Text = Resources.Resource.AnnounceFlag1;
                    break;
                case "1":
                    e.Row.Cells[1].Text = Resources.Resource.AnnounceFlag2;
                    break;
            }
        }
    }
}