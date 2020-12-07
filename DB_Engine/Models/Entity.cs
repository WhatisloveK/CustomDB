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

        public override bool Equals(object obj)
        {
            if(obj == null)
            {
                return false;
            }
            var table = (Entity)obj;

            return Name == table.Name
                && Schema.Equals(table.Schema)
                && new Func<bool>(() =>
                {
                    if (Sources.Count == table.Sources.Count)
                    {
                        for (int i = 0; i < Sources.Count; i++)
                        {
                            if (Sources[i].Equals(table.Sources[i]))
                                return false;
                        }
                        return true;
                    }
                    return false;
                }).Invoke();
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

    }
}
