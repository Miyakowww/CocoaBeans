// Copyright (c) Maila. All rights reserved.
// Licensed under the GNU AGPLv3

using System.Text.Json;
using System.Text.Json.Serialization;

namespace Maila.Cocoa.Beans.Models.Messages
{
    public interface IVoiceMessage : IMessage
    {
        [JsonPropertyName("type")]
        public string Type { get; }

        [JsonPropertyName("voiceId")]
        public string VoiceId { get; }

        [JsonPropertyName("url")]
        public string? Url { get; }
    }

    public class VoiceMessage : Message, IVoiceMessage
    {
        public string VoiceId { get; init; } = string.Empty;
        public string? Url { get; init; }

        private VoiceMessage() : base("Voice") { }

        internal new static VoiceMessage? Parse(JsonElement body)
        {
            try
            {
                if (body.TryGetProperty("url", out var url))
                {
                    return new()
                    {
                        VoiceId = body.GetProperty("voiceId").GetString() ?? string.Empty,
                        Url = body.GetProperty("url").GetString()
                    };
                }
                return new()
                {
                    VoiceId = body.GetProperty("voiceId").GetString() ?? string.Empty
                };
            }
            catch { return null; }
        }
    }
}
