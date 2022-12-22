// Copyright (c) Maila. All rights reserved.
// Licensed under the GNU AGPLv3

using System.Text.Json;

namespace Maila.Cocoa.Beans.Models.Events
{
    public class GroupRecallEvent : Event
    {
        public long AuthorId { get; }
        public int MessageId { get; }
        public int Time { get; }
        public QGroupInfo Group { get; }
        public QMemberInfo Operator { get; }

        private GroupRecallEvent(long authorId, int messageId, int time, QGroupInfo group, QMemberInfo @operator) : base("GroupRecallEvent")
        {
            AuthorId = authorId;
            MessageId = messageId;
            Time = time;
            Group = group;
            Operator = @operator;
        }

        internal static new GroupRecallEvent? Parse(JsonElement body)
        {
            try
            {
                QGroupInfo? group = QGroupInfo.Parse(body.GetProperty("group"));
                if (group is null)
                {
                    return null;
                }


                QMemberInfo? member = QMemberInfo.Parse(body.GetProperty("operator"));
                if (member is null)
                {
                    return null;
                }

                return new(body.GetProperty("authorId").GetInt64(),
                           body.GetProperty("messageId").GetInt32(),
                           body.GetProperty("time").GetInt32(),
                           group,
                           member);
            }
            catch { return null; }
        }
    }

    public class FriendRecallEvent : Event
    {
        public long AuthorId { get; }
        public int MessageId { get; }
        public int Time { get; }
        public long Operator { get; }

        private FriendRecallEvent(long authorId, int messageId, int time, long @operator) : base("FriendRecallEvent")
        {
            AuthorId = authorId;
            MessageId = messageId;
            Time = time;
            Operator = @operator;
        }

        internal static new FriendRecallEvent? Parse(JsonElement body)
        {
            try
            {
                return new(body.GetProperty("authorId").GetInt64(),
                           body.GetProperty("messageId").GetInt32(),
                           body.GetProperty("time").GetInt32(),
                           body.GetProperty("operator").GetInt64());
            }
            catch { return null; }
        }
    }
}
