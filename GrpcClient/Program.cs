using System;
using System.Threading.Tasks;

namespace GrpcClient
{
    class Program
    {
        
        static async Task Main(string[] args)
        {
            var parser = new Parser();

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
