using System.Collections;
using System.Collections.Generic;
using SeongMin;
using UnityEngine;

namespace Jaewook
{
    public class FlashLight : MonoBehaviour, IItem
    {
        public bool isOn = true;
        public Light flashlight;
        

        //NHR
        [Header("������ UI")]
        public NHR.UIFlashlight uiFlashlight;
        //�ڷ�ƾ
        private IEnumerator flashCoroutine;

        //���͸� ��
        public bool hasBattery = true;
        public int nowBatteryTime = 20;
        public int nowBattery = 20;
        //max ��
        public int maxBatteryTime = 30;
        public int maxBattery = 30;

        private void Awake()
        {
            this.uiFlashlight = this.GetComponentInChildren<NHR.UIFlashlight>();
            this.Init();
            StartCoroutine(this.flashCoroutine);

            GameDB.Instance.myFlashLight = this;
        }
        //�ʱ� ����
        private void Init()
        {
            this.flashCoroutine = this.CLightOn();
            this.nowBattery = this.maxBattery;
            this.nowBatteryTime = this.maxBatteryTime;
            this.uiFlashlight.Init();
            this.isOn = true;

            flashlight = GetComponentInChildren<Light>();
            if (flashlight != null)
            {
                flashlight.enabled = isOn;
            }

        }

        public void OnGrab()
        {
            
            
        }

        public void OnUse()
        {
            if(this.hasBattery)
            {
                // Ʈ���� ��ư�� ������ �� �÷��ö���Ʈ �ѱ�/����
                isOn = !isOn;
                flashlight.enabled = isOn;

                //������ ���͸�
                if (this.isOn) StartCoroutine(this.flashCoroutine);
                else StopCoroutine(this.flashCoroutine);

            }

            //Debug.Log("Flashlight Used: " + (isOn ? "On" : "Off"));
        }

        public void OnRelease()
        {
            
            //Debug.Log("Flashlight Released");
        }

        /// <summary>
        /// ������ ����
        /// </summary>
        public void ChargeFlashlight()
        {
            this.nowBattery = this.maxBattery;
            this.nowBatteryTime = this.maxBatteryTime;
            this.hasBattery = true;
            foreach (var battery in this.uiFlashlight.batteries)
            {
                battery.gameObject.SetActive(true);
            }
        }


        //������ ��� �ڷ�ƾ
        public IEnumerator CLightOn()
        {
            while (true)
            {
                //Debug.LogFormat("nowBattery time : {0}", this.nowBatteryTime);
                this.nowBatteryTime--;

                //���͸� �� ĭ �ð��� �� �Ǿ��� ���
                if (this.nowBatteryTime < 0)
                {
                    this.uiFlashlight.batteries[3 - this.nowBattery].gameObject.SetActive(false);
                    this.uiFlashlight.batteries[4 - this.nowBattery].gameObject.SetActive(true);

                    Debug.Log(3 - this.nowBattery);
                    this.nowBatteryTime = 3;
                    this.nowBattery--;

                    //���͸��� �� ����� ��� ������ ���� ����
                    if (this.nowBattery <= 0)
                    {
                        StopCoroutine(this.flashCoroutine);
                        this.OnUse();
                        this.hasBattery = false;
                    }
                    Debug.LogFormat("<color=yellow>nowBatery{0}</color>", this.nowBattery);
                    this.nowBatteryTime = this.maxBatteryTime;
                }
                yield return new WaitForSeconds(1f);
            }
        }

    }
}
