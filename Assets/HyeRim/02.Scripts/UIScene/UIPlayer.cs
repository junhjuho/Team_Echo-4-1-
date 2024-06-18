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

        public Transform[] imageDeaths = new Transform[3];

        //�ڷ�ƾ
        public IEnumerator runningCoroutine;
        public IEnumerator energyChargingCoroutine;

        //������ �ٿ� �����ΰ�?
        public bool isEnergyDown = false;

        bool isRunningCoroutine;
        bool isChargingCoroutine;

        //public void ImageDeaths(int count)
        //{
        //    int i = count + currentCount;
        //    imageDeaths[i].gameObject.SetActive(true);
        //    currentCount++;
        //}
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

            //for(int i = 0; i < 3; i++)
            //{
            //    imageDeaths[i] = this.transform.GetChild(0).GetChild(4).GetChild(5).GetChild(i).GetChild(0);
            //}
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
                else //(staminaBar.fillAmount <= 0)
                {
                    this.isEnergyDown = true;
                    //StopCoroutine(this.runningCoroutine);
                    StartCoroutine(this.energyChargingCoroutine);
                    Debug.Log("����");
                }
            }
            else //�޸��� ��ư�� ������ ������ ���� �ڷ�ƾ ����
                StartCoroutine(this.energyChargingCoroutine);
        }
        public IEnumerator CRunning()
        {
            while (staminaBar.fillAmount > 0f)
            {
                yield return new WaitForSeconds(1f);
                staminaBar.fillAmount -= 0.01f;
            }
            //�������� �� ����ߴٸ� �޸��� �� �ڷ�ƾ ���߱�
            this.isEnergyDown = true;
            StopCoroutine(this.runningCoroutine);
            //StartCoroutine(this.energyChargingCoroutine);  
        }

        //������ ���� ��
        public IEnumerator CEnergyCharging()
        {
            while (staminaBar.fillAmount >= 1f)
            {
                yield return new WaitForSeconds(1f);
                staminaBar.fillAmount += 0.01f;
            }
            //������ �� �����Ǿ��ٸ� ���� �ڷ�ƾ ���߱�   
             this.isEnergyDown = false;
             Debug.Log("���� ��");
             StopCoroutine(this.energyChargingCoroutine);
        }
    }
}
