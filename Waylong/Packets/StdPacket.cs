using System;
using System.Text;
using Waylong.Converter;
using Waylong.Packets.Header;
using Waylong.Packets.PacketData;
using Waylong.Users;

namespace Waylong.Packets {

    /// <summary>
    /// 標準封包 : 資料內容僅提供基礎型態
    /// </summary>
    public class StdPacket : Packaged<StdPacketHeader, StdPacketData> {

        #region Constructor

        /// <summary>
        /// 標準封包 : 空的封包
        /// </summary>
        /// <param name="user"></param>
        public StdPacket(User user)
            : base(user, new StdPacketHeader(user.VerificationCode, Emergency.None, Encryption.None, Category.None, Callback.None), new StdPacketData(new byte[BasicTypes.SizeOf.Int])) {
        }

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
        /// 設定 Packet Header
        /// </summary>
        /// <param name="emergency">緊急程度</param>
        /// <param name="encryption">加密方式</param>
        /// <param name="category">類別</param>
        /// <param name="callback">封包回調</param>
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

            //範圍檢查 : bys_packet.Length 不能小於此封包架構的基礎長度(否則回傳null -> 不合格封包)
            if (bys_packet.Length < StdPacketHeader.SIZE + StdPacketData.SIZE) {
                return null;
            }

            //創建空的封包
            var packetObj = new StdPacket(user);

            //約束封包方法
            Packaged<StdPacketHeader, StdPacketData> packeted = packetObj;

            //使用約束方法
            packeted.Unpack(bys_packet);    //使用Packaged的Upack方法進行解析

            return packetObj;
        }
    }
}

