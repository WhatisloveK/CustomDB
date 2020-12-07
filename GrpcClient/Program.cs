using Grpc.Net.Client;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace GrpcClient
{
    class Program
    {
        
        static async Task Main(string[] args)
        {
            var httpHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback =
               HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            };
            GrpcChannel channel = GrpcChannel.ForAddress("https://localhost:5001", new GrpcChannelOptions { HttpHandler = httpHandler });
            EntityService.EntityServiceClient entityClient = new EntityService.EntityServiceClient(channel);
            DBService.DBServiceClient dbServiceClient = new DBService.DBServiceClient(channel);
            var parser = new Parser(entityClient, dbServiceClient);

            while (true)
            {
                Console.WriteLine("  Comand Name         Mandatory attributes\n"+
                                  "    create-db          -d -s \n" +
                                  "    get-tables         -d\n" +
                                  "    create-tbl         -d -t\n" +
                                  "    delete-tbl         -d -t\n" +
                                  "    add-clmn           -d -t -cn -ct\n" +
                                  "    add-clmn-wth-vldtr -d -t -cn -ct -cmprt -v\n" +
                                  "    insert             -d -t -r\n" +
                                  "    select             -d -t -sh\n" +
                                  "    update             -d -t -r\n" +
                                  "    inner-join         -d -t -t2 -c -c2 -sh\n" +
                                  "    cross-join         -d -t -t2 -sh");
                Console.WriteLine("Type your command:");

                string input = Console.ReadLine();
                await parser.Parse(input);

                Console.WriteLine();
                Console.WriteLine();
            }
        }
    }
}
