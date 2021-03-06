﻿// <copyright file="PeerQueueFailedResponse.cs" company="JP Dillingham">
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
    using Soulseek.Exceptions;

    /// <summary>
    ///     The response received when an attempt to queue a file for downloading has failed.
    /// </summary>
    internal sealed class PeerQueueFailedResponse
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="PeerQueueFailedResponse"/> class.
        /// </summary>
        /// <param name="filename">The filename which failed to be queued.</param>
        /// <param name="message">The reason for the failure.</param>
        internal PeerQueueFailedResponse(string filename, string message)
        {
            Filename = filename;
            Message = message;
        }

        /// <summary>
        ///     Gets the filename which failed to be queued.
        /// </summary>
        public string Filename { get; }

        /// <summary>
        ///     Gets the reason for the failure.
        /// </summary>
        public string Message { get; }

        /// <summary>
        ///     Parses a new instance of <see cref="PeerQueueFailedResponse"/> from the specified <paramref name="message"/>.
        /// </summary>
        /// <param name="message">The message from which to parse.</param>
        /// <returns>The parsed instance.</returns>
        public static PeerQueueFailedResponse Parse(Message message)
        {
            var reader = new MessageReader(message);

            if (reader.Code != MessageCode.PeerQueueFailed)
            {
                throw new MessageException($"Message Code mismatch creating Peer Queue Failed Response (expected: {(int)MessageCode.PeerQueueFailed}, received: {(int)reader.Code}.");
            }

            var filename = reader.ReadString();
            var msg = reader.ReadString();

            return new PeerQueueFailedResponse(filename, msg);
        }
    }
}