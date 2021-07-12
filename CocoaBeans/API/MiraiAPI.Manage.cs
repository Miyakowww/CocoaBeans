// Copyright (c) Maila. All rights reserved.
// Licensed under the GNU AGPLv3

using System.Dynamic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Maila.Cocoa.Beans.Exceptions;
using Maila.Cocoa.Beans.Models;

namespace Maila.Cocoa.Beans.API
{
    public static partial class MiraiAPI
    {
        /// <summary>Mute group member.</summary>
        /// <exception cref="MiraiException" />
        /// <exception cref="WebException" />
        public static async Task Mute(string host, string sessionKey, long groupId, long memberId, int seconds)
        {
            int code = (await new
            {
                sessionKey,
                target = groupId,
                memberId,
                time = seconds
            }.PostAsRequest(host, "mute")).GetCode();
            if (code != 0)
            {
                throw new MiraiException(code);
            }
        }

        /// <summary>Unmute group member.</summary>
        /// <exception cref="MiraiException" />
        /// <exception cref="WebException" />
        public static async Task Unmute(string host, string sessionKey, long groupId, long memberId)
        {
            int code = (await new
            {
                sessionKey,
                target = groupId,
                memberId
            }.PostAsRequest(host, "unmute")).GetCode();
            if (code != 0)
            {
                throw new MiraiException(code);
            }
        }

        /// <summary>Kick group member.</summary>
        /// <exception cref="MiraiException" />
        /// <exception cref="WebException" />
        public static async Task Kick(string host, string sessionKey, long groupId, long memberId, string? reason = null)
        {
            object req = reason is null
                ? new { sessionKey, target = groupId, memberId }
                : new { sessionKey, target = groupId, memberId, msg = reason };

            int code = (await req.PostAsRequest(host, "kick")).GetCode();
            if (code != 0)
            {
                throw new MiraiException(code);
            }
        }

        /// <summary>Quit group.</summary>
        /// <exception cref="MiraiException" />
        /// <exception cref="WebException" />
        public static async Task Quit(string host, string sessionKey, long groupId)
        {
            int code = (await new { sessionKey, target = groupId }.PostAsRequest(host, "quit")).GetCode();
            if (code != 0)
            {
                throw new MiraiException(code);
            }
        }

        /// <summary>Mute group.</summary>
        /// <exception cref="MiraiException" />
        /// <exception cref="WebException" />
        public static async Task MuteAll(string host, string sessionKey, long groupId)
        {
            int code = (await new { sessionKey, target = groupId }.PostAsRequest(host, "muteAll")).GetCode();
            if (code != 0)
            {
                throw new MiraiException(code);
            }
        }

        /// <summary>Unmute group.</summary>
        /// <exception cref="MiraiException" />
        /// <exception cref="WebException" />
        public static async Task UnmuteAll(string host, string sessionKey, long groupId)
        {
            int code = (await new { sessionKey, target = groupId }.PostAsRequest(host, "unmuteAll")).GetCode();
            if (code != 0)
            {
                throw new MiraiException(code);
            }
        }

        /// <summary>Get group configurations.</summary>
        /// <exception cref="MiraiException" />
        /// <exception cref="WebException" />
        public static async Task<GroupConfig?> GetGroupConfig(string host, string sessionKey, long groupId)
        {
            string resStr = await GetAsync($"http://{host}/groupConfig?sessionKey={sessionKey}&target={groupId}");
            var res = JsonDocument.Parse(resStr).RootElement;

            int code = res.GetCode();
            if (code != 0)
            {
                throw new MiraiException(code);
            }

            return JsonSerializer.Deserialize<GroupConfig>(resStr);
        }

        /// <summary>Set group configurations.</summary>
        /// <exception cref="MiraiException" />
        /// <exception cref="WebException" />
        public static async Task SetGroupConfig(string host, string sessionKey, long groupId, GroupConfig config)
        {
            dynamic req = new ExpandoObject();
            req.sessionKey = sessionKey;
            req.target = groupId;

            if (config.name is not null)
            {
                req.config.name = config.name;
            }
            if (config.announcement is not null)
            {
                req.config.announcement = config.announcement;
            }
            if (config.confessTalk is not null)
            {
                req.config.confessTalk = config.confessTalk;
            }
            if (config.allowMemberInvite is not null)
            {
                req.config.allowMemberInvite = config.allowMemberInvite;
            }
            if (config.autoApprove is not null)
            {
                req.config.autoApprove = config.autoApprove;
            }
            if (config.anonymousChat is not null)
            {
                req.config.anonymousChat = config.anonymousChat;
            }

            int code = (await ((object)req).PostAsRequest(host, "groupConfig")).GetCode();
            if (code != 0)
            {
                throw new MiraiException(code);
            }
        }

        /// <summary>Get group member information.</summary>
        /// <exception cref="WebException" />
        public static async Task<(string nickInGroup, string nick, string specialTitle)?> GetMemberInfo(string host, string sessionKey, long groupId, long memberId)
        {
            var res = await GetAsRequest(host, "memberInfo", $"sessionKey={sessionKey}", $"target={groupId}", $"memberId={memberId}");

            var name = res.GetProperty("name").GetString();
            var nick = res.GetProperty("nick").GetString();
            var specialTitle = res.GetProperty("specialTitle").GetString();
            if (name is null ||
                nick is null ||
                specialTitle is null)
            {
                return null;
            }
            return (name, nick, specialTitle);
        }

        /// <summary>Set group member information.</summary>
        /// <exception cref="MiraiException" />
        /// <exception cref="WebException" />
        public static async Task SetMemberInfo(string host, string sessionKey, long groupId, long memberId, string? nickInGroup, string? specialTitle)
        {
            dynamic req = new ExpandoObject();
            req.sessionKey = sessionKey;
            req.target = groupId;
            req.memberId = memberId;

            if (nickInGroup is not null)
            {
                req.info.name = nickInGroup;
            }
            if (specialTitle is not null)
            {
                req.info.specialTitle = specialTitle;
            }

            int code = (await ((object)req).PostAsRequest(host, "memberInfo")).GetCode();
            if (code != 0)
            {
                throw new MiraiException(code);
            }
        }
    }
}
