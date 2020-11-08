using DB_Engine.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DB_Engine.Interfaces
{
    public interface IDbWriter
    {
        public void UpdateDb(DataBase dataBase);
        public void DeleteDb(DataBase dataBase);
        DataBase GetDb(string filePath);
        List<string> GetDbsNames(string rootPath);
    }
}
