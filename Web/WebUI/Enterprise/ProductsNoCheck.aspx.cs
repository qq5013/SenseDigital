using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebUI_Enterprise_ProductsNoCheck : BasePage
{
    int PageSize = 15;
    protected string FormID;
    protected string cnKey = "Enterprise";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["cnKey"] != null)
            cnKey = Session["cnKey"].ToString();
        FormID = Request.QueryString["FormID"];
        if (!Page.IsPostBack)
        {
            dataSearch();
            
            SetSession();

            ViewState["PageSize"] = PageSize;
            ViewState["CurrentPage"] = 1;
            ViewState["OrderField"] = "";
            ViewState["strWhere"] = string.Format("EnterpriseID='{0}'", Session["EnterpriseID"].ToString());

            SetBtnEnabled("");

            //SetPermission();
            if (Session["UserType"].ToString() == "BU")
            {
                this.btnUploadExcel.Visible = false;
                this.btnImport.Visible = false;
                this.btnCompare.Visible = false;
                this.btnCheck.Visible = false;
                this.btnUnCheck.Visible = false;
                this.btnUpload.Visible = false;
            }
        }
    }
    /// <summary>
    /// 綁定查詢
    /// </summary>
    private void dataSearch()
    {
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
        
        if (Session["EnterpriseID"] != null)
            this.ddlEnterpriseID.SelectedValue = Session["EnterpriseID"].ToString();
        //標籤模組加這句
        //if(Session["UserType"].ToString()=="BU")
        //    this.ddlEnterpriseID.Items.Insert(0, new ListItem("", ""));
    }
    private void SetSession()
    {
        Session["cnKey"] = this.ddlEnterpriseID.SelectedValue;
        cnKey = Session["cnKey"].ToString();
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
        if (ds.Tables[0].Rows.Count > 0)
        {
            this.txtUploadDate.Text = Js.Com.PageValidate.ParseDateTime(ds.Tables[0].Rows[0]["UploadDate"].ToString());
            this.txtState0.Text = ds.Tables[0].Rows[0]["State"].ToString();
            if (ds.Tables[0].Rows[0]["State"].ToString() == "0")
                this.txtState.Text = Resources.Resource.CheckState0;
            else if (ds.Tables[0].Rows[0]["State"].ToString() == "1")
                this.txtState.Text = Resources.Resource.CheckState1;
            else if (ds.Tables[0].Rows[0]["State"].ToString() == "2")
                this.txtState.Text = Resources.Resource.CheckState2;
            else
                this.txtState.Text = Resources.Resource.CheckState3;
        }
        else
        {
            this.txtUploadDate.Text = "";
            this.txtState.Text = "";
        }

        GridView1.DataSource = ds.Tables[0].DefaultView;
        GridView1.DataBind();

        if (this.GridView1.Rows.Count > 0)
        {
            if (this.GridView1.Rows[0].Cells[18].Text.Replace("&nbsp;", "").Trim().Length > 0)
            {
                this.btnCheck.Enabled = false;
                this.btnUnCheck.Enabled = true;
            }
            else
            {
                this.btnCheck.Enabled = true;
                this.btnUnCheck.Enabled = false;
            }
        }
        else
        {
            this.btnCheck.Enabled = false;
            this.btnUnCheck.Enabled = false;
        }
       
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
    protected void btnPrint_Click(object sender, EventArgs e)
    {

    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int i = e.Row.RowIndex + 1;
            e.Row.Cells[1].Text = i.ToString();
            //switch (e.Row.Cells[e.Row.Cells.Count - 1].Text.ToString())
            switch (e.Row.Cells[2].Text.ToString())
            {
                case "0":
                    e.Row.Cells[2].Text = Resources.Resource.Add;
                    break;
                case "1":
                    e.Row.Cells[2].Text = Resources.Resource.Modify;
                    break;
            }
        }
    }
   
    protected void btnToPage_Click(object sender, System.EventArgs e)
    {
        ViewState["CurrentPage"] = Convert.ToInt32(txtPageNo.Text);
        SetBtnEnabled("");
    }
    #region Button Event

    protected void btnDelete_Click(object sender, EventArgs e)
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
                iReturn = sdal.GetBillCanBeEdit(FormID, Session["EnterpriseID"].ToString(), "ProductID='" + hk.Text + "'");

                if (iReturn == 1)
                    JScript.Instance.ShowMessage(this.updatePanel, hk.Text + Resources.Resource.NotDelete_Checked);
                else if (iReturn == 2)
                    JScript.Instance.ShowMessage(this.updatePanel, hk.Text + Resources.Resource.NotDelete_IsUsed);
                else
                    //dal.Delete(Session["EnterpriseID"].ToString(), "ProductID='" + hk.Text + "'");
                    dal.Delete(hk.Text, "EnterpriseID='" + Session["EnterpriseID"].ToString() + "'");               
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

    protected void ddlEnterpriseID_SelectedIndexChanged(object sender, EventArgs e)
    {        
        ViewState["CurrentPage"] = 1;
        ViewState["OrderField"] = "";
        SetSession();
        ViewState["strWhere"] = string.Format("EnterpriseID='{0}'", Session["EnterpriseID"].ToString());       
        SetBtnEnabled("");
    }   
    
    protected void btnImport_Click(object sender, EventArgs e)
    {
        string strFileName = ConfigurationManager.AppSettings["UploadPath"] + this.ddlEnterpriseID.SelectedValue + @"\Product\Product.xls";
        if (!System.IO.File.Exists(strFileName))
        {
            JScript.Instance.ShowMessage(this.updatePanel, Resources.Resource.EP_ProdcutExcelNotExists);
            return;
        }

        Js.BLL.Enterprise.CheckDal dal = new Js.BLL.Enterprise.CheckDal(FormID,cnKey);
        DataTable dt = dal.ImportProduct1(Session["EnterpriseID"].ToString(), Session["User"].ToString());
        
        DataTable dtTemp = dal.GetProductTemp("1=2");
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataRow dr = dtTemp.NewRow();
            dr["UserName"] = Session["User"].ToString();
            for (int j = 0; j < dt.Columns.Count; j++)
            {
                dr[dt.Columns[j].ColumnName] = dt.Rows[i][j];
            }
            dtTemp.Rows.Add(dr);
        }

        dal.SaveProduct(dtTemp, Session["User"].ToString());
        SetBtnEnabled("");
    }
    protected void btnCompare_Click(object sender, EventArgs e)
    {
        Js.BLL.Enterprise.CheckDal dal = new Js.BLL.Enterprise.CheckDal(FormID, cnKey);

        dal.UpdateCheckState(Session["EnterpriseID"].ToString());
        SetBtnEnabled("");
    }
    protected void btnCheck_Click(object sender, EventArgs e)
    {
        Js.BLL.Enterprise.CheckDal dal = new Js.BLL.Enterprise.CheckDal(FormID, cnKey);
        dal.Check(this.ddlEnterpriseID.SelectedValue, Session["User"].ToString());

        SetBtnEnabled("");
    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        Js.BLL.Enterprise.CheckDal dal = new Js.BLL.Enterprise.CheckDal(FormID, cnKey);

        dal.UpoladBusinessUnit(Session["EnterpriseID"].ToString(), Session["User"].ToString());
        this.txtUploadDate.Text = Js.Com.PageValidate.ParseDateTime(DateTime.Now.ToString());
        SetBtnEnabled("");
        JScript.Instance.ShowMessage(this.updatePanel, Resources.Resource.EP_ProductUploadSuccess);
    }
    protected void btnUnCheck_Click(object sender, EventArgs e)
    {
        Js.BLL.Enterprise.CheckDal dal = new Js.BLL.Enterprise.CheckDal(FormID, cnKey);
        dal.UnCheck(this.ddlEnterpriseID.SelectedValue);
        this.txtUploadDate.Text = "";
        SetBtnEnabled("");
    }
}