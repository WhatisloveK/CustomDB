using DB_Engine.Interfaces;
using DB_Engine.Types;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DB_Engine.Models
{
    public class EntityColumn
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("type")]
        public Guid DataValueType { get; set; }

        [JsonPropertyName("validators")]
        public List<IValidator> Validators { get; set; }

        public EntityColumn()
        {
            Validators = new List<IValidator>();
        }
    }
}
