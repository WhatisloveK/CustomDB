using DB_Engine.Exceptions;
using DB_Engine.Extentions;
using DB_Engine.Interfaces;
using DB_Engine.Models;
using DB_Engine.Types;
using DB_Engine.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DB_Engine.Implementations.Servises
{
    public class EntityService : IEntityService
    {
        public Entity Entity { get;  set; }

        private IStorage _storage;

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

            _storage.AddColoumn(Entity);
        }

        public void AddColumn(EntityColumn field)
        {
            Entity.Schema.Columns.Add(field);
            _storage.UpdateDataBaseStructure();

            _storage.AddColoumn(Entity);
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
            row.Insert(0, Guid.NewGuid());
            if (!ValidateDataTypes(row))
                throw new DataValueTypeException("Incorrect data types! Expected: " + 
                    Entity.Schema.Columns.Select(x => DataValueType.GetType(x.DataValueType).Name).Aggregate((x, y) => $"{x}, {y}"));
            _storage.Insert(Entity, new List<List<object>> { row });
        }

        public void InsertRange(List<List<object>> rows)
        {
            rows.ForEach((item) =>
            {
                item.Insert(0, Guid.NewGuid());
            });
            if (!rows.All(x => ValidateDataTypes(x)))
                throw new DataValueTypeException("Incorrect data types! Expected: " +
                    Entity.Schema.Columns.Select(x => DataValueType.GetType(x.DataValueType).Name).Aggregate((x, y) => $"{x}, {y}"));
            
            _storage.Insert(Entity, rows);
        }

        public List<List<object>> Select()
        {
            return _storage.Select(Entity);
        }


        public List<List<object>> Select(Dictionary<string, List<IValidator>> conditions)
        {
            return _storage.Select(Entity, conditions);
        }


        private bool ValidateDataTypes(List<object> row)
        {
            var columns = Entity.Schema.Columns;
            
            if(columns.Count == row.Count)
            {
                for(int i = 0; i < columns.Count; i++)
                {
                    if (!DataValueType.IsValidValue(columns[i].DataValueType, row[i]))
                        return false;
                }
                return true;
            }
            return false;
        }

        public void Update(Dictionary<string, List<IValidator>> conditions, List<object> row)
        {
            if (!ValidateDataTypes(row))
            {
                 throw new DataValueTypeException("Incorrect data types! Expected: " +
                    Entity.Schema.Columns.Select(x => DataValueType.GetType(x.DataValueType).Name).Aggregate((x, y) => $"{x}, {y}"));
            }
            _storage.Update(Entity, conditions, row);
        }

        public List<List<object>> InnerJoin(Entity joinableEntity, Tuple<string, string> joinableColumns)
        {
            var firstColumn = Entity.Schema.Columns.Find(item => item.Name == joinableColumns.Item1);
            var secondColumn = joinableEntity.Schema.Columns.Find(item => item.Name == joinableColumns.Item2);
            if(firstColumn.DataValueType!= secondColumn.DataValueType)
            {
                throw new DataValueTypeException("Column types selected for join operation doesnt match");
            }
            int indexOfFirstColumnEntity = Entity.Schema.Columns.IndexOf(firstColumn),
                indexOfSecondColumnEntity = joinableEntity.Schema.Columns.IndexOf(secondColumn);
            
            var type = DataValueType.GetType(firstColumn.DataValueType);

            var result = from first in _storage.Select(Entity).ToList()
                         join second in _storage.Select(joinableEntity).ToList()
                         on first[indexOfFirstColumnEntity].ToString() equals second[indexOfSecondColumnEntity].ToString()
                         select first.Concat(second).ToList();
            return result.ToList();
        }

        public List<List<object>> CrossJoin(Entity entity)
        {
            List<List<object>> result = _storage.Select(Entity);
            var CartesianJoin = result.CrossJoin(_storage.Select(entity)).ToList();

            result = CartesianJoin.Select(t =>
            {
                var result2 = new List<object>();
                result2.AddRange(t.Item1);
                result2.AddRange(t.Item2);
                return result2;
            }).ToList();
            return result;
        }
    }
}
