using Jaewook;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialQuestObjectFlashlight : FlashLight, ITutorialQuestObject
{
    public bool isGrabDone;
    public bool isUseDone;

    private void Awake()
    {
        this.isGrabDone = false;
        this.isUseDone = false;
    }

    public void SendEventClear()
    {
        EventDispatcher.instance.SendEvent((int)NHR.EventType.eEventType.Clear_TutorialQuest);
    }

    public override void OnGrab()
    {
        if (!isGrabDone)
        {
            this.isGrabDone = true;
            this.SendEventClear();
        }
    }
    public override void OnUse()
    {
        if(!isUseDone)
        {
            this.isUseDone = true;
            this.SendEventClear();
        }
    }

}
