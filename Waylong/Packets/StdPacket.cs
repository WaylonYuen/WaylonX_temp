using System;
using System.Text;
using Waylong.Converter;
using Waylong.Packets.Header;
using Waylong.Packets.PacketData;

namespace Waylong.Packets {

    /// <summary>
    /// 標準封包 : 資料內容僅提供基礎型態
    /// </summary>
    public class StdPacket : Packaged<StdPacketHeader, StdPacketData>, IPacketMethods {

        public int StructSIZE => throw new NotImplementedException();

        #region Constructor

        /// <summary>
        /// 標準封包 : 無Header 的封包 -> 空的封包
        /// </summary>
        /// <param name="user"></param>
        public StdPacket()
            : base(new StdPacketData(null)) {
        }

        /// <summary>
        /// 標準封包 : 無Header 的封包 -> byte[]
        /// </summary>
        /// <param name="user"></param>
        /// <param name="bys_data"></param>
        public StdPacket(byte[] bys_data)
            : base(new StdPacketData(bys_data)) {
        }

        /// <summary>
        /// 標準封包 : 無Header 的封包 -> short
        /// </summary>
        /// <param name="user"></param>
        /// <param name="bys_data"></param>
        public StdPacket(int data)
            : base(new StdPacketData(BitConverter.GetBytes(data))) {
        }

        /// <summary>
        /// 標準封包 : 無Header 的封包 -> long
        /// </summary>
        /// <param name="user"></param>
        /// <param name="bys_data"></param>
        public StdPacket(bool data)
            : base(new StdPacketData(BitConverter.GetBytes(data))) {
        }

        /// <summary>
        /// 標準封包 : 無Header 的封包 -> float
        /// </summary>
        /// <param name="user"></param>
        /// <param name="bys_data"></param>
        public StdPacket(float data)
            : base(new StdPacketData(BitConverter.GetBytes(data))) {
        }

        /// <summary>
        /// 標準封包 : 無Header 的封包 -> string
        /// </summary>
        /// <param name="user"></param>
        /// <param name="bys_data"></param>
        public StdPacket(string data)
            : base(new StdPacketData(Encoding.UTF8.GetBytes(data))) {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="emergency"></param>
        /// <param name="encryption"></param>
        /// <param name="category"></param>
        /// <param name="callback"></param>
        /// <param name="verificationCode"></param>
        public StdPacket(Emergency emergency, Encryption encryption, Category category, Callback callback, int verificationCode)
            : base(new StdPacketHeader(0, emergency, encryption, category, callback), new StdPacketData(BitConverter.GetBytes(verificationCode))) {

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
        public StdPacket(int verificationCode, Emergency emergency, Encryption encryption, Category category, Callback callback, byte[] bys_data)
            : base(new StdPacketHeader(verificationCode, emergency, encryption, category, callback), new StdPacketData(bys_data)) {
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
        public StdPacket(int verificationCode, Emergency emergency, Encryption encryption, Category category, Callback callback, short data)
            : base(new StdPacketHeader(verificationCode, emergency, encryption, category, callback), new StdPacketData(BitConverter.GetBytes(data))) {
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
        public StdPacket(int verificationCode, Emergency emergency, Encryption encryption, Category category, Callback callback, int data)
            : base(new StdPacketHeader(verificationCode, emergency, encryption, category, callback), new StdPacketData(BitConverter.GetBytes(data))) {
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
        public StdPacket(int verificationCode, Emergency emergency, Encryption encryption, Category category, Callback callback, long data)
            : base(new StdPacketHeader(verificationCode, emergency, encryption, category, callback), new StdPacketData(BitConverter.GetBytes(data))) {
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
        public StdPacket(int verificationCode, Emergency emergency, Encryption encryption, Category category, Callback callback, bool data)
            : base(new StdPacketHeader(verificationCode, emergency, encryption, category, callback), new StdPacketData(BitConverter.GetBytes(data))) {
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
        public StdPacket(int verificationCode, Emergency emergency, Encryption encryption, Category category, Callback callback, float data)
            : base(new StdPacketHeader(verificationCode, emergency, encryption, category, callback), new StdPacketData(BitConverter.GetBytes(data))) {
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
        public StdPacket(int verificationCode, Emergency emergency, Encryption encryption, Category category, Callback callback, string data)
            : base(new StdPacketHeader(verificationCode, emergency, encryption, category, callback), new StdPacketData(Encoding.UTF8.GetBytes(data))) {
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
        public bool SetHeader(int verificationCode, Emergency emergency, Encryption encryption, Category category, Callback callback) {

            if(m_header != null) {
                return false;
            }

            m_header = new StdPacketHeader(verificationCode, emergency, encryption, category, callback);

            return true;
        }


        /// <summary>
        /// 封裝
        /// </summary>
        /// <returns></returns>
        public override byte[] ToPackup() {

            //封裝
            IPacketMethods header = m_header;

            var bys_header = header.ToPackup();
            var bys_data = m_data.ToPackup();

            //返回組合封裝
            return Bytes.ToPackup(ref bys_header, ref bys_data);
        }

        /// <summary>
        /// 解析: 直接取值
        /// </summary>
        /// <param name="bys_packet"></param>
        public override void Unpack(byte[] bys_packet) {

            //分割資料: Splitter返回提取內容, out剩餘內容
            IPacketMethods header = m_header;
            header.Unpack(Bytes.Splitter(out byte[] bys_data, ref bys_packet, 0, header.StructSIZE));
            m_data.Unpack(bys_data);
        }

    }
}