using System;

namespace Waylong.Packets.PacketData {

    public abstract class PacketDataBase : PacketBase {

        public static class SizeOf {

            public const int packetType = BasicTypes.SizeOf.Short;

            /// <summary>
            /// 資料內容的長度
            /// </summary>
            public const int DataLength = BasicTypes.SizeOf.Int;

        }

    }
}
