using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Main.Sys
{
    public partial class frmSelect : Form
    {
        private string FormID;        
        private bool SelectOption = false;
        private int PageSize = 20;
        private int CurrentPage = 1;
        private string strWhere = " 1=1";
        private string strInitialWhere = " 1=1 ";
        private string KeyName;
        private int BindCount = 0;

        private DataTable dtData;
        public DataTable dtSelect;
        

        public frmSelect()
        {
            InitializeComponent();
        }
        public frmSelect(bool SelectOption, string FormID, string strWhere)
        {
            InitializeComponent();
            this.SelectOption = SelectOption;
            this.FormID = FormID;            
            this.strWhere = strWhere;
            this.strInitialWhere = strWhere;
        }
        private void frmSelect_Load(object sender, EventArgs e)
        {
            Js.BLL.Sys.TreeListDal dal = new Js.BLL.Sys.TreeListDal();
            Js.Model.Sys.TreeListInfo model = dal.GetModel(FormID);
            KeyName = model.KeyField;
            BindComboBox();
            this.cmbField.SelectedIndex = 0;
            SetBtnEnabled("");
            SetColumn();
        }
        private void BindComboBox()
        {
            Js.BLL.Sys.SysComDal dal = new Js.BLL.Sys.SysComDal();
            DataTable dt = dal.SearchTable(FormID, false).Tables[0];
            this.cmbField.DataSource = dt;
            this.cmbField.DisplayMember = "FieldCName";
            this.cmbField.ValueMember = "FieldName";
        }
        private void SetColumn()
        {
            
            //this.dataGridView1.Columns[0].Width = 40;
            //this.dataGridView1.Columns[1].Width = 100;
            //this.dataGridView1.Columns[2].Width = 150;
            //this.dataGridView1.Columns[3].Width = 80;
            //this.dataGridView1.Columns[4].Width = 100;
            //this.dataGridView1.Columns[5].Width = 80;
            //this.dataGridView1.Columns[6].Width = 80;
            //this.dataGridView1.Columns[7].Width = 80;
            //this.dataGridView1.Columns[8].Width = 80;
            //this.dataGridView1.Columns[9].Width = 80;
            //this.dataGridView1.Columns[10].Width = 200;

            if (!SelectOption)
            {
                this.colCheck.Visible = false;
                this.dataGridView1.ReadOnly = true;
                this.btnCheckPage.Enabled = false;
                this.btnClearPage.Enabled = false;
                this.btnClearAll.Enabled = false;
            }
            else
            {
                this.btnCheckPage.Enabled = true;
                this.btnClearPage.Enabled = true;
                this.btnClearAll.Enabled = true;

                this.colCheck.Visible = true;
                this.colCheck.ReadOnly = false;
                this.dataGridView1.ReadOnly = true;
            }
            
            this.dataGridView1.Columns[1].Frozen = true;
            this.dataGridView1.Columns[2].Frozen = true;
        }

        /// <summary>
        /// 綁定GirdView
        /// </summary>
        /// <param name="pageIndex"></param>
        private void BindGridView(int pageIndex)
        {
            BindCount++;

            Js.BLL.Sys.SysComDal dal = new Js.BLL.Sys.SysComDal();

            DataSet ds = new DataSet();
            int RecordCount, PageCount;

            //dtData = dal.SelectTable(FormID, pageIndex, strWhere, PageSize, "", out PageCount, out RecordCount).Tables[0];
            dtData = dal.GetSearchSelectSQL(FormID, pageIndex, strWhere, PageSize, out PageCount, out RecordCount).Tables[0];
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
            if(BindCount==1)
                dtSelect = dtData.Clone();
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

        #region Button Event
        

        private void btnToPage_Click(object sender, System.EventArgs e)
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
        #endregion

        #region Search
        private void btnSearch_Click(object sender, System.EventArgs e)
        {
            txtContent.Text = txtContent.Text.Replace("'", "");
            strWhere = strInitialWhere;
            if (txtContent.Text.Trim() != "")
                strWhere += " And " + this.cmbField.SelectedValue + " like '%" + txtContent.Text + "%'";

            SetBtnEnabled("F");

            if (dtData.Rows.Count <= 0)
                MessageBox.Show("沒有查找到相關資料!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void txtContent_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSearch.Focus();
                btnSearch_Click(sender, e);
            }
        }
        
        #endregion

        #region DataGridView Event
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
                return;
            if (e.ColumnIndex == 0 && SelectOption)
            {
                DataGridViewCheckBoxCell CheckCell = (DataGridViewCheckBoxCell)this.dataGridView1.Rows[e.RowIndex].Cells[0];
                if ((bool)CheckCell.EditedFormattedValue)
                    dtSelect.ImportRow(dtData.Rows[e.RowIndex]);
                else
                {
                    //string KeyValue = dtData.Rows[e.RowIndex][KeyName].ToString();
                    if ( dtSelect.Rows.IndexOf(dtData.Rows[e.RowIndex]) != -1)
                        dtSelect.Rows.Remove(dtData.Rows[e.RowIndex]);
                    //string KeyValue = dtData.Rows[e.RowIndex][KeyName].ToString();
                    //DataRow[] dr = dtSelect.Select(KeyName + "='" + KeyValue + "'");
                    //for (int i = 0; i < dr.Length; i++)
                    //    dtSelect.Rows.Remove(dr[i]);
                }
            }
        }
        private void CheckedSelectRow()
        {
            for (int i = 0; i < this.dataGridView1.Rows.Count; i++)
            {
                string KeyValue = dtData.Rows[i][KeyName].ToString();
                DataRow[] dr = dtSelect.Select(KeyName + "='" + KeyValue + "'");
                if (dr.Length > 0)
                    this.dataGridView1.Rows[i].Cells[0].Value = true;
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
                return;
            if (SelectOption)
            {
                DataGridViewCheckBoxCell CheckCell = (DataGridViewCheckBoxCell)this.dataGridView1.Rows[e.RowIndex].Cells[0];
                if ((bool)CheckCell.EditedFormattedValue)
                {
                    this.dataGridView1.Rows[e.RowIndex].Cells[0].Value = false;
                    if (dtSelect.Rows.IndexOf(dtData.Rows[e.RowIndex]) != -1)
                        dtSelect.Rows.Remove(dtData.Rows[e.RowIndex]);
                    //string KeyValue = dtData.Rows[e.RowIndex][KeyName].ToString();
                    //DataRow[] dr = dtSelect.Select(KeyName + "='" + KeyValue + "'");
                    //for (int i = 0; i < dr.Length; i++)
                    //    dtSelect.Rows.Remove(dr[i]);
                }
                else
                {
                    this.dataGridView1.Rows[e.RowIndex].Cells[0].Value = true;
                    dtSelect.ImportRow(dtData.Rows[e.RowIndex]);
                }
            }
            else
            {
                dtSelect.ImportRow(dtData.Rows[e.RowIndex]);
                this.DialogResult = DialogResult.OK;
            }
        }
        #endregion

        #region Button Event
        private void btnCheckPage_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.dataGridView1.Rows.Count; i++)
            {
                DataGridViewCheckBoxCell CheckCell = (DataGridViewCheckBoxCell)this.dataGridView1.Rows[i].Cells[0];
                if (!(bool)CheckCell.EditedFormattedValue)
                {
                    this.dataGridView1.Rows[i].Cells[0].Value = true;
                    dtSelect.ImportRow(dtData.Rows[i]);
                }
            }
        }

        private void btnClearPage_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.dataGridView1.Rows.Count; i++)
            {
                DataGridViewCheckBoxCell CheckCell = (DataGridViewCheckBoxCell)this.dataGridView1.Rows[i].Cells[0];
                if ((bool)CheckCell.EditedFormattedValue)
                {
                    this.dataGridView1.Rows[i].Cells[0].Value = false;
                    string KeyValue = dtData.Rows[i][KeyName].ToString();
                    DataRow[] dr = dtSelect.Select(KeyName + "='" + KeyValue + "'");
                    for (int j = 0; j < dr.Length; j++)
                        dtSelect.Rows.Remove(dr[j]);
                }
            }
        }

        private void btnClearAll_Click(object sender, EventArgs e)
        {
            dtSelect = dtData.Clone();
            for (int i = 0; i < this.dataGridView1.Rows.Count; i++)
            {
                DataGridViewCheckBoxCell CheckCell = (DataGridViewCheckBoxCell)this.dataGridView1.Rows[i].Cells[0];
                if ((bool)CheckCell.EditedFormattedValue)
                {
                    this.dataGridView1.Rows[i].Cells[0].Value = false;
                }
            }
        }

        private void btnGetBack_Click(object sender, EventArgs e)
        {
            if (!SelectOption && this.dataGridView1.Rows.Count > 0)
            {
                if (this.dataGridView1.CurrentRow == null)
                {
                    MessageBox.Show("請選取資料!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                DataRow dr = dtData.Rows[this.dataGridView1.CurrentRow.Index];
                dtSelect.ImportRow(dr);
            }
            this.DialogResult = DialogResult.OK;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
        #endregion
    }
}