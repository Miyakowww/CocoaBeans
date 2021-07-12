// Copyright (c) Maila. All rights reserved.
// Licensed under the GNU AGPLv3

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Maila.Cocoa.Beans.Exceptions;
using Maila.Cocoa.Beans.Models.Messages;

namespace Maila.Cocoa.Beans.API
{
    public static partial class MiraiAPI
    {
        /// <summary>Upload image files to the server.</summary>
        /// <exception cref="MiraiException" />
        /// <exception cref="WebException" />
        public static async Task<IImageMessage?> UploadImage(string host, string sessionKey, UploadType type, Stream imgStream)
        {
            MultipartFormDataContent content = new();

            StringContent _skey = new(sessionKey);
            _skey.Headers.ContentDisposition = new("form-data") { Name = "sessionKey" };
            content.Add(_skey);

            StringContent _type = new(type.ToString().ToLower());
            _type.Headers.ContentDisposition = new("form-data") { Name = "type" };
            content.Add(_type);

            using Image img = Image.FromStream(imgStream);
            await using MemoryStream ms = new();
            img.Save(ms, ImageFormat.Png);
            ms.Seek(0, SeekOrigin.Begin);
            StreamContent _img = new(ms);
            _img.Headers.ContentDisposition = new("form-data")
            {
                Name = "img",
                FileName = $"{Guid.NewGuid():n}.png"
            };
            _img.Headers.ContentType = new("image/png");
            content.Add(_img);

            using HttpClient client = new();
            using HttpResponseMessage respMsg = await client.PostAsync($"http://{host}/uploadImage", content);

            JsonElement res;
            if (!respMsg.IsSuccessStatusCode)
            {
                throw new WebException(respMsg.StatusCode.ToString());
            }

            res = JsonDocument.Parse(await respMsg.Content.ReadAsStringAsync()).RootElement;
            return ImageMessage.Parse(res);
        }

        /// <summary>Upload voice files to the server.</summary>
        /// <exception cref="MiraiException" />
        /// <exception cref="WebException" />
        public static async Task<IVoiceMessage?> UploadVoice(string host, string sessionKey, Stream voiceStream)
        {
            MultipartFormDataContent content = new();

            StringContent _skey = new(sessionKey);
            _skey.Headers.ContentDisposition = new("form-data") { Name = "sessionKey" };
            content.Add(_skey);

            StringContent _type = new("group");
            _type.Headers.ContentDisposition = new("form-data") { Name = "type" };
            content.Add(_type);

            StreamContent _voice = new(voiceStream);
            _voice.Headers.ContentDisposition = new("form-data")
            {
                Name = "voice",
                FileName = $"{Guid.NewGuid():n}.amr"
            };
            content.Add(_voice);

            using HttpClient client = new();
            using HttpResponseMessage respMsg = await client.PostAsync($"http://{host}/uploadVoice", content);

            JsonElement res;
            if (!respMsg.IsSuccessStatusCode)
            {
                throw new WebException(respMsg.StatusCode.ToString());
            }

            res = JsonDocument.Parse(await respMsg.Content.ReadAsStringAsync()).RootElement;
            return VoiceMessage.Parse(res);
        }

        /// <summary>Upload files to group.</summary>
        /// <exception cref="MiraiException" />
        /// <exception cref="WebException" />
        public static async Task<string?> UploadFileAndSend(string host, string sessionKey, long groupId, string path, Stream fileStream)
        {
            try
            {
                MultipartFormDataContent content = new();

                StringContent _skey = new(sessionKey);
                _skey.Headers.ContentDisposition = new("form-data") { Name = "sessionKey" };
                content.Add(_skey);

                StringContent _type = new("Group");
                _type.Headers.ContentDisposition = new("form-data") { Name = "type" };
                content.Add(_type);

                StringContent _target = new(groupId.ToString());
                _target.Headers.ContentDisposition = new("form-data") { Name = "target" };
                content.Add(_target);

                StringContent _path = new(path);
                _path.Headers.ContentDisposition = new("form-data") { Name = "path" };
                content.Add(_path);

                StreamContent _voice = new(fileStream);
                _voice.Headers.ContentDisposition = new("form-data") { Name = "file" };
                content.Add(_voice);

                using HttpClient client = new();
                using HttpResponseMessage respMsg = await client.PostAsync($"http://{host}/uploadFileAndSend", content);

                JsonElement res;
                if (!respMsg.IsSuccessStatusCode)
                {
                    throw new WebException(respMsg.StatusCode.ToString());
                }

                res = JsonDocument.Parse(await respMsg.Content.ReadAsStringAsync()).RootElement;
                int code = res.GetCode();
                if (code != 0)
                {
                    throw new MiraiException(code);
                }

                return res.GetProperty("id").GetString();
            }
            catch { return null; }
        }
    }

    public enum UploadType
    {
        Friend,
        Group,
        Temp
    }
}
