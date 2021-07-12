// Copyright (c) Maila. All rights reserved.
// Licensed under the GNU AGPLv3

using System.Text.Json;

namespace Maila.Cocoa.Beans.Models.Files
{
    public class FileSummary
    {
        public string Name { get; init; }
        public string Id { get; init; }
        public string Path { get; init; }
        public bool IsFile { get; init; }

        private FileSummary()
        {
            Name = string.Empty;
            Id = string.Empty;
            Path = string.Empty;
        }

        internal static FileSummary? Parse(JsonElement body)
        {
            try
            {
                return new()
                {
                    Name = body.GetProperty("name").GetString() ?? string.Empty,
                    Id = body.GetProperty("id").GetString() ?? string.Empty,
                    Path = body.GetProperty("path").GetString() ?? string.Empty,
                    IsFile = body.GetProperty("isFile").GetBoolean()
                };
            }
            catch
            {
                return null;
            }
        }
    }
}
