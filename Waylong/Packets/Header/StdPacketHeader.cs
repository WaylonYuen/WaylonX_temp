using System;
using System.Net;
using Waylong.Converter;
using Waylong.Users;

namespace Waylong.Packets.Header {

    /// <summary>
    /// 標準封包頭資訊接口
    /// </summary>
    public interface IStdPacketHeader : IPacketHeaderIdentity, IPacketHeaderSecurity, IPacketHeaderThreads {
        //確保屬性必然存在
    }

    /// <summary>
    /// 標準封包Header
    /// </summary>
    public class StdPacketHeader : PacketHeaderBase, IStdPacketHeader, IPacketMethods {

        #region Property

        /// <summary>
        /// Header型態
        /// </summary>
        public PacketHeaderType HeaderType { get => m_headerType; }

        /// <summary>
        /// 驗證碼
        /// </summary>
        public int VerificationCode { get; set; }

        /// <summary>
        /// 緊急程度
        /// </summary>
        Emergency IPacketHeaderThreads.EmergencyType { get => m_emergency; set => m_emergency = value; }

        /// <summary>
        /// 加密方法
        /// </summary>
        Encryption IPacketHeaderSecurity.EncryptionType { get => m_encryption; }

        /// <summary>
        /// 封包類別
        /// </summary>
        Category IPacketHeaderThreads.CategoryType { get => m_category; }

        /// <summary>
        /// 封包回調
        /// </summary>
        Callback IPacketHeaderThreads.CallbackType { get => m_callback; }

        /// <summary>
        /// 此結構大小
        /// </summary>
        public int StructSIZE => SIZE;

        #endregion

        #region Constructor

        /// <summary>
        /// 快捷構造器
        /// </summary>
        /// <param name="verificationCode"></param>
        /// <param name="category"></param>
        /// <param name="callback"></param>
        public StdPacketHeader(Category category, Callback callback) {
            m_emergency = Emergency.None;
            m_encryption = Encryption.None;
            m_category = category;
            m_callback = callback;
        }

        /// <summary>
        /// 標準構造器
        /// </summary>
        /// <param name="verificationCode"></param>
        /// <param name="emergency"></param>
        /// <param name="encryption"></param>
        /// <param name="category"></param>
        /// <param name="callback"></param>
        public StdPacketHeader(Emergency emergency, Encryption encryption, Category category, Callback callback) {
            m_emergency = emergency;
            m_encryption = encryption;        
            m_category = category;
            m_callback = callback;
        }

        #endregion

        #region Local values

        //Constants

        /// <summary>
        /// Header長度
        /// </summary>
        public const int SIZE = IndexOf.Data;

        private const PacketHeaderType m_headerType = PacketHeaderType.StdPacketHeader;

        //Variables
        private Emergency m_emergency;
        private Encryption m_encryption;
        private Category m_category;
        private Callback m_callback;
        #endregion

        #region Inner class

        //架構索引
        public static class IndexOf {

            public const int HeaderType = 0;
            public const int VerificationCode = HeaderType + SizeOf.HeaderType;
            public const int EmergencyType = VerificationCode + SizeOf.VerificationCode;
            public const int EncryptionType = EmergencyType + SizeOf.EmergencyType;
            public const int CategoryType = EncryptionType + SizeOf.EncryptionType;
            public const int CallbackType = CategoryType + SizeOf.CategoryType;

            //封包內容的索引位置
            public const int Data = CallbackType + SizeOf.CallbackType;
        }

        #endregion

        #region Methods

