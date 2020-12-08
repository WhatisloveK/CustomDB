using DB_Engine.Factories;
using DB_Engine.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DB_Engine.Implementations
{
    public class MongoDbSource : DbSource
    {
        public MongoDbSource(IDbProviderFactory dbProviderFactor): base(dbProviderFactor) 
        { }
        public MongoDbSource()
        { }

        protected override IDbProvider DbProvider
        {
            get
            {

                if (_dbProvider == null)
                {
                    var data = Url.Split(GlobalSetting.Delimeter);

                    _dbProvider = dbProviderFactory.GetMongoClient(data[0], data[1], data[2]);
                }

                return _dbProvider;
            }
            set
            {
                _dbProvider = value;
            }
        }
    }
}
