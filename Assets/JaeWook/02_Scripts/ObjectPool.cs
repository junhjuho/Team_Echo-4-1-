using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Singleton - ObjectPool
/// ���������� ������ �ʿ��� ��� -> singleton Pool ���
/// </summary>
public class ObjectPool : MonoBehaviour
{
    private static ObjectPool instance; // ���� ������
    public static ObjectPool Instance
    {
        get
        {
            if(instance == null) // ���������Ͱ� ������ ObjectPoolŸ���� ������Ʈ ����
            {
                var obj = FindObjectOfType<ObjectPool>(); // * Find- �Լ� -> Update()���� ��� X
                
                if (obj != null) // ������ ������Ʈ�� ���������ͷ� ����
                {
                    instance = obj; 
                }
                else // null�̶�� ���ο� ������Ʈ ���� -> ObjectPoolŸ�� ���� -> �������� ����
                {
                    var newObj = new ObjectPool();
                    newObj.AddComponent<ObjectPool>();

                    instance = newObj.GetComponent<ObjectPool>();
                }
            }

            return instance;
        }
    }

    void Awake()
    {
        if(instance != null )
        {
            instance = this; // ����� ������Ʈ�� �������� �ְ�
            DontDestroyOnLoad( this.gameObject ); // ���� �Ұ����ϰ� ����

        }
        else
        {
            Destroy( this.gameObject ); // null�̸� ����
        }
    }

    public Queue<GameObject> _queues = new Queue<GameObject>();
    public Queue<GameObject> queues
    {
        get => _queues;
        set => _queues = value;
    }

    public GameObject prefabs;
    public int amount = 15;
    public Transform parentTF;

    void Start()
    {
        CreatePool();
    }

    //void Update()
    //{
    //    if (Input.GetMouseButton(0))
    //    {
    //        OnDequeue();
    //    }  
    //}

    private void CreatePool()
    {
        Debug.Log($"Create Pool ");
        for(int i = 0; i < amount; i++)
        {
            var newObj = Instantiate(prefabs);
            OnInit(newObj);
            
        }
    }

    /// <summary>
    /// ������ obj �ʱ�ȭ
    /// 1. ť�� �ֱ�
    /// 2. ��ġ, ���� 0���� ����
    /// 3. ���� GameObject�� ���� ( �׷�ȭ )
    /// 4. Active false ����
    /// </summary>
    /// <param name="obj"></param>
    public void OnInit(GameObject obj)
    {
        queues.Enqueue(obj);

        obj.transform.SetParent(parentTF);
        obj.transform.position = Vector3.zero;
        obj.transform.rotation = Quaternion.identity;

        obj.SetActive(false);
    }

    public GameObject OnDequeue()
    {
        if(queues.Count <= 10)
        {
            CreatePool();
        }

        GameObject obj = queues.Dequeue();
        obj.SetActive(true);

        return obj;
    }

}
