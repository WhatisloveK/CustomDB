using DB_Engine.Interfaces;
using DB_Engine.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DB_Engine.Validators
{
    public class ValidatorConverter : JsonConverter<List<IValidator>>
    {
        public override bool CanConvert(Type typeToConvert)
        {
            return typeToConvert == typeof(List<IValidator>);
        }

        public override List<IValidator> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var jsonObject = JsonDocument.ParseValue(ref reader);

            var array = JsonSerializer.Deserialize<JsonElement>(jsonObject.RootElement.GetString());
            var result = array.EnumerateArray().Select(x =>
            {
                var validatorType = Type.GetType(x.GetProperty("type").GetString());
                var element = JsonSerializer.Deserialize(x.GetRawText(), validatorType);
                IValidator validator = (IValidator)element;
                validator.ComparsonType = x.GetProperty("comparsonType").GetInt32();

                var valueType = DataValueType.GetType(new Guid (x.GetProperty("dataValueTypeId").GetString()));
                validator.Value = JsonSerializer.Deserialize(x.GetProperty("value").GetRawText(), valueType);
                validator.Init();

                return (IValidator)element;
            });

            return result.ToList();
        }

        public override void Write(Utf8JsonWriter writer, List<IValidator> value, JsonSerializerOptions options) 
        {
            var dataString = JsonSerializer.Serialize(value);
            JsonEncodedText text = JsonEncodedText.Encode(dataString, JavaScriptEncoder.UnsafeRelaxedJsonEscaping);

            writer.WriteStringValue(text);
        }
    }
}
