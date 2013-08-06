using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class WebUI_System_Upload : System.Web.UI.Page
{
    protected string FormID;
    protected void Page_Load(object sender, EventArgs e)
    {
        FormID = Request.QueryString["FormID"] + "";

        if (!IsPostBack)
        {
            this.txtUserName.Text = Session["User"].ToString();
            BindEdll();
        }
    }
    private void BindEdll()
    {
        Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID);
        string filter = "1=1";
        if (this.rbFileType2.Checked)
            filter = "1=2";
        DataTable dt = dal.GetDistinctRecord("FileDesc", filter);

        if (this.rbFileType2.Checked)
        {
            DataRow dr = dt.NewRow();
            dr["FileDesc"] = Resources.Resource.EP_ProductDesc;
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["FileDesc"] = Resources.Resource.EP_ProductResumeDesc;
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["FileDesc"] = Resources.Resource.EP_ProductLogistics;
            dt.Rows.Add(dr);

        }
        this.txtFileDesc.DataSource = dt;
    }
    protected void rbFileType1_CheckedChanged(object sender, EventArgs e)
    {
        
        this.txtFileDesc.Text = "";
        BindEdll();
        if (!this.rbFileType1.Checked)
        {
            this.chk1.Checked = true;
            this.chk2.Checked = true;
            this.chk1.Enabled = false;
            this.chk2.Enabled = false;
        }
        else
        {
            this.chk1.Enabled = true;
            this.chk2.Enabled = true;
        }
    }
}