using DB_Engine.Interfaces;
using DB_Engine.Types;
using System;
using System.Collections.Generic;
using System.Text;
using DB_Engine.Implementations.Sources;

namespace DB_Engine.Implementations.Factories
{
    public static class SourceFactory
    {
        public static ISource GetSourceObject(string dirPath)
        {
            var sourceInstance = new Source
            {
                Url = dirPath
            };

            return sourceInstance;
        }
    }
}
