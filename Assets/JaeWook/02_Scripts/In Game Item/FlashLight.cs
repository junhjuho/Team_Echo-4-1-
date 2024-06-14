using System.Collections;
using System.Collections.Generic;
using SeongMin;
using UnityEngine;

namespace Jaewook
{
    public class FlashLight : ItemObject, IItem
    {
        public bool isOn = false;
        public Light flashlight;

        void Start()
        {
            // ItemObject.Start(); // ItemObject�� Start �޼��带 ȣ���Ͽ� ���� ĳ���Ϳ� ���� ���
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
            // Ʈ���� ��ư�� ������ �� �÷��ö���Ʈ �ѱ�/����
            isOn = !isOn;
            flashlight.enabled = isOn;

            //Debug.Log("Flashlight Used: " + (isOn ? "On" : "Off"));
        }

        public void OnRelease()
        {
            
            //Debug.Log("Flashlight Released");
        }
    }
}
