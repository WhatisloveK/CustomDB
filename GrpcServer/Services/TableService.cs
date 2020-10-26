using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DB_Engine.Exceptions;
using DB_Engine.Implementations.Servises;
using DB_Engine.Interfaces;
using DB_Engine.Models;
using DB_Engine.Types;
using Google.Protobuf.Collections;
using Grpc.Core;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace GrpcServer.Services
{
    public class TableService : EntityService.EntityServiceBase
    {
        private string root = "D:\\Programming\\4term\\IT\\DBFILES\\grpc\\";
        private IValidator CreateValidator(Validator validator)
        {
            var type = DataValueType.GetType(Guid.Parse(validator.DataValueTypeId));
            
            switch (type)
            {
                case var t when t == typeof(string):
                    return new DB_Engine.Validators.Validator<string>((DB_Engine.Interfaces.ComparsonType)validator.ComparsonType, validator.Value);
                case var t when t == typeof(int):
                    return new DB_Engine.Validators.Validator<int>((DB_Engine.Interfaces.ComparsonType)validator.ComparsonType, int.Parse(validator.Value));
                case var t when t == typeof(char):
                    return new DB_Engine.Validators.Validator<char>((DB_Engine.Interfaces.ComparsonType)validator.ComparsonType, char.Parse(validator.Value));
                case var t when t == typeof(double):
                    return new DB_Engine.Validators.Validator<double>((DB_Engine.Interfaces.ComparsonType)validator.ComparsonType, double.Parse(validator.Value));
                default:
                    throw new DataValueTypeException($"Incorrect DataValueTypeId specified. IncorrectDataValueTypeId = \"{validator.DataValueTypeId}\"");
            }
        }
        public override Task<BaseReply> AddColumn(AddColumnRequest request, ServerCallContext context)
        {
            try
            {
                IDataBaseService databaseService = new DataBaseService(root + request.DbName + ".vldb");
                IEntityService entityService = databaseService.GetEntityService(request.TableName);
                var validators = new List<IValidator>();

                foreach(var item in request.Validators)
                {
                    validators.Add(CreateValidator(item));
                }

                var entityColumn = new EntityColumn() { DataValueType = Guid.Parse(request.DataValueTypeId), Name = request.ColumnName, Validators = validators };
                entityService.AddColumn(entityColumn);
                var response = new BaseReply()
                {
                    Code = 200
                };

                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("***************************************************************************************************************");
                Console.WriteLine("Column added: " + request.TableName + ", " + request.ColumnName);
                Console.WriteLine("***************************************************************************************************************");

                return Task.FromResult(response);
            }
            catch (Exception ex)
            {
                return Task.FromResult(new BaseReply() { Code = 400, Message = ex.Message, StackTrace = ex.StackTrace });
            }
        }
        public override Task<BaseReply> DropColumn(DropColumnRequst request, ServerCallContext context)
        {
            try
            {
                IDataBaseService databaseService = new DataBaseService(root + request.DbName + ".vldb");
                IEntityService entityService = databaseService.GetEntityService(request.TableName);
                entityService.DropColumn(request.ColumnName);
                var response = new BaseReply()
                {
                    Code = 200
                };

                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("***************************************************************************************************************");
                Console.WriteLine("Column dropped: " + request.TableName + ", " + request.ColumnName);
                Console.WriteLine("***************************************************************************************************************");

                return Task.FromResult(response);
            }
            catch (Exception ex)
            {
                return Task.FromResult(new BaseReply() { Code = 400, Message = ex.Message, StackTrace = ex.StackTrace });
            }
        }

        public override Task<BaseReply> Delete(DeleteRequest request, ServerCallContext context)
        {
            try
            {
                IDataBaseService databaseService = new DataBaseService(root + request.DbName + ".vldb");
                IEntityService entityService = databaseService.GetEntityService(request.TableName);
                if (request.Guids.Count > 0)
                {
                    var guids = new List<Guid>();
                    foreach(var item in request.Guids)
                    {
                        guids.Add(Guid.Parse(item));
                    }

                    entityService.DeleteRange(guids);
                }
                else
                {
                    //TODO 
                }
                
                var response = new BaseReply()
                {
                    Code = 200
                };

                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("***************************************************************************************************************");
                Console.WriteLine("Rows deleted:" + request.TableName);
                Console.WriteLine("***************************************************************************************************************");

                return Task.FromResult(response);
            }
            catch (Exception ex)
            {
                return Task.FromResult(new BaseReply() { Code = 400, Message = ex.Message, StackTrace = ex.StackTrace });
            }
        }

        public override Task<SelectReply> Select(SelectRequest request, ServerCallContext context)
        {
            try
            {
                var response = new SelectReply()
                {
                    Code = 200
                };
                IDataBaseService databaseService = new DataBaseService(root + request.DbName + ".vldb");
                IEntityService entityService = databaseService.GetEntityService(request.TableName);
                var data = new List<List<object>>();
                var resultRows = new RepeatedField<Row>();
                if (request.Conditions.Count > 0)
                {
                    var conditions = new Dictionary<string, List<IValidator>>();
                    foreach(var item in request.Conditions)
                    {
                        var validators = new List<IValidator>();
                        foreach(var condition in item.Value)
                        {
                            validators.Add(CreateValidator(condition));
                        }
                        conditions.Add(item.Key, validators);
                    }
                    data = entityService.Select(conditions, request.ShowSysColumns);
                }
                else
                {
                    data = entityService.Select(request.ShowSysColumns);
                }
                Parallel.ForEach(data, (row) => {
                    var resultRow = new Row();
                    resultRow.Items.AddRange(row.Select(x => x.ToString()));
                    response.Rows.Add(resultRow);
                });

                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("***************************************************************************************************************");
                Console.WriteLine("Rows selected: " + request.TableName);
                Console.WriteLine("***************************************************************************************************************");

                return Task.FromResult(response);
            }
            catch (Exception ex)
            {
                return Task.FromResult(new SelectReply() { Code = 400, Message = ex.Message, StackTrace = ex.StackTrace });
            }
        }


        public override Task<SelectReply> InnerJoin(InnerJoinRequest request, ServerCallContext context)
        {
            try
            {
                var response = new SelectReply()
                {
                    Code = 200
                };
                IDataBaseService databaseService = new DataBaseService(root + request.DbName + ".vldb");
                IEntityService entityService = databaseService.GetEntityService(request.FirstTableName);
                IEntityService entityService2 = databaseService.GetEntityService(request.SecondTableName);
                var data = new List<List<object>>();
                var resultRows = new RepeatedField<Row>();
                data = entityService.InnerJoin(entityService2.Entity, new Tuple<string, string>(request.FirstColumnName, request.SecondColumnName), request.ShowSysColumns);
                Parallel.ForEach(data, (row) => {
                    var resultRow = new Row();
                    resultRow.Items.AddRange(row.Select(x => x.ToString()));
                    response.Rows.Add(resultRow);
                });

                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("***************************************************************************************************************");
                Console.WriteLine("Inner join: " + request.FirstColumnName+ ", " + request.SecondColumnName);
                Console.WriteLine("***************************************************************************************************************");

                return Task.FromResult(response);
            }
            catch (Exception ex)
            {
                return Task.FromResult(new SelectReply() { Code = 400, Message = ex.Message, StackTrace = ex.StackTrace });
            }
        }

        public override Task<SelectReply> CrossJoin(CrossJoinRequest request, ServerCallContext context)
        {
            try
            {
                var response = new SelectReply()
                {
                    Code = 200
                };
                IDataBaseService databaseService = new DataBaseService(root + request.DbName + ".vldb");
                IEntityService entityService = databaseService.GetEntityService(request.FirstTableName);
                IEntityService entityService2 = databaseService.GetEntityService(request.SecondTableName);
                var data = new List<List<object>>();
                var resultRows = new RepeatedField<Row>();
                data = entityService.CrossJoin(entityService2.Entity, request.ShowSysColumns);
                Parallel.ForEach(data, (row) => {
                    var resultRow = new Row();
                    resultRow.Items.AddRange(row.Select(x => x.ToString()));
                    response.Rows.Add(resultRow);
                });

                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("***************************************************************************************************************");
                Console.WriteLine("Cross join: " + request.FirstTableName + ", " + request.SecondTableName);
                Console.WriteLine("***************************************************************************************************************");

                return Task.FromResult(response);
            }
            catch (Exception ex)
            {
                return Task.FromResult(new SelectReply() { Code = 400, Message = ex.Message, StackTrace = ex.StackTrace });
            }
        }

        public override Task<BaseReply> Insert(InsertRequest request, ServerCallContext context)
        {
            try
            {
                var response = new BaseReply()
                {
                    Code = 200
                };
                IDataBaseService databaseService = new DataBaseService(root + request.DbName + ".vldb");
                IEntityService entityService = databaseService.GetEntityService(request.TableName);
                var _columns = entityService.Entity.Schema.Columns;
                var values = new List<List<object>>();

                for (int i = 0; i < request.Rows.Count; i++)
                {
                    var row = new List<object>();

                    for (var j = 0; j < request.Rows[0].Items.Count; j++)
                    {

                        string value = request.Rows[i].Items[j];
                        try
                        {
                            row.Add(DataValueType.GetTypedValue(_columns[j + 1].DataValueType, value));
                        }
                        catch
                        {

                            throw new DataValueTypeException(string.Format("In row number {0} in column {1} value \"{2}\" has incorrect type!", i + 1, j + 1, value));
                        }
                    }
                    values.Add(row);
                }
                entityService.InsertRange(values);

                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("***************************************************************************************************************");
                Console.WriteLine("Rows inserted: " + request.TableName + ", " + request.Rows.Count);
                Console.WriteLine("***************************************************************************************************************");

                return Task.FromResult(response);
            }
            catch (Exception ex)
            {
                return Task.FromResult(new BaseReply() { Code = 400, Message = ex.Message, StackTrace = ex.StackTrace });
            }
        }

    }
}
