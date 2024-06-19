using Photon.Pun.Demo.PunBasics;
using SeongMin;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace NHR
{
    //플레이어 카메라에 부착된 UI
    public class UIPlayer : MonoBehaviour
    {
        //시계 UI
        [Header("시계 UI")]
        public UIWatch uiWatch;

        //에너지
        public TMP_Text txtEnergy;

        public Image staminaBar;

        //에너지 다운 상태인가?
        public bool isEnergyDown = false;

        private void Awake()
        {
            if (this.uiWatch == null) this.uiWatch = GetComponentInChildren<UIWatch>();
        }
        private void Start()
        {
            SeongMin.GameManager.Instance.playerManager.uiPlayer = this;
        }
        private void LateUpdate()
        {
            if (SeongMin.GameManager.Instance.playerManager.humanMovement.isRunBtnDown)
            {
                staminaBar.fillAmount -= 0.1f * Time.deltaTime;

                this.isEnergyDown = staminaBar.fillAmount > 0 ? false : true;

                Debug.Log(staminaBar.fillAmount);
            }
            else
            {
                if(staminaBar.fillAmount < 1)
                {
                    staminaBar.fillAmount += 0.1f * Time.deltaTime;
                }
            }
        }
    }
}
