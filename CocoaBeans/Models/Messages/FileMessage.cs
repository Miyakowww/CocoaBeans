// Copyright (c) Maila. All rights reserved.
// Licensed under the GNU AGPLv3

using System.Text.Json;
using System.Text.Json.Serialization;

namespace Maila.Cocoa.Beans.Models.Messages
{
    public interface IFileMessage : IMessage
    {
        [JsonPropertyName("type")]
        public string Type { get; }

        [JsonPropertyName("id")]
        public string Id { get; }

        [JsonPropertyName("name")]
        public string Name { get; }

        [JsonPropertyName("size")]
        public long Size { get; }
    }

    public class FileMessage : Message, IFileMessage
    {
        public string Id { get; init; } = string.Empty;
        public string Name { get; init; } = string.Empty;
        public long Size { get; init; }

        private FileMessage() : base("File") { }

        internal new static FileMessage? Parse(JsonElement body)
        {
            try
            {
                return new()
                {
                    Id = body.GetProperty("id").GetString() ?? string.Empty,
                    Name = body.GetProperty("name").GetString() ?? string.Empty,
                    Size = body.GetProperty("size").GetInt64()
                };
            }
            catch { return null; }
        }
    }
}
