// Copyright (c) Maila. All rights reserved.
// Licensed under the GNU AGPLv3

using System.Text.Json;
using System.Text.Json.Serialization;

namespace Maila.Cocoa.Beans.Models.Messages
{
    public interface IMusicShareMessage : IMessage
    {
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

        internal new static MusicShareMessage? Parse(JsonElement body)
        {
            try
            {
                return new()
                {
                    Kind = body.GetProperty("kind").GetString() ?? string.Empty,
                    Title = body.GetProperty("title").GetString() ?? string.Empty,
                    Summary = body.GetProperty("summary").GetString() ?? string.Empty,
                    JumpUrl = body.GetProperty("jumpUrl").GetString() ?? string.Empty,
                    PictureUrl = body.GetProperty("pictureUrl").GetString() ?? string.Empty,
                    MusicUrl = body.GetProperty("musicUrl").GetString() ?? string.Empty,
                    Brief = body.GetProperty("brief").GetString() ?? string.Empty
                };
            }
            catch { return null; }
        }
    }
}
