using System.Text.Json;
using System.Text.Json.Serialization;

namespace iWareCommon.Serializer
{
    public class SimpleDateSerializer : JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {

            writer.WriteStringValue(value.ToString("yyyy-MM-dd"));

          
        }
    }
}
