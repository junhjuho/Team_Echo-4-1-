using System.Collections;
using System.Collections.Generic;
using Jaewook;
using SeongMin;
using UnityEngine;

public class Intestine : ItemObject, IItem
{
    /*
    [Header("파티클 효과")]
    public ParticleSystem particleSys;
    [Header("체크용")]
    public PlayerMission playerMission;
    */
    private void Start()
    {
        base.Start();
        /*
        playerMission = GameDB.Instance.playerMission;
        particleSys = GetComponentInChildren<ParticleSystem>();

        // 파티클효과 상시 유지
        particleSys.Play();
        */
    }

    public void OnGrab()
    {
        /*
        // 잡으면 오브젝트 자체를 없애기
        gameObject.SetActive(false);
        playerMission.chaserMissionClearCount++;

        // 변신
        if (playerMission.isChaser && playerMission.chaserMissionClearCount >= playerMission.chaserMissionArray.Length)
        {
            // 복수자로 캐릭터 변경
            EventDispatcher.instance.SendEvent((int)NHR.EventType.eEventType.Change_Monster);
            // 카운팅 초기화
            playerMission.chaserMissionClearCount = 0;
        }
        */
    }

    public void OnRelease()
    {
        throw new System.NotImplementedException();
    }

    public void OnUse()
    {
        throw new System.NotImplementedException();
    }
}
