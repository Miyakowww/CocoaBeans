// Copyright (c) Maila. All rights reserved.
// Licensed under the GNU AGPLv3

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Maila.Cocoa.Beans.Models.Messages
{
    public interface IQuoteMessage : IMessage
    {
        [JsonPropertyName("type")]
        public string Type { get; }

        [JsonPropertyName("id")]
        public int Id { get; }

        [JsonPropertyName("groupId")]
        public long GroupId { get; }

        [JsonPropertyName("senderId")]
        public long SenderId { get; }

        [JsonPropertyName("targetId")]
        public long TargetId { get; }

        [JsonPropertyName("origin")]
        public ImmutableArray<IMessage> Origin { get; }
    }

    public class QuoteMessage : Message, IQuoteMessage
    {
        public int Id { get; init; }
        public long GroupId { get; init; }
        public long SenderId { get; init; }
        public long TargetId { get; init; }

        public ImmutableArray<IMessage> Origin { get; init; }

        private QuoteMessage() : base("Quote") { }

        internal new static QuoteMessage? Parse(JsonElement body)
        {
            try
            {
                List<IMessage> messages = new();
                foreach (var item in body.GetProperty("origin").EnumerateArray())
                {
                    var msg = Message.Parse(item);
                    if (msg is not null)
                    {
                        messages.Add(msg);
                    }
                }

                return new()
                {
                    Id = body.GetProperty("id").GetInt32(),
                    GroupId = body.GetProperty("groupId").GetInt64(),
                    SenderId = body.GetProperty("senderId").GetInt64(),
                    TargetId = body.GetProperty("targetId").GetInt64(),
                    Origin = messages.ToImmutableArray()
                };
            }
            catch { return null; }
        }
    }
}
