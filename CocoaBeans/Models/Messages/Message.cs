// Copyright (c) Maila. All rights reserved.
// Licensed under the GNU AGPLv3

using System.Text.Json;
using Maila.Cocoa.Beans.Exceptions;

namespace Maila.Cocoa.Beans.Models.Messages
{
    public interface IMessage { }

    public abstract class Message : IMessage
    {
        public string Type { get; }

        protected Message(string type)
        {
            Type = type;
        }

        /// <exception cref="UnsupportedMessageTypeException" />
        internal static Message? Parse(JsonElement body)
        {
            return body.GetProperty("type").GetString() switch
            {
                "Source" => SourceMessage.Parse(body),
                "Quote" => QuoteMessage.Parse(body),
                "At" => AtMessage.Parse(body),
                "AtAll" => AtAllMessage.Instance,
                "Face" => FaceMessage.Parse(body),
                "Plain" => PlainMessage.Parse(body),
                "Image" => ImageMessage.Parse(body),
                "FlashImage" => FlashImageMessage.Parse(body),
                "Voice" => VoiceMessage.Parse(body),
                "Xml" => XmlMessage.Parse(body),
                "Json" => JsonMessage.Parse(body),
                "App" => AppMessage.Parse(body),
                "Poke" => PokeMessage.Parse(body),
                "Forward" => ForwardMessage.Parse(body),
                "File" => FileMessage.Parse(body),
                "MusicShare" => MusicShareMessage.Parse(body),
                { Length: > 0 } type => throw new UnsupportedMessageTypeException(type),
                _ => null
            };
        }
    }
}
