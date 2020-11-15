using DB_Engine.Factories;
using DB_Engine.Interfaces;
using DB_Engine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DB_Engine.Implementations
{
    public class DbSource : ISource
    {
        public string Url { get; set;  }

        public string Type =>  typeof(DbSource).AssemblyQualifiedName;

        public long SizeInBytes  => default;

        protected IDbProvider _dbProvider;
        protected IDbProvider DbProvider
        {
            get
            {

                if (_dbProvider == null)
                {
                    var data = Url.Split(GlobalSetting.Delimeter);
                    _dbProvider = DbProviderFactory.GetMongoClient(data[0], data[1], data[2]);
                }

                return _dbProvider;
            }
            set
            {
                _dbProvider = value;
            }
        }

        protected void Create()
        {
            DbProvider.CreateTable();

        }

        public void Delete()
        {
            DbProvider.DeleteTable();

        }

        public List<List<object>> GetData()
        {
            var listStringData = DbProvider.GetData();


            var data = listStringData.Select(x => JsonSerializer.Deserialize<List<object>>(x)).ToList();
            return data;
        }

        public async Task<List<List<object>>> GetDataAsync()
        {
            var listStringData = await DbProvider.GetDataAsync();


            var data = listStringData.Select(x => JsonSerializer.Deserialize<List<object>>(x)).ToList();
            return data;
        }

        public void WriteData(List<List<object>> data)
        {
            if (!(data == null || data.Count == 0))
            {

                var newStringData = data.Select(x => JsonSerializer.Serialize(x)).ToList();
                DbProvider.ClearTable();
                DbProvider.InsertData(newStringData);
            }
        }

        public async Task WriteDataAsync(List<List<object>> data)
        {
            if (!(data == null || data.Count == 0))
            {

                var newStringData = data.Select(x => JsonSerializer.Serialize(x)).ToList();

                await DbProvider.InsertDataAsync(newStringData);
            }
        }

        public void SetUrl(DataBase db, Entity table)
        {

            Url = $"{db.Info.RootPath}{GlobalSetting.Delimeter}{db.Name}{GlobalSetting.Delimeter}{table.Name}";
            Create();
        }
    }
}
