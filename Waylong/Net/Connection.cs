using System;
using System.Net;
using System.Net.Sockets;

namespace Waylong.Net {

    public enum NetworkMode {
        Unknown,    //未知
        Connect,    //接入
        Listen,     //監聽
    }

    public class Connection {

        #region Property

        public Socket Socket { get; }

        public IPEndPoint IPEndPoint { get; }

        public NetworkMode NetworkMode { get; private set; }

        #endregion

        /// <summary>
        /// Instructor
        /// </summary>
        /// <param name="socket"></param>
        /// <param name="iPEndPoint"></param>
        public Connection(Socket socket, IPEndPoint iPEndPoint) {
            Socket = socket;
            IPEndPoint = iPEndPoint;

            NetworkMode = NetworkMode.Unknown;
        }

        /// <summary>
        /// 設定網絡模式
        /// </summary>
        /// <param name="networkMode"></param>
        public void SetNetworkMode(NetworkMode networkMode) {
            NetworkMode = networkMode;
        }


    }
}
