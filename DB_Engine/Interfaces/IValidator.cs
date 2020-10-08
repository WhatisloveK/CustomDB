using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DB_Engine.Interfaces
{
    public interface IValidator
    {
        [JsonPropertyName("value")]
        object Value { get; }

        [JsonPropertyName("operation")]
        int Operation { get; }

        [JsonPropertyName("type")]
        string Type { get; }

        //[JsonPropertyName("dataValueTypeId")]
        //Guid DataValueType { get; }

        bool IsValid(object value);
    }

    public enum ComparsonType
    {
        Equal,
        NotEqual,
        Greater,
        GreaterOrEqual,
        Less,
        LessOrEqual,
        StartsWith,
        NotStarstWith,
        Contains,
        NotContains,
        IsNull,
        IsNotNull,
        EndsWith,
        NotEndsWith
    }

}
