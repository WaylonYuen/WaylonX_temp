using System;
using System.Text;
using Waylong.Packets.Header;
using Waylong.Packets.PacketData;
using Waylong.Users;

namespace Waylong.Packets {

    /// <summary>
    /// 標準封包 : 資料內容僅資源基礎型態
    /// </summary>
    public class StdPacket : Packaged<StdPacketHeader, StdPacketData> {

        #region Non-References

        /// <summary>
        /// 標準封包 : 無Header 的封包 -> byte[]
        /// </summary>
        /// <param name="user"></param>
        /// <param name="bys_data"></param>
        public StdPacket(User user, byte[] bys_data)
            : base(user, new StdPacketData(bys_data)) {
        }

        /// <summary>
        /// 標準封包 : 無Header 的封包 -> short
        /// </summary>
        /// <param name="user"></param>
        /// <param name="bys_data"></param>
        public StdPacket(User user, short data)
            : base(user, new StdPacketData(BitConverter.GetBytes(data))) {
        }

        /// <summary>
        /// 標準封包 : 無Header 的封包 -> int
        /// </summary>
        /// <param name="user"></param>
        /// <param name="bys_data"></param>
        public StdPacket(User user, int data)
            : base(user, new StdPacketData(BitConverter.GetBytes(data))) {
        }

        /// <summary>
        /// 標準封包 : 無Header 的封包 -> long
        /// </summary>
        /// <param name="user"></param>
        /// <param name="bys_data"></param>
        public StdPacket(User user, long data)
            : base(user, new StdPacketData(BitConverter.GetBytes(data))) {
        }

        /// <summary>
        /// 標準封包 : 無Header 的封包 -> bool
        /// </summary>
        /// <param name="user"></param>
        /// <param name="bys_data"></param>
        public StdPacket(User user, bool data)
            : base(user, new StdPacketData(BitConverter.GetBytes(data))) {
        }

        /// <summary>
        /// 標準封包 : 無Header 的封包 -> float
        /// </summary>
        /// <param name="user"></param>
        /// <param name="bys_data"></param>
        public StdPacket(User user, float data)
            : base(user, new StdPacketData(BitConverter.GetBytes(data))) {
        }

        /// <summary>
        /// 標準封包 : 無Header 的封包 -> string
        /// </summary>
        /// <param name="user"></param>
        /// <param name="bys_data"></param>
        public StdPacket(User user, string data)
            : base(user, new StdPacketData(Encoding.UTF8.GetBytes(data))) {
        }

        #endregion

        #region Std References

        /// <summary>
        /// 標準封包 : 封包資料 -> byte[]
        /// </summary>
        /// <param name="user"></param>
        /// <param name="emergency"></param>
        /// <param name="encryption"></param>
        /// <param name="category"></param>
        /// <param name="callback"></param>
        /// <param name="bys_data"></param>
        public StdPacket(User user, Emergency emergency, Encryption encryption, Category category, Callback callback, byte[] bys_data)
            : base(user, new StdPacketHeader(user.VerificationCode, emergency, encryption, category, callback), new StdPacketData(bys_data)) {
        }

        /// <summary>
        /// 標準封包 : 封包資料 -> short
        /// </summary>
        /// <param name="user"></param>
        /// <param name="emergency"></param>
        /// <param name="encryption"></param>
        /// <param name="category"></param>
        /// <param name="callback"></param>
        /// <param name="data"></param>
        public StdPacket(User user, Emergency emergency, Encryption encryption, Category category, Callback callback, short data)
            : base(user, new StdPacketHeader(user.VerificationCode, emergency, encryption, category, callback), new StdPacketData(BitConverter.GetBytes(data))) {
        }

