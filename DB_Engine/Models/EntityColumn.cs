using DB_Engine.Interfaces;
using DB_Engine.Types;
using DB_Engine.Validators;
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

        [JsonPropertyName("dataValueTypeId")]
        public Guid DataValueType { get; set; }

        [JsonPropertyName("validators")]
        [JsonConverter(typeof(ValidatorConverter))]
        public List<IValidator> Validators { get; set; }

        public EntityColumn()
        {
            Validators = new List<IValidator>();
        }

        public override string ToString()
        {
            return Name;
        }

        public override bool Equals(object obj)
        {
            if(obj == null || !(obj is EntityColumn))
            {
                return false;
            }
            var field = (EntityColumn)obj;

            return Name == field.Name
                && DataValueType == field.DataValueType;
        }
    }
}
