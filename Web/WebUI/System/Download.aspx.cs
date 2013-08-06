using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.IO;

public partial class WebUI_System_Download : BasePage
{
    protected string FormID;
    protected string ID;
    protected void Page_Load(object sender, EventArgs e)
    {
        ID = Request.QueryString["ID"] + "";
        FormID = Request.QueryString["FormID"] + "";
        if (!IsPostBack)
        {
            ViewState["OrderField"] = "";
            BindGrid();
            if (Session["UserType"].ToString() == "EP")
                this.GridView1.Columns[9].Visible = false;
        }
    }
    private void BindGrid()
    {
        Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID);

        string filter = "";
        if (Session["UserType"].ToString() == "BU")
            filter = "(UseUnit=1 or UseUnit=3)";
        else
            filter = "(UseUnit=2 or UseUnit=3)";
        if (this.rbFileType1.Checked)
            filter += " and FileType=0";
        else
            filter += " and FileType=1";

        DataTable dt = dal.GetOrderByRecord(filter, ViewState["OrderField"].ToString());
        for (int i = dt.Rows.Count-1; i >-1; i--)
        {
            byte UseUnit = byte.Parse(dt.Rows[i]["UseUnit"].ToString());
            string strFileName = dt.Rows[i]["FileName"].ToString();
            string strFilePath = GetFilePath(UseUnit, strFileName);
            if (!System.IO.File.Exists(strFilePath))
                dt.Rows[i].Delete();
        }
        this.GridView1.Columns[2].Visible = true;
        this.GridView1.DataSource = dt;
        this.GridView1.DataBind();
        this.GridView1.Columns[2].Visible = false;  
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (ViewState["OrderField"].ToString() != "")
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                foreach (TableCell myheader in e.Row.Cells)
                {
                    if (myheader.HasControls())
                    {
                        LinkButton button = myheader.Controls[0] as LinkButton;
                        string OrderField = ViewState["OrderField"].ToString();
                        OrderField = OrderField.Replace(" Asc", "");
                        OrderField = OrderField.Replace(" Desc", "").Trim();
                        if (button != null)
                            if (button.CommandArgument.Equals(OrderField))//如果有排序
                            {
                                if (GridViewSortDirection == SortDirection.Ascending)//按照asc顺序加入箭头
                                    myheader.Controls.Add(new LiteralControl("▲"));
                                else
                                    myheader.Controls.Add(new LiteralControl("▼"));
                            }
                    }
                }
            }
        }
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
            Button btndel = (Button)e.Row.Cells[9].FindControl("btnDelete");
            btndel.Attributes.Add("onclick", "return confirm('" + Resources.Resource.Confirm_Delete + "')");
        }
    }
    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        string sortExpression = e.SortExpression;
        if (GridViewSortDirection == SortDirection.Ascending)
        {
            GridViewSortDirection = SortDirection.Descending;
            ViewState["OrderField"] = sortExpression + " Desc ";

            BindGrid();
            this.GridView1.SortedAscendingCellStyle.BackColor = System.Drawing.Color.Red;
        }
        else if (GridViewSortDirection == SortDirection.Descending)
        {
            GridViewSortDirection = SortDirection.Ascending;
            ViewState["OrderField"] = sortExpression + " Asc ";
            //排序並重新綁定
            BindGrid();
            this.GridView1.SortedAscendingCellStyle.BackColor = System.Drawing.Color.Red;
        }
    }
    /// <summary>
    /// 排序方向屬性
    /// </summary>
    public SortDirection GridViewSortDirection
    {
        get
        {
            if (ViewState["sortDirection"] == null)
                ViewState["sortDirection"] = SortDirection.Ascending;
            return (SortDirection)ViewState["sortDirection"];
        }
        set
        {
            ViewState["sortDirection"] = value;
        }
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DownLoad")
        {
            GridViewRow row = ((Control)e.CommandSource).BindingContainer as GridViewRow;
            int RecordID = int.Parse(e.CommandArgument.ToString());

            byte UseUnit = byte.Parse(row.Cells[2].Text);
            string strFileName = row.Cells[4].Text;
            string strFilePath = GetFilePath(UseUnit, strFileName);

            Common.DirectDownLoad(strFilePath);
        }
        else if (e.CommandName == "Del")
        {
            GridViewRow row = ((Control)e.CommandSource).BindingContainer as GridViewRow;
            byte UseUnit = byte.Parse(row.Cells[2].Text);
            string strFileName = row.Cells[4].Text;
            string strFilePath = GetFilePath(UseUnit,strFileName);

            Common.DeleteFile(strFilePath);
            BindGrid();
        }

    }
    private string GetFilePath(byte UseUnit, string FileName)
    {
        string strFilePath;
        string documentName = "Other";
        if (this.rbFileType2.Checked)
            documentName = "Sample";

        if (UseUnit == 1)
            strFilePath = ConfigurationManager.AppSettings["UploadPath"] + @"\COM\" + documentName + @"\BU\";
        else if (UseUnit == 2)
            strFilePath = ConfigurationManager.AppSettings["UploadPath"] + @"\COM\" + documentName + @"\EP\";
        else
            strFilePath = ConfigurationManager.AppSettings["UploadPath"] + @"\COM\" + documentName + @"\ALL\";

        return strFilePath + FileName;
    }
    protected void rbFileType1_CheckedChanged(object sender, EventArgs e)
    {
        BindGrid();
    }
}