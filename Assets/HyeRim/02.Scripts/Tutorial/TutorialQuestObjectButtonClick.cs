using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialQuestObjectButtonClick : MonoBehaviour, ITutorialQuestObject
{
    public void SendEventClear()
    {
        EventDispatcher.instance.SendEvent((int)NHR.EventType.eEventType.Clear_TutorialQuest);
    }

}
