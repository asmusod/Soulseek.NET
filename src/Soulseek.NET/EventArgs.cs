﻿namespace Soulseek.NET
{
    using Soulseek.NET.Messaging;
    using Soulseek.NET.Messaging.Responses;
    using Soulseek.NET.Tcp;
    using System;

    public class SearchResultReceivedEventArgs : NetworkEventArgs
    {
        public SearchResponse Result { get; set; }

        public SearchResultReceivedEventArgs(NetworkEventArgs e)
        {
            Address = e.Address;
            IPAddress = e.IPAddress;
            Port = e.Port;
        }
    }

    public class SearchCompletedEventArgs : EventArgs
    {
        public int Ticket { get; set; }

    }

    public class MessageReceivedEventArgs : NetworkEventArgs
    {
        public Message Message { get; set; }

        public MessageReceivedEventArgs(NetworkEventArgs e)
        {
            Address = e.Address;
            IPAddress = e.IPAddress;
            Port = e.Port;
        }
    }
}
