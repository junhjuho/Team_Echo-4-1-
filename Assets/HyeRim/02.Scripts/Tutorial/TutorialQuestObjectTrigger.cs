using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NHR
{
    public class TutorialQuestObjectTrigger : MonoBehaviour, ITutorialQuestObject
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log("Player in");
                this.SendEventClear();
                //this.gameObject.SetActive(false);
            }
        }
        public void SendEventClear()
        {
            EventDispatcher.instance.SendEvent((int)NHR.EventType.eEventType.Clear_TutorialQuest);
        }

    }

}
