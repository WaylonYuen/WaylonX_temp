using System;

namespace Waylong.Architecture {

    //IP.prot && IPAddress 應採用 字典結構 統一管理

    //必要參數：
    //string 名稱
    //enum 操作環境
    //

    //CSModel 參數
    public interface ICSParameter {

        /// <summary>
        /// 名稱
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// IP位址
        /// </summary>
        string IP { get; }

        /// <summary>
        /// 端口
        /// </summary>
        int Port { get; }

        /// <summary>
        /// IP地址
        /// </summary>
        string IPAddress { get; }

        /// <summary>
        /// 操作環境
        /// </summary>
        OperatingEnvironment Environment { get; }
    }

    //操作環境
    public enum OperatingEnvironment {
        Unknow,
        Unity,      //Debug.Log
        Terminal,   //CW
    }

}
