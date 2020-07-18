using System;
using System.Net.Sockets;
using System.Security.Cryptography;
using Waylong.Attributes;
using Waylong.Net;

namespace Waylong.Architecture {

    //Client-Server-Model: 主從式架構
    public abstract class CSModel {

        #region Property

        #endregion

        #region Object

        //網路管理類
        protected NetworkManagement NetworkManagement = new NetworkManagement();

        #endregion

        #region Local Values

        protected NetMode m_netMode;

        #endregion

        #region Methods

        //啓動器
        public abstract void Start(string ip, int prot);

        //關閉
        public abstract void Close();

        //初始化: 用於初始化 DataStruct 和 Registered
        protected abstract void Initialize();

        //資料結構: 用於保存各種類型的資料及資料處理的方式
        protected abstract void DataStruct();

        //回調方法註冊: 註冊後的方法才能夠被外派調用並呼叫執行
        protected abstract void Registered();

        #region Thread

        /// <summary>
        /// 啟動線程: 用於啟動各線程
        /// </summary>
        protected abstract void Start_Thread();

        /// <summary>
        /// 關閉線程
        /// </summary>
        protected abstract void Close_Thread();

        /// <summary>
        /// 執行回調線程
        /// </summary>
        protected abstract void Execute_CallbackThread();

        #endregion


        #endregion

    }

}
