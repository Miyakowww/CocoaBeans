// Copyright (c) Maila. All rights reserved.
// Licensed under the GNU AGPLv3

using System.Text.Json;
using System.Text.Json.Serialization;

namespace Maila.Cocoa.Beans.Models.Messages
{
    public interface IAtMessage : IMessage
    {
        [JsonPropertyName("type")]
        public string Type { get; }

        [JsonPropertyName("target")]
        public long Target { get; }

        [JsonPropertyName("display")]
        public string Display { get; }
    }

    public class AtMessage : Message, IAtMessage
    {
        public long Target { get; init; }
        public string Display { get; init; } = string.Empty;

        private AtMessage() : base("At") { }

        public AtMessage(long target) : base("At")
        {
            Target = target;
        }

        internal static new AtMessage? Parse(JsonElement body)
        {
            try
            {
                return new()
                {
                    Target = body.GetProperty("target").GetInt64(),
                    Display = body.GetProperty("display").GetString() ?? string.Empty
                };
            }
            catch { return null; }
        }
    }
}
