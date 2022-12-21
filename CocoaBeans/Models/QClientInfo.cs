// Copyright (c) Maila. All rights reserved.
// Licensed under the GNU AGPLv3

using System.Text.Json;

namespace Maila.Cocoa.Beans.Models
{
    public class QClientInfo
    {
        public long Id { get; }
        public string Platform { get; }

        private QClientInfo(long id, string platform)
        {
            Id = id;
            Platform = platform;
        }

        internal static QClientInfo? Parse(JsonElement body)
        {
            try
            {
                return new(body.GetProperty("id").GetInt64(),
                           body.GetProperty("platform").GetString() ?? string.Empty);
            }
            catch { return null; }
        }
    }
}
