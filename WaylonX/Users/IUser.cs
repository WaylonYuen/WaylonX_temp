using System;
using System.Collections.Generic;
using System.Net.Sockets;
using WaylonX.Net;
using WaylonX.Packets;

namespace WaylonX.Users {

    /// <summary>
    /// 用戶接口: 封包身份驗證接口
    /// </summary>
    public interface IUser : IUserNetwork {

        /// <summary>
        /// 取得用戶身份驗證碼
        /// </summary>
        int VerificationCode { get; }

        /// <summary>
        /// 設定用戶身份驗證碼
        /// </summary>
        /// <param name="verificationCode"></param>
        void SetVerificationCode(int verificationCode);
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

        /// <summary>
        /// 設定網路參數
        /// </summary>
        void SetNetworkMetrics(Socket socket, NetworkState state);

        /// <summary>
        /// 設定Socket
        /// </summary>
        /// <param name="socket"></param>
        bool SetSocket(Socket socket);

        /// <summary>
        /// 設定網路狀態
        /// </summary>
        /// <param name="networkState"></param>
        void SetNetworkState(NetworkState state);

        /// <summary>
        /// 發送封包方法
        /// </summary>
        /// <param name="netPacket">網路封包</param>
        void Send(IPacket packet);

        /// <summary>
        /// 異步發送網路封包
        /// </summary>
        /// <param name="packet">網路封包</param>
        void BeginSend(IPacket packet);
    }

}