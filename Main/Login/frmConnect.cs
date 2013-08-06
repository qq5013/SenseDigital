using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace Main.Login
{
    public partial class frmConnect : Form
    {
        private string DataBase = "SDOPDB";

        public frmConnect()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (txtServer.Text.Trim() == "")
            {
                MessageBox.Show("請輸入資料庫Server名稱﹒然後再試一次﹒", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (rbtDataSQL.Checked)
            {
                if (txtUid.Text.Trim() == "")
                {
                    MessageBox.Show("請輸入資料庫使用者名稱﹒然後再試一次﹒", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

            Js.BLL.Sys.SysComDal bll = new Js.BLL.Sys.SysComDal();
            string strCn = bll.GetConnection(this.txtServer.Text, DataBase, this.txtUid.Text, this.txtPwd.Text, this.rbtDataWind.Checked);
            if (strCn.Length > 0)
            {               
                Js.Com.ConfigHelper.SetKeyValue(System.Windows.Forms.Application.ExecutablePath, "BusinessUnit", Js.Com.DEncrypt.DESEncrypt.Encrypt(strCn));
                
                Js.Com.User.ConnectStringBU = strCn;
                Js.Com.User.ServerBU = txtServer.Text.Trim();
                Js.Com.User.UidBU = txtUid.Text.Trim();
                Js.Com.User.PwdBU = this.txtPwd.Text.Trim();
                Js.Com.User.AuthenticationBU = this.rbtDataWind.Checked;

                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("因在初始化提供者時遭遇錯誤，系統登錄失敗.。", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {            
            if (txtServer.Text.ToLower() == "local" || txtServer.Text == ".")
                txtServer.Text = "(local)";
            Js.BLL.Sys.SysComDal bll = new Js.BLL.Sys.SysComDal();
            if (bll.OpenConnection(this.txtServer.Text, DataBase, this.txtUid.Text, this.txtPwd.Text, this.rbtDataWind.Checked))
            {
                MessageBox.Show("測試連接成功!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("因在初始化提供者時遭遇錯誤，測試連接失敗。", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void frmConnection_Load(object sender, EventArgs e)
        {
            string strCn = Js.Com.DEncrypt.DESEncrypt.Decrypt(Js.Com.ConfigHelper.GetAppConfigValue(Application.ExecutablePath, "BusinessUnit"));
            
            if (Js.Com.ConfigHelper.GetAppConfigValue(Application.ExecutablePath, "LinkedOption") == "1")
                rbtDataSQL.Checked = true;
            else
                rbtDataWind.Checked = true;

            Js.BLL.Sys.SysComDal bll = new Js.BLL.Sys.SysComDal();
            if (bll.OpenConnection(this.txtServer.Text, DataBase, this.txtUid.Text, this.txtPwd.Text, this.rbtDataWind.Checked))
            {
                Js.Com.User.ServerBU = txtServer.Text.Trim();
                Js.Com.User.UidBU = txtUid.Text.Trim();
                Js.Com.User.PwdBU = this.txtPwd.Text.Trim();
                Js.Com.User.AuthenticationBU = this.rbtDataWind.Checked;

                this.DialogResult = DialogResult.OK;
                return;
            }
        }
        private void rbtDataSQL_CheckedChanged(object sender, EventArgs e)
        {
            if (!rbtDataSQL.Checked)
            {
                txtUid.Enabled = false;
                txtPwd.Enabled = false;
            }
            else
            {
                txtUid.Enabled = true;
                txtPwd.Enabled = true;
            }
        }        
    }
}
