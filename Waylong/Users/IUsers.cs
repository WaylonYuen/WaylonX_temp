using System;
using System.Net.Sockets;
using Waylong.Packets;

namespace Waylong.Users {

    //用戶群基礎介面
    public interface IUsers {

        Socket GetSocket { get; }

        //用戶驗證碼
        int VerificationCode { get; set; }

        bool Send(INetPacket netPacket);
    }

    //一般用戶介面
    public interface IUser : IUsers {

    }

    //玩家用戶介面
    public interface IPlayer : IUsers {

    }

    //管理員介面
    public interface IAdministrator : IUsers {

    }

}
