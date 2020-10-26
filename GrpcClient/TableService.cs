using System;
using System.Collections.Generic;
using System.Text;
using Grpc.Net.Client;
using System.Threading.Tasks;

namespace GrpcClient
{
    class TableService
    {
        public static string backSlash = @"\";
        public static string databaseExtension = ".vldb";
        public static string tableExtension = ".vldb";
        public static string path = @"C:\University\IT\dbms_core\databases\";

        static GrpcChannel channel;
        static EntityService.EntityServiceClient client;

        public TableService()
        {
            channel = GrpcChannel.ForAddress("https://localhost:5001");
            client = new EntityService.EntityServiceClient(channel);
        }

        public static async Task AddColumn(string dbname, string tableName, string columnName, string dataValueTypeID, List<Validator> validators)
        {
            //var 
        }

    }
}
