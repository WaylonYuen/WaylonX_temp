using System;
using System.Net.Sockets;
using System.Threading;
using WaylonX.Cloud;
using WaylonX.Net;
using WaylonX.Packets;

namespace WaylonX.Architecture {

    /// <summary>
    /// Client-Server-Model: 主從式架構
    /// </summary>
    public abstract class CSBase_Catalina : CSDArchitecture_Catalina {

        #region Property

        /// <summary>
        /// 網路管理類
        /// </summary>
        protected NetworkManagement NetworkManagement { get; set; }

        #endregion

        //Constructor
        public CSBase_Catalina(string name) : base(name) {
            NetworkManagement = new NetworkManagement();
        }

        #region Methods

        /// <summary>
        /// 佇列分配器 : 分配封包到對應的佇列隊伍中
        /// </summary>
        protected abstract void QueueDistributor(Packet packet);

        /// <summary>
        /// 監聽封包_線程
        /// </summary>
        /// <param name="obj"></param>
        protected abstract void PacketReceiverThread(object obj);

        #endregion
    }

    /// <summary>
    /// 基本服務框架 -> Client,Server,Database框架 -> Catalina版
    /// </summary>
    public abstract class CSDArchitecture_Catalina {

        #region Property

        /// <summary>
        /// 名稱
        /// </summary>
        public static string Name { get; protected set; }

        /// <summary>
        /// 運行狀態判斷
        /// </summary>
        public static bool IsClose { get; protected set; }

        #endregion

        #region Local Values

        //事件執行器
        protected event EventHandler Init;
        public event EventHandler ShutDown;

        //事件參數
        protected EventArgs CSDargs { get; private set; }

        #endregion

        //Constructor
        public CSDArchitecture_Catalina(string name) {
            Name = name;
        }

        #region Trigger

        /// <summary>
        /// 啓動器
        /// </summary>
        public bool Start(EventArgs args) {

            //賦值
            IsClose = false;
            CSDargs = args;

            //註冊接收器
            StartingEventReceiver();
            StartedEventReceiver();

            //執行
            if (Init != null) {
                Shared.Logger.Info("正在啟動...");
                Init.Invoke(null, EventArgs.Empty);
            }

            return !IsClose;
        }

        /// <summary>
        /// 關閉程序
        /// </summary>
        public void Close() {

            //賦值
            IsClose = true;

            //註冊接收器
            ClosingEventReceiver();
            ClosedEventReceiver();

            //執行
            if (ShutDown != null) {
                ShutDown.Invoke(null, EventArgs.Empty);
            }
        }

        #endregion

        #region Receiver

        /// <summary>
        /// 啟動程序時的事件接收器註冊
        /// </summary>
        protected virtual void StartingEventReceiver() {
            Init += new EventHandler(OnAwake);
            Init += new EventHandler(OnConnecting);
            Init += new EventHandler(OnRegistered);
            Init += new EventHandler(OnStartThread);
        }

        /// <summary>
        /// 啟動程序後的事件接收器註冊
        /// </summary>
        protected virtual void StartedEventReceiver() {

        }

        /// <summary>
        /// 關閉程序時的事件接收器註冊
        /// </summary>
        protected virtual void ClosingEventReceiver() {
            ShutDown += new EventHandler(OnCloseThread);
        }

        /// <summary>
        /// 關閉程序後的事件接收器註冊
        /// </summary>
        protected virtual void ClosedEventReceiver() {

        }

        #endregion

        #region EventReceiver

        /// <summary>
        /// 在Start函數被調用前執行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected abstract void OnAwake(object sender, EventArgs e);

        /// <summary>
        /// 連線
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected abstract void OnConnecting(object sender, EventArgs e);

        /// <summary>
        /// 回調方法註冊: 註冊後的方法才能夠被外派調用並呼叫執行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected abstract void OnRegistered(object sender, EventArgs e);

        /// <summary>
        /// 啟動線程: 用於啟動各線程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected abstract void OnStartThread(object sender, EventArgs e);

        /// <summary>
        /// 關閉線程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected abstract void OnCloseThread(object sender, EventArgs e);

        #endregion

        #region Methods

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

        #endregion

    }

}
