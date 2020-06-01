using System;
using Waylong.Users;

namespace Waylong.Packets {

    // IPackets - 封包架構介面(接口)
    // Explain:
    //  ～使用者可以根據自己的需求來繼承Customer介面(接口)以實現指定樣式的特殊封包架構.
    //  ～

    public interface IPacketMethods {

        /// <summary>
        /// 資料描述欄位長度
        /// </summary>
        int PacketHeadDescriptionLength { get; }

        /// <summary>
        /// 封裝
        /// </summary>
        /// <returns>Bytes</returns>
        byte[] ToPackup();

        /// <summary>
        /// 封包
        /// </summary>
        /// <param name="user"></param>
        /// <param name="bys_netPacket"></param>
        /// <returns></returns>
        void Unpack(byte[] bys_packet);
    }

}
