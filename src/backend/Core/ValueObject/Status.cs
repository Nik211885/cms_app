using System.ComponentModel;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Runtime.Serialization;

namespace backend.Core.ValueObject
{
    
    //[Flags, JsonConverter(typeof(JsonStringEnumConverter<Status>))]
    public enum Status
    {
        //[JsonPropertyName("Nháp")]
        Note,
        Send,
        Failed,
        Success
    }
}
