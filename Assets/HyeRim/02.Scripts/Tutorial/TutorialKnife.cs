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
            GameManager.Instance.lobbySceneManager.playerMission.chaserPrefab.SetActive(true);
            GameManager.Instance.lobbySceneManager.playerMission.currentRunnerCharacrer.gameObject.SetActive(false);
            EventDispatcher.instance.SendEvent((int)NHR.EventType.eEventType.Clear_TutorialQuest);
            this.isMissionDone = true;
        }
    }

    public void OnRelease()
    {

    }

    public void OnUse()
    {

    }

}
