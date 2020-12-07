using System;
using System.Collections.Generic;
using System.Text;
using Grpc.Net.Client;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;
using Google.Protobuf;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Linq;

namespace GrpcClient
{
    public class DatabaseService
    {
        
        private readonly DBService.DBServiceClient _client;

        public DatabaseService(DBService.DBServiceClient client)
        {
            _client = client;
        }
        private void WriteToConsole(string message)
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine(message);
        }

        public async Task CreateDatabase(string name, long filesize)
        {
            var request = new CreateDbRequest
            {
                Name = name,
                RootPath = Constants.Path,
                FileSize = filesize
            };

            var reply = await _client.CreateDatabaseAsync(request);

            
            if (reply.Code == 200)
            {
                WriteToConsole("Database created: " + name);
            }
            else
            {
                WriteToConsole("One or more errors ocured. Message: " + reply.Message + "\nStackTrace: " + reply.StackTrace);
            }
            
        }

        public  async Task CreateTable(string dbName, string tableName)
        {
            var reply = await _client.CreateTableAsync(new TableRequest
            {
                DbName = dbName,
                TableName = tableName
            });

            if (reply.Code == 200)
            {
                WriteToConsole("Table created: " + tableName);
            }
            else
            {
                WriteToConsole("One or more errors ocured. Message: " + reply.Message + "\nStackTrace: " + reply.StackTrace);
            }
            
        }

        public  async Task DeleteTable(string dbName, string tableName)
        {
            var reply = await _client.DeleteTableAsync(new TableRequest
            {
                DbName = dbName,
                TableName = tableName
            });

            if (reply.Code == 200)
            {
                WriteToConsole("Table deleted: " + tableName);
            }
            else
            {
                WriteToConsole("One or more errors ocured. Message: " + reply.Message + "\nStackTrace: " + reply.StackTrace);
            }
           
        }

        public  async Task GetTableList(string dbName)
        {
            var reply = await _client.GetTableListAsync(new GetTableListRequest
            {
                DbName = dbName
            });
            if (reply.Code == 200)
            {
                WriteToConsole("Tables list: "+reply.Tables.Aggregate((x, y) => $"{x}, {y}"));
            }
            else
            {
                WriteToConsole("One or more errors ocured. Message: "+reply.Message+"\nStackTrace: "+reply.StackTrace);
            }
        }
    }
}
