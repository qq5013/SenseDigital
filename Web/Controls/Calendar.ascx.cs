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

public partial class Controls_Calendar : System.Web.UI.UserControl
{
    string virtualPath;
    protected string JsDateFormat;
    private string _dateFormat;
    bool _ym = false;
    bool _readonly = false;
    string _changed = "";
    public bool ShowYm
    {
        get
        {
            return _ym;
        }
        set
        {
            _ym = value;
        }
    }
    /// <summary>
    /// 只讀
    /// </summary>
    public bool ReadOnly
    {
        get
        {
            return _readonly;
        }
        set
        {
            _readonly = value;
        }
    }

    public TextBox tDate
    {
        get
        {
            return txtDate;
        }
    }
    public string DateFormat
    {
        get
        {
            return _dateFormat;
        }
        set
        {
            _dateFormat = value;
        }
    }
    public string Text
    {
        get
        {
            return this.txtDate.Text;
        }
        set
        {
            this.txtDate.Text = value;
        }
    }
    /// <summary>
    /// 日期專用
    /// </summary>
    public DateTime DateValue
    {
        get
        {
            if (_ym) return DateTime.Parse("0001/01/01");
            return Js.Com.PageValidate.ParseDateTime(this.txtDate.Text, _dateFormat);
        }
        set
        {
            if (_ym)
            {
               this.txtDate.Text = "";
               return;
            }
            this.txtDate.Text=Js.Com.PageValidate.GetDateTimeString(value,_dateFormat);
            this.txtDate.Attributes.Add("datevalue", Js.Com.PageValidate.GetDateTimeString(value, "yyyy/MM/dd"));
        }
    }

    /// <summary>
    /// 日期專用
    /// </summary>
    public string YmValue
    {
        get
        {
            if (_ym)
            {
                if (_dateFormat.IndexOf('g') != -1)
                    return "" + (int.Parse(this.txtDate.Text.Substring(0, this.txtDate.Text.Length - 3)) + 1911) + "/" + this.txtDate.Text.Substring(this.txtDate.Text.Length - 2, 2);

                return this.txtDate.Text; 
            }

            return "";
        }
        set
        {
            if (_ym)
            {
                this.txtDate.Attributes.Add("datevalue", value);
                DateTime t = DateTime.Parse("" + value + "/01");
                if (_dateFormat.IndexOf('g') != -1)
                    this.txtDate.Text = "" + (t.Year - 1911) + _dateFormat.ToLower().Replace("g", "").Replace("m", "").Replace("y", "").Replace("d", "").Substring(1) + (t.Month > 9 ? "" + t.Month : "0" + t.Month);
                else
                    this.txtDate.Text = value;
            }
        }
    } 

