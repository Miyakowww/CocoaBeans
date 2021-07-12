// Copyright (c) Maila. All rights reserved.
// Licensed under the GNU AGPLv3

using System.Text.Json;
using System.Text.Json.Serialization;

namespace Maila.Cocoa.Beans.Models.Messages
{
    public interface IJsonMessage : IMessage
    {
        [JsonPropertyName("type")]
        public string Type { get; }

        [JsonPropertyName("json")]
        public string Json { get; }
    }

    public class JsonMessage : Message, IJsonMessage
    {
        public string Json { get; init; } = string.Empty;

        private JsonMessage() : base("Json") { }

        public JsonMessage(string json) : base("Json")
        {
            Json = json;
        }

        internal new static JsonMessage? Parse(JsonElement body)
        {
            try
            {
                return new(body.GetProperty("json").GetString() ?? string.Empty);
            }
            catch { return null; }
        }
    }
}
