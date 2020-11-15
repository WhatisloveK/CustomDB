using DB_Engine.Interfaces;
using DB_Engine.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace DB_Engine.Implementations.DbWriters
{
    class JsonDbWriter: IDbWriter
    {
        public void DeleteDb(DataBase dataBase)
        {
            File.Delete($"{dataBase.Info.RootPath}\\{dataBase.Name}{GlobalSetting.Extention}");

        }

        public DataBase GetDb(string filePath)
        {
            string data = File.ReadAllText(filePath);

            var dataBase = JsonSerializer.Deserialize<DataBase>(data);
            return dataBase;

        }

        public List<string> GetDbsNames(string rootPath)
        {

            return Directory.GetFiles(rootPath, $"*{GlobalSetting.Extention}").ToList();
        }

        public void UpdateDb(DataBase dataBase)
        {
            var stringData = JsonSerializer.Serialize(dataBase);


            File.WriteAllText($"{dataBase.Info.RootPath}\\{dataBase.Name}{GlobalSetting.Extention}", stringData);
        }
    }
}
