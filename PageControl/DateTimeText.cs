using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Kunchen.PageControl
{
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:DateTimeText runat=server></{0}:DateTimeText>")]
    public class DateTimeText : CompositeControl
    {
        private string _dateFormat;
        private TextBox txtDate;
        private Button btnSearch;
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
        
        public DateTime DateValue
        {
            get
            {
                return ParseDateTime(this.txtDate.Text, _dateFormat);
            }
            set
            {
                this.txtDate.Text = GetDateTimeString(value, _dateFormat);
            }
        }
        protected override void CreateChildControls()
        {
            this.Controls.Clear();
            EnsureChildControls();

            string JsDateFormat = _dateFormat.Replace("yyyy", "y");
            JsDateFormat = JsDateFormat.Replace("MM", "mm");
            JsDateFormat = JsDateFormat.Replace("yy", "y");
            
            this.Controls.Add(new LiteralControl("<table width=\"" + this.Width + "\" height=\"" + this.Height +"\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">"));
            this.Controls.Add(new LiteralControl("<tr width=\"100%\" height=\"" + this.Height + "\"><td width='*'>"));

            this.txtDate = new TextBox();
            
            this.txtDate.TextChanged+=new EventHandler(txtDate_TextChanged);

           // this.txtDate.ToolTip = "輸入日期格式為:[" + (_dateFormat.IndexOf('g') >= 0 ? "民國" + _dateFormat.Replace("g", "") : _dateFormat) + "] 例如:" + GetDateTimeString(DateTime.Now, _dateFormat);
            this.txtDate.Attributes.Add("onblur", "Validate('" + _dateFormat + "',this);");
            this.Controls.Add(this.txtDate);
            this.Controls.Add(new LiteralControl("</td><td width=\"20px\">"));
            btnSearch = new Button();
            btnSearch.Text = "...";
            btnSearch.Attributes.Add("OnClientClick", "return showCalendar('" + txtDate.ClientID + "', '" + JsDateFormat + "');");
           

            btnSearch.CssClass = "DateButton";
            this.Controls.Add(btnSearch);
            this.Controls.Add(new LiteralControl("</td></tr></table>"));
            base.CreateChildControls();
        }
        public class SelectedDataValueChanged : EventArgs
        {
            public SelectedDataValueChanged(DateTime btnID)
            {
                this.dtTime = btnID;
            }
            public DateTime dtTime;
        }
        public delegate void DateValueChnagedHandler(object sender, SelectedDataValueChanged ce);

        public event DateValueChnagedHandler DateValueChanged;
        
        protected virtual void OnDateValueChanged(object sender, SelectedDataValueChanged e)
        {
            if (DateValueChanged != null) DateValueChanged(sender, e);
        }


        protected void txtDate_TextChanged(object sender, EventArgs e)
        {
            if (this.DateValue > new DateTime(1912, 1, 1, 0, 0, 1))
                OnDateValueChanged(this, new SelectedDataValueChanged(this.DateValue));
        }

        private string GetDateTimeString(DateTime dttime, string strFormat)
        {
            if (string.IsNullOrEmpty(strFormat))
                strFormat = "yyyy/MM/dd";
            string strDateValue = "";
            if (dttime <= new DateTime(1912, 1, 1, 0, 0, 1))
                return strDateValue;
            string spliter = "/";

            string strNewFormat = strFormat.Replace("gyyyy", "").Replace("yyyy", "").Replace("MM", "").Replace("dd", "").Trim();
            if (strNewFormat.Length > 0)
                spliter = strNewFormat.Substring(0, 1);
            string[] s = new string[3];
            int y = dttime.Year;
            if (strFormat.IndexOf("g") >= 0)
            {
                y = y - 1911;
            }
            s[0] = y.ToString();
            s[1] = dttime.Month.ToString().PadLeft(2, '0');
            s[2] = dttime.Day.ToString().PadLeft(2, '0');

            if (strFormat.Replace("g", "") == "yyyy" + spliter + "MM" + spliter + "dd")
            {
                strDateValue = s[0] + spliter + s[1] + spliter + s[2];
            }
            if (strFormat.Replace("g", "") == "MM" + spliter + "dd" + spliter + "yyyy")
            {
                strDateValue = s[1] + spliter + s[2] + spliter + s[0];
            }
            if (strFormat.Replace("g", "") == "dd" + spliter + "MM" + spliter + "yyyy")
            {
                strDateValue = s[2] + spliter + s[1] + spliter + s[0];
            }
            //strDateValue = newDate.ToString(strFormat.Replace("g",""), new System.Globalization.CultureInfo("zh-TW", true));

            return strDateValue;
        }
        private DateTime ParseDateTime(string inputDate, string DateFormat)
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
    }
}
