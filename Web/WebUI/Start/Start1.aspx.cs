using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebUI_Start_Start1 : System.Web.UI.Page
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
    protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlLanguage.SelectedIndex == 1)
        {
            this.ltlAdddress.Text = "地址: 106 台北市信义路三段 170 之 1 号10楼";
            this.ltlWeb.Text = "本网站为信实数码股份有限公司版权所有";
            this.btnEP.Text = "企业用户";
            this.btnBU.Text = "营运用户";
        }
        if (ddlLanguage.SelectedIndex == 0)
        {
            this.ltlAdddress.Text = "地址: 106 台北市信義路三段 170 之 1 號 10 樓";
            this.ltlWeb.Text = "本網站為信實數碼股份有限公司版權所有";
            this.btnEP.Text = "企業用戶";
            this.btnBU.Text = "營運用戶";
        }
    }
}