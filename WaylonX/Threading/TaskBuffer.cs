using System;
using System.Collections.Concurrent;
using WaylonX.Packets;

namespace WaylonX.Threading {

    /// <summary>
    /// 任務緩衝列隊資料
    /// </summary>
    public class TaskBufferQueueInfoEventArgs : EventArgs {
        public int BreakTime { get; set; }
        public Category Category { get; set; }
    }

    public class TaskBuffer<TCategory, TCallback, THandler> {

        #region Property

        public int CategoryCount { get => TaskQueueDict.Count; }

        #endregion

        #region Local values

        /// <summary>
        /// 回調字典
        /// </summary>
        public ConcurrentDictionary<TCategory, ConcurrentDictionary<TCallback, THandler>> TaskQueueDict;

        /// <summary>
        /// 封包佇列字典
        /// </summary>
        public ConcurrentDictionary<TCategory, ConcurrentQueue<CallbackHandlerPacket>> PacketQueueDict;

        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        public TaskBuffer() {

            //Instance
            TaskQueueDict = new ConcurrentDictionary<TCategory, ConcurrentDictionary<TCallback, THandler>>();
            PacketQueueDict = new ConcurrentDictionary<TCategory, ConcurrentQueue<CallbackHandlerPacket>>();
        }

        #region Methods


        #region Register
        /// <summary>
        /// 回調方法註冊器
        /// </summary>
        /// <param name="category">佇列組類別</param>
        /// <param name="callback">回調方法</param>
        /// <param name="heandler">委派處理器</param>
        public void CallbackHandlerRegister(TCategory category, TCallback callback, THandler handler) {

            //判斷Category(類別)是否已存在
            if (TaskQueueDict.ContainsKey(category)) {  //不存在表示沒有該類別, 則不准註冊

                //判斷Category(類別)索引中的字典Callback索引是否存在
                if (!TaskQueueDict[category].ContainsKey(callback)) {   //不存在才允許註冊
                    TaskQueueDict[category].TryAdd(callback, handler); //註冊
                    return;

                } else {
                    //回調已存在
                }
            } else {
                //類別不存在
            }

        }

        /// <summary>
        /// 類別註冊器
        /// </summary>
        /// <param name="category">佇列類別</param>
        public void CategoryRegister(TCategory category) {

            //判斷是否已存在對應類別
            if (!TaskQueueDict.ContainsKey(category) && !PacketQueueDict.ContainsKey(category)) {

                //創建新類別
                TaskQueueDict.TryAdd(category, new ConcurrentDictionary<TCallback, THandler>());
                PacketQueueDict.TryAdd(category, new ConcurrentQueue<CallbackHandlerPacket>());
                return;
            }

            //類別已存在
        }
        #endregion


        /// <summary>
        /// 回調索引檢查
        /// </summary>
        /// <param name="category">類別</param>
        /// <param name="callback">回調</param>
        public bool CallbackContainsKey(TCategory category, TCallback callback) {

            //判斷Category是否存在
            if (TaskQueueDict.ContainsKey(category)) {

                //判斷Callback是否存在
                if (TaskQueueDict[category].ContainsKey(callback)) {
                    return true;
                }

                //callback不存在
                return false;
            }

            //類別不存在
            return false;
        }

        /// <summary>
        /// 獲取回調用: 請不要隨意調用(此處未進行存在檢查, 分配器中由於必然性因此可被直接調用且無需檢查)
        /// </summary>
        /// <param name="category"></param>
        /// <param name="callback"></param>
        /// <returns></returns>
        public THandler GetHandler(TCategory category, TCallback callback) {
            return TaskQueueDict[category][callback];
        }

        /// <summary>
        /// 加入佇列隊伍: 請不要隨意調用(此處未進行存在檢查, 分配器中由於必然性因此可被直接調用且無需檢查)
        /// </summary>
        /// <param name="packet"></param>
        public void Enqueue(TCategory category, CallbackHandlerPacket packet) {
            PacketQueueDict[category].Enqueue(packet);
        }

        #endregion
    }

}
