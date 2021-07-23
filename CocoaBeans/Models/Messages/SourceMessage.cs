// Copyright (c) Maila. All rights reserved.
// Licensed under the GNU AGPLv3

using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Maila.Cocoa.Beans.Models.Messages
{
    public interface ISourceMessage
    {
        [JsonPropertyName("type")]
        public string Type { get; }

        [JsonPropertyName("id")]
        public int Id { get; }

        [JsonPropertyName("time")]
        public int Time { get; }
    }

    public class SourceMessage : Message, ISourceMessage
    {
        public int Id { get; init; }
        public int Time { get; init; }

        private SourceMessage() : base("Source") { }

        [Obsolete("You should not use it except for middleware.")]
        public SourceMessage(int id, int time) : base("Source")
        {
            Id = id;
            Time = time;
        }

        internal new static SourceMessage? Parse(JsonElement body)
        {
            try
            {
                return new()
                {
                    Id = body.GetProperty("id").GetInt32(),
                    Time = body.GetProperty("time").GetInt32()
                };
            }
            catch { return null; }
        }
    }
}
