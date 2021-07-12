// Copyright (c) Maila. All rights reserved.
// Licensed under the GNU AGPLv3

using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Maila.Cocoa.Beans.Exceptions;
using Maila.Cocoa.Beans.Models;

namespace Maila.Cocoa.Beans.API
{
    public static partial class MiraiAPI
    {
        /// <summary>Get friend list.</summary>
        /// <exception cref="MiraiException" />
        /// <exception cref="WebException" />
        public static async Task<QFriendInfo[]> GetFriendList(string host, string sessionKey)
        {
            var res = await GetAsRequest(host, "friendList", $"sessionKey={sessionKey}");

            List<QFriendInfo> friends = new();
            foreach (var f in res.EnumerateArray())
            {
                var friend = QFriendInfo.Parse(f);
                if (friend is not null)
                {
                    friends.Add(friend);
                }
            }
            return friends.ToArray();
        }

        /// <summary>Get group list.</summary>
        /// <exception cref="MiraiException" />
        /// <exception cref="WebException" />
        public static async Task<QGroupInfo[]> GetGroupList(string host, string sessionKey)
        {
            var res = await GetAsRequest(host, "groupList", $"sessionKey={sessionKey}");

            List<QGroupInfo> groups = new();
            foreach (var g in res.EnumerateArray())
            {
                var group = QGroupInfo.Parse(g);
                if (group is not null)
                {
                    groups.Add(group);
                }
            }
            return groups.ToArray();
        }

        /// <summary>Get member list of the specified group.</summary>
        /// <exception cref="MiraiException" />
        /// <exception cref="WebException" />
        public static async Task<QMemberInfo[]> GetMemberList(string host, string sessionKey, long group)
        {
            var res = await GetAsRequest(host, "memberList", $"sessionKey={sessionKey}", $"target={group}");

            List<QMemberInfo> members = new();
            QGroupInfo? groupInfo = QGroupInfo.Parse(res[0].GetProperty("group"));
            if (groupInfo is null)
            {
                return Array.Empty<QMemberInfo>();
            }
            foreach (var m in res.EnumerateArray())
            {
                var member = QMemberInfo.Parse(m, groupInfo);
                if (member is not null)
                {
                    members.Add(member);
                }
            }
            return members.ToArray();
        }
    }
}
