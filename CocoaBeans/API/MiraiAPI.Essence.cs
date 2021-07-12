// Copyright (c) Maila. All rights reserved.
// Licensed under the GNU AGPLv3

using System.Net;
using System.Threading.Tasks;
using Maila.Cocoa.Beans.Exceptions;

namespace Maila.Cocoa.Beans.API
{
    public static partial class MiraiAPI
    {
        /// <summary>Set group essence message.</summary>
        /// <exception cref="MiraiException" />
        /// <exception cref="WebException" />
        public static async Task SetEssence(string host, string sessionKey, int messageId)
        {
            int code = (await new { sessionKey, target = messageId }.PostAsRequest(host, "setEssence")).GetCode();
            if (code != 0)
            {
                throw new MiraiException(code);
            }
        }
    }
}
