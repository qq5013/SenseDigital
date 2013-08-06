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
    public partial class frmEnterpriseDbView : Form
    {
        string EnterpriseID;
        string FormID = "BU_EnterpriseDb";
        DataTable dt = new DataTable();
        public frmEnterpriseDbView()
        {
            InitializeComponent();
        }
        public frmEnterpriseDbView(string EnterpriseID)
        {
            InitializeComponent();
            this.EnterpriseID = EnterpriseID;
        }

        private void frmfrmEnterpriseDbView_Load(object sender, EventArgs e)
        {
            BindComboBox();
            Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID);
            string filter = string.Format("EnterpriseID='{0}'",EnterpriseID);
            dt = dal.GetRecord(filter);
            BindData();
        }
        private void BindComboBox()
        {
            Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID);
            DataTable dt = dal.GetDistinctRecord("ManageGroup");
            this.cmbManageGroup.DataSource = dt;
            this.cmbManageGroup.DisplayMember = "ManageGroup";
            this.cmbManageGroup.ValueMember = "ManageGroup";
        }
        private void BindData()
        {
            if (dt.Rows.Count > 0)
            {
                this.txtEnterpriseID.Text = dt.Rows[0]["EnterpriseID"].ToString();
                this.txtEnterpriseName.Text = dt.Rows[0]["EnterpriseName"].ToString();
                this.txtSQLServer.Text = dt.Rows[0]["SQLServer"].ToString();
                this.txtDbName.Text = dt.Rows[0]["DbName"].ToString();
                if(dt.Rows[0]["State"].ToString()=="0")
                    this.txtState.Text = "未開立";
                else
                    this.txtState.Text = "開立";
                this.txtDbDataPath.Text = dt.Rows[0]["DbDataPath"].ToString();
                this.txtUserName.Text = dt.Rows[0]["UserName"].ToString();
                this.cmbManageGroup.Text = dt.Rows[0]["ManageGroup"].ToString();
                this.txtMemo.Text = dt.Rows[0]["Memo"].ToString();
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
            dal.Update(dr, this.txtEnterpriseID.Text);

            BindData();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (this.txtState.Text == "開立")
            {
                Js.BLL.Sys.SysComDal dal = new Js.BLL.Sys.SysComDal();
                if (dal.OpenConnection(this.txtSQLServer.Text, this.txtDbName.Text, "", "", true))
                    MessageBox.Show("測試連接成功!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("因在初始化提供者時遭遇錯誤，測試連接失敗。", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else 
            {
                MessageBox.Show("資料庫未開立，請在開立資料庫后測試連接。", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        /// <summary>
        /// 開立資料庫
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (this.txtCheckUserName.Text == "")
            {
                MessageBox.Show("請先營運覆核后，再開立資料庫。", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            Js.BLL.Sys.SysComDal dal = new Js.BLL.Sys.SysComDal();
            string strPath = Application.StartupPath + @"\SQL\CreateDataBase.SQL";
            string dbPath = Js.Com.ConfigHelper.GetAppConfigValue(Application.ExecutablePath, "DbPath");
            if (dal.CreateDataBase(this.txtSQLServer.Text, txtDbName.Text,dbPath, strPath))
            {
                MessageBox.Show("資料庫開立成功！", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

        }
        /// <summary>
        /// 創建用戶
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCreatUser_Click(object sender, EventArgs e)
        {
            string CnKey = txtEnterpriseID.Text.Trim();
            Js.BLL.Account.UserDal dal = new Js.BLL.Account.UserDal(CnKey);
            if (dal.HasUser("supervisor") > 0)
            {
                MessageBox.Show("用戶《supervisor》已經創建！", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                frmPassWordConfirm frm = new frmPassWordConfirm();
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    string strPassword = frm.strPassWord;

                    Js.Model.Account.UsersInfo model = new Js.Model.Account.UsersInfo();
                    model.UserName = "supervisor";
                    model.Password = Encoding.Default.GetBytes(strPassword);
                    model.UserLevel = 1000;
                    model.State = 1;
                    dal.Add(model);
                    MessageBox.Show("用戶《supervisor》創建成功！", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
