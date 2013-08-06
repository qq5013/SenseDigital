using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebUI_System_ReplyMessageEdit : BasePage
{
    protected string AnnounceID;
    protected string FormID;
    protected void Page_Load(object sender, EventArgs e)
    {
        AnnounceID = Request.Params["AnnounceID"];
        FormID = Request.Params["FormID"];
        if (!Page.IsPostBack)
        {
            int UserFlag = 0;
            if (Session["UserType"].ToString() == "EP")
                UserFlag = 1;

            Js.BLL.System.MessageDal dal = new Js.BLL.System.MessageDal();
            DataTable dt = dal.GetSubRecord(AnnounceID, UserFlag,Session["User"].ToString());
            if (dt.Rows.Count > 0)
            {
                this.txtAnnouncerUserName.Text = Session["User"].ToString();
                this.txtReceiverFlag.Text = dt.Rows[0]["ReceiveUnitNo"].ToString();
                this.txtReceiver.Text = dt.Rows[0]["ReceiveUnitNo"].ToString();
            }
        }
    }
    protected void btnReply_Click(object sender, EventArgs e)
    {
        Js.BLL.System.MessageDal dal = new Js.BLL.System.MessageDal();
        int UserFlag = 0;
        if(Session["UserType"].ToString()=="EP")
            UserFlag = 1;
        dal.ReplyMessage(AnnounceID, UserFlag, Session["User"].ToString(), this.txtReplyTitle.Text, this.txtReplyContent.Text);
        ClientScript.RegisterStartupScript(this.GetType(), "Submit", "<script type=\"text/javascript\">window.parent.returnValue ='1';window.parent.close();</script>");

    }
}