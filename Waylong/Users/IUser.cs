using System;
using System.Collections.Generic;
using System.Net.Sockets;
using Waylong.Net;
using Waylong.Packets;

namespace Waylong.Users {

    /// <summary>
    /// 用戶接口: 封包身份驗證接口
    /// </summary>
    public interface IUser : IUserNetwork {

        /// <summary>
        /// 取得用戶身份驗證碼
        /// </summary>
        int VerificationCode { get; }

        /// <summary>
        /// 發送封包方法
        /// </summary>
        /// <param name="netPacket">網路封包</param>
        void Send(IPacket packet);
    }

    /// <summary>
    /// 用戶網路接口: 用戶狀態接口
    /// </summary>
    public interface IUserNetwork {

        /// <summary>
        /// 網路Socket
        /// </summary>
        Socket Socket { get; }

        /// <summary>
        /// 網路狀態
        /// </summary>
        NetworkState NetworkState { get; }

    }

}