using NHR;
using SeongMin;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NHR
{
    public class UIWatch : MonoBehaviour
    {
        public UIInventory uiInventory;
        public UIHeartWatch uiHeart;
        public UIMission uiMission;
        public int nowHeart;

        public Image dim;

        private void Awake()
        {
            this.uiInventory = GetComponentInChildren<UIInventory>();
            this.uiHeart = GetComponentInChildren<UIHeartWatch>();
            this.uiMission = GetComponentInChildren<UIMission>();
        }
        //private void Start()
        //{
        //    this.dim.gameObject.SetActive(false);
        //}

    }

}
