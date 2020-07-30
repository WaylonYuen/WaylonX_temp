using System;
using System.Net.Sockets;
using Waylong.Net;

namespace Waylong.Architecture.Client {

    public partial class StdClient : StdClientModel, ICSParameter {

        #region Property

        /// <summary>
        /// 名稱
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 操作環境
        /// </summary>
        public virtual Environment Environment { get => Environment.Terminal; }

        #endregion

        public StdClient() {

        }




        #region Methods

        /// <summary>
        /// 啟動主連線
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="prot"></param>
        public override void Start(string ip, int port) {

            //創建socket
            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            //創建連線Info
            var MainConn = new Connection(socket, ip, port);

            //啟動連接
            IsClose = !NetworkManagement.StartToConnect(ConnectionChannel.MainConnection, MainConn);

            Console.WriteLine("連接成功");
        }

        /// <summary>
        /// 停止運行
        /// </summary>
        public override void Close() {

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

        }

        /// <summary>
        /// 關閉線程
        /// </summary>
        protected override void Close_Thread() {

        }

        /// <summary>
        /// 執行_回調線程
        /// </summary>
        protected override void Execute_CallbackThread() {
            
        }

        #endregion
    }

}
