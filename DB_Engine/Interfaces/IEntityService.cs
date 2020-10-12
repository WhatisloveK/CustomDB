using DB_Engine.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DB_Engine.Interfaces
{
    public interface IEntityService
    {
        Entity Entity { get; set; }
        void AddColumn(string name, Guid dataValueTypeId, List<IValidator> validators = null);
        void AddColumn(EntityColumn column);
        void DropColumn(string name);
        void Insert(List<object> row);
        void InsertRange(List<List<object>> rows);
        void Delete(Dictionary<string, List<IValidator>> conditions);
        void Update(Dictionary<string, List<IValidator>> conditions, List<object> row);
        List<List<object>> Select(bool showSystemColumns = true);
        List<List<object>> Select(Dictionary<string, List<IValidator>> conditions, bool showSystemColumns = true);
        List<List<object>> InnerJoin(Entity entity, Tuple<string, string> joinableColumns, bool showSystemColumns = true);
        List<List<object>> CrossJoin(Entity entity, bool showSystemColumns = true);
    }
}
