using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using WaylonXUnity.Users;

namespace WaylonXUnity.Utils {

    public class Demo : MonoBehaviour, IPointerDownHandler {

        [SerializeField] private Camera cam;                //相機
        [SerializeField] private Canvas canvas;             //主畫布
        [SerializeField] private RectTransform baseRect;    //基底(用於座標轉換,將screen座標 轉換 成基底座標) = swipeBox

        private CoordinateTransformation screenPointToAncPos;

        
        private void Start() {

            //Debug.Log(baseRect.sizeDelta);

            //screenPointToAncPos = new CoordinateTransformation(
            //    new ScreenPointToAnchoredPosition(cam, canvas, baseRect, baseRect.sizeDelta, OffsetDirection.Bottom));   //座標轉換實例
        }

        public void OnPointerDown(PointerEventData eventData) {

            //Vector2 touchPoint = screenPointToAncPos.Execute(eventData.position);   //取得在baseRect平面上的座標

            //Debug.Log($"Pos {eventData.position}  Touch {touchPoint}");

            //Debug.Log(eventData.position);
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(baseRect, eventData.position, null, out Vector2 localPoint)) {

                localPoint += (baseRect.sizeDelta / 2f);

                Debug.Log(localPoint);
            }
        }


    }

}