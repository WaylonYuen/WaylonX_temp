using System;

namespace Waylong.Architecture {

    public abstract class CSModelBase {

        //初始化: 用於初始化 DataStruct 和 Registered
        protected abstract void Initialize();

        //資料結構: 用於保存各種類型的資料及資料處理的方式
        protected abstract void DataStruct();

        //回調方法註冊: 註冊後的方法才能夠被外派調用並呼叫執行
        protected abstract void Registered();

        #region Network

        //網絡管理: 用於管理協議
        protected abstract void NetworkManagement();


        #endregion

        #region Thread

        /// <summary>
        /// 啟動線程: 用於啟動各線程
        /// </summary>
        protected abstract void StartThread();

        /// <summary>
        /// 執行回調線程
        /// </summary>
        protected abstract void Execute_CallbackThread();

        #endregion

    }
}
