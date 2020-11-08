using System;
using System.Collections.Generic;
using System.Text;

namespace DB_Engine.DbProviders.Extentions
{
    static class StringExtentions
    {
        public static string WithParameters(this string format, params object[] parameters)
        {
            return string.Format(format, parameters);
        }
    }
}
