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

        [Header("팁 팝업 이벤트 UI")]
        public UITip uiTip;

        //에너지
        public TMP_Text txtEnergy;

        public Image staminaBar;

        private void Awake()
        {
            if (this.uiWatch == null) this.uiWatch = GetComponentInChildren<UIWatch>();
            this.uiTip = GetComponentInChildren<UITip>();
            this.uiTip.Init();
        }
        private void Start()
        {
            SeongMin.GameManager.Instance.playerManager.uiPlayer = this;
        }
        private void LateUpdate()
        {
            // 스테미너 코드
            if (SeongMin.GameManager.Instance.playerManager.humanMovement.isRunBtnDown)
            {
                staminaBar.fillAmount -= 0.2f * Time.deltaTime;

                SeongMin.GameManager.Instance.playerManager.humanMovement.isEnergyDown = staminaBar.fillAmount > 0 ? false : true;
            }
            else
            {
                if(staminaBar.fillAmount < 1)
                {
                    staminaBar.fillAmount += 0.2f * Time.deltaTime;
                }
            }
        }
    }
}
