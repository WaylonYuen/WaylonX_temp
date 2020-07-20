using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace Waylong.Net {

    public class NetworkManagement {

        #region Property
        public Dictionary<ConnectionChannel, ILinkInfo> ConnectionList { get; }    //網路連線資料表
        #endregion

        #region Local Values

        #endregion

        #region Constructor

        public NetworkManagement() {
            ConnectionList = new Dictionary<ConnectionChannel, ILinkInfo>();
        }

        #endregion

        #region Methods

        /// <summary>
        /// 添加連線
        /// </summary>
        /// <returns>連線添加是否成功</returns>
        public bool Add(ConnectionChannel channel, Connection connection) {

            //檢查該連線是否存在
            if (!ConnectionList.ContainsKey(channel)) {
                ConnectionList.Add(channel, connection);
                return true;
            }

            return false;
        }

        /// <summary>
        /// 啟動連線模式
        /// </summary>
        /// <param name="connection">連線Info</param>
        /// <param name="connectionChannel">連線頻道</param>
        /// <returns>連線是否成功</returns>
        public bool StartToConnect(ConnectionChannel channel, Connection connection) {

            //檢查該連線是否存在
            if (!ConnectionList.ContainsKey(channel)) {

                //接口過濾
                IConnection IConnection = connection;

                //啟動連線並判斷連線是否成功
                if (IConnection.Connect()) {    //由於Connect()方法被限定在IConnection接口中,因此必須接口過濾
                    ConnectionList.Add(channel, connection);    //保存該連線資料
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 啟動監聽模式 : 若監聽量參數為0表示無
        /// </summary>
        /// <param name="connection">監聽Info</param>
        /// <param name="backlog">監聽量</param>
        /// <returns>監聽是否成功</returns>
        public bool StartToListen(ConnectionChannel channel, Connection connection, int backlog) {

            //檢查該連線是否存在
            if (!ConnectionList.ContainsKey(channel)) {

                //接口過濾
                IConnection IConnection = connection;

                //啟動監聽並判斷監聽是否成功
                if (IConnection.Listen(backlog)) {  //由於Connect()方法被限定在IConnection接口中,因此必須接口過濾
                    ConnectionList.Add(channel, connection);    //保存該監聽資料
                    return true;
                }
            }

            return false;
        }

        #endregion
    }


}
