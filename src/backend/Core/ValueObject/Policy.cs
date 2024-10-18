﻿using System.Text.Json.Serialization;

namespace backend.Core.ValueObject
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Policy
    {
        Write,
        Censor,
        Read,
        ManagerAccount,
    }
}
