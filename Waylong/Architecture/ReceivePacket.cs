using System;
using System.Net.Sockets;
using System.Threading;
using Waylong.Users;

namespace Waylong.Architecture {

    /// <summary>
    /// 接收封包
    /// </summary>
    public class ReceivePacket {

        protected readonly User user;

        public ReceivePacket(User user) {
            this.user = user;
        }

        //執行
        public void Excute() {

        }

        /// <summary>
        /// 接收資料
        /// </summary>
        /// <param name="socket"></param>
        /// <param name="dataLength"></param>
        /// <returns></returns>
        private static byte[] Receive(Socket socket, int dataLength) {

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

                    //UNDONE: ServerClose Flag
                    //if (MyETC.IsExited) {
                    //    data_Bytes = null;
                    //    break;
                    //}
                }
            }

            return data_Bytes;
        }

    }
}
