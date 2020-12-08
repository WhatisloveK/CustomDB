using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrpcServer
{
    public class Settings
    {
        public string MongoDbConnectionString  { get; set; }
        public string SqlServerConnectionString { get; set; }
        public string JSONRootPath { get; set; }
    }
}
