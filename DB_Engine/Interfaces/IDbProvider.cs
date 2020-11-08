using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DB_Engine.Interfaces
{
    public interface IDbProvider
    {
        void UpdateOrCreateDb(string stringDbData);
        void CreateTable();
        void DeleteTable();
        List<string> GetData();
        Task<List<string>> GetDataAsync();
        void InsertData(List<string> data);
        Task InsertDataAsync(List<string> data);
        void ClearTable();
        void DeleteDatabase();
        string GetDb();
        List<string> GetDbsNames();
    }
}
