//using System;
//using System.Net;
//using System.Runtime.Remoting.Messaging;
//using Waylong.Converter;
//using Waylong.Users;

//namespace Waylong.Packets {

//    /// <summary>
//    /// 標準封包頭資訊接口
//    /// </summary>
//    public interface IPacketHeader : IBasePacketHeader, IPacketHeaderIdentity, IPacketHeaderSecurity, IPacketHeaderThreads {
//        //確保屬性必然存在
//    }

//    /// <summary>
//    /// 標準封包頭參數
//    /// </summary>
//    public static class PacketHeaderRef {

//        /// <summary>
//        /// Header data size
//        /// </summary>
//        public static class SizeOf {

//            public const int VerificationCode = BasicTypes.SizeOf.Int;
//            public const int EmergencyType = BasicTypes.SizeOf.Short;
//            public const int EncryptionType = BasicTypes.SizeOf.Short;
//            public const int CategoryType = BasicTypes.SizeOf.Short;
//            public const int CallbackType = BasicTypes.SizeOf.Short;
//            public const int PacketType = BasicTypes.SizeOf.Short;
//            public const int Header = IndexOf.Data;  //需修改

//        }

//        /// <summary>
//        /// Header index
//        /// </summary>
//        public static class IndexOf {

//            public const int VerificationCode = 0;
//            public const int EmergencyType = VerificationCode + SizeOf.VerificationCode;
//            public const int EncryptionType = EmergencyType + SizeOf.EmergencyType;
//            public const int CategoryType = EncryptionType + SizeOf.EncryptionType;
//            public const int CallbackType = CategoryType + SizeOf.CategoryType;
//            public const int PacketType = CallbackType + SizeOf.CallbackType;
//            public const int Data = PacketType + SizeOf.PacketType; //封包內容的索引位置
//        }
//    }

//    //標準網絡封包內容描述
//    public class PacketHeader : IPacketHeader {

//        #region Prop

//        /// <summary>
//        /// 驗證碼
//        /// </summary>
//        int IPacketHeaderIdentity.VerificationCode { get => m_verificationCode; }

//        /// <summary>
//        /// 加密方法
//        /// </summary>
//        Encryption IPacketHeaderSecurity.EncryptionType { get => m_encryption; }

//        /// <summary>
//        /// 緊急程度
//        /// </summary>
//        Emergency IPacketHeaderThreads.EmergencyType { get => m_emergency; set => m_emergency = value; }

//        /// <summary>
//        /// 封包類別
//        /// </summary>
//        Category IPacketHeaderThreads.CategoryType { get => m_category; }

//        /// <summary>
//        /// 封包回調
//        /// </summary>
//        Callback IPacketHeaderThreads.CallbackType { get => m_callback; }

//        /// <summary>
//        /// 封包型態
//        /// </summary>
//        PacketType IPacketHeaderIdentity.Type { get => m_packetType; }

//        #endregion

//        #region Local References

//        private int m_verificationCode;

//        private Encryption m_encryption;
//        private Emergency m_emergency;
        
//        private Category m_category;
//        private Callback m_callback;

//        private PacketType m_packetType;
//        private int verificationCode;
//        private Encryption rES256;
//        private Emergency level3;
//        private Category general;
//        private Callback packetHeaderSync;

//        #endregion

//        #region Constructor

//        public PacketHeader(User user, Encryption encryption, Emergency emergency, Category category, Callback callback, PacketType packetType) {

//            //Refereces
//            m_verificationCode = user.VerificationCode;

//            m_encryption = encryption;
//            m_emergency = emergency;
  
//            m_category = category;
//            m_callback = callback;

//            m_packetType = packetType;
//        }

//        public PacketHeader(int verificationCode, Encryption rES256, Emergency level3, Category general, Callback packetHeaderSync) {
//            this.verificationCode = verificationCode;
//            this.rES256 = rES256;
//            this.level3 = level3;
//            this.general = general;
//            this.packetHeaderSync = packetHeaderSync;
//        }
//        #endregion

//        #region Methods

//        //封裝
//        byte[] IBasePacketHeader.ToPackup() {

//            //指定封包尺寸
//            var bys_packetHeader = new byte[PacketHeaderRef.SizeOf.Header];

//            //封裝Header : 將local values 封裝成 Bytes
//            BitConverter.GetBytes(IPAddress.HostToNetworkOrder(m_verificationCode)).CopyTo(bys_packetHeader, PacketHeaderRef.IndexOf.VerificationCode);
//            BitConverter.GetBytes(IPAddress.HostToNetworkOrder((short)m_emergency)).CopyTo(bys_packetHeader, PacketHeaderRef.IndexOf.EmergencyType);
//            BitConverter.GetBytes(IPAddress.HostToNetworkOrder((short)m_encryption)).CopyTo(bys_packetHeader, PacketHeaderRef.IndexOf.EncryptionType);
//            BitConverter.GetBytes(IPAddress.HostToNetworkOrder((short)m_category)).CopyTo(bys_packetHeader, PacketHeaderRef.IndexOf.CategoryType);
//            BitConverter.GetBytes(IPAddress.HostToNetworkOrder((short)m_callback)).CopyTo(bys_packetHeader, PacketHeaderRef.IndexOf.CallbackType);

//            return bys_packetHeader;
//        }

//        //解析
//        void IBasePacketHeader.Unpack(byte[] bys_packetHeader) {
            
//            //封包最小長度不能夠小於Header.length -> 否則不是完整的封包
//            if (bys_packetHeader.Length < PacketHeaderRef.SizeOf.Header) {
//                return;
//            }

//            //Hack: 如果解析時short數值不在enum範圍內,則有可能無法獲得指定type.
            
//            //Unpack
//            m_verificationCode = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(Bytes.Extract(bys_packetHeader, PacketHeaderRef.IndexOf.VerificationCode, PacketHeaderRef.SizeOf.VerificationCode), 0));
//            m_emergency = (Emergency)IPAddress.NetworkToHostOrder(BitConverter.ToInt16(Bytes.Extract(bys_packetHeader, PacketHeaderRef.IndexOf.EmergencyType, PacketHeaderRef.SizeOf.EmergencyType), 0));
//            m_encryption = (Encryption)IPAddress.NetworkToHostOrder(BitConverter.ToInt16(Bytes.Extract(bys_packetHeader, PacketHeaderRef.IndexOf.EncryptionType, PacketHeaderRef.SizeOf.EncryptionType), 0));
//            m_category = (Category)IPAddress.NetworkToHostOrder(BitConverter.ToInt16(Bytes.Extract(bys_packetHeader, PacketHeaderRef.IndexOf.CategoryType, PacketHeaderRef.SizeOf.CategoryType), 0));
//            m_callback = (Callback)IPAddress.NetworkToHostOrder(BitConverter.ToInt16(Bytes.Extract(bys_packetHeader, PacketHeaderRef.IndexOf.CallbackType, PacketHeaderRef.SizeOf.CallbackType), 0));

//        }

//        public override string ToString() {
//            return
//                base.ToString() + ":\n"
//                 + "Verification\t" + m_verificationCode + "\n"
//                 + "Encryption\t" + m_encryption + "\n"
//                 + "Emergency\t" + m_emergency + "\n"
//                 + "callback\t" + m_callback + "\n"
//                 + "category\t" + m_category + "\n"
//                 + "Type\t\t" + m_packetType + "\n"
//                 + "\n";
//        }

//        #endregion

//    }

//}
