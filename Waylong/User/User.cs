using System;
using System.Net.Sockets;
using Waylong.Packets;

namespace Waylong.Users {

    public class User : IUser {

        #region Prop

        /// <summary>
        /// 取得用戶Socket
        /// </summary>
        public Socket GetSocket { get => m_socket; }

        /// <summary>
        /// 取得用戶網路狀態
        /// </summary>
        public NetStates GetNetStates { get => m_netStates; }

        /// <summary>
        /// 取得用戶身份驗證碼
        /// </summary>
        public int GetVerificationCode { get => m_verificationCode; }
        #endregion

        #region Local Values
        private Socket m_socket;
        private NetStates m_netStates;
        private int m_verificationCode;
        #endregion

        #region Constructor

        //Warning: 發佈前必須移除
        public User() { }

        public User(Socket socket) {
            m_socket = socket;
            m_netStates = NetStates.None;
            m_verificationCode = this.GetHashCode();
        }
        #endregion

        #region Methods

        /// <summary>
        /// 發送網路封包
        /// </summary>
        /// <param name="netPacket">網路封包</param>
        public void Send(INetPacket netPacket) {

            //封裝封包
            byte[] bys_packet = netPacket.ToPackup();

            if (m_socket.Connected) {
                try {
                    m_socket.Send(bys_packet, bys_packet.Length, 0);  //發送封包
                } catch (Exception ex) {

                    //Format: cw
                    Console.WriteLine($"Error -> Packet sending failed : {ex.Message}");
                }
            } else {
                throw new Exception($"{m_socket.RemoteEndPoint} : is offline！");
                //檢查用戶是否還存在
            }

        }

        #endregion

    }

}
