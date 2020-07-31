using System;
using System.Net.Sockets;
using Waylong.Net;
using Waylong.Users;

namespace Waylong.Architecture.Server {

    /// <summary>
    /// 標準服務器模型架構
    /// </summary>
    public abstract class StdServerModel : CSModel {

        /// <summary>
        /// 客戶端連線積壓數
        /// </summary>
        protected abstract int Backlog { get; set; }

        /// <summary>
        /// 用戶管理
        /// </summary>
        protected UserManagement UserManagement = new UserManagement();

        #region Methods

        /// <summary>
        /// 等待客戶端_線程
        /// </summary>
        protected abstract void AwaitClientThread();

        /// <summary>
        /// 監聽封包_線程
        /// </summary>
        /// <param name="obj"></param>
        protected abstract void ReceivePacketThread(object obj);

        /// <summary>
        /// 在線判斷_線程
        /// </summary>
        /// <param name="socket"></param>
        protected abstract void AliveThread(object socket);

        /// <summary>
        /// 客戶端連線
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        /// <returns>連線成功與否</returns>
        protected bool Connect(string ip, int port) {

            //創建socket
            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            //創建連線Info
            var MainConn = new Connection(socket, ip, port);

            //啟動監聽
            IsClose = !NetworkManagement.StartToListen(ConnectionChannel.MainConnection, MainConn, Backlog);

            return !IsClose;
        }

        #endregion
    }
}
