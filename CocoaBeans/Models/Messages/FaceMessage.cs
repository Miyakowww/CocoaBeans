// Copyright (c) Maila. All rights reserved.
// Licensed under the GNU AGPLv3

using System.Text.Json;
using System.Text.Json.Serialization;

namespace Maila.Cocoa.Beans.Models.Messages
{
    public interface IFaceMessage : IMessage
    {
        [JsonPropertyName("type")]
        public string Type { get; }

        [JsonPropertyName("faceId")]
        public int FaceId { get; }

        [JsonPropertyName("name")]
        public string Name { get; }
    }

    public class FaceMessage : Message, IFaceMessage
    {
        public int FaceId { get; init; }
        public string Name { get; init; } = string.Empty;

        private FaceMessage() : base("Face") { }

        public FaceMessage(int faceId) : base("Face")
        {
            FaceId = faceId;
        }

        internal static new FaceMessage? Parse(JsonElement body)
        {
            try
            {
                return new()
                {
                    FaceId = body.GetProperty("faceId").GetInt32(),
                    Name = body.GetProperty("name").GetString() ?? string.Empty
                };
            }
            catch { return null; }
        }
    }
}
