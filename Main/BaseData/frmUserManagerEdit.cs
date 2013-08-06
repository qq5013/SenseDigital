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
    public partial class frmUserManagerEdit : frmBase
    {
        string UserName;
        string FormID = "BU_UserManager";

        public frmUserManagerEdit()
        {
            InitializeComponent();
            UserName = "";
        }
        public frmUserManagerEdit(string UserName)
        {
            InitializeComponent();
            this.UserName = UserName;
        }

        private void frmUserManagerEdit_Load(object sender, EventArgs e)
        {
            Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID);
            if (UserName.Length > 0)
            {                
                string filter = string.Format("UserName='{0}'", UserName);
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

            dtsub = new DataTable[1];
            dtsub[0] = dal.GetSubDetail(this.txtUserName.Text.Trim()).Tables[0];
            BindGridEventDoFormat();
            BindGridView();
            dgView1.Columns[2].ReadOnly = true;
        }
        private void BindData(DataTable dt)
        {
            if (dt.Rows.Count > 0)
            {
                this.txtUserName.Text = dt.Rows[0]["UserName"].ToString();
                this.txtPersonID.Text = dt.Rows[0]["PersonID"].ToString();
                this.txtPersonName.Text = dt.Rows[0]["PersonName"].ToString();
                this.txtCreateUserName.Text = dt.Rows[0]["CreateUserName"].ToString();
                this.txtCreateDate.Text = dt.Rows[0]["CreateDate"].ToString();
                this.txtLastModifyUserName.Text = dt.Rows[0]["LastModifyUserName"].ToString();
                this.txtLastModifyDate.Text = dt.Rows[0]["LastModifyDate"].ToString();
                this.txtCheckUserName.Text = dt.Rows[0]["CheckUserName"].ToString();
                this.txtCheckDate.Text = dt.Rows[0]["CheckDate"].ToString();
            }
        }

        private void txtUserName_DoubleClick(object sender, EventArgs e)
        {
            Sys.frmSelect f = new Sys.frmSelect(false, "BU_User", " 1=1 ");
            f.ShowDialog();
            if (f.DialogResult == DialogResult.OK)
            {
                if (f.dtSelect.Rows.Count > 0)
                {
                    this.txtUserName.Text = f.dtSelect.Rows[0][0].ToString();
                    this.txtPersonID.Text = f.dtSelect.Rows[0][1].ToString();
                    this.txtPersonName.Text = f.dtSelect.Rows[0][2].ToString();
                }
            }
        }
        private void txtUserName_TextChanged(object sender, EventArgs e)
        {
           
        }
        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (this.txtUserName.Text.Trim() == "")
            {
                MessageBox.Show("請輸入登入帳號!", "存檔", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.txtUserName.Focus();
                return;
            }
            if (dtsub[0].Rows.Count <= 0)
            {
                MessageBox.Show("明細不能為空!", "存檔", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            for (int i = 0; i < dgView1.Rows.Count; i++)
            {
                if (dgView1.Rows[i].IsNewRow)
                    continue;
                if (dgView1.Rows[i].Cells[1].Value.ToString().Trim() == "")
                {
                    MessageBox.Show("企業編號不能為空!", "存檔", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else if(dgView1.Rows[i].Cells[2].Value.ToString().Trim() == "")
                {
                    MessageBox.Show("企業編號不存在!", "存檔", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            
            Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID);
            if (UserName.Length > 0 && this.txtUserName.Text.Trim() != UserName || UserName.Length <= 0)
            {
                if (dal.Exists(this.txtUserName.Text.Trim()))
                {
                    MessageBox.Show("此登入帳號已經存在,請選取其他登入帳號!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            DataTable dt = dal.GetRecord("1=1");
            DataRow dr = dt.NewRow();
            dr["UserName"] = this.txtUserName.Text.Trim();
            dr["PersonName"] = this.txtPersonName.Text.Trim();
            dr["PersonID"] = this.txtPersonID.Text.Trim();
            
            dr["CreateUserName"] = Js.Com.User.UserID;
            if (UserName.Length > 0)
                dr["CreateDate"] = this.txtCreateDate.Text;
            else
                dr["CreateDate"] = DateTime.Now.ToString("yyyy/MM/dd HH:mm");
            dr["LastModifyUserName"] = Js.Com.User.UserID;
            dr["LastModifyDate"] = DateTime.Now.ToString("yyyy/MM/dd HH:mm");
            dr["CheckUserName"] = this.txtCheckUserName.Text;
            if (this.txtCheckDate.Text.Length > 0)
                dr["CheckDate"] = this.txtCheckDate.Text;

            if (UserName.Length > 0)
                dal.Update(dr, UserName);
            else
                dal.Add(dr);
            dgView1.Update();

            for (int i = 0; i < dtsub[0].Rows.Count; i++)
            {
                if (dgView1.Rows[i].IsNewRow)
                {
                    dtsub[0].Rows.RemoveAt(i);
                }
                else
                {
                    dtsub[0].Rows[i]["UserName"] = this.txtUserName.Text;
                }
            }
            dal.SaveDetail(dtsub, UserName);

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        /// <summary>
        /// 可復寫,也可用dgv  事件
        /// </summary>
        public override void CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            if (dgv.Name.ToLower() == "dgview1")
            {
                if (e.ColumnIndex == this.Column2.Index)
                {
                    //檢查是否有重複
                    if (Exists(dgv))
                    {
                        MessageBox.Show("對不起,此編號已經存在!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dgv.EditingControl.Text = "" + histroyValue;
                        dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = histroyValue;
                        return;
                    }
                    Js.BLL.BaseDal dal = new Js.BLL.BaseDal("BU_Enterprise");
                    string filter = string.Format("EnterpriseID='{0}'", dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                    DataTable dt = dal.GetRecord(filter);
                    if (dt.Rows.Count > 0)
                    {
                        dgv.Rows[e.RowIndex].Cells[this.Column3.Index].Value = dt.Rows[0]["EnterpriseName"].ToString();
                    }                    
                }
            }
        }

        private void dgView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            if (e.RowIndex == -1 || dgv.CurrentRow == null || dgv.CurrentRow.IsNewRow) return;
            //if (frmState == Js.NetBase.KcfrmState.View) return;
            if (this.Column2.Index == e.ColumnIndex)
            {
                Sys.frmSelect frm = new Sys.frmSelect(true, "BU_Enterprise", " 1=1 ");
                //Js.NetBase.frmSelect frm = new Js.NetBase.frmSelect(bll.getSysPid("machine"), true);
                frm.ShowInTaskbar = false;
                frm.ShowDialog();
                RefreshTable();

                DataRow dr;
                if (frm.DialogResult == DialogResult.OK)
                {
                    for (int i = frm.dtSelect.Rows.Count - 1; i >= 0; i--)
                    {
                        if (!Exists(dgv, frm.dtSelect.Rows[i][0].ToString()))
                        {
                            if (i == (frm.dtSelect.Rows.Count - 1))
                                dr = GetRow(dgv, e.RowIndex);
                            else
                                dr = NewRowInsertAt(dgv, e.RowIndex);
                            dgv.Rows[e.RowIndex].Cells[this.Column2.Index].Value = frm.dtSelect.Rows[i][0].ToString();
                            dgv.Rows[e.RowIndex].Cells[this.Column3.Index].Value = frm.dtSelect.Rows[i][1].ToString();
                        }
                    }
                    CheckRowID(dgv);
                }
            }
        }

        private void txtUserName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.txtUserName.Text = this.txtUserName.Text.Trim();
                Js.BLL.BaseDal dal = new Js.BLL.BaseDal("BU_User");
                string filter = string.Format("UserName='{0}'", this.txtUserName.Text);
                DataTable dt = dal.GetRecord(filter);

                if (dt.Rows.Count > 0)
                {
                    this.txtUserName.Text = dt.Rows[0]["UserName"].ToString();
                    this.txtPersonID.Text = dt.Rows[0]["PersonID"].ToString();
                    this.txtPersonName.Text = dt.Rows[0]["PersonName"].ToString();
                }
            }
        }

        //private void dgView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        //{
        //    if (e.ColumnIndex == this.Column2.Index)
        //    {
        //        //檢查是否有重複
        //        if (Exists(dgView1))
        //        {
        //            MessageBox.Show("對不起,此編號已經存在!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
        //            dgView1.EditingControl.Text = "" + histroyValue;
        //            dgView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = histroyValue;
        //            return;
        //        }
        //        Js.BLL.BaseDal dal = new Js.BLL.BaseDal("BU_Enterprise");
        //        string filter = string.Format("EnterpriseID='{0}'", dgView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
        //        DataTable dt = dal.GetRecord(filter);
        //        if (dt.Rows.Count > 0)
        //        {
        //            dgView1.Rows[e.RowIndex].Cells[this.Column3.Index].Value = dt.Rows[0]["EnterpriseName"].ToString();
        //        }
        //    }
            
        //}
    }
}
