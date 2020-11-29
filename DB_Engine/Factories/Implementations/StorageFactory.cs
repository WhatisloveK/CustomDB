using DB_Engine.Implementations;
using DB_Engine.Interfaces;
using DB_Engine.Models;

namespace DB_Engine.Factories
{
    public class StorageFactory : IStorageFactory
    {
        ISourceFactory _sourceFactory;
        public StorageFactory(ISourceFactory sourceFactory)
        {
            _sourceFactory = sourceFactory;
        }
        public IStorage GetStorage(DataBase dataBase)
        {

            return new Storage(dataBase, _sourceFactory);
        }
    }
}
