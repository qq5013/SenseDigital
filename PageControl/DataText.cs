using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Js.PageControl
{
    public class DataText : TextBox
    {
        protected string _format;
        [Bindable(true)]
        public string DataFormat
        {
            set
            {
                if (string.IsNullOrEmpty(value))
                    value = "NO";
                this._format = value;
                //this.Attributes.Add("onblur", "this.value=formatNumber(this.value,'" + this._format + "');");
            }
            get
            {
                return this._format;
            }
        }
        public decimal DataValue
        {
            get 
            {
                decimal d = 0;
                if (!string.IsNullOrEmpty(this.Text.Replace(",", "")))
                    d = decimal.Parse(this.Text.Replace(",", ""));
                return d;
            }
            set
            {
                this.Text = value.ToString(this._format);
            }
        }
        private string match=@"/^\d*\.?\d{0,6}$/";
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.Attributes.Add("onkeypress", @"return regInput(this," + match + ",String.fromCharCode(event.keyCode)) ;");
            this.Attributes.Add("ondrop", @"return regInput(this," + match + ",event.dataTransfer.getData('Text'));");
            this.Attributes.Add("onpaste", @"return regInput(this," + match + ",window.clipboardData.getData('Text'));");
            this.Attributes.Add("style", "text-align:right;padding-right:2px;");
            this.Attributes.Add("onfocus", "setFocus();");
            this.Attributes.Add("onblur", "formatCurrNumericControl(this);");
            // "this.value=this.value.replace(',','');var r = this.createTextRange();r.collapse(true); r.moveStart('character',this.value.length); r.select();");
        }
        //protected override void Render(HtmlTextWriter output)
        //{
        //    StringBuilder builder = new StringBuilder("");
        //    builder.Append("<input name='" + this.ID + "' type='text' maxlength='32767' id='" + this.ID + "' class='" + this.CssClass + "' ");
        //    builder.Append("onkeypress=\"return regInput(this," + match + ",String.fromCharCode(event.keyCode)) ;\" ");
        //    builder.Append("ondrop=\"return regInput(this," + match + ",event.dataTransfer.getData('Text'));\" ");
        //    builder.Append("onpaste=\"return regInput(this," + match + ",window.clipboardData.getData('Text'));\" ");
        //    builder.Append("onfocus=\"this.value=this.value.replace(',','');var r = this.createTextRange();r.collapse(true); r.moveStart('character',this.value.length); r.select();\" ");
        //    builder.Append("onblur=\"this.value=formatNumber(this.value,'" + this._format + "');\" ");
        //    builder.Append("style='width:" + this.Width + ";text-align:right;' />");

        //    output.Write(builder.ToString());
        //}

    }
}
