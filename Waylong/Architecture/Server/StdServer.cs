using System;
using System.Collections.Generic;
using System.Net.Sockets;
using Waylong.Net;
using Waylong.Users;
using Waylong.Threading;
using System.Diagnostics;
using Waylong.Architecture;
using Waylong.Architecture.Client;

namespace Waylong.Architecture.Server {


    /// <summary>
    /// 標準服務器架構
    /// </summary>
    public partial class StdServer : StdServerModel, IServerParameter {

        #region Property

        /// <summary>
        /// 名稱
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 操作環境
        /// </summary>
        public virtual Environment Environment { get { return Environment.Terminal; } }

        /// <summary>
        /// 客戶端連線積壓數
        /// </summary>
        protected override int Backlog { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// 啟動主連線
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="prot"></param>
        public override void Start(string ip, int port) {

            Logger.Info("服務器正在啟動...");
            if (Connect(ip, port)) {

                Initialize();   //初始化

                Logger.Info("服務器啟動成功");
            } else {
                Logger.Warn("服務器啟動失敗");
                //undone: 執行失敗程序
            }

        }

        /// <summary>
        /// 停止運行
        /// </summary>
        public override void Close() {

            Logger.Info("服務器正在關閉...");

            //關閉線程
            Close_Thread();

            //～異步執行 -> LoggerQueueue中的內容輸出, 直至確認所有需要關閉的線程進行回應（要做超時判斷)

            //～抓出自己的socket關閉
            //var socket = NetworkManagement.ConnectionDict[ConnectionChannel.MainConnection].Socket;
            //socket.Shutdown(SocketShutdown.Both);
            //socket.Close();

            //～清除UserList


        }

        /// <summary>
        /// 服務器初始化: 無序
        /// </summary>
        protected override void Initialize() {
            Backlog = 5;

            Registered();   //註冊

            //HACK: 設定線程池中的線程
            //System.Threading.ThreadPool.SetMinThreads(3, 3);
            //System.Threading.ThreadPool.SetMaxThreads(0, 0);

            Start_Thread(); //啟動線程
        }

        /// <summary>
        /// 資料架構
        /// </summary>
        protected override void DataStruct() { }     

        /// <summary>
        /// 註冊器
        /// </summary>
        protected override void Registered() { }

        [Obsolete("Undone", true)]
        /// <summary>
        /// 佇列分配器 : 分配封包到對應的佇列隊伍中
        /// </summary>
        protected override void QueueDistributor() { }

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

            //調取Server連線Info
            if (NetworkManagement.ConnectionDict.ContainsKey(ConnectionChannel.MainConnection)) {
                var IPEndPoint = NetworkManagement.ConnectionDict[ConnectionChannel.MainConnection].IPEndPoint; //取得EndPoint

                //建立客戶端 -> 讓Server Accept()中斷從而跳出Await線程
                var client = new TcpClient();
                client.Connect(IPEndPoint.Address.ToString(), IPEndPoint.Port);
                client.Close();
            }

        }

        #endregion
    }
}
