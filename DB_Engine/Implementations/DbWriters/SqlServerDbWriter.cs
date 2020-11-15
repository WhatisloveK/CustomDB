using DB_Engine.Factories;
using DB_Engine.Interfaces;
using DB_Engine.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace DB_Engine.Implementations.DbWriters
{
    class SqlServerDbWriter: IDbWriter
    {
        public void DeleteDb(DataBase dataBase)
        {
            var client = DbProviderFactory.GetSqlServcerClient(dataBase.Info.RootPath, dataBase.Name);
            client.DeleteDatabase();

        }

        public DataBase GetDb(string filePath)
        {
            var data = filePath.Split(GlobalSetting.Delimeter);
            var client = DbProviderFactory.GetSqlServcerClient(data[0], data[1]);

            string dbString = client.GetDb();
            return JsonSerializer.Deserialize<DataBase>(dbString);

        }

        public List<string> GetDbsNames(string rootPath)
        {
            var client = DbProviderFactory.GetSqlServcerClient(rootPath, string.Empty);
            return client.GetDbsNames();

        }

        public void UpdateDb(DataBase dataBase)
        {
            var client = DbProviderFactory.GetSqlServcerClient(dataBase.Info.RootPath, dataBase.Name);
            string stringData = JsonSerializer.Serialize(dataBase);
            client.UpdateOrCreateDb(stringData);

        }
    }
}
