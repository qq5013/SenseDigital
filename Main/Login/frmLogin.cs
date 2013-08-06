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
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }
        private void frmLogin_Load(object sender, EventArgs e)
        {
            txtUserName.Text = Js.Com.ConfigHelper.GetAppConfigValue(Application.ExecutablePath, "User");
            this.Show();
            if (txtUserName.Text != "")
                txtPass.Focus();
            else
                txtUserName.Focus();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string userName = Js.Com.PageValidate.InputText(txtUserName.Text.Trim(), 30);
            string Password = Js.Com.PageValidate.InputText(txtPass.Text.Trim(), 30);

            Js.BLL.Account.UserPrincipal newUser = Js.BLL.Account.UserPrincipal.ValidateLogin(userName, Password);
            if (newUser == null)
            {
                MessageBox.Show(userName + " 登錄失敗,請檢查用戶名密碼是否正確!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                //記錄使用者
                Js.Com.ConfigHelper.SetKeyValue(Application.ExecutablePath, "User", userName);
                Js.Com.User.UserID = userName;
                this.DialogResult = DialogResult.OK;
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }        
    }
}
