// Copyright (c) Maila. All rights reserved.
// Licensed under the GNU AGPLv3

using System;
using System.Text.Json;

namespace Maila.Cocoa.Beans.Models
{
    public class QGroupInfo
    {
        public long Id { get; }
        public string Name { get; }
        public GroupPermission BotPermission { get; }

        private QGroupInfo(long id, string name, GroupPermission botPermission)
        {
            Id = id;
            Name = name;
            BotPermission = botPermission;
        }

        internal static QGroupInfo? Parse(JsonElement body)
        {
            try
            {
                return new(body.GetProperty("id").GetInt64(),
                           body.GetProperty("name").GetString() ?? string.Empty,
                           Enum.Parse<GroupPermission>(body.GetProperty("permission").GetString() ?? string.Empty));
            }
            catch { return null; }
        }
    }

    public enum GroupPermission
    {
        OWNER = 2,
        ADMINISTRATOR = 1,
        MEMBER = 0
    }
}
