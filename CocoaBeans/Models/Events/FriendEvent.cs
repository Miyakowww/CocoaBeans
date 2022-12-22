// Copyright (c) Maila. All rights reserved.
// Licensed under the GNU AGPLv3

using System.Text.Json;

namespace Maila.Cocoa.Beans.Models.Events
{
    public class FriendInputStatusChangedEvent : Event
    {
        public QFriendInfo Friend { get; }
        public bool Inputting { get; }

        private FriendInputStatusChangedEvent(QFriendInfo friend, bool inputting) : base("FriendInputStatusChangedEvent")
        {
            Friend = friend;
            Inputting = inputting;
        }

        internal static new FriendInputStatusChangedEvent? Parse(JsonElement body)
        {
            try
            {
                QFriendInfo? friend = QFriendInfo.Parse(body.GetProperty("friend"));
                if (friend is null)
                {
                    return null;
                }

                return new(friend, body.GetProperty("inputting").GetBoolean());
            }
            catch { return null; }
        }
    }

    public class FriendNickChangedEvent : Event
    {
        public QFriendInfo Friend { get; }
        public string From { get; }
        public string To { get; }

        private FriendNickChangedEvent(QFriendInfo friend, string from, string to) : base("FriendNickChangedEvent")
        {
            Friend = friend;
            From = from;
            To = to;
        }

        internal static new FriendNickChangedEvent? Parse(JsonElement body)
        {
            try
            {
                QFriendInfo? friend = QFriendInfo.Parse(body.GetProperty("friend"));
                if (friend is null)
                {
                    return null;
                }

                return new(friend,
                    body.GetProperty("from").GetString() ?? string.Empty,
                    body.GetProperty("to").GetString() ?? string.Empty);
            }
            catch { return null; }
        }
    }
}
