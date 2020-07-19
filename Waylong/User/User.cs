using System;
using System.Net;
using System.Net.Sockets;
using Waylong.Packets;

namespace Waylong.Users {

    public class User : IUser {

        #region Prop

        /// <summary>
        /// 取得用戶Socket
        /// </summary>
        public Socket Socket { get => m_socket; }

        /// <summary>
        /// 取得用戶網路狀態
        /// </summary>
        public NetStates NetStates { get => m_netStates; }

        /// <summary>
        /// 取得用戶身份驗證碼
        /// </summary>
        public int VerificationCode { get => m_verificationCode; }
        #endregion

        #region Local Values
        private Socket m_socket;
        private NetStates m_netStates;
        private int m_verificationCode;
        #endregion

        #region Constructor

        //Warning: 發佈前必須移除
        public User() { }
        public User(int verificationCode) { m_verificationCode = verificationCode; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="socket"></param>
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
        public void Send(IPacketMethods packet) {

            //封裝封包
            byte[] bys_packet = packet.ToPackup();

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
