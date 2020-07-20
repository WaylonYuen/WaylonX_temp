using System;
using System.Threading;

namespace Waylong.Threading {

    public class Thread {

        /// <summary>
        /// 創建無參數線程
        /// </summary>
        /// <param name="method"></param>
        /// <param name="isBackground"></param>
        /// <returns></returns>
        public static System.Threading.Thread CreateThread(ThreadStart method, bool isBackground) {
            return new System.Threading.Thread(method) { IsBackground = isBackground };
        }

        /// <summary>
        /// 創建帶參數線程
        /// </summary>
        /// <param name="medthod"></param>
        /// <returns></returns>
        public static System.Threading.Thread CreateThread(ParameterizedThreadStart method, bool isBackground) {
            return new System.Threading.Thread(method) { IsBackground = isBackground };
        }
    }
}
