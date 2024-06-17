using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NHR
{
    public class UIFlashlight : MonoBehaviour
    {
        //배터리들
        public Image[] batteries;

        private void Awake()
        {
            this.batteries = GetComponentsInChildren<Image>();
        }

        //초기 설정
        public void Init()
        {
            foreach(var battery in batteries)
            {
                battery.gameObject.SetActive(false);
            }
            this.batteries[0].gameObject.SetActive(true);
        }
    }
}
