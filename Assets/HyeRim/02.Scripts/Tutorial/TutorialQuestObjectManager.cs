using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NHR
{
    public class TutorialQuestObjectManager : MonoBehaviour
    {
        public GameObject[] questObjects;
        public GameObject[] removeObjects;

        private void Awake()
        {
            //this.questObjects = GetComponentsInChildren<TutorialQuestObjectTrigger>();
            //��� ������Ʈ ��Ȱ��ȭ
            foreach (var obj in questObjects)
            {
                obj.gameObject.SetActive(false);
            }
            foreach (var obj in removeObjects)
            {
                obj.gameObject.SetActive(false);
            }
        }
    }

}
