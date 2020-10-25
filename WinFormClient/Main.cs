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
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormClient.Forms;

namespace WinFormClient
{
    public partial class Main : Form
    {
        private DbManager _formManager;

        public Main()
        {
            InitializeComponent();
            InitMenu();
            CommonControls.FlowLayoutPanelTopMenu = flowLayoutPanelTopMenu;
            CommonControls.StructureTreeView = structureTreeView;
            dataGridView.AllowUserToAddRows = false;
            _formManager = new DbManager();

            var contextMenu = ControlsHelper.GetContextMenuStrip(DataGridMenuList);

            dataGridView.ContextMenuStrip = contextMenu;
        }

        private List<(string name, Action<object, EventArgs> action)> MenuItemList(bool isDbMenuList) => isDbMenuList ?
                new List<(string name, Action<object, EventArgs> action)>()
                {
                    (Constants.DbPanelControl.AddNewTable, new Action<object, EventArgs>((o, f) =>
                        {
                            AddTable();
                        })
                    )
                } :
                new List<(string name, Action<object, EventArgs> action)>()
                {
                    (Constants.TableButtonControl.EditSchema, new Action<object, EventArgs>((o, f) =>
                        {
                            EditTableShema();
                        })
                    )
                };

        private List<(string name, Action<object, EventArgs> action)> DataGridMenuList =>
           new List<(string name, Action<object, EventArgs> action)>
           {
                (Constants.MainForm.DeleteSelectedRows, new Action<object, EventArgs>((o, f) =>
                    {
                        DeleteSelectedRows();
                    })),
                (Constants.MainForm.UpdateRow, new Action<object, EventArgs>((o, f) =>
                    {
                        UpdateSelectedRows();
                    })),
           };

        private void UpdateSelectedRows()
        {
            var values = new List<List<object>>();
            var entityService = (IEntityService)structureTreeView.SelectedNode.Tag;
            for (int i = 0; i < dataGridView.SelectedRows.Count; i++)
            {
                var row = new List<object>();
                row.Add(Guid.Parse(dataGridView.SelectedRows[i].Cells[0].Value.ToString()));

                for (var j = 1; j < dataGridView.Columns.Count; j++)
                {
                    
                    string value = dataGridView.SelectedRows[i].Cells[j].Value?.ToString();
                    try
                    {
                        row.Add(DataValueType.GetTypedValue(entityService.Entity.Schema.Columns[j - 1].DataValueType, value));
                    }
                    catch
                    {
                        MessageBox.Show(string.Format(Constants.TableButtonControl.InsertIncorrectData, i + 1, j, value));
                        return;
                    }
                    
                }
                values.Add(row);
            }

            entityService.Update(values);
            MessageBox.Show(Constants.TableButtonControl.UpdatedSuccess);
        }

        private void DeleteSelectedRows()
        {
            var ids = dataGridView.SelectedRows.Cast<DataGridViewRow>().Select(x => Guid.Parse(x.Cells[0].Value.ToString()));

            foreach (DataGridViewRow row in dataGridView.SelectedRows)
            {
                dataGridView.Rows.Remove(row);
            }
            var entityService = (IEntityService)structureTreeView.SelectedNode.Tag;
            entityService.DeleteRange(ids.ToList());
        }

        private void EditTableShema()
        {
            var selectedNodeTableService = (IEntityService)structureTreeView.SelectedNode.Tag;
            new EntitySchemaForm(selectedNodeTableService).ShowDialog();
            selectedNodeTableService.UpdateSchemaStructure();
        }

        private void AddTable()
        {
            var form = new InputForm(Constants.DbPanelControl.NewTableName);

            form.ShowDialog();

            if (form.IsSet)
            {
                var dataBaseService = (IDataBaseService)structureTreeView.SelectedNode.Tag;
                dataBaseService.AddTable(form.Value);
                var tableService = dataBaseService.GetEntityService(form.Value);
                var childNode = new TreeNode(form.Value);
                childNode.Tag = tableService;
                structureTreeView.SelectedNode.Nodes.Add(childNode);
            }
        }

        private void InitMenu()
        {
            foreach (var item in TopMenuList)
            {
                var outItem = new ToolStripMenuItem(item.Key);
                foreach (var inItem in item.Value)
                {
                    var insideItem = new ToolStripMenuItem(inItem.name);
                    insideItem.Click += inItem.action.Invoke;

                    outItem.DropDownItems.Add(insideItem);
                }
                menuStripTop.Items.Add(outItem);
            }
        }

        private void SetContextMenuToDataGrid()
        {
            var contextMenuStrip = new ContextMenuStrip();
            var updateItem = new ToolStripMenuItem("Create table");
            //updateItem.Click += UpdateItem_Click;
            //contextMenuStrip.Items.Add(updateItem);
            //dataGridView1.ContextMenuStrip = contextMenuStrip;
        }

        
        private Dictionary<string, List<(string name, Action<object, EventArgs> action)>> TopMenuList =>
            new Dictionary<string, List<(string name, Action<object, EventArgs> action)>>()
            {
                [Constants.MainForm.Actions] = new List<(string name, Action<object, EventArgs> action)>
                {
                    (Constants.MainForm.Open, new Action<object, EventArgs>((o, f) =>
                    {
                        DialogResult result = openFileDialog.ShowDialog();
                        if (result == DialogResult.OK)
                        {
                           string file = openFileDialog.FileName;

                            _formManager.OpenDb(file);
                        }
                    })),
                    (Constants.MainForm.CreateDB, new Action<object, EventArgs>((o, f) =>
                    {
                        var form = new CreateDatabaseForm();

                        form.ShowDialog();
                        if (form.IsSet)
                        {
                            _formManager.CreateDb(form.Data);
                        }
                    }))
                },

                [Constants.MainForm.Help] = new List<(string name, Action<object, EventArgs> action)>
                {
                    (Constants.MainForm.Instruction, new Action<object, EventArgs>((o, f) =>
                    {
                        MessageBox.Show("TEST");
                    }))
                },
            };

