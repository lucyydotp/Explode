namespace Explode {
    partial class FormMain {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.btnGoUp = new System.Windows.Forms.Button();
            this.txtCurrentDirectory = new System.Windows.Forms.TextBox();
            this.btnForward = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.spltViewers = new System.Windows.Forms.SplitContainer();
            this.lstQuickAccess = new System.Windows.Forms.ListView();
            this.qa_Column0 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.qa_Column1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lstFiles = new System.Windows.Forms.ListView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.spltViewers)).BeginInit();
            this.spltViewers.Panel1.SuspendLayout();
            this.spltViewers.Panel2.SuspendLayout();
            this.spltViewers.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnGoUp
            // 
            this.btnGoUp.BackColor = System.Drawing.SystemColors.Window;
            this.btnGoUp.FlatAppearance.BorderSize = 0;
            this.btnGoUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGoUp.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.btnGoUp.Location = new System.Drawing.Point(60, 0);
            this.btnGoUp.Margin = new System.Windows.Forms.Padding(0);
            this.btnGoUp.Name = "btnGoUp";
            this.btnGoUp.Size = new System.Drawing.Size(30, 30);
            this.btnGoUp.TabIndex = 3;
            this.btnGoUp.Text = "↑";
            this.btnGoUp.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnGoUp.UseVisualStyleBackColor = false;
            this.btnGoUp.Click += new System.EventHandler(this.btnGoUp_Click);
            // 
            // txtCurrentDirectory
            // 
            this.txtCurrentDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCurrentDirectory.Location = new System.Drawing.Point(93, 7);
            this.txtCurrentDirectory.Name = "txtCurrentDirectory";
            this.txtCurrentDirectory.Size = new System.Drawing.Size(1146, 20);
            this.txtCurrentDirectory.TabIndex = 2;
            this.txtCurrentDirectory.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCurrentDirectory_KeyPress);
            // 
            // btnForward
            // 
            this.btnForward.BackColor = System.Drawing.SystemColors.Window;
            this.btnForward.FlatAppearance.BorderSize = 0;
            this.btnForward.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnForward.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.btnForward.Location = new System.Drawing.Point(30, 0);
            this.btnForward.Margin = new System.Windows.Forms.Padding(0);
            this.btnForward.Name = "btnForward";
            this.btnForward.Size = new System.Drawing.Size(30, 30);
            this.btnForward.TabIndex = 1;
            this.btnForward.Text = "→";
            this.btnForward.UseVisualStyleBackColor = false;
            this.btnForward.Click += new System.EventHandler(this.GoForward);
            // 
            // btnBack
            // 
            this.btnBack.BackColor = System.Drawing.SystemColors.Window;
            this.btnBack.FlatAppearance.BorderSize = 0;
            this.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBack.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBack.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnBack.Location = new System.Drawing.Point(0, 0);
            this.btnBack.Margin = new System.Windows.Forms.Padding(0);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(30, 30);
            this.btnBack.TabIndex = 0;
            this.btnBack.Text = "←";
            this.btnBack.UseVisualStyleBackColor = false;
            this.btnBack.Click += new System.EventHandler(this.GoBack);
            // 
            // spltViewers
            // 
            this.spltViewers.BackColor = System.Drawing.SystemColors.Menu;
            this.spltViewers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spltViewers.Location = new System.Drawing.Point(0, 36);
            this.spltViewers.Margin = new System.Windows.Forms.Padding(0);
            this.spltViewers.Name = "spltViewers";
            // 
            // spltViewers.Panel1
            // 
            this.spltViewers.Panel1.Controls.Add(this.lstQuickAccess);
            // 
            // spltViewers.Panel2
            // 
            this.spltViewers.Panel2.Controls.Add(this.lstFiles);
            this.spltViewers.Size = new System.Drawing.Size(1278, 674);
            this.spltViewers.SplitterDistance = 196;
            this.spltViewers.SplitterWidth = 2;
            this.spltViewers.TabIndex = 4;
            // 
            // lstQuickAccess
            // 
            this.lstQuickAccess.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstQuickAccess.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.qa_Column0,
            this.qa_Column1});
            this.lstQuickAccess.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstQuickAccess.FullRowSelect = true;
            this.lstQuickAccess.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lstQuickAccess.Location = new System.Drawing.Point(0, 0);
            this.lstQuickAccess.Name = "lstQuickAccess";
            this.lstQuickAccess.Size = new System.Drawing.Size(196, 674);
            this.lstQuickAccess.TabIndex = 0;
            this.lstQuickAccess.UseCompatibleStateImageBehavior = false;
            this.lstQuickAccess.View = System.Windows.Forms.View.Details;
            // 
            // qa_Column0
            // 
            this.qa_Column0.Text = "";
            this.qa_Column0.Width = 20;
            // 
            // qa_Column1
            // 
            this.qa_Column1.Text = "";
            this.qa_Column1.Width = 181;
            // 
            // lstFiles
            // 
            this.lstFiles.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstFiles.Location = new System.Drawing.Point(0, 0);
            this.lstFiles.Name = "lstFiles";
            this.lstFiles.Size = new System.Drawing.Size(1080, 674);
            this.lstFiles.TabIndex = 0;
            this.lstFiles.UseCompatibleStateImageBehavior = false;
            this.lstFiles.View = System.Windows.Forms.View.Details;
            this.lstFiles.ItemActivate += new System.EventHandler(this.lstFiles_ItemActivate);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.btnBack);
            this.panel1.Controls.Add(this.btnForward);
            this.panel1.Controls.Add(this.txtCurrentDirectory);
            this.panel1.Controls.Add(this.btnGoUp);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1278, 36);
            this.panel1.TabIndex = 5;
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.BackColor = System.Drawing.SystemColors.Window;
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(1242, 3);
            this.button2.Margin = new System.Windows.Forms.Padding(0);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(30, 30);
            this.button2.TabIndex = 4;
            this.button2.Text = "↻";
            this.button2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.Refresh);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(1278, 710);
            this.Controls.Add(this.spltViewers);
            this.Controls.Add(this.panel1);
            this.Name = "FormMain";
            this.Text = "FormMain";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.spltViewers.Panel1.ResumeLayout(false);
            this.spltViewers.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spltViewers)).EndInit();
            this.spltViewers.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnForward;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.SplitContainer spltViewers;
        private System.Windows.Forms.Button btnGoUp;
        public System.Windows.Forms.ListView lstFiles;
        public System.Windows.Forms.TextBox txtCurrentDirectory;
        private System.Windows.Forms.ColumnHeader qa_Column1;
        private System.Windows.Forms.ColumnHeader qa_Column0;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button2;
        public System.Windows.Forms.ListView lstQuickAccess;
    }
}