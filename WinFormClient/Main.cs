using DB_Engine.Interfaces;
using DB_Engine.Models;
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
            _formManager = new DbManager();
        }

        private List<(string name, Action<object, EventArgs> action)> MenuItemList(bool isDbMenuList) => isDbMenuList ?
            new List<(string name, Action<object, EventArgs> action)>()
               {
                    (Constants.DbPanelControl.AddNewTable, new Action<object, EventArgs>((o, f) =>
                        {
                            AddTable();
                        })
                    )
               } 
            :
            new List<(string name, Action<object, EventArgs> action)>()
            {
                (Constants.TableButtonControl.EditSchema, new Action<object, EventArgs>((o, f) =>
                    {
                        EditTableShema();
                    })
                )
            };

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
                    ContextMenuStrip = ControlsHelper.GetContextMenuStrip(MenuItemList(true));
                }
                else
                {
                    ContextMenuStrip = ControlsHelper.GetContextMenuStrip(MenuItemList(false));
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

            FillColumnHeaders(entityService.Entity.Schema.Columns.Select(x => x.Name).ToList());
            FillDataGrid(entityService.Select(), entityService.Entity.Schema.Columns);
        }

        private void FillDataGrid(List<List<object>> rows, List<EntityColumn> columns)
        {
            dataGridView.Rows.Clear();

            var fieldCount = columns.Count() + 1;
            var rowsCount = rows.Count();
            if (rowsCount < 1)
            {
                MessageBox.Show(Constants.TableButtonControl.EmptyTable);
            }
            else
            {
                dataGridView.RowCount = rowsCount;
                int rowCount = 0;

                foreach (var row in rows)
                {
                    for (int i = 0; i < fieldCount; i++)
                    {
                        dataGridView.Rows[rowCount].Cells[i].Value = row[i];
                    }
                    rowCount++;
                }
                dataGridView.Columns[0].Visible = false;
            }
        }

        private void FillColumnHeaders(List<string> headers)
        {
            var columnCount = headers.Count();
            dataGridView.ColumnCount = headers.Count + 1;

            for (int i = 0; i < columnCount; i++)
            {
                dataGridView.Columns[i + 1].Name = headers[i];
            }
            dataGridView.Columns[0].Visible = false;
        }

        private void AddTopMenuButtons()
        {
            CommonControls.FlowLayoutPanelTopMenu.Controls
                .Cast<Control>()
                .Where(x => x.Name.Contains(Constants.SelectedNode.Table))
                .ToList().ForEach(x => CommonControls.FlowLayoutPanelTopMenu.Controls.Remove(x));

            var size = new Size(Settings.TopMenuButtonWidth, Settings.SubButtonHeght);

            var buttonConditions = new Button
            {
                Text = Constants.TableButtonControl.AddConditions,
                Size = size,
                Name = nameof(Constants.SelectedNode.Table)
            };
            buttonConditions.Click += ButtonConditions_Click;

            var buttonInsert = new Button
            {
                Text = Constants.TableButtonControl.InsertData,
                Size = size,
                Name = nameof(Constants.SelectedNode.Table)
            };
            buttonInsert.Click += ButtonInsert_Click;

            var buttonDelete = new Button
            {
                Text = Constants.TableButtonControl.DeleteData,
                Size = size,
                Name = Constants.SelectedNode.Table
            };
            buttonDelete.Click += ButtonDelete_Click;

            var buttonUnion = new Button
            {
                Text = Constants.TableButtonControl.UnionTables,
                Size = size,
                Name = Constants.SelectedNode.Table
            };
            buttonUnion.Click += ButtonUnion_Click; ;

            CommonControls.FlowLayoutPanelTopMenu.Controls.AddRange(new Control[]
            {
                buttonConditions,
                buttonInsert,
                buttonDelete,
                buttonUnion
            });
        }


        private void ButtonUnion_Click(object sender, EventArgs e)
        {
            //var form = new UnionTablesForm(_dataBaseService, _tableService);
            //form.ShowDialog();
            //if (form.IsSet)
            //{
            //    List<string> headers = new List<string>();
            //    for (int i = 0; i < _tableService.Table.Schema.Fields.Count; i++)
            //    {
            //        headers.Add($"Column {i + 1}({_tableService.Table.Schema.Fields[i].Type.GetName()})");
            //    }

            //    FillColumnHeaders(headers);
            //    FillDataGrid(form.Data);
            //}
        }

        private void ButtonConditions_Click(object sender, EventArgs e)
        {
            //var form = new SelectConditionsForm(_tableService.Table.Schema.Fields, false);
            //form.ShowDialog();

            //if (form.IsSet)
            //{
            //    FillColumnHeaders(_tableService.Table.Schema.Fields.Select(x => x.Name).ToList());

            //    if (form.SelectConditions.Validators.Any())
            //    {
            //        FillDataGrid(_tableService.Select(form.SelectConditions.Top,
            //            form.SelectConditions.Offset, form.SelectConditions.Validators));
            //    }
            //    else
            //    {
            //        FillDataGrid(_tableService.Select(form.SelectConditions.Top,
            //            form.SelectConditions.Offset));
            //    }
            //}
        }

        private void ButtonDelete_Click(object sender, EventArgs e)
        {
            //var form = new SelectConditionsForm(_tableService.Table.Schema.Fields, true);
            //form.ShowDialog();

            //if (form.IsSet)
            //{
            //    if (form.SelectConditions.Validators.Any())
            //    {
            //        _tableService.DeleteRows(form.SelectConditions.Validators);

            //        FillDataGrid(_tableService.Select(100, 0));
            //    }
            //}
        }

        private void ButtonInsert_Click(object sender, EventArgs e)
        {
            //var form = new InsertForm(_tableService.Table.Schema.Fields);
            //form.ShowDialog();

            //if (form.IsSet)
            //{
            //    _tableService.InsertDataRange(form.Values);

            //    FillDataGrid(_tableService.Select(100, 0));
            //}
        }
    }
}
