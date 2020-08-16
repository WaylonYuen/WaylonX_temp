using System;
using MySql.Data.MySqlClient;
using WaylonX.Architecture;

namespace WaylonX_Database {

    /// <summary>
    /// 資料庫連線資訊EventArgs
    /// </summary>
    public class DatabaseConnectInfoEventArgs : EventArgs {

        /// <summary>
        /// 連接地址
        /// </summary>
        public string DBHost { get; set; }

        /// <summary>
        /// 用戶ID
        /// </summary>
        public string DBUser { get; set; }

        /// <summary>
        /// 數據庫名稱
        /// </summary>
        public string DBName { get; set; }

        /// <summary>
        /// 連接端口
        /// </summary>
        public string DBPort { get; set; }

        /// <summary>
        /// 字型協議
        /// </summary>
        public string DBFormat { get; set; }

        /// <summary>
        /// 數據庫密碼
        /// </summary>
        private string DBPass;

        /// <summary>
        /// 設定資料庫密碼
        /// </summary>
        /// <param name="pw"></param>
        public DatabaseConnectInfoEventArgs InputPassword(string pw) {
            DBPass = pw;
            return this;
        }

        /// <summary>
        /// 取得連線資訊字串
        /// </summary>
        /// <returns></returns>
        public string GetConnInfoStr() {
            return "server=" + DBHost +
                    ";user=" + DBUser +
                    ";database=" + DBName +
                    ";port=" + DBPort +
                    ";password=" + DBPass +
                    ";CharSet=" + DBFormat;
        }

    }

    /// <summary>
    /// 資料庫基本架構
    /// </summary>
    public abstract class DBBase_Catalina : CSDArchitecture_Catalina {

        #region Property

        /// <summary>
        /// MySql連線器
        /// </summary>
        public static MySqlConnection DBConnection { get; protected set; }

        #endregion

        //Constructor
        public DBBase_Catalina(string name) : base(name) {

        }

        #region Methods

        //添加

        //刪除

        //查找

        #endregion

    }

}
