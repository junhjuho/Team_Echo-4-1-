using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NHR
{
    public class TutorialQuestObjectManager : MonoBehaviour
    {
        public TutorialQuestObject[] questObjects;

        private void Awake()
        {
            this.questObjects = GetComponentsInChildren<TutorialQuestObject>();
            //��� ������Ʈ ��Ȱ��ȭ
            foreach (var obj in questObjects)
            {
                obj.gameObject.SetActive(false);
            }
        }
    }

}
