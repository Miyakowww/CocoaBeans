// Copyright (c) Maila. All rights reserved.
// Licensed under the GNU AGPLv3

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text.Json;
using Maila.Cocoa.Beans.Models.Messages;

namespace Maila.Cocoa.Beans.Models.Events
{
    public class FriendMessageEvent : Event
    {
        public ImmutableArray<Message> MessageChain { get; }
        public QFriendInfo Sender { get; }

        private FriendMessageEvent(ImmutableArray<Message> messageChain, QFriendInfo sender) : base("FriendMessage")
        {
            MessageChain = messageChain;
            Sender = sender;
        }

        internal new static FriendMessageEvent? Parse(JsonElement body)
        {
            try
            {
                List<Message> chain = new();
                foreach (var item in body.GetProperty("messageChain").EnumerateArray())
                {
                    var msg = Message.Parse(item);
                    if (msg is not null)
                    {
                        chain.Add(msg);
                    }
                }

                QFriendInfo? sender = QFriendInfo.Parse(body.GetProperty("sender"));
                if (sender is null)
                {
                    return null;
                }

                return new(chain.ToImmutableArray(), sender);
            }
            catch { return null; }
        }
    }

    public class GroupMessageEvent : Event
    {
        public ImmutableArray<Message> MessageChain { get; }
        public QMemberInfo Sender { get; }

        private GroupMessageEvent(ImmutableArray<Message> messageChain, QMemberInfo sender) : base("GroupMessage")
        {
            MessageChain = messageChain;
            Sender = sender;
        }

        internal new static GroupMessageEvent? Parse(JsonElement body)
        {
            try
            {
                List<Message> chain = new();
                foreach (var item in body.GetProperty("messageChain").EnumerateArray())
                {
                    var msg = Message.Parse(item);
                    if (msg is not null)
                    {
                        chain.Add(msg);
                    }
                }

                var senderElement = body.GetProperty("sender");
                QMemberInfo? sender = QMemberInfo.Parse(senderElement);
                if (sender is null)
                {
                    return null;
                }

                return new(chain.ToImmutableArray(), sender);
            }
            catch { return null; }
        }
    }

    public class TempMessageEvent : Event
    {
        public ImmutableArray<Message> MessageChain { get; }
        public QMemberInfo Sender { get; }

        private TempMessageEvent(ImmutableArray<Message> messageChain, QMemberInfo sender) : base("TempMessage")
        {
            MessageChain = messageChain;
            Sender = sender;
        }

        internal new static TempMessageEvent? Parse(JsonElement body)
        {
            try
            {
                List<Message> chain = new();
                foreach (var item in body.GetProperty("messageChain").EnumerateArray())
                {
                    var msg = Message.Parse(item);
                    if (msg is not null)
                    {
                        chain.Add(msg);
                    }
                }

                var senderElement = body.GetProperty("sender");
                QMemberInfo? sender = QMemberInfo.Parse(senderElement);
                if (sender is null)
                {
                    return null;
                }

                return new(chain.ToImmutableArray(), sender);
            }
            catch { return null; }
        }
    }
}
