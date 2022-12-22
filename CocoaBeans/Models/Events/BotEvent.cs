// Copyright (c) Maila. All rights reserved.
// Licensed under the GNU AGPLv3

using System;
using System.Text.Json;

namespace Maila.Cocoa.Beans.Models.Events
{
    public abstract class BotEvent : Event
    {
        public long QQ { get; }

        protected BotEvent(long qq, string type) : base(type)
        {
            QQ = qq;
        }
    }

    public class BotOnlineEvent : BotEvent
    {
        private BotOnlineEvent(long qq) : base(qq, "BotOnlineEvent") { }

        internal static new BotOnlineEvent? Parse(JsonElement body)
        {
            try
            {
                return new(body.GetProperty("qq").GetInt64());
            }
            catch { return null; }
        }
    }

    public class BotOfflineEventActive : BotEvent
    {
        private BotOfflineEventActive(long qq) : base(qq, "BotOfflineEventActive") { }

        internal static new BotOfflineEventActive? Parse(JsonElement body)
        {
            try
            {
                return new(body.GetProperty("qq").GetInt64());
            }
            catch { return null; }
        }
    }

    public class BotOfflineEventForce : BotEvent
    {
        private BotOfflineEventForce(long qq) : base(qq, "BotOfflineEventForce") { }

        internal static new BotOfflineEventForce? Parse(JsonElement body)
        {
            try
            {
                return new(body.GetProperty("qq").GetInt64());
            }
            catch { return null; }
        }
    }

    public class BotOfflineEventDropped : BotEvent
    {
        private BotOfflineEventDropped(long qq) : base(qq, "BotOfflineEventDropped") { }

        internal static new BotOfflineEventDropped? Parse(JsonElement body)
        {
            try
            {
                return new(body.GetProperty("qq").GetInt64());
            }
            catch { return null; }
        }
    }

    public class BotReloginEvent : BotEvent
    {
        private BotReloginEvent(long qq) : base(qq, "BotReloginEvent") { }

        internal static new BotReloginEvent? Parse(JsonElement body)
        {
            try
            {
                return new(body.GetProperty("qq").GetInt64());
            }
            catch { return null; }
        }
    }

    public class BotGroupPermissionChangeEvent : Event
    {
        public GroupPermission Origin { get; }
        public GroupPermission Current { get; }
        public QGroupInfo Group { get; }

        private BotGroupPermissionChangeEvent(GroupPermission origin, GroupPermission current, QGroupInfo group) : base("BotGroupPermissionChangeEvent")
        {
            Origin = origin;
            Current = current;
            Group = group;
        }

        internal static new BotGroupPermissionChangeEvent? Parse(JsonElement body)
        {
            try
            {
                var group = QGroupInfo.Parse(body.GetProperty("group"));
                if (group is null)
                {
                    return null;
                }

                return new(Enum.Parse<GroupPermission>(body.GetProperty("origin").GetString() ?? string.Empty),
                           Enum.Parse<GroupPermission>(body.GetProperty("current").GetString() ?? string.Empty),
                           group);
            }
            catch { return null; }
        }
    }

    public class BotMuteEvent : Event
    {
        public int Duration { get; }
        public QMemberInfo Operator { get; }

        private BotMuteEvent(int duration, QMemberInfo @operator) : base("BotMuteEvent")
        {
            Duration = duration;
            Operator = @operator;
        }

        internal static new BotMuteEvent? Parse(JsonElement body)
        {
            try
            {
                var member = QMemberInfo.Parse(body.GetProperty("operator"));
                if (member is null)
                {
                    return null;
                }

                return new(body.GetProperty("durationSeconds").GetInt32(), member);
            }
            catch { return null; }
        }
    }

    public class BotUnmuteEvent : Event
    {
        public QMemberInfo Operator { get; }

        private BotUnmuteEvent(QMemberInfo @operator) : base("BotUnmuteEvent")
        {
            Operator = @operator;
        }

        internal static new BotUnmuteEvent? Parse(JsonElement body)
        {
            var member = QMemberInfo.Parse(body.GetProperty("operator"));
            return member is null ? null : new(member);
        }
    }

    public class BotJoinGroupEvent : Event
    {
        public QGroupInfo Group { get; }

        private BotJoinGroupEvent(QGroupInfo group) : base("BotJoinGroupEvent")
        {
            Group = group;
        }

        internal static new BotJoinGroupEvent? Parse(JsonElement body)
        {
            var group = QGroupInfo.Parse(body.GetProperty("group"));
            return group is null ? null : new(group);
        }
    }

    public class BotLeaveEventActive : Event
    {
        public QGroupInfo Group { get; }

        private BotLeaveEventActive(QGroupInfo group) : base("BotLeaveEventActive")
        {
            Group = group;
        }

        internal static new BotLeaveEventActive? Parse(JsonElement body)
        {
            var group = QGroupInfo.Parse(body.GetProperty("group"));
            return group is null ? null : new(group);
        }
    }

    public class BotLeaveEventKick : Event
    {
        public QGroupInfo Group { get; }

        private BotLeaveEventKick(QGroupInfo group) : base("BotLeaveEventKick")
        {
            Group = group;
        }

        internal static new BotLeaveEventKick? Parse(JsonElement body)
        {
            var group = QGroupInfo.Parse(body.GetProperty("group"));
            return group is null ? null : new(group);
        }
    }

    public class BotLeaveEventDisband : Event
    {
        public QGroupInfo Group { get; }

        private BotLeaveEventDisband(QGroupInfo group) : base("BotLeaveEventDisband")
        {
            Group = group;
        }

        internal static new BotLeaveEventDisband? Parse(JsonElement body)
        {
            var group = QGroupInfo.Parse(body.GetProperty("group"));
            return group is null ? null : new(group);
        }
    }
}
