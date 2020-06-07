using System;
using System.Net;
using System.Net.Sockets;

namespace Waylong.Net.Protocol {

    /// <summary>
    /// Tcp連線
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
        private int m_backlog;

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
        /// 設定監聽上限人數
        /// </summary>
        public void SetBacklog(int backlog) {
            m_backlog = backlog;
        }

        /// <summary>
        /// 建立連線
        /// </summary>
        /// <returns>回傳是否建立成功</returns>
        bool IConnection.Connect() {

            //創建Socket
            m_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            switch (m_NetworkMode) {

                #region Connect
                case NetworkMode.Connect:

                    try {
                        m_socket.Connect(new IPEndPoint(System.Net.IPAddress.Parse(IP), Port));   //協議綁定                       
                        return true;
                    } catch (Exception e) {
                        throw new Exception("\n! 連接失敗:" + e.Message);
                    }
                #endregion

                #region Listen
                case NetworkMode.Listen:

                    try {
                        m_socket.Bind(new IPEndPoint(System.Net.IPAddress.Parse(IP), Port));      //協議綁定
                        m_socket.Listen(m_backlog);
                        return true;
                    } catch (Exception e) {
                        throw new Exception("\n! 綁定&監聽失敗:" + e.Message);
                    }
                #endregion

                default:
                    //
                    break;
            }

            return false;
        }

        #endregion
    }
}
