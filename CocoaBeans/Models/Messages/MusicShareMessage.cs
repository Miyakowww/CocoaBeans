// Copyright (c) Maila. All rights reserved.
// Licensed under the GNU AGPLv3

using System.Text.Json;
using System.Text.Json.Serialization;

namespace Maila.Cocoa.Beans.Models.Messages
{
    public interface IMusicShareMessage : IMessage
    {
        [JsonPropertyName("type")]
        public string Type { get; }

        [JsonPropertyName("kind")]
        public string Kind { get; }

        [JsonPropertyName("title")]
        public string Title { get; }

        [JsonPropertyName("summary")]
        public string Summary { get; }

        [JsonPropertyName("jumpUrl")]
        public string JumpUrl { get; }

        [JsonPropertyName("pictureUrl")]
        public string PictureUrl { get; }

        [JsonPropertyName("musicUrl")]
        public string MusicUrl { get; }

        [JsonPropertyName("brief")]
        public string Brief { get; }
    }

    public class MusicShareMessage : Message, IMusicShareMessage
    {
        public string Kind { get; init; } = string.Empty;
        public string Title { get; init; } = string.Empty;
        public string Summary { get; init; } = string.Empty;
        public string JumpUrl { get; init; } = string.Empty;
        public string PictureUrl { get; init; } = string.Empty;
        public string MusicUrl { get; init; } = string.Empty;
        public string Brief { get; init; } = string.Empty;

        private MusicShareMessage() : base("MusicShare") { }
        public MusicShareMessage(string kind, string title, string summary, string jumpUrl, string pictureUrl, string musicUrl, string brief) : base("MusicShare")
        {
            Kind = kind;
            Title = title;
            Summary = summary;
            JumpUrl = jumpUrl;
            PictureUrl = pictureUrl;
            MusicUrl = musicUrl;
            Brief = brief;
        }

        internal new static MusicShareMessage? Parse(JsonElement body)
        {
            try
            {
                return new(
                    body.GetProperty("kind").GetString() ?? string.Empty,
                    body.GetProperty("title").GetString() ?? string.Empty,
                    body.GetProperty("summary").GetString() ?? string.Empty,
                    body.GetProperty("jumpUrl").GetString() ?? string.Empty,
                    body.GetProperty("pictureUrl").GetString() ?? string.Empty,
                    body.GetProperty("musicUrl").GetString() ?? string.Empty,
                    body.GetProperty("brief").GetString() ?? string.Empty
                );
            }
            catch { return null; }
        }
    }
}
