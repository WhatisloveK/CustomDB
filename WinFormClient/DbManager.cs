using DB_Engine.Factories;
using DB_Engine.Implementations.Servises;
using DB_Engine.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using WinFormClient.DTO;
using Microsoft.Extensions.DependencyInjection;

namespace WinFormClient
{
    public class DbManager
    {
        IServiceProvider _serviceProvider;
        IDataBaseServiceFactory _dataBaseServiceFactory;
        public DbManager(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

            _dataBaseServiceFactory = _serviceProvider.GetService<IDataBaseServiceFactory>();
        }

        public void OpenDb(string path)
        {
            try
            {
                IDataBaseService dataBaseService = _dataBaseServiceFactory.GetDataBaseService($"{Settings.SqlServerName}|{path}");
                var mainNode = new TreeNode(dataBaseService.DataBase.Name);
                mainNode.Tag = dataBaseService;
                foreach(var item in dataBaseService.DataBase.Entities)
                {
                    IEntityService entityService = dataBaseService.GetEntityService(item.Name);
                    var node = new TreeNode(item.Name);
                    node.Tag = entityService;
                    mainNode.Nodes.Add(node);
                }
                CommonControls.StructureTreeView.Nodes.Add(mainNode);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        internal void CreateDb(DbInfoDTO dbinfo)
        {
            IDataBaseService dataBaseService =
                _dataBaseServiceFactory.GetDataBaseService(dbinfo.Name,
                Settings.SqlServerName, dbinfo.FileSize);
            var mainNode = new TreeNode(dataBaseService.DataBase.Name);
            mainNode.Tag = dataBaseService;
            CommonControls.StructureTreeView.Nodes.Add(mainNode);
             
        }
    }
}
