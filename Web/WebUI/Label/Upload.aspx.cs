using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class WebUI_Label_Upload : BaseLanguage
{
   protected string FormID;
   protected string StyleFlag;
   protected string StyleID;
   protected string StyleName;

    protected void Page_Load(object sender, EventArgs e)
    {
        FormID = Request.QueryString["FormID"];

        StyleFlag = Request.QueryString["StyleFlag"] + "";
        StyleID = Request.QueryString["StyleID"] + "";
        StyleName = Request.QueryString["StyleName"] + "";

        this.txtUploadUserName.Text = Session["User"].ToString();
        this.txtStyleID.Text = StyleID;
        this.txtStyleName.Text = StyleName;
    }
}