using System;
using System.Net.Sockets;

namespace Waylong.Net {

    public enum NetworkMode {
        None,       //未知
        Connect,    //接入
        Listen,     //監聽
    }

    public interface IConnection {

        #region Property

        //連線目標IP
        string IP { get; }

        //連線目標端口
        int Port { get; }

        //連線目標地址
        string IPAddress { get; }

        //連線模式
        NetworkMode NetworkMode { get; }

        //連線所使用的協議
        ProtocolType ProtocolType { get; }

        #endregion

        #region Methods

        /// <summary>
        /// 建立連線
        /// </summary>
        /// <returns>連線是否成功</returns>
        bool Connect();

        #endregion
    }

}
