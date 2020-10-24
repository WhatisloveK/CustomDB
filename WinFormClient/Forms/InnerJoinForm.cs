using DB_Engine.Interfaces;
using DB_Engine.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace WinFormClient.Forms
{
    public partial class InnerJoinForm : Form
    {
        private IDataBaseService _dataBaseService;
        private Settings Settings;
        private IEntityService _tableService;

        public bool IsSet { get; set; }
        public Entity SelectedTable { get; set; }
        public List<List<object>> Data { get; set; }
        public InnerJoinForm(IDataBaseService dataBaseService, IEntityService tableService)
        {
            InitializeComponent();
            Settings = new Settings();
            CurrentTableLabel.Text = tableService.Entity.Name;
            tableLayoutPanel.RowStyles[0].Height = Settings.DataBaseButtonHeight;
            Data = new List<List<object>>();

            currentTableColumnComboBox.DataSource = tableService.Entity.Schema.Columns.ToList();

            _tableService = tableService;
            _dataBaseService = dataBaseService;
            IsSet = false;
        }

        private void buttonAddTable_Click(object sender, EventArgs e)
        {
            var size = new Size(Settings.LeftSideButtonWidth, Settings.TableSchemaButtonHeight);
            var guid = Guid.NewGuid();

            var tablesComboBox = new ComboBox()
            {
                DataSource = _dataBaseService.GetTables().ToList(),
                Size = size,
                Name = "TableComboBox"
            };
            tablesComboBox.SelectedValueChanged += AfterTableSelected;

            var columnsComboBox = new ComboBox()
            {
                Size = size,
                Name = "ColumnComboBox"
            };
            columnsComboBox.Visible = false;

            var buttonDelete = new Button()
            {
                Text = Constants.UnionForm.DeleteTable,
                Size = size,
                Name = guid.ToString() + "." + nameof(Button)
            };
            
            buttonDelete.Click += ButtonDelete_Click;
            tableLayoutPanel.Controls.AddRange(new Control[] { buttonDelete, tablesComboBox, columnsComboBox  });
            buttonAddTable.Enabled = false;
        }

        private void AfterTableSelected(object sender, EventArgs e)
        {
            var comboBox = (ComboBox)sender;
            var entityService = (IEntityService)comboBox.SelectedItem;
            var columnComboBox = (ComboBox)tableLayoutPanel.Controls.Cast<Control>().Where(x => x.Name == "ColumnComboBox").FirstOrDefault();
            if(columnComboBox!= null)
            {
                columnComboBox.DataSource = entityService.Entity.Schema.Columns;
                columnComboBox.Visible = true;
            }
            
        }

        private void ButtonDelete_Click(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var nameGuid = button.Name.Split('.').First();

            tableLayoutPanel.Controls.Remove(button);
            tableLayoutPanel.Controls.RemoveByKey(nameGuid + "." + nameof(ComboBox));
            tableLayoutPanel.Controls.RemoveByKey("ColumnComboBox");
            buttonAddTable.Enabled = true;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonApply_Click(object sender, EventArgs e)
        {
            var combobxes = tableLayoutPanel.Controls
                .Cast<Control>()
                .Where(x => x is ComboBox)
                .Select(x => (ComboBox)x)
                .ToList();

            
            
            if (combobxes.Count > 0)
            {
                var currentEntityColumn = (EntityColumn)currentTableColumnComboBox.SelectedItem;
                EntityColumn secondEntityColumn = null;
                
                combobxes.ForEach(x => {
                    switch (x.Name)
                    {
                        case "ColumnComboBox":
                            secondEntityColumn = (EntityColumn)x.SelectedItem;
                            break;
                        case "TableComboBox":
                            SelectedTable  = ((IEntityService)x.SelectedItem).Entity;
                            break;
                        default:
                            break;

                    }
                });
                try
                {
                    Data = _tableService.InnerJoin(SelectedTable, new Tuple<string, string>(currentEntityColumn.Name, secondEntityColumn.Name), false);
                    IsSet = true;
                    Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

    }
}
