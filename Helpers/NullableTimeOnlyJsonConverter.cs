using System.Text.Json;
using System.Text.Json.Serialization;

namespace BloodDonationSupportSystem.Helpers
{
    public class NullableTimeOnlyJsonConverter : JsonConverter<TimeOnly?>
    {
        private readonly string _format = "HH:mm:ss";

        public override TimeOnly? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var str = reader.GetString();
            return string.IsNullOrWhiteSpace(str) ? null : TimeOnly.ParseExact(str, _format);
        }

        public override void Write(Utf8JsonWriter writer, TimeOnly? value, JsonSerializerOptions options)
        {
            if (value.HasValue)
                writer.WriteStringValue(value.Value.ToString(_format));
            else
                writer.WriteNullValue();
        }
    }
}
