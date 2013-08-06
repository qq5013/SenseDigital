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
    public partial class frmPassWordConfirm : Form
    {
        public frmPassWordConfirm()
        {
            InitializeComponent();
        }
        public string strPassWord = "";
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (this.txtPassWord.Text.Length == 0)
            {
                MessageBox.Show("請輸入用戶密碼！", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.txtPassWord.Focus();
                return;
            }
            if (this.txtPassWordConfirm.Text.CompareTo(this.txtPassWord.Text)!=0)
            {
                MessageBox.Show("兩次輸入的密碼不一致，請重新輸入！", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.txtPassWordConfirm.Focus();
                return;
            }

            strPassWord = this.txtPassWord.Text;
            this.DialogResult = DialogResult.OK;

        }

        private void btnCreatUser_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
