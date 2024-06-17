using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NHR
{
    public class UIFlashlight : MonoBehaviour
    {
        //���͸���
        public Image[] batteries;

        private void Awake()
        {
            this.batteries = GetComponentsInChildren<Image>();
        }

        //�ʱ� ����
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
