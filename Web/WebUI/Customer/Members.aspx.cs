using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebUI_Customer_Members : BasePage
{
    int PageSize = 15;
    protected string FormID;
    protected string strd = "yyyy/MM/dd";
    protected string cnKey = "Customer";
    protected void Page_Load(object sender, EventArgs e)
    {
        FormID = Request.QueryString["FormID"];
        if (!Page.IsPostBack)
        {
            ViewState["PageSize"] = PageSize;
            ViewState["CurrentPage"] = 1;
            ViewState["OrderField"] = "";
            ViewState["strWhere"] = "";
            SetBtnEnabled("");
            dataSearch();
            //SetPermission();
        }
    }
    /// <summary>
    /// 綁定查詢
    /// </summary>
    private void dataSearch()
    {
        Js.BLL.Sys.SysComDal dal = new Js.BLL.Sys.SysComDal();
        this.ddlFieldName.DataSource = dal.SearchTable(FormID, false).Tables[0];
        this.ddlFieldName.DataTextField = "FieldCName";
        this.ddlFieldName.DataValueField = "FieldName";
        this.ddlFieldName.DataBind();

        this.ddlPageSize.Items.Add(new ListItem("15", "15"));
        this.ddlPageSize.Items.Add(new ListItem("20", "20"));
        this.ddlPageSize.Items.Add(new ListItem("25", "25"));
        this.ddlPageSize.Items.Add(new ListItem("30", "30"));
        this.ddlPageSize.Items.Add(new ListItem("40", "40"));
        this.ddlPageSize.Items.Add(new ListItem("50", "50"));
    }
    /// <summary>
    /// 綁定GirdView
    /// </summary>
    /// <param name="pageIndex"></param>
    private void BindGrid(int pageIndex)
    {
        Js.BLL.Sys.SysComDal dal = new Js.BLL.Sys.SysComDal(cnKey);

        DataSet ds = new DataSet();
        int RecordCount, PageCount;
        string strWhere = ViewState["strWhere"].ToString();
        ds = dal.SelectTable(FormID, pageIndex, strWhere, int.Parse(ViewState["PageSize"].ToString()), ViewState["OrderField"].ToString(), out PageCount, out RecordCount);
        if (ViewState["CurrentPage"].ToString() == "0")
            ViewState["CurrentPage"] = PageCount;
        if (RecordCount != 0)
        {
            this.btnLast.Enabled = true;
            this.btnFirst.Enabled = true;
            this.btnToPage.Enabled = true;

            if (int.Parse(ViewState["CurrentPage"].ToString()) > 1)
                this.btnPre.Enabled = true;
            else
                this.btnPre.Enabled = false;

            if (int.Parse(ViewState["CurrentPage"].ToString()) < PageCount)
                this.btnNext.Enabled = true;
            else
                this.btnNext.Enabled = false;

            lblCurrentPage.Visible = true;
            lblCurrentPage.Text = "共 [" + RecordCount.ToString() + "] 筆記錄  第 [" + ViewState["CurrentPage"] + "] 頁  共 [" + PageCount.ToString() + "] 頁";
            lblCurrentPage.Text = "Records：" + RecordCount.ToString() + " Page：" + ViewState["CurrentPage"] + "/" + PageCount.ToString();

        }
        else
        {
            this.btnFirst.Enabled = false;
            this.btnPre.Enabled = false;
            this.btnNext.Enabled = false;
            this.btnLast.Enabled = false;
            this.btnToPage.Enabled = false;
            lblCurrentPage.Visible = false;
        }
        GridView1.DataSource = ds.Tables[0].DefaultView;
        GridView1.DataBind();
    }
    private void SetPermission()
    {
        //Js.BLL.Account.User user = new Js.BLL.Account.User(Context.User.Identity.Name);

        //string Permission = user.GetUserPermissionByPermissionID(SysID, PermissionID);

        //if (Permission.Substring(1, 1) != "1")
        //{
        //    GridView1.Columns[GridView1.Columns.Count - 2].Visible = false;
        //}
        //if (Permission.Substring(2, 1) != "1")
        //{
        //    GridView1.Columns[GridView1.Columns.Count - 1].Visible = false;
        //}
    }

    #region 翻頁處理
    protected void btnFirst_Click(object sender, System.EventArgs e)
    {
        SetBtnEnabled("F");
    }

    protected void btnLast_Click(object sender, System.EventArgs e)
    {
        SetBtnEnabled("L");
    }

    protected void btnPre_Click(object sender, System.EventArgs e)
    {
        SetBtnEnabled("P");
    }

    protected void btnNext_Click(object sender, System.EventArgs e)
    {
        SetBtnEnabled("N");
    }

    private void SetBtnEnabled(string movePage)
    {
        switch (movePage)
        {
            case "F":
                ViewState["CurrentPage"] = 1;
                break;
            case "P":
                ViewState["CurrentPage"] = int.Parse(ViewState["CurrentPage"].ToString()) - 1;
                break;
            case "N":
                ViewState["CurrentPage"] = int.Parse(ViewState["CurrentPage"].ToString()) + 1;
                break;
            case "L":
                ViewState["CurrentPage"] = 0;
                break;
            default:
                if (ViewState["CurrentPage"] == null)
                    ViewState["CurrentPage"] = 1;
                break;
        }

        BindGrid(int.Parse(ViewState["CurrentPage"].ToString()));
        ScriptManager.RegisterStartupScript(this.updatePanel, this.GetType(), "Resize", "content_resize();", true);
    }
    #endregion
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
            switch (e.Row.Cells[3].Text)
            {
                case "False":
                    e.Row.Cells[3].Text = Resources.Resource.Male;
                    break;
                case "True":
                    e.Row.Cells[3].Text = Resources.Resource.Female;
                    break;
            }            
        }
    }
    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        string sortExpression = e.SortExpression;
        if (GridViewSortDirection == SortDirection.Ascending)
        {
            GridViewSortDirection = SortDirection.Descending;
            ViewState["OrderField"] = sortExpression + " Desc ";

            SetBtnEnabled("");
            this.GridView1.SortedAscendingCellStyle.BackColor = System.Drawing.Color.Red;
        }
        else if (GridViewSortDirection == SortDirection.Descending)
        {
            GridViewSortDirection = SortDirection.Ascending;
            ViewState["OrderField"] = sortExpression + " Asc ";
            //排序並重新綁定
            SetBtnEnabled("");
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

    protected void btnToPage_Click(object sender, System.EventArgs e)
    {
        ViewState["CurrentPage"] = Convert.ToInt32(txtPageNo.Text);
        SetBtnEnabled("");
    }
    #region Button Event
    protected void btnSearch_Click(object sender, System.EventArgs e)
    {
        ViewState["strWhere"] = this.HiddenField1.Value;
        ViewState["CurrentPage"] = 1;
        SetBtnEnabled("");
    }

    protected void btnMultiSearch_Click(object sender, System.EventArgs e)
    {
        ViewState["strWhere"] = this.HiddenField1.Value;
        ViewState["CurrentPage"] = 1;
        SetBtnEnabled("");
    }

    protected void btnDeletet_Click(object sender, EventArgs e)
    {
        Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID, cnKey);
        for (int i = 0; i < this.GridView1.Rows.Count; i++)
        {
            CheckBox cb = (CheckBox)(this.GridView1.Rows[i].FindControl("cbSelect"));
            if (cb.Checked)
            {
                HyperLink hk = (HyperLink)(this.GridView1.Rows[i].FindControl("HyperLink1"));
                dal.Delete(hk.Text);
            }
        }
        SetBtnEnabled("");
    }
    #endregion

    protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["PageSize"] = this.ddlPageSize.SelectedValue;
        SetBtnEnabled("");
    }
}