using System;
using System.Net.Sockets;
using System.Security.Cryptography;
using Waylong.Net;

namespace Waylong.Architecture {

    //Client-Server-Model: 主從式架構
    public abstract class CSModel : CSModelBase, ICSParameter {

        #region Property

        /// <summary>
        /// 名稱
        /// </summary>
        public abstract string Name { get; set; }

        /// <summary>
        /// IP地址
        /// </summary>
        public abstract string IPAddress { get; }

        /// <summary>
        /// 操作環境
        /// </summary>
        public abstract Environment Environment { get; }

        #endregion
        
        #region Local Values

        protected NetMode m_netMode;

        #endregion

        #region Methods

        //Undone: Start(IP)
        public virtual void Start(string ip, int prot) {
            //Networking.TcpConnection(NetMode.Listen);
        }

        //Undone: Start(Socket)
        public virtual void Start(Socket socket) {

            switch (socket.ProtocolType) {

                case ProtocolType.Tcp:
                    //TcpConnection(m_netMode)
                    break;

                case ProtocolType.Udp:
                    //UdoConnection(m_netMode)
                    break;

                default:
                    ///Unknow
                    break;
            }

        }

        #endregion
    }

}
