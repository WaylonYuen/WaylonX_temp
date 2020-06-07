using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace Waylong.Net {

    public class NetworkManagement {

        //List<連線型態(此連線的目的 -> enum Type), 連線接口(IConnection）>
        public List<IConnection> NetworkList;


        #region Constructor

        public NetworkManagement() {
            NetworkList = new List<IConnection>();
        }

        #endregion

        #region Methods


        //Undone:
        /// <summary>
        /// 建立連線
        /// </summary>
        /// <param name="type">連線協議</param>
        /// <returns>建立是否成功</returns>
        public bool Connect(IConnection protocol) {

            //如果成功連線的話,將此協議加入List中
            if (protocol.Connect()) {
                NetworkList.Add(protocol);
            }

            return false;
        }

        //Undone: 2
        public void ShowList() {

            foreach (var connected in NetworkList) {
                //Show
            }

        }

        public override string ToString() {
            return base.ToString();
        }

        #endregion
    }


}
