using DB_Engine.Interfaces;
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
                var selectedNode = e.Node;
                if(selectedNode.Parent == null)
                {
                    ContextMenuStrip = ControlsHelper.GetContextMenuStrip(MenuItemList(true));
                }
                else
                {
                    ContextMenuStrip = ControlsHelper.GetContextMenuStrip(MenuItemList(false));
                }
            }
        }
    }
}
