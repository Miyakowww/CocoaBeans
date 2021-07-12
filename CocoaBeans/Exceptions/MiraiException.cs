// Copyright (c) Maila. All rights reserved.
// Licensed under the GNU AGPLv3

using System;

namespace Maila.Cocoa.Beans.Exceptions
{
    [Serializable]
    public class MiraiException : Exception
    {
        public int StatusCode { get; }

        public MiraiException(int statusCode) : base(statusCode switch
        {
            0 => "Success",
            1 => "Wrong auth key",
            2 => "The specified Bot does not exist",
            3 => "Session is invalid or does not exist",
            4 => "Session is not authenticated or not activated",
            5 => "The destination of the message or the specified object does not exist",
            6 => "The specified file does not exist",
            10 => "No operation authority",
            20 => "The bot is banned",
            30 => "Message is too long",
            400 => "Bad access",
            _ => $"Unknown status({statusCode})."
        })
        => StatusCode = statusCode;
    }
}
