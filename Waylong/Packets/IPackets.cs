using System;
using Waylong.Users;

namespace Waylong.Packets {

    //網路數據包線程類別
    public interface INetPacketThreadingType {
        Emergency GetEmergencyType { get; }             //緊急程度
        Category GetCategoryType { get; }               //類別
        Callback GetCallbackType { get; }               //回調
    }

    //數據包安全
    public interface IPacketSecurity {
        Encryption GetEncryptionType { get; }           //加密方法
    }

    //數據包身份識別
    public interface IPacketIdentity {
        int GetVerificationCode { get; }                //驗證碼
    }

    //封包描述基礎必備方法
    public interface IBasicOfHeader {

        //內容長度
        int GetDataLength { get; }

        //封裝封包Header
        byte[] ToPackup();

        //解析封包Header
        void Unpack(byte[] bys_netPacket);
    }


    public interface INetPacket {

        /// <summary>
        /// 獲取用戶
        /// </summary>
        IUser GetUser { get; }

        /// <summary>
        /// 封裝封包
        /// </summary>
        /// <returns>Bytes</returns>
        byte[] ToPackup();
    }
}
