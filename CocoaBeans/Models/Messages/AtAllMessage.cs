// Copyright (c) Maila. All rights reserved.
// Licensed under the GNU AGPLv3

using System.Text.Json.Serialization;

namespace Maila.Cocoa.Beans.Models.Messages
{
    public interface IAtAllMessage : IMessage
    {
        [JsonPropertyName("type")]
        public string Type { get; }
    }

    public class AtAllMessage : Message, IAtAllMessage
    {
        private AtAllMessage() : base("AtAll") { }

        public static readonly AtAllMessage Instance = new();
    }
}
