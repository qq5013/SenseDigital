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
    [ToolboxData("<{0}:Page02 runat=server></{0}:Page02>")]
    public class Page02 : WebControl
    {
 // Fields
        private int page_Count;
        private int page_Current;
        private string page_Index;
        private int page_Size;
        private int page_width;
        private int PageStep;
        private int record_Count;

        public Page02()
        {
            this.page_Size = 10;
            this.page_Current = 1;
            this.page_Index = "index.aspx";
            this.PageStep = 10;
            this.page_width = 700;
        }
        protected override void Render(HtmlTextWriter output)
        {
            int intWidth = 20;
            StringBuilder builder = new StringBuilder("");
            builder.Append("<script type=\"text/javascript\" language=\"javascript\" src=\"../../Date_Js/DataProcess.js\"></script>");
            builder.Append("<table width=\"" + this.Width + "\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" align=\"center\" >\n");
            builder.Append("\t<tr style=\"Height:4px;\"><td colspan=\"4\"></td></tr><tr  class=\"table_bgcolor\">\n");
            if (this.Record_Count.ToString().Length <= 2)
                intWidth = 30;
            else
                intWidth=8*(this.Record_Count.ToString().Length+1);
            builder.Append("\t\t<td align=\"center\" width=\"" + intWidth + "px\"  class=\"bottomtable\">");
            builder.Append(string.Concat(new object[] { "<b>", this.Record_Count, "</b>" }));
            builder.Append("</td>\n");
            if (this.Page_Current.ToString().Length + this.page_Count.ToString().Length <= 2)
                intWidth = 40;
            else
                intWidth = 8* (this.Page_Current.ToString().Length + this.page_Count.ToString().Length + 2);
            builder.Append("<td align=\"center\" width=\"" + intWidth + "px\"  class=\"bottomtable\" >");
            builder.Append(string.Concat(new object[] { "<b>", this.Page_Current, "/", this.Page_Count, "</b></td>\n" }));
            //builder.Append("\t\t<td width=\"171\">");
            //builder.Append(" <input id=\"txtToPage\" style=\"width: 37px\" class=\"TextBox\" type=\"text\" onkeydown=\"if(event.keyCode==13){jumppage('txtToPage','" + this.Page_Index + "?page='," + this.page_Count + ");event.keyCode=9;return false;}\" />");

            //builder.Append("\t\t</td>\n");
            //builder.Append("\t\t<td width=\"171\">");
            //builder.Append("<input id=\"btnToPage\" type=\"button\" class=\"mdbtn\" value=\"button\" onclick=\"jumppage('txtToPage','" + this.Page_Index + "?page=',"+this.page_Count+")\" />");
            //builder.Append("\t\t</td>\n");
           
            builder.Append("\t\t<td width=\"*\" align=\"right\">\n");
            
            
            int num = 1;
            int pagePre;
            int intCell;
            if (this.Page_Current-2 >=1)
            {
                num = this.Page_Current - 2;
            }
            else
            {
                num = 1;
            }
            int num2 = num+this.PageStep-1;
            if (num2 > this.Page_Count)
            {
                if (this.PageStep > this.Page_Count)
                {
                    num = 1;
                }
                else
                {
                    num = this.Page_Count -  this.PageStep+1;
                }
                num2 = this.Page_Count;

            }
            intCell = num2 - num + 1;
            if (num > 1)
                intCell++;
            if (Page_Current > num)
                intCell++;
            if (Page_Current < num2)
                intCell++;
            if (Page_Count > num2)
                intCell++;

            //builder.Append("\t\t<div align=\"right\" >\n");
            if (num > 1)
            {
                builder.Append(string.Concat(new object[] { "\t\t<a href='", this.Page_Index, "?page=", 1, "' class=\"link1\">" }));
                builder.Append( "|<" + "</a>"); 
            }
            if (Page_Current > num)
            {
                pagePre = Page_Current - 1;
                builder.Append(string.Concat(new object[] { "\t\t<a href='", this.Page_Index, "?page=", pagePre, "'  class=\"link1\">" }));
                builder.Append("<<" + "</a>");
            }
            
            for (int i = num; i <= num2; i++)
            {
                if (this.Page_Current != i)
                {
                    builder.Append(string.Concat(new object[] { "\t\t<a href='", this.Page_Index, "?page=", i, "'  class=\"link1\">" }));
                    builder.Append( i + "</a>");
                }
                else
                {
                    builder.Append(string.Concat(new object[] { "\t\t<a href='", this.Page_Index, "?page=", i, "'  class=\"hover1\">" }));
                    builder.Append(i + "</a>");
                }
            }
            if (Page_Current < num2)
            {

                pagePre = Page_Current + 1;
                builder.Append(string.Concat(new object[] { "\t\t<a href='", this.Page_Index, "?page=", pagePre, "' class=\"link1\" >" }));
                builder.Append( ">>" + "</a>");
            }
            if (Page_Count > num2)
            {
                builder.Append(string.Concat(new object[] { "\t\t<a href='", this.Page_Index, "?page=", Page_Count, "'   class=\"link1\">" }));
                builder.Append( ">|" + "</a>");
            }
            //builder.Append("\t\t</div>\n");
            builder.Append("\t\t</td>\n");
            builder.Append("\t\t<td align=\"right\" width=\"37px\" >");
            builder.Append(" <input id=\"txtToPage\" style=\"width: 34px\" class=\"pagetext\" type=\"text\" onkeydown=\"if(event.keyCode==13){jumppage('txtToPage','" + this.Page_Index + "?page='," + this.page_Count + ");event.keyCode=9;return false;}\"");
            builder.Append(" onkeypress=\"return regInput(this,/^\\d+$/,String.fromCharCode(event.keyCode)) ;\" ondrop=\"return regInput(this,/^\\d+$/,event.dataTransfer.getData('Text'));\" onpaste=\"return regInput(this,/^\\d+$/,window.clipboardData.getData('Text'));\" />");
            //
            builder.Append("\t\t</td>\n");
            builder.Append("\t</tr><tr style=\"Height:4px;\"><td colspan=\"4\"></td></tr>\n");
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
        public int Record_Count
        {
            get
            {
                return this.record_Count;
            }
            set
            {
                 this.record_Count=value;
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
    }
}

