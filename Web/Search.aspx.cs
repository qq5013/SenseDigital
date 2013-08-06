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

public partial class Search : BasePage
{
    protected int SysID;
    protected int PermissionID;
    protected string FormID;
    protected void Page_Load(object sender, EventArgs e)
    {
        
        FormID = Request.Params["FormID"] + "";
        BindData();
    }
    protected void btnEnter_Click(object sender, EventArgs e)
    {
        string strWhere = "1=1 ";
        RadioButton rb = new RadioButton();
        Js.BLL.Sys.SysComDal dal = new Js.BLL.Sys.SysComDal();
        DataTable dt = dal.SearchTable(FormID, true).Tables[0];
        for (int t = 0; t < dt.Rows.Count; t++)
        {
            if (dt.Rows[t]["OptionText"].ToString().Trim() == "")
            {
                TextBox txt = (TextBox)this.PlaceHolder1.FindControl("txt" + dt.Rows[t]["FieldName"].ToString());
                DropDownList ddl = (DropDownList)this.PlaceHolder1.FindControl("ddl" + dt.Rows[t]["FieldName"].ToString());
                if (txt.Text.Trim() != "")
                {
                    if ((dt.Rows[t]["FieldType"].ToString()) == "nvarchar" || (dt.Rows[t]["FieldType"].ToString()) == "varchar" || (dt.Rows[t]["FieldType"].ToString()) == "text")
                    {
                        if (ddl.SelectedValue == "左含")
                            strWhere += "And " + dt.Rows[t]["FieldName"].ToString() + " like N'" + txt.Text.Trim().Replace("'", "''") + "%' ";
                        else if (ddl.SelectedValue == "全含")
                            strWhere += "And " + dt.Rows[t]["FieldName"].ToString() + " like N'%" + txt.Text.Trim().Replace("'", "''") + "%' ";
                        else if (ddl.SelectedValue == "右含")
                            strWhere += "And " + dt.Rows[t]["FieldName"].ToString() + " like N'%" + txt.Text.Trim().Replace("'", "''") + "' ";
                        else
                            strWhere += "And " + dt.Rows[t]["FieldName"].ToString() + ddl.SelectedValue + "N'" + txt.Text.Trim().Replace("'", "''") + "' ";
                    }
                    else if (dt.Rows[t]["FieldType"].ToString() == "datetime") //datetime
                    {
                        string strDate = Js.Com.PageValidate.ParseDateTime(txt.Text.Trim(), Js.Com.User.strDateFormat).ToString("yyyy/MM/dd");
                        if(dt.Rows[t]["FieldName"].ToString().ToLower().IndexOf("convert")>=0)
                            strWhere += "And " + dt.Rows[t]["FieldName"].ToString() + " like '%" + strDate + "%' ";
                        else
                            strWhere += "And convert(varchar(10)," + dt.Rows[t]["FieldName"].ToString() + ",111) like '%" + strDate + "%' ";
                        //strWhere += "And convert(varchar(10)," + dt.Rows[t]["FieldName"].ToString() + ",112) like '%" + txt.Text.Trim().Replace("'", "''").Replace("/", "").Replace("-", "").Replace(".", "") + "%' ";
                    }
                    else   //numeric
                        strWhere += "And " + dt.Rows[t]["FieldName"].ToString() + ddl.SelectedValue + txt.Text.Trim().Replace("'", "''");
                }
            }
            else
            {
                string[] OptionText;
                OptionText = dt.Rows[t]["OptionText"].ToString().Split(',');
                if (dt.Rows[t]["ControlType"].ToString().Trim() == "RadioButton")
                {
                    for (int j = 0; j < OptionText.Length; j++)
                    {
                        rb = (RadioButton)this.PlaceHolder1.FindControl(dt.Rows[t]["FieldName"].ToString() + j);
                        if (rb.Checked)
                        {
                            strWhere += " And " + dt.Rows[t]["FieldName"].ToString() + " = " + j;
                            break;
                        }
                    }
                }
                else if (dt.Rows[t]["ControlType"].ToString().Trim() == "DropDownList")
                {
                    DropDownList ddl = (DropDownList)this.PlaceHolder1.FindControl("ddl" + dt.Rows[t]["FieldName"].ToString());
                    if (dt.Rows[t]["FieldType"].ToString() != "nvarchar")
                    {
                        if (ddl.SelectedIndex > 0)
                        {
                            int k = ddl.SelectedIndex - 1;
                            if (dt.Rows[t]["FieldName"].ToString() == "會員" && k > 0)
                                strWhere += " And " + dt.Rows[t]["FieldName"].ToString() + " >= " + k;
                            else if (dt.Rows[t]["FieldName"].ToString() == "會員" && k <= 0)
                                strWhere += " And " + dt.Rows[t]["FieldName"].ToString() + " <= " + k;
                            else
                                strWhere += " And " + dt.Rows[t]["FieldName"].ToString() + " = " + k;
                        }
                    }
                    else
                    {
                        if (ddl.SelectedValue != "" && ddl.SelectedValue != "<請選擇>")
                            strWhere += " And " + dt.Rows[t]["FieldName"].ToString() + " = '" + ddl.SelectedValue + "'";
                    }
                }
            }
        }
        hdnReturn.Value = strWhere;
        ClientScript.RegisterStartupScript(this.GetType(), "Submit", "<script>window.parent.returnValue = document.getElementById('hdnReturn').value;window.parent.close();</script>");
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterStartupScript(this.GetType(), "Submit", "<script> document.getElementById('hdnReturn').value=\"false\";window.parent.returnValue = document.getElementById('hdnReturn').value;window.parent.close();</script>");
    }
    #region BindData
    private void BindData()
    {
        Js.BLL.Sys.SysComDal dal = new Js.BLL.Sys.SysComDal();
        DataTable dt = dal.SearchTable(FormID, true).Tables[0];
        HtmlTable tb;
        HtmlTableRow tr;
        HtmlTableCell ce;

        tb = new HtmlTable();
        tb.Border = 1;
        tb.BorderColor = "#93bee2";
        tb.CellPadding = 0;
        tb.CellSpacing = 0;
        tb.Width = "98%";
        tb.Style["BORDER-COLLAPSE"] = "collapse";

        int i = 0;
        tr = new HtmlTableRow();
        for (int t = 0; t < dt.Rows.Count; t++)
        {
            ce = new HtmlTableCell();
            ce.Style["font-size"] = "9pt";
            ce.Style["white-space"] = "nowrap";
            int width = dt.Rows[t]["FieldCName"].ToString().Length * 13;
            ce.Width = width.ToString() + "px";
            ce.Align = "left";
            ce.InnerText = dt.Rows[t]["FieldCName"].ToString();
            tr.Controls.Add(ce);
            ce = new HtmlTableCell();
            ce.Style["font-size"] = "9pt";
            ce.Width = "210";
            #region OptionText!=""
            if (dt.Rows[t]["OptionText"].ToString().Trim() != "")
            {
                string[] OptionText;
                OptionText = dt.Rows[t]["OptionText"].ToString().Split(',');
                if (dt.Rows[t]["ControlType"].ToString().Trim() == "RadioButton")
                {
                    RadioButton rb;
                    for (int j = 0; j < OptionText.Length; j++)
                    {
                        rb = new RadioButton();
                        rb.ID = dt.Rows[t]["FieldName"].ToString() + j;
                        rb.Text = OptionText[j];
                        rb.GroupName = dt.Rows[t]["FieldName"].ToString();
                        ce.Controls.Add(rb);
                    }
                }
                else if (dt.Rows[t]["ControlType"].ToString().Trim() == "DropDownList")
                {
                    DropDownList ddl = new DropDownList();
                    ddl.Style["font-size"] = "9pt";
                    ddl.ID = "ddl" + dt.Rows[t]["FieldName"].ToString();
                    ddl.CssClass = "TextBox";
                    ddl.Width = 200;
                    if (dt.Rows[t]["OptionText"].ToString().Trim() == "DataBind")
                    {
                        //dllBindData(ddl, dt.Rows[t]["FieldCName"].ToString());
                    }
                    else
                    {
                        ddl.Items.Insert(0, new ListItem("<請選擇>"));
                        for (int k = 1; k <= OptionText.Length; k++)
                            ddl.Items.Insert(k, new ListItem(OptionText[k - 1]));
                    }
                    ce.Controls.Add(ddl);
                }
            }
            #endregion
            #region optiontext==""
            else
            {
                if ((dt.Rows[t]["FieldType"].ToString()) == "nvarchar" || (dt.Rows[t]["FieldType"].ToString()) == "varchar" || (dt.Rows[t]["FieldType"].ToString()) == "text" || dt.Rows[t]["FieldType"].ToString() == "datetime")
                {
                    DropDownList ddl = new DropDownList();
                    ddl.CssClass = "TextBox";
                    ddl.Style[HtmlTextWriterStyle.FontSize] = "9pt";
                    ddl.Items.Insert(0, new ListItem("全含"));
                    ddl.Items.Insert(1, new ListItem("左含"));
                    ddl.Items.Insert(2, new ListItem("右含"));
                    ddl.Items.Insert(3, new ListItem(">="));
                    ddl.Items.Insert(4, new ListItem("="));
                    ddl.Items.Insert(5, new ListItem("<="));
                    ddl.Items.Insert(6, new ListItem("<>"));
                    ddl.Items.Insert(7, new ListItem(">"));
                    ddl.Items.Insert(8, new ListItem("<"));
                    ddl.Width = 50;
                    ddl.ID = "ddl" + dt.Rows[t]["FieldName"].ToString();
                    ce.Controls.Add(ddl);

                    TextBox txt = new TextBox();
                    txt.ID = "txt" + dt.Rows[t]["FieldName"].ToString();
                    txt.Width = 150;
                    txt.CssClass = "TextBox";
                    ce.Controls.Add(txt);
                }
                else
                {
                    DropDownList ddl = new DropDownList();
                    ddl.CssClass = "TextBox";
                    ddl.Style[HtmlTextWriterStyle.FontSize] = "9pt";
                    ddl.Items.Insert(0, new ListItem(">="));
                    ddl.Items.Insert(1, new ListItem("="));
                    ddl.Items.Insert(2, new ListItem("<="));
                    ddl.Items.Insert(3, new ListItem("<>"));
                    ddl.Items.Insert(4, new ListItem(">"));
                    ddl.Items.Insert(5, new ListItem("<"));
                    ddl.Width = 50;
                    ddl.ID = "ddl" + dt.Rows[t]["FieldName"].ToString();
                    ce.Controls.Add(ddl);
                    TextBox txt = new TextBox();
                    txt.ID = "txt" + dt.Rows[t]["FieldName"].ToString();
                    txt.Width = 150;
                    txt.CssClass = "TextBox";
                    ce.Controls.Add(txt);
                }
            }
            #endregion

            tr.Controls.Add(ce);
            if (i % 2 == 1)
            {
                tb.Controls.Add(tr);
                tr = new HtmlTableRow();
            }
            i += 1;
        }
        if (i % 2 == 1)
        {
            tb.Controls.Add(tr);
        }
        PlaceHolder1.Controls.Add(tb);
    }
    #endregion
    
}
