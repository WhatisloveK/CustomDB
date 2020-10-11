using DB_Engine.Implementations;

namespace DB_Engine.Factories
{
    public static class SourceFactory
    {
        public static Source GetSourceObject(string dirPath)
        {
            var sourceInstance = new Source
            {
                Url = dirPath
            };

            return sourceInstance;
        }
    }
}
