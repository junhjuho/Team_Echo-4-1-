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
        FlashLight flashlight;
        public void OnGrab()
        {
            flashlight = GameDB.Instance.myFlashLight;
            // SeoungMin.ItemObject.cs ->public bool isFind = false;
            base.isFind = true;

            if (isFind && (flashlight != null))
            {
                flashlight.ChargeFlashlight();
            }
            // 배터리 충전 후배터리 오브젝트 삭제
            gameObject.SetActive(false);
            base.isFind = false;
        }

        public void OnUse()
        {

        }

        public void OnRelease()
        {

        }
    }
}
