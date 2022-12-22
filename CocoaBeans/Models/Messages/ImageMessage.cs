// Copyright (c) Maila. All rights reserved.
// Licensed under the GNU AGPLv3

using System.Text.Json;
using System.Text.Json.Serialization;

namespace Maila.Cocoa.Beans.Models.Messages
{
    public interface IImageMessage : IMessage
    {
        [JsonPropertyName("type")]
        public string Type { get; }

        [JsonPropertyName("imageId")]
        public string ImageId { get; }

        [JsonPropertyName("url")]
        public string? Url { get; }
    }

    public class ImageMessage : Message, IImageMessage
    {
        public string ImageId { get; init; } = string.Empty;
        public string? Url { get; init; }

        internal ImageMessage() : base("Image") { }

        public FlashImageMessage ToFlashImage()
        {
            return new()
            {
                ImageId = ImageId,
                Url = Url
            };
        }

        internal static new ImageMessage? Parse(JsonElement body)
        {
            try
            {
                return new()
                {
                    ImageId = body.GetProperty("imageId").GetString() ?? string.Empty,
                    Url = body.GetProperty("url").GetString()
                };
            }
            catch { return null; }
        }
    }
}
