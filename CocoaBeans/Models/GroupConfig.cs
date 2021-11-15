// Copyright (c) Maila. All rights reserved.
// Licensed under the GNU AGPLv3

using System.Text.Json;

namespace Maila.Cocoa.Beans.Models
{
    public class GroupConfig
    {
        public string? Name;

        public string? Announcement;

        public bool? ConfessTalk;

        public bool? AllowMemberInvite;

        public bool? AutoApprove;

        public bool? AnonymousChat;

        internal static GroupConfig? Parse(JsonElement body)
        {
            try
            {
                return new()
                {
                    Name = body.GetProperty("name").GetString(),
                    Announcement = body.TryGetProperty("announcement", out var ann) ? ann.GetString() : null,
                    ConfessTalk = body.GetProperty("confessTalk").GetBoolean(),
                    AllowMemberInvite = body.GetProperty("allowMemberInvite").GetBoolean(),
                    AutoApprove = body.GetProperty("autoApprove").GetBoolean(),
                    AnonymousChat = body.GetProperty("anonymousChat").GetBoolean(),
                };
            }
            catch { return null; }
        }
    }
}
