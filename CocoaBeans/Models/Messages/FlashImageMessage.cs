﻿// Copyright (c) Maila. All rights reserved.
// Licensed under the GNU AGPLv3

using System.Text.Json;
using System.Text.Json.Serialization;

namespace Maila.Cocoa.Beans.Models.Messages
{
    public interface IFlashImageMessage : IMessage
    {
        [JsonPropertyName("type")]
        public string Type { get; }

        [JsonPropertyName("imageId")]
        public string ImageId { get; }

        [JsonPropertyName("url")]
        public string? Url { get; }
    }

    public class FlashImageMessage : Message, IFlashImageMessage
    {
        public string ImageId { get; init; } = string.Empty;
        public string? Url { get; init; }

        internal FlashImageMessage() : base("FlashImage") { }

        public ImageMessage ToNormalImage()
        {
            return new()
            {
                ImageId = ImageId,
                Url = Url
            };
        }

        internal static new FlashImageMessage? Parse(JsonElement body)
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
