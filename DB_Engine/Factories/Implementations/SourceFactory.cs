using DB_Engine.Implementations;
using DB_Engine.Implementations.DbWriters;
using DB_Engine.Interfaces;
using DB_Engine.Models;

namespace DB_Engine.Factories
{
    public class SourceFactory: ISourceFactory
    {
        private IDbProviderFactory _dbProviderFactory;

        public SourceFactory(IDbProviderFactory dbProviderFactory)
        {
            _dbProviderFactory = dbProviderFactory;
        }

        private IDbWriter _dbWriter;
        private IDbWriter DbWriter
        {
            get
            {

                if (_dbWriter == null)
                    _dbWriter = new MongoDbWriter(_dbProviderFactory);
                //_dbWriter = new SqlServerDbWriter(_dbProviderFactory);
                return _dbWriter;
            }
            set
            {
                _dbWriter = value;
            }
        }
        public ISource GetSourceObject(DataBase db, Entity entity)
        {
            var sourceInstance = new DbSource(_dbProviderFactory);

            sourceInstance.SetUrl(db, entity);

            return sourceInstance;
        }

        public IDbWriter GetDbWriter()
        {
            return DbWriter;

        }
        
    }
}
