using System;
using System.Net.Sockets;
using Waylong.Net;

namespace Waylong.Architecture {

    //Client-Server-Model: 主從式架構
    public abstract class CSModel : ICSParameter {

        #region Prop
        public string Name { get; }
        public string IP { get; }
        public int Port { get; }
        public string IPAddress { get; }

        public OperatingEnvironment Environment { get; }
        #endregion
        
        #region Local Values
        protected NetMode m_netMode;
        #endregion

        #region Constructor

        public CSModel(string name, string IPAddress, OperatingEnvironment environment) {
            Name = name;
            this.IPAddress = IPAddress;
            Environment = environment;
        }

        public CSModel(string name, string ip, int port, OperatingEnvironment environment) {
            Name = name;
            IP = ip;
            Port = port;
            Environment = environment;
        }
        #endregion

        
        public virtual void Start(Socket socket) {

            switch (socket.ProtocolType) {

                case ProtocolType.Tcp:
                    //TcpConnection(m_netMode)
                    break;

                case ProtocolType.Udp:
                    //UdoConnection(m_netMode)
                    break;

                default:
                    ///Unknow
                    break;
            }

        }

        protected abstract void Initialize();

        //資料結構: 用於保存各種類型的資料及資料處理的方式
        protected abstract void DataStruct();

        //回調方法註冊: 註冊後的方法才能夠被外派調用並呼叫執行
        protected abstract void Registered();

        #region Thread
        //啟動線程
        protected abstract void StartThread();

        //監聽封包
        //protected abstract void ReceivePacketThread();

        //特殊情況線程 -> 執行封包
        protected abstract void Execute_GeneralCallbackThread();
        #endregion
    }

}
