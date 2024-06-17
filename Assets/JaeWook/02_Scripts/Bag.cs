using NHR;
using SeongMin;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 그냥 오브젝트로 제작하기로 결정 
/// </summary>
public class Bag : ItemObject, Jaewook.IItem
{
    [Header("가방 프리펩")]
    public GameObject inventoryObjectPrefab;

    private void Start()
    {
        base.Start();

    }

    public void OnGrab()
    {
        /*
        // Bag 오브젝트를 비활성화하고 인벤토리 UI를 활성화합니다.
        if (this.gameObject != null)
        {
            // enableInventoryObject.gameObject.SetActive(true);  // UI 활성화 ( 현재 없으므로 오류 뜸 )

            
        }
        */
    }

    public void OnRelease()
    {
        // 필요한 경우 OnRelease 구현
    }

    public void OnUse()
    {
       // 
    }
}
