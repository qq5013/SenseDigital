using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebUI_Start_Start : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.ddlLanguage.Items.Add(new ListItem("繁體", "zh-TW"));
            this.ddlLanguage.Items.Add(new ListItem("简体", "zh-cn"));
            this.ddlLanguage.Items.Add(new ListItem("English", "en-us"));
            //Session["CheckCode"] = "1234";
        }
    }
}