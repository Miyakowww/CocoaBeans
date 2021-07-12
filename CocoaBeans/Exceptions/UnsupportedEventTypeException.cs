// Copyright (c) Maila. All rights reserved.
// Licensed under the GNU AGPLv3

using System;

namespace Maila.Cocoa.Beans.Exceptions
{
    [Serializable]
    public class UnsupportedEventTypeException : Exception
    {
        public UnsupportedEventTypeException(string message) : base(message) { }
    }
}
