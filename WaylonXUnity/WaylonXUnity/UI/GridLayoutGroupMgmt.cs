using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WaylonXUnity.Effects;

namespace WaylonXUnity.UI {

    //自動添加GridLayoutGroup
    public class GridLayoutGroupMgmt : MonoBehaviour {

        /// <summary>
        /// Max Slot Count
        /// </summary>
        public int GridCount { get => m_Slots.Length; }

        #region Local Values

        /// <summary>
        /// 生成Slot的Prefab
        /// </summary>
        [SerializeField]
        private GameObject m_Slot;

        /// <summary>
        /// 保存Slot
        /// </summary>
        [SerializeField]
        private GameObject[] m_Slots;

        //Componenet
        private GridLayoutGroup m_GridLayoutGroup;

        #endregion

        private void Awake() {
            AddController();
        }

        // Start is called before the first frame update
        void Start() {

            m_GridLayoutGroup = GetComponent<GridLayoutGroup>();

            //將Slot尺寸設定成GridLayoutGroup尺寸()
            m_Slot.GetComponent<RectTransform>().sizeDelta = m_GridLayoutGroup.cellSize;

            //創建Slot(創建空白的,方便替換)
            m_Slots = new GameObject[GridCount];
            for (int i = 0; i < GridCount; i++) {
                GameObject slot = m_Slots[i] = Instantiate(m_Slot); //預設new 空白slot

                //優化
                slot.GetComponent<Drag_stlyeA>().BaseRect = GetComponent<RectTransform>();

                slot.transform.SetParent(transform, false);
            }
        }

        //添加手勢控制方法
        private void AddController() {
            //var drag = m_Slot.AddComponent<Drag_stlyeA>();
            //var T = m_Slot.GetComponent<Drag_stlyeA>().BaseRect;
            //Debug.Log(T is null);

            //m_Slot.GetComponent<Drag_stlyeA>().BaseRect = GetComponent<RectTransform>();
            //Debug.Log(drag.BaseRect.position);

            //var T = GetComponent<RectTransform>();
            //RectTransformUtility.ScreenPointToLocalPointInRectangle(T, new Vector3(500f, 150f, 0f), null, out Vector2 localPoint);
            //Debug.Log(localPoint);
        }

        //填充
        public bool Filling(int index, GameObject stuffing, RectTransform stuffingRectTransform) {

            if (index < 0 || index >= GridCount) return false;

            //將stuffing尺寸設定成GridLayoutGroup尺寸()
            stuffingRectTransform.sizeDelta = m_GridLayoutGroup.cellSize;
            m_Slots[index] = stuffing;
            return true;
        }

        //移除
        public bool Remove(int index, GameObject stuffing) {

            if (index < 0 || index >= GridCount) return false;

            m_Slots[index] = m_Slot;
            return true;
        }

        //移動
        public bool Shift(int direction) {
            throw new System.Exception("Undone");
        }

        //排序
        public bool Sort() {
            throw new System.Exception("Undone");
        }
    }

}