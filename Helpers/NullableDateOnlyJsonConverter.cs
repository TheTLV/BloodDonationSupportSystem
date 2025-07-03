using System.Text.Json;
using System.Text.Json.Serialization;

namespace BloodDonationSupportSystem.Helpers
{
    public class NullableDateOnlyJsonConverter : JsonConverter<DateOnly?>
    {
        private readonly string _format = "yyyy-MM-dd";

        public override DateOnly? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var str = reader.GetString();
            return string.IsNullOrWhiteSpace(str) ? null : DateOnly.ParseExact(str, _format);
        }

        public override void Write(Utf8JsonWriter writer, DateOnly? value, JsonSerializerOptions options)
        {
            if (value.HasValue)
                writer.WriteStringValue(value.Value.ToString(_format));
            else
                writer.WriteNullValue();
        }
    }
}
