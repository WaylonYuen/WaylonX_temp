using System;
using System.Threading;

namespace Waylong.Architecture.Server {

    public partial class StdServer : IServer {

        protected bool canClose;    //Flag -> 線程可否繼續執行

        /// <summary>
        /// 等待客戶端_線程
        /// </summary>
        public void AwaitClientThread() {

            //取得socket
            

            //持續等待 -> 直到canClose Flag is true
            while (!canClose) {

                try {

                    //參數：為新的客户端连接创建一个Socket对象，接收並返回一個新的Socket
                    //同步等待, 程序会阻塞在这里
                    //Accept    //UNDONE: 等待客戶端連線請求而造成的線程阻塞 -> 未編寫超時等待的方法進行阻塞排除.

                    //子線程
                    try {
                        //為user建立封包監聽子線程 & 啟動子線程
                        Threading.Thread.CreateThread(new ParameterizedThreadStart(ReceivePacketThread), true);

                        //為user建立在線監測子線程 & 啟動子線程
                        Threading.Thread.CreateThread(new ParameterizedThreadStart(AliveThread), true);

                    } catch (Exception ex) {
                        Console.WriteLine(ex.Message);
                    }


                } catch (Exception ex) {
                    Console.WriteLine(ex.Message);
                }

            }

            //stop

        }

        #region SubThread

        /// <summary>
        /// 監聽封包_線程
        /// </summary>
        /// <param name="obj"></param>
        public void ReceivePacketThread(object obj) {
            throw new NotImplementedException();
        }

        public void AliveThread(object socket) {
            throw new NotImplementedException();
        }

        #endregion
    }

}
