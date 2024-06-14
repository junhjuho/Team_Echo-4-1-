using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace NHR
{
    //���忡 ��ġ�� UI
    public class UIWorld : MonoBehaviour
    {
        //�ӽ� ������ ���� ��ư
        public Button buttonFlashOnOff;
        private TMP_Text txtOnOff;
        //�ӽ� ������ ���� ��ư
        public Button buttonFlashCharge;
        //�ӽ� Ȯ��
        public bool isFlashlightOn = false;

        //������
        public UIFlashlight uiFlashlight;

        //�ڷ�ƾ
        private IEnumerator flashCoroutine;

        private void Awake()
        {
            this.txtOnOff = this.buttonFlashOnOff.GetComponentInChildren<TMP_Text>();
            this.flashCoroutine = this.CLightOn();
        }

        private void Start()
        {
            this.buttonFlashOnOff.onClick.AddListener(() =>
            {
                Debug.Log("btn flash OnOff clicked");
                if(this.uiFlashlight.hasBattery)
                {
                    this.isFlashlightOn = !this.isFlashlightOn;
                    if (this.isFlashlightOn)
                    {
                        this.txtOnOff.text = "On";
                        StartCoroutine(this.flashCoroutine);
                        Debug.Log("Start CLightOn int UIWorld");
                    }
                    else
                    {
                        this.txtOnOff.text = "Off";
                        StopCoroutine(flashCoroutine);
                    }

                }
            });
            this.buttonFlashCharge.onClick.AddListener(() =>
            {
                this.uiFlashlight.ChargeFlashlight();
            });
        }

        //������ ���͸�
        public IEnumerator CLightOn()
        {
            while (true)
            {
                Debug.LogFormat("nowBattery time : {0}", this.uiFlashlight.nowBatteryTime);
                this.uiFlashlight.nowBatteryTime--;

                //���͸� �� ĭ �ð��� �� �Ǿ��� ���
                if (this.uiFlashlight.nowBatteryTime < 0)
                {
                    this.uiFlashlight.batteries[3 - this.uiFlashlight.nowBattery].SetActive(false);
                    Debug.Log(3 - this.uiFlashlight.nowBattery);
                    this.uiFlashlight.nowBatteryTime = 3;
                    this.uiFlashlight.nowBattery--;

                    //���͸��� �� ����� ��� ������ ���� ����
                    if (this.uiFlashlight.nowBattery <= 0)
                    {
                        StopCoroutine(this.flashCoroutine);
                        this.txtOnOff.text = "Off";
                        this.isFlashlightOn = false;
                        this.uiFlashlight.hasBattery = false;
                    }
                    Debug.LogFormat("<color=yellow>nowBatery{0}</color>", this.uiFlashlight.nowBattery);
                    this.uiFlashlight.nowBatteryTime = this.uiFlashlight.maxBatteryTime;
                }
                yield return new WaitForSeconds(1f);
            }
        }
    }

}
