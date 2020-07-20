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
        public Environment Environment { get { return Environment.Terminal; } }


        public List<User> Users => m_Users;


        #endregion

        #region Local values

        #region Must do Initialize
        private List<User> m_Users;
        #endregion



        #endregion

        #region Constructor

        public StdServer() {
            
        }

        #endregion

        #region Methods

        /// <summary>
        /// 服務器初始化
        /// </summary>
        protected override void Initialize() {
            m_Users = new List<User>();
        }

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
            
            //啟動監聽
            NetworkManagement.StartToListen(ConnectionChannel.MainConnection, MainConn, 10);

            //方法2
            //IConnection iMainConn = MainConn;
            //iMainConn.Listen(10);
            //NetworkManagement.Add(ConnectionChannel.MainConnection, MainConn);
        }

        /// <summary>
        /// 停止運行
        /// </summary>
        public override void Close() {
            throw new NotImplementedException();
        }


        /// <summary>
        /// 資料架構
        /// </summary>
        protected override void DataStruct() {
            throw new NotImplementedException();
        }     

        

        /// <summary>
        /// 註冊器
        /// </summary>
        protected override void Registered() {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 啟動線程
        /// </summary>
        protected override void Start_Thread() {
            isServerClose = false;   // partial -> Thread

            //啟動線程
            Thread.CreateThread(AwaitClientThread, true).Start();
            //Thread.CreateThread(Execute_SpecialCircumstancesCallbackThread, true);

            //HACK: 技術保留
            System.Threading.ThreadPool.SetMinThreads(3, 3);
        }

        /// <summary>
        /// 關閉線程
        /// </summary>
        protected override void Close_Thread() {
            isServerClose = true;   // partial -> Thread
        }

        /// <summary>
        /// 執行_回調線程
        /// </summary>
        protected override void Execute_CallbackThread() {
            throw new NotImplementedException();
        }

        

        #endregion
    }
}
