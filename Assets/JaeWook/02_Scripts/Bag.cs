using NHR;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �׳� ������Ʈ�� �����ϱ�� ���� 
/// </summary>
public class Bag : MonoBehaviour, Jaewook.IItem
{
    [Header("���� ������")]
    public GameObject inventoryObjectPrefab;

    private void Start()
    {
        // �κ��丮 UI ( ���� )
        if (inventoryObjectPrefab != null)
        {
            // pos, rot �߰����� �ʿ� -> ���δ� �ǵ�� : �����۸Ŵ��� ���� ( pos,rot ���� )
            GameObject inventoryObjectInstance = Instantiate(inventoryObjectPrefab, transform.position, transform.rotation);
        }
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
