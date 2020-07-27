using System;

namespace Waylong.Packets.Header {

    public abstract class PacketHeaderBase : IPacketBase {

        /// <summary>
        /// StdPacketHeader架構長度: 資料的索引起始位置即為該架構長度
        /// </summary>
        public abstract int StructSIZE { get; }

        /// <summary>
        /// 封裝
        /// </summary>
        /// <returns></returns>
        public abstract byte[] ToPackup();

        /// <summary>
        /// 解析
        /// </summary>
        /// <param name="bys_packetHeader">不包含其他資料的bys</param>
        public abstract void Unpack(byte[] bys_packet);

        /// <summary>
        /// 封包Header參數Size
        /// </summary>
        public static class SizeOf {

            /// <summary>
            /// 驗證碼
            /// </summary>
            public const int VerificationCode = BasicTypes.SizeOf.Int;

            /// <summary>
            /// 緊急程度
            /// </summary>
            public const int EmergencyType = BasicTypes.SizeOf.Short;

            /// <summary>
            /// 加密方法
            /// </summary>
            public const int EncryptionType = BasicTypes.SizeOf.Short;

            /// <summary>
            /// 封包類別
            /// </summary>
            public const int CategoryType = BasicTypes.SizeOf.Short;

            /// <summary>
            /// 回調
            /// </summary>
            public const int CallbackType = BasicTypes.SizeOf.Short;

        }

    }

}
