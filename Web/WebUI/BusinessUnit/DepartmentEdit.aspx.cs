using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebUI_BusinessUnit_DepartmentEdit : BasePage
{
    protected string FormID;
    protected string ID;
    protected void Page_Load(object sender, EventArgs e)
    {
        ID = Request.QueryString["ID"] + "";
        FormID = Request.QueryString["FormID"] + "";
        if (!IsPostBack)
        {
            
            ViewState["StrWhere"] = string.Format(" DepartmentID='{0}'", ID);
            Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID);
            if (ID != "")
            {                
                DataTable dt = dal.GetRecord(ViewState["StrWhere"].ToString());
                BindData(dt);
                this.txtDepartmentID.ReadOnly = true;
            }
            else
            {
                this.txtDepartmentID.Text = dal.GetMaxID();
                this.txtLastModifyUserName.Text = Session["User"].ToString();
                this.txtLastModifyDate.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm");
                this.txtCreateUserName.Text = Session["User"].ToString();
                this.txtCreateDate.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm");
            }
            //BindEdll();
        }
    }
    private void BindEdll()
    {
        //Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID);
        //DataTable dt = dal.GetIDNameList("");
        //this.EddlDepartment.DataSource = dt;
        //this.EddlDepartment.DataTextField = "IDName";
        //this.EddlDepartment.DataValueField = "ID";
        //this.EddlDepartment.DataBind();
    }
    private void BindData(DataTable dt)
    {        
        if (dt.Rows.Count > 0)
        {
            this.txtDepartmentID.Text = dt.Rows[0]["DepartmentID"].ToString();
            this.txtDepartmentName.Text = dt.Rows[0]["DepartmentName"].ToString();
            this.txtDepartmentEName.Text = dt.Rows[0]["DepartmentEName"].ToString();
            this.txtMemo.Text = dt.Rows[0]["Memo"].ToString();
            this.txtCreateUserName.Text = dt.Rows[0]["CreateUserName"].ToString();
            this.txtCreateDate.Text = Js.Com.PageValidate.ParseDateTime(dt.Rows[0]["CreateDate"].ToString());
            this.txtLastModifyUserName.Text = dt.Rows[0]["LastModifyUserName"].ToString();
            this.txtLastModifyDate.Text = Js.Com.PageValidate.ParseDateTime(dt.Rows[0]["LastModifyDate"].ToString());
            this.txtCheckUserName.Text = dt.Rows[0]["CheckUserName"].ToString();
            this.txtCheckDate.Text = Js.Com.PageValidate.ParseDateTime(dt.Rows[0]["CheckDate"].ToString());
            this.txtStopUserName.Text = dt.Rows[0]["StopUserName"].ToString();
            this.txtStopDate.Text = Js.Com.PageValidate.ParseDateTime(dt.Rows[0]["StopDate"].ToString());
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID);
        DataTable dt = dal.GetRecord(ViewState["StrWhere"].ToString());
        DataRow dr = dt.NewRow();
        dr["DepartmentID"] = this.txtDepartmentID.Text;
        dr["DepartmentName"] = this.txtDepartmentName.Text;
        dr["DepartmentEName"] = this.txtDepartmentEName.Text.Trim();
        dr["Memo"] = this.txtMemo.Text.Trim();
        dr["CreateUserName"] = this.txtCreateUserName.Text;
        dr["CreateDate"] = this.txtCreateDate.Text;
        dr["LastModifyUserName"] = Session["User"].ToString();
        dr["LastModifyDate"] = DateTime.Now.ToString(Js.Com.User.strDateFormat);
        dr["CheckUserName"] = this.txtCheckUserName.Text;
        if(this.txtCheckDate.Text.Length>0)
            dr["CheckDate"] = this.txtCheckDate.Text;
        dr["StopUserName"] = this.txtStopUserName.Text;
        if (this.txtStopDate.Text.Length>0)
            dr["StopDate"] = this.txtStopDate.Text;

        if (ID.Length > 0)
            dal.Update(dr, ID);
        else
            dal.Add(dr);

        //Response.Redirect("Departments.aspx?FormID=" + Server.UrlEncode(FormID));
        Response.Redirect("DepartmentView.aspx?FormID=" + Server.UrlEncode(FormID) + "&ID=" + Server.UrlEncode(this.txtDepartmentID.Text));
    }
}