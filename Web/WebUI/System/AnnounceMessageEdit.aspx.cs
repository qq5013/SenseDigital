using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class WebUI_System_AnnounceMessageEdit : BasePage
{
    protected string FormID;

    protected void Page_Load(object sender, EventArgs e)
    {
        ID = Request.QueryString["ID"] + "";
        FormID = Request.QueryString["FormID"] + "";
        if (!IsPostBack)
        {
            ViewState["StrWhere"] = string.Format(" AnnounceID='{0}'", ID);
            if (Session["UserType"].ToString() == "BU")
            {
                this.txtAnnounceFlag.Text = Resources.Resource.AnnounceFlag1;
                Js.BLL.BusinessUnit.CompanyDal cdal = new Js.BLL.BusinessUnit.CompanyDal();
                Js.Model.BusinessUnit.CompanyInfo model = cdal.GetModel();
                this.txtAnnounceUnitNo.Text = model.CompanyNo;
                Js.BLL.Account.UserDal udal = new Js.BLL.Account.UserDal();
                Js.Model.Account.UsersInfo umodel = udal.GetModel(Session["User"].ToString());
                this.txtAnnouncer.Text = umodel.PersonName;
            }
            else
            {
                this.txtAnnounceFlag.Text = Resources.Resource.AnnounceFlag2;
                this.txtAnnounceUnitNo.Text = Session["EnterpriseID"].ToString();

                Js.BLL.Account.UserDal udal = new Js.BLL.Account.UserDal(Session["EnterpriseID"].ToString());
                Js.Model.Account.UsersInfo umodel = udal.GetModel(Session["User"].ToString());
                this.txtAnnouncer.Text = umodel.PersonName;
            }

            Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID);
            string strWhere = " AnnounceID like '" + this.txtAnnounceUnitNo.Text + DateTime.Now.ToString("yyyyMMdd") + "%'";
            dal.GetMaxID(strWhere);

            this.txtAnnounceID.Text = dal.GetMaxID(strWhere);
            if(this.txtAnnounceID.Text.Length<=0)
                this.txtAnnounceID.Text =this.txtAnnounceUnitNo.Text + DateTime.Now.ToString("yyyyMMdd") + "0001";
            this.txtAnnouncerUserName.Text = Session["User"].ToString();
            this.txtSource.Text = Resources.Resource.AnnounceSource1;

            BindGrid();
        }
    }
    private void BindGrid()
    {
        Js.BLL.BaseDal bdal = new Js.BLL.BaseDal(FormID);
        DataTable dtSub = bdal.GetSubDetail("").Tables[0];
        
        Js.BLL.Account.UserDal dal = new Js.BLL.Account.UserDal();

        if (Session["UserType"].ToString() == "BU")
        {
            //營運用戶
            DataTable dt = dal.GetAllUsers().Tables[0];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["UserName"].ToString() != Session["User"].ToString())
                {
                    DataRow dr = dtSub.NewRow();
                    dr["AnnounceID"] = this.txtAnnounceID.Text;
                    dr["ReceiverUserName"] = dt.Rows[i]["UserName"];
                    dr["ReceiverFlag"] = 0;
                    Js.BLL.BusinessUnit.CompanyDal cdal = new Js.BLL.BusinessUnit.CompanyDal();
                    Js.Model.BusinessUnit.CompanyInfo model = cdal.GetModel();

                    dr["ReceiveUnitNo"] = model.CompanyNo;
                    dr["Receiver"] = dt.Rows[i]["PersonName"];

                    dtSub.Rows.Add(dr);
                }
            }
            //各企業用戶
            DataTable dtEP = dal.GetEnterprise();
            for (int i = 0; i < dtEP.Rows.Count; i++)
            {
                Js.BLL.Account.UserDal edal = new Js.BLL.Account.UserDal(dtEP.Rows[i]["EnterpriseID"].ToString());
                DataTable dtUser = edal.GetAllUsers().Tables[0];
                for (int j = 0; j < dtUser.Rows.Count; j++)
                {
                    DataRow dr = dtSub.NewRow();
                    dr["AnnounceID"] = this.txtAnnounceID.Text;
                    dr["ReceiverUserName"] = dtUser.Rows[j]["UserName"];
                    dr["ReceiverFlag"] = 1;
                    dr["ReceiveUnitNo"] = dtEP.Rows[i]["EnterpriseID"].ToString();
                    dr["Receiver"] = dtUser.Rows[j]["PersonName"];

                    dtSub.Rows.Add(dr);
                }
            }
        }
        else
        {
            Js.BLL.Account.UserDal edal = new Js.BLL.Account.UserDal(Session["EnterpriseID"].ToString());
            DataTable dtUser = edal.GetAllUsers().Tables[0];
            for (int i = 0; i < dtUser.Rows.Count; i++)
            {
                if (dtUser.Rows[i]["UserName"].ToString() != Session["User"].ToString())
                {
                    DataRow dr = dtSub.NewRow();
                    dr["AnnounceID"] = this.txtAnnounceID.Text;
                    dr["ReceiverUserName"] = dtUser.Rows[i]["UserName"];
                    dr["ReceiverFlag"] = 1;
                    dr["ReceiveUnitNo"] = Session["EnterpriseID"].ToString();
                    dr["Receiver"] = dtUser.Rows[i]["PersonName"];

                    dtSub.Rows.Add(dr);
                }
            }
            //營運管理用戶
            Js.BLL.Enterprise.CheckDal checkdal = new Js.BLL.Enterprise.CheckDal();
            dtUser = checkdal.GetManagerUser(Session["EnterpriseID"].ToString());

            Js.BLL.BusinessUnit.CompanyDal cdal = new Js.BLL.BusinessUnit.CompanyDal();
            Js.Model.BusinessUnit.CompanyInfo model = cdal.GetModel();

            for (int i = 0; i < dtUser.Rows.Count; i++)
            {
                DataRow dr = dtSub.NewRow();
                dr["AnnounceID"] = this.txtAnnounceID.Text;
                dr["ReceiverUserName"] = dtUser.Rows[i]["UserName"];
                dr["ReceiverFlag"] = 0;
                dr["ReceiveUnitNo"] = model.CompanyNo;
                dr["Receiver"] = dtUser.Rows[i]["PersonName"];

                dtSub.Rows.Add(dr);
            }
        }
        this.GridView1.DataSource = dtSub.DefaultView;
        this.GridView1.DataBind();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID);
        DataTable dt = dal.GetRecord(ViewState["StrWhere"].ToString());
        DataRow dr = dt.NewRow();
        dr["AnnounceID"] = this.txtAnnounceID.Text;
        dr["Announcer"] = this.txtAnnouncer.Text;
        if (this.txtAnnounceFlag.Text == Resources.Resource.AnnounceFlag1)
            dr["AnnounceFlag"] = 0;
        else
            dr["AnnounceFlag"] = 1;
        dr["AnnounceUnitNo"] = this.txtAnnounceUnitNo.Text;
        dr["AnnouncerUserName"] = this.txtAnnouncerUserName.Text;

        if (this.txtSource.Text == Resources.Resource.AnnounceSource1)
            dr["Source"] = 0;
        else
            dr["Source"] = 1;
        dr["AnnounceDate"] = DateTime.Now;
        dr["Title"] = this.txtTitle.Text.Trim();
        dr["Contents"] = this.txtContents.Text.Trim();
        
        dal.Add(dr);

        DataTable dtSub = dal.GetSubDetail("").Tables[0];
       
        for (int i = 0; i < this.GridView1.Rows.Count; i++)
        {
            CheckBox cb = (CheckBox)(this.GridView1.Rows[i].FindControl("cbSelect"));
            if (cb.Checked)
            {
                DataRow subdr = dtSub.NewRow();
                subdr["AnnounceID"] = this.txtAnnounceID.Text;
                subdr["ReceiverUserName"] = this.GridView1.Rows[i].Cells[1].Text;
                if (this.GridView1.Rows[i].Cells[2].Text == Resources.Resource.AnnounceFlag1)
                    subdr["ReceiverFlag"] = 0;
                else
                    subdr["ReceiverFlag"] = 1;
                subdr["ReceiveUnitNo"] = this.GridView1.Rows[i].Cells[3].Text;
                subdr["Receiver"] = this.GridView1.Rows[i].Cells[4].Text;

                dtSub.Rows.Add(subdr);
            }
        }

        dal.SaveDetail(dtSub, "");

        Response.Redirect("AnnounceMessageView.aspx?FormID=" + Server.UrlEncode(FormID) + "&ID=" + Server.UrlEncode(this.txtAnnounceID.Text));

    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            switch (e.Row.Cells[2].Text.ToString())
            {
                case "0":
                    e.Row.Cells[2].Text = Resources.Resource.AnnounceFlag1;
                    break;
                case "1":
                    e.Row.Cells[2].Text = Resources.Resource.AnnounceFlag2;
                    break;
            }
        }
    }
}