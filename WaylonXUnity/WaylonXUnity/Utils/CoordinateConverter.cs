using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WaylonXUnity.Utils {

    /// <summary>
    /// 座標轉換器
    /// </summary>
    public class CoordinateConverter : MonoBehaviour {

        /// <summary>
        /// 螢幕座標轉換成錨點座標
        /// </summary>
        public Vector2 ScreenPointToAnchoredPosition(RectTransform rect, Vector2 screenPoint, Camera cam) {

            Vector2 localPoint = Vector2.zero;


            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(rect, screenPoint, cam, out localPoint)) {
                //return localPoint += OffsetDirection();    //計算偏差值
            }

            return Vector2.zero;

        }


    }

}