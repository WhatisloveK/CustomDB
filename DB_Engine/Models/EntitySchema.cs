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

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            var schema = (EntitySchema)obj;
            if (schema.Columns.Count == Columns.Count)
            {
                for (int i = 0; i < Columns.Count; i++)
                {
                    if (!Columns[i].Equals(schema.Columns[i]))
                        return false;
                }
                return true;
            }
            return false;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
