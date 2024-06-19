using System.Collections;
using System.Collections.Generic;
using SeongMin;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Jaewook
{
    public class MapScroll : ItemObject, IItem
    {
        public RawImage mapUI;

        private void Start()
        {
            base.Start();
            
            mapUI = GetComponentInChildren<RawImage>();

            if (mapUI != null)
            {
                mapUI.enabled = false;
            }
            else
            {
                Debug.LogError("Map UI ����");
            }
        }

        public void OnGrab()
        {
            if (mapUI != null)
            {
                mapUI.enabled = true;
                Debug.Log("���� ����, UI Ȱ��ȭ");
            }
        }

        public void OnUse()
        {
            // �ʿ�� �߰� ��� ����
        }

        public void OnRelease()
        {
            if (mapUI != null)
            {
                mapUI.enabled = false;
            }
        }
    }
}
