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
        public MeshRenderer scrollMeshRen;
        public Collider scrollCollider;

        public bool isGrab = false;

        private void Start()
        {
            base.Start();
            
            scrollMeshRen = GetComponent<MeshRenderer>();
            scrollCollider = GetComponent<Collider>();

            mapUI = GetComponentInChildren<RawImage>();

            /*
            if (mapUI != null)
            {
                mapUI.enabled = false;
            }
            else
            {
                Debug.LogError("Map UI ����");
            }
            */
        }

        public void OnGrab()
        {
            // ������ UI Ȱ��ȭ�ϰ� ��� mesh Renderer ���� 
            isGrab = true;

            /*
            if (isGrab)
            {
                this.scrollMeshRen.enabled = false;
                this.scrollCollider.enabled = false;

                if (mapUI != null)
                {
                    mapUI.enabled = true;
                    Debug.Log("���� ����, UI Ȱ��ȭ");
                }
                else
                {
                    Debug.LogError("������ ����, UI Ȱ�� �Ұ�");
                }
            }
            else
            {
                isGrab = false;
            }
            */
        }

        public void OnUse()
        {
            // �ʿ�� �߰� ��� ����
        }

        public void OnRelease()
        {
            /*
            if (mapUI != null)
            {
                mapUI.enabled = false;
                this.scrollMeshRen.enabled = true;
                this.scrollCollider.enabled = true;
            }
            */
        }
    }
}
