// Copyright (c) Maila. All rights reserved.
// Licensed under the GNU AGPLv3

using System;

namespace Maila.Cocoa.Beans.Exceptions
{
    [Serializable]
    public class UnsupportedMessageTypeException : Exception
    {
        public UnsupportedMessageTypeException(string message) : base(message) { }
    }
}
