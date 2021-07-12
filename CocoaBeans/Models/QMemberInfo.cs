// Copyright (c) Maila. All rights reserved.
// Licensed under the GNU AGPLv3

using System;
using System.Text.Json;

namespace Maila.Cocoa.Beans.Models
{
    public class QMemberInfo
    {
        public long Id { get; }
        public string MemberName { get; }
        public GroupPermission Permission { get; }

        public QGroupInfo Group { get; }

        private QMemberInfo(long id, string memberName, GroupPermission permission, QGroupInfo group)
        {
            Id = id;
            MemberName = memberName;
            Permission = permission;
            Group = group;
        }

        internal static QMemberInfo? Parse(JsonElement body)
        {
            return Parse(body, QGroupInfo.Parse(body.GetProperty("group")));
        }

        internal static QMemberInfo? Parse(JsonElement body, QGroupInfo? group)
        {
            if (group is null)
            {
                return null;
            }

            try
            {
                return new(body.GetProperty("id").GetInt64(),
                           body.GetProperty("memberName").GetString() ?? string.Empty,
                           Enum.Parse<GroupPermission>(body.GetProperty("permission").GetString() ?? string.Empty),
                           group);
            }
            catch { return null; }
        }
    }
}
