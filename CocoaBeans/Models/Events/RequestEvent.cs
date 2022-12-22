// Copyright (c) Maila. All rights reserved.
// Licensed under the GNU AGPLv3

using System.Text.Json;

namespace Maila.Cocoa.Beans.Models.Events
{
    public class NewFriendRequestEvent : Event
    {
        public long EventId { get; }
        public long FromId { get; }
        public long GroupId { get; }
        public string Nick { get; }
        public string Message { get; }

        public bool FromGroup => GroupId != 0;

        private NewFriendRequestEvent(long eventId, long fromId, long groupId, string nick, string message) : base("NewFriendRequestEvent")
        {
            EventId = eventId;
            FromId = fromId;
            GroupId = groupId;
            Nick = nick;
            Message = message;
        }

        internal static new NewFriendRequestEvent? Parse(JsonElement body)
        {
            try
            {
                return new(body.GetProperty("eventId").GetInt64(),
                           body.GetProperty("fromId").GetInt64(),
                           body.GetProperty("groupId").GetInt64(),
                           body.GetProperty("nick").GetString() ?? string.Empty,
                           body.GetProperty("message").GetString() ?? string.Empty);
            }
            catch { return null; }
        }
    }

    public class MemberJoinRequestEvent : Event
    {
        public long EventId { get; }
        public long FromId { get; }
        public long GroupId { get; }
        public string GroupName { get; }
        public string Nick { get; }
        public string Message { get; }

        public bool FromGroup => GroupId != 0;

        private MemberJoinRequestEvent(long eventId, long fromId, long groupId, string groupName, string nick, string message) : base("MemberJoinRequestEvent")
        {
            EventId = eventId;
            FromId = fromId;
            GroupId = groupId;
            GroupName = groupName;
            Nick = nick;
            Message = message;
        }

        internal static new MemberJoinRequestEvent? Parse(JsonElement body)
        {
            try
            {
                return new(body.GetProperty("eventId").GetInt64(),
                           body.GetProperty("fromId").GetInt64(),
                           body.GetProperty("groupId").GetInt64(),
                           body.GetProperty("groupName").GetString() ?? string.Empty,
                           body.GetProperty("nick").GetString() ?? string.Empty,
                           body.GetProperty("message").GetString() ?? string.Empty);
            }
            catch { return null; }
        }
    }

    public class BotInvitedJoinGroupRequestEvent : Event
    {
        public long EventId { get; }
        public long FromId { get; }
        public long GroupId { get; }
        public string GroupName { get; }
        public string Nick { get; }
        public string Message { get; }

        public bool FromGroup => GroupId != 0;

        private BotInvitedJoinGroupRequestEvent(long eventId, long fromId, long groupId, string groupName, string nick, string message) : base("BotInvitedJoinGroupRequestEvent")
        {
            EventId = eventId;
            FromId = fromId;
            GroupId = groupId;
            GroupName = groupName;
            Nick = nick;
            Message = message;
        }

        internal static new BotInvitedJoinGroupRequestEvent? Parse(JsonElement body)
        {
            try
            {
                return new(body.GetProperty("eventId").GetInt64(),
                           body.GetProperty("fromId").GetInt64(),
                           body.GetProperty("groupId").GetInt64(),
                           body.GetProperty("groupName").GetString() ?? string.Empty,
                           body.GetProperty("nick").GetString() ?? string.Empty,
                           body.GetProperty("message").GetString() ?? string.Empty);
            }
            catch { return null; }
        }
    }
}
