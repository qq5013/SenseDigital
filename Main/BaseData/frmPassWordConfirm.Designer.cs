namespace Main.BaseData
{
    partial class frmPassWordConfirm
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
            this.txtPassWord = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPassWordConfirm = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnCreatUser = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtPassWord
            // 
            this.txtPassWord.Location = new System.Drawing.Point(99, 34);
            this.txtPassWord.Name = "txtPassWord";
            this.txtPassWord.Size = new System.Drawing.Size(171, 22);
            this.txtPassWord.TabIndex = 31;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(43, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 30;
            this.label1.Text = "新  密  碼";
            // 
            // txtPassWordConfirm
            // 
            this.txtPassWordConfirm.Location = new System.Drawing.Point(99, 70);
            this.txtPassWordConfirm.Name = "txtPassWordConfirm";
            this.txtPassWordConfirm.Size = new System.Drawing.Size(171, 22);
            this.txtPassWordConfirm.TabIndex = 33;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Blue;
            this.label2.Location = new System.Drawing.Point(43, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 32;
            this.label2.Text = "企業編號";
            // 
            // btnCreatUser
            // 
            this.btnCreatUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCreatUser.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnCreatUser.Location = new System.Drawing.Point(192, 126);
            this.btnCreatUser.Name = "btnCreatUser";
            this.btnCreatUser.Size = new System.Drawing.Size(80, 30);
            this.btnCreatUser.TabIndex = 54;
            this.btnCreatUser.Text = "取消";
            this.btnCreatUser.Click += new System.EventHandler(this.btnCreatUser_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnSave.Location = new System.Drawing.Point(58, 126);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(84, 30);
            this.btnSave.TabIndex = 53;
            this.btnSave.Text = "確認";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // frmPassWordConfirm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(314, 174);
            this.Controls.Add(this.btnCreatUser);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtPassWordConfirm);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtPassWord);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPassWordConfirm";
            this.Text = "確認密碼";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtPassWord;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPassWordConfirm;
        private System.Windows.Forms.Label label2;
        protected System.Windows.Forms.Button btnCreatUser;
        protected System.Windows.Forms.Button btnSave;
    }
}