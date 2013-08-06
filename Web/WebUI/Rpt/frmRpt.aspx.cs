using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FastReport;
using System.Collections.Generic;
using FastReport.Data;
using FastReport.Utils;
using System.Data;
using System.IO;

public partial class WebUI_Rpt_frmRpt : System.Web.UI.Page
{
    protected string FormID;
    protected string ID;
    protected string cnKey = "Label";
    Js.Model.Sys.TreeListInfo SysModel;
    DataTable dtHead;
    string strwhere;

    protected void Page_Load(object sender, EventArgs e)
    {
        FormID = Request.QueryString["FormID"] + "";
        cnKey = Request.QueryString["cnKey"] + "";
        ID = Request.QueryString["ID"] + "";

        SysModel = new Js.DAO.Sys.TreeListDao().GetModel(FormID);
        if (!IsPostBack)
        {
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "Resize", "content_resize();", true);
            rptview.Visible = false;
            //this.rptform.Visible = false;
            rptopt.Visible = false;
            // WebReport1.Zoom = 1.5f;
            WebReport1.Width = int.Parse(Request.QueryString["W"] + "") - 150;
            WebReport1.Height = int.Parse(Request.QueryString["H"] + "") - 25;

            this.txtAlone.Text = ID;
            optArea.Checked = (ID.Length == 0);            

            Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID, cnKey);

            if (Session["language_session"] + "" == "zh-cn")
                lblCaption.Text = SysModel.Text_cn + "--" + lblCaption.Text;
            else if (Session["language_session"] + "" == "en-us")
                lblCaption.Text = SysModel.Text_en + "--" + lblCaption.Text;
            else
                lblCaption.Text = SysModel.Text + "--" + lblCaption.Text;
            DataTable dt;
            
