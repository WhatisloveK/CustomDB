
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WinFormClient;
using WinFormClient.DTO;

namespace WinFormClient.Forms
{
    public partial class CreateDatabaseForm : Form
    {
        public bool IsSet { get; set; }
        public DbInfoDTO Data { get; set; }
        private string _path;

        public CreateDatabaseForm()
        {
            InitializeComponent();

            IsSet = false;

        }

        

        private bool IsValid => !string.IsNullOrEmpty(textBoxName.Text) && !string.IsNullOrEmpty(_path);

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                IsSet = true;
                Data = new DbInfoDTO
                {
                    Name = textBoxName.Text,
                    RootPath = _path,
                    FileSize = (long)numericUpDownFIleSize.Value
                };

                Close();
            }
            else
            {
                MessageBox.Show(Constants.EnterNewDb.EnterData);
            }
        }

        private void buttonPath_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                _path = folderBrowserDialog.SelectedPath;
            }
            else
            {
                MessageBox.Show("Incorrect path");
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
