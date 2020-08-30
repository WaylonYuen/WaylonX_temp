using UnityEngine;

namespace WaylonXUnity.UnitDrawingEditor.Hex {

    [System.Serializable]
    public struct HexCoordinates {

        [SerializeField]
        private int x, z;

        public int X { get => x; }
        public int Z { get => z; }
        public int Y { get => -X - Z; }

        public Vector3 Vector3 { get { return new Vector3(X, Y, Z); } }

        public HexCoordinates(int x, int z) {
            this.x = x;
            this.z = z;
        }

        //物件座標設定
        public static HexCoordinates FromOffsetCoordinates(int x, int z) {
            return new HexCoordinates(x - z / 2, z);
        }

        //取得物件座標的物件索引
        /// <summary>
        /// 六角形平面三維座標
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public static HexCoordinates FromPosition(Vector3 position) {

            float x = position.x / (HexMetrics.innerRadius * 2f);
            float y = -x;

            float offset = position.z / (HexMetrics.outerRadius * 3f);

            x -= offset;
            y -= offset;

            int iX = Mathf.RoundToInt(x);
            int iY = Mathf.RoundToInt(y);
            int iZ = Mathf.RoundToInt(-x - y);

            if (iX + iY + iZ != 0) {
                float dX = Mathf.Abs(x - iX);
                float dY = Mathf.Abs(y - iY);
                float dZ = Mathf.Abs(-x - y - iZ);

                if (dX > dY && dX > dZ) {
                    iX = -iY - iZ;
                } else if (dZ > dY) {
                    iZ = -iX - iY;
                }
            }

            return new HexCoordinates(iX, iZ);
        }


        public override string ToString() {
            return "(" +
                X.ToString() + ", " + Y.ToString() + ", " + Z.ToString() + ")";
        }

        public string ToStringOnSeparateLines() {
            return X.ToString() + "\n" + Y.ToString() + "\n" + Z.ToString();
        }

    }

}