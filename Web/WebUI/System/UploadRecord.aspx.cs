using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class WebUI_System_UploadRecord : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string FormID = Request.QueryString["FormID"] + "";
        Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID);
        DataTable dt = dal.GetRecord("1=1");
        this.GridView1.DataSource = dt;
        this.GridView1.DataBind();      
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int i = e.Row.RowIndex + 1;
            e.Row.Cells[0].Text = i.ToString();
            switch (e.Row.Cells[1].Text.ToString())
            {
                case "0":
                    e.Row.Cells[1].Text = Resources.Resource.Upload_FileType1;
                    break;
                case "1":
                    e.Row.Cells[1].Text = Resources.Resource.Upload_FileType2;
                    break;
            }
        }
    }
}