        /// <summary>
        /// 標準封包 : 封包資料 -> int
        /// </summary>
        /// <param name="user"></param>
        /// <param name="emergency"></param>
        /// <param name="encryption"></param>
        /// <param name="category"></param>
        /// <param name="callback"></param>
        /// <param name="data"></param>
        public StdPacket(User user, Emergency emergency, Encryption encryption, Category category, Callback callback, int data)
            : base(user, new StdPacketHeader(user.VerificationCode, emergency, encryption, category, callback), new StdPacketData(BitConverter.GetBytes(data))) {
        }

        /// <summary>
        /// 標準封包 : 封包資料 -> long
        /// </summary>
        /// <param name="user"></param>
        /// <param name="emergency"></param>
        /// <param name="encryption"></param>
        /// <param name="category"></param>
        /// <param name="callback"></param>
        /// <param name="data"></param>
        public StdPacket(User user, Emergency emergency, Encryption encryption, Category category, Callback callback, long data)
            : base(user, new StdPacketHeader(user.VerificationCode, emergency, encryption, category, callback), new StdPacketData(BitConverter.GetBytes(data))) {
        }

        /// <summary>
        /// 標準封包 : 封包資料 -> bool
        /// </summary>
        /// <param name="user"></param>
        /// <param name="emergency"></param>
        /// <param name="encryption"></param>
        /// <param name="category"></param>
        /// <param name="callback"></param>
        /// <param name="data"></param>
        public StdPacket(User user, Emergency emergency, Encryption encryption, Category category, Callback callback, bool data)
            : base(user, new StdPacketHeader(user.VerificationCode, emergency, encryption, category, callback), new StdPacketData(BitConverter.GetBytes(data))) {
        }

        /// <summary>
        /// 標準封包 : 封包資料 -> float
        /// </summary>
        /// <param name="user"></param>
        /// <param name="emergency"></param>
        /// <param name="encryption"></param>
        /// <param name="category"></param>
        /// <param name="callback"></param>
        /// <param name="data"></param>
        public StdPacket(User user, Emergency emergency, Encryption encryption, Category category, Callback callback, float data)
            : base(user, new StdPacketHeader(user.VerificationCode, emergency, encryption, category, callback), new StdPacketData(BitConverter.GetBytes(data))) {
        }

        /// <summary>
        /// 標準封包 : 封包資料 -> string
        /// </summary>
        /// <param name="user"></param>
        /// <param name="emergency"></param>
        /// <param name="encryption"></param>
        /// <param name="category"></param>
        /// <param name="callback"></param>
        /// <param name="data"></param>
        public StdPacket(User user, Emergency emergency, Encryption encryption, Category category, Callback callback, string data)
            : base(user, new StdPacketHeader(user.VerificationCode, emergency, encryption, category, callback), new StdPacketData(Encoding.UTF8.GetBytes(data))) {
        }

        //標準封包 : 封包資料 -> string[]

        #endregion

        /// <summary>
        /// 設定Header
        /// </summary>
        /// <param name="emergency"></param>
        /// <param name="encryption"></param>
        /// <param name="category"></param>
        /// <param name="callback"></param>
        public bool SetHeader(Emergency emergency, Encryption encryption, Category category, Callback callback) {

            if(m_header != null) {
                return false;
            }

            m_header = new StdPacketHeader(m_user.VerificationCode, emergency, encryption, category, callback);

            return true;
        }

        /// <summary>
        /// 解析
        /// </summary>
        /// <param name="bys_packet"></param>
        /// <returns></returns>
        public static StdPacket Unpack(User user, byte[] bys_packet) {

            if (bys_packet.Length < StdPacketHeader.SIZE + StdPacketData.SIZE) {
                return null;
            }

            //創建空的封包
            var packetObj = new StdPacket(user, Emergency.None, Encryption.None, Category.None, Callback.None, 0);

            //約束封包方法
            Packaged<StdPacketHeader, StdPacketData> packeted = packetObj;

            //使用約束方法
            packeted.Unpack(bys_packet);    //解析

            return packetObj;
        }
    }
}

