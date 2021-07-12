// Copyright (c) Maila. All rights reserved.
// Licensed under the GNU AGPLv3

namespace Maila.Cocoa.Beans.Models.Messages
{
    public interface IAtAllMessage : IMessage { }

    public class AtAllMessage : Message, IAtAllMessage
    {
        private AtAllMessage() : base("AtAll") { }

        public static readonly AtAllMessage Instance = new();
    }
}
