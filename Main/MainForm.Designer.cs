namespace Main
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.系統SToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_Db = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_Manager = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_LabelHost = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_LabelImageHost = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_BatchUpdate = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_BatchUpdateRecord = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_BillClear = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_DataBase = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_Schedule = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.系統SToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(718, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 系統SToolStripMenuItem
            // 
            this.系統SToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_Db,
            this.ToolStripMenuItem_Manager,
            this.ToolStripMenuItem_LabelHost,
            this.ToolStripMenuItem_LabelImageHost,
            this.ToolStripMenuItem_BatchUpdate,
            this.ToolStripMenuItem_BatchUpdateRecord,
            this.ToolStripMenuItem_BillClear,
            this.ToolStripMenuItem_DataBase,
            this.ToolStripMenuItem_Schedule});
            this.系統SToolStripMenuItem.Name = "系統SToolStripMenuItem";
            this.系統SToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.系統SToolStripMenuItem.Text = "系統(&S)";
            // 
            // ToolStripMenuItem_Db
            // 
            this.ToolStripMenuItem_Db.Name = "ToolStripMenuItem_Db";
            this.ToolStripMenuItem_Db.Size = new System.Drawing.Size(196, 22);
            this.ToolStripMenuItem_Db.Text = "企業資料庫管理作業";
            this.ToolStripMenuItem_Db.Click += new System.EventHandler(this.ToolStripMenuItem_Db_Click);
            // 
            // ToolStripMenuItem_Manager
            // 
            this.ToolStripMenuItem_Manager.Name = "ToolStripMenuItem_Manager";
            this.ToolStripMenuItem_Manager.Size = new System.Drawing.Size(196, 22);
            this.ToolStripMenuItem_Manager.Text = "企業營運管理員設定";
            this.ToolStripMenuItem_Manager.Click += new System.EventHandler(this.ToolStripMenuItem_Manager_Click);
            // 
            // ToolStripMenuItem_LabelHost
            // 
            this.ToolStripMenuItem_LabelHost.Name = "ToolStripMenuItem_LabelHost";
            this.ToolStripMenuItem_LabelHost.Size = new System.Drawing.Size(196, 22);
            this.ToolStripMenuItem_LabelHost.Text = "標籤序號主機設定";
            this.ToolStripMenuItem_LabelHost.Click += new System.EventHandler(this.ToolStripMenuItem_LabelHost_Click);
            // 
            // ToolStripMenuItem_LabelImageHost
            // 
            this.ToolStripMenuItem_LabelImageHost.Name = "ToolStripMenuItem_LabelImageHost";
            this.ToolStripMenuItem_LabelImageHost.Size = new System.Drawing.Size(196, 22);
            this.ToolStripMenuItem_LabelImageHost.Text = "標籤存證圖片主機設定";
            this.ToolStripMenuItem_LabelImageHost.Click += new System.EventHandler(this.ToolStripMenuItem_LabelImageHost_Click);
            // 
            // ToolStripMenuItem_BatchUpdate
            // 
            this.ToolStripMenuItem_BatchUpdate.Name = "ToolStripMenuItem_BatchUpdate";
            this.ToolStripMenuItem_BatchUpdate.Size = new System.Drawing.Size(196, 22);
            this.ToolStripMenuItem_BatchUpdate.Text = "基本資料編號變更作業";
            // 
            // ToolStripMenuItem_BatchUpdateRecord
            // 
            this.ToolStripMenuItem_BatchUpdateRecord.Name = "ToolStripMenuItem_BatchUpdateRecord";
            this.ToolStripMenuItem_BatchUpdateRecord.Size = new System.Drawing.Size(196, 22);
            this.ToolStripMenuItem_BatchUpdateRecord.Text = "基本資料變更紀錄";
            // 
            // ToolStripMenuItem_BillClear
            // 
            this.ToolStripMenuItem_BillClear.Name = "ToolStripMenuItem_BillClear";
            this.ToolStripMenuItem_BillClear.Size = new System.Drawing.Size(196, 22);
            this.ToolStripMenuItem_BillClear.Text = "交易單據結清作業";
            // 
            // ToolStripMenuItem_DataBase
            // 
            this.ToolStripMenuItem_DataBase.Name = "ToolStripMenuItem_DataBase";
            this.ToolStripMenuItem_DataBase.Size = new System.Drawing.Size(196, 22);
            this.ToolStripMenuItem_DataBase.Text = "資料庫管理器";
            // 
            // ToolStripMenuItem_Schedule
            // 
            this.ToolStripMenuItem_Schedule.Name = "ToolStripMenuItem_Schedule";
            this.ToolStripMenuItem_Schedule.Size = new System.Drawing.Size(196, 22);
            this.ToolStripMenuItem_Schedule.Text = "工作指令排程主控台";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(718, 434);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "信實防偽後臺管理系統";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 系統SToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_Db;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_Manager;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_LabelHost;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_LabelImageHost;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_BatchUpdate;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_BatchUpdateRecord;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_BillClear;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_DataBase;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_Schedule;

    }
}