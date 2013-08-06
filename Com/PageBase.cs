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
                    return "共用";
                case "1":
                    return "訂單";
                case "2":
                    return "庫存";
                case "3":
                    return "生管";
                case "4":
                    return "進口";
                case "5":
                    return "出口";
                case "6":
                    return "票據";
                case "7":
                    return "固資";
                case "8":
                    return "財務";
                case "9":
                    return "帳款";
                case "10":
                    return "發票";
                case "11":
                    return "申報";
                case "12":
                    return "人薪";
                case "13":
                    return "客服";
                case "14":
                    return "權限";
                default:
                    return "";
            }
        }
        public int GetSysID(string SysName)
        {
            switch (SysName)
            {
                case "共用":
                    return 0;
                case "訂單":
                    return 1;
                case "庫存":
                    return 2;
                case "生管":
                    return 3;
                case "進口":
                    return 4;
                case "出口":
                    return 5;
                case "票據":
                    return 6;
                case "固資":
                    return 7;
                case "財務":
                    return 8;
                case "帳款":
                    return 9;
                case "發票":
                    return 10;
                case "申報":
                    return 11;
                case "人薪":
                    return 12;
                case "客服":
                    return 13;
                case "權限":
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
