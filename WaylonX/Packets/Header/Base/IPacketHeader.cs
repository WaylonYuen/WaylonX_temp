using System;

namespace WaylonX.Packets.Header.Base {

    /// <summary>
    /// 數據包身份識別
    /// </summary>
    public interface IPacketHeaderIdentity {
        int VerificationCode { get; set; }            //驗證碼
    }

    /// <summary>
    /// 數據包安全
    /// </summary>
    public interface IPacketHeaderSecurity {
        Encryption EncryptionType { get; }           //加密方法
    }

    /// <summary>
    /// 網路數據包線程描述
    /// </summary>
    public interface IPacketHeaderThreads {
        Emergency EmergencyType { get; set; }        //緊急程度(可修改)
        Category CategoryType { get; }               //類別
        Callback CallbackType { get; }               //回調
    }

}
