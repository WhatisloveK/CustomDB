using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DB_Engine.Models
{
    public class EntitySchema
    {
        [JsonPropertyName("columns")]
        public List<EntityColumn> Columns { get; set; }

        public EntitySchema()
        {
            Columns = new List<EntityColumn>();
        }
    }
}
