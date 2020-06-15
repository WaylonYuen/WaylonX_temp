using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace Waylong.Net {

    public class NetworkManagement {
        
        #region Property

        //UNDONE: 不可以直接被外部調用,需要修改.
        /// <summary>
        /// 網路連線資料表
        /// </summary>
        //public List<IConnection> NetworkList { get { return m_networkList; } }

        #endregion

        #region Local Values

        private List<IConnection> m_networkList;    //網路連線資料表

        #endregion

        #region Constructor

        public NetworkManagement() {
            m_networkList = new List<IConnection>();
        }

        #endregion

        #region Methods

        /// <summary>
        /// 建立連線
        /// </summary>
        /// <param name="type">連線協議</param>
        /// <returns>建立是否成功</returns>
        public bool Connect(IConnection protocol) {

            //如果成功連線的話,將此協議加入List中
            if (protocol.Connect()) {
                m_networkList.Add(protocol);
                return true;
            }

            return false;
        }

        //展示NetworkList
        public void ShowList() {
            foreach (var connected in m_networkList) {
                //Show
            }
        }

        //GetIP(ref 誰的IP）

        //GetPortr(ref 誰的Port)

        //GetIPAddress(ref 誰的)

        //GetNetworkMode

        public override string ToString() {
            return base.ToString();
        }

        #endregion
    }


}
