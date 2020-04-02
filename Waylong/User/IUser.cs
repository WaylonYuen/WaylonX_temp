using System;
using System.Collections.Generic;
using System.Net.Sockets;
using Waylong.Packets;

namespace Waylong.User {

    /// <summary>
    /// 客戶端用戶接口: 用戶狀態接口, 封包身份驗證接口
    /// </summary>
    public interface IUser : IUserNetStates, IPacketIdentity {

        /// <summary>
        /// 用戶Socket
        /// </summary>
        Socket GetSocket { get; }

        /// <summary>
        /// 發送封包方法
        /// </summary>
        /// <param name="netPacket">網路封包</param>
        void Send(INetPacket netPacket);
    }

    public interface IUserNetStates {

        /// <summary>
        /// 用戶網路狀態
        /// </summary>
        NetStates GetNetStates { get; }

    }

    public enum NetStates {
        None,
        Connected,
        Connecting,
        Disconnect,
        Overtime,
    }

}