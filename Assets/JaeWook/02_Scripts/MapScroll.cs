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
                Debug.LogError("Map UI 없음");
            }
            */
        }

        public void OnGrab()
        {
            // 잡으면 UI 활성화하고 잠깐 mesh Renderer 삭제 
            isGrab = true;

            /*
            if (isGrab)
            {
                this.scrollMeshRen.enabled = false;
                this.scrollCollider.enabled = false;

                if (mapUI != null)
                {
                    mapUI.enabled = true;
                    Debug.Log("지도 잡음, UI 활성화");
                }
                else
                {
                    Debug.LogError("지도가 없음, UI 활성 불가");
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
            // 필요시 추가 기능 구현
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
