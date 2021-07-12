// Copyright (c) Maila. All rights reserved.
// Licensed under the GNU AGPLv3

using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Maila.Cocoa.Beans.Models.Messages
{
    public interface IPlainMessage : IMessage
    {
        [JsonPropertyName("type")]
        public string Type { get; }

        [JsonPropertyName("text")]
        public string Text { get; }
    }

    public class PlainMessage : Message, IPlainMessage
    {
        public string Text { get; }

        private PlainMessage() : base("Plain")
        {
            Text = string.Empty;
        }

        public PlainMessage(string text) : base("Plain")
        {
            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentException("Cannot create PlainMessage with null or empty text.");
            }
            Text = text;
        }

        internal new static PlainMessage? Parse(JsonElement body)
        {
            string? text;
            try
            {
                text = body.GetProperty("text").GetString();
            }
            catch { return null; }

            return text is null ? null : new(text);
        }
    }
}
