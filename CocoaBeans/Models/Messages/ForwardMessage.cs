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

        [JsonPropertyName("nodeList")]
        public ImmutableArray<IForwardMessageNode> NodeList { get; }
    }

    public class ForwardMessage : Message, IForwardMessage
    {
        public ImmutableArray<IForwardMessageNode> NodeList { get; init; }

        private ForwardMessage() : base("Forward") { }
        public ForwardMessage(params IForwardMessageNode[] nodeList) : base("Forward")
        {
            NodeList = nodeList.ToImmutableArray();
        }

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

    public class ForwardMessageNode : IForwardMessageNode
    {
        public long SenderId { get; init; }
        public int Time { get; init; }
        public string SenderName { get; init; } = string.Empty;
        public ImmutableArray<IMessage> MessageChain { get; init; }
        private ForwardMessageNode() { }
        public ForwardMessageNode(long senderId, int time, string senderName, params IMessage[] messageChain)
        {
            SenderId = senderId;
            Time = time;
            SenderName = senderName;
            MessageChain = messageChain.ToImmutableArray();
        }

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
