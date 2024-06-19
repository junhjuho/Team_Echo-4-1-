using System.Collections;
using System.Collections.Generic;
using SeongMin;
using UnityEngine;

namespace Jaewook
{
    public class FlashLight : ItemObject, IItem
    {
        public bool isOn = true;
        public Light flashlight;
        

        //NHR
        [Header("손전등 UI")]
        public NHR.UIFlashlight uiFlashlight;
        //코루틴
        private IEnumerator flashCoroutine;

        //배터리 값
        public bool hasBattery = true;
        public int nowBatteryTime = 20;
        public int nowBattery = 20;
        //max 값
        public int maxBatteryTime = 30;
        public int maxBattery = 30;

        private void Awake()
        {
            this.uiFlashlight = this.GetComponentInChildren<NHR.UIFlashlight>();
            this.Init();
            StartCoroutine(this.flashCoroutine);

            GameDB.Instance.myFlashLight = flashlight;
        }
        //초기 설정
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
        protected void Start()
        {
            base.Start(); // ItemObject의 Start 메서드를 호출하여 씬과 캐릭터에 따라 등록
        }

        public void OnGrab()
        {
            
            
        }

        public void OnUse()
        {
            if(this.hasBattery)
            {
                // 트리거 버튼을 눌렀을 때 플래시라이트 켜기/끄기
                isOn = !isOn;
                flashlight.enabled = isOn;

                //손전등 배터리
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
        /// 손전등 충전
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


        //손전등 사용 코루틴
        public IEnumerator CLightOn()
        {
            while (true)
            {
                //Debug.LogFormat("nowBattery time : {0}", this.nowBatteryTime);
                this.nowBatteryTime--;

                //배터리 한 칸 시간이 다 되었을 경우
                if (this.nowBatteryTime < 0)
                {
                    this.uiFlashlight.batteries[3 - this.nowBattery].gameObject.SetActive(false);
                    this.uiFlashlight.batteries[4 - this.nowBattery].gameObject.SetActive(true);

                    Debug.Log(3 - this.nowBattery);
                    this.nowBatteryTime = 3;
                    this.nowBattery--;

                    //배터리가 다 닳았을 경우 손전등 강제 종료
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
