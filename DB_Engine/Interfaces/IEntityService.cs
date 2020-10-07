using DB_Engine.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DB_Engine.Interfaces
{
    public interface IEntityService
    {
        Entity Entity { get; }
        void AddColumn(string name, Guid dataValueTypeId, List<IValidator> validators = null);
        void AddColumn(EntityColumn column);
        void DropColumn(string name);
        void Insert(List<object> row);
        void InsertRange(List<List<object>> rows);
        void Delete(Dictionary<string, List<IValidator>> conditions);
        void UpdateRow(Dictionary<string, object> keyValues, List<List<object>> rows);
        IEnumerable<List<object>> Select();
        IEnumerable<List<object>> Select(Dictionary<string, List<IValidator>> conditions);

        //List<List<object>> Union(params Entity[] tables);

        //List<List<object>> CrossJoin(Entity table);
    }
}
