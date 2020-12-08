using DB_Engine.Factories;
using DB_Engine.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DB_Engine.Interfaces
{
    public interface ISource
    {
        [JsonPropertyName("url")]
        string Url { get; set; }

        [JsonPropertyName("type")]
        string Type { get; }

        [JsonIgnore]
        long SizeInBytes { get; }

        [JsonIgnore]
        IDbProviderFactory dbProviderFactory { get; set; }

        List<List<object>> GetData();
        Task<List<List<object>>> GetDataAsync();
        void WriteData(List<List<object>> data);
        Task WriteDataAsync(List<List<object>> data);
        void SetUrl(DataBase db, Entity table);
    }
}
