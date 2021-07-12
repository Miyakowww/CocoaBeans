// Copyright (c) Maila. All rights reserved.
// Licensed under the GNU AGPLv3

using System.Dynamic;
using System.Net;
using System.Threading.Tasks;
using Maila.Cocoa.Beans.Exceptions;

namespace Maila.Cocoa.Beans.API
{
    public static partial class MiraiAPI
    {
        /// <summary>Get the configuration of the specified Session.</summary>
        /// <exception cref="WebException" />
        public static async Task<(int cacheSize, bool enableWebsocket)> GetConfig(string host, string sessionKey)
        {
            var res = await GetAsRequest(host, "config", $"sessionKey={sessionKey}");
            return (res.GetProperty("cacheSize").GetInt32(),
                    res.GetProperty("enableWebsocket").GetBoolean());
        }

        /// <summary>Set the configuration of the specified Session.</summary>
        /// <exception cref="MiraiException" />
        /// <exception cref="WebException" />
        public static async Task SetConfig(string host, string sessionKey, int? cacheSize, bool? enableWebsocket)
        {
            if (cacheSize is null && enableWebsocket is null)
            {
                return;
            }

            dynamic req = new ExpandoObject();
            req.sessionKey = sessionKey;
            if (cacheSize is not null)
            {
                req.cacheSize = cacheSize.Value;
            }
            if (enableWebsocket is not null)
            {
                req.enableWebsocket = enableWebsocket.Value;
            }

            int code = (await ((object)req).PostAsRequest(host, "config")).GetCode();
            if (code != 0)
            {
                throw new MiraiException(code);
            }
        }
    }
}