        /// <summary>
        /// 封裝
        /// </summary>
        /// <returns></returns>
        public byte[] ToPackup() {

            //指定封包尺寸
            var bys_packetHeader = new byte[SIZE];

            //封裝Header : 將local values 封裝成 Bytes
            BitConverter.GetBytes(IPAddress.HostToNetworkOrder((short)m_headerType)).CopyTo(bys_packetHeader, IndexOf.HeaderType);
            BitConverter.GetBytes(IPAddress.HostToNetworkOrder(VerificationCode)).CopyTo(bys_packetHeader, IndexOf.VerificationCode);
            BitConverter.GetBytes(IPAddress.HostToNetworkOrder((short)m_emergency)).CopyTo(bys_packetHeader, IndexOf.EmergencyType);
            BitConverter.GetBytes(IPAddress.HostToNetworkOrder((short)m_encryption)).CopyTo(bys_packetHeader, IndexOf.EncryptionType);
            BitConverter.GetBytes(IPAddress.HostToNetworkOrder((short)m_category)).CopyTo(bys_packetHeader, IndexOf.CategoryType);
            BitConverter.GetBytes(IPAddress.HostToNetworkOrder((short)m_callback)).CopyTo(bys_packetHeader, IndexOf.CallbackType);

            return bys_packetHeader;
        }

        /// <summary>
        /// 解析
        /// </summary>
        /// <param name="bys_packetHeader">不包含其他資料的bys</param>
        public void Unpack(byte[] bys_packetHeader) {
            //封包最小長度不能夠小於Header.length -> 否則不是完整的封包
            if (bys_packetHeader.Length < SIZE) {
                return;
            }

            //Hack: 如果解析時short數值不在enum範圍內,則有可能無法獲得指定type.

            //Unpack
            //m_headerType = (PacketHeaderType)IPAddress.NetworkToHostOrder(BitConverter.ToInt16(Bytes.Extract(bys_packetHeader, IndexOf.HeaderType, SizeOf.HeaderType), 0));
            VerificationCode = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(Bytes.Extract(bys_packetHeader, IndexOf.VerificationCode, SizeOf.VerificationCode), 0));
            m_emergency = (Emergency)IPAddress.NetworkToHostOrder(BitConverter.ToInt16(Bytes.Extract(bys_packetHeader, IndexOf.EmergencyType, SizeOf.EmergencyType), 0));
            m_encryption = (Encryption)IPAddress.NetworkToHostOrder(BitConverter.ToInt16(Bytes.Extract(bys_packetHeader, IndexOf.EncryptionType, SizeOf.EncryptionType), 0));
            m_category = (Category)IPAddress.NetworkToHostOrder(BitConverter.ToInt16(Bytes.Extract(bys_packetHeader, IndexOf.CategoryType, SizeOf.CategoryType), 0));
            m_callback = (Callback)IPAddress.NetworkToHostOrder(BitConverter.ToInt16(Bytes.Extract(bys_packetHeader, IndexOf.CallbackType, SizeOf.CallbackType), 0));

        }

        /// <summary>
        /// Information
        /// </summary>
        /// <returns></returns>
        public override string ToString() {
            return
                 "\n" + base.ToString() + ":\n"
                 + "----------------------------------------------\n"
                 + "HeaderType\t" + m_headerType + "\n"
                 + "Verification\t" + VerificationCode + "\n"
                 + "Emergency\t" + m_emergency + "\n"
                 + "Encryption\t" + m_encryption + "\n"                    
                 + "category\t" + m_category + "\n"
                 + "callback\t" + m_callback + "\n"
                 + "End-------------------------------------------\n\n";
        }

        /// <summary>
        /// 測試用
        /// </summary>
        public static void Testing() {

            IUser user = new User();
            var stdHeader = new StdPacketHeader(Emergency.Level2, Encryption.RES256, Category.General, Callback.PacketHeaderSync);
            Console.WriteLine(stdHeader.ToString());

            IPacketMethods header = stdHeader;
            var bys_header = header.ToPackup();

            var newStdHeader = new StdPacketHeader(Emergency.None, Encryption.None, Category.None, Callback.None);
            header = newStdHeader;
            header.Unpack(bys_header);

            Console.WriteLine(newStdHeader);
        }

        #endregion
    }

}
