using DB_Engine.Factories;
using DB_Engine.Interfaces;
using DB_Engine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace DB_Engine.Implementations.Servises
{
    public class DataBaseService : IDataBaseService
    {
        private IStorage _storage;
        private IStorageFactory _storageFactory;
        private IEntityServiceFactory _entityServiceFactory;
        public DataBase DataBase { get; set; }

        public string Name => DataBase.Name;
       

        private string _dataBaseFile => $"{DataBase.Name}{GlobalSetting.Extention}";
        public IEntityService GetEntityService(string tableName)
        {
            return _entityServiceFactory.GetEntityService(DataBase.Entities.Find(x => x.Name == tableName), _storage);
        }

        public DataBaseService(string name, string rootPath, long fileSize, 
               IStorageFactory storageFactory, IEntityServiceFactory entityServiceFactory)
        {
            _storageFactory = storageFactory;
            _entityServiceFactory = entityServiceFactory;
            
            DataBase = new DataBase
            {
                Name = name,
                Info = new DatabaseInfo { RootPath = rootPath, FileSize = fileSize }
            };
            _storage = _storageFactory.GetStorage(DataBase);

            _storage.UpdateDataBaseStructure();
           
        }

        public DataBaseService(string path, IStorageFactory storageFactory, IEntityServiceFactory entityServiceFactory)
        {
            _storageFactory = storageFactory;
            _entityServiceFactory = entityServiceFactory;
            _storage = _storageFactory.GetStorage(DataBase);
            DataBase = _storage.GetDataBaseFromFile(path);

            _storage.DataBase = DataBase;
        }

        public void AddTable(string tableName)
        {
            if (DataBase.Entities.Any(x => x.Name == tableName))
                throw new ArgumentException($"Entity with name: {tableName} already exist!");

            DataBase.Entities.Add(new Entity { Name = tableName });
            _storage.UpdateDataBaseStructure();
        }

        public void AddTable(string tableName, EntitySchema schema)
        {
            DataBase.Entities.Add(new Entity { Name = tableName, Schema = schema });
            _storage.UpdateDataBaseStructure();
        }

        public void DeleteTable(string tableName)
        {
            var Entity = DataBase.Entities.First(x => x.Name == tableName);
            _storage.DeleteEntitySources(Entity);
            DataBase.Entities.Remove(Entity);
            _storage.UpdateDataBaseStructure();
        }

        public IEnumerable<IEntityService> GetTables()
        {
            foreach (var table in DataBase.Entities)
            {
                yield return new EntityService(table, _storage);
            }
        }
    }
}
