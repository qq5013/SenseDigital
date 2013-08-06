using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebUI_Label_WarehouseEdit : BasePage
{
    protected string FormID;
    protected string ID;
    protected string cnKey = "Label";
    protected void Page_Load(object sender, EventArgs e)
    {
        ID = Request.QueryString["ID"] + "";
        FormID = Request.QueryString["FormID"] + "";
        if (!IsPostBack)
        {

            ViewState["StrWhere"] = string.Format(" WarehouseID='{0}'", ID);
            Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID, cnKey);
            if (ID != "")
            {
                DataTable dt = dal.GetRecord(ViewState["StrWhere"].ToString());
                BindData(dt);
                this.txtWarehouseID.ReadOnly = true;
            }
            else
            {
                this.txtWarehouseID.Text = dal.GetMaxID();
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
            this.txtWarehouseID.Text = dt.Rows[0]["WarehouseID"].ToString();
            this.txtWarehouseName.Text = dt.Rows[0]["WarehouseName"].ToString();
            this.txtContact.Text = dt.Rows[0]["Contact"].ToString();
            this.txtContactPhone.Text = dt.Rows[0]["ContactPhone"].ToString();
            this.txtAddress.Text = dt.Rows[0]["Address"].ToString();
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
        Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID, cnKey);
        DataTable dt = dal.GetRecord(ViewState["StrWhere"].ToString());
        DataRow dr = dt.NewRow();
        dr["WarehouseID"] = this.txtWarehouseID.Text.Trim().ToUpper();
        dr["WarehouseName"] = this.txtWarehouseName.Text;
        dr["Contact"] = this.txtContact.Text.Trim();
        dr["ContactPhone"] = this.txtContactPhone.Text.Trim();
        dr["Address"] = this.txtAddress.Text.Trim();
        dr["Memo"] = this.txtMemo.Text.Trim();
        dr["CreateUserName"] = this.txtCreateUserName.Text;
        dr["CreateDate"] = this.txtCreateDate.Text;
        dr["LastModifyUserName"] = Session["User"].ToString();
        dr["LastModifyDate"] = DateTime.Now.ToString(Js.Com.User.strDateFormat);
        dr["CheckUserName"] = this.txtCheckUserName.Text;
        if (this.txtCheckDate.Text.Length > 0)
            dr["CheckDate"] = this.txtCheckDate.Text;
        dr["StopUserName"] = this.txtStopUserName.Text;
        if (this.txtStopDate.Text.Length > 0)
            dr["StopDate"] = this.txtStopDate.Text;

        if (ID.Length > 0)
            dal.Update(dr, ID);
        else
        {
            dal.Add(dr);
            //插入標籤數量
            Js.BLL.Label.StyleDal sdal = new Js.BLL.Label.StyleDal(cnKey);
            sdal.InsertWarehousePagesByWID(this.txtWarehouseID.Text.Trim().ToUpper(), Session["User"].ToString());
        }

        //Response.Redirect("Departments.aspx?FormID=" + Server.UrlEncode(FormID));
        Response.Redirect("WarehouseView.aspx?FormID=" + Server.UrlEncode(FormID) + "&ID=" + Server.UrlEncode(this.txtWarehouseID.Text));
    }
}