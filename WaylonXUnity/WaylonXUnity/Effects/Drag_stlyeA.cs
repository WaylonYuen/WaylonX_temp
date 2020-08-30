using UnityEngine;
using UnityEngine.EventSystems;

namespace WaylonXUnity.Effects {

    public class Drag_stlyeA : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler {

        public RectTransform BaseRect { get; set; }

        //紀錄觸控座標
        private Vector2 TouchPosition;

        private Vector3 OriPosition;

        

        private void Start() {

            //OriPosition = BaseRect.position;

            //RectTransformUtility.ScreenPointToLocalPointInRectangle(BaseRect, transform.position, null, out Vector2 localPoint);
            //localPoint += BaseRect.sizeDelta / 2f;

            //Debug.Log(OriPosition);
            //RectTransformUtility.ScreenPointToLocalPointInRectangle(baseRect, transform.position, null, out Vector2 localPoint);
            //    localPoint += baseRect.sizeDelta / 2f;

            //沒有計算偏移所以會回到 00座標
            //OriPosition = localPoint;
            Debug.Log(BaseRect is null);
        }

        public void OnPointerDown(PointerEventData eventData) {
            TouchPosition = eventData.position;
            //TouchPosition = localPoint;
        }

        public void OnDrag(PointerEventData eventData) {
            transform.position = eventData.position;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(BaseRect, eventData.position, null, out Vector2 localPoint);
            Debug.Log(localPoint);
        }

        public void OnPointerUp(PointerEventData eventData) {
            transform.position = OriPosition;
            //Debug.Log($"Test = {transform.position}");
        }
    }

}