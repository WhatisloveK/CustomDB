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
            var request = new AddColumnRequest
            {
                DbName = dbname,
                TableName = tableName,
                ColumnName = columnName,
                DataValueTypeId = dataValueTypeID
            };

            request.Validators.AddRange(validators);

            var reply = await client.AddColumnAsync(request);

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Column added: " + columnName);
        }

        public static async Task DropColumn(string dbname, string tableName, string columnName)
        {
            var reply = await client.DropColumnAsync(new DropColumnRequst
            {
                DbName = dbname,
                TableName = tableName,
                ColumnName = columnName
            });

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Column dropped: " + columnName);
        }

        public static async Task Insert(string dbname, string tableName, List<Row> rows)
        {
            var request = new InsertRequest
            {
                DbName = dbname,
                TableName = tableName
            };

            request.Rows.AddRange(rows);

            var reply = await client.InsertAsync(request);

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Rows inserted.");
        }

        public static async Task Delete(string dbname, string tableName, List<string> guids)
        {
            var request = new DeleteRequest
            {
                DbName = dbname,
                TableName = tableName
            };

            request.Guids.AddRange(guids);

            var reply = await client.DeleteAsync(request);

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Row(s) deleted.");
        }

        //public static Task Update(string dbname, string tableName, List<Row> rows, List<ConditionsFieldEntry>)
        //{
        //    var request = new DeleteRequest
        //    {
        //        DbName = dbname,
        //        TableName = tableName
        //    };
        //}
    }
}
