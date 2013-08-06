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
    [ToolboxData("<{0}:Navigation01 runat=server></{0}:Navigation01>")]
    public class Navigation01 : WebControl
    {        
        // Fields
        private string page_Prefix;
        private string key_Str;
        private string page_Add;
        private string page_Delete;
        private string page_Index;
        private Mode page_Mode;
        private string page_Modify;
        private string page_Search;
        private string page_Show;
        private Unit page_width;
        private string para_Str;
        private string table_Name;


        public Navigation01()
        {
            this.page_Index = "index.aspx";
            this.page_Add = "add.aspx";
            this.page_Delete = "delete.aspx";
            this.page_Modify = "modify.aspx";
            this.page_Search = "search.aspx";
            this.page_Show = "show.aspx";
            this.page_width = 600;
            this.page_Mode = Mode.Show;
        }
        protected override void Render(HtmlTextWriter output)
        {
            StringBuilder builder = new StringBuilder("");
            builder.Append("<table width=\"" + this.page_width + "\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" align=\"center\">");
            builder.Append("<tr>");
            builder.Append("<td height=\"22\" align=\"left\" width=\"20%\">");
            builder.Append("<font color=\"#CCCCCC\">≡ ≡</font>");
            builder.Append("</td>");
            builder.Append("<td height=\"22\" width=\"*\">");
            builder.Append("<div align=\"right\">");

            switch (page_Mode)
            {
                case Mode.Show:
                    builder.Append(" [ <a href=\"..\\..\\frmRpt\\WebRpt.aspx?" + this.Para_Str + "\">列印</a> ]");
                    if (this.page_Add != "")
                        builder.Append(" [ <a href=\"" + this.page_Add + "\">新增</a>");
                    else
                        builder.Append(" [ <font color=\"#CCCCCC\">新增</font>");
                    builder.Append(" ]");

                    if (this.page_Delete != "")
                        builder.Append(" [ <a name=\"dotoolbar_delete\" href=\"" + this.page_Delete + "?" + this.Para_Str + "\">刪除</a>");
                    else
                        builder.Append(" [ <font color=\"#CCCCCC\">刪除</font>");
                    builder.Append(" ]");
                    if (this.page_Modify != "")
                        builder.Append(" [ <a name=\"dotoolbar_modify\" href=\"" + this.page_Modify + "?" + this.Para_Str + "\">修改</a>");
                    else
                        builder.Append(" [ <font color=\"#CCCCCC\">修改</font>");
                    builder.Append(" ]");
                    builder.Append(" [ <font color=\"#CCCCCC\">放棄</font> ]");
                    break;
                case Mode.Modify:
                    builder.Append(" [ <font color=\"#CCCCCC\">新增</font> ]");
                    builder.Append(" [ <font color=\"#CCCCCC\">刪除</font> ]");
                    builder.Append(" [ <font color=\"#CCCCCC\">修改</font> ]");
                    if (this.page_Show != "")
                        builder.Append(" [ <a href=\"" + this.page_Show + "?" + this.Para_Str + "\">放棄</a>");
                    else
                        builder.Append(" [ <font color=\"#CCCCCC\">放棄</font>");
                    builder.Append(" ]");
                    break;
                case Mode.Add:
                    builder.Append(" [ <font color=\"#CCCCCC\">新增</font> ]");
                    builder.Append(" [ <font color=\"#CCCCCC\">刪除</font> ]");
                    builder.Append(" [ <font color=\"#CCCCCC\">修改</font> ]");
                    //builder.Append(" [ <font color=\"#CCCCCC\">放棄</font> ]");
                    if (this.page_Show != "")
                    {
                        if (this.Para_Str != null)
                            builder.Append(" [ <a href=\"" + this.page_Show + "?" + this.Para_Str + "\">放棄</a>");
                        else
                            //builder.Append(" [ <a href=\"" + this.page_Index + "\">放棄</a>");
                            builder.Append(" [ <a href=\"javascript:history.go(-1);\">放棄</a>");
                    }                   
                    
                    else
                        builder.Append(" [ <font color=\"#CCCCCC\">放棄</font>");
                    builder.Append(" ]");
                    break;
            }
            builder.Append(" [ <a href=\"" + this.page_Index + "\">返回</a> ] </div>");
            builder.Append("</td></tr></table>");
            output.Write(builder.ToString());
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
                this.page_Delete = value + "_Delete.aspx";
                this.page_Add = value + "_Add.aspx";
                this.page_Show = value + "_Show.aspx";
                this.page_Modify = value + "_Modify.aspx";
                //if (value.Substring(value.Length - 1, 1) == "y")
                //    this.page_Index = value.Substring(0, value.Length - 1) + "ies.aspx";
                //else if (value.Substring(value.Length - 1, 1) == "s")
                //    this.page_Index = value + "es.aspx";
                //else
                //    this.page_Index = value + "s.aspx";
                if (value.Substring(value.Length - 1, 1) == "y" && "aoieu".IndexOf(value.Substring(value.Length - 2, 1)) < 0)
                    this.page_Index = value.Substring(0, value.Length - 1) + "ies.aspx";
                else if (value.Substring(value.Length - 1, 1) == "s")
                    this.page_Index = value + "es.aspx";
                else
                    this.page_Index = value + "s.aspx";
            }
        }
        public string Key_Str
        {
            get
            {
                return this.key_Str;
            }
            set
            {
                this.key_Str = value;
            }
        }
        public void SetPageAddEmpty()
        {
            this.page_Add = "";
        }
        public void SetPageModifyEmpty()
        {
            this.page_Modify = "";
        }
        public void SetPageDeleteEmpty()
        {
            this.page_Delete = "";
        }
        //public string Page_Add
        //{
        //    get
        //    {
        //        return this.page_Add;
        //    }
        //    set
        //    {
        //        this.page_Add = value;
        //    }
        //}
        //public string Page_Delete
        //{
        //    get
        //    {
        //        return this.page_Delete;
        //    }
        //    set
        //    {
        //        this.page_Delete = value;
        //    }
        //}
      
        public Mode Page_Mode
        {
            get
            {
                return this.page_Mode;
            }
            set
            {
                this.page_Mode = value;
            }
        }
        //public string Page_Modify
        //{
        //    get
        //    {
        //        return this.page_Modify;
        //    }
        //    set
        //    {
        //        this.page_Modify = value;
        //    }
        //}
       
        [DefaultValue(""), Bindable(true), Category("Appearance")]
        public Unit Page_Width
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
        public string Para_Str
        {
            get
            {
                return this.para_Str;
            }
            set
            {
                this.para_Str = value;
            }
        }
       
        // Nested Types
        public enum Mode
        {
            Add,
            Modify,
            Show
        }

    }
}
