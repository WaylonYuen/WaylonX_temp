//using System;
//using System.Net;
//using Waylong.Converter;
//using Waylong.Users;

//namespace Waylong.Packets {

//    public static class PacketRef {

//        /// <summary>
//        /// packet data size
//        /// </summary>
//        public static class SizeOf {
//            public const int DataLength = BasicTypes.SizeOf.Int;
//        }

//        /// <summary>
//        /// Packet index
//        /// </summary>
//        public static class IndexOf {
//            public const int DataLength = 0;
//            public const int Data = DataLength + SizeOf.DataLength;
//        }
//    }

//    public class Packet : Packaged, IBasePacket {

//        public byte[] PacketData { get => mBys_oriData; }

//        private byte[] mBys_oriData;

//        #region Constructor

//        public Packet(User user) : base(user, Encryption.None, Emergency.None, Category.None, Callback.None, PacketType.Packet) {
//            mBys_oriData = null;
//        }

//        public Packet(User user, Encryption encryption, Emergency emergency, Category category, Callback callback, byte[] bys_oriData)
//            : base(user, encryption, emergency, category, callback, PacketType.Packet) {
//            mBys_oriData = bys_oriData;
//        }

//        #endregion


//        #region Methods

//        /// <summary>
//        /// 封裝bytes: Interface -> IBasicOfNetPacket
//        /// </summary>
//        /// <returns>封包字節組</returns>
//        public byte[] ToPackup() {

//            //訂製封包內容架構大小(Body[dataLength + Data])
//            var bys_packetData = new byte[PacketRef.SizeOf.DataLength + mBys_oriData.Length];

//            //打包資料
//            BitConverter.GetBytes(IPAddress.HostToNetworkOrder(mBys_oriData.Length)).CopyTo(bys_packetData, PacketRef.IndexOf.DataLength);
//            mBys_oriData.CopyTo(bys_packetData, PacketRef.IndexOf.Data);

//            return Std_ToPackup(bys_packetData);
//        }

//        /// <summary>
//        /// 解析bytes: Interface -> IBasicOfNetPacket
//        /// </summary>
//        /// <param name="user">Used for new STRUCT.Header(IUsers user, ...).</param>
//        /// <param name="bys_netPacket">Complete bytes packet.</param>
//        public void Unpack(byte[] bys_packet) {

//            if (bys_packet.Length < (PacketHeaderRef.SizeOf.Header + PacketRef.SizeOf.DataLength)) {
//                //Error: Header length is not incompatible.
//                return;
//            }

//            //解析Header
//            IBasePacketHeader stdPacketHeader = this;
//            stdPacketHeader.Unpack(bys_packet);

//            //解析資料長度
//            var dataLength = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(Bytes.Extract(bys_packet, PacketHeaderRef.IndexOf.Data, PacketRef.SizeOf.DataLength), 0));

//            //提取封包資料
//            mBys_oriData = Bytes.Extract(bys_packet, PacketHeaderRef.IndexOf.Data + PacketRef.IndexOf.Data, dataLength);
//        }

//        public override string ToString() {
//            return
//                base.ToString()
//                 + "PacketData\t" + BitConverter.ToInt32(mBys_oriData, 0) + "\n";
//        }

//        #endregion



//    }

//}
