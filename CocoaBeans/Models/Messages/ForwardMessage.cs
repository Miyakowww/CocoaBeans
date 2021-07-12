// Copyright (c) Maila. All rights reserved.
// Licensed under the GNU AGPLv3

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Maila.Cocoa.Beans.Models.Messages
{
    public interface IForwardMessage : IMessage
    {
        [JsonPropertyName("type")]
        public string Type { get; }

        [JsonPropertyName("title")]
        public string Title { get; }

        [JsonPropertyName("brief")]
        public string Brief { get; }

        [JsonPropertyName("source")]
        public string Source { get; }

        [JsonPropertyName("summary")]
        public string Summary { get; }

        [JsonPropertyName("nodeList")]
        public ImmutableArray<IForwardMessageNode> NodeList { get; }
    }

    public class ForwardMessage : Message, IForwardMessage
    {
        public string Title { get; init; } = string.Empty;
        public string Brief { get; init; } = string.Empty;
        public string Source { get; init; } = string.Empty;
        public string Summary { get; init; } = string.Empty;

        public ImmutableArray<IForwardMessageNode> NodeList { get; init; }

        private ForwardMessage() : base("Forward") { }

        internal new static ForwardMessage? Parse(JsonElement body)
        {
            try
            {
                List<IForwardMessageNode> list = new();
                foreach (var item in body.GetProperty("nodeList").EnumerateArray())
                {
                    var node = ForwardMessageNode.Parse(item);
                    if (node is not null)
                    {
                        list.Add(node);
                    }
                }
                return new()
                {
                    Title = body.GetProperty("title").GetString() ?? string.Empty,
                    Brief = body.GetProperty("brief").GetString() ?? string.Empty,
                    Source = body.GetProperty("source").GetString() ?? string.Empty,
                    Summary = body.GetProperty("summary").GetString() ?? string.Empty,
                    NodeList = list.ToImmutableArray()
                };
            }
            catch { return null; }
        }
    }

    public interface IForwardMessageNode
    {
        [JsonPropertyName("senderId")]
        public long SenderId { get; }

        [JsonPropertyName("time")]
        public int Time { get; }

        [JsonPropertyName("senderName")]
        public string SenderName { get; }

        [JsonPropertyName("messageChain")]
        public ImmutableArray<IMessage> MessageChain { get; }
    }

    internal class ForwardMessageNode : IForwardMessageNode
    {
        public long SenderId { get; init; }
        public int Time { get; init; }
        public string SenderName { get; init; } = string.Empty;
        public ImmutableArray<IMessage> MessageChain { get; init; }

        public static ForwardMessageNode? Parse(JsonElement body)
        {
            try
            {
                List<IMessage> chain = new();
                foreach (var item in body.GetProperty("messageChain").EnumerateArray())
                {
                    var msg = Message.Parse(item);
                    if (msg is not null)
                    {
                        chain.Add(msg);
                    }
                }
                return new()
                {
                    SenderId = body.GetProperty("senderId").GetInt64(),
                    Time = body.GetProperty("time").GetInt32(),
                    SenderName = body.GetProperty("senderName").GetString() ?? string.Empty,
                    MessageChain = chain.ToImmutableArray()
                };
            }
            catch { return null; }
        }
    }
}
