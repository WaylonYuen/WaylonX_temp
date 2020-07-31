using System;
using System.Net.Sockets;
using Waylong.Net;

namespace Waylong.Architecture.Client {

    /// <summary>
    /// 標準服務器模型架構
    /// </summary>
    public abstract class StdClientModel : CSModel {

        /// <summary>
        /// 監聽封包_線程
        /// </summary>
        /// <param name="obj"></param>
        protected abstract void ReceivePacketThread();

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

            //啟動連接
            IsClose = !NetworkManagement.StartToConnect(ConnectionChannel.MainConnection, MainConn);

            return !IsClose;
        }
    }
}
