// Copyright (c) Maila. All rights reserved.
// Licensed under the GNU AGPLv3

using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Maila.Cocoa.Beans.Exceptions;
using Maila.Cocoa.Beans.Models.Files;

namespace Maila.Cocoa.Beans.API
{
    public static partial class MiraiAPI
    {
        /// <summary>Get the list of the group files.</summary>
        /// <exception cref="WebException" />
        public static async Task<FileSummary[]> GetGroupFileList(string host, string sessionKey, long groupId, string? dir = null)
        {
            var res = dir is null
                ? await GetAsRequest(host, "groupFileList", $"sessionKey={sessionKey}", $"target={groupId}")
                : await GetAsRequest(host, "groupFileList", $"sessionKey={sessionKey}", $"target={groupId}", $"dir={dir}");
            List<FileSummary> files = new();
            foreach (var file in res.EnumerateArray())
            {
                var sum = FileSummary.Parse(file);
                if (sum is not null)
                {
                    files.Add(sum);
                }
            }
            return files.ToArray();
        }

        /// <summary>Get the details of a specified file.</summary>
        /// <exception cref="WebException" />
        public static async Task<FileDetails> GetGroupFileInfo(string host, string sessionKey, long groupId, string fileId)
        {
            var res = await GetAsRequest(host, "groupFileInfo", $"sessionKey={sessionKey}", $"target={groupId}", $"id={fileId}");
            return FileDetails.Parse(res) ?? throw new Exception("Invalid response.");
        }

        /// <summary>Rename a file.</summary>
        /// <exception cref="MiraiException" />
        /// <exception cref="WebException" />
        public static async Task RenameGroupFile(string host, string sessionKey, long groupId, string fileId, string newName)
        {
            int code = (await new { sessionKey, target = groupId, id = fileId, rename = newName }.PostAsRequest(host, "groupFileRename")).GetCode();
            if (code != 0)
            {
                throw new MiraiException(code);
            }
        }

        /// <summary>Make a group file directory.</summary>
        /// <exception cref="MiraiException" />
        /// <exception cref="WebException" />
        public static async Task MakeFileDirectory(string host, string sessionKey, long groupId, string directoryName)
        {
            int code = (await new { sessionKey, target = groupId, dir = directoryName }.PostAsRequest(host, "groupMkdir")).GetCode();
            if (code != 0)
            {
                throw new MiraiException(code);
            }
        }

        /// <summary>Move specified file to a new directory.</summary>
        /// <exception cref="MiraiException" />
        /// <exception cref="WebException" />
        public static async Task MoveGroupFile(string host, string sessionKey, long groupId, string fileId, string newPath)
        {
            int code = (await new { sessionKey, target = groupId, id = fileId, movePath = newPath }.PostAsRequest(host, "groupFileMove")).GetCode();
            if (code != 0)
            {
                throw new MiraiException(code);
            }
        }

        /// <summary>Delete a file.</summary>
        /// <exception cref="MiraiException" />
        /// <exception cref="WebException" />
        public static async Task DeleteGroupFile(string host, string sessionKey, long groupId, string fileId)
        {
            int code = (await new { sessionKey, target = groupId, id = fileId }.PostAsRequest(host, "groupFileDelete")).GetCode();
            if (code != 0)
            {
                throw new MiraiException(code);
            }
        }
    }
}
