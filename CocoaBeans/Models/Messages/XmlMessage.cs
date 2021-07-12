// Copyright (c) Maila. All rights reserved.
// Licensed under the GNU AGPLv3

using System.Text.Json;
using System.Text.Json.Serialization;

namespace Maila.Cocoa.Beans.Models.Messages
{
    public interface IXmlMessage : IMessage
    {
        [JsonPropertyName("type")]
        public string Type { get; }

        [JsonPropertyName("xml")]
        public string Xml { get; }
    }

    public class XmlMessage : Message, IXmlMessage
    {
        public string Xml { get; init; } = string.Empty;

        private XmlMessage() : base("Xml") { }

        public XmlMessage(string xml) : base("Xml")
        {
            Xml = xml;
        }

        internal new static XmlMessage? Parse(JsonElement body)
        {
            try
            {
                return new(body.GetProperty("xml").GetString() ?? string.Empty);
            }
            catch { return null; }
        }
    }
}
