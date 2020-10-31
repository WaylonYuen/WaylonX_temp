﻿using System;
using System.Net;
using WaylonX.Converter;
using WaylonX.Packets.Header.Base;
using WaylonX.Users;

namespace WaylonX.Packets.Header {

    /// <summary>
    /// 標準封包描述資訊接口
    /// </summary>
    public interface IStdPacketHeader : IPacketHeaderIdentity, IPacketHeaderSecurity, IPacketHeaderThreads {

        /// <summary>
        /// 用戶
        /// </summary>
        IUser User { get; }
    }

    /// <summary>
    /// 標準封包描述 -> full Spelling: standard Packet Header;
    /// PacketHeaderBase: 封包描述基類(抽象類) -> 所有PacketHeader必須派生自此抽象類別(該類別包含了PacketHeader必須包含的實作屬性及方法);
    /// IStdPacketHeader: 封包描述接口 -> 用戶自定義PacketHeader所必須包含的屬性及方法;
    /// </summary>
    public class StdPacketHeader : PacketHeaderBase, IStdPacketHeader {

        #region Property

        /// <summary>
        /// StdPacketHeader架構長度: 資料的索引起始位置即為該架構長度
        /// </summary>
        public override int StructSIZE { get => IndexOf.Data; }

        /// <summary>
        /// PacketHeader型態
        /// </summary>
        public override PacketHeaderType PacketHeaderType { get => PacketHeaderType.StdPacketHeader; }

        /// <summary>
        /// 用戶網路資料: 只有但checking通過時, 才會對此賦值
        /// </summary>
        public IUser User { get; protected set; }

        #region Must be pack ( Important: Do Not Easily Change! )
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
        #endregion

        #endregion

        #region Constructor

        [Obsolete("This constructor will be optimization in the future", false)]
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
        private Emergency m_emergency;
        private Encryption m_encryption;
        private Category m_category;
        private Callback m_callback;
        #endregion

        #region Inner class

        /// <summary>
        /// 此類別架構索引
        /// </summary>
        public static class IndexOf {

            public const int VerificationCode = 0;
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
        public override byte[] ToPackup() {

            //指定封包尺寸
            var bys_packetHeader = new byte[StructSIZE];

            //封裝Header : 將local values 封裝成 Bytes
            System.BitConverter.GetBytes(IPAddress.HostToNetworkOrder(VerificationCode)).CopyTo(bys_packetHeader, IndexOf.VerificationCode);
            System.BitConverter.GetBytes(IPAddress.HostToNetworkOrder((short)m_emergency)).CopyTo(bys_packetHeader, IndexOf.EmergencyType);
            System.BitConverter.GetBytes(IPAddress.HostToNetworkOrder((short)m_encryption)).CopyTo(bys_packetHeader, IndexOf.EncryptionType);
            System.BitConverter.GetBytes(IPAddress.HostToNetworkOrder((short)m_category)).CopyTo(bys_packetHeader, IndexOf.CategoryType);
            System.BitConverter.GetBytes(IPAddress.HostToNetworkOrder((short)m_callback)).CopyTo(bys_packetHeader, IndexOf.CallbackType);

            return bys_packetHeader;
        }

        /// <summary>
        /// 解析
        /// </summary>
        /// <param name="bys_packetHeader">不包含其他資料的bys</param>
        public override void Unpack(byte[] bys_packetHeader) {
            //封包最小長度不能夠小於Header.length -> 否則不是完整的封包
            if (bys_packetHeader.Length < StructSIZE) {
                return;
            }

            //Hack: 如果解析時short數值不在enum範圍內,則有可能無法獲得指定type.

            //Unpack
            VerificationCode = IPAddress.NetworkToHostOrder(System.BitConverter.ToInt32(Bytes.Extract(bys_packetHeader, IndexOf.VerificationCode, SizeOf.VerificationCode), 0));
            m_emergency = (Emergency)IPAddress.NetworkToHostOrder(System.BitConverter.ToInt16(Bytes.Extract(bys_packetHeader, IndexOf.EmergencyType, SizeOf.EmergencyType), 0));
            m_encryption = (Encryption)IPAddress.NetworkToHostOrder(System.BitConverter.ToInt16(Bytes.Extract(bys_packetHeader, IndexOf.EncryptionType, SizeOf.EncryptionType), 0));
            m_category = (Category)IPAddress.NetworkToHostOrder(System.BitConverter.ToInt16(Bytes.Extract(bys_packetHeader, IndexOf.CategoryType, SizeOf.CategoryType), 0));
            m_callback = (Callback)IPAddress.NetworkToHostOrder(System.BitConverter.ToInt16(Bytes.Extract(bys_packetHeader, IndexOf.CallbackType, SizeOf.CallbackType), 0));

        }

        /// <summary>
        /// 檢查
        /// </summary>
        /// <param name="bys_packet"></param>
        /// <returns></returns>
        public override bool Checking(IUser user) {

            //封包條件檢查
            switch (m_callback) {

                //放行以下封包
                case Callback.Testing:
                case Callback.PacketHeaderSync:
                    User = user; //設定封包對象
                    return true;
            }

            //檢查封包驗證碼
            if (user.VerificationCode.Equals(VerificationCode)) {
                User = user;
                return true;
            }

            //不通過
            return false;
        }

        /// <summary>
        /// Information
        /// </summary>
        /// <returns> string </returns>
        public override string ToString() {
            return
                 "\n" + base.ToString() + ":\n"
                 + "----------------------------------------------\n"
                 + "Verification\t" + VerificationCode + "\n"
                 + "Emergency\t" + m_emergency + "\n"
                 + "Encryption\t" + m_encryption + "\n"
                 + "category\t" + m_category + "\n"
                 + "callback\t" + m_callback + "\n"
                 + "End-------------------------------------------\n\n";
        }

        [Obsolete("Undone", true)]
        /// <summary>
        /// 測試用
        /// </summary>
        public static void Testing() {

            //IUser user = new User();
            //var stdHeader = new StdPacketHeader(Emergency.Level2, Encryption.RES256, Category.General, Callback.PacketHeaderSync);
            //Console.WriteLine(stdHeader.ToString());

            //IPacketMethods header = stdHeader;
            //var bys_header = header.ToPackup();

            //var newStdHeader = new StdPacketHeader(Emergency.None, Encryption.None, Category.None, Callback.None);
            //header = newStdHeader;
            //header.Unpack(bys_header);

            //Console.WriteLine(newStdHeader);
        }

        #endregion
    }

}
