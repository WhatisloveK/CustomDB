using System;
using System.Collections.Generic;
using System.Text;
using Grpc.Net.Client;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;
using Google.Protobuf;
using System.Net.Http;

namespace GrpcClient
{
    public class DatabaseService
    {
        public static string backSlash = @"\";
        public static string databaseExtension = ".vldb";
        public static string tableExtension = ".vldb";
        public static string path = @"D:\Programming\4term\IT\DBFILES\grpc\";

        static GrpcChannel channel;
        static DBService.DBServiceClient client;

        public DatabaseService()
        {
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback =
                HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            channel = GrpcChannel.ForAddress("https://localhost:5001", new GrpcChannelOptions { HttpHandler = httpHandler });
            client = new DBService.DBServiceClient(channel);
        }

        public async Task CreateDatabase(string name, long filesize)
        {
            var request = new CreateDbRequest();
            request.Name = name;
            request.RootPath = path;
            request.FileSize = filesize;
            
            var reply = await client.CreateDatabaseAsync(request);

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Database created.");
        }

        public static async Task CreateTable(string dbName, string tableName)
        {
            var reply = await client.CreateTableAsync(new TableRequest
            {
                DbName = dbName,
                TableName = tableName
            });

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Table created: " + tableName);
        }

        public static async Task DeleteTable(string dbName, string tableName)
        {
            var reply = await client.DeleteTableAsync(new TableRequest
            {
                DbName = dbName,
                TableName = tableName
            });

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Table deleted: " + tableName);
        }

        public static async Task GetTableList(string dbName)
        {
            var reply = await client.GetTableListAsync(new GetTableListRequest
            {
                DbName = dbName
            });

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Tables list:");
        }
    }
}
