using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class WebUI_Account_BU_CopyUserPermission : BasePage
{
    protected string FormID;
    protected string UserName;

    protected void Page_Load(object sender, EventArgs e)
    {
        FormID = Request.QueryString["FormID"];
        UserName = Request.QueryString["UserName"];
        BindDropDownList();
    }
    private void BindDropDownList()
    {
        Js.BLL.Account.UserDal dal = new Js.BLL.Account.UserDal();
        Js.Model.Account.UsersInfo model = dal.GetModel(UserName);
        string filter = string.Format("UserName<>'administrator' and UserName<>'{0}' and ",UserName);
        if (model.UserLevel % 1000 == 0)
            filter += "UserLevel % 1000=0";
        else
            filter += "UserLevel % 1000>0";
        DataTable dt = dal.GetRecord(filter);
        this.ddlUserID.DataSource = dt;
        this.ddlUserID.DataTextField = "UserName";
        this.ddlUserID.DataValueField = "UserID";
        this.ddlUserID.DataBind();
    }
}