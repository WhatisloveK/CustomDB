using System;
using System.Collections.Generic;
using System.Text;

namespace DB_Engine.Exceptions
{
    public class StorageException : System.Exception
    {
        public StorageException(string message) : base(message) { }
    }
}
