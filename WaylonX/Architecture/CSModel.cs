using System;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Threading;
using WaylonX.Attributes;
using WaylonX.Net;
using WaylonX.Packets;

namespace WaylonX.Architecture {

    /// <summary>
    /// Client-Server-Model: 主從式架構
    /// </summary>
    public abstract class CSModel {

        #region Object

        /// <summary>
        /// 網路管理類
        /// </summary>
        protected NetworkManagement NetworkManagement = new NetworkManagement();

        #endregion

        #region Property

        /// <summary>
        /// 運行狀態判斷
        /// </summary>
        protected static  bool IsClose { get; set; }    //Flag

        #endregion

        #region Methods

        /// <summary>
        /// 啓動器
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="prot"></param>
        public abstract void Start(string ip, int port);

        /// <summary>
        /// 關閉程序
        /// </summary>
        public abstract void Close();

        /// <summary>
        /// 初始化: 用於初始化 DataStruct 和 Registered
        /// </summary>
        protected abstract void Initialize();

        /// <summary>
        /// 資料結構: 用於保存各種類型的資料及資料處理的方式
        /// </summary>
        protected abstract void DataStruct();

        /// <summary>
        /// 回調方法註冊: 註冊後的方法才能夠被外派調用並呼叫執行
        /// </summary>
        protected abstract void Registered();

        /// <summary>
        /// 佇列分配器 : 分配封包到對應的佇列隊伍中
        /// </summary>
        protected abstract void QueueDistributor(Packet packet);

        /// <summary>
        /// 接收資料
        /// </summary>
        /// <param name="socket"></param>
        /// <param name="dataLength"></param>
        /// <returns></returns>
        public static byte[] Receive(Socket socket, int dataLength) {

            if (dataLength <= 0) {
                return null;
            }

            var data_Bytes = new byte[dataLength];

            //如果當前需要接收的字節數大於0 and 遊戲未退出 則循環接收
            while (dataLength > 0) {
                var recvData_Bytes = new byte[dataLength < 1024 ? dataLength : 1024];

                //檢查緩存區是否有資料需要讀取: True為有資料, False為緩存區沒有資料
                //為避免線程阻塞在讀取部分而設置的緩存去內容判斷
                if (!(socket.Available == 0)) {

                    //防沾包：如果當前接收的字節組大於緩存區，則按緩存區大小接收，否則按剩餘需要接收的字節組接收。
                    int recvAlready =
                            (dataLength >= recvData_Bytes.Length)
                                ? socket.Receive(recvData_Bytes, recvData_Bytes.Length, 0)
                                : socket.Receive(recvData_Bytes, dataLength, 0);

                    //將接收到的字節數保存 
                    recvData_Bytes.CopyTo(data_Bytes, data_Bytes.Length - dataLength);

                    //減掉已經接收到的字節數
                    dataLength -= recvAlready;

                } else {
                    Thread.Sleep(50);   //本地緩存為空

                    if (IsClose) {
                        data_Bytes = null;
                        break;
                    }
                }
            }

            return data_Bytes;
        }

        #region Thread

        /// <summary>
        /// 啟動線程: 用於啟動各線程
        /// </summary>
        protected abstract void Start_Thread();

        /// <summary>
        /// 關閉線程
        /// </summary>
        protected abstract void Close_Thread();

        #endregion

        #endregion

    }

}
