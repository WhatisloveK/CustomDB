using DB_Engine.Interfaces;
using DB_Engine.Models;
using DB_Engine.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WinFormClient.Forms
{
    public partial class AddFieldForm : Form
    {
        public EntityColumn Column { get; set; }
        public bool IsSet { get; set; }
        private List<IValidator> validators;
        public AddFieldForm()
        {
            InitializeComponent();
            validators = new List<IValidator>();
            comboBoxType.DataSource = Constants.SupportedTypes.Keys.ToList();

            IsSet = false;
            Column = new EntityColumn();
        }

        private bool IsValid => !string.IsNullOrEmpty(textBoxName.Text);

        //private void buttonValidators_Click(object sender, EventArgs e)
        //{
        //    if (comboBoxType.SelectedItem != null)
        //    {
        //        var form = new ValidatorsForm(validators, (SupportedTypes)comboBoxType.SelectedItem, false);

        //        form.ShowDialog();

        //        if (form.IsChanged)
        //        {
        //            validators = form.Validators;
        //            if (validators.Count > 0)
        //            {
        //                comboBoxType.Enabled = false;
        //            }
        //            else
        //            {
        //                comboBoxType.Enabled = true;
        //            }
        //        }
        //    }
        //    else
        //    {
        //        MessageBox.Show(Constants.TableSchemaForm.ChooseType);
        //    }
        //}

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                IsSet = true;

                Column.Name = textBoxName.Text;
                Column.DataValueType = Constants.SupportedTypes[(string)comboBoxType.SelectedItem];
                Column.Validators = validators;

                Close();
            }

            else
            {
                MessageBox.Show(Constants.EnterNewDb.EnterData);
            }
        }
    }
}
