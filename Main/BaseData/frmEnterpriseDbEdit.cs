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
    public partial class frmEnterpriseDbEdit : Form
    {
        string EnterpriseID = "";
        string FormID = "BU_EnterpriseDb";
        private bool blnExists = true;

        public frmEnterpriseDbEdit()
        {
            InitializeComponent();
            EnterpriseID = "";
        }
        public frmEnterpriseDbEdit(string EnterpriseID)
        {
            InitializeComponent();
            this.EnterpriseID = EnterpriseID;
        }

        private void frmfrmEnterpriseDbEdit_Load(object sender, EventArgs e)
        {
            BindComboBox();
            if (EnterpriseID.Length>0)
            {
                Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID);
                string filter = string.Format("EnterpriseID='{0}'", EnterpriseID);
                DataTable dt = dal.GetRecord(filter);
                BindData(dt);
            }
            else
            {
                this.txtState.Text = "未開立";
                this.txtLastModifyUserName.Text = Js.Com.User.UserID;
                this.txtLastModifyDate.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm");
                this.txtCreateUserName.Text = Js.Com.User.UserID;
                this.txtCreateDate.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm");
            }
        }
        private void BindComboBox()
        {
            Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID);
            DataTable dt = dal.GetDistinctRecord("ManageGroup");
            this.cmbManageGroup.DataSource = dt;
            this.cmbManageGroup.DisplayMember = "ManageGroup";
            this.cmbManageGroup.ValueMember = "ManageGroup";
        }
        private void BindData(DataTable dt)
        {
            if (dt.Rows.Count > 0)
            {
                this.txtEnterpriseID.Text = dt.Rows[0]["EnterpriseID"].ToString();
                this.txtEnterpriseName.Text = dt.Rows[0]["EnterpriseName"].ToString();
                this.txtSQLServer.Text = dt.Rows[0]["SQLServer"].ToString();
                this.txtDbName.Text = dt.Rows[0]["DbName"].ToString();
                if (dt.Rows[0]["State"].ToString() == "0")
                    this.txtState.Text = "未開立";
                else
                {
                    this.txtState.Text = "開立";
                    this.txtDbDataPath.ReadOnly = true;
                    this.btnBrowse.Enabled = false;
                }
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
            }
        }

        private void txtEnterpriseID_DoubleClick(object sender, EventArgs e)
        {
            Sys.frmSelect f = new Sys.frmSelect(false, "BU_Enterprise", " EnterpriseID not in (select distinct EnterpriseID from Com_EnterpriseDb) ");
            f.ShowDialog();
            if (f.DialogResult == DialogResult.OK)
            {
                if (f.dtSelect.Rows.Count > 0)
                {
                    this.txtEnterpriseID.Text = f.dtSelect.Rows[0][0].ToString();
                    this.txtEnterpriseName.Text = f.dtSelect.Rows[0][1].ToString();
                }
            }
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (this.txtEnterpriseID.Text.Trim() == "")
            {
                MessageBox.Show("請輸入企業編號!", "存檔", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.txtEnterpriseID.Focus();
                return;
            }
            if (this.txtSQLServer.Text.Trim() == "")
            {
                MessageBox.Show("請輸入SQL Server名稱!", "存檔", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.txtSQLServer.Focus();
                return;
            }
            if (this.txtDbDataPath.Text.Trim() == "")
            {
                MessageBox.Show("請輸入資料庫存放路徑!", "存檔", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.txtDbDataPath.Focus();
                return;
            }

            //Js.BLL.Sys.SysComDal sdal = new Js.BLL.Sys.SysComDal();
            //if (!sdal.OpenConnection(this.txtSQLServer.Text, this.txtDbName.Text, "", "", true))
            //{
            //    MessageBox.Show("因在初始化提供者時遭遇錯誤，測試連接失敗。", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}
            if(!System.IO.Directory.Exists(this.txtDbDataPath.Text.Trim()))
            {
                MessageBox.Show("資料庫路徑不存在或無法存取，請確認！", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID);
            if (EnterpriseID.Length > 0 && this.txtEnterpriseID.Text.Trim() != EnterpriseID || EnterpriseID.Length <= 0)
            {
                if (dal.Exists(this.txtEnterpriseID.Text.Trim()))
                {
                    MessageBox.Show("此企業編號已經存在,請選取其他企業編號!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            if (!blnExists)
            {
                MessageBox.Show("此企業編號不存在,請選取其他企業編號!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            Js.BLL.Sys.SysComDal SysComDal = new Js.BLL.Sys.SysComDal();
            if (!SysComDal.OpenConnection(this.txtSQLServer.Text, "master", "", "", true))
            {
                MessageBox.Show("因在初始化提供者時遭遇錯誤，測試連接失敗。", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            DataTable dt = dal.GetRecord("1=1");
            DataRow dr = dt.NewRow();
            dr["EnterpriseID"] = this.txtEnterpriseID.Text.Trim();
            dr["EnterpriseName"] = this.txtEnterpriseName.Text.Trim();
            dr["SQLServer"] = this.txtSQLServer.Text.Trim();
            dr["DbDataPath"] = this.txtDbDataPath.Text.Trim();
            dr["DbName"] = "SD" + this.txtEnterpriseID.Text.Trim();
            dr["UserName"] = "supervisor";
            //if (this.txtState.Text == "未啟用")
            dr["State"] = 0;
            //else
            //    dr["State"] = 1;
            dr["ManageGroup"] = this.cmbManageGroup.Text;
            dr["Memo"] = this.txtMemo.Text.Trim();
            dr["CreateUserName"] = Js.Com.User.UserID;
            if (EnterpriseID.Length > 0)
                dr["CreateDate"] = this.txtCreateDate.Text;
            else
                dr["CreateDate"] = DateTime.Now.ToString("yyyy/MM/dd HH:mm");
            dr["LastModifyUserName"] = Js.Com.User.UserID;
            dr["LastModifyDate"] = DateTime.Now.ToString("yyyy/MM/dd HH:mm");
            dr["CheckUserName"] = this.txtCheckUserName.Text;
            if (this.txtCheckDate.Text.Length > 0)
                dr["CheckDate"] = this.txtCheckDate.Text;

            if (EnterpriseID.Length > 0)
                dal.Update(dr, EnterpriseID);
            else
                dal.Add(dr);

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            if (this.folderBrowserDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.txtDbDataPath.Text = this.folderBrowserDialog1.SelectedPath;
            }
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (this.txtEnterpriseID.Text.Trim() == "")
            {
                MessageBox.Show("請輸入企業編號!", "存檔", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.txtEnterpriseID.Focus();
                return;
            }
            if (this.txtSQLServer.Text.Trim() == "")
            {
                MessageBox.Show("請輸入SQL Server名稱!", "存檔", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.txtSQLServer.Focus();
                return;
            }

            Js.BLL.Sys.SysComDal dal = new Js.BLL.Sys.SysComDal();
            if (dal.OpenConnection(this.txtSQLServer.Text, "master", "", "", true))
                MessageBox.Show("測試連接成功!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("因在初始化提供者時遭遇錯誤，測試連接失敗。", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void txtEnterpriseID_TextChanged(object sender, EventArgs e)
        {
            this.txtEnterpriseID.Text = this.txtEnterpriseID.Text.Trim();
            Js.BLL.BaseDal dal = new Js.BLL.BaseDal("BU_Enterprise");
            string filter = string.Format("EnterpriseID='{0}'", this.txtEnterpriseID.Text);
            DataTable dt = dal.GetViewRecord(filter);
            if (dt.Rows.Count > 0)
            {

                this.txtEnterpriseName.Text = dt.Rows[0]["EnterpriseName"].ToString();
                filter = string.Format("EnterpriseID='{0}' and EnterpriseID not in (select distinct EnterpriseID from Com_EnterpriseDb)", this.txtEnterpriseID.Text);
                dt = dal.GetViewRecord(filter);
                if (dt.Rows.Count == 0 && EnterpriseID.Length==0)
                {
                    blnExists = false;
                    this.txtEnterpriseName.Text = "";
                    MessageBox.Show("該企業編號已經建立數據庫，請輸入正確的企業編號！", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else 
            {
                blnExists = false;
                MessageBox.Show("企業編號不存在，請輸入正確的企業編號！",Application.ProductName,MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
        }
    }
}
