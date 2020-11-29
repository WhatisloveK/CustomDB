using DB_Engine.Implementations.Servises;
using DB_Engine.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DB_Engine.Factories.Implementations
{
    public class DataBaseServiceFactory : IDataBaseServiceFactory
    {
        private IStorageFactory _storageFactory;
        private IEntityServiceFactory _entityServiceFactory;

        public DataBaseServiceFactory(IEntityServiceFactory entityServiceFactory,
            IStorageFactory storageFactory)
        {
            _storageFactory = storageFactory;
            _entityServiceFactory = entityServiceFactory;
        }

        public IDataBaseService GetDataBaseService(string path)
        {
            return new DataBaseService(path, _storageFactory, _entityServiceFactory);
        }

        public IDataBaseService GetDataBaseService(string name, string rootPath, long fileSize)
        {
            return new DataBaseService(name, rootPath, fileSize,  _storageFactory, _entityServiceFactory);
        }
    }
}
