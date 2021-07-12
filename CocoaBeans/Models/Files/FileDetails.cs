// Copyright (c) Maila. All rights reserved.
// Licensed under the GNU AGPLv3

using System.Text.Json;

namespace Maila.Cocoa.Beans.Models.Files
{
    public class FileDetails
    {
        public string Name { get; init; }
        public string Path { get; init; }
        public string Id { get; init; }
        public long Length { get; init; }
        public int DownloadTimes { get; init; }
        public long UploaderId { get; init; }
        public int UploadTime { get; init; }
        public int LastModifyTime { get; init; }
        public string DownloadUrl { get; init; }
        public string SHA1 { get; init; }
        public string MD5 { get; init; }

        private FileDetails()
        {
            Name = string.Empty;
            Path = string.Empty;
            Id = string.Empty;
            DownloadUrl = string.Empty;
            SHA1 = string.Empty;
            MD5 = string.Empty;
        }

        internal static FileDetails? Parse(JsonElement body)
        {
            try
            {
                return new()
                {
                    Name = body.GetProperty("name").GetString() ?? string.Empty,
                    Path = body.GetProperty("path").GetString() ?? string.Empty,
                    Id = body.GetProperty("id").GetString() ?? string.Empty,
                    Length = body.GetProperty("length").GetInt64(),
                    DownloadTimes = body.GetProperty("downloadTimes").GetInt32(),
                    UploaderId = body.GetProperty("uploaderId").GetInt64(),
                    UploadTime = body.GetProperty("uploadTime").GetInt32(),
                    LastModifyTime = body.GetProperty("lastModifyTime").GetInt32(),
                    DownloadUrl = body.GetProperty("downloadUrl").GetString() ?? string.Empty,
                    SHA1 = body.GetProperty("sha1").GetString() ?? string.Empty,
                    MD5 = body.GetProperty("md5").GetString() ?? string.Empty,
                };
            }
            catch
            {
                return null;
            }
        }
    }
}
