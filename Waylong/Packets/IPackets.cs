using System;
using Waylong.Users;

namespace Waylong.Packets {

    //網絡封包Head相關的介面
    public interface INetHeader {

    }

    //網絡封包Body相關的介面
    public interface INetPacket {

        //用戶資料
        IUsers GetUsers { get; }

        //封包頭資料
        NetHeader GetHeaderInfo { get; }

        //Bytes封包內容資料
        byte[] GetBytes { get; }    //取得Bytes的封包資料

        //封裝封包
        byte[] ToPackup();
    }

}
