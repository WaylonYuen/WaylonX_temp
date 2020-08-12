using System;
using WaylonX.Packets;
using WaylonX.Users;

namespace AutoChessDll.Packets {

    /// <summary>
    /// 委派: 回調處理器
    /// </summary>
    /// <param name="packet">回調處理封包</param>
    public delegate void CallbackHandler(CallbackHandlerPacket packet);

    /// <summary>
    /// 回調處理封包
    /// </summary>
    public class CallbackHandlerPacket {

        #region Property

        /// <summary>
        /// 回調處理器
        /// </summary>
        public CallbackHandler CallbackHandler { get; set; }

        /// <summary>
        /// 用戶
        /// </summary>
        public User User { get; }

        /// <summary>
        /// 加密方法
        /// </summary>
        public Encryption Encryption { get; }

        /// <summary>
        /// 內容
        /// </summary>
        public byte[] Bys_Data { get; }

        #endregion

        public CallbackHandlerPacket(User user, Encryption encryption, byte[] bys_data) {
            User = user;
            Encryption = encryption;
            Bys_Data = bys_data;
        }

        /// <summary>
        /// 回調執行
        /// </summary>
        public void Excute() {
            CallbackHandler?.Invoke(this);
        }

    }
}
