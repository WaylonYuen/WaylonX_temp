using System;
using System.Collections.Generic;
using System.Net.Sockets;
using Waylong.Net;
using Waylong.Net.Protocol;
using Waylong.Users;

namespace Waylong.Architecture {

    public interface IServer {
        List<User> Users { get; }
    }

    /// <summary>
    /// 標準服務器架構
    /// </summary>
    public class StdServer : CSModel, IServer {

        #region Property

        public override string Name { get; set; }

        public override Environment Environment { get { return Environment.Terminal; } }


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
            NetworkManagement.StartToListen(MainConn, 10);

        }


        /// <summary>
        /// 資料架構
        /// </summary>
        protected override void DataStruct() {
            throw new NotImplementedException();
        }     

        /// <summary>
        /// 服務器初始化
        /// </summary>
        protected override void Initialize() {
            m_Users = new List<User>();
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
            throw new NotImplementedException();
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
