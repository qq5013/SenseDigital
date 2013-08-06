using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


namespace Js.PageControl
{
    public class ClickEventArgs : EventArgs
    {
        public ClickEventArgs(string btnID)
        {
            this.btnID = btnID;
        }
        public string btnID;
    }
    public delegate void ClickEventHandler(object sender, ClickEventArgs ce);

    [DefaultProperty("Text")]
    [ToolboxData("<{0}:Search runat=server></{0}:Search>")]
    [Bindable(true)]
    
    [DefaultValue("")]
    
    [Themeable(true)]
    [Localizable(true)]
    
    public class Search : CompositeControl
    {
        private string _strwhere;
        private DropDownList ddlSearch;
        private TextBox txtSearch;
        private DataTable _DataSource;
        private Button btnSearch;
        private Button btnMultiSearch;
        private TextBox txthidn;
        private string _strDateFormat = "yyyy/MM/dd";
        private static readonly object EventSubmitKey = new object();
        
        public Search()
        {
            ViewState["PageUrl"] = "";
            ViewState["ShowPageHeight"] = 0;
            ViewState["SysID"] = 0;
            ViewState["PermissionId"] = 0;
            ViewState["DateFormat"] = "yyyy/MM/dd";
        }
        protected override void CreateChildControls()
        {
            
            this.Controls.Clear();
            EnsureChildControls();
            this.Controls.Add(new LiteralControl("<table width=\"" + this.Width + "\" height=\"25px\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">"));
            this.Controls.Add(new LiteralControl("<tr width=\"100%\" height=\"25px\" ><td width=\"53px\">查詢欄位</td><td width=\"120px\">"));
           
            //this.Controls.Add(new LiteralControl("查詢欄位"));
            
            ddlSearch = new DropDownList();
            ddlSearch.Style[HtmlTextWriterStyle.Width] = "100%";
            //add item
            if (DataSource != null)
                ViewState["Sdt"] = DataSource;
            ddlSearch.DataSource = DataSource;
            ddlSearch.DataTextField = "FieldCName";
            ddlSearch.DataValueField = "FieldName";
            
            ddlSearch.SelectedIndex = 0;
            ddlSearch.Style[HtmlTextWriterStyle.FontSize] = "9pt";
            ddlSearch.CssClass = "TextBox";

            ddlSearch.DataBind();
            ddlSearch.SelectedIndexChanged += new EventHandler(ddlSearch_SelectedIndexChanged);
            this.Controls.Add(ddlSearch);
            //double tdWidth = 0;
            //if (this.Page_Width >= 500 && this.Page_Width < 600)
            //    tdWidth = 45;
            //else if (this.Page_Width >= 600 && this.Page_Width < 700)
            //    tdWidth = 57.1;
            //else if (this.Page_Width >= 700 && this.Page_Width < 800)
            //    tdWidth = 62.5;
            //else
            //    tdWidth = 67;
            this.Controls.Add(new LiteralControl("</td><td width=\"56px\">&nbsp;查詢內容</td><td width=\"*\" align=\"left\">"));
            txtSearch = new TextBox();
            txtSearch.Style[HtmlTextWriterStyle.Width] = "96%";

            txtSearch.CssClass = "TextBox";
            this.Controls.Add(txtSearch);
            this.Controls.Add(new LiteralControl("</td><td width=\"73px\" align=\"left\" >"));
            btnSearch = new Button();
            btnSearch.Text = "立即查詢";
            
            btnSearch.Click+=new EventHandler(this.btnSearch_Click);
            //__doPostBack
            //txtSearch.Attributes.Add("onkeydown", "if (event.keyCode==13) {event.keyCode=9;event.returnValue=false;" + this.ClientID.ToString() + ".Submit(); return false;}");

            btnSearch.CssClass = "cssbtn";
            btnSearch.Style[HtmlTextWriterStyle.Width] = "70px";
            this.Controls.Add(btnSearch);
            this.Controls.Add(new LiteralControl("</td><td width=\"72px\" align=\"center\">"));
            btnMultiSearch = new Button();
            btnMultiSearch.Text = "多條件查詢";
            btnMultiSearch.Click+=new EventHandler(btnMultiSearch_Click);
            btnMultiSearch.CssClass = "cssbtn";
            btnMultiSearch.Style[HtmlTextWriterStyle.Width] = "70px";
            this.Controls.Add(btnMultiSearch);

            txtSearch.Attributes.Add("onkeydown", "if (event.keyCode==13 && document.activeElement==document.getElementById('" +txtSearch.ClientID+ "')) {document.getElementById('" + btnSearch.ClientID + "').click(); return false;}");

            this.Controls.Add(new LiteralControl("</td></tr><tr style=\"height:5px;\"><td colspan=\"6\" height=\"10px\">"));
            txthidn=new TextBox();
            txthidn.Height = 3;
            txthidn.Style[HtmlTextWriterStyle.Display] = "none";
            this.Controls.Add(txthidn);
            //btnMultiSearch.Attributes.Add("onclick", "var strReturn=window.showModalDialog('" + ViewState["PageUrl"] + "SearchTemp.aspx?Sysid=" + (int)ViewState["SysID"] + "&PermissionID=" + (int)ViewState["PermissionId"] + "',window,'DialogHeight:" + (int)ViewState["ShowPageHeight"] + "px;DialogWidth:600px;help:no;scroll:no');if(strReturn!=null){if(strReturn==\"false\") return false; else {document.getElementById('" + txthidn.ClientID + "').value=strReturn;return true;}}");
            //btnMultiSearch.Attributes.Add("onclick", "var strReturn=window.showModalDialog('" + HttpRuntime.AppDomainAppVirtualPath + "/SearchTemp.aspx?Sysid=" + (int)ViewState["SysID"] + "&PermissionID=" + (int)ViewState["PermissionId"] + "',window,'DialogHeight:" + (int)ViewState["ShowPageHeight"] + "px;DialogWidth:600px;help:no;scroll:no');if(strReturn!=null){if(strReturn==\"false\") return false; else {document.getElementById('" + txthidn.ClientID + "').value=strReturn;return true;}}");
            if (this.Parent.NamingContainer.ToString().Split('.', '_').Length >7)
                btnMultiSearch.Attributes.Add("onclick", "var strReturn=window.showModalDialog('" + HttpRuntime.AppDomainAppVirtualPath + "/SearchTemp.aspx?FormID=" + this.Parent.NamingContainer.ToString().Split('.', '_')[5] + "',window,'DialogHeight:" + (int)ViewState["ShowPageHeight"] + "px;DialogWidth:600px;DialogHeight:300px;help:no;scroll:no;resizable:yes');if(strReturn!=null){if(strReturn==\"false\") return false; else {document.getElementById('" + txthidn.ClientID + "').value=strReturn;return true;}}");
            else
                btnMultiSearch.Attributes.Add("onclick", "var strReturn=window.showModalDialog('" + HttpRuntime.AppDomainAppVirtualPath + "/SearchTemp.aspx?FormID=" + this.Parent.NamingContainer.ToString().ToLower() + "&Sysid=" + (int)ViewState["SysID"] + "&PermissionID=" + (int)ViewState["PermissionId"] + "',window,'DialogHeight:" + (int)ViewState["ShowPageHeight"] + "px;DialogWidth:600px;DialogHeight:300px;help:no;scroll:no');if(strReturn!=null){if(strReturn==\"false\") return false; else {document.getElementById('" + txthidn.ClientID + "').value=strReturn;return true;}}");
            this.Controls.Add(new LiteralControl("</td></tr></table>"));

            //<table style=\"background-color:White;\"><tr  style=\"height:10px;\"><td style=\"width:100%;\"></td></table>
            //this.Controls.Add(new LiteralControl("<script type=\"text/javascript\" language=\"javascript\" >"));
           
            //this.Controls.Add(new LiteralControl("function WebForm_OnSubmit()"));
            //this.Controls.Add(new LiteralControl(" {if (typeof(ValidatorOnSubmit) == \"function\" && ValidatorOnSubmit() == false)return false;return true;}"));
            //this.Controls.Add(new LiteralControl("</script>"));
            base.CreateChildControls();
            
        }

        void ddlSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        protected override HtmlTextWriterTag TagKey
        {
            get
            {
                return HtmlTextWriterTag.Div;
            }
        }
        protected override void DataBindChildren()
        {
            base.DataBindChildren();
        }
       
        //}
        //protected override void OnPreRender(EventArgs e)
        //{
        //    base.OnPreRender(e);
        //}

        //protected override void AddAttributesToRender(HtmlTextWriter writer)
        //{
        //    writer.AddAttribute("Onkeydown", "if(keyCode==13 && document.activeElement.ID==\"" + this.txtSearch.ClientID + "\") {btnSearch.Click();return false;}");
        //}

       
       // }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)ViewState["Sdt"];
            DataRow[] dr = dt.Select("FieldName='" + this.ddlSearch.SelectedValue + "'");


            if (txtSearch.Text != "")
            {
                if (dr.Length > 0)
                {
                    if (dr[0]["FieldType"].ToString().ToLower().Trim() == "datetime")
                    {
                        string strDate = ParseDateTime(txtSearch.Text.Trim().Replace("'", "''"), ViewState["DateFormat"].ToString()).ToString("yyyy/MM/dd");
                        if (dr[0]["FieldName"].ToString().ToLower().IndexOf("convert") >= 0)
                            this._strwhere = ddlSearch.SelectedValue + " like '%" + strDate + "%'";
                        else
                            this._strwhere = " convert(varchar(10)," + ddlSearch.SelectedValue + ",111) like '%" + strDate + "%' ";                        
                    }
                    else
                        this._strwhere = ddlSearch.SelectedValue + " like '%" + txtSearch.Text.Trim().Replace("'", "''") + "%'";
                }
            }
            else
                this._strwhere = "";
            
            OnSubmit(btnSearch, "btnSearch");
        }
        private void btnMultiSearch_Click(object sender, EventArgs e)
        {
            if (txthidn.Text != "")
               this._strwhere = txthidn.Text;
            else
                this._strwhere = "";
            txtSearch.Text = "";
            OnSubmit(btnMultiSearch, "btnMultiSearch");
        }

        //protected override void Render(HtmlTextWriter writer)
        //{
        //    StringBuilder builder = new StringBuilder("");
        //    builder.Append("<script type=\"text/javascript\" language=\"javascript\" >");
        //    builder.Append("function WebForm_OnSubmit() {if (typeof(ValidatorOnSubmit) == \"function\" && ValidatorOnSubmit() == false) return false;return true;}");
        //    builder.Append("</script>");
        //    writer.Write(builder.ToString());
          
        //}
        /// <summary>
        /// 字符串转化成日期，DateFormat指定日期格式。
        /// </summary>
        /// <param name="inputDate"></param>
        /// <param name="DateFormat"></param>
        /// <returns></returns>
        public static DateTime ParseDateTime(string inputDate, string DateFormat)
        {
            DateTime dtime = new DateTime(1912, 1, 1, 0, 0, 1);
            if (!string.IsNullOrEmpty(inputDate))
            {
                try
                {
                    int y = 1, m = 1, d = 1;
                    char cSpliter = '/';
                    if (DateFormat.Replace("gyyyy", "").Replace("yyyy", "").Replace("MM", "").Replace("dd", "").Trim().Length > 0)
                        cSpliter = char.Parse(DateFormat.Replace("gyyyy", "").Replace("yyyy", "").Replace("MM", "").Replace("dd", "").Trim().Substring(0, 1));

                    char[] Spliter = { cSpliter, cSpliter };
                    string[] a = inputDate.Split(Spliter);
                    string[] b = DateFormat.Split(Spliter);
                    for (int index = 0; index < b.Length; index++)
                    {
                        if (b[index] == "gyyyy")
                        {
                            y = int.Parse(a[index]) + 1911;
                        }
                        if (b[index] == "yyyy")
                        {
                            y = int.Parse(a[index]);
                        }
                        if (b[index] == "MM")
                        {
                            m = int.Parse(a[index]);
                        }
                        if (b[index] == "dd")
                        {
                            d = int.Parse(a[index]);
                        }
                    }
                    dtime = new DateTime(y, m, d);
                }
                catch
                {

                }
            }
            return dtime;

        }
        #region 屬性
        [Category("Misc")]
        public string StrWhere
        {
            get 
            {
                return this._strwhere;
            }
        }
        [Category("Misc")]
        public int SysID
        {
            set 
            {
                ViewState["SysID"] = value;
            }
        }
        [Category("Misc")]
        public int ShowPageHeight
        {
            set
            {
                ViewState["ShowPageHeight"] = value;
            }
        }
        [Category("Misc")]
        public int PermissionId
        {
            set
            {
                ViewState["PermissionId"] = value;
            }
        }
        public string PageUrl
        {
            set
            {
                ViewState["PageUrl"] = value;
            }
        }
        public DataTable DataSource
        {
            set { this._DataSource = value; }
            get { return this._DataSource; }
        }
        public string StrDateFormat
        {
            set
            {
                ViewState["DateFormat"] = value;
            }
        }
#endregion
        #region 事件

        public event ClickEventHandler Submit;
        //{
        //    add
        //    {
        //        Events.AddHandler(EventSubmitKey, value);
        //    }
        //    remove
        //    {
        //        Events.RemoveHandler(EventSubmitKey, value);
        //    }

        //}
        
        
        protected virtual void OnSubmit(object sender,string btnID)
        {
            ClickEventArgs ce = new ClickEventArgs(btnID);
            if (Submit != null) Submit(sender, ce);

            //EventHandler SubmitHandler = (EventHandler)Events[EventSubmitKey];
            //if (SubmitHandler != null)
            //{
            //    SubmitHandler(this, e);
            //}
        }

        #endregion
    }
    
}
