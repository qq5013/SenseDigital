using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;

public partial class WebUI_BusinessUnit_EnterpriseEdit : BasePage
{
    protected string FormID;
    protected string ID;
    protected void Page_Load(object sender, EventArgs e)
    {
        ID = Request.QueryString["ID"] + "";
        FormID = Request.QueryString["FormID"] + "";
        if (!IsPostBack)
        {
            ViewState["StrWhere"] = string.Format(" EnterpriseID='{0}'", ID);
            Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID);
            BindDropDownList();
            if (ID != "")
            {
                DataTable dt = dal.GetRecord(ViewState["StrWhere"].ToString());
                BindData(dt);
                this.txtEnterpriseID.ReadOnly = true;
            }
            else
            {
                this.txtEnterpriseID.Text = dal.GetMaxID();
                this.txtLastModifyUserName.Text = Session["User"].ToString();
                this.txtLastModifyDate.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm");
                this.txtCreateUserName.Text = Session["User"].ToString();
                this.txtCreateDate.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm");
            }
        }
    }
    private void BindDropDownList()
    {
        Js.BLL.BaseDal dal = new Js.BLL.BaseDal("BU_EnterpriseCategory");
        DataTable dt = dal.GetIDNameList("");
        this.ddlCategoryID.DataSource = dt;
        this.ddlCategoryID.DataTextField = "IDName";
        this.ddlCategoryID.DataValueField = "ID";
        this.ddlCategoryID.DataBind();
    }
    private void BindData(DataTable dt)
    {

        if (dt.Rows.Count > 0)
        {
            this.txtEnterpriseID.Text = dt.Rows[0]["EnterpriseID"].ToString();
            this.ddlCategoryID.SelectedValue = dt.Rows[0]["CategoryID"].ToString();
            this.txtEnterpriseName.Text = dt.Rows[0]["EnterpriseName"].ToString();
            this.txtEnterpriseEName.Text = dt.Rows[0]["EnterpriseEName"].ToString();
            this.txtEnterpriseSName.Text = dt.Rows[0]["EnterpriseSName"].ToString();
            this.txtUnionID.Text = dt.Rows[0]["UnionID"].ToString();
            this.txtPresident.Text = dt.Rows[0]["President"].ToString();
            this.txtPhone.Text = dt.Rows[0]["Phone"].ToString();
            this.txtFax.Text = dt.Rows[0]["Fax"].ToString();
            this.txtWebUrl.Text = dt.Rows[0]["WebUrl"].ToString();
            this.txtServiceYears.Text = dt.Rows[0]["ServiceYears"].ToString();
            this.ddlEnableMonths.Text = dt.Rows[0]["EnableMonths"].ToString();

            this.txtAddress.Text = dt.Rows[0]["Address"].ToString();
            this.txtZipNo.Text = dt.Rows[0]["ZipNo"].ToString();
            this.txtMemo.Text = dt.Rows[0]["Memo"].ToString();
            this.txtCreateUserName.Text = dt.Rows[0]["CreateUserName"].ToString();
            this.txtCreateDate.Text = dt.Rows[0]["CreateDate"].ToString();
            this.txtLastModifyUserName.Text = dt.Rows[0]["LastModifyUserName"].ToString();
            this.txtLastModifyDate.Text = dt.Rows[0]["LastModifyDate"].ToString();
            this.txtCheckUserName.Text = dt.Rows[0]["CheckUserName"].ToString();
            this.txtCheckDate.Text = dt.Rows[0]["CheckDate"].ToString();

            Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID);
            DataTable dtSub = dal.GetSubDetail(ID).Tables[0];
            string sdtSub = Js.Com.JsonHelper.Dtb2Json(dtSub);
            this.HdnSubDetail1.Value = sdtSub;
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID);
        

        DataTable dt = dal.GetRecord(ViewState["StrWhere"].ToString());
        DataRow dr = dt.NewRow();
        dr["EnterpriseID"] = this.txtEnterpriseID.Text;
        dr["CategoryID"] = this.ddlCategoryID.SelectedValue.ToString();
        dr["EnterpriseName"] = this.txtEnterpriseName.Text;
        dr["EnterpriseEName"] = this.txtEnterpriseEName.Text;
        dr["EnterpriseSName"] = this.txtEnterpriseSName.Text;
        dr["UnionID"] = this.txtUnionID.Text;
        //dr["LabelFrom"] = this.rbtLabelFromYes.Checked;
        dr["President"] = this.txtPresident.Text;
        //dr["PresidentPost"] = this.txtPresidentPost.Text;
        dr["Phone"] = this.txtPhone.Text;
        dr["Fax"] = this.txtFax.Text;
        //dr["Contact"] = this.txtContact.Text;
        //dr["ContactPost"] = this.txtContactPost.Text;
        //dr["ContactPhone"] = this.txtContactPhone.Text;
        //dr["CellPhone"] = this.txtCellPhone.Text;
        //dr["Email"] = this.txtEmail.Text;
        dr["WebUrl"] = this.txtWebUrl.Text;
        dr["Address"] = this.txtAddress.Text;
        dr["ZipNo"] = this.txtZipNo.Text;
        
        byte ServiceYears;
        byte.TryParse(this.txtServiceYears.Text.Trim(), out ServiceYears);
        this.txtServiceYears.Text = ServiceYears.ToString();
        dr["ServiceYears"] = ServiceYears;
        dr["EnableMonths"] = byte.Parse(this.ddlEnableMonths.Text);
        dr["Memo"] = this.txtMemo.Text.Trim();
        dr["CreateUserName"] = this.txtCreateUserName.Text;
        dr["CreateDate"] = this.txtCreateDate.Text;
        dr["LastModifyUserName"] = Session["User"].ToString();
        dr["LastModifyDate"] = DateTime.Now.ToString(Js.Com.User.strDateFormat);
        dr["CheckUserName"] = this.txtCheckUserName.Text;
        if (this.txtCheckDate.Text.Length > 0)
            dr["CheckDate"] = this.txtCheckDate.Text;


        if (ID.Length > 0)
        {
            Js.BLL.BusinessUnit.EnterpriseDal edal = new Js.BLL.BusinessUnit.EnterpriseDal();
            Js.Model.BusinessUnit.EnterpriseInfo model = edal.GetModel(ID);
            if (ServiceYears != model.ServiceYears || this.ddlEnableMonths.Text != model.EnableMonths.ToString())
            {
                edal.InsertModifyRecord(ID, ServiceYears, byte.Parse(this.ddlEnableMonths.Text));
            }
            dal.Update(dr, ID);

        }
        else
            dal.Add(dr);

        ////同步更新企業庫裡的這筆資料
        //Js.BLL.BaseDal edal = new Js.BLL.BaseDal("EP_Enterprise", this.txtEnterpriseID.Text);
        //if (edal.Exists(this.txtEnterpriseID.Text))
        //    edal.Update(dr,ID);
        //else
        //    edal.Add(dr);

        DataTable dtSub1 = dal.GetSubDetail("").Tables[0];

        DataTable newdtb = Js.Com.JsonHelper.Json2Dtb(this.HdnSubDetail1.Value);
        for (int i = 0; i < newdtb.Rows.Count; i++)
        {
            DataRow subdr = dtSub1.NewRow();
            subdr["EnterpriseID"] = this.txtEnterpriseID.Text;
            for (int j = 0; j < newdtb.Columns.Count; j++)
            {
                subdr[newdtb.Columns[j].ColumnName] = newdtb.Rows[i][j];
            }
            if (ID.Length > 0)
            {
                subdr["LastModifyDate"] = DateTime.Now.ToString(Js.Com.User.strDateFormat);
                subdr["LastModifyUserName"] = Session["User"].ToString();
            }
            else
            {
                subdr["CreateUserName"] = this.txtCreateUserName.Text;
                subdr["CreateDate"] = this.txtCreateDate.Text;
                subdr["LastModifyDate"] = DateTime.Now.ToString(Js.Com.User.strDateFormat);
                subdr["LastModifyUserName"] = Session["User"].ToString();
            }
            dtSub1.Rows.Add(subdr);
        }
        dal.SaveDetail(dtSub1, ID);

        //dtSub1.TableName = "EP_EnterpriseLinkMan";
        //edal.SaveDetail(dtSub1, ID);
        
        Response.Redirect("EnterpriseView.aspx?FormID=" + Server.UrlEncode(FormID) + "&ID=" + Server.UrlEncode(this.txtEnterpriseID.Text));

    }
}