using Photon.Pun.Demo.PunBasics;
using SeongMin;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace NHR
{
    //�÷��̾� ī�޶� ������ UI
    public class UIPlayer : MonoBehaviour
    {
        //�ð� UI
        [Header("�ð� UI")]
        public UIWatch uiWatch;

        //������
        public TMP_Text txtEnergy;

        public Image staminaBar;

        //�ڷ�ƾ
        public IEnumerator runningCoroutine;
        public IEnumerator energyChargingCoroutine;

        //������ �ٿ� �����ΰ�?
        public bool isEnergyDown = false;

        private void Awake()
        {
            if (this.uiWatch == null) this.uiWatch = GetComponentInChildren<UIWatch>();
            //�ڷ�ƾ �Ҵ�
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
                //�޸��� ��ư�� ������ ���׹̳��� 0���� ũ�� �޸��� �ڷ�ƾ ���� , ���� �ڷ�ƾ ����
                if (staminaBar.fillAmount > 0)
                {
                    this.isEnergyDown = false;
                    StartCoroutine(this.runningCoroutine);
                    StopCoroutine(this.energyChargingCoroutine);
                    Debug.Log("�޸�");
                }
                //�޸��� ��ư�� �������� ���׹̳��� 0�̶�� �޸��� �ڷ�ƾ ���߰� ���� �ڷ�ƾ ����
                if (staminaBar.fillAmount <= 0)
                {
                    this.isEnergyDown = true;
                    StopCoroutine(this.runningCoroutine);
                    StartCoroutine(this.energyChargingCoroutine);
                    Debug.Log("����");
                }
            }
            else //�޸��� ��ư�� ������ ������ ���� �ڷ�ƾ ����
                StartCoroutine(this.energyChargingCoroutine);
        }
        public IEnumerator CRunning()
        {
            while (true)
            {
                yield return new WaitForSeconds(1f);

                staminaBar.fillAmount -= 0.01f;
                
                //�������� �� ����ߴٸ� �޸��� �� �ڷ�ƾ ���߱�
                if (staminaBar.fillAmount <= 0)
                {
                    this.isEnergyDown = true;
                    StopCoroutine(this.runningCoroutine);
                    StartCoroutine(this.energyChargingCoroutine);
                }
            }
        }

        //������ ���� ��
        public IEnumerator CEnergyCharging()
        {
            while (true)
            {
                yield return new WaitForSeconds(1f);

                //������ �� �����Ǿ��ٸ� ���� �ڷ�ƾ ���߱�
                if (staminaBar.fillAmount >= 1)
                {
                    this.isEnergyDown = false;
                    Debug.Log("���� ��");
                    StopCoroutine(this.energyChargingCoroutine);
                }
                staminaBar.fillAmount += 0.01f;
            }
        }
    }
}
