using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DB_Engine.Factories;
using DB_Engine.Implementations.Servises;
using DB_Engine.Interfaces;
using Google.Protobuf.Collections;
using Grpc.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GrpcServer
{
    public class DatabaseService: DBService.DBServiceBase
    {
        private IDataBaseServiceFactory _dataBaseServiceFactory;

        private readonly ILogger<DatabaseService> _logger;

        private readonly Settings _settings;

        public DatabaseService(ILogger<DatabaseService> logger, IDataBaseServiceFactory dataBaseServiceFactory, Settings settings)
        {
            _logger = logger;
            _dataBaseServiceFactory = dataBaseServiceFactory;
            _settings = settings;
        }
        private string root  = "D:\\Programming\\4term\\IT\\DBFILES\\grpc\\";
        public override Task<BaseReply> CreateDatabase(CreateDbRequest request, ServerCallContext context)
        {
            try
            {
                IDataBaseService databaseService = _dataBaseServiceFactory.GetDataBaseService(request.Name, _settings.MongoDbConnectionString, request.FileSize);

                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("***************************************************************************************************************");
                Console.WriteLine("Database created: " + request.Name);
                Console.WriteLine("***************************************************************************************************************");

                return Task.FromResult(new BaseReply() { Code = 200, Message = "", StackTrace = "" });
            }
            catch(Exception ex)
            {
                return Task.FromResult(new BaseReply() { Code = 400, Message = ex.Message, StackTrace = ex.StackTrace });
            }
        }

        public override Task<BaseReply> CreateTable(TableRequest request, ServerCallContext context)
        {
            try
            {
                IDataBaseService databaseService = _dataBaseServiceFactory.GetDataBaseService($"{_settings.MongoDbConnectionString}|{request.DbName}");
                databaseService.AddTable(request.TableName);

                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("***************************************************************************************************************");
                Console.WriteLine("Table created: " + request.TableName);
                Console.WriteLine("***************************************************************************************************************");

                return Task.FromResult(new BaseReply() { Code = 200 });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new BaseReply() { Code = 400, Message = ex.Message, StackTrace = ex.StackTrace });
            }
        }

        public override Task<BaseReply> DeleteTable(TableRequest request, ServerCallContext context)
        {

            try
            {
                IDataBaseService databaseService = _dataBaseServiceFactory.GetDataBaseService($"{_settings.MongoDbConnectionString}|{request.DbName}");
                databaseService.DeleteTable(request.TableName);

                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("***************************************************************************************************************");
                Console.WriteLine("Table deleted: " + request.TableName);
                Console.WriteLine("***************************************************************************************************************");

                return Task.FromResult(new BaseReply() { Code = 200 });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new BaseReply() { Code = 400, Message = ex.Message, StackTrace = ex.StackTrace });
            }
        }

        public override Task<GetTableListReply> GetTableList(GetTableListRequest request, ServerCallContext context)
        {
            try
            {   
                IDataBaseService databaseService = _dataBaseServiceFactory.GetDataBaseService($"{_settings.MongoDbConnectionString}|{request.DbName}");
                var response = new GetTableListReply()
                {
                    Code = 200
                };
                response.Tables.AddRange(databaseService.GetTables().Select(x => x.Entity.Name));

                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("***************************************************************************************************************");
                Console.WriteLine("List of tables displayed");
                Console.WriteLine("***************************************************************************************************************");

                return Task.FromResult(response);
            }
            catch (Exception ex)
            {
                return Task.FromResult(new GetTableListReply() { Code = 400, Message = ex.Message, StackTrace = ex.StackTrace });
            }
        }

    }
}
