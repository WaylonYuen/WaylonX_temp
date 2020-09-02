using System;
using UnityEngine;
//using BattleChess.Management.Map;

//Undone

namespace WaylonXUnity.UnitDrawingEditor.Hex {

    public abstract class MapFloor : MonoBehaviour {

        public int Width { get; protected set; }
        public int Height { get; protected set; }

        public abstract void HandleInit(object sender, EventArgs e);
    }

    public class HexGridCellInfo {

        /// <summary>
        /// 非空檢查
        /// </summary>
        public bool IsObjEmpty { get; set; }

        /// <summary>
        /// 世界座標
        /// </summary>
        public Vector3 WorldPosition { get; set; }

        /// <summary>
        /// 3D座標
        /// </summary>
        public HexCoordinates Coordinates { get; set; }
    }

    /// <summary>
    /// 六角網格
    /// </summary>
    public class HexGrid : MapFloor {

        #region Property

        /// <summary>
        /// GridCell集合: 保存每一個cell的資訊
        /// </summary>
        public HexGridCellInfo[] Cells { get; private set; }

        #endregion

        /// <summary>
        /// 執行初始化: Handle前綴請使用EventHandler進行調用
        /// </summary>
        /// <param name="sender">發送者</param>
        /// <param name="e">事件參數</param>
        public override void HandleInit(object sender, EventArgs e) {

            //安全性檢查
            if (Cells != null) return;

            //物件拆箱
            //MapEventArgs mapEventArgs = e as MapEventArgs;

            //初始化設定
            //Width = mapEventArgs.Width;
            //Height = mapEventArgs.Height;
            Cells = new HexGridCellInfo[Height * Width];

            //建立Cell群組
            for (int z = 0, i = 0; z < Height; z++) {
                for (int x = 0; x < Width; x++, i++) {

                    //創建Cell資訊物件
                    var cell = Cells[i] = new HexGridCellInfo();

                    //設定參數
                    cell.IsObjEmpty = true;
                    cell.WorldPosition = FromGridCellCoordinates(x, z);   //紀錄世界座標
                    cell.Coordinates = HexCoordinates.FromPosition(cell.WorldPosition);//紀錄3D座標
                }
            }
        }

        /// <summary>
        /// 獲取索引
        /// </summary>
        /// <param name="coordinates">3D座標</param>
        /// <returns></returns>
        public int GetCellIndex(HexCoordinates coordinates) {
            return coordinates.X + coordinates.Z * Width + coordinates.Z / 2; //計算該座標下物件的索引
        }

        /// <summary>
        /// 網格座標: 座標系轉換(2D座標系 轉換成 3D物件座標系 以取得網格座標)
        /// </summary>
        /// <returns></returns>
        public static Vector3 FromGridCellCoordinates(int x, int z) {

            Vector3 position;
            position.x = (x + z * 0.5f - z / 2) * (HexMetrics.innerRadius * 2f);    //(x + 偏差值0.5 - 是否偏差) * 六邊形內圓直徑
            position.y = 0f;
            position.z = z * (HexMetrics.outerRadius * 1.5f);   //頂角長度 * 1.5f倍對齊

            return position;
        }

    }

}
