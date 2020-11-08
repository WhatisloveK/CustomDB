using DB_Engine.Implementations;
using DB_Engine.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DB_Engine.Models
{
    public class Entity
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("sources")]
        [JsonConverter(typeof(SourceListConverter))]
        public List<ISource> Sources { get; set; }

        [JsonPropertyName("entitySchema")]
        public EntitySchema Schema { get; set; }

        [JsonIgnore]
        public List<List<object>> Items { get; set; }

        public Entity()
        {
            Sources = new List<ISource>();
            Items = new List<List<object>>();
            Schema = new EntitySchema();
        }
    }
}
