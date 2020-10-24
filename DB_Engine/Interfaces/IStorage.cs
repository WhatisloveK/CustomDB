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
        void DeleteRange(Entity entity, List<Guid> guids);
        void Update(Entity entity, List<List<object>> rows);
        void Update(Entity entity, Dictionary<string, List<IValidator>> conditions, List<object> updatedRow);
        void Insert(Entity entity, List<List<object>> rows);
        List<List<object>> Select(Entity entity, bool showSystemColumns = true);
        List<List<object>> Select(Entity entity, Dictionary<string, List<IValidator>> conditions, bool showSystemColumns = true);
    }
}
