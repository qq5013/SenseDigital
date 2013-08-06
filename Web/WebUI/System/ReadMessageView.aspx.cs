using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class WebUI_System_ReadMessageView : BasePage
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
            Js.BLL.System.MessageDal mdal = new Js.BLL.System.MessageDal();
            int UserFlag;

            if (this.txtAnnouncerUserName.Text == Session["User"].ToString())
            {
                UserFlag = int.Parse(this.txtReceiverFlag0.Text);
                mdal.UpdateMessageRead1(ID, UserFlag, this.txtReceiverUserName.Text);
            }
            else
            {
                UserFlag = Session["UserType"].ToString() == "BU" ? 0 : 1;
                mdal.UpdateMessageRead(ID, UserFlag, Session["User"].ToString());
            }
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
            if (dt.Rows[0]["Source"].ToString() == "0")
                this.txtSource.Text = Resources.Resource.AnnounceSource1;
            else
                this.txtSource.Text = Resources.Resource.AnnounceSource2;
            this.txtTitle.Text = dt.Rows[0]["Title"].ToString();
            this.txtContents.Text = dt.Rows[0]["Contents"].ToString();

            this.txtReceiverFlag0.Text = dt.Rows[0]["ReceiverFlag"].ToString();
            if (dt.Rows[0]["ReceiverFlag"].ToString() == "0")
                this.txtReceiverFlag.Text = Resources.Resource.AnnounceFlag1;
            else
                this.txtReceiverFlag.Text = Resources.Resource.AnnounceFlag2;
            this.txtReceiveUnitNo.Text = dt.Rows[0]["ReceiveUnitNo"].ToString();
            this.txtReceiverUserName.Text = dt.Rows[0]["ReceiverUserName"].ToString();
            this.txtReceiver.Text = dt.Rows[0]["Receiver"].ToString();
            this.txtReplyTitle.Text = dt.Rows[0]["ReplyTitle"].ToString();
            this.txtReplyContent.Text = dt.Rows[0]["ReplyContent"].ToString();
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

    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {

    }
}