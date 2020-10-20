
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using DB_Engine.Interfaces;
using DB_Engine.Models;

namespace WinFormClient.Forms
{
    public partial class EntitySchemaForm : Form
    {
        private IEntityService _tableService;
        private Settings _settings;

        public EntitySchemaForm(IEntityService tableService)
        {
            InitializeComponent();
            _settings = new Settings();

            _tableService = tableService;

            SetFields();
        }

        private void SetFields()
        {
            tableLayoutPanelMain.Controls.Clear();

            InsertHeader();

            foreach (var column in _tableService.Entity.Schema.Columns)
            {
                AddExistingFieldRedactor(column);
            }
        }

        private void InsertHeader()
        {
            tableLayoutPanelMain.RowStyles[0].Height = _settings.DataBaseButtonHeight;

            var addFieldButton = new Button()
            {
                Text = Constants.TableSchemaForm.AddField,
                Size = new Size(_settings.LeftSideButtonWidth, _settings.TableSchemaButtonWidth)
            };

            addFieldButton.Click += AddFieldButton_Click;
            tableLayoutPanelMain.Controls.Add(addFieldButton);


            tableLayoutPanelMain.Controls.Add(new Label() 
            { 
                Text = Constants.TableSchemaForm.FieldName, 
                Dock = DockStyle.Fill 
            });
            tableLayoutPanelMain.Controls.Add(new Label() 
            { 
                Text = Constants.TableSchemaForm.FieldType, 
                Dock = DockStyle.Fill 
            });
        }

        private void AddFieldButton_Click(object sender, EventArgs e)
        {
            var form = new AddFieldForm();

            form.ShowDialog();

            if (form.IsSet)
            {
                _tableService.AddColumn(form.Column);
                AddExistingFieldRedactor(form.Column);
            }
        }

        private void AddExistingFieldRedactor(EntityColumn column)
        {
            var size = new Size(_settings.LeftSideButtonWidth, _settings.TableSchemaButtonHeight);

            var delteButton = new Button() { Text = Constants.TableSchemaForm.DeleteField, Tag = column, 
                Size = new Size(_settings.LeftSideButtonWidth, _settings.TableSchemaButtonWidth) };
            delteButton.Click += DeleteButton_Click;

            var textbox = new TextBox() {Name="Name"+column.Name, Text = column.Name, Size = size, Tag = column};
            textbox.LostFocus += Textbox_LostFocus;

            var textboxType = new TextBox() { Text = Constants.SupportedTypes.Where(item=>item.Value == column.DataValueType).First().Key, Size = size, Enabled = false};

                
            tableLayoutPanelMain.Controls.AddRange(new Control[] { delteButton, textbox, textboxType });
        }

        private void Textbox_LostFocus(object sender, EventArgs e)
        {
            var textbox = (TextBox)sender;

            var column = (EntityColumn)(textbox).Tag;

            column.Name = textbox.Text;
        }


        private void DeleteButton_Click(object sender, EventArgs e)
        {
            var button = (Button)sender;

            var column = (EntityColumn)button.Tag;

            _tableService.DropColumn(column.Name);

            SetFields();
        }
    }
}
