using System;
using System.Net;
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
 
            //取得socket
            Socket socket = NetworkManagement.ConnectionDict[ConnectionChannel.MainConnection].Socket;

            //持續等待 -> 直到canClose Flag is true
            while (!IsClose) {

                try {

                    //參數：為新的客户端连接创建一个Socket对象，接收並返回一個新的Socket
                    //同步等待, 程序会阻塞在这里

                    var user = new User(socket.Accept(), NetworkState.Connecting);   //UNDONE: 等待客戶端連線請求而造成的線程阻塞 -> 未編寫超時等待的方法進行阻塞排除.

                    //
                    if (IsClose) break;

                    //子線程
                    try {
                        //為user建立封包監聽子線程 & 啟動子線程
                        Threading.Thread.Create(new ParameterizedThreadStart(ReceivePacketThread), true).Start(user);

                        //為user建立在線監測子線程 & 啟動子線程
                        Threading.Thread.Create(new ParameterizedThreadStart(AliveThread), true).Start(user);

                    } catch (Exception ex) {
                        Console.WriteLine(ex.Message);
                    }

                    #region User Init

                    IUser IUser = user;

                    //發送封包同步驗證碼
                    user.Send(new Packet(Emergency.None, Encryption.None, Category.General, Callback.PacketHeaderSync, IUser.VerificationCode));

                    //添加用戶到用戶清單
                    UserManagement.UserList.Add(user);

                    #endregion

                } catch (Exception e) {
                    Console.WriteLine(e.Message);
                }

            }

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
            while (!IsClose) {

                //if (userNet.NetworkState != NetworkState.Connected) {
                //    break;
                //}

                //解析 & 佇列分類 -> 由於佇列分類需要定義, 因此Std暫不提供

                //var bys_packetLength = Receive(userNet.Socket, BasicTypes.SizeOf.Int);

                //if (bys_packetLength != null) {

                //    //取得封包 長度(Receive取得後會進行: 網絡字節組 轉換成 主機字節組 並解析為 Int)
                //    var packetLength = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(bys_packetLength, 0));

                //    //取得封包 &解析
                //    var packet = new Packet().Unpack(Receive(userNet.Socket, packetLength));

                //    //檢查封包(通過則進行封包分類, 否則丟棄封包)
                //    if (packet.Header.Checking(user)) {
                //        QueueDistributor(packet);   //佇列分配器(將不同類別的封包分配到不同的佇列中等待處理)
                //    }//丟棄
                //}

            }

        }

        /// <summary>
        /// 客戶端連線狀態_線程
        /// </summary>
        /// <param name="socket"></param>
        protected override void AliveThread(object socket) { }

        #endregion
    }

}
