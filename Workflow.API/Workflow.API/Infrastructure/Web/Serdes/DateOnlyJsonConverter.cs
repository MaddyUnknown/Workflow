using System.Text.Json.Serialization;
using System.Text.Json;

namespace Workflow.API.Infrastructure.Web.Serdes
{
    public class DateOnlyJsonConverter : JsonConverter<DateOnly?>
    {
        private const string DateFormat = "yyyy-MM-dd"; // Customize as needed

        public override DateOnly? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string? value = reader.GetString();
            if (value == null) return null;

            return DateOnly.ParseExact(value, DateFormat);
        }

        public override void Write(Utf8JsonWriter writer, DateOnly? value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value == null ? (string?) null : value.Value.ToString(DateFormat));
        }
    }
}
