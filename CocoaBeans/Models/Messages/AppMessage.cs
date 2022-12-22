// Copyright (c) Maila. All rights reserved.
// Licensed under the GNU AGPLv3

using System.Text.Json;
using System.Text.Json.Serialization;

namespace Maila.Cocoa.Beans.Models.Messages
{
    public interface IAppMessage : IMessage
    {
        [JsonPropertyName("type")]
        public string Type { get; }

        [JsonPropertyName("content")]
        public string Content { get; }
    }

    public class AppMessage : Message, IAppMessage
    {
        public string Content { get; init; } = string.Empty;

        private AppMessage() : base("App") { }

        public AppMessage(string content) : base("App")
        {
            Content = content;
        }

        internal static new AppMessage? Parse(JsonElement body)
        {
            string? content;
            try
            {
                content = body.GetProperty("content").GetString();
            }
            catch { return null; }

            if (content is null)
            {
                return null;
            }
            return new(content);
        }
    }
}
