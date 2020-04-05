using System;
using System.Net.Sockets;

namespace Waylong.Net {

    public interface IConnection {

        //連線目標IP
        string GetIP { get; }

        //連線目標端口
        int GetPort { get; }

        //連線目標地址
        //string IPAddress { get => IP + ":" + Port.ToString(); }

        //連線所使用的協議
        ProtocolType GetProtocolType { get; }

        //連線方法
        bool Connection();
    }

}
