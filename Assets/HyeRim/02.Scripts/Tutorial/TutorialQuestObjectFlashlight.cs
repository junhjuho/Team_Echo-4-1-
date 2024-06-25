using Jaewook;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialQuestObjectFlashlight : FlashLight, ITutorialQuestObject
{
    public bool isGrabDone;
    public bool isUseDone;

    public GameObject canvas;

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
        this.canvas.SetActive(false);
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
