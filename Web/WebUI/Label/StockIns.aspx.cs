﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class WebUI_Label_InStocks : BasePage
{
    int PageSize = 15;
    protected string FormID;
    protected string strd = "yyyy/MM/dd";
    protected string cnKey = "Label";

    protected void Page_Load(object sender, EventArgs e)
    {
        FormID = Request.QueryString["FormID"];
        if (!Page.IsPostBack)
        {
            dataSearch();
            ViewState["PageSize"] = PageSize;
            ViewState["CurrentPage"] = 1;
            ViewState["OrderField"] = "";
            ViewState["strWhere"] = "";
            if (this.ddlEnterpriseID.SelectedValue.ToString().Length > 0)
            {
                ViewState["strWhere"] = string.Format("EnterpriseID='{0}'", this.ddlEnterpriseID.SelectedValue);
                SetSession();
            }
            SetBtnEnabled("");

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

        Js.BLL.BaseDal bdal = new Js.BLL.BaseDal("BU_Enterprise");
        string filter = "1=1";
        if (Session["UserType"].ToString() == "EP")
            filter = string.Format("EnterpriseID='{0}'", Session["EnterpriseID"].ToString());
        this.ddlEnterpriseID.DataSource = bdal.GetIDNameList(filter);
        this.ddlEnterpriseID.DataTextField = "IDName";
        this.ddlEnterpriseID.DataValueField = "ID";
        this.ddlEnterpriseID.DataBind();

        if (Session["UserType"].ToString() == "BU")
            this.ddlEnterpriseID.Items.Insert(0, new ListItem("", ""));
        if (Session["EnterpriseID"] != null)
            this.ddlEnterpriseID.SelectedValue = Session["EnterpriseID"].ToString();
    }
    private void SetSession()
    {
        Session["EnterpriseID"] = this.ddlEnterpriseID.SelectedValue.ToString();
        Session["EnterpriseName"] = this.ddlEnterpriseID.SelectedItem.Text.Substring(this.ddlEnterpriseID.SelectedItem.Text.IndexOf("  ") + 1);
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
            switch (e.Row.Cells[5].Text)
            {
                case "False":
                    e.Row.Cells[5].Text = Literal22.Text;
                    break;
                case "True":
                    e.Row.Cells[5].Text = Literal2.Text;
                    break;
            }
            switch (e.Row.Cells[11].Text)
            {
                //入庫狀況  0:空白 1:序號產出中 2:序號已產出 3:圖檔檢查中 4.圖檔已檢查 5.圖檔匯入中 6.圖檔已匯入 7.壞品未作廢 8.入庫結案
                case "0":
                    e.Row.Cells[11].Text = Literal3.Text;
                    break;
                case "1":
                    e.Row.Cells[11].Text = Literal4.Text;
                    break;
                case "2":
                    e.Row.Cells[11].Text = Literal5.Text;
                    break;
                case "3":
                    e.Row.Cells[11].Text = Literal6.Text;
                    break;
                case "4":
                    e.Row.Cells[11].Text = Literal7.Text;
                    break;
                case "5":
                    e.Row.Cells[11].Text = Literal8.Text;
                    break;
                case "6":
                    e.Row.Cells[11].Text = Literal9.Text;
                    break;
                case "7":
                    e.Row.Cells[11].Text = Literal10.Text;
                    break;
                case "8":
                    e.Row.Cells[11].Text = Literal11.Text;
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
        Js.BLL.Sys.SysComDal sdal = new Js.BLL.Sys.SysComDal(cnKey);
        // Js.BLL.Sys.SysComDal sdal = new Js.BLL.Sys.SysComDal();
        int iReturn = 0;
        for (int i = 0; i < this.GridView1.Rows.Count; i++)
        {
            CheckBox cb = (CheckBox)(this.GridView1.Rows[i].FindControl("cbSelect"));
            if (cb.Checked)
            {
                HyperLink hk = (HyperLink)(this.GridView1.Rows[i].FindControl("HyperLink1"));
                iReturn = sdal.GetBillCanBeEdit(FormID, hk.Text);// "EnterpriseID='" + Session["EnterpriseID"].ToString() + "'");

                if (iReturn == 1)
                    JScript.Instance.ShowMessage(this.updatePanel, hk.Text + Resources.Resource.NotDelete_Checked);
                else if (iReturn == 2)
                    JScript.Instance.ShowMessage(this.updatePanel, hk.Text + Resources.Resource.NotDelete_IsUsed);
                else
                {
                    dal.Delete(hk.Text);//, "EnterpriseID='" + Session["EnterpriseID"].ToString() + "'");
                    dal.DeleteDetail(hk.Text);
                }
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
    protected void ddlEnterpriseID_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["CurrentPage"] = 1;
        ViewState["OrderField"] = "";
        if (this.ddlEnterpriseID.SelectedValue.ToString().Length > 0)
            ViewState["strWhere"] = string.Format("EnterpriseID='{0}'", this.ddlEnterpriseID.SelectedValue);
        else
            ViewState["strWhere"] = "";
        SetSession();
        SetBtnEnabled("");
    }
}