using NHR;
using SeongMin;
using UnityEngine;
using System.Collections.Generic;

namespace Jaewook
{
    /// <summary>
    /// flashlight - battery 연동
    /// </summary>
    public class Battery : ItemObject, IItem
    {
        public void OnGrab()
        {
            // 수정 필요
            FlashLight flashlight = FindObjectOfType<FlashLight>();

            // SeoungMin.ItemObject.cs ->public bool isFind = false;
            this.isFind = true;

            if (isFind && (flashlight != null))
            {
                flashlight.ChargeFlashlight();
            }
            // 배터리 충전 후배터리 오브젝트 삭제
            gameObject.SetActive(false);
            this.isFind = false;
        }

        public void OnUse()
        {
            
        }

        public void OnRelease()
        {
            
        }
    }
}
