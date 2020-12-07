using DB_Engine.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DB_Engine.Implementations
{
    public class SourceListConverter : JsonConverter<List<ISource>>
    {
        public override bool CanConvert(Type typeToConvert)
        {

            return typeToConvert == typeof(List<ISource>);
        }

        public override List<ISource> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {

            var jsonObject = JsonDocument.ParseValue(ref reader);

            var array = JsonSerializer.Deserialize<JsonElement>(jsonObject.RootElement.GetString());
            var result = array.EnumerateArray().Select(x =>
            {
                var sourceType = Type.GetType(x.GetProperty("type").GetString());
                var element = JsonSerializer.Deserialize(x.GetRawText(), sourceType);
                ISource sourceElement = (ISource)element;
                sourceElement.Url = x.GetProperty("url").GetString();

                return sourceElement;
            });

            return result.ToList();
        }

        public override void Write(Utf8JsonWriter writer, List<ISource> value, JsonSerializerOptions options)
        {

            var dataString = JsonSerializer.Serialize(value);
            JsonEncodedText text = JsonEncodedText.Encode(dataString, JavaScriptEncoder.UnsafeRelaxedJsonEscaping);

            writer.WriteStringValue(text);
        }
    }
}
