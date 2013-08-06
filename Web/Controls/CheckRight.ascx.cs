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
using Js.BLL.Account;
public partial class Controls_CheckRight : System.Web.UI.UserControl
{
    public int PermissionID = -1;//無限制
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack && Session["UserInfo"] != null)
        {
            if (Context.User.Identity.IsAuthenticated)
            {
                UserPrincipal user = new UserPrincipal(Context.User.Identity.Name);
                if (Session["UserInfo"] == null)
                {
                    Js.BLL.Account.UserDal currentUser = new Js.BLL.Account.UserDal(user);
                    Session["UserInfo"] = currentUser;
                    Response.Write("<script defer>location.reload();</script>");
                }
                if ((PermissionID != -1) && (!user.HasPermissionID(int.Parse(Session["SysID"].ToString()), PermissionID)))
                {
                    Response.Clear();
                    Response.Write("<script defer>window.alert('您沒有權限進入本頁!\\n請重新登錄或與管理員聯絡!');history.back();</script>");
                    Response.End();
                }

            }
            else
            {
                FormsAuthentication.SignOut();
                Session.Clear();
                Session.Abandon();
                Response.Clear();
                Response.Write("<script defer>window.alert('您沒有權限進入本頁或當前登錄用戶已過期!\\n請重新登錄或與管理員聯絡!');parent.location='" + ResolveUrl("~/Login/Login.aspx") + ";</script>");
                Response.End();
            }

        }

    }
}
