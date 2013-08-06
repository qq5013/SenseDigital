using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebUI_System_ParameterCloseDate : BasePage
{
    protected string CloseDate;
    protected string FormID;
    protected void Page_Load(object sender, EventArgs e)
    {
        CloseDate = Request.Params["AnnounceID"]+"";
        FormID = Request.Params["FormID"];
        if (!Page.IsPostBack)
        {
            this.txtCloseDate.Text = CloseDate;
        }
    }
    protected void btnOK_Click(object sender, EventArgs e)
    {
        Js.BLL.BusinessUnit.ParameterDal dal = new Js.BLL.BusinessUnit.ParameterDal();
        Js.Model.BusinessUnit.ParameterInfo model = new Js.Model.BusinessUnit.ParameterInfo();
        model.CloseDate = this.txtCloseDate.DateValue;
        model.LastModifyDate = DateTime.Now;
        model.LastModifyUserName = Session["User"].ToString();

        dal.UpdateCloseDate(model);

        ClientScript.RegisterStartupScript(this.GetType(), "Submit", "<script type=\"text/javascript\">window.parent.returnValue ='1';window.parent.close();</script>");
    }
}