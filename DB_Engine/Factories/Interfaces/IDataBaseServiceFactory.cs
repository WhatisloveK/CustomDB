using DB_Engine.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DB_Engine.Factories
{
    public interface IDataBaseServiceFactory
    {
        IDataBaseService GetDataBaseService(string path);
        IDataBaseService GetDataBaseService(string name, string rootPath, long fileSize);
    }
}
