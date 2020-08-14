using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using WaylonX.Loggers;
using WaylonX.Net;
using WaylonX.Packets;
using WaylonX.Users;

namespace WaylonX.Architecture.Server {

    /// <summary>
    /// 標準服務器模型架構
    /// </summary>
    public abstract class StdServerModel : CSModel, IServerParameter {

        #region Property

        /// <summary>
        /// 名稱
        /// </summary>
        public abstract string Name { get; set; }

        /// <summary>
        /// 操作環境
        /// </summary>
        public abstract Environment Environment { get; }

        /// <summary>
        /// 客戶端連線積壓數
        /// </summary>
        protected abstract int Backlog { get; set; }

        /// <summary>
        /// 服務器日誌
        /// </summary>
        public StdLogger Logger = new StdLogger();

        /// <summary>
        /// 用戶管理
        /// </summary>
        protected UserManagement UserManagement = new UserManagement();

        #endregion

        #region Methods

        /// <summary>
        /// 監聽封包_線程
        /// </summary>
        /// <param name="obj"></param>
        protected abstract void ReceivePacketThread(object obj);

        /// <summary>
        /// 客戶端連線狀態_線程
        /// </summary>
        /// <param name="socket"></param>
        protected abstract void AliveThread(object socket);

        #endregion

        #region 已實作 Methods

        /// <summary>
        /// 客戶端連線
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        /// <returns>連線成功與否</returns>
        protected bool Connect(string ip, int port) {

            //創建socket
            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            //創建連線Info
            var MainConn = new Connection(socket, ip, port);

            //啟動監聽
            IsClose = !NetworkManagement.StartToListen(ConnectionChannel.MainConnection, MainConn, Backlog);

            return !IsClose;
        }

        /// <summary>
        /// 啟動主連線
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="prot"></param>
        public override void Start(string ip, int port) {

            Logger.Info("服務器正在啟動...");

            if (Connect(ip, port)) {
                Logger.Info("服務器啟動成功");

                Initialize();   //初始化

            } else {
                Logger.Warn("服務器啟動失敗");
                //undone: 執行失敗程序
            }

        }

        /// <summary>
        /// 停止運行
        /// </summary>
        public override void Close() {

            Logger.Info("服務器正在關閉...");

            //關閉線程
            Close_Thread();

            //～異步執行 -> LoggerQueueue中的內容輸出, 直至確認所有需要關閉的線程進行回應（要做超時判斷)

            //～抓出自己的socket關閉
            //var socket = NetworkManagement.ConnectionDict[ConnectionChannel.MainConnection].Socket;
            //socket.Shutdown(SocketShutdown.Both);
            //socket.Close();

            //～清除UserList


        }

        /// <summary>
        /// 服務器初始化: 無序
        /// </summary>
        protected override void Initialize() {

            Logger.Info("正在初始化......");

            //Optimization: 使用異步進行初始化

            //初始化服務器參數
            Backlog = 5;

            //啟動項目
            Registered();   //註冊
            Start_Thread(); //啟動線程

            //通過異步初始化完成後 輸出 Logger, 並返回控制權於主線程
        }

        /// <summary>
        /// 啟動線程
        /// </summary>
        protected override void Start_Thread() {
            IsClose = false;

            //線程池設定
            //HACK: 設定線程池中的線程
            ThreadPool.SetMinThreads(3, 3);
            ThreadPool.SetMaxThreads(5, 5);

            //啟動監聽等待客戶端線程
            Threading.Thread.Create(AwaitClientThread, true).Start(); //啟動等待客戶端線程
        }

        /// <summary>
        /// 關閉線程
        /// </summary>
        protected override void Close_Thread() {
            IsClose = true;   //關閉線程的Flag: 所有線程將會被此Flag中斷無限循環

            //調取Server連線Info
            if (NetworkManagement.ConnectionDict.ContainsKey(ConnectionChannel.MainConnection)) {
                var IPEndPoint = NetworkManagement.ConnectionDict[ConnectionChannel.MainConnection].IPEndPoint; //取得EndPoint

                //Optimization: Client可發送指令給Server進行各種決策(此處為退出指令)
                //建立客戶端 -> 讓Server Accept()中斷從而跳出Await線程
                var client = new TcpClient();
                client.Connect(IPEndPoint.Address.ToString(), IPEndPoint.Port);
                client.Close();
            }

            //關掉用戶
        }

        /// <summary>
        /// 等待客戶端_線程
        /// </summary>
        protected virtual void AwaitClientThread() {

            //取得socket
            Socket socket = NetworkManagement.ConnectionDict[ConnectionChannel.MainConnection].Socket;

            //持續等待 -> 直到canClose Flag is true
            while (!IsClose) {

                try {

                    //監聽連線請求並建立用戶項, 程序會阻塞於Accept();
                    var userWorkItem = new UserWorkItem(new User(socket.Accept(), NetworkState.Connecting));

                    //Optimization: 當關閉線程後再度連線則會退出Accept, 需優化成接收指令確認是不是退出
                    if (IsClose) break;

                    //子線程
                    try {
                        //為user建立封包監聽子線程 & 啟動子線程
                        userWorkItem.AddAndStartThread(
                            Threading.Thread.Create(new ParameterizedThreadStart(ReceivePacketThread), true),
                            "ReceivePacketThread",
                            true);

                        //為user建立在線監測子線程 & 啟動子線程
                        userWorkItem.AddAndStartThread(
                            Threading.Thread.Create(new ParameterizedThreadStart(AliveThread), true),
                            "AliveThread",
                            true);

                    } catch (Exception ex) {
                        Console.WriteLine(ex.Message);
                    }

                    #region User Init

                    //發送封包同步驗證碼
                    IUser user = userWorkItem.User; //接口約束
                    userWorkItem.User.Send(new Packet(Emergency.None, Encryption.None, Category.General, Callback.PacketHeaderSync, user.VerificationCode));

                    //添加用戶到用戶清單
                    UserManagement.UserList.Add(userWorkItem);
                    //show 用戶上線資料

                    #endregion

                } catch (Exception e) {
                    Console.WriteLine(e.Message);
                }

            }

        }

        #endregion

    }
}
