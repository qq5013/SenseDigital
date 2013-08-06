using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Js.PageControl
{
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:Page04 runat=server></{0}:Page04>")]

    public class Page04 : WebControl
    {

               // Fields
        private int page_Count;
        private int page_Current;
        private string page_Index;
        private int page_Size;
        private int page_width;
        private int pageStep;

        public Page04()
        {
            this.page_Size = 10;
            this.page_Current = 1;
            this.page_Index = "index.aspx";
            this.pageStep = 6;
            this.page_width = 700;
        }
        protected override void Render(HtmlTextWriter output)
        {
            StringBuilder builder = new StringBuilder("");
            builder.Append("<table width=\"" + this.page_width + "\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" align=\"center\" height=\"22\">\n");
            builder.Append("\t<tr>\n");
            builder.Append("\t\t<td width=\"171\">");
            builder.Append(string.Concat(new object[] { "○ 頁次：<font color=\"#e78a29\">", this.Page_Current, "</font>/", this.Page_Count, "，每頁：<font color='#e78a29'>", this.Page_Size, "</font>筆</td>\n" }));
            builder.Append("\t\t<td width=\"629\">\n");
            builder.Append("\t\t<div align=\"right\">頁數：\n");
            int num = 1;
            if (this.Page_Current > this.PageStep)
            {
                num = this.Page_Current - this.PageStep;
            }
            else
            {
                num = 1;
            }
            int num2 = num + (2 * this.PageStep);
            if ((num + (2 * this.PageStep)) > this.Page_Count)
            {
                if (((2 * this.PageStep) + 1) > this.Page_Count)
                {
                    num = 1;
                }
                else
                {
                    num = this.Page_Count - (2 * this.PageStep);
                }
                num2 = this.Page_Count;
            }
            builder.Append("<ul id=\"hovershow\">");
            for (int i = num; i <= num2; i++)
            {
                if (this.Page_Current != i)
                {
                    builder.Append(string.Concat(new object[] { "<li>", "\t\t<a href='", this.Page_Index, "?page=", i, "'>" }));
                    builder.Append("<b>" + i + "</b></a></li>");
                }
                else
                {
                    builder.Append("\t\t<font color=#e78a29><b>" + i + "</b></font>");
                }
            }
            builder.Append("</ul>");
            builder.Append("\t\t</div>\n");
            builder.Append("\t\t</td>\n");
            builder.Append("\t</tr>\n");
            builder.Append("</table>");
            output.Write(builder.ToString());
        }
        // Properties
        public int Page_Count
        {
            get
            {
                return this.page_Count;
            }
            set
            {
                this.page_Count = value;
            }
        }

        public int Page_Current
        {
            get
            {
                return this.page_Current;
            }
            set
            {
                this.page_Current = value;
            }
        }

        public string Page_Index
        {
            get
            {
                return this.page_Index;
            }
            set
            {
                this.page_Index = value;
            }
        }

        public int Page_Size
        {
            get
            {
                return this.page_Size;
            }
            set
            {
                this.page_Size = value;
            }
        }

        [Bindable(true), DefaultValue(""), Category("Appearance")]
        public int Page_Width
        {
            get
            {
                return this.page_width;
            }
            set
            {
                this.page_width = value;
            }
        }

        public int PageStep
        {
            get
            {
                return this.pageStep;
            }
            set
            {
                this.pageStep = value;
            }
        }

    }
}