using DB_Engine.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace DB_Engine.Types
{
    public class DataValueType
    {
        public static readonly Guid IntegerDataValueTypeId  = new Guid("{4f291941-5fc0-4c9b-ba3a-809bf1029084}");

        public static readonly Guid RealDataValueTypeId  = new Guid("{f13b9117-9e0f-40a9-983f-abbcd4dc1281}");

        public static readonly Guid CharDataValueTypeId  = new Guid("{04f1b105-ee3b-4de3-b730-fadccc724735}");

        public static readonly Guid StringDataValueTypeId  = new Guid("{4dda6f8f-fc99-42ec-bdfe-a76b671df5e4}");

        public static readonly Guid ComplexIntegerDataValueTypeId  = new Guid("{620de49c-df9b-4eac-9bed-507652d88a61}");

        public static readonly Guid ComplexRealDataValueTypeId  = new Guid("{64b02720-d6bc-454c-8994-1cd25f733ac0}");

        public static Type GetType(Guid dataValueTypeId)
        {
            switch (dataValueTypeId)
            {
                case var t when (t == IntegerDataValueTypeId):
                    return typeof(int);
                case var t when (t == RealDataValueTypeId):
                    return typeof(double);
                case var t when (t == CharDataValueTypeId):
                    return typeof(char);
                case var t when (t == StringDataValueTypeId):
                    return typeof(string);
                case var t when (t == ComplexIntegerDataValueTypeId):
                    return typeof(ComplexInteger);
                case var t when (t == ComplexRealDataValueTypeId):
                    return typeof(ComplexReal);
                default:
                    throw new DataValueTypeException($"Incorrect DataValueTypeId specified. IncorrectDataValueTypeId = \"{dataValueTypeId}\"");
            }
        }
    }

}
