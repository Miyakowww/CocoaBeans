// Copyright (c) Maila. All rights reserved.
// Licensed under the GNU AGPLv3

using System.Text.Json;

namespace Maila.Cocoa.Beans.Models.Events
{
    public class OtherClientOnlineEvent : Event
    {
        public QClientInfo Client { get; }

        private OtherClientOnlineEvent(QClientInfo client) : base("OtherClientOnlineEvent")
        {
            Client = client;
        }

        internal static new OtherClientOnlineEvent? Parse(JsonElement body)
        {
            try
            {
                var client = QClientInfo.Parse(body.GetProperty("client"));
                if (client is null)
                {
                    return null;
                }

                return new(client);
            }
            catch { return null; }
        }
    }

    public class OtherClientOfflineEvent : Event
    {
        public QClientInfo Client { get; }

        private OtherClientOfflineEvent(QClientInfo client) : base("OtherClientOfflineEvent")
        {
            Client = client;
        }

        internal static new OtherClientOfflineEvent? Parse(JsonElement body)
        {
            try
            {
                var client = QClientInfo.Parse(body.GetProperty("client"));
                if (client is null)
                {
                    return null;
                }

                return new(client);
            }
            catch { return null; }
        }
    }
}
