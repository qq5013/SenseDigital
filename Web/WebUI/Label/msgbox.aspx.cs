﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebUI_Label_msgbox : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ltlBillID.Text = "**確認是否要切換已估算記錄?";
    }
}