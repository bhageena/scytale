namespace Scytale
{
    partial class ScytaleCylinder
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
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblKey = new System.Windows.Forms.Label();
            this.btnEncryptDecrypt = new System.Windows.Forms.Button();
            this.txtOutputFile = new System.Windows.Forms.TextBox();
            this.lblOutput = new System.Windows.Forms.Label();
            this.txtInputFile = new System.Windows.Forms.TextBox();
            this.lblInput = new System.Windows.Forms.Label();
            this.inputBrowser = new System.Windows.Forms.OpenFileDialog();
            this.outputBrowserFolder = new System.Windows.Forms.FolderBrowserDialog();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbEncrypt = new System.Windows.Forms.RadioButton();
            this.rbDecrypt = new System.Windows.Forms.RadioButton();
            this.btnUpload = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtPassword
            // 
            this.txtPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPassword.Location = new System.Drawing.Point(20, 88);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(250, 20);
            this.txtPassword.TabIndex = 0;
            // 
            // lblKey
            // 
            this.lblKey.AutoSize = true;
            this.lblKey.Location = new System.Drawing.Point(18, 64);
            this.lblKey.Name = "lblKey";
            this.lblKey.Size = new System.Drawing.Size(78, 13);
            this.lblKey.TabIndex = 72;
            this.lblKey.Text = "Encryption Key";
            // 
            // btnEncryptDecrypt
            // 
            this.btnEncryptDecrypt.Location = new System.Drawing.Point(171, 269);
            this.btnEncryptDecrypt.Margin = new System.Windows.Forms.Padding(3, 2, 1, 3);
            this.btnEncryptDecrypt.Name = "btnEncryptDecrypt";
            this.btnEncryptDecrypt.Size = new System.Drawing.Size(153, 23);
            this.btnEncryptDecrypt.TabIndex = 2;
            this.btnEncryptDecrypt.TabStop = false;
            this.btnEncryptDecrypt.Text = "Encrypt";
            this.btnEncryptDecrypt.Click += new System.EventHandler(this.btnEncrypt_Click);
            // 
            // txtOutputFile
            // 
            this.txtOutputFile.Cursor = System.Windows.Forms.Cursors.Hand;
            this.txtOutputFile.Location = new System.Drawing.Point(21, 218);
            this.txtOutputFile.Margin = new System.Windows.Forms.Padding(3, 1, 3, 2);
            this.txtOutputFile.Multiline = true;
            this.txtOutputFile.Name = "txtOutputFile";
            this.txtOutputFile.ReadOnly = true;
            this.txtOutputFile.Size = new System.Drawing.Size(426, 20);
            this.txtOutputFile.TabIndex = 3;
            this.txtOutputFile.Click += new System.EventHandler(this.txtOutputFile_Click);
            // 
            // lblOutput
            // 
            this.lblOutput.Location = new System.Drawing.Point(21, 194);
            this.lblOutput.Margin = new System.Windows.Forms.Padding(3, 3, 2, 1);
            this.lblOutput.Name = "lblOutput";
            this.lblOutput.Size = new System.Drawing.Size(200, 24);
            this.lblOutput.TabIndex = 68;
            this.lblOutput.Text = "Encrypted File Location";
            // 
            // txtInputFile
            // 
            this.txtInputFile.Cursor = System.Windows.Forms.Cursors.Hand;
            this.txtInputFile.Location = new System.Drawing.Point(21, 150);
            this.txtInputFile.Margin = new System.Windows.Forms.Padding(1, 1, 3, 2);
            this.txtInputFile.Multiline = true;
            this.txtInputFile.Name = "txtInputFile";
            this.txtInputFile.ReadOnly = true;
            this.txtInputFile.Size = new System.Drawing.Size(426, 32);
            this.txtInputFile.TabIndex = 1;
            this.txtInputFile.Click += new System.EventHandler(this.txtInputFile_Click);
            // 
            // lblInput
            // 
            this.lblInput.Location = new System.Drawing.Point(21, 126);
            this.lblInput.Margin = new System.Windows.Forms.Padding(3, 3, 2, 1);
            this.lblInput.Name = "lblInput";
            this.lblInput.Size = new System.Drawing.Size(200, 24);
            this.lblInput.TabIndex = 66;
            this.lblInput.Text = "Files to be Encrypted";
            // 
            // inputBrowser
            // 
            this.inputBrowser.Multiselect = true;
            this.inputBrowser.RestoreDirectory = true;
            this.inputBrowser.SupportMultiDottedExtensions = true;
            this.inputBrowser.Tag = "txtInputFile";
            this.inputBrowser.FileOk += new System.ComponentModel.CancelEventHandler(this.inputBrowser_FileOk);
            // 
            // outputBrowserFolder
            // 
            this.outputBrowserFolder.Tag = "txtOutputFile";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbEncrypt);
            this.groupBox1.Controls.Add(this.rbDecrypt);
            this.groupBox1.Location = new System.Drawing.Point(132, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(215, 51);
            this.groupBox1.TabIndex = 75;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Select Mode";
            // 
            // rbEncrypt
            // 
            this.rbEncrypt.AutoSize = true;
            this.rbEncrypt.Checked = true;
            this.rbEncrypt.Location = new System.Drawing.Point(18, 20);
            this.rbEncrypt.Name = "rbEncrypt";
            this.rbEncrypt.Size = new System.Drawing.Size(61, 17);
            this.rbEncrypt.TabIndex = 76;
            this.rbEncrypt.TabStop = true;
            this.rbEncrypt.Text = "Encrypt";
            this.rbEncrypt.UseVisualStyleBackColor = true;
            this.rbEncrypt.CheckedChanged += new System.EventHandler(this.rbEncrypt_CheckedChanged);
            // 
            // rbDecrypt
            // 
            this.rbDecrypt.AutoSize = true;
            this.rbDecrypt.Location = new System.Drawing.Point(130, 19);
            this.rbDecrypt.Name = "rbDecrypt";
            this.rbDecrypt.Size = new System.Drawing.Size(62, 17);
            this.rbDecrypt.TabIndex = 75;
            this.rbDecrypt.TabStop = true;
            this.rbDecrypt.Text = "Decrypt";
            this.rbDecrypt.UseVisualStyleBackColor = true;
            // 
            // btnUpload
            // 
            this.btnUpload.Location = new System.Drawing.Point(207, 302);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(75, 23);
            this.btnUpload.TabIndex = 76;
            this.btnUpload.Text = "Upload";
            this.btnUpload.UseVisualStyleBackColor = true;
            this.btnUpload.Visible = false;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // ScytaleCylinder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(459, 333);
            this.Controls.Add(this.btnUpload);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.lblKey);
            this.Controls.Add(this.btnEncryptDecrypt);
            this.Controls.Add(this.txtOutputFile);
            this.Controls.Add(this.lblOutput);
            this.Controls.Add(this.txtInputFile);
            this.Controls.Add(this.lblInput);
            this.Name = "ScytaleCylinder";
            this.Text = "Scytale";
            this.Load += new System.EventHandler(this.ScytaleCylinder_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.TextBox txtPassword;
        internal System.Windows.Forms.Label lblKey;
        internal System.Windows.Forms.Button btnEncryptDecrypt;
        internal System.Windows.Forms.TextBox txtOutputFile;
        internal System.Windows.Forms.Label lblOutput;
        internal System.Windows.Forms.TextBox txtInputFile;
        internal System.Windows.Forms.Label lblInput;
        private System.Windows.Forms.OpenFileDialog inputBrowser;
        private System.Windows.Forms.FolderBrowserDialog outputBrowserFolder;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbEncrypt;
        private System.Windows.Forms.RadioButton rbDecrypt;
        private System.Windows.Forms.Button btnUpload;
    }
}

