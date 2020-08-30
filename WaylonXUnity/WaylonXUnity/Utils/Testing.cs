using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WaylonXUnity.Utils {


    public interface ICoordinateTransformation {

        Camera Cam { get; set; }

        //主畫布
        Canvas Canvas { get; set; }

        //轉換目標的基底
        RectTransform BaseRect { get; set; }


        Vector2 RectOffset { get; set; }


        OffsetDirection OffsetDirection { get; set; }


        Vector2 CoorTransformation(Vector2 screenPosition);
    }

    public class CoordinateTransformation {

        ICoordinateTransformation TransformationTo;

        //讀取轉換類
        public CoordinateTransformation(ICoordinateTransformation TransformationTo) {
            this.TransformationTo = TransformationTo;
        }

        //執行
        public Vector2 Execute(Vector2 screenPosition) {
            return TransformationTo.CoorTransformation(screenPosition);
        }

    }

    //實現屏幕座標轉換成父類Rect座標
    /// <summary>
    /// 
    /// </summary>
    public class ScreenPointToAnchoredPosition : ICoordinateTransformation {

        #region Property

        Camera ICoordinateTransformation.Cam {
            get => m_camera;
            set => m_camera = value;
        }

        Canvas ICoordinateTransformation.Canvas {
            get => m_canvas;
            set => m_canvas = value;
        }

        RectTransform ICoordinateTransformation.BaseRect {
            get => m_baseRect;
            set => m_baseRect = value;
        }

        Vector2 ICoordinateTransformation.RectOffset {
            get => m_rectOffset;
            set => m_rectOffset = value;
        }

        OffsetDirection ICoordinateTransformation.OffsetDirection {
            get => m_offsetDirection;
            set => m_offsetDirection = value;
        }

        #endregion

        #region Local Values

        private Camera m_camera;
        private Canvas m_canvas;
        private RectTransform m_baseRect;
        private OffsetDirection m_offsetDirection;
        private Vector2 m_rectOffset;

        #endregion


        //Instance
        public ScreenPointToAnchoredPosition(Camera cam, Canvas canvas, RectTransform baseRect, Vector2 rectOffset, OffsetDirection offsetDirection) {
            m_camera = cam;
            m_canvas = canvas;
            m_baseRect = baseRect;
            m_rectOffset = rectOffset;
            m_offsetDirection = offsetDirection;
        }

        //轉換方法
        Vector2 ICoordinateTransformation.CoorTransformation(Vector2 screenPosition) {

            Vector2 localPoint = Vector2.zero;

            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(m_baseRect, screenPosition, m_camera, out localPoint)) {
                return localPoint += OffsetDirection();    //計算偏差值
            }

            return Vector2.zero;
        }

        //偏差位
        public Vector2 OffsetDirection() {

            switch (m_offsetDirection) {

                case Utils.OffsetDirection.None:
                    return Vector2.zero;

                case Utils.OffsetDirection.Bottom:
                    return new Vector2(m_rectOffset.x / 2f, 0f);

                default:
                    return Vector2.zero;
            }
        }

    }

    //參考方位
    public enum OffsetDirection {
        None,
        LeftUp,
        Up,
        RightUp,
        Left,
        Mid,
        Right,
        LeftBottom,
        Bottom,
        RightBottom,
    }

}