using System;
using System.Net;
using System.Net.Sockets;
using WaylonX.Net;
using WaylonX.Packets;

namespace WaylonX.Users {

    public class User : IUser {

        #region Prop

        /// <summary>
        /// 取得用戶Socket
        /// </summary>
        Socket IUserNetwork.Socket { get => m_Socket; }

        /// <summary>
        /// 取得用戶網路狀態
        /// </summary>
        NetworkState IUserNetwork.NetworkState { get => m_networkState; }

        /// <summary>
        /// 取得用戶身份驗證碼
        /// </summary>
        int IUser.VerificationCode { get => m_VerificationCode; }

        #endregion

        #region Local Values
        private readonly Socket m_Socket;
        private NetworkState m_networkState;
        private readonly int m_VerificationCode;
        #endregion

        #region Constructor

        /// <summary>
        /// 用戶
        /// </summary>
        /// <param name="socket">用戶Socket</param>
        /// <param name="networkState">用戶網路狀態</param>
        public User(Socket socket, NetworkState networkState) {
            m_Socket = socket;
            m_networkState = networkState;              //設定用戶網路狀態
            m_VerificationCode = this.GetHashCode();    //設定用戶驗證碼
        }
        #endregion

        #region Methods

        /// <summary>
        /// 設定用戶網路狀態
        /// </summary>
        /// <param name="networkState"></param>
        public void SetNetworkState(NetworkState networkState) {
            m_networkState = networkState;
        }

        /// <summary>
        /// 發送網路封包
        /// </summary>
        /// <param name="netPacket">網路封包</param>
        public void Send(IPacket packet) {

            //封包簽名: 將用戶驗證碼設定到封包中
            packet.VerificationCode = m_VerificationCode;

            //封裝封包
            byte[] bys_packet = packet.ToPackup();

            //檢查用戶是否處於連線狀態
            if (m_Socket.Connected) {
                try {
                    m_Socket.Send(bys_packet, bys_packet.Length, 0);  //發送封包
                } catch (Exception ex) {

                    //Format: cw
                    Console.WriteLine($"Error -> Packet sending failed : {ex.Message}");
                }
            } else {
                throw new Exception($"{m_Socket.RemoteEndPoint} : is offline！");
                //檢查用戶是否還存在
            }

        }

        #endregion

    }

}
