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
    public partial class frmLabelNoDbView : Form
    {
        string FormID = "BU_EnterpriseDb";
        private bool blnExists = true;
        string DbName = "";
        int intFlag = 1;

        DataTable dt = null;
         public frmLabelNoDbView()
        {
            InitializeComponent();
        }
        public frmLabelNoDbView(int Flag, string EnterpriseID)
        {
            InitializeComponent();
            this.DbName = EnterpriseID;
            intFlag = Flag;
            if (Flag == 1)
            {
                FormID = "BU_LabelNoDb";
                this.Text = "標籤序號主機設定";
            }
            else
            {
                FormID = "BU_LabelPicDb";
                 this.Text = "標籤存證圖片主機設定";
            }
            
        }

        private void frmLabelNoDbView_Load(object sender, EventArgs e)
        {
            Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID);
            string filter = string.Format("DbName='{0}'", DbName);
           dt = dal.GetRecord(filter);
            BindData(dt);
        }
        private void BindData(DataTable dt)
        {
            if (dt.Rows.Count > 0)
            {

                this.rbtSingle.Checked = Convert.ToBoolean(dt.Rows[0]["Single"]);
                this.rbtMul.Checked = !Convert.ToBoolean(dt.Rows[0]["Single"]);
                this.txtSQLServer.Text = dt.Rows[0]["DbName"].ToString();
                this.txtMemo.Text = dt.Rows[0]["Memo"].ToString();
                this.txtGroupStartNo.Text = dt.Rows[0]["GroupStartNo"].ToString();
                this.txtGroupEndNo.Text = dt.Rows[0]["GroupEndNo"].ToString();
                this.txtCreateUserName.Text = dt.Rows[0]["CreateUserName"].ToString();
                this.txtCreateDate.Text = dt.Rows[0]["CreateDate"].ToString();
                this.txtLastModifyUserName.Text = dt.Rows[0]["LastModifyUserName"].ToString();
                this.txtLastModifyDate.Text = dt.Rows[0]["LastModifyDate"].ToString();
                this.txtCheckUserName.Text = dt.Rows[0]["CheckUserName"].ToString();
                this.txtCheckDate.Text = dt.Rows[0]["CheckDate"].ToString();
                if (this.txtCheckUserName.Text.Length > 0)
                {
                    this.btnCheck.Text = "取消覆核";
                }
                else
                {
                    this.btnCheck.Text = "營運覆核";
                }
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
            dal.Update(dr, this.txtSQLServer.Text);

            BindData(dt);
        }


        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }
        
       

    }
}
