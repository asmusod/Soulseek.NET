﻿// <copyright file="BrowseResponse.cs" company="JP Dillingham">
//     Copyright (c) JP Dillingham. All rights reserved.
//
//     This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as
//     published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
//
//     This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty
//     of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the GNU General Public License for more details.
//
//     You should have received a copy of the GNU General Public License along with this program. If not, see https://www.gnu.org/licenses/.
// </copyright>

namespace Soulseek.Messaging.Messages
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Soulseek.Exceptions;

    /// <summary>
    ///     The response to a peer browse request.
    /// </summary>
    public sealed class BrowseResponse
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="BrowseResponse"/> class.
        /// </summary>
        /// <param name="directoryCount">The optional directory count.</param>
        /// <param name="directoryList">The optional directory list.</param>
        public BrowseResponse(int directoryCount, IEnumerable<Directory> directoryList = null)
        {
            DirectoryCount = directoryCount;
            DirectoryList = directoryList ?? Array.Empty<Directory>();
        }

        /// <summary>
        ///     Gets the list of directories.
        /// </summary>
        public IReadOnlyCollection<Directory> Directories => DirectoryList.ToList().AsReadOnly();

        /// <summary>
        ///     Gets the number of directories.
        /// </summary>
        public int DirectoryCount { get; }

        private IEnumerable<Directory> DirectoryList { get; }

        /// <summary>
        ///     Parses a new instance of <see cref="BrowseResponse"/> from the specified <paramref name="message"/>.
        /// </summary>
        /// <param name="message">The message from which to parse.</param>
        /// <returns>The parsed instance.</returns>
        public static BrowseResponse Parse(Message message)
        {
            var reader = new MessageReader(message);

            if (reader.Code != MessageCode.PeerBrowseResponse)
            {
                throw new MessageException($"Message Code mismatch creating Peer Browse Response (expected: {(int)MessageCode.PeerBrowseResponse}, received: {(int)reader.Code}");
            }

            reader.Decompress();

            var directoryCount = reader.ReadInteger();
            var directoryList = new List<Directory>();

            for (int i = 0; i < directoryCount; i++)
            {
                var dir = new Directory(
                    directoryname: reader.ReadString(),
                    fileCount: reader.ReadInteger());

                var fileList = new List<File>();

                for (int j = 0; j < dir.FileCount; j++)
                {
                    var file = new File(
                        code: reader.ReadByte(),
                        filename: reader.ReadString(),
                        size: reader.ReadLong(),
                        extension: reader.ReadString(),
                        attributeCount: reader.ReadInteger());

                    var attributeList = new List<FileAttribute>();

                    for (int k = 0; k < file.AttributeCount; k++)
                    {
                        var attribute = new FileAttribute(
                            type: (FileAttributeType)reader.ReadInteger(),
                            value: reader.ReadInteger());

                        attributeList.Add(attribute);
                    }

                    fileList.Add(new File(
                        code: file.Code,
                        filename: file.Filename,
                        size: file.Size,
                        extension: file.Extension,
                        attributeCount: file.AttributeCount,
                        attributeList: attributeList));
                }

                directoryList.Add(new Directory(
                    directoryname: dir.Directoryname,
                    fileCount: dir.FileCount,
                    fileList: fileList));
            }

            return new BrowseResponse(directoryCount, directoryList);
        }
    }
}