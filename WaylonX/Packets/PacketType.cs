
namespace WaylonX.Packets {

    /// <summary>
    /// 封包架構型態
    /// </summary>
    public enum PacketType {
        None,
        StdPacket,
        Packet,
    }

    public enum PacketHeaderType {
        None,
        StdPacketHeader,
    }

    public enum PacketDataType {
        None,
        StdPacketData,
    }



    /// <summary>
    /// 加密方式
    /// </summary>
    public enum Encryption {
        None,
        Testing,

        RES256,
    }

    /// <summary>
    /// 緊急等級：判斷封包緊急程度(數值越大越緊急)
    /// </summary>
    public enum Emergency {
        None,
        Testing,

        Level1,
        Level2,
        Level3,
    }






    /// <summary>
    /// 封包類別Type：判斷封包類型並分配到指定佇列等待線程處理 
    /// </summary>
    /// 
    /// Info佇列：         處理一般資訊
    /// Room佇列：         處理房間資料(包括同步資料)
    /// GameLogic佇列：    處理遊戲邏輯
    /// Database佇列：     處理遊戲資料
    /// GameData佇列：     遊戲資料加載
    public enum Category {
        None,
        Testing,

        //緊急處理
        Emergency,

        #region Queue
        //一般(線程池)
        General,

        //遊戲資料同步
        Gaming,

        //特殊(獨立線程)
        SpecialCircumstances,

        //用戶資料：所有用戶數據資料
        Database,
        #endregion

        SIZE,
    }

    /// <summary>
    /// 回調方式：Delegate對方法回調的判斷
    /// </summary>
    public enum Callback {
        None,
        Testing,
        Login,

        ChessToBuild,

        PacketHeaderSync,
        SIZE,
    }

}


