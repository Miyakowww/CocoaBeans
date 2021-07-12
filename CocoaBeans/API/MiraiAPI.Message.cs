// Copyright (c) Maila. All rights reserved.
// Licensed under the GNU AGPLv3

using System;
using System.Net;
using System.Threading.Tasks;
using Maila.Cocoa.Beans.Exceptions;
using Maila.Cocoa.Beans.Models.Messages;

namespace Maila.Cocoa.Beans.API
{
    public static partial class MiraiAPI
    {
        /// <summary>Send message to friend.</summary>
        /// <returns>MessageID</returns>
        /// <exception cref="MiraiException" />
        /// <exception cref="WebException" />
        public static async Task<int?> SendFriendMessage(string host, string sessionKey, long qqId, params IMessage[] chain)
        {
            var res = await new { sessionKey, qq = qqId, messageChain = chain }.PostAsRequest(host, "sendFriendMessage");

            int code = res.GetCode();
            if (code != 0)
            {
                throw new MiraiException(code);
            }

            try
            {
                return res.GetProperty("messageId").GetInt32();
            }
            catch { return null; }
        }

        /// <summary>Send reply message to friend.</summary>
        /// <returns>MessageID</returns>
        /// <exception cref="MiraiException" />
        /// <exception cref="WebException" />
        public static async Task<int?> SendFriendMessage(string host, string sessionKey, long qqId, int? quote, params IMessage[] chain)
        {
            object req = quote is null
                ? new { sessionKey, qq = qqId, messageChain = chain }
                : new { sessionKey, qq = qqId, quote, messageChain = chain };
            var res = await req.PostAsRequest(host, "sendFriendMessage");

            int code = res.GetCode();
            if (code != 0)
            {
                throw new MiraiException(code);
            }

            try
            {
                return res.GetProperty("messageId").GetInt32();
            }
            catch { return null; }
        }

        /// <summary>Send message to group member.</summary>
        /// <returns>MessageID</returns>
        /// <exception cref="MiraiException" />
        /// <exception cref="WebException" />
        public static async Task<int?> SendTempMessage(string host, string sessionKey, long groupId, long qqId, params IMessage[] chain)
        {
            var res = await new { sessionKey, group = groupId, qq = qqId, messageChain = chain }.PostAsRequest(host, "sendTempMessage");

            int code = res.GetCode();
            if (code != 0)
            {
                throw new MiraiException(code);
            }

            try
            {
                return res.GetProperty("messageId").GetInt32();
            }
            catch { return null; }
        }

        /// <summary>Send reply message to group member.</summary>
        /// <returns>MessageID</returns>
        /// <exception cref="MiraiException" />
        /// <exception cref="WebException" />
        public static async Task<int?> SendTempMessage(string host, string sessionKey, long groupId, long qqId, int? quote, params IMessage[] chain)
        {
            object req = quote is null
                ? new { sessionKey, group = groupId, qq = qqId, messageChain = chain }
                : new { sessionKey, group = groupId, qq = qqId, quote, messageChain = chain };
            var res = await req.PostAsRequest(host, "sendTempMessage");

            int code = res.GetCode();
            if (code != 0)
            {
                throw new MiraiException(code);
            }

            try
            {
                return res.GetProperty("messageId").GetInt32();
            }
            catch { return null; }
        }

        /// <summary>Send message to group.</summary>
        /// <returns>MessageID</returns>
        /// <exception cref="MiraiException" />
        /// <exception cref="WebException" />
        public static async Task<int?> SendGroupMessage(string host, string sessionKey, long groupId, params IMessage[] chain)
        {
            var res = await new { sessionKey, group = groupId, messageChain = chain }.PostAsRequest(host, "sendGroupMessage");

            int code = res.GetCode();
            if (code != 0)
            {
                throw new MiraiException(code);
            }

            try
            {
                return res.GetProperty("messageId").GetInt32();
            }
            catch { return null; }
        }

        /// <summary>Send reply message to group.</summary>
        /// <returns>MessageID</returns>
        /// <exception cref="MiraiException" />
        /// <exception cref="WebException" />
        public static async Task<int?> SendGroupMessage(string host, string sessionKey, long groupId, int? quote, params IMessage[] chain)
        {
            object req = quote is null
                ? new { sessionKey, group = groupId, messageChain = chain }
                : new { sessionKey, group = groupId, quote, messageChain = chain };
            var res = await req.PostAsRequest(host, "sendGroupMessage");

            int code = res.GetCode();
            if (code != 0)
            {
                throw new MiraiException(code);
            }

            try
            {
                return res.GetProperty("messageId").GetInt32();
            }
            catch { return null; }
        }

        /// <summary>Recall message.</summary>
        /// <exception cref="MiraiException" />
        /// <exception cref="WebException" />
        public static async Task Recall(string host, string sessionKey, int messageId)
        {
            int code = (await new { sessionKey, target = messageId }.PostAsRequest(host, "recall")).GetCode();
            if (code != 0)
            {
                throw new MiraiException(code);
            }
        }

        /// <summary>Mamoe does not recommend using this interface, so I underworked ο(=•ω＜=)ρ⌒☆</summary>
        //// <exception cref="MiraiException"/>
        //// <exception cref="WebException"/>
        [Obsolete("Not Implemented", true)]
        public static Task<string[]?> SendImageMessage(string host, string sessionKey, long? qqId, long? groupId, params string[] urls)
        {
            throw new NotImplementedException();
        }
    }
}
