using DB_Engine.Implementations;
using DB_Engine.Interfaces;
using DB_Engine.Models;

namespace DB_Engine.Factories
{
    public class StorageFactory
    {
        public static IStorage GetStorage(DataBase dataBase)
        {
            return new Storage(dataBase);
        }
    }
}
