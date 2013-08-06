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
    public partial class frmBase : Form
    {
        public frmBase()
        {
            InitializeComponent();
        }

        #region 子檔事件
        /// <summary>
        /// 子檔數組
        /// </summary>
        public DataTable[] dtsub;
        /// <summary>
        /// 針對數量  直接  CellValue  的附值
        /// 數字重新格式再附值 如果不一致還會激活CellValueChanged事件
        /// subCellValueChanged = false 不考慮格式的數值
        /// subCellValueChanged = true  考慮格式的數值
        /// </summary>
        public bool subCellValueChanged = false;

        private void dtsub_RowChanged(object sender, DataRowChangeEventArgs e)
        {
            if (e.Action == DataRowAction.Add)
            {
                SetNewRowValue(e.Row);
                SetNewRowValue(Array.IndexOf(dtsub, sender), e.Row);
            }
        }

        /// <summary>
        /// 歷史值 dgView cell value
        /// </summary>
        public object histroyValue;

        /// <summary>
        /// dtsub[] 要先加載數據 
        /// </summary>
        public void BindGridView()
        {
            if (dtsub == null) return;

            Control[] arr;

            for (int i = 0; i < dtsub.Length; i++)
            {
                arr = this.Controls.Find("dgView" + (i + 1), true);
                if (arr.Length > 0)
                {
                    ((DataGridView)arr[0]).EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2;
                    //綁定datatable
                    ((DataGridView)arr[0]).AutoGenerateColumns = false;
                    ((DataGridView)arr[0]).DataSource = dtsub[i];
                    //
                    dtsub[i].RowChanged += new DataRowChangeEventHandler(dtsub_RowChanged);
                }
            }
        }

        public void BindGridView(DataGridView dgv)
        {
            int i = int.Parse(dgv.Name.Substring(dgv.Name.Length - 1, 1)) - 1;
            //dgv.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2;
            ////綁定datatable
            //dgv.AutoGenerateColumns = false;
            dgv.DataSource = dtsub[i];
            //
            //dtsub[i].RowChanged += new DataRowChangeEventHandler(dtsub_RowChanged);
        }
        /// <summary>
        /// 加載事件 與  格式
        /// </summary>
        public void BindGridEventDoFormat()
        {
            if (dtsub == null) return;

            Control[] arr;

            for (int i = 0; i < dtsub.Length; i++)
            {
                arr = this.Controls.Find("dgView" + (i + 1), true);
                if (arr.Length > 0)
                {
                    CloseSortMode((DataGridView)arr[0]);
                    ((DataGridView)arr[0]).AlternatingRowsDefaultCellStyle.BackColor = Color.Gainsboro;
                    ((DataGridView)arr[0]).AlternatingRowsDefaultCellStyle.SelectionBackColor = ((DataGridView)arr[0]).AlternatingRowsDefaultCellStyle.ForeColor;
                    //加載事件
                    BindEvent(arr[0]);

                    dgviewFormat(((DataGridView)arr[0]));
                }
            }
        }

        private static void dgView_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (((DataGridView)sender).Parent.Name.ToString() == "frmFastQuery") return;
                if (((DataGridView)sender).Parent.Name.ToString() == "frmSelect") return;
                if (e.ColumnIndex < 0 && e.RowIndex > -1)
                {
                    ((DataGridView)sender).EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2;
                    ((DataGridView)sender).EndEdit();
                    ((DataGridView)sender).SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    ((DataGridView)sender).CurrentCell = ((DataGridView)sender).Rows[e.RowIndex].Cells[0];
                    ((DataGridView)sender).CurrentRow.Selected = true;
                    //((DataGridView)sender).Rows[e.RowIndex].Selected = true;
                }
                else
                {
                    ((DataGridView)sender).SelectionMode = DataGridViewSelectionMode.CellSelect;
                    ((DataGridView)sender).EditMode = DataGridViewEditMode.EditOnEnter;
                }
            }
            catch
            {
            }
        }


        private void CloseSortMode(DataGridView dgv)
        {
            for (int i = 0; i < dgv.Columns.Count; i++)
            {
                dgv.Columns[i].SortMode = dgv.AllowUserToOrderColumns ? DataGridViewColumnSortMode.Automatic : DataGridViewColumnSortMode.Programmatic;
                dgv.ColumnHeadersHeight = 25;
            }
        }
        private void BindEvent(Control ctrl)
        {
            ((DataGridView)ctrl).EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(dgView_EditingControlShowing);
            ((DataGridView)ctrl).CellMouseClick += new DataGridViewCellMouseEventHandler(dgView_CellMouseClick);
            ((DataGridView)ctrl).KeyDown += new KeyEventHandler(dgView_KeyDown);
            //((DataGridView)ctrl).DefaultValuesNeeded += new DataGridViewRowEventHandler(dgView_DefaultValuesNeeded);
            //((DataGridView)ctrl).RowsAdded += new DataGridViewRowsAddedEventHandler(dgView_RowsAdded);
            ((DataGridView)ctrl).RowsRemoved += new DataGridViewRowsRemovedEventHandler(dgView_RowsRemoved);
            //((DataGridView)ctrl).CellValidated += new DataGridViewCellEventHandler(dgView_CellValidated);
            ((DataGridView)ctrl).CellValueChanged += new DataGridViewCellEventHandler(dgView_CellValueChanged);
            ((DataGridView)ctrl).DataError += new DataGridViewDataErrorEventHandler(dgView_DataError);
            //((DataGridView)ctrl).CellEnter += new DataGridViewCellEventHandler(dgView_CellEnter);
            //((DataGridView)ctrl).SelectionChanged += new EventHandler(dgView_SelectionChanged);
        }
        /// <summary>
        ///指定長度 \ 數字右對齊 等
        /// </summary>
        private void dgviewFormat(DataGridView dgv)
        {
            string str = dtsub[int.Parse(dgv.Name.Trim().Substring(6, 1)) - 1].TableName;
            Js.DAO.TableFieldInfo tablecols = new Js.DAO.TableFieldInfo(str);
            
            str = "";
            for (int i = 0; i < dgv.Columns.Count; i++)
            {
                //dgv.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                str = dgv.Columns[i].DataPropertyName;

                switch (tablecols.GetColType(str))
                {
                    case SqlDbType.Int:
                    case SqlDbType.SmallInt:
                        if (dgv.Columns[i] is DataGridViewTextBoxColumn && dgv.Columns[i].DataPropertyName.ToLower() != "rowid")
                            dgv.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        else
                            dgv.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        //if (str.ToLower().Trim() == "subid") SubIDColindex = i;
                        break;
                    case SqlDbType.Decimal:
                        dgv.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                        //if (str.Length > 3 && str.ToLower().Substring(str.Length - 4) == "cost" || str.Length > 4 && str.ToLower().Substring(str.Length - 5) == "money")
                        //    dgv.Columns[i].DefaultCellStyle.Format = Js.Com.User.strDataFormat[2];
                        //if (str.Length > 4 && str.ToLower().Substring(str.Length - 5) == "price")
                        //    dgv.Columns[i].DefaultCellStyle.Format = Js.Com.User.strDataFormat[1];
                        //if (str.Length > 3 && str.ToLower().Substring(str.Length - 4) == "rate")
                        //    dgv.Columns[i].DefaultCellStyle.Format = Js.Com.User.strDataFormat[3];
                        //if (str.Length > 2 && (str.ToLower().Substring(str.Length - 3) == "num" || str.ToLower().Substring(str.Length - 3) == "qty"))
                        //    dgv.Columns[i].DefaultCellStyle.Format = Js.Com.User.strDataFormat[0];

                        break;
                    case SqlDbType.NVarChar:
                        if (dgv.Columns[i] is DataGridViewTextBoxColumn && tablecols.GetTypeLength(str) > 0)
                            ((DataGridViewTextBoxColumn)dgv.Columns[i]).MaxInputLength = tablecols.GetTypeLength(str);
                        //if (dgv.Columns[i] is Js.Controls.DataGridViewYearMonth.DataGridViewYearMonthColumn || dgv.Columns[i] is Js.Controls.DataGridViewDateTime.DataGridViewDateTimeColumn)
                        //    dgv.Columns[i].DefaultCellStyle.Format = Js.Common.User.strDateFormat;
                        break;
                    case SqlDbType.DateTime:
                        //dgv.Columns[i].DefaultCellStyle.FormatProvider = new System.Globalization.CultureInfo("zh-TW", true);
                        //string format = Js.Common.User.strDateFormat;
                        dgv.Columns[i].DefaultCellStyle.Format = Js.Com.User.strDateFormat;
                        break;
                    default:
                        break;
                }

            }
        }
        /// <summary>
        /// 刷新數據  同步當前 cellvalue
        /// </summary>
        public void RefreshGrid()
        {
            //this.Validate(false);

            if (dtsub == null) return;

            Control[] arr;
            int curR, curc;

            for (int i = 0; i < dtsub.Length; i++)
            {
                arr = this.Controls.Find("dgView" + (i + 1), true);
                if (arr.Length > 0)
                {
                    if (((DataGridView)arr[0]).CurrentRow != null && !((DataGridView)arr[0]).CurrentRow.IsNewRow && ((DataGridView)arr[0]).CurrentCell != null)
                    {
                        curR = ((DataGridView)arr[0]).CurrentCell.RowIndex;
                        curc = ((DataGridView)arr[0]).CurrentCell.ColumnIndex;
                        dgView_CellValidated(arr[0], new DataGridViewCellEventArgs(curc, curR));
                    }
                    //((DataGridView)arr[0]).EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2;
                    //((DataGridView)arr[0]).CellEnter -= new DataGridViewCellEventHandler(dgView_CellEnter);
                    //((DataGridView)arr[0]).CurrentCell = ((DataGridView)arr[0]).Rows[((DataGridView)arr[0]).Rows.Count - 1].Cells[0];
                    ////((DataGridView)arr[0]).CurrentCell = ((DataGridView)arr[0]).Rows[curR].Cells[curc];
                    //((DataGridView)arr[0]).CellEnter += new DataGridViewCellEventHandler(dgView_CellEnter);
                }
                //dtsub[i].AcceptChanges();
            }
        }
        /// <summary>
        /// 刷新數據  同步 dtsub 並統計合計
        /// </summary>
        public void RefreshTable()
        {
            //if (dtsub == null) return;

            this.Validate(false);
            DataGridView dgv;
            int cindex, rindex;

            for (int i = 0; i < dtsub.Length; i++)
            {
                cindex = -1;
                rindex = -1;
                dgv = (DataGridView)this.Controls.Find("dgView" + (i + 1), true)[0];

                dgv.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2;
                dgv.EndEdit();
                //dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                if (dgv.CurrentCell != null)
                {
                    cindex = dgv.CurrentCell.ColumnIndex;
                    rindex = dgv.CurrentCell.RowIndex;
                }
                //    //if (arr.Length > 0)
                //    //{
                //    //    curc = ((DataGridView)arr[0]).CurrentCell.ColumnIndex;
                //    //    ((DataGridView)arr[0]).EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2;
                //    //    //((DataGridView)arr[0]).CellEnter -= new DataGridViewCellEventHandler(dgView_CellEnter);
                //    //    //((DataGridView)arr[0]).SelectionChanged -= new EventHandler(dgView_SelectionChanged);
                //    //    ((DataGridView)arr[0]).CurrentCell = ((DataGridView)arr[0]).Rows[((DataGridView)arr[0]).Rows.Count - 1].Cells[0];
                //    //    ((DataGridView)arr[0]).CurrentCell = ((DataGridView)arr[0]).Rows[((DataGridView)arr[0]).Rows.Count - 2].Cells[curc];
                //    //    //((DataGridView)arr[0]).CellEnter += new DataGridViewCellEventHandler(dgView_CellEnter);
                //    //    //((DataGridView)arr[0]).SelectionChanged += new EventHandler(dgView_SelectionChanged);
                //    //}
                dtsub[i].AcceptChanges();
                if (cindex != -1) dgv.CurrentCell = dgv.Rows[rindex].Cells[cindex];
                doMainTotal(dgv);
            }
        }
        /// <summary>
        /// 刷新欄號
        /// </summary>
        public void CheckRowID(DataGridView dgv)
        {
            int colIndex = -1;

            for (int i = 0; i < dgv.Columns.Count; i++)
            {
                if (dgv.Columns[i].DataPropertyName.ToLower().Trim() == "rowid")
                {
                    colIndex = i;
                    break;
                }
            }
            //if (dgv.Columns.IndexOf(dgv.Columns["ColRowID"]) == -1) return;
            //CheckRowID(dgv, dgv.Columns.IndexOf(dgv.Columns["ColRowID"]));
            //int colIndex = dgv.Columns["ColRowID"].Index;
            if (colIndex == -1) return;
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                if (!dgv.Rows[i].IsNewRow)
                    dgv.Rows[i].Cells[colIndex].Value = i + 1;
            }
        }

        /// <summary>
        /// 數字加總
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="colIndex"></param>
        /// <returns></returns>
        public decimal Sum(DataGridView dgv, int colIndex)
        {
            decimal num = 0;
            try
            {
                for (int i = 0; i < dgv.RowCount; i++)
                {
                    if (!dgv.Rows[i].IsNewRow)
                        num += decimal.Parse("" + dgv.Rows[i].Cells[colIndex].Value);
                }
            }
            catch { }

            return num;
        }
        /// <summary>
        /// 主檔加總
        /// </summary>
        public virtual void doMainTotal(DataGridView dgv)
        {
            //if (frmState != NetBase.KcfrmState.View)
            //{
            //資料庫欄位
            //    txttotalmoney.Text = "" + Sum(dgv, this.Colmoney.Index);
            //}
            //dataText5money.Text = "" + (decimal.Parse(txttotalmoney.Text) + decimal.Parse(txtTaxmoney.Text));
        }
        /// <summary>
        /// 當前輸入字竄  存在否
        /// 目前只做  string比較
        /// </summary>
        public bool Exists(DataGridView dgv)
        {
            string str1, str2;
            for (int i = 0; i < dgv.RowCount; i++)
            {
                if (i != dgv.CurrentRow.Index && !dgv.CurrentRow.IsNewRow)
                {
                    str1 = "" + dgv.CurrentCell.Value;
                    str2 = "" + dgv.Rows[i].Cells[dgv.CurrentCell.ColumnIndex].Value;
                    if (str2.Length == 0) return false;
                    if (str1.Trim().ToLower() == str2.Trim().ToLower())
                        return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 字竄  存在否
        /// </summary>
        public bool Exists(DataGridView dgv, string strFind)
        {
            string str2;
            for (int i = 0; i < dgv.RowCount; i++)
            {
                if (i != dgv.CurrentRow.Index && !dgv.CurrentRow.IsNewRow)
                {
                    str2 = "" + dgv.Rows[i].Cells[dgv.CurrentCell.ColumnIndex].Value;
                    if (str2.Length == 0) return false;
                    if (strFind.Trim().ToLower() == str2.Trim().ToLower())
                        return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 插  入  一欄數據  (帶初值)
        /// 返回  一個指向  新增欄指針
        /// </summary>
        public DataRow NewRowInsertAt(DataGridView dgv, int index)
        {
            DataTable pdt = dtsub[int.Parse(dgv.Name.Trim().Substring(6, 1)) - 1];
            //if (dgv.AllowUserToAddRows && (dgv.Rows.Count - 1) != pdt.Rows.Count && dgv.CurrentCell != null)
            //{
            //    dgv.NotifyCurrentCellDirty(true);                
            //}
            //pdt.AcceptChanges();
            DataRow dr;
            dr = pdt.NewRow();
            pdt.Rows.InsertAt(dr, index);
            dgView_DefaultValuesNeeded(dgv, new DataGridViewRowEventArgs(dgv.Rows[index]));
            SetNewRowValue(dr);
            //if (dgv.Columns.IndexOf(dgv.Columns["ColRowID"]) != -1) CheckRowID(dgv, dgv.Columns.IndexOf(dgv.Columns["ColRowID"]));
            return dr;
        }
        public DataRow GetRow(DataGridView dgv, int index)
        {
            DataTable pdt = dtsub[int.Parse(dgv.Name.Trim().Substring(6, 1)) - 1];
            return pdt.Rows[index];
        }
        /// <summary>
        /// 刪除後 手動調用 CheckRowID
        /// </summary>
        public void RemoveAt(DataGridView dgv, int index)
        {
            dgv.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2;
            dgv.EndEdit();
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //dgv.CurrentRow.Selected = true;
            dgv.Rows.RemoveAt(index);
            // if (dgv.Columns.IndexOf(dgv.Columns["ColRowID"]) != -1) CheckRowID(dgv, dgv.Columns.IndexOf(dgv.Columns["ColRowID"]));       
        }

        private void dgView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (subCellValueChanged) return;
            subCellValueChanged = false;
            DataGridView dgv = (DataGridView)sender;
            if (dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null) return;
            switch (dgv.Columns[e.ColumnIndex].ValueType.Name.ToLower())
            {
                case "decimal":
                    int iNum = 4;
                    if (dgv.Columns[e.ColumnIndex].DefaultCellStyle.Format.Length != 0)
                        iNum = int.Parse(dgv.Columns[e.ColumnIndex].DefaultCellStyle.Format.Substring(1, 1));
                    subCellValueChanged = true;
                    if ((decimal)dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value >= 1000000000000000)//溢出
                        dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = 0;
                    dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = decimal.Round((decimal)dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value, iNum);
                    subCellValueChanged = false;
                    break;
            }
        }
        private void dgView_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            CheckRowID(dgv);
            if (dgv.AllowUserToAddRows)
            {
                //if (dgv.Rows.Count >e.RowIndex && !dgv.Rows [e.RowIndex ].IsNewRow )
                //   changeTax = false;
                doMainTotal(dgv);
            }
        }
        //private void dgView_CellEnter(object sender, DataGridViewCellEventArgs e)
        //{
        //    DataGridView dgv = (DataGridView)sender;
        //    if (dgv.ReadOnly || dgv.CurrentRow == null) return;
        //    if (dgv.CurrentRow.IsNewRow && e.ColumnIndex > -1 )//&& dgv.SelectionMode == DataGridViewSelectionMode.CellSelect)
        //    {
        //        dgv.BeginEdit(false);
        //        if (dgv.EditingControl != null)
        //        {
        //            //dgv.RowsAdded += new DataGridViewRowsAddedEventHandler(dgView_RowsAdded);
        //            dgv.EditingControl.Text = dgv.EditingControl.Text + " ";
        //            dgv.EditingControl.Text = dgv.EditingControl.Text.Trim();
        //            //dgv.RowsAdded -= new DataGridViewRowsAddedEventHandler(dgView_RowsAdded);
        //        }
        //        dgView_DefaultValuesNeeded(sender, new DataGridViewRowEventArgs(dgv.Rows[dgv.CurrentRow.Index]));
        //    //    this.Validate(false);
        //    //    dgv.BeginEdit(false);
        //    //    //dgv.EndEdit();
        //    }
        //}

        //private void dgView_SelectionChanged(object sender, EventArgs e)
        //{
        //    DataGridView dgv = (DataGridView)sender;
        //    if (dgv.ReadOnly || dgv.CurrentRow == null) return;

        //    if (dgv.CurrentRow.IsNewRow)
        //    {
        //        if (dgv.EditingControl != null)
        //        {
        //            //dgv.RowsAdded += new DataGridViewRowsAddedEventHandler(dgView_RowsAdded);
        //            dgv.EditingControl.Text = dgv.EditingControl.Text + " ";
        //            dgv.EditingControl.Text = dgv.EditingControl.Text.Trim();
        //            //dgv.RowsAdded -= new DataGridViewRowsAddedEventHandler(dgView_RowsAdded);
        //            //dgView_DefaultValuesNeeded(sender, new DataGridViewRowEventArgs(dgv.Rows[dgv.CurrentRow.Index]));
        //        }
        //    }

        //}
        private void dgView_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Delete)
            //    changeTax = false;
            DataGridView dgv = (DataGridView)sender;
            if (dgv.CurrentRow.IsNewRow || dgv.ReadOnly || !dgv.AllowUserToAddRows || !dgv.AllowUserToDeleteRows) return;
            if (e.KeyCode == Keys.Insert)
            {
                DataTable pdt = dtsub[int.Parse(dgv.Name.Trim().Substring(6, 1)) - 1];

                this.Validate(false);
                pdt.AcceptChanges();

                DataRow dr;
                dr = pdt.NewRow();
                //////
                //for (int i = 0; i < dgv.Columns.Count; i++)
                //{
                //    switch (dgv.Columns[i].ValueType.Name.ToLower())
                //    {
                //        case "string":
                //            dr[dgv.Columns[i].DataPropertyName] = "";
                //            break;
                //        case "decimal":
                //        case "double":
                //            dr[dgv.Columns[i].DataPropertyName] = decimal.Parse("0");
                //            break;
                //        case "byte":
                //            dr[dgv.Columns[i].DataPropertyName] = byte.Parse("0");
                //            break;
                //        case "int":
                //        case "int32":
                //        case "int64":
                //            dr[dgv.Columns[i].DataPropertyName] = int.Parse("0");
                //            break;
                //        case "boolean":
                //            dr[dgv.Columns[i].DataPropertyName] = false;
                //            break;
                //        case "datetime":
                //            dr[dgv.Columns[i].DataPropertyName] = DateTime.Parse("0001/01/01");
                //            break;
                //    }

                //}

                pdt.Rows.InsertAt(dr, dgv.CurrentRow.Index);
                dgView_DefaultValuesNeeded(sender, new DataGridViewRowEventArgs(dgv.Rows[dgv.CurrentRow.Index - 1]));
                SetNewRowValue(dr);
                CheckRowID(dgv);
            }
        }

        private void dgView_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            string str = "";

            for (int i = 0; i < dgv.Columns.Count; i++)
            {

                str = dgv.Columns[i].DataPropertyName.ToLower().Trim();
                if (str == "rowid")
                {
                    if (e.Row.IsNewRow)
                        e.Row.Cells[i].Value = dgv.Rows.Count;
                    else
                        e.Row.Cells[i].Value = dgv.Rows.Count - 1;
                }
                else
                {
                    switch (dgv.Columns[i].ValueType.Name.ToLower())
                    {
                        case "string":
                            e.Row.Cells[i].Value = "";
                            break;
                        case "decimal":
                        case "double":
                            e.Row.Cells[i].Value = decimal.Parse("0");
                            break;
                        case "byte":
                            e.Row.Cells[i].Value = byte.Parse("0");
                            break;
                        case "int":
                        case "int32":
                        case "int64":
                            e.Row.Cells[i].Value = int.Parse("0");
                            break;
                        case "boolean":
                            e.Row.Cells[i].Value = false;
                            break;
                        case "datetime":
                            e.Row.Cells[i].Value = DateTime.Parse("0001/01/01");
                            break;
                    }
                }
            }
            ////以下 overwrite
            DefaultValuesNeeded(sender, e);
        }

        private void dgView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            histroyValue = ((DataGridView)sender).CurrentCell.Value;
            ((Control)e.Control).ImeMode = System.Windows.Forms.ImeMode.OnHalf;
            ((DataGridView)sender).SelectionMode = DataGridViewSelectionMode.CellSelect;
            ((DataGridView)sender).EditMode = DataGridViewEditMode.EditOnEnter;

            e.Control.Validated += new EventHandler(EditingControl_Validated);

            if (((DataGridView)sender).CurrentRow.IsNewRow)
            {
                ((DataGridView)sender).NotifyCurrentCellDirty(true);
                dgView_DefaultValuesNeeded(sender, new DataGridViewRowEventArgs(((DataGridView)sender).Rows[((DataGridView)sender).CurrentRow.Index]));
                histroyValue = ((DataGridView)sender).CurrentCell.Value;
                if (e.Control is DataGridViewTextBoxEditingControl)
                    e.Control.Text = "" + ((DataGridView)sender).CurrentCell.Value;
            }
        }


        private void EditingControl_Validated(object sender, EventArgs e)
        {
            if (dtsub == null) return;

            Control[] arr;

            for (int i = 0; i < dtsub.Length; i++)
            {
                arr = this.Controls.Find("dgView" + (i + 1), true);
                if (arr.Length > 0)
                {
                    //綁定datatable
                    if (((DataGridView)arr[0]).CurrentCell != null && ((DataGridView)arr[0]).EditingControl == (Control)sender)
                        dgView_CellValidated(arr[0], new DataGridViewCellEventArgs(((DataGridView)arr[0]).CurrentCell.ColumnIndex, ((DataGridView)arr[0]).CurrentCell.RowIndex));
                }
            }
        }

        private void dgView_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            if (dgv.EditingControl == null) return;

            string str = dgv.Columns[e.ColumnIndex].DataPropertyName.ToLower().Trim();
            try
            {
                
                if (dgv.EditingControl is DataGridViewComboBoxEditingControl)
                {
                    if ((dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value) != ((DataGridViewComboBoxEditingControl)dgv.EditingControl).SelectedValue)
                        dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = ((DataGridViewComboBoxEditingControl)dgv.EditingControl).SelectedValue;
                }
                else
                {
                    switch (dgv.Columns[e.ColumnIndex].ValueType.Name.ToLower())
                    {
                        case "decimal":
                            if (decimal.Parse(dgv.EditingControl.Text) >= 1000000000000000)
                                dgv.EditingControl.Text = "" + histroyValue;
                            if (decimal.Parse("" + dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value) != decimal.Parse(dgv.EditingControl.Text))
                                dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = decimal.Parse(dgv.EditingControl.Text);
                            break;
                        case "byte":
                            if (byte.Parse("" + dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value) != byte.Parse(dgv.EditingControl.Text))
                                dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = byte.Parse(dgv.EditingControl.Text);
                            break;
                        case "int":
                        case "int32":
                        case "int64":
                            if (int.Parse("" + dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value) != int.Parse(dgv.EditingControl.Text))
                                dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = int.Parse(dgv.EditingControl.Text);
                            break;
                        //case "boolean":
                        //    //e.Row.Cells[i].Value = false;
                        //    break;
                        default:
                            if (("" + dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value) != dgv.EditingControl.Text)
                                dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = dgv.EditingControl.Text;
                            break;
                    }
                }
            }
            catch
            {
                dgv.EditingControl.Text = "" + histroyValue;
                return;
            }

            CellValidated(sender, e);
        }

        private void dgView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        //overide
        /// <summary>
        /// 依情況  不在datagridview中的欄位
        /// 單個table
        /// </summary>
        public virtual void SetNewRowValue(DataRow dr)
        {
            //dr["AskPriceID"] = Guid.NewGuid();
            //dr["ProductID"] = model.ProductID;
        }
        /// <summary>
        /// 依情況  不在datagridview中的欄位
        /// 多個table 
        /// </summary>
        public virtual void SetNewRowValue(int tableIndex, DataRow dr)
        {
            //switch (tableIndex)
            //{
            //    case 0:
            //        dr["AskPriceID"] = Guid.NewGuid();
            //        dr["ProductID"] = model.ProductID;
            //        break;
            //    case 1:
            //        break;
            //    case 2:
            //        break;
            //    case 3:
            //        break;
            //}            
        }
        /// <summary>
        /// 特別  初始
        /// </summary>
        public virtual void DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            //以下 overwrite
            if (dgv.Name.ToLower() == "dgview1")
            {
                //if (dgv.Rows.Count == 1)
                //{
                //    e.Row.Cells[dgv.Columns[ColYm.Name].Index].Value = "" + DateTime.Now.Year + (DateTime.Now.Month > 9 ? "/" : "/0") + DateTime.Now.Month;
                //}
                //else
                //{
                //    string str = "" + dgv.Rows[e.Row.Index - 1].Cells[dgv.Columns[ColYm.Name].Index].Value;
                //    if (str.Length == 0) return;
                //    DateTime datetime = new DateTime(int.Parse(str.Trim().Substring(0, 4)), int.Parse(str.Trim().Substring(5, 2)), 1, 0, 0, 1).AddMonths(1);
                //    e.Row.Cells[dgv.Columns[ColYm.Name].Index].Value = "" + datetime.Year + (datetime.Month > 9 ? "/" : "/0") + datetime.Month;
                //}
            }
        }
        /// <summary>
        /// 可復寫,也可用dgv  事件
        /// </summary>
        public virtual void CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            if (dgv.Name.ToLower() == "dgview1")
            {
                //if (e.ColumnIndex == dgv.Columns[ColYm.Name].Index)
                //{
                //    //檢查是否有重複
                //    if (Exists(dgv))
                //    {
                //        MessageBox.Show("對不起,此年月已存在!", "訊息");
                //        ((Js.Controls.DateTimeText)dgv.EditingControl).DateYearMonth = "" + histroyValue;
                //        dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = histroyValue;
                //        return;
                //    }
                //}

                //if (e.ColumnIndex == this.dgView1.Columns[ColAmount.Name].Index)
                //{
                //    txtMoney1.Text = "" + (Sum(dgv, e.ColumnIndex) + decimal.Parse(txtMoney.Text));
                //}
            }
        }

        #endregion      
    }
}
