using System;

namespace Waylong.Packets.Header {


    public abstract class PacketHeaderBase {

        /// <summary>
        /// 封包Header參數Size
        /// </summary>
        public static class SizeOf {

            public const int HeaderType = BasicTypes.SizeOf.Short;
            public const int VerificationCode = BasicTypes.SizeOf.Int;
            public const int EmergencyType = BasicTypes.SizeOf.Short;
            public const int EncryptionType = BasicTypes.SizeOf.Short;
            public const int CategoryType = BasicTypes.SizeOf.Short;
            public const int CallbackType = BasicTypes.SizeOf.Short;

        }

    }

}
