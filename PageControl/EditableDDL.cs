//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Text;
//using System.Web;
//using System.Web.UI;
//using System.Web.UI.WebControls;

//namespace Js.PageControl
//{
//    [DefaultProperty("Text")]
//    [ToolboxData("<{0}:EditableDDL runat=server></{0}:EditableDDL>")]
//    public class EditableDDL : ListControl
//    {
//        private System.Web.UI.WebControls.TextBox txtShow = new TextBox();
//        private System.Web.UI.WebControls.DropDownList droplSelect = new DropDownList();

//        [Bindable(true)]
//        [Category("Appearance")]
//        [DefaultValue("")]
//        [Localizable(true)]
//        public override string Text
//        {
//            get
//            {
//                String s = (String)ViewState["Text"];
//                return ((s == null) ? String.Empty : s);
//            }

//            set
//            {
//                ViewState["Text"] = value;
//            }
//        }

//        [Bindable(true)]
//        [Category("Appearance")]
//        [DefaultValue("")]
//        [Localizable(true)]
//        [Description("控件寬度")]
//        public override Unit Width
//        {
//            get
//            {
//                return droplSelect.Width;
//            }

//            set
//            {
//                droplSelect.Width = Unit.Parse(value.ToString());
//                txtShow.Width = Unit.Parse((value.Value - 25).ToString() + "px");
//            }
//        }

//        //[Bindable(true)]
//        //[Category("Appearance")]
//        //[DefaultValue("")]
//        //[Localizable(true)]
//        //[Description("控件高度")]
//        //public override Unit Height
//        //{
//        //    get
//        //    {
//        //        return droplSelect.Height;
//        //    }

//        //    set
//        //    {
//        //        droplSelect.Height = value;
//        //        txtShow.Height = value;
//        //    }
//        //}

//        protected override void CreateChildControls()
//        {
//            Controls.Clear();


//            //_textBoxCode.ID = TextBoxCodeID;
//            //_textBoxCode.BackColor = ColorTranslator.FromHtml("#F0F0F0");
//            //_textBoxCode.Attributes.Add("disabled", "disabled");
//            //_textBoxCode.Width = Unit.Parse("48px");
//            //_textBoxCode.Height = Unit.Parse("15px");
//            //_textBoxCode.BorderWidth = Unit.Parse("0px");
//            //_textBoxName.ID = TextBoxNameID;
//            //_textBoxName.BackColor = ColorTranslator.FromHtml("#F0F0F0");
//            //_textBoxName.Attributes.Add("disabled", "disabled");
//            //_textBoxName.Width = Unit.Parse("120px");
//            //_textBoxName.BorderWidth = Unit.Parse("0px");
//            Controls.Add(txtShow);
//            Controls.Add(droplSelect);
//        }

//        private void droplSelect_SelectedIndexChanged(object sender, EventArgs e)
//        {
//            txtShow.Text = droplSelect.SelectedItem.Text.Trim();
//        }

//        protected override void OnInit(EventArgs e)
//        {
//            base.OnInit(e);
//            //txtShow = new TextBox();
//            txtShow.ID = "txtShow";
//            txtShow.Attributes.Add("style", "position:absolute;");


//            //droplSelect = new DropDownList();
//            droplSelect.SelectedIndexChanged += new EventHandler(droplSelect_SelectedIndexChanged);
//            droplSelect.ID = "droplSelect";
//            droplSelect.Attributes.Add("style", "font-size:9pt;");
//        }

//        //protected override void RenderContents(HtmlTextWriter output)
//        //{
//        //    output.Write(Text);
//        //}

//        ///
//        /// 将此控件呈现给指定的输出参数。
//        ///
//        /// 要写出到的 HTML 编写器
//        protected override void Render(HtmlTextWriter output)
//        {
//            EnsureChildControls();
//            //output.AddAttribute(HtmlTextWriterAttribute.Cellspacing, "0");
//            //output.AddAttribute(HtmlTextWriterAttribute.Cellpadding, "0");
//            //output.AddAttribute(HtmlTextWriterAttribute.Border, "0");
//            //output.AddAttribute(HtmlTextWriterAttribute.Style, "border:solid 1px #AECAF0;border-collapse:collapse; background:#F0F0F0;");

//            //output.RenderBeginTag(HtmlTextWriterTag.Table);

//            //output.RenderBeginTag(HtmlTextWriterTag.Tr);

//            //output.AddStyleAttribute(HtmlTextWriterStyle.BorderWidth, "0px");
//            //output.RenderBeginTag(HtmlTextWriterTag.Td);
//            output.RenderBeginTag(HtmlTextWriterTag.Span);

