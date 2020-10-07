using DB_Engine.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DB_Engine.Interfaces
{
    public interface IDataBaseService
    {
        DataBase DataBase { get; }
        IEntityService GetEntityService(string tableName); 
        void AddTable(string tableName);
        //void AddTable(string tableName, EntitySchema schema);
        void DeleteTable(string tableName);
    }
}
