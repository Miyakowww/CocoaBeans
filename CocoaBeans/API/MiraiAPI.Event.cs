// Copyright (c) Maila. All rights reserved.
// Licensed under the GNU AGPLv3

using System;
using System.IO;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Maila.Cocoa.Beans.Models.Events;

namespace Maila.Cocoa.Beans.API
{
    public static partial class MiraiAPI
    {
        public static void ListenMessageEvent(string host, string sessionKey, Action<Event> onReceive, Action? onDisconnected, CancellationToken token)
        {
            StartListening($"ws://{host}/message?sessionKey={sessionKey}",
                res =>
                {
                    try
                    {
                        var body = JsonDocument.Parse(Encoding.UTF8.GetString(res)).RootElement;
                        var @event = Event.Parse(body);
                        if (@event is not null)
                        {
                            onReceive.Invoke(@event);
                        }
                    }
                    catch { }
                },
                onDisconnected, token);
        }

        public static void ListenBotEvent(string host, string sessionKey, Action<Event> onReceive, Action? onDisconnected, CancellationToken token)
        {
            StartListening($"ws://{host}/event?sessionKey={sessionKey}",
                res =>
                {
                    try
                    {
                        var body = JsonDocument.Parse(Encoding.UTF8.GetString(res)).RootElement;
                        var @event = Event.Parse(body);
                        if (@event is not null)
                        {
                            onReceive.Invoke(@event);
                        }
                    }
                    catch { }
                },
                onDisconnected, token);
        }

        public static void ListenAllEvent(string host, string sessionKey, Action<Event> onReceive, Action? onDisconnected, CancellationToken token)
        {
            StartListening($"ws://{host}/all?sessionKey={sessionKey}",
                res =>
                {
                    try
                    {
                        var body = JsonDocument.Parse(Encoding.UTF8.GetString(res)).RootElement;
                        var @event = Event.Parse(body);
                        if (@event is not null)
                        {
                            onReceive.Invoke(@event);
                        }
                    }
                    catch { }
                },
                onDisconnected, token);
        }


        private static async void StartListening(string url, Action<byte[]> onReceive, Action? onDisconnected, CancellationToken token)
        {
            try
            {
                using ClientWebSocket ws = new();
                await ws.ConnectAsync(new(url), token);
                while (ws.State == WebSocketState.Open)
                {
                    byte[] buff = new byte[1024];
                    await using MemoryStream stream = new(1024);
                    while (true)
                    {
                        var result = await ws.ReceiveAsync(buff, token);
                        await stream.WriteAsync(buff.AsMemory(0, result.Count), token);
                        if (result.EndOfMessage)
                        {
                            break;
                        }
                    }

                    byte[] data = stream.ToArray();
                    try
                    {
                        _ = Task.Run(() => onReceive.Invoke(data), CancellationToken.None);
                    }
                    catch { }
                }

                ws.Abort();
            }
            catch (WebSocketException) { }
            catch (OperationCanceledException) { }
            finally
            {
                try
                {
                    onDisconnected?.Invoke();
                }
                catch { }
            }
        }
    }
}
