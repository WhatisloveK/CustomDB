using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DB_Engine.Exceptions;
using DB_Engine.Implementations.Servises;
using DB_Engine.Interfaces;
using DB_Engine.Models;
using DB_Engine.Types;
using Grpc.Core;

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
                    return new DB_Engine.Validators.Validator<string>((DB_Engine.Interfaces.ComparsonType)validator.ComparsonType, validator.Value);
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
                    
                    
                }
                //var entityColumn = new EntityColumn() { DataValueType = Guid.Parse(request.DataValueTypeId), Name = request.ColumnName, }
                //entityService.AddColumn()
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
    }
}
