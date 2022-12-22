// Copyright (c) Maila. All rights reserved.
// Licensed under the GNU AGPLv3

using System.Text.Json;
using System.Text.Json.Serialization;

namespace Maila.Cocoa.Beans.Models.Messages
{
    public interface IPokeMessage : IMessage
    {
        [JsonPropertyName("type")]
        public string Type { get; }

        [JsonPropertyName("name")]
        public string Name { get; }
    }

    public class PokeMessage : Message, IPokeMessage
    {
        public string Name { get; init; }

        private PokeMessage() : base("Poke")
        {
            Name = string.Empty;
        }
        public PokeMessage(string name) : base("Poke")
        {
            Name = name;
        }

        internal static new PokeMessage? Parse(JsonElement body)
        {
            try
            {
                return new(body.GetProperty("name").GetString() ?? string.Empty);
            }
            catch { return null; }
        }
    }

    public enum PokeName
    {
        Poke,
        ShowLove,
        Like,
        Heartbroken,
        SixSixSix,
        FangDaZhao
    }
}
