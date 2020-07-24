using System;
using System.Net;
using System.Net.Sockets;
using Waylong.Net;
using Waylong.Packets;

namespace Waylong.Users {

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

        //Warning: 發佈前必須移除
        public User() { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="socket"></param>
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
        public void Send(IPacketMethods packet) {

            //封裝封包
            byte[] bys_packet = packet.ToPackup();

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

        //public void SendSy

        ///// <summary>
        ///// 發送Blank封包
        ///// </summary>
        ///// <param name="category"></param>
        ///// <param name="callback"></param>
        //public void SendData(Category category, Callback callback) =>
        //    Send(new StdPacket(this, category, callback, null));

        ///// <summary>
        ///// 發送bool型態封包
        ///// </summary>
        ///// <param name="category"></param>
        ///// <param name="callback"></param>
        ///// <param name="data"></param>
        //public void SendData(Category category, Callback callback, bool data) =>
        //    Send(new StdPacket(this, category, callback, BitConverter.GetBytes(data)));

        ///// <summary>
        ///// 發送short型態封包
        ///// </summary>
        ///// <param name="category"></param>
        ///// <param name="callback"></param>
        ///// <param name="data"></param>
        //public void SendData(Category category, Callback callback, short data) =>
        //    Send(new StdPacket(this, category, callback, BitConverter.GetBytes(IPAddress.HostToNetworkOrder(data))));

        ///// <summary>
        ///// 發送int型態封包
        ///// </summary>
        ///// <param name="category"></param>
        ///// <param name="callback"></param>
        ///// <param name="data"></param>
        //public void SendData(Category category, Callback callback, int data) =>
        //    Send(new StdPacket(this, category, callback, BitConverter.GetBytes(IPAddress.HostToNetworkOrder(data))));

        ///// <summary>
        ///// 發送long型態封包
        ///// </summary>
        ///// <param name="category"></param>
        ///// <param name="callback"></param>
        ///// <param name="data"></param>
        //public void SendData(Category category, Callback callback, long data) =>
        //    Send(new StdPacket(this, category, callback, BitConverter.GetBytes(IPAddress.HostToNetworkOrder(data))));

        ///// <summary>
        ///// 發送float型態封包
        ///// </summary>
        ///// <param name="category"></param>
        ///// <param name="callback"></param>
        ///// <param name="data"></param>
        //public void SendData(Category category, Callback callback, float data) =>
        //    Send(new StdPacket(this, category, callback, BitConverter.GetBytes(data)));

        //task: Send char

        //task: Send string

        #endregion

    }

}
