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
                
                Console.WriteLine("Type your command:");

                string input = Console.ReadLine();
                await parser.Parse(input);

                Console.WriteLine();
                Console.WriteLine();
            }
        }
    }
}
