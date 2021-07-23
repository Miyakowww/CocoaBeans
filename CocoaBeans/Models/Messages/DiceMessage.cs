// Copyright (c) Maila. All rights reserved.
// Licensed under the GNU AGPLv3

using System.Text.Json;
using System.Text.Json.Serialization;

namespace Maila.Cocoa.Beans.Models.Messages
{
    public interface IDiceMessage : IMessage
    {
        [JsonPropertyName("type")]
        public string Type { get; }

        [JsonPropertyName("value")]
        public int Value { get; }
    }

    public class DiceMessage : Message, IDiceMessage
    {
        public int Value { get; init; }

        private DiceMessage() : base("Dice") { }

        public DiceMessage(int value) : base("Dice")
        {
            Value = value;
        }

        internal new static DiceMessage? Parse(JsonElement body)
        {
            try
            {
                return new(body.GetProperty("value").GetInt32());
            }
            catch { return null; }
        }
    }
}
