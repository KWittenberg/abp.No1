﻿using System.Text.Json.Serialization;

namespace No1.Models;

public class TmdbErrorModel
{
    [JsonPropertyName("status_code")]
    public int StatusCode { get; set; }

    [JsonPropertyName("status_message")]
    public string StatusMessage { get; set; }

    [JsonPropertyName("success")]
    public bool Success { get; set; }
}