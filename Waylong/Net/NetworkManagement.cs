using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace Waylong.Net {

    public class NetworkManagement {
        
        #region Property

        #endregion

        #region Local Values

        private List<Connection> networkList;    //網路連線資料表
        #endregion

        #region Constructor

        public NetworkManagement() {
            networkList = new List<Connection>();
        }

        #endregion

        #region Methods

        //建立連線機制
        public void CreateConnection(Socket socket, string ip, int port) {

            //創建Connection保存連線資料 & 存入List中
            networkList.Add(new Connection(socket, new IPEndPoint(IPAddress.Parse(ip), port))); //此狀態下僅俱備連線時必要資料, 並未啟動
        }


        public void StartToConnect(Socket socket) {

        }

        public void StartToListen(Socket socket, int backlog) {

        }



        

        public override string ToString() {
            return base.ToString();
        }

        #endregion
    }


}
