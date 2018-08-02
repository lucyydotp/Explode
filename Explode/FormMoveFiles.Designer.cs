namespace Explode {
    partial class FormMoveFiles {
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
            this.barPrimary = new System.Windows.Forms.ProgressBar();
            this.barSecondary = new System.Windows.Forms.ProgressBar();
            this.lblPrimary = new System.Windows.Forms.Label();
            this.lblSecondary = new System.Windows.Forms.Label();
            this.btnAbort = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // barPrimary
            // 
            this.barPrimary.Location = new System.Drawing.Point(15, 25);
            this.barPrimary.Name = "barPrimary";
            this.barPrimary.Size = new System.Drawing.Size(486, 23);
            this.barPrimary.TabIndex = 0;
            // 
            // barSecondary
            // 
            this.barSecondary.Location = new System.Drawing.Point(15, 76);
            this.barSecondary.Name = "barSecondary";
            this.barSecondary.Size = new System.Drawing.Size(486, 23);
            this.barSecondary.TabIndex = 1;
            // 
            // lblPrimary
            // 
            this.lblPrimary.AutoSize = true;
            this.lblPrimary.Location = new System.Drawing.Point(12, 9);
            this.lblPrimary.Name = "lblPrimary";
            this.lblPrimary.Size = new System.Drawing.Size(35, 13);
            this.lblPrimary.TabIndex = 2;
            this.lblPrimary.Text = "label1";
            // 
            // lblSecondary
            // 
            this.lblSecondary.AutoSize = true;
            this.lblSecondary.Location = new System.Drawing.Point(12, 60);
            this.lblSecondary.Name = "lblSecondary";
            this.lblSecondary.Size = new System.Drawing.Size(35, 13);
            this.lblSecondary.TabIndex = 3;
            this.lblSecondary.Text = "label2";
            // 
            // btnAbort
            // 
            this.btnAbort.Enabled = false;
            this.btnAbort.Location = new System.Drawing.Point(15, 133);
            this.btnAbort.Name = "btnAbort";
            this.btnAbort.Size = new System.Drawing.Size(75, 23);
            this.btnAbort.TabIndex = 4;
            this.btnAbort.Text = "Abort";
            this.btnAbort.UseVisualStyleBackColor = true;
            // 
            // FormMoveFiles
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(513, 168);
            this.Controls.Add(this.btnAbort);
            this.Controls.Add(this.lblSecondary);
            this.Controls.Add(this.lblPrimary);
            this.Controls.Add(this.barSecondary);
            this.Controls.Add(this.barPrimary);
            this.MaximizeBox = false;
            this.Name = "FormMoveFiles";
            this.Text = "FormMoveFiles";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMoveFiles_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar barPrimary;
        private System.Windows.Forms.ProgressBar barSecondary;
        private System.Windows.Forms.Label lblPrimary;
        private System.Windows.Forms.Label lblSecondary;
        private System.Windows.Forms.Button btnAbort;
    }
}