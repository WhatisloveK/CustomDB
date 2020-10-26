using System;
using System.Collections.Generic;
using System.Text;
using Grpc.Net.Client;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;
using Google.Protobuf;

namespace GrpcClient
{
    public class DatabaseService
    {
        public static string backSlash = @"\";
        public static string databaseExtension = ".vldb";
        public static string tableExtension = ".vldb";
        public static string path = @"C:\University\IT\dbms_core\databases\VladDB\";

        static GrpcChannel channel;
        static DBService.DBServiceClient client;

        public DatabaseService()
        {
            channel = GrpcChannel.ForAddress("https://localhost:5001");
            client = new DBService.DBServiceClient(channel);
        }

        public static async Task CreateDatabase(string name, long filesize)
        {
            var reply = await client.CreateDatabaseAsync(new CreateDbRequest
            {
                Name = name,
                RootPath = path,
                FileSize = filesize
            });

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
