namespace DB_Engine.Exceptions
{
    public class DataValueTypeException: System.Exception
    {
        public DataValueTypeException(string errorMessage) : base(errorMessage) {
        }
    }
}
