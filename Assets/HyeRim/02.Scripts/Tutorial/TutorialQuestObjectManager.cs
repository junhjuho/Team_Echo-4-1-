using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NHR
{
    public class TutorialQuestObjectManager : MonoBehaviour
    {
        public TutorialQuestObjectTrigger[] questObjects;

        private void Awake()
        {
            this.questObjects = GetComponentsInChildren<TutorialQuestObjectTrigger>();
            //모든 오브젝트 비활성화
            foreach (var obj in questObjects)
            {
                obj.gameObject.SetActive(false);
            }
        }
    }

}
