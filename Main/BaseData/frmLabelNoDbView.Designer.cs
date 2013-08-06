namespace Main.BaseData
{
    partial class frmLabelNoDbView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLabelNoDbView));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnCheck = new System.Windows.Forms.Button();
            this.txtGroupStartNo = new System.Windows.Forms.TextBox();
            this.txtGroupEndNo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.rbtMul = new System.Windows.Forms.RadioButton();
            this.rbtSingle = new System.Windows.Forms.RadioButton();
            this.txtCheckDate = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtCheckUserName = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtLastModifyDate = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtLastModifyUserName = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtCreateDate = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtCreateUserName = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtMemo = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtSQLServer = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnReturn = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnCheck);
            this.groupBox1.Controls.Add(this.txtGroupStartNo);
            this.groupBox1.Controls.Add(this.txtGroupEndNo);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.rbtMul);
            this.groupBox1.Controls.Add(this.rbtSingle);
            this.groupBox1.Controls.Add(this.txtCheckDate);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.txtCheckUserName);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.txtLastModifyDate);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.txtLastModifyUserName);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.txtCreateDate);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.txtCreateUserName);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txtMemo);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtSQLServer);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(12, 74);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(572, 305);
            this.groupBox1.TabIndex = 63;
            this.groupBox1.TabStop = false;
            // 
            // btnCheck
            // 
            this.btnCheck.Location = new System.Drawing.Point(494, 264);
            this.btnCheck.Name = "btnCheck";
            this.btnCheck.Size = new System.Drawing.Size(68, 25);
            this.btnCheck.TabIndex = 61;
            this.btnCheck.Text = "營運覆核";
            this.btnCheck.UseVisualStyleBackColor = true;
            this.btnCheck.Click += new System.EventHandler(this.btnCheck_Click);
            // 
            // txtGroupStartNo
            // 
            this.txtGroupStartNo.Location = new System.Drawing.Point(120, 95);
            this.txtGroupStartNo.Name = "txtGroupStartNo";
            this.txtGroupStartNo.ReadOnly = true;
            this.txtGroupStartNo.Size = new System.Drawing.Size(171, 22);
            this.txtGroupStartNo.TabIndex = 60;
            // 
            // txtGroupEndNo
            // 
            this.txtGroupEndNo.Location = new System.Drawing.Point(363, 95);
            this.txtGroupEndNo.Name = "txtGroupEndNo";
            this.txtGroupEndNo.ReadOnly = true;
            this.txtGroupEndNo.Size = new System.Drawing.Size(199, 22);
            this.txtGroupEndNo.TabIndex = 59;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(9, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 12);
            this.label1.TabIndex = 58;
            this.label1.Text = "SQL Server名稱";
            // 
            // rbtMul
            // 
            this.rbtMul.AutoSize = true;
            this.rbtMul.Enabled = false;
            this.rbtMul.Location = new System.Drawing.Point(247, 59);
            this.rbtMul.Name = "rbtMul";
            this.rbtMul.Size = new System.Drawing.Size(59, 16);
            this.rbtMul.TabIndex = 57;
            this.rbtMul.TabStop = true;
            this.rbtMul.Text = "多主機";
            this.rbtMul.UseVisualStyleBackColor = true;
            // 
            // rbtSingle
            // 
            this.rbtSingle.AutoSize = true;
            this.rbtSingle.Enabled = false;
            this.rbtSingle.Location = new System.Drawing.Point(120, 59);
            this.rbtSingle.Name = "rbtSingle";
            this.rbtSingle.Size = new System.Drawing.Size(71, 16);
            this.rbtSingle.TabIndex = 56;
            this.rbtSingle.TabStop = true;
            this.rbtSingle.Text = "單一主機";
            this.rbtSingle.UseVisualStyleBackColor = true;
            // 
            // txtCheckDate
            // 
            this.txtCheckDate.Location = new System.Drawing.Point(384, 267);
            this.txtCheckDate.Name = "txtCheckDate";
            this.txtCheckDate.ReadOnly = true;
            this.txtCheckDate.Size = new System.Drawing.Size(107, 22);
            this.txtCheckDate.TabIndex = 54;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label12.Location = new System.Drawing.Point(295, 270);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(77, 12);
            this.label12.TabIndex = 53;
            this.label12.Text = "營運覆核日期";
            // 
            // txtCheckUserName
            // 
            this.txtCheckUserName.Location = new System.Drawing.Point(120, 267);
            this.txtCheckUserName.Name = "txtCheckUserName";
            this.txtCheckUserName.ReadOnly = true;
            this.txtCheckUserName.Size = new System.Drawing.Size(171, 22);
            this.txtCheckUserName.TabIndex = 52;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label13.Location = new System.Drawing.Point(44, 270);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(53, 12);
            this.label13.TabIndex = 51;
            this.label13.Text = "營運覆核";
            // 
            // txtLastModifyDate
            // 
            this.txtLastModifyDate.Location = new System.Drawing.Point(384, 239);
            this.txtLastModifyDate.Name = "txtLastModifyDate";
            this.txtLastModifyDate.ReadOnly = true;
            this.txtLastModifyDate.Size = new System.Drawing.Size(178, 22);
            this.txtLastModifyDate.TabIndex = 50;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label10.Location = new System.Drawing.Point(319, 239);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 12);
            this.label10.TabIndex = 49;
            this.label10.Text = "異動日期";
            // 
            // txtLastModifyUserName
            // 
            this.txtLastModifyUserName.Location = new System.Drawing.Point(120, 239);
            this.txtLastModifyUserName.Name = "txtLastModifyUserName";
            this.txtLastModifyUserName.ReadOnly = true;
            this.txtLastModifyUserName.Size = new System.Drawing.Size(171, 22);
            this.txtLastModifyUserName.TabIndex = 48;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label11.Location = new System.Drawing.Point(44, 242);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(53, 12);
            this.label11.TabIndex = 47;
            this.label11.Text = "異動人員";
            // 
            // txtCreateDate
            // 
            this.txtCreateDate.Location = new System.Drawing.Point(384, 211);
            this.txtCreateDate.Name = "txtCreateDate";
            this.txtCreateDate.ReadOnly = true;
            this.txtCreateDate.Size = new System.Drawing.Size(178, 22);
            this.txtCreateDate.TabIndex = 46;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label9.Location = new System.Drawing.Point(319, 211);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 12);
            this.label9.TabIndex = 45;
            this.label9.Text = "建檔日期";
            // 
            // txtCreateUserName
            // 
            this.txtCreateUserName.Location = new System.Drawing.Point(120, 211);
            this.txtCreateUserName.Name = "txtCreateUserName";
            this.txtCreateUserName.ReadOnly = true;
            this.txtCreateUserName.Size = new System.Drawing.Size(171, 22);
            this.txtCreateUserName.TabIndex = 44;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label8.Location = new System.Drawing.Point(44, 214);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 43;
            this.label8.Text = "建檔人員";
            // 
            // txtMemo
            // 
            this.txtMemo.Location = new System.Drawing.Point(120, 131);
            this.txtMemo.Multiline = true;
            this.txtMemo.Name = "txtMemo";
            this.txtMemo.ReadOnly = true;
            this.txtMemo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMemo.Size = new System.Drawing.Size(442, 62);
            this.txtMemo.TabIndex = 42;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label7.Location = new System.Drawing.Point(68, 131);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 12);
            this.label7.TabIndex = 41;
            this.label7.Text = "備註";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label5.Location = new System.Drawing.Point(304, 105);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 39;
            this.label5.Text = "管理群組";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label6.Location = new System.Drawing.Point(8, 105);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(89, 12);
            this.label6.TabIndex = 37;
            this.label6.Text = "企業管理者帳號";
            // 
            // txtSQLServer
            // 
            this.txtSQLServer.Location = new System.Drawing.Point(120, 21);
            this.txtSQLServer.Name = "txtSQLServer";
            this.txtSQLServer.ReadOnly = true;
            this.txtSQLServer.Size = new System.Drawing.Size(442, 22);
            this.txtSQLServer.TabIndex = 32;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Blue;
            this.label2.Location = new System.Drawing.Point(14, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 12);
            this.label2.TabIndex = 31;
            this.label2.Text = "SQL Server名稱";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.btnReturn);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(572, 56);
            this.groupBox2.TabIndex = 62;
            this.groupBox2.TabStop = false;
            // 
            // btnReturn
            // 
            this.btnReturn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReturn.Image = ((System.Drawing.Image)(resources.GetObject("btnReturn.Image")));
            this.btnReturn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnReturn.Location = new System.Drawing.Point(476, 15);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(86, 30);
            this.btnReturn.TabIndex = 50;
            this.btnReturn.Text = " 回上一页";
            this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
            // 
            // frmLabelNoDbView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(593, 411);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Name = "frmLabelNoDbView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmLabelNoDbView";
            this.Load += new System.EventHandler(this.frmLabelNoDbView_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtGroupStartNo;
        private System.Windows.Forms.TextBox txtGroupEndNo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rbtMul;
        private System.Windows.Forms.RadioButton rbtSingle;
        private System.Windows.Forms.TextBox txtCheckDate;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtCheckUserName;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtLastModifyDate;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtLastModifyUserName;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtCreateDate;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtCreateUserName;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtMemo;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtSQLServer;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        protected System.Windows.Forms.Button btnReturn;
        private System.Windows.Forms.Button btnCheck;
    }
}