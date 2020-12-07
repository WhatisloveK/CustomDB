using DB_Engine.DbProviders.Implementations;
using DB_Engine.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DB_Engine.Factories
{
    class DbProviderFactory: IDbProviderFactory
    {
        public  IDbProvider GetSqlServerClient(string localhost, string db, string table = "")
        {
            return new SqlServerDbProvider(localhost, db, table);

        }

        public  IDbProvider GetMongoClient(string localhost, string db, string table = "")
        {
            return new MongoDbProvider(localhost, db, table);

        }

        //public static IDbClient GetJsonClient(string path)
        //{
        //    return new JsonDbClient(path);
        //}
    }
}
