using System;
using System.Collections.Generic;
using System.Net.Sockets;
using Waylong.Net;
using Waylong.Users;
using Waylong.Threading;
using System.Diagnostics;

namespace Waylong.Architecture.Server {


    /// <summary>
    /// 標準服務器架構
    /// </summary>
    public partial class StdServer : StdServerModel, ICSParameter {

        #region Property

        /// <summary>
        /// 名稱
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 操作環境
        /// </summary>
        public virtual Environment Environment { get { return Environment.Terminal; } }

        #endregion

        #region Methods

        /// <summary>
        /// 啟動主連線
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="prot"></param>
        public override void Start(string ip, int port) {

            Console.WriteLine("正在啟動服務器...");

            //創建socket
            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            //創建連線Info
            var MainConn = new Connection(socket, ip, port);
            
            //啟動監聽
            IsClose = !NetworkManagement.StartToListen(ConnectionChannel.MainConnection, MainConn, 10);

            Console.WriteLine($"服務器啟動成功: {ip}:{port}");

            //方法2
            //IConnection iMainConn = MainConn;
            //iMainConn.Listen(10);
            //NetworkManagement.Add(ConnectionChannel.MainConnection, MainConn);
        }

        /// <summary>
        /// 停止運行
        /// </summary>
        public override void Close() {
            Console.WriteLine("服務器關閉...");
        }

        /// <summary>
        /// 服務器初始化
        /// </summary>
        protected override void Initialize() {

        }

        /// <summary>
        /// 資料架構
        /// </summary>
        protected override void DataStruct() {
        }     

        /// <summary>
        /// 註冊器
        /// </summary>
        protected override void Registered() {

        }

        [Obsolete("Undone", true)]
        /// <summary>
        /// 佇列分配器 : 分配封包到對應的佇列隊伍中
        /// </summary>
        protected override void QueueDistributor() {

        }

        /// <summary>
        /// 啟動線程
        /// </summary>
        protected override void Start_Thread() {
            IsClose = false;   // partial -> Thread

            //啟動監聽等待客戶端線程
            Thread.Create(AwaitClientThread, true).Start(); //啟動等待客戶端線程
        }

        /// <summary>
        /// 關閉線程
        /// </summary>
        protected override void Close_Thread() {
            IsClose = true;   // partial -> Thread
        }

        #endregion
    }
}
