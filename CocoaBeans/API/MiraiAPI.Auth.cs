// Copyright (c) Maila. All rights reserved.
// Licensed under the GNU AGPLv3

using System.Net;
using System.Threading.Tasks;
using Maila.Cocoa.Beans.Exceptions;

namespace Maila.Cocoa.Beans.API
{
    public static partial class MiraiAPI
    {
        /// <summary>Verify identity and start a Session.</summary>
        /// <returns>Session Key</returns>
        /// <exception cref="MiraiException" />
        /// <exception cref="WebException" />
        public static async Task<string?> Auth(string host, string authKey)
        {
            var res = await new { authKey }.PostAsRequest(host, "auth");
            int code = res.GetCode();
            if (code != 0)
            {
                throw new MiraiException(code);
            }
            return res.GetProperty("session").GetString() ?? null;
        }

        /// <summary>Verify and activate a Session, and bind the Session to a logged-in Bot.</summary>
        /// <exception cref="MiraiException" />
        /// <exception cref="WebException" />
        public static async Task Verify(string host, string sessionKey, long qqId)
        {
            int code = (await new { sessionKey, qq = qqId }.PostAsRequest(host, "verify")).GetCode();
            if (code != 0)
            {
                throw new MiraiException(code);
            }
        }

        /// <summary>
        ///     Release the session and its related resources.<br />
        ///     You should release the session that is no longer in use, otherwise it will cause a memory leak.
        /// </summary>
        /// <exception cref="MiraiException" />
        /// <exception cref="WebException" />
        public static async Task Release(string host, string sessionKey, long qqId)
        {
            int code = (await new { sessionKey, qq = qqId }.PostAsRequest(host, "release")).GetCode();
            if (code != 0)
            {
                throw new MiraiException(code);
            }
        }
    }
}
