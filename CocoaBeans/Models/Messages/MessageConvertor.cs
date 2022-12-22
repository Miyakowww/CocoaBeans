// Copyright (c) Maila. All rights reserved.
// Licensed under the GNU AGPLv3

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Maila.Cocoa.Beans.Models.Messages
{
    public class GeneralMessageConvertor
    {
        public static IEnumerable<IMessage> Read(ref Utf8JsonReader reader, JsonSerializerOptions options)
        {
            return JsonSerializer.Deserialize<JsonElement>(ref reader, options)
                                 .EnumerateArray()
                                 .Select(Message.Parse)
                                 .OfType<IMessage>();
        }

        public static void Write(Utf8JsonWriter writer, IEnumerable<IMessage> value, JsonSerializerOptions options)
        {
            writer.WriteStartArray();
            foreach (IMessage m in value)
            {
                JsonSerializer.Serialize(writer, m, m switch
                {
                    ISourceMessage => typeof(ISourceMessage),
                    IQuoteMessage => typeof(IQuoteMessage),
                    IAtMessage => typeof(IAtMessage),
                    IAtAllMessage => typeof(IAtAllMessage),
                    IFaceMessage => typeof(IFaceMessage),
                    IPlainMessage => typeof(IPlainMessage),
                    IImageMessage => typeof(IImageMessage),
                    IFlashImageMessage => typeof(IFlashImageMessage),
                    IVoiceMessage => typeof(IVoiceMessage),
                    IXmlMessage => typeof(IXmlMessage),
                    IJsonMessage => typeof(IJsonMessage),
                    IAppMessage => typeof(IAppMessage),
                    IPokeMessage => typeof(IPokeMessage),
                    IDiceMessage => typeof(IDiceMessage),
                    IForwardMessage => typeof(IForwardMessage),
                    IFileMessage => typeof(IFileMessage),
                    IMusicShareMessage => typeof(IMusicShareMessage),
                    IMiraiCodeMessage => typeof(IMiraiCodeMessage),
                    _ => typeof(IMessage)
                }, options);
            }
            writer.WriteEndArray();
        }
    }

    public class MessageConvertor : JsonConverter<IMessage[]>
    {
        public override IMessage[] Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            => GeneralMessageConvertor.Read(ref reader, options).ToArray();

        public override void Write(Utf8JsonWriter writer, IMessage[] value, JsonSerializerOptions options)
            => GeneralMessageConvertor.Write(writer, value, options);
    }

    public class ForwardMessageConvertor : JsonConverter<ImmutableArray<IMessage>>
    {
        public override ImmutableArray<IMessage> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            => GeneralMessageConvertor.Read(ref reader, options).ToImmutableArray();

        public override void Write(Utf8JsonWriter writer, ImmutableArray<IMessage> value, JsonSerializerOptions options)
            => GeneralMessageConvertor.Write(writer, value, options);
    }
}