        private void structureTreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if(e.Node.Parent == null)
                {

                    structureTreeView.ContextMenuStrip = ControlsHelper.GetContextMenuStrip(MenuItemList(true));
                }
                else
                {
                    structureTreeView.ContextMenuStrip = ControlsHelper.GetContextMenuStrip(MenuItemList(false));
                }
            }
            if (e.Button == MouseButtons.Left)
            {
                if(e.Node.Parent != null)
                {
                    Select((IEntityService)e.Node.Tag);
                }
            }
        }
        private void Select(IEntityService entityService)
        {
            AddTopMenuButtons();

            FillColumnHeaders(entityService.Entity.Schema.Columns.Skip(1).Select(x => x.Name).ToList());
            FillDataGrid(entityService.Select(), entityService.Entity.Schema.Columns);
        }

        private void FillDataGrid(List<List<object>> rows, List<EntityColumn> columns, bool afterJoin = false)
        {
            dataGridView.Rows.Clear();

            var fieldCount = columns.Count();
            var rowsCount = rows.Count();
            if (rowsCount < 1)
            {
                MessageBox.Show(Constants.TableButtonControl.EmptyTable);
            }
            else
            {
                dataGridView.RowCount = rowsCount;
                dataGridView.ColumnCount = rows[0].Count;
                int rowCount = 0;

                foreach (var row in rows)
                {
                    for (int i = 0; i < fieldCount; i++)
                    {
                        dataGridView.Rows[rowCount].Cells[i].Value = row[i];
                    }
                    rowCount++;
                }
                if(!afterJoin)
                    dataGridView.Columns[0].Visible = false;
                else
                    dataGridView.Columns[0].Visible = true;
            }
        }

        private void FillColumnHeaders(List<string> headers, bool afterJoin = false)
        {
            var columnCount = headers.Count();
            dataGridView.ColumnCount = headers.Count + 1;

            for (int i = 0; i < columnCount; i++)
            {
                if(!afterJoin)
                    dataGridView.Columns[i + 1].Name = headers[i];
                else
                    dataGridView.Columns[i].Name = headers[i];
            }
            if(!afterJoin)
                dataGridView.Columns[0].Visible = false;
            else
                dataGridView.Columns[0].Visible = true;
        }

        private void AddTopMenuButtons()
        {
            CommonControls.FlowLayoutPanelTopMenu.Controls
                .Cast<Control>()
                .Where(x => x.Name.Contains(Constants.SelectedNode.Table))
                .ToList().ForEach(x => CommonControls.FlowLayoutPanelTopMenu.Controls.Remove(x));

            var size = new Size(Settings.TopMenuButtonWidth, Settings.SubButtonHeght);

            var buttonInsert = new Button
            {
                Text = Constants.TableButtonControl.InsertData,
                Size = size,
                Name = nameof(Constants.SelectedNode.Table)
            };
            buttonInsert.Click += ButtonInsert_Click;

            var buttonInnerJoin = new Button
            {
                Text = Constants.TableButtonControl.InnerJoin,
                Size = size,
                Name = Constants.SelectedNode.Table
            };
            buttonInnerJoin.Click += ButtonInnerJoin_Click; ;

            CommonControls.FlowLayoutPanelTopMenu.Controls.AddRange(new Control[]
            {
                buttonInsert,
                buttonInnerJoin
            });
        }


        private void ButtonInnerJoin_Click(object sender, EventArgs e)
        {
            var databaseService = (IDataBaseService)structureTreeView.SelectedNode.Parent.Tag;
            var entityService = (IEntityService)structureTreeView.SelectedNode.Tag;
            var form = new InnerJoinForm(databaseService, entityService);
            form.ShowDialog();
            if (form.IsSet)
            {
                List<string> headers = new List<string>();
                for (int i = 1; i < entityService.Entity.Schema.Columns.Count; i++)
                {
                    headers.Add($"{entityService.Entity.Schema.Columns[i].Name}");
                }

                for (int i = 1; i < form.SelectedTable.Schema.Columns.Count; i++)
                {
                    headers.Add($"{form.SelectedTable.Schema.Columns[i].Name}");
                }
                FillColumnHeaders(headers, true);
                var columns = new List<EntityColumn>();
                columns.AddRange(entityService.Entity.Schema.Columns.Skip(1).ToList());
                columns.AddRange(form.SelectedTable.Schema.Columns.Skip(1).ToList());
                FillDataGrid(form.Data, columns, true);
            }
        }


        private void ButtonInsert_Click(object sender, EventArgs e)
        {
            var entityService = (IEntityService)structureTreeView.SelectedNode.Tag;
            var form = new InsertForm(entityService.Entity.Schema.Columns);
            form.ShowDialog();

            if (form.IsSet)
            {
                entityService.InsertRange(form.Values);

                FillDataGrid(entityService.Select(), entityService.Entity.Schema.Columns);
            }
        }
    }
}
