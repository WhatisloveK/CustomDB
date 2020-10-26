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
                IDataBaseService databaseService = new DataBaseService(request.DbName);
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
                IDataBaseService databaseService = new DataBaseService(request.DbName);
                IEntityService entityService = databaseService.GetEntityService(request.TableName);
                entityService.DropColumn(request.ColumnName);
                var response = new BaseReply()
                {
                    Code = 200
                };
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
                IDataBaseService databaseService = new DataBaseService(request.DbName);
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
                IDataBaseService databaseService = new DataBaseService(request.DbName);
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
                return Task.FromResult(response);
            }
            catch (Exception ex)
            {
                return Task.FromResult(new SelectReply() { Code = 400, Message = ex.Message, StackTrace = ex.StackTrace });
            }
        }


    }
}
