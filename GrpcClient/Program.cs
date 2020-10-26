using System;
using System.Threading.Tasks;

namespace GrpcClient
{
    class Program
    {
        
        static async Task Main(string[] args)
        {
            var t = new DatabaseService();
            await t.CreateDatabase("test1", 100);
        }
    }
}
