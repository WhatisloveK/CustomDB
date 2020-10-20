using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WinFormClient.Forms
{
    public partial class InputForm : Form
    {
        public bool IsSet { get; set; }

        public string Value { get; set; }

        private bool IsValid => !string.IsNullOrEmpty(Value);

        public InputForm(string label)
        {
            InitializeComponent();

            IsSet = false;

            labelName.Text = label;
        }

        private void buttonCancle_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            Value = textBoxValue.Text;

            if (IsValid)
            {
                IsSet = true;
                Close();
            }

            else
            {
                MessageBox.Show(Constants.EnterNewDb.EnterData);
            }
        }
    }
}
