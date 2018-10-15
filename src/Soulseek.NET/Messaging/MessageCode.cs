﻿namespace Soulseek.NET.Messaging
{
    public enum MessageCode
    {
        Unknown = 0,
        Login = 1,
        SetListenPort = 2,
        GetPeerAddress = 3,
        AddUser = 5,
        GetStatus = 7,
        SayInChatRoom = 13,
        JoinRoom = 14,
        LeaveRoom = 15,
        UserJoinedRoom = 16,
        UserLeftRoom = 17,
        ConnectToPeer = 18,
        PrivateMessages = 22,
        AcknowledgePrivateMessage = 23,
        FileSearch = 26,
        SetOnlineStatus = 28,
        Ping = 32,
        SendSpeed = 34,
        SharedFoldersAndFiles = 35,
        GetUserStats = 36,
        QueuedDownloads = 40,
        KickedFromServer = 41,
        UserSearch = 42,
        InterestAdd = 51,
        InterestRemove = 52,
        GetRecommendations = 54,
        GetGlobalRecommendations = 56,
        GetUserInterests = 57,
        RoomList = 64,
        ExactFileSearch = 65,
        GlobalAdminMessage = 66,
        PrivilegedUsers = 69,
        HaveNoParents = 71,
        ParentsIP = 73,
        ParentMinSpeed = 83,
        ParentSpeedRatio = 84,
        ParentInactivityTimeout = 86,
        SearchInactivityTimeout = 87,
        MinimumParentsInCache = 88,
        DistributedAliveInterval = 90,
        AddPrivilegedUser = 91,
        CheckPrivileges = 92,
        SearchRequest = 93,
        AcceptChildren = 100,
        NetInfo = 102,
        WishlistSearch = 103,
        WishlistInterval = 104,
        GetSimilarUsers = 110,
        GetItemRecommendations = 111,
        GetItemSimilarUsers = 112,
        RoomTickers = 113,
        RoomTickerAdd = 114,
        RoomTickerRemove = 115,
        SetRoomTicker = 116,
        HatedInterestAdd = 117,
        HatedInterestRemove = 118,
        RoomSearch = 120,
        SendUploadSpeed = 121,
        UserPrivileges = 122,
        GivePrivileges = 123,
        NotifyPrivileges = 124,
        AcknowledgeNotifyPrivileges = 125,
        BranchLevel = 126,
        BranchRoot = 127,
        ChildDepth = 129,
        PrivateRoomUsers = 133,
        PrivateRoomAddUser = 134,
        PrivateRoomRemoveUser = 135,
        PrivateRoomDropMembership = 136,
        PrivateRoomDropOwnership = 137,
        PrivateRoomUnknown = 138,
        PrivateRoomAdded = 139,
        PrivateRoomRemoved = 140,
        PrivateRoomToggle = 141,
        NewPassword = 142,
        PrivateRoomAddOperator = 143,
        PrivateRoomRemoveOperator = 144,
        PrivateRoomOperatorAdded = 145,
        PrivateRoomOperatorRemoved = 146,
        PrivateRoomOwned = 148,
        MessageUsers = 149,
        AskPublicChat = 150,
        StopPublicChat = 151,
        PublicChat = 152,
        CannotConnect = 1001,
    }
}