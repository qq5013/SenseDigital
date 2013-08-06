using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.Security;
using System.Security.Cryptography;

namespace Js.Com
{
    public class PageBase : Page
    {
        // Fields
        private DataSet _UISet;
        private DataTable _UITable;
        protected Hashtable baseEditFillHashtable;
        protected Hashtable baseEditHashtable;
        protected Hashtable baseHashtable;
        protected Hashtable baseListHashtable;
        protected string errMsg;
        protected bool errState;
        protected int pageCount;
        protected int recordCount;
        protected Hashtable UIhashtable;

        // Methods
        public PageBase()
        {

        }
        protected void MsgBoxShow(Page page, string Msg)
        {
            page.Response.Write("<script defer>window.alert(\"" + Msg.Replace("\"", "'") + "\");</script>");
        }


        private void PageBase_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                if (this.Session["baseHashtable"] != null)
                {
                    this.baseHashtable = (Hashtable)this.Session["baseHashtable"];
                }
                if (this.Session["baseEditHashtable"] != null)
                {
                    this.baseEditHashtable = (Hashtable)this.Session["baseEditHashtable"];
                    this.Session.Remove("baseEditHashtable");
                }
                else
                {
                    this.baseEditHashtable.Add("EditSafeState", false);
                }
            }
            if (this.Session["baseListHashtable"] != null)
            {
                this.baseListHashtable = (Hashtable)this.Session["baseListHashtable"];
                this.Session.Remove("baseListHashtable");
            }
            else
            {
                this.baseListHashtable.Add("IsRefresh", false);
            }
        }

        public string GetSysName(int SysID)
        {
            switch (SysID.ToString())
            {
                case "0":
                    return "�@��";
                case "1":
                    return "�q��";
                case "2":
                    return "�w�s";
                case "3":
                    return "�ͺ�";
                case "4":
                    return "�i�f";
                case "5":
                    return "�X�f";
                case "6":
                    return "����";
                case "7":
                    return "�T��";
                case "8":
                    return "�]��";
                case "9":
                    return "�b��";
                case "10":
                    return "�o��";
                case "11":
                    return "�ӳ�";
                case "12":
                    return "�H�~";
                case "13":
                    return "�ȪA";
                case "14":
                    return "�v��";
                default:
                    return "";
            }
        }
        public int GetSysID(string SysName)
        {
            switch (SysName)
            {
                case "�@��":
                    return 0;
                case "�q��":
                    return 1;
                case "�w�s":
                    return 2;
                case "�ͺ�":
                    return 3;
                case "�i�f":
                    return 4;
                case "�X�f":
                    return 5;
                case "����":
                    return 6;
                case "�T��":
                    return 7;
                case "�]��":
                    return 8;
                case "�b��":
                    return 9;
                case "�o��":
                    return 10;
                case "�ӳ�":
                    return 11;
                case "�H�~":
                    return 12;
                case "�ȪA":
                    return 13;
                case "�v��":
                    return 14;
                default:
                    return -1;
            }
        }
        protected void PageReset(Page page)
        {
            StringBuilder builder1 = new StringBuilder();
            builder1.Append("<script language = javascript defer>");
            builder1.Append(" this.location.reset(); ");
            builder1.Append("</script>");
            page.Response.Write(builder1.ToString());
        }


        protected void ParentPageRefresh(Page page)
        {
            StringBuilder builder1 = new StringBuilder();
            builder1.Append("<script language = javascript defer>");
            builder1.Append("window.opener.refresh(false);");
            builder1.Append(" window.focus();");
            builder1.Append(" window.opener=null;");
            builder1.Append(" window.close(); ");
            builder1.Append("</script>");
            page.Response.Write(builder1.ToString());
        }

        // Properties
        protected int RecordCount
        {
            get
            {
                return this.recordCount;
            }
        }

        protected DataSet UISet
        {
            get
            {
                return this._UISet;
            }
            set
            {
                this._UISet = value;
            }
        }

        protected DataTable UITable
        {
            get
            {
                return this._UITable;
            }
            set
            {
                this._UITable = value;
            }
        }
    }
}
