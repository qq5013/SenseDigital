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
    public partial class frmUserManagers : Form
    {
        string FormID = "BU_UserManager";
        int PageSize = 30;
        int CurrentPage = 1;
        string strIniWhere = "1=1";
        string strWhere = "1=1";

        DataTable dtData;

        public frmUserManagers()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 綁定查詢
        /// </summary>
        private void dataSearch()
        {
            Js.BLL.Sys.SysComDal dal = new Js.BLL.Sys.SysComDal();
            DataTable dt = dal.SearchTable(FormID, false).Tables[0];
            this.cmbField.DataSource = dt;
            this.cmbField.DisplayMember = "FieldCName";
            this.cmbField.ValueMember = "FieldName";
        }
        private void frmUserManagers_Load(object sender, EventArgs e)
        {
            this.dataGridView1.AutoGenerateColumns = false;
            dataSearch();
            BindGridView(1);
            this.dataGridView1.Columns[0].Frozen = true;
            this.dataGridView1.Columns[1].Frozen = true;
            this.dataGridView1.Columns[2].Frozen = true;
        }
        /// <summary>
        /// 綁定GirdView
        /// </summary>
        /// <param name="pageIndex"></param>
        private void BindGridView(int pageIndex)
        {
            Js.BLL.Sys.SysComDal dal = new Js.BLL.Sys.SysComDal();

            int RecordCount, PageCount;

            dtData = dal.SelectTable(FormID, pageIndex, strWhere, PageSize, "", out PageCount, out RecordCount).Tables[0];
            if (CurrentPage == 0)
                CurrentPage = PageCount;
            if (RecordCount != 0)
            {
                this.btnLast.Enabled = true;
                this.btnFirst.Enabled = true;
                this.btnToPage.Enabled = true;

                if (CurrentPage > 1)
                    this.btnPre.Enabled = true;
                else
                    this.btnPre.Enabled = false;

                if (CurrentPage < PageCount)
                    this.btnNext.Enabled = true;
                else
                    this.btnNext.Enabled = false;

                lblCurrentPage.Visible = true;
                lblCurrentPage.Text = "共 [" + RecordCount.ToString() + "] 筆記錄  第 [" + CurrentPage.ToString() + "] 頁  共 [" + PageCount.ToString() + "] 頁";
            }
            else
            {
                this.btnFirst.Enabled = false;
                this.btnPre.Enabled = false;
                this.btnNext.Enabled = false;
                this.btnLast.Enabled = false;
                this.btnToPage.Enabled = false;
                lblCurrentPage.Visible = false;
            }
            this.dataGridView1.DataSource = dtData.DefaultView;
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            SetBtnEnabled("F");
        }

        private void btnPre_Click(object sender, EventArgs e)
        {
            SetBtnEnabled("P");
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            SetBtnEnabled("N");
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            SetBtnEnabled("L");
        }

        private void btnToPage_Click(object sender, EventArgs e)
        {
            try
            {
                CurrentPage = int.Parse(txtPageNo.Text);
                SetBtnEnabled("");
            }
            catch
            {
                MessageBox.Show("請輸入正確的頁碼!");
                txtPageNo.Text = "";
                txtPageNo.Focus();
            }

        }
        private void txtPageNo_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnToPage_Click(sender, e);
            }

        }
        private void SetBtnEnabled(string movePage)
        {
            switch (movePage)
            {
                case "F":
                    CurrentPage = 1;
                    break;
                case "P":
                    CurrentPage = CurrentPage - 1;
                    break;
                case "N":
                    CurrentPage = CurrentPage + 1;
                    break;
                case "L":
                    CurrentPage = 0;
                    break;
                default:
                    CurrentPage = 1;
                    break;
            }

            BindGridView(CurrentPage);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string KeyValue = this.dataGridView1.Rows[this.dataGridView1.CurrentRow.Index].Cells[2].Value.ToString();
            if (KeyValue == "")
                return;
            if (e.ColumnIndex == 0)
            {
                frmUserManagerView f = new frmUserManagerView(KeyValue);

                f.ShowDialog();
                SetBtnEnabled("");
            }
            else if (e.ColumnIndex == 1)
            {
                if (this.dataGridView1.Rows[this.dataGridView1.CurrentRow.Index].Cells["Column13"].Value.ToString().Trim().Length > 0)
                {
                    MessageBox.Show("已被覆核，不可編輯!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                frmUserManagerEdit f = new frmUserManagerEdit(KeyValue);

                if (f.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    SetBtnEnabled("");
            }
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            frmUserManagerEdit f = new frmUserManagerEdit();

            if (f.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                SetBtnEnabled("");
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            strWhere = strIniWhere;
            txtContent.Text = txtContent.Text.Replace("'", "");
            if (txtContent.Text.Trim() != "")
                strWhere += " And " + this.cmbField.SelectedValue + " like '%" + txtContent.Text + "%'";
            SetBtnEnabled("F");

            if (dtData.Rows.Count <= 0)
                MessageBox.Show("沒有資料可查詢!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.CurrentRow.Index < 0)
                return;
            string KeyValue = this.dataGridView1.Rows[this.dataGridView1.CurrentRow.Index].Cells[2].Value.ToString();
            if (this.dataGridView1.Rows[this.dataGridView1.CurrentRow.Index].Cells[9].Value.ToString().Length >0)
            {
                MessageBox.Show("除企業管理員[" + KeyValue + "]之資料已經覆核，不能刪除！", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;

            }
            if (MessageBox.Show("您確定要刪除企業管理員[" + KeyValue + "]之資料嗎?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID);
                dal.Delete(KeyValue);
                SetBtnEnabled("");
            }
        }
    }

}
