using Scytale;
using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Windows.Forms;
namespace Scytale
{
    public partial class ScytaleCylinder: Form
    {
        private List<string> filesTobBeUploaded = new List<string>();
        private static readonly ILog Log = LogManager.GetLogger(typeof(ScytaleCylinder));
        public ScytaleCylinder()
        {
            InitializeComponent();
        }

        private void ScytaleCylinder_Load(object sender, EventArgs e)
        {
            XmlConfigurator.Configure();
        }

        // Encrypt the plaintext file.
        private void btnEncrypt_Click(object sender, EventArgs e)
        {
            bool isTobeEncypted = rbEncrypt.Checked;
            try
            {
                btnUpload.Visible = btnEncryptDecrypt.Enabled = rbEncrypt.Enabled = rbDecrypt.Enabled = false;

                if (ValidateInput())
                {
                    string pwd = txtPassword.Text;

                    foreach (var file in inputBrowser.FileNames)
                    {
                        FileInfo inputFile = new FileInfo(file);
                        string outputFile = string.Empty;
                        if (isTobeEncypted)
                        {
                            outputFile = string.Format("{0}\\{1}.ebk", outputBrowserFolder.SelectedPath, inputFile.Name);
                            Crypto.EncryptFile(pwd, inputFile.FullName, outputFile);
                            Log.Info(string.Format("Encrypted file: {0} ", outputFile));
                            filesTobBeUploaded.Add(outputFile);

                        }
                        else
                        {
                            outputFile = string.Format("{0}\\{1}", outputBrowserFolder.SelectedPath, Path.GetFileNameWithoutExtension(inputFile.Name));
                            Crypto.DecryptFile(pwd, inputFile.FullName, outputFile);
                            Log.Info(string.Format("Decrypted file: {0} ", outputFile));
                        }
                    }
                    MessageBox.Show(string.Format("Done! Please Checkout the resultant files in {0}", txtOutputFile.Text), "Attention", MessageBoxButtons.OKCancel);
                    txtPassword.Text = txtInputFile.Text = txtOutputFile.Text = string.Empty;
                    rbEncrypt.Checked = true;


                }


            }
            catch (Exception ex)
            {
                if (ex.GetType() == typeof(CryptographicException))
                {
                    MessageBox.Show("Password is bad!", "Attention", MessageBoxButtons.OKCancel);
                    txtPassword.Focus();
                }
                else
                    MessageBox.Show(ex.Message, "Attention", MessageBoxButtons.OKCancel);

                Log.Error(ex.Message);
            }
            finally
            {

                if (isTobeEncypted) { btnUpload.Visible = true; btnEncryptDecrypt.Enabled = false; }
                else
                { btnEncryptDecrypt.Enabled = rbEncrypt.Enabled = rbDecrypt.Enabled = true; }

            }


        }

        private bool ValidateInput()
        {
            bool isValid = true;
            if (string.IsNullOrEmpty(txtPassword.Text.Trim()))
            {
                MessageBox.Show("Please Enter Key!", "Attention", MessageBoxButtons.OKCancel);
                txtPassword.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtInputFile.Text.Trim()))
            {
                MessageBox.Show("Please select the files!", "Attention", MessageBoxButtons.OKCancel);
                txtInputFile.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtOutputFile.Text.Trim()))
            {
                MessageBox.Show("Please select a folder where files are to be kept!", "Attention", MessageBoxButtons.OKCancel);
                txtOutputFile.Focus();
                return false;
            }
            return isValid;
        }

        private void rbEncrypt_CheckedChanged(object sender, EventArgs e)
        {
            if (rbEncrypt.Checked)
            {
                lblInput.Text = "Files to be Encrypted";
                lblOutput.Text = "Encrypted Files Location";
                lblKey.Text = "Encryption Key";
                btnEncryptDecrypt.Text = "Encypt";
            }
            else
            {
                lblInput.Text = "Files to be Decrypted";
                lblOutput.Text = "Decrypted Files Location";
                lblKey.Text = "Decryption Key";
                btnEncryptDecrypt.Text = "Decrypt";
            }
        }
        private void inputBrowser_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (inputBrowser.SafeFileNames.Length > 1)
                txtInputFile.Text = inputBrowser.FileNames[0];
            else
                txtInputFile.Text = inputBrowser.FileName;

            txtInputFile.Text = string.Format("Directory : -> {0}  Files: -> {1}", new FileInfo(inputBrowser.FileName).DirectoryName, string.Join(" & ", inputBrowser.SafeFileNames));

        }

        private void outputBrowser_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            txtOutputFile.Text = outputBrowserFolder.SelectedPath;
        }

        private void txtInputFile_Click(object sender, EventArgs e)
        {
            inputBrowser.ShowDialog();
        }

        private void txtOutputFile_Click(object sender, EventArgs e)
        {
            DialogResult result = outputBrowserFolder.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtOutputFile.Text = string.Format("Directory : -> {0}", outputBrowserFolder.SelectedPath);
            }
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                AWSUploader aws = new AWSUploader();
                foreach (var file in filesTobBeUploaded)
                {
                    aws.Upload(file);
                    Log.Info(string.Format("{0} sent to AWS", file));
                }
                btnUpload.Visible = false;
                btnEncryptDecrypt.Enabled = true;
                rbEncrypt.Checked = true;
            }
            catch (Exception ex)
            {

                Log.Error(ex.Message, ex.InnerException);
            }


        }
    }
}
