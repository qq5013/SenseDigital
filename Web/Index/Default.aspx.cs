using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.ltlUserName.Text += Session["User"].ToString();
            //System.Net.IPAddress addr = new System.Net.IPAddress(Dns.GetHostByName(Dns.GetHostName()).AddressList[0].Address);
            //this.ltlIP.Text += addr.ToString();
            //this.ltlIP.Text += Request.UserHostAddress;
            this.ltlIP.Text += Page.Request.UserHostAddress;
        }
    }
}
