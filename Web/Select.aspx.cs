using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class Select : BaseLanguage
{
    string FormID;
    bool blnMultiSelect;
    string cnKey;
    protected string IsExists = "1";
    protected string SelectField;
    private string[] spliter = { "@|$" };
    private string[] Field;
    private string[] optionTxt;
    private string strWhere;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        FormID = Request.Params["FormID"].ToString() + "";
        cnKey = Request.Params["cnKey"].ToString() + "";
        blnMultiSelect = Request.Params["Option"].ToString() == "1" ? true : false;
        strWhere = Server.UrlDecode(Request.Params["strWhere"].ToString());

        if (!Page.IsPostBack)
        {
            Bind();
            btnAll.Enabled = blnMultiSelect;
            btnClear.Enabled = blnMultiSelect;
            btnSelect.Enabled = blnMultiSelect;
            ViewState["CurrentPage"] = 1;
            dataBind(int.Parse(ViewState["CurrentPage"].ToString()));
            ViewState["SelectField"] = SelectField;
        }
    }
    private void Bind()
    {
        Js.BLL.Sys.SysComDal dal = new Js.BLL.Sys.SysComDal();
        DataTable dt = dal.SearchTable(FormID, false).Tables[0];
        this.ddlFieldName.DataSource = dal.SearchTable(FormID, false).Tables[0];
        this.ddlFieldName.DataTextField = "FieldCName";
        this.ddlFieldName.DataValueField = "FieldCName";
        this.ddlFieldName.DataBind();
    }
   
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string strReturn = "";
        GridView1.HeaderStyle.Wrap = false;
        GridView1.RowStyle.Wrap = false;
        if (e.Row.RowType == DataControlRowType.Header)
        {
            for (int i = 4; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].Wrap = false;
            }
            if (blnMultiSelect)//多選
            {
                e.Row.Cells[2].Visible = false;
                e.Row.Cells[3].Visible = false;
            }
            else
            {
                e.Row.Cells[0].Visible = false;
                e.Row.Cells[3].Visible = false;
            }

        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            
            //for (int i = 4; i < e.Row.Cells.Count; i++)
            //{
            //    e.Row.Cells[i].Wrap = false;                

            //    //e.Row.Cells[i].Text = Server.HtmlDecode(e.Row.Cells[i].Text);
            //}
            //当鼠标停留时更改背景色

            e.Row.Attributes.Add("onmouseover", "HandleMouseEvent('over')");
            //当鼠标移开时还原背景色
            e.Row.Attributes.Add("onmouseout", "HandleMouseEvent('out')");
            if (blnMultiSelect)//多選
            {
                if ((HtmlInputCheckBox)e.Row.Cells[2].FindControl("chkSelect") != null)
                {
                    HtmlInputCheckBox oChk = (HtmlInputCheckBox)e.Row.Cells[2].FindControl("chkSelect");
                    string strChkName = oChk.ClientID;
                    strReturn = "";
                    for (int i = 4; i < e.Row.Cells.Count; i++)
                    {
                        if (i == e.Row.Cells.Count - 1)
                            strReturn += "\"" + Field[i - 4] + "\": \"" + Microsoft.JScript.GlobalObject.escape(((DataRowView)e.Row.DataItem).Row.ItemArray[i - 3].ToString()) + "\"";
                        else
                            strReturn += "\"" + Field[i - 4] + "\": \"" + Microsoft.JScript.GlobalObject.escape(((DataRowView)e.Row.DataItem).Row.ItemArray[i - 3].ToString()) + "\",";
                    }
                    strReturn = "{" + strReturn + "}";

                    oChk.Attributes.Add("onclick", "AddValues('" + strChkName + "','" + strReturn + "');");
                    e.Row.Attributes.Add("ondblclick", "document.getElementById('" + strChkName + "').click();");
                    
                }
                if ((Literal)e.Row.Cells[1].FindControl("IDShow") != null)
                {
                    if (HdnSelectedValues.Value.IndexOf(Server.UrlEncode(((Literal)e.Row.Cells[1].FindControl("IDShow")).Text.Replace("&nbsp;", ""))) >= 0)
                    {
                        HtmlInputCheckBox ChkSelected = (HtmlInputCheckBox)(e.Row.Cells[0].FindControl("ChkSelect"));
                        ChkSelected.Checked = true;
                    }
                }
                e.Row.Cells[2].Visible = false;
                e.Row.Cells[3].Visible = false;

            }
            else//單選
            {
                if ((Button)e.Row.Cells[2].FindControl("btnSingle") != null)
                {
                    Button btn = (Button)e.Row.Cells[2].FindControl("btnSingle");

                    strReturn = "";
                    for (int i = 4; i < e.Row.Cells.Count; i++)
                    {
                        if (i == e.Row.Cells.Count - 1)
                            strReturn += "\"" + Field[i - 4] + "\": \"" + Microsoft.JScript.GlobalObject.escape(((DataRowView)e.Row.DataItem).Row.ItemArray[i - 3].ToString()) + "\"";
                        else
                            strReturn += "\"" + Field[i - 4] + "\": \"" + Microsoft.JScript.GlobalObject.escape(((DataRowView)e.Row.DataItem).Row.ItemArray[i - 3].ToString()) + "\",";
                    }
                    strReturn = "{" + strReturn + "}";
                    btn.Attributes.Add("onclick", "SelectPage.HdnSelectedValues.value ='[" + strReturn + "]';window.parent.returnValue = document.getElementById('HdnSelectedValues').value;window.parent.close();");
                    e.Row.Attributes.Add("ondblclick", "SelectPage.HdnSelectedValues.value ='[" + strReturn + "]';window.parent.returnValue = document.getElementById('HdnSelectedValues').value;window.parent.close();");
                }

                e.Row.Cells[0].Visible = false;
                e.Row.Cells[3].Visible = false;
            }

            int n = 0;
            int nBase = this.GridView1.Columns.Count;

            DataRowView drv = e.Row.DataItem as DataRowView;
            DataTable db = drv.DataView.Table;

            foreach (DataColumn col in db.Columns)
            {
                n = nBase + col.Ordinal - 0;
                e.Row.Cells[n].Wrap = false;
                if (col.DataType == typeof(System.Decimal) || col.DataType == typeof(System.Int32) || col.DataType == typeof(System.Byte) || col.DataType == typeof(System.Int32))
                    e.Row.Cells[n].HorizontalAlign = HorizontalAlign.Right;
                else if (col.DataType == typeof(System.Boolean))
                    e.Row.Cells[n].HorizontalAlign = HorizontalAlign.Center;
                else
                    e.Row.Cells[n].HorizontalAlign = HorizontalAlign.Left;

                for (int j = 0; j < optionTxt.Length; j++)
                {
                    string[] strFld = optionTxt[j].Split(':');
                    if (strFld[0]==col.ColumnName)
                    {
                        string[] strTmp = new string[] { };

                        strTmp = strFld[1].Split(',');

                        e.Row.Cells[n].Text = strTmp[int.Parse(e.Row.Cells[n].Text)];
                        e.Row.Cells[n].HorizontalAlign = HorizontalAlign.Center;
                    }
                }
            }
        }
    }
    private void dataBind(int PageIndex)
    {
        int record_Count = 0;
        int PageCount =0;
        Js.BLL.Sys.SysComDal dal = new Js.BLL.Sys.SysComDal(cnKey);
        DataTable dt = dal.GetSearchSelectSQL(FormID, PageIndex, strWhere, GridView1.PageSize, out PageCount, out record_Count, out SelectField).Tables[0];

        string strOptionTxt = "";
        DataTable dt1 = dal.SearchTable(FormID, true).Tables[0];
        for (int i = 0; i < dt1.Rows.Count; i++)
        {
            if (dt1.Rows[i]["FieldType"].ToString().ToLower() == "tinyint" && dt1.Rows[i]["controltype"].ToString().ToLower() == "dropdownlist" && dt1.Rows[i]["OptionText"].ToString().Trim() != "")
            {
                if (strOptionTxt=="")
                    strOptionTxt = dt1.Rows[i]["FieldCName"].ToString() + ":" + dt1.Rows[i]["OptionText"].ToString().Trim();
                else
                    strOptionTxt += "|" + dt1.Rows[i]["FieldCName"].ToString() + ":" + dt1.Rows[i]["OptionText"].ToString().Trim();
            }
        }
        
        optionTxt = strOptionTxt.Split('|');
        Field = SelectField.Split(spliter, StringSplitOptions.None);
        if (dt == null || dt.Rows.Count <= 0)
        {
            IsExists = "0";
            //Js.Common.MessageBox.Show(this, "");
            return;
        }
        ViewState["PageCount"] = PageCount;
        if (PageCount > 0)
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
            lblPage.Visible = true;
            lblPage.Text = "共 [" + record_Count + "] 筆記錄  第 [" + ViewState["CurrentPage"] + "] 頁  共 [" + PageCount + "] 頁";
            GridView1.DataSource = dt;
            GridView1.PageIndex = PageIndex;
            GridView1.DataBind();
        }
        else
        {
            this.btnFirst.Enabled = false;
            this.btnPre.Enabled = false;
            this.btnNext.Enabled = false;
            this.btnLast.Enabled = false;
            this.btnToPage.Enabled = false;
            lblPage.Visible = false;
            GridView1.DataSource = "";
            GridView1.DataBind();
        }
        for (int i = 0; i < dt.Columns.Count; i++)
        {
            if (dt.Columns[i].DataType.ToString() == "System.DateTime")
            {

            }
        }
    }
    #region   Button

    protected void btnSelect_Click(object sender, EventArgs e)
    {
        //if (blnReturn)
        ClientScript.RegisterStartupScript(this.GetType(), "Submit", "<script type=\"text/javascript\">window.parent.returnValue ='['+document.getElementById('HdnSelectedValues').value+']';window.parent.close();</script>");
        //else
        //    ClientScript.RegisterStartupScript(this.GetType(), "Submit", "<script type=\"text/javascript\">window.parent.returnValue = document.getElementById('HdnSelectedValues').value;window.parent.close();</script>");
    }
    
    protected void btnFirst_Click(object sender, EventArgs e)
    {
        ViewState["CurrentPage"] = 1;
        dataBind(int.Parse(ViewState["CurrentPage"].ToString()));
    }
    protected void btnPre_Click(object sender, EventArgs e)
    {
        ViewState["CurrentPage"] = int.Parse(ViewState["CurrentPage"].ToString()) - 1;
        dataBind(int.Parse(ViewState["CurrentPage"].ToString()));
    }
    protected void btnNext_Click(object sender, EventArgs e)
    {
        ViewState["CurrentPage"] = int.Parse(ViewState["CurrentPage"].ToString()) + 1;
        dataBind(int.Parse(ViewState["CurrentPage"].ToString()));

    }
    protected void btnLast_Click(object sender, EventArgs e)
    {
        ViewState["CurrentPage"] = int.Parse(ViewState["PageCount"].ToString());
        dataBind(int.Parse(ViewState["CurrentPage"].ToString()));
    }
    protected void btnToPage_Click(object sender, EventArgs e)
    {
        if (Js.Com.PageValidate.IsNumber(txtPage.Text))
        {
            int intPageNo = int.Parse(txtPage.Text);
            if (intPageNo > 0 && intPageNo <= int.Parse(ViewState["PageCount"].ToString()))
            {

                ViewState["CurrentPage"] = int.Parse(txtPage.Text);
                dataBind(int.Parse(ViewState["CurrentPage"].ToString()));
            }
            else
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "Info", "<script type=\"text/javascript\"> alert('請輸入正确頁碼!');document.getElementById('txtPage').focus();</script>");
            }
        }
    }

    #endregion
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        ViewState["strWhere"] = this.HiddenField1.Value;
        ViewState["CurrentPage"] = 1;
        dataBind(int.Parse(ViewState["CurrentPage"].ToString()));
    }
    protected void btnMultiSearch_Click(object sender, EventArgs e)
    {

    }
}
