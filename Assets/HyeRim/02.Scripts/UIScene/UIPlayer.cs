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

        //코루틴
        public IEnumerator runningCoroutine;
        public IEnumerator energyChargingCoroutine;

        //에너지 다운 상태인가?
        public bool isEnergyDown = false;

        private void Awake()
        {
            if (this.uiWatch == null) this.uiWatch = GetComponentInChildren<UIWatch>();
            //코루틴 할당
            this.runningCoroutine = this.CRunning();
            this.energyChargingCoroutine = this.CEnergyCharging();
        }
        private void Start()
        {
            SeongMin.GameManager.Instance.playerManager.uiPlayer = this;
        }
        private void LateUpdate()
        {
            if (SeongMin.GameManager.Instance.playerManager.humanMovement.isRunBtnDown)
            {
                //달리는 버튼이 눌리고 스테미나가 0보다 크면 달리기 코루틴 실행 , 충전 코루틴 멈춤
                if (staminaBar.fillAmount > 0)
                {
                    this.isEnergyDown = false;
                    StartCoroutine(this.runningCoroutine);
                    StopCoroutine(this.energyChargingCoroutine);
                    Debug.Log("달림");
                }
                //달리는 버튼이 눌렸지만 스테미나가 0이라면 달리는 코루틴 멈추고 충전 코루틴 실행
                if (staminaBar.fillAmount <= 0)
                {
                    this.isEnergyDown = true;
                    StopCoroutine(this.runningCoroutine);
                    StartCoroutine(this.energyChargingCoroutine);
                    Debug.Log("충전");
                }
            }
            else //달리는 버튼이 눌리지 않으면 충전 코루틴 실행
                StartCoroutine(this.energyChargingCoroutine);
        }
        public IEnumerator CRunning()
        {
            while (true)
            {
                yield return new WaitForSeconds(1f);

                staminaBar.fillAmount -= 0.01f;
                
                //에너지를 다 사용했다면 달리는 중 코루틴 멈추기
                if (staminaBar.fillAmount <= 0)
                {
                    this.isEnergyDown = true;
                    StopCoroutine(this.runningCoroutine);
                    StartCoroutine(this.energyChargingCoroutine);
                }
            }
        }

        //에너지 충전 중
        public IEnumerator CEnergyCharging()
        {
            while (true)
            {
                yield return new WaitForSeconds(1f);

                //에너지 다 충전되었다면 충전 코루틴 멈추기
                if (staminaBar.fillAmount >= 1)
                {
                    this.isEnergyDown = false;
                    Debug.Log("충전 끝");
                    StopCoroutine(this.energyChargingCoroutine);
                }
                staminaBar.fillAmount += 0.01f;
            }
        }
    }
}
