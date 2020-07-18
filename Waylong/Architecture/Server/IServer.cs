using System;
using System.Collections.Generic;
using Waylong.Users;

namespace Waylong.Architecture.Server {

    public interface IServer {
        List<User> Users { get; }

        //等待客戶端
        void AwaitClientThread();

        //監聽封包
        void ReceivePacketThread(object obj);

        //心跳包
        void AliveThread(object socket);
    }

}
