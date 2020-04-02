using System;
using System.Net;
using System.Runtime.Remoting.Messaging;
using Waylong.Converter;
using Waylong.Users;

namespace Waylong.Packets {

    //網絡封包Head相關的介面
    public interface IStdNetHeader : IBasicOfHeader, IPacketIdentity, IPacketSecurity, INetPacketThreadingType { }

    //標準網絡封包內容描述
    public struct StdNetHeader : IStdNetHeader {

        #region Constants
        public struct SizeOf {
            public const int VerificationCode = BasicTypes.SizeOf.Int;
            public const int EmergencyType = BasicTypes.SizeOf.Short;
            public const int EncryptionType = BasicTypes.SizeOf.Short;
            public const int CategoryType = BasicTypes.SizeOf.Short;
            public const int CallbackType = BasicTypes.SizeOf.Short;
            public const int DataLength = BasicTypes.SizeOf.Int;

            public const int Header = IndexOf.DataLength + DataLength;
        }

        public struct IndexOf {
            public const int VerificationCode = 0;
            public const int EmergencyType = VerificationCode + SizeOf.VerificationCode;
            public const int EncryptionType = EmergencyType + SizeOf.EmergencyType;
            public const int CategoryType = EncryptionType + SizeOf.EncryptionType;
            public const int CallbackType = CategoryType + SizeOf.CategoryType;
            public const int DataLength = CallbackType + SizeOf.CallbackType;

            public const int Data = DataLength + SizeOf.DataLength;
        }
        #endregion

        #region Prop

        /// <summary>
        /// 驗證碼
        /// </summary>
        int IPacketIdentity.GetVerificationCode { get => m_verificationCode; }

        /// <summary>
        /// 加密方法
        /// </summary>
        Encryption IPacketSecurity.GetEncryptionType { get => m_encryption; }

        /// <summary>
        /// 緊急程度
        /// </summary>
        Emergency INetPacketThreadingType.GetEmergencyType { get => m_emergency; }

        /// <summary>
        /// 封包類別
        /// </summary>
        Category INetPacketThreadingType.GetCategoryType { get => m_category; }

        /// <summary>
        /// 封包回調
        /// </summary>
        Callback INetPacketThreadingType.GetCallbackType { get => m_callback; }

        /// <summary>
        /// 資料長度
        /// </summary>
        public int GetDataLength { get => m_dataLength; }
        #endregion

        #region Local References

        private static int m_verificationCode;

        private static Encryption m_encryption;
        private static Emergency m_emergency;
        
        private static Category m_category;
        private static Callback m_callback;

        private static int m_dataLength;
        #endregion

        #region Local Values
        //...
        #endregion


        #region Constructor
        public StdNetHeader(IUser user, Encryption encryption, Emergency emergency, Category category, Callback callback, int dataLength) {

            //Refereces
            m_verificationCode = user.GetVerificationCode;

            m_encryption = encryption;
            m_emergency = emergency;
  
            m_category = category;
            m_callback = callback;

            m_dataLength = dataLength;

        }
        #endregion

        #region Methods

        public void SetEncryptionType(Encryption encryption) {
            m_encryption = encryption;
        }

        public void SetEmergencyType(Emergency emergency) {
            m_emergency = emergency;
        }

        //封裝
        //verificationCode
        //emergency
        //encryption
        //category
        //callback
        //datalength
        //??PacketContentType
        byte[] IBasicOfHeader.ToPackup() {

            //指定封包尺寸
            var bys_header = new byte[SizeOf.Header];

            //封裝Header : 將local values 封裝成 Bytes
            BitConverter.GetBytes(IPAddress.HostToNetworkOrder(m_verificationCode)).CopyTo(bys_header, IndexOf.VerificationCode);
            BitConverter.GetBytes(IPAddress.HostToNetworkOrder((short)m_emergency)).CopyTo(bys_header, IndexOf.EmergencyType);
            BitConverter.GetBytes(IPAddress.HostToNetworkOrder((short)m_encryption)).CopyTo(bys_header, IndexOf.EncryptionType);
            BitConverter.GetBytes(IPAddress.HostToNetworkOrder((short)m_category)).CopyTo(bys_header, IndexOf.CategoryType);
            BitConverter.GetBytes(IPAddress.HostToNetworkOrder((short)m_callback)).CopyTo(bys_header, IndexOf.CallbackType);
            BitConverter.GetBytes(IPAddress.HostToNetworkOrder(m_dataLength)).CopyTo(bys_header, IndexOf.DataLength);

            return bys_header;
        }

        //解析
        void IBasicOfHeader.Unpack(byte[] bys_netPacket) {
            
            //封包最小長度不能夠小於Header.length -> 否則不是完整的封包
            if (bys_netPacket.Length < SizeOf.Header) {
                return;
            }

            Unpacking(bys_netPacket);

        }

        //解析
        public static StdNetHeader Unpack(IUser user, byte[] bys_netPacket) {
            Unpacking(bys_netPacket);
            return new StdNetHeader(user, m_encryption, m_emergency, m_category, m_callback, m_dataLength);
        }

        //拆包
        private static void Unpacking(byte[] bys_netPacket) {
            //Hack: 如果解析時short數值不在enum範圍內,則有可能無法獲得指定type.
            //Unpack
            m_verificationCode = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(Bytes.Extract(bys_netPacket, IndexOf.VerificationCode, SizeOf.VerificationCode), 0));
            m_emergency = (Emergency)IPAddress.NetworkToHostOrder(BitConverter.ToInt16(Bytes.Extract(bys_netPacket, IndexOf.EmergencyType, SizeOf.EmergencyType), 0));
            m_encryption = (Encryption)IPAddress.NetworkToHostOrder(BitConverter.ToInt16(Bytes.Extract(bys_netPacket, IndexOf.EncryptionType, SizeOf.EncryptionType), 0));
            m_category = (Category)IPAddress.NetworkToHostOrder(BitConverter.ToInt16(Bytes.Extract(bys_netPacket, IndexOf.CategoryType, SizeOf.CategoryType), 0));
            m_callback = (Callback)IPAddress.NetworkToHostOrder(BitConverter.ToInt16(Bytes.Extract(bys_netPacket, IndexOf.CallbackType, SizeOf.CallbackType), 0));
            m_dataLength = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(Bytes.Extract(bys_netPacket, IndexOf.DataLength, SizeOf.DataLength), 0));
        }


        public override string ToString() {
            return base.ToString();
        }
        #endregion
    }

}
