using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NHR
{
    public class TutorialQuestObjectManager : MonoBehaviour
    {
        [Header("튜토리얼 퀘스트 오브젝트 배열")]
        public GameObject[] questObjects;

        public void Awake()
        {
            //this.questObjects = GetComponentsInChildren<TutorialQuestObjectTrigger>();
            //모든 오브젝트 비활성화
            foreach (var obj in questObjects)
            {
                obj.gameObject.SetActive(false);
            }
        }
    }

}
