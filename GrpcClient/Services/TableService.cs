using System;
using System.Collections.Generic;
using System.Text;
using Grpc.Net.Client;
using System.Threading.Tasks;
using System.Net.Http;
using System.Diagnostics;
using System.Linq;
using System.ComponentModel;

namespace GrpcClient
{
    public class TableService
    {
        private readonly GrpcChannel channel;
        private readonly EntityService.EntityServiceClient client;

        public TableService()
        {
            var httpHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback =
                HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            };
            channel = GrpcChannel.ForAddress("https://localhost:5001", new GrpcChannelOptions { HttpHandler = httpHandler });
            client = new EntityService.EntityServiceClient(channel);
        }

        private void WriteToConsole(string message)
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine(message);
        }

        public  async Task AddColumn(string dbname, string tableName, string columnName, string dataValueTypeID, List<Validator> validators)
        {
            var request = new AddColumnRequest
            {
                DbName = dbname,
                TableName = tableName,
                ColumnName = columnName,
                DataValueTypeId = dataValueTypeID
            };

            if(validators!=null)
                request.Validators.AddRange(validators);

            var reply = await client.AddColumnAsync(request);

            
            if (reply.Code == 200)
            {
                WriteToConsole("Column added: " + columnName);
            }
            else
            {
                WriteToConsole("One or more errors ocured. Message: " + reply.Message + "\nStackTrace: \n" + reply.StackTrace);
            }
            
        }

        public  async Task DropColumn(string dbname, string tableName, string columnName)
        {
            var reply = await client.DropColumnAsync(new DropColumnRequst
            {
                DbName = dbname,
                TableName = tableName,
                ColumnName = columnName
            });

            if (reply.Code == 200)
            {
                WriteToConsole("Column dropped: " + columnName);
            }
            else
            {
                WriteToConsole("One or more errors ocured. Message: " + reply.Message + "\nStackTrace: \n" + reply.StackTrace);
            }
        }

        public  async Task Insert(string dbname, string tableName, List<Row> rows)
        {
            var request = new InsertRequest
            {
                DbName = dbname,
                TableName = tableName
            };

            request.Rows.AddRange(rows);

            var reply = await client.InsertAsync(request);
            if (reply.Code == 200)
            {
                WriteToConsole("Rows inserted.");
            }
            else
            {
                WriteToConsole("One or more errors ocured. Message: " + reply.Message + "\nStackTrace: \n" + reply.StackTrace);
            }
        }

        public  async Task Delete(string dbname, string tableName, List<string> guids)
        {
            var request = new DeleteRequest
            {
                DbName = dbname,
                TableName = tableName
            };

            request.Guids.AddRange(guids);

            var reply = await client.DeleteAsync(request);

            if (reply.Code == 200)
            {
                WriteToConsole("Row(s) deleted.");
            }
            else
            {
                WriteToConsole("One or more errors ocured. Message: " + reply.Message + "\nStackTrace: \n" + reply.StackTrace);
            }
        }

        public async Task Select(string dbname, string tableName, bool showSysColumns = false, Dictionary<string,Validator> conditions = null)
        {
            var request = new SelectRequest
            {
                DbName = dbname,
                TableName = tableName,
                ShowSysColumns = showSysColumns
            };

            var reply = await client.SelectAsync(request);

            if (reply.Code == 200)
            {
                for(int i = 0;i < reply.Rows.Count; i++)
                {
                    Console.WriteLine($"Row #{i+1} | " + reply.Rows[i].Items.Aggregate((x, y) => $"{x}, {y}"));
                }
            }
            else
            {
                WriteToConsole("One or more errors ocured. Message: " + reply.Message + "\nStackTrace: \n" + reply.StackTrace);
            }
        }

        public async Task Update(string dbname, string tableName, List<Row> rows, List<ConditionsFieldEntry>fields = null)
        {
            var request = new UpdateRequest
            {
                DbName = dbname,
                TableName = tableName
            };

            request.Rows.AddRange(rows);
            var reply = await client.UpdateAsync(request);
            if (reply.Code == 200)
            {
                WriteToConsole("Updated successfully");
            }
            else
            {
                WriteToConsole("One or more errors ocured. Message: " + reply.Message + "\nStackTrace: \n" + reply.StackTrace);
            }
        }

        public async Task InnerJoin(string dbname, string tableName, string tableName2, string column, string column2, bool showSystemColumns)
        {
            var request = new InnerJoinRequest
            {
                DbName = dbname,
                FirstTableName = tableName,
                SecondTableName = tableName2,
                FirstColumnName = column,
                SecondColumnName = column2,
                ShowSysColumns = showSystemColumns
            };

            var reply = await client.InnerJoinAsync(request);
            if (reply.Code == 200)
            {
                for (int i = 0; i < reply.Rows.Count; i++)
                {
                    Console.WriteLine($"Row #{i + 1} | " + reply.Rows[i].Items.Aggregate((x, y) => $"{x}, {y}"));
                }
            }
            else
            {
                WriteToConsole("One or more errors ocured. Message: " + reply.Message + "\nStackTrace: \n" + reply.StackTrace);
            }
        }

        public async Task CrossJoin(string dbname, string tableName, string tableName2, bool showSystemColumns)
        {
            var request = new CrossJoinRequest
            {
                DbName = dbname,
                FirstTableName = tableName,
                SecondTableName = tableName2,
                ShowSysColumns = showSystemColumns
            };

            var reply = await client.CrossJoinAsync(request);
            if (reply.Code == 200)
            {
                for (int i = 0; i < reply.Rows.Count; i++)
                {
                    Console.WriteLine($"Row #{i + 1} | " + reply.Rows[i].Items.Aggregate((x, y) => $"{x}, {y}"));
                }
            }
            else
            {
                WriteToConsole("One or more errors ocured. Message: " + reply.Message + "\nStackTrace: \n" + reply.StackTrace);
            }
        }
    }
}
