using DB_Engine.Exceptions;
using DB_Engine.Extentions;
using DB_Engine.Interfaces;
using DB_Engine.Models;
using DB_Engine.Types;
using DB_Engine.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace DB_Engine.Implementations.Servises
{
    public class EntityService : IEntityService
    {
        public Entity Entity { get;  set; }

        private readonly IStorage _storage;

        public string Name => Entity.Name;

        public List<EntityColumn> Columns => Entity.Schema.Columns;

        public EntityService(Entity table, IStorage storage)
        {
            Entity = table;

            _storage = storage;
        }

        public void UpdateSchema()
        {
            _storage.UpdateDataBaseStructure();
        }

        public void AddColumn(string name, Guid dataValueTypeId, List<IValidator> validators = null)
        {
            if (Entity.Schema.Columns.Count == 0)
            {
                Entity.Schema.Columns.Add(new EntityColumn { Name = "Id", DataValueType = DataValueType.UniqueidentifierDataValueTypeId, Validators = null });
            }
            if (Entity.Schema.Columns.Select(x => x.Name.ToLower()).Any(x => x == name.ToLower()))
                throw new ArgumentException($"EntityColumn with name: {name} already exist!");

            Entity.Schema.Columns.Add(new EntityColumn { Name = name, DataValueType = dataValueTypeId, Validators = validators });
            _storage.UpdateDataBaseStructure();

            _storage.AddColumn(Entity);
        }

        public void AddColumn(EntityColumn field)
        {
            if (Entity.Schema.Columns.Count == 0)
            {
                Entity.Schema.Columns.Add(new EntityColumn { Name = "Id", DataValueType = DataValueType.UniqueidentifierDataValueTypeId, Validators = null });
            }
            Entity.Schema.Columns.Add(field);
            _storage.UpdateDataBaseStructure();

            _storage.AddColumn(Entity);
        }

        public void DropColumn(string name)
        {
            var field = Entity.Schema.Columns.Find(x => x.Name == name);
            var index = Entity.Schema.Columns.IndexOf(field);
            Entity.Schema.Columns.Remove(field);
            _storage.UpdateDataBaseStructure();

            _storage.DropColumn(Entity, index);
        }

        public void Delete(Dictionary<string, List<IValidator>> conditions)
        { 
            _storage.Delete(Entity, conditions);
        }

        public void Insert(List<object> row)
        {
            if (!ValidateDataTypes(row))
                throw new DataValueTypeException("Incorrect data types! Expected: " + 
                    Entity.Schema.Columns.Select(x => DataValueType.GetType(x.DataValueType).Name).Aggregate((x, y) => $"{x}, {y}"));
            _storage.Insert(Entity, new List<List<object>> { row });
        }

        public void InsertRange(List<List<object>> rows)
        {
            
            if (!rows.All(x => ValidateDataTypes(x)))
                throw new DataValueTypeException("Incorrect data types! Expected: " +
                    Entity.Schema.Columns.Select(x => DataValueType.GetType(x.DataValueType).Name).Aggregate((x, y) => $"{x}, {y}"));
            
            _storage.Insert(Entity, rows);
        }

        public List<List<object>> Select(bool showSystemColumns = true)
        {
            return _storage.Select(Entity, showSystemColumns);
        }


        public List<List<object>> Select(Dictionary<string, List<IValidator>> conditions, bool showSystemColumns = true)
        {
            return _storage.Select(Entity, conditions,showSystemColumns);
        }


        private bool ValidateDataTypes(List<object> row)
        {
            var columns = Entity.Schema.Columns;
            
            if(columns.Count-1 == row.Count)
            {
                for(int i = 0; i < columns.Count - 1; i++)
                {
                    if (!DataValueType.IsValidValue(columns[i+1].DataValueType, row[i]))
                        return false;
                }
                return true;
            }
            return false;
        }

        public void Update(List<List<object>> rows)
        {
            for (int i = 0; i < rows.Count; i++)
            {
                if (!ValidateDataTypes(rows[i].Skip(1).ToList()))
                    throw new ArgumentException("Incorrect data types! Expected: " +
                    Entity.Schema.Columns.Select(x => DataValueType.GetType(x.DataValueType).Name).Aggregate((x, y) => $"{x}, {y}"));
            }
            _storage.Update(Entity, rows);
        }

        public void Update(Dictionary<string, List<IValidator>> conditions, List<object> row)
        {
            if (!ValidateDataTypes(row.Skip(1).ToList()))
            {
                 throw new DataValueTypeException("Incorrect data types! Expected: " +
                    Entity.Schema.Columns.Select(x => DataValueType.GetType(x.DataValueType).Name).Aggregate((x, y) => $"{x}, {y}"));
            }
            _storage.Update(Entity, conditions, row);
        }

        public List<List<object>> InnerJoin(Entity joinableEntity, Tuple<string, string> joinableColumns, bool showSystemColumns = true)
        {
            var firstColumn = Entity.Schema.Columns.Find(item => item.Name == joinableColumns.Item1);
            var secondColumn = joinableEntity.Schema.Columns.Find(item => item.Name == joinableColumns.Item2);
            if(firstColumn.DataValueType!= secondColumn.DataValueType)
            {
                throw new DataValueTypeException("Column types selected for join operation didn't match!\n" +
                    $"{firstColumn.Name} columnType = {DataValueType.GetType(firstColumn.DataValueType).Name}\n" +
                    $"{secondColumn.Name} columnType = {DataValueType.GetType(secondColumn.DataValueType).Name}");
            }
            int indexOfFirstColumnEntity = Entity.Schema.Columns.IndexOf(firstColumn),
                indexOfSecondColumnEntity = joinableEntity.Schema.Columns.IndexOf(secondColumn);
            
            var type = DataValueType.GetType(firstColumn.DataValueType);

            IEnumerable<List<object>>  result = null;
            if (showSystemColumns)
            {
                result = from first in _storage.Select(Entity, showSystemColumns).ToList()
                             join second in _storage.Select(joinableEntity, showSystemColumns).ToList()
                             on first[indexOfFirstColumnEntity].ToString() equals second[indexOfSecondColumnEntity].ToString()
                             select first.Concat(second).ToList();
            }
            else
            {
                result = from first in _storage.Select(Entity, showSystemColumns).ToList()
                             join second in _storage.Select(joinableEntity, showSystemColumns).ToList()
                             on first[indexOfFirstColumnEntity-1].ToString() equals second[indexOfSecondColumnEntity-1].ToString()
                             select first.Concat(second).ToList();
            }
            

            return result.ToList();
        }

        public List<List<object>> CrossJoin(Entity entity, bool showSystemColumns = true)
        {
            List<List<object>> result = _storage.Select(Entity, showSystemColumns);
            var CartesianJoin = result.CrossJoin(_storage.Select(entity,showSystemColumns)).ToList();

            result = CartesianJoin.Select(t =>
            {
                var result2 = new List<object>();
                result2.AddRange(t.Item1);
                result2.AddRange(t.Item2);
                return result2;
            }).ToList();
            return result;
        }

        public void UpdateSchemaStructure()
        {
            _storage.UpdateDataBaseStructure();
        }

        public void DeleteRange(List<Guid> guids)
        {
            _storage.DeleteRange(Entity, guids);
        }

        public override string ToString()
        {
            return Entity.Name;
        }
    }
}
