using DB_Engine.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DB_Engine.Factories
{
    public interface IDbProviderFactory
    {
        IDbProvider GetSqlServerClient(string localhost, string db, string table = "");
        IDbProvider GetMongoClient(string localhost, string db, string table = "");

    }
}
