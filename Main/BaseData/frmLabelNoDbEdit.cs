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
    public partial class frmLabelNoDbEdit : Form
    {
        string FormID = "BU_EnterpriseDb";
        private bool blnExists = true;
        string DbName = "";
        int intFlag = 1;
        public bool blnCanEdit = false;
        public frmLabelNoDbEdit()
        {
            InitializeComponent();
        }
        public frmLabelNoDbEdit(int Flag)
        {
            InitializeComponent();
            this.DbName = "";
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
        public frmLabelNoDbEdit(int Flag, string EnterpriseID)
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
        private void frmfrmEnterpriseDbEdit_Load(object sender, EventArgs e)
        {
            if (blnCanEdit)
            {
                this.rbtMul.Enabled = true;
                this.rbtSingle.Enabled = true;
                this.rbtSingle.Checked = true;
            }
            else
            {
                this.rbtMul.Enabled = false;
                this.rbtMul.Enabled = false;
                this.rbtMul.Checked = true;
            }

            if (DbName.Length>0)
            {
                Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID);
                string filter = string.Format("DbName='{0}'", DbName);
                DataTable dt = dal.GetRecord(filter);
                BindData(dt);
            }
            else
            {
                
                this.txtLastModifyUserName.Text = Js.Com.User.UserID;
                this.txtLastModifyDate.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm");
                this.txtCreateUserName.Text = Js.Com.User.UserID;
                this.txtCreateDate.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm");
            }
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
            }
        }

      

        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
           
            if (this.txtSQLServer.Text.Trim() == "")
            {
                MessageBox.Show("請輸入SQL Server名稱!", "存檔", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.txtSQLServer.Focus();
                return;
            }
            Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID);
            if (DbName.Length > 0 && this.txtSQLServer.Text.Trim() != DbName || DbName.Length <= 0)
            {
                if (dal.Exists(this.txtSQLServer.Text.Trim()))
                {
                    MessageBox.Show("此標籤服務器已經存在,請選取其他標籤服務器。!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

            Js.BLL.Sys.SysLabelNoDb SysComDal = new Js.BLL.Sys.SysLabelNoDb();
            if (!SysComDal.OpenConnection(this.txtSQLServer.Text, "SDLabelDB", "", "", true))
            {
                MessageBox.Show("因在初始化提供者時遭遇錯誤，測試連接失敗。", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (rbtMul.Checked)
            {
                if (this.txtGroupStartNo.Text.Trim() == "" || this.txtGroupStartNo.Text.Trim().Length!=20)
                {
                    MessageBox.Show("起始群組編號不能為空，請輸入!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (this.txtGroupEndNo.Text.Trim() == "" || this.txtGroupEndNo.Text.Trim().Length != 20)
                {
                    MessageBox.Show("終止群組編號不能為空，請輸入!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                try
                {
                    int GroupStartNo=Convert.ToInt32(this.txtGroupStartNo.Text.Trim().Substring(11,9));
                    int GroupEndNo = Convert.ToInt32(this.txtGroupEndNo.Text.Trim().Substring(11, 9));

                    if (!GetBLNBetWeen(SysComDal, GroupStartNo, GroupEndNo))
                    {
                        MessageBox.Show("群組編號區間不能重複，請重新輸入!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                catch
                {
                    MessageBox.Show("群組編號輸入有誤，請重新輸入!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

            }
            DataTable dt = dal.GetRecord("1=1");
            DataRow dr = dt.NewRow();
            dr["Flag"] = intFlag;
            dr["DbName"] = this.txtSQLServer.Text.Trim();
            dr["Single"] = this.rbtSingle.Checked;
            if (this.rbtSingle.Checked)
            {
                dr["GroupStartNo"] = "";
                dr["GroupEndNo"] = "";
            }
            else
            {
                dr["GroupStartNo"] = this.txtGroupStartNo.Text.Trim();
                dr["GroupEndNo"] = this.txtGroupEndNo.Text.Trim();
            }
           
            dr["Memo"] = this.txtMemo.Text.Trim();
            dr["CreateUserName"] = Js.Com.User.UserID;
            if (DbName.Length > 0)
                dr["CreateDate"] = this.txtCreateDate.Text;
            else
                dr["CreateDate"] = DateTime.Now.ToString("yyyy/MM/dd HH:mm");
            dr["LastModifyUserName"] = Js.Com.User.UserID;
            dr["LastModifyDate"] = DateTime.Now.ToString("yyyy/MM/dd HH:mm");
            dr["CheckUserName"] = this.txtCheckUserName.Text;
            if (this.txtCheckDate.Text.Length > 0)
                dr["CheckDate"] = this.txtCheckDate.Text;

            if (DbName.Length > 0)
                dal.Update(dr, DbName);
            else
                dal.Add(dr);

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

       

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (this.txtSQLServer.Text.Trim() == "")
            {
                MessageBox.Show("請輸入SQL Server名稱!", "存檔", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.txtSQLServer.Focus();
                return;
            }

            Js.BLL.Sys.SysComDal dal = new Js.BLL.Sys.SysComDal();
            if (dal.OpenConnection(this.txtSQLServer.Text, "SDLabelDB", "", "", true))
                MessageBox.Show("測試連接成功!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("因在初始化提供者時遭遇錯誤，測試連接失敗。", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void rbtSingle_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rbt = sender as RadioButton;
            if (rbt.Name == "rbtSingle" && rbt.Checked)
            {
                this.txtGroupEndNo.Enabled = false;
                this.txtGroupStartNo.Enabled = false;
            }
            else if (rbt.Name == "rbtMul" && rbt.Checked)
            {
                this.txtGroupEndNo.Enabled = true;
                this.txtGroupStartNo.Enabled = true;
            }
        }

        private bool GetBLNBetWeen(Js.BLL.Sys.SysLabelNoDb dal, int strGroupStartNo, int strGroupEndNo)
        {
            bool blnvalue = true;
            DataTable dt = dal.GetAllTable(intFlag.ToString());

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                int GroupStartNo = Convert.ToInt32(dt.Rows[i][0].ToString().Substring(11,9));
                int GroupEndNo = Convert.ToInt32(dt.Rows[i][1].ToString().Substring(11,9));
                if (strGroupStartNo > GroupStartNo && strGroupStartNo < GroupEndNo)
                {
                    blnvalue = false;
                    break;
                }
                if (strGroupEndNo > GroupStartNo && strGroupEndNo < GroupEndNo)
                {
                    blnvalue = false;
                    break;
                }
            }


            return blnvalue;
        }
    }
}
