using DB_Engine.Exceptions;
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
        private Entity Entity { get;  set; }

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
            if (!ValidateDataTypes(row))
                throw new DataValueTypeException("Incorrect data types! Expected: " + 
                    Entity.Schema.Columns.Select(x => DataValueType.GetType(x.DataValueType).Name).Aggregate((x, y) => $"{x}, {y}"));
            row.Insert(0, Guid.NewGuid());
            _storage.Insert(Entity, new List<List<object>> { row });
        }

        public void InsertRange(List<List<object>> rows)
        {
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
            throw new NotImplementedException();
        }
    }
}
