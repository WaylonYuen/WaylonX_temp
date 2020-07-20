using System;
namespace Waylong.Architecture.Server {

    /// <summary>
    /// 標準服務器模型架構
    /// </summary>
    public abstract class StdServerModel : CSModel {

        //protected 

        /// <summary>
        /// 等待客戶端_線程
        /// </summary>
        protected abstract void AwaitClientThread();

        /// <summary>
        /// 監聽封包_線程
        /// </summary>
        /// <param name="obj"></param>
        protected abstract void ReceivePacketThread(object obj);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="socket"></param>
        protected abstract void AliveThread(object socket);

    }
}
