using NHR;
using SeongMin;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// �׳� ������Ʈ�� �����ϱ�� ���� 
/// </summary>
public class Bag : ItemObject, Jaewook.IItem
{
    [Header("���� ������")]
    public GameObject inventoryObjectPrefab;

    private void Start()
    {
        base.Start();

    }

    public void OnGrab()
    {
        /*
        // Bag ������Ʈ�� ��Ȱ��ȭ�ϰ� �κ��丮 UI�� Ȱ��ȭ�մϴ�.
        if (this.gameObject != null)
        {
            // enableInventoryObject.gameObject.SetActive(true);  // UI Ȱ��ȭ ( ���� �����Ƿ� ���� �� )

            
        }
        */
    }

    public void OnRelease()
    {
        // �ʿ��� ��� OnRelease ����
    }

    public void OnUse()
    {
       // 
    }
}
