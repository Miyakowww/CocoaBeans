// Copyright (c) Maila. All rights reserved.
// Licensed under the GNU AGPLv3

using System.Text.Json;

namespace Maila.Cocoa.Beans.Models
{
    public class QStrangerInfo
    {
        public long Id { get; }
        public string Nickname { get; }
        public string Remark { get; }

        private QStrangerInfo(long id, string nickname, string remark)
        {
            Id = id;
            Nickname = nickname;
            Remark = remark;
        }

        internal static QStrangerInfo? Parse(JsonElement body)
        {
            try
            {
                return new(body.GetProperty("id").GetInt64(),
                           body.GetProperty("nickname").GetString() ?? string.Empty,
                           body.GetProperty("remark").GetString() ?? string.Empty);
            }
            catch { return null; }
        }
    }
}
