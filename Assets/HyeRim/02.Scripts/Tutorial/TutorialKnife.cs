using Jaewook;
using SeongMin;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialKnife : MonoBehaviour, IItem
{
    public bool isMissionDone = false;
    public void OnGrab()
    {
        if (!this.isMissionDone)
        {
            GameDB.Instance.playerMission.chaserPrefab.SetActive(true);
            foreach (var character in GameDB.Instance.playerMission.currentRunnerCharacrers) character.gameObject.SetActive(false);
            EventDispatcher.instance.SendEvent((int)NHR.EventType.eEventType.Clear_TutorialQuest);
            this.isMissionDone = true;
        }
        //EventDispatcher.instance.SendEvent((int)NHR.EventType.eEventType.Change_Monster);
    }

    public void OnRelease()
    {

    }

    public void OnUse()
    {

    }

}
