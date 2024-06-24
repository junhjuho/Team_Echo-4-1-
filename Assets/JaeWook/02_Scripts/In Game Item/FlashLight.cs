using System.Collections;
using System.Collections.Generic;
using NHR;
using SeongMin;
using UnityEngine;

namespace Jaewook
{
    public class FlashLight : MonoBehaviour, IItem
    {
        private WaitForSeconds onesec = new WaitForSeconds(1);
        public bool isOn = true;
        public Light flashlight;
        

        //NHR
        [Header("������ UI")]
        public NHR.UIFlashlight uiFlashlight;
        //�ڷ�ƾ
        private IEnumerator flashCoroutine;

        //���͸� ��
        public bool hasBattery = true;
        public int nowBatteryTime = 2;
        public int nowBattery = 0;
        //max ��
        public int maxBatteryTime = 3;
        public int maxBattery = 3;

        private void Awake()
        {
            /*
            this.uiFlashlight = this.GetComponentInChildren<NHR.UIFlashlight>();
            this.Init();
            StartCoroutine(this.flashCoroutine);

            // GameDB.Instance.myFlashLight = this;
            */
        }
        //�ʱ� ����
        
        private void Init()
        {
            /*
            this.flashCoroutine = this.CLightOn();
            this.nowBattery = this.maxBattery;
            this.nowBatteryTime = this.maxBatteryTime;
            this.uiFlashlight.Init();
            */
            this.isOn = true;

            flashlight = GetComponentInChildren<Light>();
            if (flashlight != null)
            {
                flashlight.enabled = isOn;
            }

        }

        public virtual void OnGrab()
        {
            //
            
        }

        public virtual void OnUse()
        {

            // Ʈ���� ��ư�� ������ �� �÷��ö���Ʈ �ѱ�/����
            isOn = !isOn;
            flashlight.enabled = isOn;

            /*
            //������ ���͸�
            if (this.isOn) StartCoroutine(this.flashCoroutine);
            else StopCoroutine(this.flashCoroutine);
            */


            Debug.Log("Flashlight Used: " + (isOn ? "On" : "Off"));
        }

        public void OnRelease()
        {
            
            //Debug.Log("Flashlight Released");
        }

        /// <summary>
        /// ������ ����
        /// </summary>
        /*
        public void ChargeFlashlight()
        {
            this.nowBattery = this.maxBattery; // maxtime = 30;
            this.nowBatteryTime = this.maxBatteryTime - 1;
            this.hasBattery = true;
            foreach (var battery in this.uiFlashlight.batteries)
            {
                battery.gameObject.SetActive(true);
            }
        }
        */

        /*
        //������ ��� �ڷ�ƾ
        public IEnumerator CLightOn()
        {
            while (this.nowBatteryTime > 0)
            {
                this.nowBatteryTime--;

                this.uiFlashlight.batteries[2 - this.nowBattery].gameObject.SetActive(false);
                this.uiFlashlight.batteries[3 - this.nowBattery].gameObject.SetActive(true);

                this.nowBattery--;

                if (this.nowBattery <= 0 && this.nowBatteryTime <= 0)
                {
                    this.hasBattery = false;
                    this.OnUse();
                    yield break;
                }

                Debug.Log(3 - this.nowBattery);
                this.nowBatteryTime = maxBatteryTime;

                //���͸��� �� ����� ��� ������ ���� ����
                Debug.LogFormat("<color=yellow>nowBatery{0}</color>", this.nowBattery);

                yield return onesec;
            }
            yield break;
        }
        */
    }
}
