using System;

namespace Waylong.Architecture.Client {

    /// <summary>
    /// 標準服務器模型架構
    /// </summary>
    public abstract class StdClientModel : CSModel {

        /// <summary>
        /// 監聽封包_線程
        /// </summary>
        /// <param name="obj"></param>
        protected abstract void ReceivePacketThread();
    }
}
