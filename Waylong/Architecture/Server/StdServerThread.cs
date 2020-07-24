using System;
using System.Net.Sockets;
using System.Threading;
using Waylong.Net;
using Waylong.Packets;
using Waylong.Users;

namespace Waylong.Architecture.Server {

    public partial class StdServer {

        /// <summary>
        /// 等待客戶端_線程
        /// </summary>
        protected override void AwaitClientThread() {

            Console.WriteLine("");
            
            //取得socket
            Socket socket = NetworkManagement.ConnectionDict[ConnectionChannel.MainConnection].Socket;

            //持續等待 -> 直到canClose Flag is true
            while (!IsClose) {

                try {

                    //參數：為新的客户端连接创建一个Socket对象，接收並返回一個新的Socket
                    //同步等待, 程序会阻塞在这里
                    IUser user = new User(socket.Accept(), NetworkState.Connecting);   //UNDONE: 等待客戶端連線請求而造成的線程阻塞 -> 未編寫超時等待的方法進行阻塞排除.

                    //子線程
                    try {
                        //為user建立封包監聽子線程 & 啟動子線程
                        Threading.Thread.CreateThread(new ParameterizedThreadStart(ReceivePacketThread), true);

                        //為user建立在線監測子線程 & 啟動子線程
                        Threading.Thread.CreateThread(new ParameterizedThreadStart(AliveThread), true);

                    } catch (Exception ex) {
                        Console.WriteLine(ex.Message);
                    }

                    //Undone: 同步驗證碼, 未設定Callback
                    user.Send(new StdPacket(Emergency.None, Encryption.None, Category.General, Callback.None, user.VerificationCode));

                    //添加用戶
                    UserManagement.UserList.Add(user);


                } catch (Exception ex) {
                    Console.WriteLine(ex.Message);
                }

            }

            //stop
            Console.WriteLine("");

        }

        #region SubThread

        /// <summary>
        /// 監聽封包_線程
        /// </summary>
        /// <param name="obj"></param>
        protected override void ReceivePacketThread(object obj) {

            //拆箱: 將Obj還原成 target
            var user = obj as User;

            //指定user網絡接口進行接口約束
            IUserNetwork userNet = user;

            // if 服務器已關閉 or 用戶網絡狀態不等於Connected狀態, 則停止此線程
            while (!IsClose || userNet.NetworkState != NetworkState.Connected) {
                //執行等待封包
            }

        }

        protected override void AliveThread(object socket) {
            //
        }

        #endregion
    }

}
