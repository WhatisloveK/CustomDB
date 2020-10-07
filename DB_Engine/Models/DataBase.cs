using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DB_Engine.Models
{
    public class DataBase
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("dbInfo")]
        public DatabaseInfo Info{ get; set; }

        [JsonPropertyName("tables")]
        public List<Entity> Entities { get; set; }

        public DataBase()
        {
            Entities = new List<Entity>();
            Info = new DatabaseInfo();
        }
    }
}
