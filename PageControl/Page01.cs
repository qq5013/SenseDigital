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
    [ToolboxData("<{0}:Page01 runat=server></{0}:Page01>")]
    public class Page01 : WebControl
    {
        // Fields
        private string page_Add;
        private int page_Count;
        private int page_Current;
        private string page_Index;
        private string page_Makesql;
        private string page_Search;
        private int page_Size;
        private string page_width;
        private int record_Count;
        private string page_para;
        private string page_Prefix;

        public Page01()
        {
            this.page_Size = 10;
            this.page_Current = 1;
            this.page_Index = "index.aspx";
            this.page_Add = "add.aspx";
            this.page_Search = "search.aspx";
            this.page_Makesql = "makesql.aspx";
            this.page_width = "700";
        }

        protected override void Render(HtmlTextWriter output)
        {
            StringBuilder builder = new StringBuilder("");
            builder.Append("<table width=\"" + this.page_width + "\"  border=\"0\" cellspacing=\"0\" cellpadding=\"2\" height=\"22\">\n");
            builder.Append("\t<tr>\n");
            builder.Append("\t\t<td width=\"35%\" align=\"left\">");
            builder.Append(string.Concat(new object[] { "○ 共", this.Record_Count, "筆，共", this.Page_Count, "頁，第<font color=\"#e78a29\">", this.Page_Current, "</font>頁</td>\n" }));
            builder.Append("\t\t<td width=\"65%\">");
            builder.Append("\t\t<div align=\"right\">\n");
            if (this.Page_para != "")
            {
                builder.Append("\t\t\t[ <a href=../../frmRpt/WebRpt.aspx" + this.page_para + ">列印</a> ]&nbsp;\n");
            }
            if (this.page_Makesql != "")
            {
                builder.Append("\t\t\t[ <a href=" + this.page_Makesql + ">全部</a> ]&nbsp;\n");
            }
            if (this.page_Add != "")
            {
                builder.Append("\t\t\t[ <a href=" + this.page_Add + ">新增</a> ]&nbsp;\n");
            }
            //if (this.Page_Search != "")
            //{
            //    builder.Append("\t\t\t[ <a href=" + this.Page_Search + ">查詢</a> ]&nbsp;\n");
            //}
            builder.Append("\t\t\t[ <a href=" + this.page_Index + ">重新整理</a> ]&nbsp;&nbsp;[");
            if (this.Page_Current > 1)
            {
                builder.Append("\t\t\t<a href=" + this.page_Index + "?page=1>首頁</a>\n");
            }
            else
            {
                builder.Append("\t\t\t<font color=#cccccc>首頁</font> \n");
            }
            builder.Append("]&nbsp;[");
            if ((this.Page_Current - 1) > 0)
            {
                builder.Append(string.Concat(new object[] { "\t\t\t<a href=", this.page_Index, "?page=", this.Page_Current - 1, ">上一頁</a>\n" }));
            }
            else
            {
                builder.Append("\t\t\t<font color=#cccccc>上一頁</font> \n");
            }
            builder.Append("]&nbsp;[");
            if ((this.Page_Current + 1) <= this.Page_Count)
            {
                builder.Append(string.Concat(new object[] { "\t\t\t<a href=", this.page_Index, "?page=", this.Page_Current + 1, ">下一頁</a> \n" }));
            }
            else
            {
                builder.Append("\t\t\t<font color=#cccccc>下一頁</font> \n");
            }
            builder.Append("]&nbsp;[");
            if (this.Page_Current < this.Page_Count)
            {
                builder.Append(string.Concat(new object[] { "\t\t\t<a href=", this.page_Index, "?page=", this.Page_Count, ">尾頁</a>\n" }));
            }
            else
            {
                builder.Append("\t\t\t<font color=#cccccc>尾頁</font>\n");
            }
            builder.Append("]");
            builder.Append("\t\t</div>\n");
            builder.Append("\t\t</td>\n");
            builder.Append("\t</tr>");
            builder.Append("</table>\n");
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

        [DefaultValue(""), Bindable(true), Category("Appearance")]
        public string Page_Width
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

        public int Record_Count
        {
            get
            {
                return this.record_Count;
            }
            set
            {
                this.record_Count = value;
            }
        }
        public string Page_para
        {
            get
            {
                return this.page_para;
            }
            set
            {
                this.page_para = value;
            }
        }


        public string Page_Prefix
        {
            get
            {
                return this.page_Prefix;
            }
            set
            {
                this.page_Prefix = value;
                this.page_Add = value + "_Add.aspx";
                this.page_Makesql = value + "_All.aspx";


                if (value.Substring(value.Length - 1, 1) == "y" && "aoieu".IndexOf(value.Substring(value.Length - 2, 1))<0)
                    this.page_Index = value.Substring(0, value.Length - 1) + "ies.aspx";
                else if (value.Substring(value.Length - 1, 1) == "s")
                    this.page_Index = value + "es.aspx";
                else
                    this.page_Index = value + "s.aspx";
            }
        }
        public void SetPageAddEmpty()
        {
            this.page_Add = "";
        }
    }
}
