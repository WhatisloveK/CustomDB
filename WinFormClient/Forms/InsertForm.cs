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
using System.Windows.Forms.VisualStyles;

namespace WinFormClient.Forms
{
    public partial class InsertForm : Form
    {
        public bool IsSet { get; set; }

        public List<List<object>> Values { get; set; }

        private List<EntityColumn> _columns;

        public InsertForm(List<EntityColumn> columns)
        {
            InitializeComponent();

            _columns = columns;
            SetHeader();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void SetHeader()
        {
            var fieldCount = _columns.Count;
            dataGridViewData.ColumnCount = _columns.Count-1;

            for (int i = 1; i < fieldCount; i++)
            {
                dataGridViewData.Columns[i-1].Name = _columns[i].Name;
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            Values = new List<List<object>>();

            for (int i = 0; i < dataGridViewData.Rows.Count - 1; i++)
            {
                var row = new List<object>();

                for(var j = 0; j < dataGridViewData.Columns.Count; j++)
                {
                    
                    string value = dataGridViewData.Rows[i].Cells[j].Value?.ToString();
                    try
                    {
                        row.Add(DataValueType.GetTypedValue(_columns[j+1].DataValueType, value));
                    }
                    catch
                    {
                        Values.Clear();
                        IsSet = false;
                        MessageBox.Show(string.Format(Constants.TableButtonControl.InsertIncorrectData, i + 1, j + 1, value));
                        return;
                    }
                    
                }
                Values.Add(row); 
            }
            IsSet = true;

            Close();
        }

        
    }
}
