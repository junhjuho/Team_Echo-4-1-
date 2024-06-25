using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NHR
{
    public class TutorialQuestObjectManager : MonoBehaviour
    {
        [Header("Ʃ�丮�� ����Ʈ ������Ʈ �迭")]
        public GameObject[] questObjects;

        public void Awake()
        {
            //this.questObjects = GetComponentsInChildren<TutorialQuestObjectTrigger>();
            //��� ������Ʈ ��Ȱ��ȭ
            foreach (var obj in questObjects)
            {
                obj.gameObject.SetActive(false);
            }
        }
    }

}