            dt = dal.GetRecord("F", "");
            if (dt != null && dt.Rows.Count > 0)
                this.txtStart.Text = "" + dt.Rows[0][SysModel.KeyField];
            dt = dal.GetRecord("L", "");
            if (dt != null && dt.Rows.Count > 0)
                this.txtEnd.Text = "" + dt.Rows[0][SysModel.KeyField];
            DropDownList_ReSetRptName(FormID, cnKey, 0, ref cmbRptName);            
        }
        btViewLast.Enabled = Request.Cookies["rptwhere_" + FormID] != null && ("" + Request.Cookies["rptwhere_" + FormID].Value != "");

        txtAlone.Attributes.Add("ondblclick", "GetOtherJsonValue(\"" + FormID + "\", \"" + cnKey + "\", \"" + SysModel.KeyField + "\", \"txtAlone\");$('#optAlone').attr('checked', true);");
        txtStart.Attributes.Add("ondblclick", "GetOtherJsonValue(\"" + FormID + "\", \"" + cnKey + "\", \"" + SysModel.KeyField + "\", \"txtAlone\");$('#optArea').attr('checked', true);");
        txtEnd.Attributes.Add("ondblclick", "GetOtherJsonValue(\"" + FormID + "\", \"" + cnKey + "\", \"" + SysModel.KeyField + "\", \"txtAlone\");$('#optArea').attr('checked', true);");
        btnSel1.OnClientClick = "getMultiItems(\"" + FormID + "\", \"" + cnKey + "\", 1, \"" + SysModel.KeyField + "\", this, '#Hidden1');return false;";
    }
    protected void WebReport1_StartReport(object sender, EventArgs e)
    {
        if (!rptview.Visible) return;
        LoadRpt();
    }
    protected void btnPreview_Click(object sender, EventArgs e)
    {
        string strwhere = GetStrWhere(); 
        SaveCookie30Day("rptwhere_" + FormID, strwhere);

        Js.DAO.Rpt.RptDao dal = new Js.DAO.Rpt.RptDao(FormID, cnKey);
        dal.InitHead(dtHead, GetHostName());
        dal.SetHeadParameter("CompanyName", "WebUI_Rpt_frmRpt_測試");
        dal.SaveHead();
        
        this.rptform.Visible = false;
        rptview.Visible = true;
        rptopt.Visible = true;
        WebReport1.Refresh();
    }

    private void SaveCookie30Day(string key, string value)
    {
        HttpCookie cookie = new HttpCookie(key, value);    //实例化HttpCookie类并添加值
        cookie.Expires = DateTime.Now.AddDays(30);                             //设置保存时间
        Response.Cookies.Add(cookie);
    }

    private string GetHostName()
    {
        if (Request.Cookies["HostName_" + FormID] == null || ("" + Request.Cookies["HostName_" + FormID].Value == ""))
            SaveCookie30Day("HostName_" + FormID, Guid.NewGuid ().ToString ());
        return "" + Request.Cookies["HostName_" + FormID].Value;
    }

    private string GetStrWhere()
    {
        strwhere = SysModel.strWhere;
        if (new Js.BLL.BaseDal(FormID, cnKey).GetRecord(SysModel.KeyField + "=''").Columns.IndexOf("EnterpriseID") != -1 && "" + Session["EnterpriseID"] != "")
            strwhere = strwhere + string.Format(" and EnterpriseID='{0}' ", Session["EnterpriseID"].ToString());
        if (optAlone.Checked)
            strwhere = strwhere + " and " + SysModel.KeyField + string.Format("='{0}'", this.txtAlone.Text.Trim());
        else
            if (this.Hidden1.Value.Length == 0)
                strwhere = strwhere + " and " + string.Format(SysModel.KeyField + ">='{0}' and " + SysModel.KeyField + "<='{1}' ", this.txtStart.Text.Trim(), this.txtEnd.Text.Trim());
            else
                strwhere = strwhere + " and " + SysModel.KeyField + " in (" + this.Hidden1.Value + ") ";
        return strwhere;
    }

    protected void btnback_Click(object sender, EventArgs e)
    {
        rptform.Visible = true;
        rptview.Visible = false;
        rptopt.Visible = false;
    }
    protected void BtBack_Click(object sender, EventArgs e)
    {
        SysModel = new Js.DAO.Sys.TreeListDao().GetModel(FormID);
        Response.Redirect(SysModel.Url + "?FormID=" + FormID);
    }

    protected void DropDownList_ReSetRptName(string FormID, string cnKey,  int RptCls, ref DropDownList DropDownListRpt)
    {
        string strFind = FormID + "*.frx";

        string strPath = Js.Com.ConfigHelper.GetConfigString("RptPath");
                
        if (RptCls == 1)//用戶自定義報表
            strPath = strPath + "\\User\\";
        else
            strPath = strPath + "\\" + cnKey + "\\";
        DropDownListRpt.Items.Clear(); 
        DirectoryInfo dir = new DirectoryInfo(strPath);
        FileInfo[] files = dir.GetFiles(strFind);

        for (int i = 0; i < files.Length; i++)
        {
            ListItem item = new ListItem();
            item.Text = files[i].Name.Substring(FormID.Length, files[i].Name.Length - 4 - FormID.Length);
            if (item.Text.Length == 0)
                item.Text = files[i].Name.Substring(0, files[i].Name.Length - 4);
            item.Value = files[i].FullName;
            DropDownListRpt.Items.Add(item);
        }
    }
    protected void rbtFixed_CheckedChanged(object sender, EventArgs e)
    {
        DropDownList_ReSetRptName(FormID, cnKey, 0, ref cmbRptName);
    }
    protected void rbtUser_CheckedChanged(object sender, EventArgs e)
    {
        DropDownList_ReSetRptName(FormID, cnKey, 1, ref cmbRptName);
    }
    protected void btPrint_Click(object sender, EventArgs e)
    {
        //if (LoadRpt())
        //    WebReport1.PrintPdf();
    }

    private bool LoadRpt()
    {
        if (cmbRptName.SelectedValue + "" == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('" + Literal8.Text + "');", true);
            return false;
        }
        try
        {
            WebReport1.Report = new Report();
            WebReport1.Report.Load(cmbRptName.SelectedValue);
            //Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID, cnKey);
            //Js.DAO.Rpt.RptDao dal = new Js.DAO.Rpt.RptDao("LB_Style", "Label");
            Js.DAO.Rpt.RptDao dal = new Js.DAO.Rpt.RptDao(FormID, cnKey);
            //string strwhere = ""+Request.Cookies["rptwhere_" + FormID].Value;

            DataTable dt ;
            for (int i = 0; i < WebReport1.Report.Dictionary.Connections[0].Tables.Count; i++)
            {               
                if ("Rpt_Head" == WebReport1.Report.Dictionary.Connections[0].Tables[i].Name)
                    WebReport1.Report.RegisterData(dal.GetHead(GetHostName()), WebReport1.Report.Dictionary.Connections[0].Tables[i].Name);
                else
                {
                    dt = dal.GetRptData(WebReport1.Report.Dictionary.Connections[0].Tables[i].Name, strwhere);
                    if (dt.Rows.Count == 0 && i == 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('" + Literal1.Text + "');", true);
                    }
                    WebReport1.Report.RegisterData(dt, WebReport1.Report.Dictionary.Connections[0].Tables[i].Name);
                }
            }

            //WebReport1.Report.RegisterData(dal.GetViewRecord("1=1 and id = 'A0001_A02'"), WebReport1.Report.Dictionary.RegisteredItems[0].Name);
            //DataSet ds = new DataSet();
            //WebReport1.Report.RegisterData(ds);
            //for (int i = 0; i < WebReport1.Report.Dictionary.RegisteredItems.Count; i++)
            //{
            //    WebReport1.Report.RegisterData(dal.GetViewRecord("1=1 and id = 'A0001_A02'"), WebReport1.Report.Dictionary.RegisteredItems[i].Name);
            //}
            //Js.BLL.BaseDal dal = new Js.BLL.BaseDal("LB_Style", "Label");
            ////WebReport1.Report.Dictionary.RegisteredItems[0].Name
            //WebReport1.Report.RegisterData(dal.GetViewRecord("1=1 and id = 'A0001_A02'"), "LB_Style");
            
        }
        catch
        { }
        return true;
    }
    protected void btViewLast_Click(object sender, EventArgs e)
    {
        this.rptform.Visible = false;
        rptview.Visible = true;
        rptopt.Visible = true;
        strwhere = "" + Request.Cookies["rptwhere_" + FormID].Value;
        WebReport1.Refresh();
    }
}