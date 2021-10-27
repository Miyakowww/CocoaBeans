// Copyright (c) Maila. All rights reserved.
// Licensed under the GNU AGPLv3

using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Maila.Cocoa.Beans.Models.Messages;

namespace Maila.Cocoa.Beans.API
{
    public static partial class MiraiAPI
    {
        private static readonly JsonSerializerOptions serializerOptions = new();

        static MiraiAPI()
        {
            serializerOptions.Converters.Add(new MessageConvertor());
            serializerOptions.Converters.Add(new ForwardMessageConvertor());
        }

        /// <summary>Get mirai-api-http version number.</summary>
        public static async Task<string?> About(string host)
        {
            try
            {
                var res = await GetAsRequest(host, "about");
                return res.GetProperty("data").GetProperty("version").GetString();
            }
            catch { return null; }
        }

        /// <exception cref="WebException" />
        private static async Task<JsonElement> PostAsRequest(this object obj, string host, string method)
        {
            return JsonDocument.Parse(await PostAsync($"http://{host}/{method}", JsonSerializer.Serialize(obj, obj.GetType(), serializerOptions))).RootElement;
        }

        /// <exception cref="WebException" />
        private static async Task<JsonElement> GetAsRequest(string host, string method, params string[] args)
        {
            return JsonDocument.Parse(await GetAsync($"http://{host}/{method}?{string.Join('&', args)}")).RootElement;
        }

        /// <exception cref="WebException" />
        private static async Task<string> PostAsync(string url, string body)
        {
            using HttpClient client = new();
            using StringContent content = new(body, Encoding.UTF8);
            using HttpResponseMessage res = await client.PostAsync(url, content);

            if (!res.IsSuccessStatusCode)
            {
                throw new WebException($"The remote server returned an error: ({res.StatusCode}) {res.ReasonPhrase}.");
            }
            return await res.Content.ReadAsStringAsync();
        }

        /// <exception cref="WebException" />
        private static async Task<string> GetAsync(string url)
        {
            using HttpClient client = new();
            using HttpResponseMessage res = await client.GetAsync(url);

            if (!res.IsSuccessStatusCode)
            {
                throw new WebException($"The remote server returned an error: ({res.StatusCode}) {res.ReasonPhrase}.");
            }
            return await res.Content.ReadAsStringAsync();
        }

        private static int GetCode(this JsonElement obj)
        {
            return obj.GetProperty("code").GetInt32();
        }
    }
}
