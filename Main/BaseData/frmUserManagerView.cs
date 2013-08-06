using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Main.BaseData
{
    public partial class frmUserManagerView : Form
    {
        string UserName;
        string FormID = "BU_UserManager";
        DataTable dt = new DataTable();
        DataTable dtSub = new DataTable();

        public frmUserManagerView()
        {
            InitializeComponent();
            UserName = "";
        }
        public frmUserManagerView(string UserName)
        {
            InitializeComponent();
            this.UserName = UserName;
        }

        private void frmUserManagerView_Load(object sender, EventArgs e)
        {
            Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID);
            string filter = string.Format("UserName='{0}'", UserName);
            dt = dal.GetRecord(filter);
            BindData();
            this.dataGridView1.AutoGenerateColumns = false;
            dtSub = dal.GetSubDetail(UserName).Tables[0];
            this.dataGridView1.DataSource = dtSub.DefaultView;
        }
        private void BindData()
        {
            if (dt.Rows.Count > 0)
            {
                this.txtUserName.Text = dt.Rows[0]["UserName"].ToString();
                this.txtPersonID.Text = dt.Rows[0]["PersonID"].ToString();
                this.txtPersonName.Text = dt.Rows[0]["PersonName"].ToString();
                this.txtCreateUserName.Text = dt.Rows[0]["CreateUserName"].ToString();
                this.txtCreateDate.Text = dt.Rows[0]["CreateDate"].ToString();
                this.txtLastModifyUserName.Text = dt.Rows[0]["LastModifyUserName"].ToString();
                this.txtLastModifyDate.Text = dt.Rows[0]["LastModifyDate"].ToString();
                this.txtCheckUserName.Text = dt.Rows[0]["CheckUserName"].ToString();
                this.txtCheckDate.Text = dt.Rows[0]["CheckDate"].ToString();                
            }
            if (this.txtCheckUserName.Text.Length > 0)
            {
                this.btnCheck.Text = "取消覆核";
            }
            else
            {
                this.btnCheck.Text = "營運覆核";
            }
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            DataRow dr = dt.Rows[0];
            Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID);

            if (this.txtCheckUserName.Text.Length > 0)
            {
                dr["CheckUserName"] = "";
                dr["CheckDate"] = DBNull.Value;
                this.btnCheck.Text = "營運覆核";
            }
            else
            {
                dr["CheckUserName"] = Js.Com.User.UserID;
                dr["CheckDate"] = DateTime.Now;
                this.btnCheck.Text = "取消覆核";
            }
            dal.Update(dr, this.txtUserName.Text);

            BindData();
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }
    }
}
