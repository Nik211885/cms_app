using System.Text.Json.Serialization;

namespace backend.Core.ValueObject
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Policy
    {
        ChangeNews,
        CensorNews,
        ReadNews,
        Manager
    }
}
