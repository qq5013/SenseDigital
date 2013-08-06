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
    public partial class frmLabelNoDbs : Form
    {
        string FormID = "BU_LabelNoDb";
        int PageSize = 30;
        int CurrentPage = 1;        
        string strIniWhere = "1=1";
        string strWhere = "1=1";
        int IntFlag = 1;

        DataTable dtData;
        public frmLabelNoDbs()
        {
            InitializeComponent();
            //CreateUnboundButtonColumn();
        }
        public frmLabelNoDbs(int Flag)
        {
            InitializeComponent();
            //CreateUnboundButtonColumn();
           
            IntFlag = Flag;
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
        /// <summary>
        /// 綁定查詢
        /// </summary>
        private void dataSearch()
        {
            Js.BLL.Sys.SysLabelNoDb dal = new Js.BLL.Sys.SysLabelNoDb();
            DataTable dt = dal.SearchTable(FormID, false).Tables[0];
            this.cmbField.DataSource = dt;
            this.cmbField.DisplayMember = "FieldCName";
            this.cmbField.ValueMember = "FieldName";
        }
        private void CreateUnboundButtonColumn()
        {
            // Initialize the button column.
            DataGridViewButtonColumn buttonColumn = new DataGridViewButtonColumn();
            buttonColumn.Name = "Detail";
            buttonColumn.HeaderText = "詳細資料";
            buttonColumn.Text = "詳細資料";
            buttonColumn.Width = 60;
            //buttonColumn.UseColumnTextForButtonValue = true;
            buttonColumn.DefaultCellStyle.NullValue = "詳細資料";

            dataGridView1.Columns.Insert(0, buttonColumn);

            buttonColumn = new DataGridViewButtonColumn();
            buttonColumn.Name = "Edit";
            buttonColumn.HeaderText = "編輯";
            buttonColumn.Text = "編輯";
            buttonColumn.Width = 40;
            //buttonColumn.UseColumnTextForButtonValue = true;
            buttonColumn.DefaultCellStyle.NullValue = "編輯";

            dataGridView1.Columns.Insert(1, buttonColumn);
        }
        private void frmLabelNoDbs_Load(object sender, EventArgs e)
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
            Js.BLL.Sys.SysLabelNoDb dal = new Js.BLL.Sys.SysLabelNoDb();

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
                frmLabelNoDbView f = new frmLabelNoDbView(IntFlag, KeyValue);

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
                
                frmLabelNoDbEdit f = new frmLabelNoDbEdit(IntFlag, KeyValue);
                if (this.dataGridView1.Rows.Count == 1)
                {
                    f.blnCanEdit = true;
                }
                else
                {
                    f.blnCanEdit = false;
                }
                if (f.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    SetBtnEnabled("");
            }
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            bool blnvalue = false;

            if (this.dataGridView1.Rows.Count == 1)
            {
                if (this.dataGridView1.Rows[this.dataGridView1.CurrentRow.Index].Cells[3].Value.ToString() == "單一主機")
                {
                    MessageBox.Show("單一主機只有一條記錄，不能再新增!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    blnvalue = true;
                }
            }
            if (blnvalue)
            {
                return;
            }
            frmLabelNoDbEdit f = new frmLabelNoDbEdit(IntFlag);
            if (this.dataGridView1.Rows.Count == 0)
                f.blnCanEdit = true;
            else
                f.blnCanEdit = false;
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
            if (this.dataGridView1.Rows[this.dataGridView1.CurrentRow.Index].Cells[11].Value.ToString().Length > 0)
            {
                MessageBox.Show("標籤伺服器[" + KeyValue + "]之資料已經覆核，不能刪除！", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;

            }
            if (MessageBox.Show("您確定要刪除標籤[" + KeyValue + "]之資料嗎?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID);
                dal.Delete(KeyValue);
                SetBtnEnabled("");
            }
        }
    }
    
}
