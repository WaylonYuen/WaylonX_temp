using System;
using System.Net.Sockets;

namespace Waylong.Net {

    /// <summary>
    /// 
    /// </summary>
    public class TcpConnection : IConnection {

        #region Property

        /// <summary>
        /// IP位址
        /// </summary>
        public string IP { get { return m_ip; } }

        /// <summary>
        /// 端口
        /// </summary>
        public int Port { get { return m_port; } }

        /// <summary>
        /// IP地址
        /// </summary>
        public string IPAddress { get { return m_ip + m_port.ToString(); } }

        /// <summary>
        /// 網路連接模式
        /// </summary>
        public NetworkMode NetworkMode { get { return m_NetworkMode; } }

        /// <summary>
        /// 協議
        /// </summary>
        public ProtocolType ProtocolType { get { return ProtocolType.Tcp; } }

        #endregion

        #region Local Values

        private readonly string m_ip;
        private readonly int m_port;
        private readonly NetworkMode m_NetworkMode;
        private readonly ProtocolType m_protocolType;

        private Socket m_socket;

        #endregion

        #region Constructor

        //建立連線
        public TcpConnection(NetworkMode networkMode, string ip, int port) {
            m_NetworkMode = networkMode;
            m_ip = ip;
            m_port = port;
        }

        #endregion

        #region Methods

        /// <summary>
        /// 建立連線
        /// </summary>
        /// <returns>回傳是否建立成功</returns>
        public bool Connection() {

            //創建Socket
            m_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);


        }

        #endregion
    }
}
