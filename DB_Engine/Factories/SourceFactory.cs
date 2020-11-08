using DB_Engine.Implementations;
using DB_Engine.Implementations.DbWriters;
using DB_Engine.Interfaces;
using DB_Engine.Models;

namespace DB_Engine.Factories
{
    public static class SourceFactory
    {
        public static ISource GetSourceObject(DataBase db, Entity entity)
        {
            var sourceInstance = new Source();
            sourceInstance.SetUrl(db, entity);

            return sourceInstance;
        }

        public static IDbWriter GetDbWriter()
        {
            return DbWriter;
        }
        static IDbWriter _dbWriter;
        static IDbWriter DbWriter
        {
            get
            {
                if (_dbWriter == null)
                    _dbWriter = new JsonDbWriter();
                return _dbWriter;
            }
            set
            {
                _dbWriter = value;
            }
        }
    }
}
