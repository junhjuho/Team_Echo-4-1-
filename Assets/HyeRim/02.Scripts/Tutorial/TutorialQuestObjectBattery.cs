using Jaewook;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialQuestObjectBattery : Battery, ITutorialQuestObject
{

    public void SendEventClear()
    {
        EventDispatcher.instance.SendEvent((int)NHR.EventType.eEventType.Clear_TutorialQuest);
    }

    public override void OnGrab()
    {
        this.SendEventClear();
    }
}
