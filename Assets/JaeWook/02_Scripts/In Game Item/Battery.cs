using NHR;
using SeongMin;
using UnityEngine;
using System.Collections.Generic;

namespace Jaewook
{
    /// <summary>
    /// flashlight - battery ����
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
            // ���͸� ���� �Ĺ��͸� ������Ʈ ����
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
