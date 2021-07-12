// Copyright (c) Maila. All rights reserved.
// Licensed under the GNU AGPLv3

using System.Text.Json;
using Maila.Cocoa.Beans.Exceptions;

namespace Maila.Cocoa.Beans.Models.Events
{
    public abstract class Event
    {
        public string Type { get; }

        protected Event(string type)
        {
            Type = type;
        }

        internal static Event? Parse(JsonElement body)
        {
            return body.GetProperty("type").GetString() switch
            {
                "FriendMessage" => FriendMessageEvent.Parse(body),
                "GroupMessage" => GroupMessageEvent.Parse(body),
                "TempMessage" => TempMessageEvent.Parse(body),

                "BotOnlineEvent" => BotOnlineEvent.Parse(body),
                "BotOfflineEventActive" => BotOfflineEventActive.Parse(body),
                "BotOfflineEventForce" => BotOfflineEventForce.Parse(body),
                "BotOfflineEventDropped" => BotOfflineEventDropped.Parse(body),
                "BotReloginEvent" => BotReloginEvent.Parse(body),
                "BotGroupPermissionChangeEvent" => BotGroupPermissionChangeEvent.Parse(body),
                "BotMuteEvent" => BotMuteEvent.Parse(body),
                "BotUnmuteEvent" => BotUnmuteEvent.Parse(body),
                "BotJoinGroupEvent" => BotJoinGroupEvent.Parse(body),
                "BotLeaveEventActive" => BotLeaveEventActive.Parse(body),
                "BotLeaveEventKick" => BotLeaveEventKick.Parse(body),

                "GroupRecallEvent" => GroupRecallEvent.Parse(body),
                "FriendRecallEvent" => FriendRecallEvent.Parse(body),

                "GroupNameChangeEvent" => GroupNameChangeEvent.Parse(body),
                "GroupEntranceAnnouncementChangeEvent" => GroupEntranceAnnouncementChangeEvent.Parse(body),
                "GroupMuteAllEvent" => GroupMuteAllEvent.Parse(body),
                "GroupAllowAnonymousChatEvent" => GroupAllowAnonymousChatEvent.Parse(body),
                "GroupAllowConfessTalkEvent" => GroupAllowConfessTalkEvent.Parse(body),
                "GroupAllowMemberInviteEvent" => GroupAllowMemberInviteEvent.Parse(body),
                "MemberJoinEvent" => MemberJoinEvent.Parse(body),
                "MemberLeaveEventKick" => MemberLeaveEventKick.Parse(body),
                "MemberLeaveEventQuit" => MemberLeaveEventQuit.Parse(body),
                "MemberCardChangeEvent" => MemberCardChangeEvent.Parse(body),
                "MemberSpecialTitleChangeEvent" => MemberSpecialTitleChangeEvent.Parse(body),
                "MemberPermissionChangeEvent" => MemberPermissionChangeEvent.Parse(body),
                "MemberMuteEvent" => MemberMuteEvent.Parse(body),
                "MemberUnmuteEvent" => MemberUnmuteEvent.Parse(body),

                "NewFriendRequestEvent" => NewFriendRequestEvent.Parse(body),
                "MemberJoinRequestEvent" => MemberJoinRequestEvent.Parse(body),
                "BotInvitedJoinGroupRequestEvent" => BotInvitedJoinGroupRequestEvent.Parse(body),

                "NudgeEvent" => NudgeEvent.Parse(body),

                { Length: > 0 } type => throw new UnsupportedEventTypeException(type),
                _ => null
            };
        }
    }
}
