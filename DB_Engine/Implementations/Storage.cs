﻿using DB_Engine.Exceptions;
using DB_Engine.Factories;
using DB_Engine.Interfaces;
using DB_Engine.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DB_Engine.Implementations
{
    public class Storage : IStorage
    {
        public DataBase DataBase { get; set; }
        public Storage(DataBase dataBase)
        {
            DataBase = dataBase;
        }

        public DataBase GetDataBaseFromFile(string filePath)
        {
            string data = File.ReadAllText(filePath);

            var dataBase = JsonSerializer.Deserialize<DataBase>(data);

            return dataBase;
        }

        public void AddColoumn(Entity entity)
        {
            if (entity.Sources == null || entity.Sources.Count == 0)
            {
                AddNewSource(entity);
            }

            Parallel.ForEach(entity.Sources, (source) =>
            {
                var data = source.GetData();
                data?.ForEach(x => x.Add(new object()));

                source.WriteData(data);
            });
        }

        public void DropColumn(Entity entity, int index)
        {
            if (entity.Sources == null || entity.Sources.Count == 0)
            {
                throw new StorageException("The entity is empty!");
            }

            Parallel.ForEach(entity.Sources, (source) =>
            {
                var data = source.GetData();
                data.ForEach(x => x.RemoveAt(index));

                source.WriteData(data);
            });
        }

        public void Delete(Entity entity, Dictionary<string, List<IValidator>> conditions)
        {
            if (entity.Sources == null || entity.Sources.Count == 0)
            {
                throw new StorageException("The entity is empty!");
            }

            Parallel.ForEach(entity.Sources, (source) =>
            {
                var data = source.GetData();
                data.RemoveAll(element =>
                {
                    foreach (var condition in conditions)
                    {
                        var column = entity.Schema.Columns.Find(x => x.Name == condition.Key);
                        var index = entity.Schema.Columns.IndexOf(column);

                        if (!PassAllValidators(condition.Value, element[index]))
                        {
                            return false;
                        }
                    }
                    return true;
                });

                source.WriteData(data);
            });
        }

        public void DeleteEntitySources(Entity entity)
        {
            if (entity.Sources == null || entity.Sources.Count == 0)
            {
                throw new StorageException("The entity is empty!");
            }

            Parallel.ForEach(entity.Sources, source =>
            {
                File.Delete(source.Url);
            });
        }

        public void Insert(Entity entity, List<List<object>> rows)
        {
            if (!(entity.Sources == null || entity.Sources.Count == 0))
            {
                if ((entity.Sources == null || entity.Sources.Count == 0) || entity.Sources.Last().SizeInBytes >= DataBase.Info.FileSize)
                {
                    AddNewSource(entity);
                }

                rows = rows.Where(element =>
                {
                    for (int i = 0; i < element.Count; i++)
                    {
                        if (!PassAllValidators(entity.Schema.Columns[i].Validators, element[i]))
                        {
                            return false;
                        }
                    }
                    return true;
                }).ToList();

                var source = entity.Sources.Last();
                source.WriteData(rows);
            }

        }

        public List<List<object>> Select(Entity entity, bool showSystemColumns = false)
        {
            List<List<object>> result = new List<List<object>>();

            if (entity.Sources == null || entity.Sources.Count == 0)
            {
                return result;
            }

            foreach (var source in entity.Sources)
            {
                
                result = result.Union(source.GetData()).ToList();
            }
            if (!showSystemColumns)
            {
                result.ForEach(item=>item.RemoveAt(0));
            }
            return result;
        }

       
        public List<List<object>> Select(Entity entity, Dictionary<string, List<IValidator>> conditions, bool showSystemColumns = false)
        {
            List<List<object>> result = new List<List<object>>();

            if (entity.Sources == null || entity.Sources.Count == 0)
            {
                return result;
            }

            foreach (var source in entity.Sources)
            {
                var data = source.GetData();
                data = data.Where(element =>
                {
                    foreach (var condition in conditions)
                    {
                        var field = entity.Schema.Columns.Find(x => x.Name == condition.Key);
                        var index = entity.Schema.Columns.IndexOf(field);

                        if (!PassAllValidators(condition.Value, element[index]))
                        {
                            return false;
                        }
                    }
                    return true;
                }).ToList();
                result = result.Union(data).ToList();
            }
            if (!showSystemColumns)
            {
                result.ForEach(item => item.RemoveAt(0));
            }
            return result;
        }


        public void UpdateDataBaseStructure()
        {
            var stringData = JsonSerializer.Serialize(DataBase);

            File.WriteAllText($"{DataBase.Info.RootPath}\\{DataBase.Name}{GlobalSetting.Extention}", stringData);
        }

        private bool PassAllValidators(List<IValidator> validators, object value)
        {
            if (validators == null || validators.Count == 0)
            {
                return true;
            }

            foreach (var predicate in validators)
            {
                if (!predicate.IsValid(value))
                    return false;
            }
            return true;
        }

        private void AddNewSource(Entity entity)
        {
            var source = SourceFactory.GetSourceObject($"{DataBase.Info.RootPath}\\{entity.Name}{entity.Sources.Count + 1}{GlobalSetting.Extention}");

            entity.Sources.Add(source);
            using (File.Create(source.Url)) { }

            UpdateDataBaseStructure();
        }

        public void Update(Entity entity, Dictionary<string, List<IValidator>> conditions, List<object> updatedRow)
        {
            if (entity.Sources == null || entity.Sources.Count == 0)
            {
                throw new StorageException("The entity is empty!");
            }
            Parallel.ForEach(entity.Sources, (source) =>
            {
                var data = source.GetData();
                var selectedForUpdate = data.Where(element =>
                {
                    foreach (var condition in conditions)
                    {
                        var field = entity.Schema.Columns.Find(x => x.Name == condition.Key);
                        var index = entity.Schema.Columns.IndexOf(field);

                        if (!PassAllValidators(condition.Value, element[index]))
                        {
                            return false;
                        }
                    }
                    return true;
                });
                var updatedData = data.Except(selectedForUpdate).ToList();
                selectedForUpdate = selectedForUpdate.Select(item =>
                {
                    for (int i = 1; i < item.Count; i++)
                    {
                        item[i] = updatedRow[i];
                    }
                    return item;
                });
                updatedData.AddRange(selectedForUpdate);


                source.WriteData(data);
            });
        }

        public void Update(Entity entity, List<List<object>> rows)
        {
            if (entity.Sources == null || entity.Sources.Count == 0)
            {
                throw new StorageException("The entity is empty!");
            }

            Parallel.ForEach(entity.Sources, (source) =>
            {
                var data = source.GetData();
                var ids = rows.Select(x => (Guid)x[0]);

                data.RemoveAll(element => ids.Contains(Guid.Parse(element[0].ToString())));
                data.AddRange(rows);

                source.WriteData(data);
            });
        }


    }
}