//            if (droplSelect.Items.Count > 0)
//                txtShow.Text = droplSelect.Items[0].Text;
//            else
//                txtShow.Text = "";
//            txtShow.RenderControl(output);
//            droplSelect.RenderControl(output);
//            output.RenderEndTag();
//        }
//    }
//}


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
    [ToolboxData("<{0}:EditableDDL runat=server></{0}:EditableDDL>")]
    public class EditableDDL : CompositeControl,IPostBackDataHandler
    {
        TextBox _input;
        DropDownList _select;
        private object dataSource;

        public EditableDDL()
        {
            _select = new DropDownList();
            _input = new TextBox();
        }

        #region 控件属性
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]
        public string Text
        {
            get
            {
                String s = (String)ViewState["Text"];
                return ((s == null) ? String.Empty : s);
            }

            set
            {
                ViewState["Text"] = value;
            }
        }

        [DefaultValue((string)null)]
        public object DataSource
        {
            get
            {
                return this.dataSource;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentException("unvalid data source.", this.ID);
                }
                this.dataSource = value;
            }
        }

        [DefaultValue("")]
        public virtual string DataTextField
        {
            get
            {
                object obj1 = this.ViewState["DataTextField"];
                if (obj1 != null)
                {
                    return (string)obj1;
                }
                return string.Empty;
            }
            set
            {
                this.ViewState["DataTextField"] = value;
            }
        }

        [DefaultValue("")]
        public virtual string DataValueField
        {
            get
            {
                object obj1 = this.ViewState["DataValueField"];
                if (obj1 != null)
                {
                    return (string)obj1;
                }
                return string.Empty;
            }
            set
            {
                this.ViewState["DataValueField"] = value;
            }
        }
        #endregion

        #region  控件行为
        /// <summary>
        /// 清除所有选取项
        /// </summary>
        public void ClearSelection()
        {
            for (int i = 0; i < _select.Items.Count; i++)
            {
                _select.Items[i].Selected = false;
            }
        }

        /// <summary>
        /// 清除所有的item
        /// </summary>
        public void Clear()
        {
            _select.Items.Clear();
            this.Text = string.Empty;
        }

        protected override void OnDataBinding(EventArgs e)
        {
            base.OnDataBinding(e);
            _select.DataSource = DataSource;
            _select.DataTextField = DataTextField;
            _select.DataValueField = DataValueField;
            _select.DataBind();
        }
        #endregion

        protected override void CreateChildControls()
        {
            Controls.Clear();
            
            this.Controls.Add(_select);
            this.Controls.Add(_input);

            ChildControlsCreated = true;
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            this.Page.RegisterRequiresPostBack(this);
        }

        protected override void RenderContents(HtmlTextWriter output)
        {
            EnsureChildControls();
            int iWidth = Convert.ToInt32(base.Width.Value);
            if (iWidth == 0)
            {
                iWidth = 102;
                _input.Width = Unit.Parse("102px");
            }

            int sWidth = iWidth + 16;
            int spanWidth = sWidth - 18;

            if (Page != null)
            {
                Page.VerifyRenderingInServerForm(this);
            }
            output.Write(@"<div style='POSITION:relative'>");
            output.Write(@"<span style='MARGIN-LEFT:" + spanWidth.ToString() + "px;OVERFLOW:hidden;WIDTH:18px'>");

            _select.Width = Unit.Parse(sWidth.ToString() + "px");
            _select.Style.Add("MARGIN-LEFT", "-" + spanWidth.ToString() + "px");
            _select.ID = base.ID + "_Select";
            _select.Attributes.Add("onchange", "this.parentNode.nextSibling.value=this.value");
            _select.Attributes.Add("onfocus", "" + this.getFocusScript() + "");
            _select.RenderControl(output);
            output.Write("</span>");

            _input.Style.Clear();
            _input.Width = Unit.Parse(iWidth.ToString() + "px");
            _input.Style.Add("left", "0px");
            _input.Style.Add("POSITION", "absolute");
            _input.ID = this.UniqueID;
            _input.Text = Text;
            _input.RenderControl(output);

            output.Write("</div>");
        }

        private string getFocusScript()
        {
            string strScript = " ";
            strScript += "var isExist = -2; ";
            strScript += "var obj = event.srcElement; ";
            strScript += "var str = this.parentNode.nextSibling.value; ";
            strScript += "var ary = obj.options; ";
            strScript += "for(var i=0;i<ary.length;i++){ ";
            strScript += " if(str == ary[i].text){ ";
            strScript += "  isExist = i; ";
            strScript += "  break; ";
            strScript += " } ";
            strScript += "} ";
            strScript += "if(isExist != -2){ ";
            strScript += " obj.selectedIndex = isExist; ";
            strScript += "} ";
            strScript += "else{ ";
            strScript += " obj.selectedIndex = -1; ";
            strScript += "} ";
            return strScript;
        }

        #region IPostBackDataHandler 成员

        public bool LoadPostData(string postDataKey, System.Collections.Specialized.NameValueCollection postCollection)
        {
            string newtext = postCollection[this.UniqueID + "$" + this.UniqueID];
            this.Text = newtext;
            return false;
        }

        public void RaisePostDataChangedEvent()
        {
        }

        #endregion
    }
}
