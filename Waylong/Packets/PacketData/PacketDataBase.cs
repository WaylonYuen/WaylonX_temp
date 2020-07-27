using System;

namespace Waylong.Packets.PacketData {

    public abstract class PacketBodyBase : IPacketBase {

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

        public static class SizeOf {

            public const int packetType = BasicTypes.SizeOf.Short;

            /// <summary>
            /// 資料內容的長度
            /// </summary>
            public const int DataLength = BasicTypes.SizeOf.Int;

        }

    }
}
