// Copyright (c) Maila. All rights reserved.
// Licensed under the GNU AGPLv3

using System.Net;
using System.Threading.Tasks;
using Maila.Cocoa.Beans.Exceptions;

namespace Maila.Cocoa.Beans.API
{
    public static partial class MiraiAPI
    {
        /// <summary>Handle new friend request.</summary>
        /// <exception cref="MiraiException" />
        /// <exception cref="WebException" />
        public static async Task NewFriendRequestResp(string host, string sessionKey, long eventId, long fromId, long groupId, NewFriendRequestOperate operate, string message = "")
        {
            int code = (await new { sessionKey, eventId, fromId, groupId, operate = (int)operate, message }
                .PostAsRequest(host, "resp/newFriendRequestEvent")).GetCode();
            if (code != 0)
            {
                throw new MiraiException(code);
            }
        }

        /// <summary>Handle others join group request.</summary>
        /// <exception cref="MiraiException" />
        /// <exception cref="WebException" />
        public static async Task MemberJoinRequestResp(string host, string sessionKey, long eventId, long fromId, long groupId, MemberJoinRequestOperate operate, string message = "")
        {
            int code = (await new { sessionKey, eventId, fromId, groupId, operate = (int)operate, message }
                .PostAsRequest(host, "resp/memberJoinRequestEvent")).GetCode();
            if (code != 0)
            {
                throw new MiraiException(code);
            }
        }

        /// <summary>Handle group invitation request.</summary>
        /// <exception cref="MiraiException" />
        /// <exception cref="WebException" />
        public static async Task BotInvitedJoinGroupRequestResp(string host, string sessionKey, long eventId, long fromId, long groupId, BotInvitedJoinGroupRequestOperate operate, string message = "")
        {
            int code = (await new { sessionKey, eventId, fromId, groupId, operate = (int)operate, message }
                .PostAsRequest(host, "resp/botInvitedJoinGroupRequestEvent")).GetCode();
            if (code != 0)
            {
                throw new MiraiException(code);
            }
        }
    }

    public enum NewFriendRequestOperate
    {
        Agree = 0,
        Refuse = 1,
        RefuseAndAddToBlacklist = 2
    }

    public enum MemberJoinRequestOperate
    {
        Agree = 0,
        Refuse = 1,
        Ignore = 2,
        RefuseAndAddToBlacklist = 3,
        IgnoreAndAddToBlacklist = 4
    }

    public enum BotInvitedJoinGroupRequestOperate
    {
        Agree = 0,
        Refuse = 1
    }
}
