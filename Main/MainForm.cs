using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Main
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
        private bool OpenOnce(Form frm)
        {
            foreach (Form mdifrm in this.MdiChildren)
            {
                if (frm.Text == mdifrm.Text)
                {
                    mdifrm.Activate();
                    mdifrm.WindowState = FormWindowState.Maximized;
                    return false;
                }
            }
            return true;

        }
        private void ToolStripMenuItem_Db_Click(object sender, EventArgs e)
        {
            BaseData.frmEnterpriseDbs f = new BaseData.frmEnterpriseDbs();
            if (OpenOnce(f))
            {
                f.MdiParent = this;
                f.Show();
                f.WindowState = FormWindowState.Maximized;
            }
        }       

        private void ToolStripMenuItem_Manager_Click(object sender, EventArgs e)
        {
            BaseData.frmUserManagers f = new BaseData.frmUserManagers();
            if (OpenOnce(f))
            {
                f.MdiParent = this;
                f.Show();
                f.WindowState = FormWindowState.Maximized;
            }
        }

        private void ToolStripMenuItem_LabelHost_Click(object sender, EventArgs e)
        {
            BaseData.frmLabelNoDbs f = new BaseData.frmLabelNoDbs(1);
            if (OpenOnce(f))
            {
                f.MdiParent = this;
                f.Show();
                f.WindowState = FormWindowState.Maximized;
            }
        }

        private void ToolStripMenuItem_LabelImageHost_Click(object sender, EventArgs e)
        {
            BaseData.frmLabelNoDbs f = new BaseData.frmLabelNoDbs(2);
            if (OpenOnce(f))
            {
                f.MdiParent = this;
                f.Show();
                f.WindowState = FormWindowState.Maximized;
            }
        }
    }
}
