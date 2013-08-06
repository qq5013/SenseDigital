using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class WebUI_BusinessUnit_ProductResumeCompare : BasePage
{
    string FormID = "";
    string EnterpriseID = "";
   // string KeyField = "ResumeID";
    string cnKey = "Enterprise";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["cnKey"] != null)
            cnKey = Session["cnKey"].ToString();
        Page.SmartNavigation = true;
        EnterpriseID = Request.QueryString["EnterpriseID"] + "";
        FormID = Request.QueryString["FormID"] + "";

        if (!IsPostBack)
        {
            BindData();
            if (this.GridView1.Rows.Count > 0)
            {
                BindDetail(this.GridView1.Rows[0].Cells[2].Text);
                this.GridView1.Rows[0].BackColor = System.Drawing.Color.FromName("#fecf83");
            }
        }
    }
    private void BindData()
    {
        Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID, cnKey);        
        DataTable dt = dal.GetEnterpriseDetail(EnterpriseID).Tables[0];
        this.GridView1.DataSource = dt.DefaultView;
        this.GridView1.DataBind();
    }
    private void BindDetail(string ID)
    {       
        Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID, cnKey);
       // string filter = string.Format("Flag=1 and {0}='{1}'", KeyField, ID);
        string filter = string.Format("Flag=1 and EnterpriseID='{0}'", EnterpriseID);
        DataTable dt = dal.GetSubDetail(ID, filter).Tables[0];
        this.GridView2.DataSource = dt.DefaultView;
        this.GridView2.DataBind();

        //filter = string.Format("Flag=2 and {0}='{1}'", KeyField, ID);
        filter = string.Format("Flag=2 and EnterpriseID='{0}'", EnterpriseID);
        dt = dal.GetSubDetail(ID, filter).Tables[0];
        this.GridView3.DataSource = dt.DefaultView;
        this.GridView3.DataBind();
        if (GridView2.Rows.Count != 0 && GridView3.Rows.Count != 0)
        {
            for (var i = 0; i < this.GridView2.Columns.Count; i++)
            {
                if (GridView2.Rows[0].Cells[i].Text != GridView3.Rows[0].Cells[i].Text)
                {
                    GridView2.Rows[0].Cells[i].ForeColor = System.Drawing.Color.Red;
                    GridView3.Rows[0].Cells[i].ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    GridView2.Rows[0].Cells[i].Style.Add("display ", "none ");
                    GridView3.Rows[0].Cells[i].Style.Add("display ", "none ");
                    GridView2.HeaderRow.Cells[i].Style.Add("display ", "none ");
                    GridView3.HeaderRow.Cells[i].Style.Add("display ", "none ");                                    
                }
            }
        }
    }
    protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        BindDetail(this.GridView1.DataKeys[e.NewSelectedIndex].Value.ToString());
        this.GridView1.Rows[e.NewSelectedIndex].BackColor = System.Drawing.Color.FromName("#fecf83");
        for (var i = 0; i < this.GridView1.Rows.Count; i++)
        {
            if (i != e.NewSelectedIndex)
            {
                if (i % 2 == 0)
                    this.GridView1.Rows[i].BackColor = System.Drawing.Color.White;
                else
                    this.GridView1.Rows[i].BackColor = System.Drawing.Color.FromName("#e9f2ff");

            }
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int i = e.Row.RowIndex + 1;
            e.Row.Cells[1].Text = i.ToString();
        }
    }
}