    public string changed
    {        
        set
        {
            _changed = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //this.DateFormat = Js.Common.User.strDateFormat;  
        if (_dateFormat == null || _dateFormat.Length == 0) _dateFormat = "yyyy/MM/dd";
        JsDateFormat = _dateFormat.Replace("yyyy", "y");
        JsDateFormat = JsDateFormat.Replace("MM", "mm");
        JsDateFormat = JsDateFormat.Replace("yy", "y");
        if (_ym)
        {
            string sp = JsDateFormat.Replace("y", "").Replace("m", "").Replace("d", "").Replace("g", "").Substring(1);
            this.txtDate.ToolTip = "輸入年月格式為:[" + (_dateFormat.IndexOf('g') >= 0 ? "民國 YY" + sp + "MM" : "YYYY" + sp + "MM") + "] 例如:" + (_dateFormat.IndexOf('g') >= 0 ? DateTime.Now.Year - 1911 : DateTime.Now.Year) + sp + (DateTime.Now.Month > 9 ? "" + DateTime.Now.Month : "0" + DateTime.Now.Month);
        }
        else
            this.txtDate.ToolTip = "輸入日期格式為:[" + (_dateFormat.IndexOf('g') >= 0 ? "民國" + _dateFormat.Replace("g", "") : _dateFormat) + "] 例如:" + Js.Com.PageValidate.GetDateTimeString(DateTime.Now, _dateFormat);

        //virtualPath = Js.Com.ConfigHelper.GetConfigString("VirtualPath");
        //if (!Page.ClientScript.IsClientScriptIncludeRegistered("headjquery"))
        //    Page.ClientScript.RegisterClientScriptInclude("headjquery", @"/" + virtualPath + @"/Date_Js/jquery-1.5.1.js ");
        // Define the name and type of the client scripts on the page.
        
        if (!Page.ClientScript.IsClientScriptIncludeRegistered("headcalendar"))
            Page.ClientScript.RegisterClientScriptInclude("headcalendar", ResolveUrl("~/JScript/Date/lyz.calendar.min.js"));


        //if (!Page.ClientScript.IsClientScriptIncludeRegistered("headcalendar1"))
        //    Page.ClientScript.RegisterClientScriptInclude("headcalendar1", @"/" + virtualPath + @"/Date_Js/calendar.js");
        //this.tDate.Attributes.Add("onblur", "Validate('" + JsDateFormat + "',this);");
        //this.btnDate.Attributes.Add("onclick", " return showCalendar('" + txtDate.ClientID + "', '" + JsDateFormat + "');");

        //Response.Write("<link rel='stylesheet' href='/" + virtualPath + @"/Date_Js/date_style.css' type='text/css'/>");

        //String csname1 = "PopupScript";
        String csname2 = "MyUserLink_date_style";
        Type cstype = this.GetType();

        // Get a ClientScriptManager reference from the Page class.
        ClientScriptManager cs = Page.ClientScript;
        // Check to see if the client script is already registered.
        if (!cs.IsClientScriptBlockRegistered(cstype, csname2))
        {
            System.Text.StringBuilder cstext2 = new System.Text.StringBuilder();
            cstext2.Append("<link href=\"" + ResolveUrl("~/JScript/Date/lyz.calendar.css") + "\" rel=\"stylesheet\" type=\"text/css\" />");
            //cstext2.Append("Form1.Message.value='Text from client script.'} </");
            //cstext2.Append("script>");
            cs.RegisterClientScriptBlock(cstype, csname2, cstext2.ToString(), false);
        }

        //// Check to see if the startup script is already registered.
        //if (!cs.IsStartupScriptRegistered(cstype, csname1 + this.txtDate.ClientID))
        //{
        //    string str = "";
        //    _changed = _changed.Replace("this.value", "$(\"#" + this.txtDate.ClientID + "\").val()");
        //    if (_changed.Length > 0)
        //        str = ",callback:function() {" + _changed + "}";
        //    String cstext1 = "$(\"#" + this.txtDate.ClientID + "\").calendar({readonly: " + (_readonly ? "true" : "false") + ", DateFormat: '" + JsDateFormat + "' ,Ym: " + (_ym ? "true" : "false") + str + "});";
        //    cs.RegisterStartupScript(cstype, csname1 + this.txtDate.ClientID, cstext1, true);
        //}

        
    }

    protected override void Render(HtmlTextWriter writer)
    {
        base.Render(writer);
        string str = "";
        _changed = _changed.Replace("this.value", "$(\"#" + this.txtDate.ClientID + "\").val()");
        if (_changed.Length > 0)
            str = ",callback:function() {" + _changed + "}";
        String cstext1 = "$(\"#" + this.txtDate.ClientID + "\").calendar({readonly: " + (_readonly ? "true" : "false") + ", DateFormat: '" + JsDateFormat + "' ,Ym: " + (_ym ? "true" : "false") + str + "});" +
                         "if ($(\"#" + this.txtDate.ClientID + "\").attr(\"readonly\")) $(\"#" + this.txtDate.ClientID + "\").attr(\"class\",\"TextRead\"); else $(\"#" + this.txtDate.ClientID + "\").attr(\"class\",\"TextBox\");";

        //cs.RegisterStartupScript(cstype, csname1 + this.txtDate.ClientID, cstext1, true); class="TextRead"  class="TextBox"
        if (ScriptManager.GetCurrent(this.Page) != null)
        {
            ScriptManager.RegisterStartupScript(this, base.GetType(), "init" + this.ClientID, cstext1.ToString (), true);
        }
        else
        {
            this.Page.ClientScript.RegisterStartupScript(base.GetType(), "init" + this.ClientID, cstext1.ToString(), true);
        }       
    }
}
