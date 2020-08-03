using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Waylong.Loggers {

    public class StdLogger {

        /// <summary>
        /// log資訊佇列
        /// </summary>
        public static ConcurrentQueue<string> logQueue = new ConcurrentQueue<string>();

        /// <summary>
        /// 日誌紀錄
        /// </summary>
        private class Log {

            private readonly LogType Type;
            private readonly string Detail;

            public Log(LogType type, string detail) {
                Type = type;
                Detail = detail;
            }

            public string WriteLogs() {

                string logType = " ";
                char typeLabel = ' ';
                char typeTag = ' ';

                switch (Type) {

                    case LogType.Info:
                        logType = "Info:";
                        typeLabel = '#';
                        break;

                    case LogType.Catch:
                        logType = "Catch:";
                        typeLabel = '>';
                        typeTag = '%';
                        break;

                    case LogType.Warn:
                        logType = "Warning:";
                        typeLabel = '>';
                        typeTag = '@';
                        break;

                    case LogType.Error:
                        logType = "Error:";
                        typeLabel = '>';
                        typeTag = '!';
                        break;

                    case LogType.Correct:
                        logType = "Correct:";
                        typeLabel = 'R';
                        typeTag = '<';
                        break;

                    default:
                        //沒有這個類別
                        break;
                }

                //整合
                string Logs = $"  {typeTag}  {DateTime.Now:dd/MM/yyyy  HH:mm:ss - fff}  {typeLabel} [{logType,10}  {logType} ]  Details -> {Detail}";

                //Undone: 寫到外部Log日誌

                return Logs;               
            }

            /// <summary>
            /// 將日誌Push到Logger自帶的佇列中
            /// </summary>
            public void EnQueue() {
                logQueue.Enqueue(WriteLogs());
            }
        }

        /// <summary>
        /// 日誌容器
        /// </summary>
        public class Container {

            private readonly string Discription;

            private readonly string BaseToString;

            private Dictionary<string, string> containerDict = new Dictionary<string, string>();

            public Container(string discription, string baseToString) {
                Discription = discription;
                BaseToString = baseToString;
            }

            public void Add(string title, string detail) {
                containerDict.Add(title, detail);
            }

            public string WriteLogs() {

                int maxLength = 0;

                //取得最長的title 以便做格式化
                foreach (var info in containerDict) {
                    if (maxLength < info.Key.Length) {
                        maxLength = info.Key.Length;
                    }
                }

                string str_Logs = "\n  v  " + DateTime.Now.ToString("dd/MM/yyyy  HH:mm:ss - fff") + "  # " + Discription + " -> " + BaseToString + "\n\t  |  Info:\n";


                foreach (var info in containerDict) {

                    //動態調整空格對齊
                    str_Logs += "\t  |  >>  ";
                    for (int i = 0; i < (maxLength - info.Key.Length); i++) {
                        str_Logs += " ";
                    }
                    str_Logs += info.Key + " : " + info.Value + "\n";
                }

                //動態調整結尾包裹
                str_Logs += "\t  |";
                if ((maxLength - 40) > 10) {
                    for (int i = 0; i < (maxLength - 40) + 10; i++) {
                        str_Logs += "-";
                    }
                }
                str_Logs += "-----------------------------------------------------end]\n";

                //Undone: 寫到外部Log日誌

                return str_Logs;
            }

            public void Excute() {
                logQueue.Enqueue(WriteLogs());
            }
        }

        #region Logged Methods

        public void Info(string content) {
            var Log = new Log(LogType.Info, content);
            Log.EnQueue();
        }

        public void Debug(string content) {
            var Log = new Log(LogType.Debug, content);
            Log.EnQueue();
        }

        public void Warn(string content) {
            var Log = new Log(LogType.Warn, content);
            Log.EnQueue();
        }

        public void Error(string content) {
            var Log = new Log(LogType.Error, content);
            Log.EnQueue();
        }

        /// <summary>
        /// 取得Log容器: 用以儲存相關的多筆需要紀錄的資訊
        /// </summary>
        /// <param name="discription">容器描述 -> 此容器紀錄的資訊概括</param>
        /// <param name="baseToString">來自被呼叫的父類方法ToString</param>
        /// <returns>Log容器</returns>
        public Container GetContainer(string discription, string baseToString) {
            return new Container(discription, baseToString);
        }

        public void Testing() {

            Console.WriteLine("Start Test");
            while (logQueue.Count > 0) {
                logQueue.TryDequeue(out string logged);
                Console.WriteLine(logged);
            }
            Console.WriteLine("End Test");
        }
        #endregion

        private enum LogType {
            Info,
            Debug,
            Warn,
            Error,
            Catch,
            Correct,
        }
    }
}
