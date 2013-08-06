using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;


public partial class Webparts_LeftBar : System.Web.UI.UserControl
{
    DataTable dt;
    string cnKey = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["cnKey"] != null)
            cnKey = Session["cnKey"].ToString();

        Js.BLL.Sys.TreeListDal dal = new Js.BLL.Sys.TreeListDal(cnKey);
        string UserType = Session["UserType"].ToString();
        string filter = "";
        if (UserType == "BU")
            filter = "SysID<11 and FormID<>'EP_UserPwdUpdate'";
        //else if (UserType == "EP")
        //    filter = "(SysID=0 or SysID=3 Or PrintPrefix='EP')";        
        else
            filter = " 1=1";
        dt = dal.GetLeftBar(filter, Session["language_session"].ToString());
        string preModuleName = "";
        string preSubModuleName = "";
        Table tbModule = null;
        Panel pSubModule = null;
        int index = 0;
        foreach (DataRow dr in dt.Rows)
        {
            string currentModuleName = dr["ParentTitle"].ToString();
            string currentSubModuleName = dr["Text"].ToString();
            string url = dr["Url"].ToString() + "?FormID=" + dr["FormID"].ToString();
            string image = dr["ImageUrl"].ToString();
            string ParentImage = dr["ParentImageUrl"].ToString();
            string ModuleID = dr["ID"].ToString();
            if (preModuleName != currentModuleName)
            {
                preModuleName = currentModuleName;
                preSubModuleName = currentSubModuleName;
                //string parentCode = dr["MenuCode"].ToString().Substring(0, 8);
                tbModule = this.CreateModuleTable(preModuleName, ParentImage);
                tbModule.ID = "table" + index.ToString();
                this.plMenu.Controls.Add(tbModule);
                pSubModule = new Panel();
                // pSubModule.Height = 50; 
                pSubModule.ID = "div" + index.ToString();
              
                    pSubModule.Attributes.Add("style", "display:none;");
                tbModule.Attributes.Add("onclick", "Display('" + index.ToString() + "');");
                this.plMenu.Controls.Add(pSubModule);
                pSubModule.Controls.Add(CreateSubModuleTable(preModuleName, preSubModuleName, url, image, "tab_" + ModuleID));
            }
            else
            {
                if (preSubModuleName != currentSubModuleName)
                {
                    preSubModuleName = currentSubModuleName;
                    pSubModule.Controls.Add(CreateSubModuleTable(preModuleName, preSubModuleName, url, image, "tab_" + ModuleID));
                }
            }
            index++;
        }

        //tbModule = CreateModuleTable("退出系统", "");
        //tbModule.ID = "table" + index.ToString();
        //pSubModule = new Panel();
        //this.plMenu.Controls.Add(tbModule);
        //pSubModule = new Panel();
        //pSubModule.ID = "div" + index.ToString();
        //pSubModule.Attributes.Add("style", "display:none;");
        //pSubModule.Controls.Add(CreateLogoutTable());
        //tbModule.Attributes.Add("onclick", "Display('" + index.ToString() + "');");
        //this.plMenu.Controls.Add(pSubModule);


        //操作权限保存Sesion中（ModuleID,OperatorCode,MenuCode)
        //dt.Columns.Remove("MenuImage");
        //dt.Columns.Remove("MenuUrl");
        //dt.Columns.Remove("MenuTitle");
        //dt.Columns.Remove("MenuParent");
        //Session["DT_UserOperation"] = dt;

    }

    public Table CreateModuleTable(string ModuleName, string ParentImage)
    {
        Table tbl = new Table();
        //tbl.Attributes.Add("class", "Menu");
        tbl.Attributes.Add("cellspacing", "0");
        tbl.Attributes.Add("cellpadding", "0");
        tbl.Attributes.Add("style", "width: 100%; height: 28px; padding:2 5 3 2;  cursor:hand; color:#000000;background-image:url(../images/leftmenu/button.jpg)");
        //tbl.Attributes.Add("style", "width: 100%; height: 28px; padding:2 5 3 2; border-right: buttonshadow 1px solid; border-top: #f5f5f5 1px solid; border-left: #f5f5f5 1px solid; border-bottom: buttonshadow 1px solid; background-color:Transparent; cursor:hand; color:#000000;");
        TableRow tr = new TableRow();
        TableCell tdImg = new TableCell();
        TableCell td = new TableCell();
        //Image img = new Image();
        //img.Attributes.Add("style", "vertical-align: middle; border:0;hspace:3;");//width:20px;height:20px;
        //if (ParentImage == "")
        //{
        //    img.ImageUrl = "../images/leftmenu/exit.gif";
        //}
        //else
        //{
        //    img.ImageUrl = "../images/leftmenu/" + ParentImage;
        //}
        //tdImg.Attributes.Add("style", "width:5%; text-align:right;");//FILTER:progid:DXImageTransform.Microsoft.Gradient(GradientType=1, StartColorStr=#ffffff, EndColorStr=buttonface);
        //tdImg.Controls.Add(img);
        td.Text = "&nbsp;&nbsp;" + ModuleName;
        // td.Attributes.Add("style", "text-align:left; FILTER:progid:DXImageTransform.Microsoft.Gradient(GradientType=1, StartColorStr=buttonface, EndColorStr=white);");   //#91D6FA
        tr.Controls.Add(tdImg);
        tr.Controls.Add(td);
        tbl.Controls.Add(tr);
        return tbl;
    }

    public Table CreateSubModuleTable(string ModuleName, string SubModuleName, string url, string image, string tabID)
    {
        Table tbl = new Table();
        tbl.Attributes.Add("class", "Option");
        //tbl.Attributes.Add("style", "width: 100%; height: 24px; padding:2 5 3 16; border: 1 1 1 1 solid #ffffff; background-color:white;");
        TableRow tr = new TableRow();
        TableCell td = new TableCell();
        TableCell tdImg = new TableCell();
        tdImg.Attributes.Add("style", "width:15%; text-align:right;");
        Image img = new Image();
        img.ImageUrl = "../images/leftmenu/" + image;
        tdImg.Controls.Add(img);
        //td.Text ="<a href='"+url+"'  target='mainFrame'>"+ SubModuleName+"</a>";
        //string nav = string.Format("window.open(\"NavigationPage.aspx?PG={0}>>{1}\",\"Navigation\")", ModuleName, SubModuleName);
        string nav = "";
        //td.Text = string.Format("<a href=\"javascript:void(0);\" onclick='window.parent.hrefTab(\"{0}&tabId={3}\",\"{1}\",\"{3}\");{2}'  target='mainFrame'>{1}</a>", url, SubModuleName, nav, tabID);
        //td.Text = string.Format("<a href=\"javascript:void(0);\" onclick='window.parent.mainFrame.addTab(\"{0}&tabId={3}\",\"{1}\",\"{3}\");{2}'  target='mainFrame'>{1}</a>", ResolveUrl(url), SubModuleName, nav, tabID);
        td.Text = string.Format("<a href=\"javascript:void(0);\" onclick='window.parent.mainFrame.addTab(\"{0}\",\"{1}\",\"{3}\");{2}' target='mainFrame'>{1}</a>", ResolveUrl(url), SubModuleName, nav, tabID);

        //td.Attributes.Add("onclick", string.Format("window.open('NavigationPage.aspx?PG={0}>>{1}','Navigation')",ModuleName,SubModuleName));
        tr.Controls.Add(tdImg);
        tr.Controls.Add(td);
        tbl.Controls.Add(tr);
        return tbl;
    }

    public Table CreateLogoutTable()
    {
        Table tbl = new Table();
        tbl.Attributes.Add("class", "Option");
        //tbl.Attributes.Add("style", "width: 100%; height: 24px; padding:2 5 3 16; border: 1 1 1 1 solid #ffffff; background-color: white;");
        TableRow trCancellation = new TableRow();
        TableRow trWithdrawal = new TableRow();

        TableCell tdCancellation = new TableCell();
        TableCell tdWithdrawal = new TableCell();

        TableCell tdCancellationImg = new TableCell();
        TableCell tdWithdrawalImg = new TableCell();

        tdCancellationImg.Attributes.Add("style", "width:15%; text-align:right;");
        tdWithdrawalImg.Attributes.Add("style", "width:15%;text-align:right;");

        Image imgCancellation = new Image();
        imgCancellation.ImageUrl = "../images/leftmenu/15.gif";
        Image imgWithdrawal = new Image();
        imgWithdrawal.ImageUrl = "../images/leftmenu/15.gif";

        tdCancellationImg.Controls.Add(imgCancellation);
        tdWithdrawalImg.Controls.Add(imgWithdrawal);

        tdCancellation.Text = "<a href='#'>注销</a>";
        tdCancellation.Attributes.Add("onclick", "Logout();");
        tdWithdrawal.Text = "<a href='#'>退出</a>";
        tdWithdrawal.Attributes.Add("onclick", "Exit();");

        trCancellation.Controls.Add(tdCancellationImg);
        trCancellation.Controls.Add(tdCancellation);
        trWithdrawal.Controls.Add(tdWithdrawalImg);
        trWithdrawal.Controls.Add(tdWithdrawal);
        tbl.Controls.Add(trCancellation);
        tbl.Controls.Add(trWithdrawal);
        return tbl;
    }
    private DataTable DataTable()
    {
        //Create a DataTable instance
        DataTable dt = new DataTable();

        //Create 4 columns for this DataTable and DataType of the Columns
        DataColumn col1 = new DataColumn("ID", typeof(int));
        DataColumn col2 = new DataColumn("MenuTitle", typeof(string));
        DataColumn col3 = new DataColumn("MenuParent", typeof(string));
        DataColumn col4 = new DataColumn("MenuUrl", typeof(string));
        DataColumn col5 = new DataColumn("MenuImage", typeof(string));
        DataColumn col6 = new DataColumn("ParentImage", typeof(string));
        DataColumn col7 = new DataColumn("OrderIndex", typeof(string));
        
        dt.Columns.Add("ID", typeof(int));
        dt.Columns.Add("MenuTitle", typeof(string));
        dt.Columns.Add("MenuParent", typeof(string));
        dt.Columns.Add("MenuUrl", typeof(string));
        dt.Columns.Add("MenuImage", typeof(string));
        dt.Columns.Add("ParentImage", typeof(string));
        dt.Columns.Add("OrderIndex", typeof(string));

        dt.Rows.Add(11, "營運公告", "公告", "~/WebUI/BusinessUnit/Departments.aspx?FormID=Department", "15.gif", "BASE.gif", "1");
        dt.Rows.Add(12, "企業用戶公告", "公告", "~/WebUI/BusinessUnit/Default.aspx?FormID=Department", "15.gif", "BASE.gif", "1");
        dt.Rows.Add(13, "標籤公告", "公告", "~/WebUI/BusinessUnit/Departments.aspx?FormID=Department", "15.gif", "BASE.gif", "1");

        dt.Rows.Add(21, "郵件通知", "待處理工作", "~/WebUI/BusinessUnit/Departments.aspx?FormID=Department", "15.gif", "BASE.gif", "2");
        dt.Rows.Add(22, "電話提醒", "待處理工作", "~/WebUI/BusinessUnit/Departments.aspx?FormID=Department", "15.gif", "BASE.gif", "2");
        dt.Rows.Add(23, "其他通知", "待處理工作", "~/WebUI/BusinessUnit/Departments.aspx?FormID=Department", "15.gif", "BASE.gif", "2");

        dt.Rows.Add(31, "公司資料設定", "營運單位模組", "~/WebUI/BusinessUnit/Departments.aspx?FormID=Department", "15.gif", "BASE.gif", "3");
        dt.Rows.Add(32, "部門資料設定", "營運單位模組", "~/WebUI/BusinessUnit/Departments.aspx?FormID=Department", "15.gif", "BASE.gif", "3");
        dt.Rows.Add(33, "人員資料設定", "營運單位模組", "~/WebUI/BusinessUnit/Departments.aspx?FormID=Department", "15.gif", "BASE.gif", "3");

        dt.Rows.Add(41, "企業用戶設定", "企業用戶模組", "~/WebUI/BusinessUnit/Departments.aspx?FormID=Department", "15.gif", "BASE.gif", "4");
        dt.Rows.Add(42, "企業產品設定", "企業用戶模組", "~/WebUI/BusinessUnit/Departments.aspx?FormID=Department", "15.gif", "BASE.gif", "4");
        dt.Rows.Add(43, "企業產品類別", "企業用戶模組", "~/WebUI/BusinessUnit/Departments.aspx?FormID=Department", "15.gif", "BASE.gif", "4");

        dt.Rows.Add(51, "標籤入庫", "標籤作業模組", "~/WebUI/BusinessUnit/Departments.aspx?FormID=Department", "15.gif", "BASE.gif", "5");
        dt.Rows.Add(52, "標籤出庫", "標籤作業模組", "~/WebUI/BusinessUnit/Departments.aspx?FormID=Department", "15.gif", "BASE.gif", "5");
        dt.Rows.Add(53, "標籤上傳", "標籤作業模組", "~/WebUI/BusinessUnit/Departments.aspx?FormID=Department", "15.gif", "BASE.gif", "5");

        dt.Rows.Add(61, "真偽查詢", "消費者(會員)模組", "~/Default.aspx", "15.gif", "BASE.gif", "6");
        dt.Rows.Add(62, "產品查詢", "消費者(會員)模組", "~/Default.aspx", "15.gif", "BASE.gif", "6");
        dt.Rows.Add(63, "會員優惠", "消費者(會員)模組", "~/Default.aspx", "15.gif", "BASE.gif", "6");

        

        return dt;
    }
}
