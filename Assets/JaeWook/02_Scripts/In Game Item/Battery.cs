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
        public void OnGrab()
        {
            // ���� �ʿ�
            FlashLight flashlight = FindObjectOfType<FlashLight>();

            // SeoungMin.ItemObject.cs ->public bool isFind = false;
            this.isFind = true;

            if (isFind && (flashlight != null))
            {
                flashlight.ChargeFlashlight();
            }
            // ���͸� ���� �Ĺ��͸� ������Ʈ ����
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
