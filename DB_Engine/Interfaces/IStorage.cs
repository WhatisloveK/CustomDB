using DB_Engine.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DB_Engine.Interfaces
{
    public interface IStorage
    {
        DataBase DataBase { get; set; }
        DataBase GetDataBaseFromFile(string filePath);
        void UpdateDataBaseStructure();
        void DeleteEntitySources(Entity entity);
        void AddColoumn(Entity entity);
        void DropColumn(Entity entity, int index);
        void Delete(Entity entity, Dictionary<string, List<IValidator>> conditions);
        void Update(Entity entity, Dictionary<string, List<IValidator>> conditions);
        void Insert(Entity entity, List<object> row);
        void InsertRange(Entity entity, List<List<object>> rows);
        IEnumerable<List<object>> Select(Entity entity);
        //IEnumerable<List<object>> Select(Entity entity, int top, int offset);
        IEnumerable<List<object>> Select(Entity entity, Dictionary<string, List<IValidator>> conditions);
        //IEnumerable<List<object>> Select(Entity entity, int top, int offset, Dictionary<string, List<IValidator>> conditions);
    }
}
