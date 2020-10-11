using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DB_Engine.Interfaces
{
    public interface IValidator
    {
        [JsonPropertyName("value")]
        object Value { get; set; }

        [JsonPropertyName("comparsonType")]
        int ComparsonType { get; set;  }

        [JsonPropertyName("type")]
        string Type { get; }

        [JsonPropertyName("dataValueTypeId")]
        Guid DataValueTypeId { get; }

        bool IsValid(object value);

        void Init();
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
