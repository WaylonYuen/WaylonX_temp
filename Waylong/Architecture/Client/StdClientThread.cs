﻿using System;
namespace Waylong.Architecture.Client {

    public partial class StdClient {

        /// <summary>
        /// 監聽封包_線程
        /// </summary>
        /// <param name="obj"></param>
        protected override void ReceivePacketThread() {

            //GetUser

            while (!IsClose) {
                //執行等待封包
            }

        }

    }
}
