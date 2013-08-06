using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Net;

/// <summary>
/// Summary description for BasePage
/// </summary>
public class BasePage : BaseLanguage
{
    public string Permission;
    public BasePage()
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //
        
    }
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            if (Session["User"] == null)
            {
                Response.Write("<script language=javascript>parent.parent.location.href='../../WebUI/Start/SessionTimeOut.aspx';</script>");
                return;
            }  
            if (!IsPostBack)
            {               
                InitLoading();
            }
            string FormID = Request.QueryString["FormID"];
            //權限
            if (Context.User.Identity.Name.ToLower() == "administrator" || Context.User.Identity.Name.ToLower() == "supervisor")
                ViewState["Permission"] = "3333333330";
            else
            {
                string cnkey = "";
                if (Session["UserType"].ToString() == "EP")
                    cnkey = Session["EnterpriseID"].ToString();

                Js.BLL.Account.UserDal user = new Js.BLL.Account.UserDal(Context.User.Identity.Name, cnkey);
                ViewState["Permission"] = user.GetUserPermissionByFormID(FormID);
            }

            Js.BLL.Sys.SysManageDal dal = new Js.BLL.Sys.SysManageDal();
            DataTable dt = dal.GetSysEmptyRecord();
            DataRow dr = dt.NewRow();
            dr["UserType"] = Session["UserType"].ToString();
            dr["UserName"] = Session["User"].ToString();
            dr["PersonName"] = Session["User"].ToString();
            dr["OpDate"] = DateTime.Now;
            dr["FormID"] = Request.QueryString["FormID"] + "";
            Js.BLL.Sys.TreeListDal tdal = new Js.BLL.Sys.TreeListDal();
            Js.Model.Sys.TreeListInfo model = tdal.GetModel(dr["FormID"].ToString());
            if(Session["language_session"].ToString().ToLower()=="zh-tw")
                dr["FormName"] = model.Text;
            else if (Session["language_session"].ToString().ToLower() == "zh-cn")
                dr["FormName"] = model.Text_cn;
            else
                dr["FormName"] = model.Text_en;
            dr["ActionState"] = "";
            //dr["IP"] = HttpContext.Current.Request.UserHostAddress;
            dr["IP"] = Page.Request.UserHostAddress;
            dal.InsertSysRrecord(dr);
        }
        catch(Exception ex)
        {

        }
    }

    public void InitLoading()
    {
        HttpContext hc = HttpContext.Current;
        //创建一个页面居中的div
        hc.Response.Write("<style>");
        hc.Response.Write("#loader_container {text-align:center; position:absolute; top:40%; width:100%; left: 0;}");
        hc.Response.Write("#loader {font-family:Tahoma, Helvetica, sans; font-size:11.5px; color:#000000; background-color:#FFFFFF; padding:10px 0 16px 0; margin:0 auto; display:block; width:320px; border:1px solid #5a667b; text-align:left; z-index:2;}");
        hc.Response.Write("#progress {height:8px; font-size:1px; width:34px; position:relative; top:1px; left:0px; background-color:#8894a8;}");
        hc.Response.Write("#loader_bg {background-color:#e4e7eb; position:relative; top:8px; left:22px; height:10px; width:270px; font-size:1px;}");
        hc.Response.Write("</style>");
        hc.Response.Write("<div id=loader_container>");
        hc.Response.Write("<div id=loader>");
        hc.Response.Write("<div align=center style='font-size:20px'>頁面正在加載,請稍候 ...</div>");
        hc.Response.Write("<div id=loader_bg><marquee direction='right' scrollamount='10'><div id=progress> </div></marquee></div>");
        hc.Response.Write("</div></div>");
        //hc.Response.Response.Write("<script>mydiv.innerText = '';</script>");
        hc.Response.Write("<script type=text/javascript>");
        //最重要是这句了,重写文档的onreadystatechange事件,判断文档是否加载完毕
        hc.Response.Write("function document.onreadystatechange()");
        hc.Response.Write(@"{ try  
                                   {
                                    if (document.readyState == 'complete') 
                                    {
                                         delNode('loader_container');
                                        
                                    }
                                   }
                                 catch(e)
                                    {
                                        alert('頁面加載失敗');
                                    }
                                                        } 

                            function delNode(nodeId)
                            {   
                                try
                                {   
                                      var div =document.getElementById(nodeId); 
                                      if(div !==null)
                                      {
                                          div.parentNode.removeChild(div);   
                                          div=null;    
                                          CollectGarbage(); 
                                      } 
                                }
                                catch(e)
                                {   
                                   alert('刪除ID為'+nodeId+'的節點出現異常');
                                }   
                            }

                            ");

        hc.Response.Write("</script>");
        hc.Response.Flush();
    }
    #region 小高
    /// <summary>
    /// 將日期格式yyyy/MM/dd
    /// </summary>
    public string ToYMD(object dateobj)
    {
        return DateFormat(dateobj, "yyyy/MM/dd");
    }
    /// <summary>
    /// 將日期格式yyyy/MM/dd HH:mm
    /// </summary>
    public string ToYMDHM(object dateobj)
    {
        return DateFormat(dateobj, "yyyy/MM/dd HH:mm");
    }
    private string DateFormat(object dateobj, string strFormat)
    {
        if (dateobj == null || dateobj.ToString().Trim().Length == 0) return "";
        return ((DateTime)dateobj).ToString(strFormat).Replace("-", "/");   //"yyyy/MM/dd HH:mm"     
    }

    /// <summary>
    /// 取得下拉結構
    /// </summary>
    private string getJsddlstruct(string strddlname)
    {
        //ddlIsLimitedProduct
        System.Web.UI.WebControls.DropDownList ctrl = (System.Web.UI.WebControls.DropDownList)this.FindControl(strddlname);

        string strReturn = "";
        for (int i = 0; i < ctrl.Items.Count; i++)
        {
            if (strReturn.Length == 0)
                strReturn = "<option value=\"" + ctrl.Items[i].Value + "\">" + ctrl.Items[i].Text + "</option>";
            else
                strReturn = strReturn + "<option value=\"" + ctrl.Items[i].Value + "\">" + ctrl.Items[i].Text + "</option>";
        }
        return strReturn;
    }

    /// <summary>
    /// 往前台 寫入變量\下拉控件js結構\提示信息
    /// </summary>
    ///  <param name="strddlcols">要寫入所有下拉控件名稱 "ddl1,ddl2" </param>
    public void writeJsvar(string FormID, string cnKey, string id)
    {
        String cstext = "var cnKey = '" + cnKey + "';" +
                        "var FormID = '" + FormID + "';" +
                        "var oldID = '" + id.Trim() + "';" +
                        "var strUserName = '" + Session["User"].ToString() + "';" +
                        "var isEditState = " + (id.Trim().Length == 0 ? "false" : "true") + ";" +
                        "var strDateFormat = 'y/mm/dd';" +
                        "var strDataFormat = new Object();" +
                        "strDataFormat[0] = 'N0';" +//數量
                        "strDataFormat[1] = 'N0';" +//單價
                        "strDataFormat[2] = 'N0';" +//金額
                        "strDataFormat[3] = '" + GetSparesFormat() + "';";//比率
        //明細下拉結構
        System.Web.UI.Control ctrl = this.FindControl("subddlstruct");
        if (ctrl != null)
        {
            for (int i = 0; i < ctrl.Controls.Count; i++)
            {
                if (ctrl.Controls[i].ID != null)
                {
                    cstext = cstext + "var " + ctrl.Controls[i].ID.Trim() + " = '" + getJsddlstruct(ctrl.Controls[i].ID) + "';";
                }
            }
        }
        //string[] arr = strddlcols.Split(',');
        //for (int i = 0; i < arr.Length; i++)
        //{
        //    if (arr[i].Trim().Length > 0)
        //        cstext = cstext + "var " + arr[i].Trim() + " = '" + getJsddlstruct(arr[i].Trim()) + "';";
        //}
        //msg
        ctrl = this.FindControl("msg");
        if (ctrl != null)
        {
            for (int i = 0; i < ctrl.Controls.Count; i++)
            {
                if (ctrl.Controls[i].ID != null)
                {
                    cstext = cstext + "var " + ctrl.Controls[i].ID.Trim() + " = '" + ((System.Web.UI.WebControls.Literal)ctrl.Controls[i]).Text + "';";
                }
            }
        }
        this.Page.ClientScript.RegisterClientScriptBlock(base.GetType(), "Jsvar", cstext.ToString(), true);
    }
    /// <summary>
    /// 取得比率位數 
    /// </summary>
    /// <returns></returns>
    public string GetSparesFormat()
    {
        Js.BLL.BaseDal dal = new Js.BLL.BaseDal("BU_Parameter");
        DataTable dt = dal.GetRecord("1=1");
        return (dt != null && dt.Rows.Count > 0 ? "N" + dt.Rows[0]["PercentDecimalDigits"].ToString().Trim() : "N0");
        //string.Format ("{0:N2}",111)
    }
    /// <summary>
    /// 參考 Web\WebUI\Label\OrderEdit.aspx調用 
    /// 動態生成 
    /// init_subColsName1() 初始化明細1
    /// init_subColsName2() 初始化明細2
    /// ...
    /// </summary>
    /// <param name="strctrlID">strctrlID 以sub1xxRowID 為例說明 xx 為 xx 時不區分功能都顯示, 當xx 為 BU 剛 只有BU塊才顯示</param>
    /// <param name="strTableName">對應明細資料表</param>
    public void InitSubCols(string FormID, string cnKey, string strctrlID, string strTableName)
    {
        //      colname,colwidth,type,readonly 
        //Text="(序號) ,40      ,text,1       "
        //String cstext1 = "var test = '" + subColsName1.Controls.Count + "'";
        String cstext1 = "var row1 = new Object();";
        String cstext2 = "var row2 = new Object();";
        String cstext3 = "var row3 = new Object();";
        String cstext4 = "";
        //subColsName1.Controls.Count;
        Js.DAO.TableFieldInfo tf = new Js.DAO.TableFieldInfo(strTableName, cnKey);
        string[] arr;
        System.Web.UI.Control ctrl = this.FindControl(strctrlID);
        for (int i = 0; i < ctrl.Controls.Count; i++)
        {
            if (ctrl.Controls[i].ID != null)
            {
                arr = ((System.Web.UI.WebControls.Literal)ctrl.Controls[i]).Text.Split(',');
                if (ctrl.Controls[i].ID.Substring(4, 2).ToLower() != "xx" && ctrl.Controls[i].ID.Substring(4, 2) != Session["UserType"].ToString())
                    cstext4 = cstext4 + "isShowColumn(\"Detail" + strctrlID.Substring(strctrlID.Length - 1, 1) + "_Col_" + ctrl.Controls[i].ID.Substring(6) + "\", false);";
                cstext1 = cstext1 + "row1." + ctrl.Controls[i].ID.Substring(6) + "=\"" + arr[0] + "\";";
                cstext2 = cstext2 + "row2." + ctrl.Controls[i].ID.Substring(6) + "=\"" + arr[1] + "\";";
                if ("label" == arr[2].Trim().ToLower() || "date" == arr[2].Trim().ToLower() || "datetime" == arr[2].Trim().ToLower())
                    cstext3 = cstext3 + "row3." + ctrl.Controls[i].ID.Substring(6) + "=\"" + arr[2] + "\";";
                //else if ("dropdown" == arr[2].Trim().ToLower())
                //    cstext3 = cstext3 + "row3." + ctrl.Controls[i].ID.Substring(6) + "=\"" + arr[2] + "\";";
                else if ("checkbox" == arr[2].Trim().ToLower() || "dropdown" == arr[2].Trim().ToLower())
                    cstext3 = cstext3 + "row3." + ctrl.Controls[i].ID.Substring(6) + "=\"type=\\\"" + arr[2] + "\\\" " + (arr[3].Trim() == "1" ? "disabled=\\\"disabled\\\"" : "") + "\";";
                else
                    cstext3 = cstext3 + "row3." + ctrl.Controls[i].ID.Substring(6) + "=\"type=\\\"" + arr[2] + "\\\" " + (arr[3].Trim() == "1" ? "readonly=\\\"true\\\"" : "") + " " + (arr[2].ToLower() == "text" && arr[3].Trim() != "1" ? "maxlength=\\\"" + tf.GetTypeLength(ctrl.Controls[i].ID.Substring(6)) + "\\\"" : "") + "\";";
                //}

            }
        }

        cstext1 = cstext1 + "tbColHeadText.push(row1);";
        cstext2 = cstext2 + "tbColWidth.push(row2);";
        cstext3 = cstext3 + "tbColType.push(row3);";

        String cstext = " function init_" + strctrlID + "(){ " + cstext1 + cstext2 + cstext3 + " } " +
                        " function isShowColumn_" + strctrlID + "(){ " + cstext4 + " } ";

        this.Page.ClientScript.RegisterClientScriptBlock(base.GetType(), "subinit", cstext.ToString(), true);
    }
    #endregion
}