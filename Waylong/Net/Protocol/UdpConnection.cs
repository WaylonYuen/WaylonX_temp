using System;
using System.Net;
using System.Net.Sockets;

namespace Waylong.Net.Protocol {

    public class UdpConnection : IConnection {

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
        public ProtocolType ProtocolType { get { return ProtocolType.Udp; } }

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
        public UdpConnection(NetworkMode networkMode, string ip, int port) {
            m_NetworkMode = networkMode;
            m_ip = ip;
            m_port = port;
        }

        #endregion

        #region Methods

        bool IConnection.Connect() {


            //建立 Socket
            m_socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            switch (m_NetworkMode) {

                #region Connect
                case NetworkMode.Connect:
                    //UNDONE: UDP -> NetMode.Connet unfinished.
                    break;

                #endregion

                #region Listen
                case NetworkMode.Listen:

                    try {
                        m_socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.ReuseAddress, true);
                        m_socket.Bind(new IPEndPoint(System.Net.IPAddress.Parse(IP), Port));      //協議綁定
                        return true;
                    } catch (Exception e) {
                        throw new Exception("\n! 綁定&監聽失敗:" + e.Message);    //暫時性
                    }

                #endregion

                default:
                    //Error
                    break;
            }

            return false;
        }

        #endregion
    }
}
