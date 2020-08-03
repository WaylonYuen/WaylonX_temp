using System;
using System.Net.Sockets;
using Waylong.Net;
using Waylong.Threading;
using Waylong.Users;

namespace Waylong.Architecture.Client {

    /// <summary>
    /// 標準服務器模型架構
    /// </summary>
    public abstract class StdClientModel : CSModel, IClientParameter {

        /// <summary>
        /// 名稱
        /// </summary>
        public abstract string Name { get; set; }

        /// <summary>
        /// 操作環境
        /// </summary>
        public abstract Environment Environment { get; }

        /// <summary>
        /// 用戶資訊
        /// </summary>
        public abstract User User { get; set; }

        /// <summary>
        /// 監聽封包_線程
        /// </summary>
        /// <param name="obj"></param>
        protected abstract void ReceivePacketThread();

        #region 已實作 Methods

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

        /// <summary>
        /// 啟動主連線
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="prot"></param>
        public override void Start(string ip, int port) {

            Console.WriteLine("正在連線...");

            if (Connect(ip, port)) {

                Initialize();   //初始化

                Console.WriteLine($"連接成功: {ip}:{port}");
            } else {
                Console.WriteLine($"連接失敗: {ip}:{port}");
            }

        }

        /// <summary>
        /// 停止運行
        /// </summary>
        public override void Close() {
            Close_Thread();

            Console.WriteLine("客戶端關閉...");
        }

        /// <summary>
        /// 服務器初始化
        /// </summary>
        protected override void Initialize() {

            //創建User
            User = new User(NetworkManagement.ConnectionDict[ConnectionChannel.MainConnection].Socket, NetworkState.Connected);

            Registered();   //註冊
            Start_Thread(); //啟動線程
        }

        /// <summary>
        /// 啟動線程
        /// </summary>
        protected override void Start_Thread() {
            IsClose = false;    // partial -> Thread

            Thread.Create(ReceivePacketThread, true).Start();
        }

        /// <summary>
        /// 關閉線程
        /// </summary>
        protected override void Close_Thread() {
            IsClose = true;     // partial -> Thread
        }

        #endregion

    }
}
