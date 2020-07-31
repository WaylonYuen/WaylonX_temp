using System;
using System.Collections.Generic;
using System.Net.Sockets;
using Waylong.Net;
using Waylong.Users;
using Waylong.Threading;
using System.Diagnostics;
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

            Console.WriteLine("正在啟動服務器...");

            if (Connect(ip, port)) {

                Registered();   //註冊
                Initialize();   //初始化
                Start_Thread(); //啟動線程

                Console.WriteLine($"服務器啟動成功: {ip}:{port}");
            } else {
                Console.WriteLine($"服務器啟動失敗: {ip}:{port}");
            }

        }

        /// <summary>
        /// 停止運行
        /// </summary>
        public override void Close() {
            Close_Thread();

            Console.WriteLine("服務器關閉...");
        }

        /// <summary>
        /// 服務器初始化: 無序
        /// </summary>
        protected override void Initialize() {
            Backlog = 5;
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

            var BlockAccept = new StdClient();
            BlockAccept.Start("127.0.0.1", 8808);
            BlockAccept.Close();
        }

        #endregion
    }
}
