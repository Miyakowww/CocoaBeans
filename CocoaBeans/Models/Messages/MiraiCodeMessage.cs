// Copyright (c) Maila. All rights reserved.
// Licensed under the GNU AGPLv3

using System.Text.Json;
using System.Text.Json.Serialization;

namespace Maila.Cocoa.Beans.Models.Messages
{
    public interface IMiraiCodeMessage : IMessage
    {
        [JsonPropertyName("type")]
        public string Type { get; }

        [JsonPropertyName("code")]
        public string Code { get; }
    }

    public class MiraiCodeMessage : Message, IMiraiCodeMessage
    {
        public string Code { get; init; } = string.Empty;

        private MiraiCodeMessage() : base("MiraiCode") { }

        public MiraiCodeMessage(string code) : base("MiraiCode")
        {
            Code = code;
        }

        internal static new MiraiCodeMessage? Parse(JsonElement body)
        {
            try
            {
                return new(body.GetProperty("code").GetString() ?? string.Empty);
            }
            catch { return null; }
        }
    }
}
