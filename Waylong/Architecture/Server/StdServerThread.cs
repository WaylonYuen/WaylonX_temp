using System;
using System.Net.Sockets;
using System.Threading;
using Waylong.Net;
using Waylong.Packets;
using Waylong.Users;

namespace Waylong.Architecture.Server {

    public partial class StdServer {

        protected bool isServerClose;    //Flag -> 線程可否繼續執行

        /// <summary>
        /// 等待客戶端_線程
        /// </summary>
        protected override void AwaitClientThread() {

            //取得socket
            Socket socket = NetworkManagement.ConnectionDict[ConnectionChannel.MainConnection].Socket;

            //持續等待 -> 直到canClose Flag is true
            while (!isServerClose) {

                try {

                    //參數：為新的客户端连接创建一个Socket对象，接收並返回一個新的Socket
                    //同步等待, 程序会阻塞在这里
                    IUser user = new User(socket.Accept());   //UNDONE: 等待客戶端連線請求而造成的線程阻塞 -> 未編寫超時等待的方法進行阻塞排除.

                    //子線程
                    try {
                        //為user建立封包監聽子線程 & 啟動子線程
                        Threading.Thread.CreateThread(new ParameterizedThreadStart(ReceivePacketThread), true);

                        //為user建立在線監測子線程 & 啟動子線程
                        Threading.Thread.CreateThread(new ParameterizedThreadStart(AliveThread), true);

                    } catch (Exception ex) {
                        Console.WriteLine(ex.Message);
                    }

                    //同步驗證碼
                    user.Send(new StdPacket(Emergency.None, Encryption.None, Category.General, Callback.None, user.VerificationCode));

                    //添加用戶


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
        protected override void ReceivePacketThread(object obj) {

            var user = obj as User;

            while (!isServerClose) {
                //執行等待封包
            }

        }

        protected override void AliveThread(object socket) {
            //
        }

        #endregion
    }

}
