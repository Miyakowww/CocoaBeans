// Copyright (c) Maila. All rights reserved.
// Licensed under the GNU AGPLv3

using System.Net;
using System.Threading.Tasks;
using Maila.Cocoa.Beans.Exceptions;

namespace Maila.Cocoa.Beans.API
{
    public static partial class MiraiAPI
    {
        /// <summary>Send nudge to private chat.</summary>
        /// <exception cref="MiraiException" />
        /// <exception cref="WebException" />
        public static async Task SendNudge(string host, string sessionKey, long qqId)
        {
            int code = (await new
            {
                sessionKey,
                target = qqId,
                subject = qqId,
                kind = "Friend"
            }.PostAsRequest(host, "sendNudge")).GetCode();
            if (code != 0)
            {
                throw new MiraiException(code);
            }
        }

        /// <summary>Send nudge to group.</summary>
        /// <exception cref="MiraiException" />
        /// <exception cref="WebException" />
        public static async Task SendNudge(string host, string sessionKey, long groupId, long qqId)
        {
            int code = (await new
            {
                sessionKey,
                target = qqId,
                subject = groupId,
                kind = "Group"
            }.PostAsRequest(host, "sendNudge")).GetCode();
            if (code != 0)
            {
                throw new MiraiException(code);
            }
        }
    }
}
