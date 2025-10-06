using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Fundsy_backend.Helper
{
    public class GuidFromHexConverter : JsonConverter<Guid>
    {
        public override Guid Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var str = reader.GetString()?.Trim();

            if (string.IsNullOrEmpty(str))
                return Guid.Empty;

            // Handle hex format starting with "0x"
            if (str.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
            {
                try
                {
                    string hex = str.Substring(2); // Remove "0x"
                    byte[] bytes = Convert.FromHexString(hex);
                    return new Guid(bytes);
                }
                catch
                {
                    throw new JsonException($"Invalid hex format for Guid: {str}");
                }
            }

            // Handle standard Guid format
            return Guid.Parse(str);
        }

        public override void Write(Utf8JsonWriter writer, Guid value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}
