using System.Collections;
using System.Collections.Generic;
using Jaewook;
using SeongMin;
using UnityEngine;

public class Intestine : ItemObject, IItem
{
    /*
    [Header("��ƼŬ ȿ��")]
    public ParticleSystem particleSys;
    [Header("üũ��")]
    public PlayerMission playerMission;
    */
    private void Start()
    {
        base.Start();
        /*
        playerMission = GameDB.Instance.playerMission;
        particleSys = GetComponentInChildren<ParticleSystem>();

        // ��ƼŬȿ�� ��� ����
        particleSys.Play();
        */
    }

    public void OnGrab()
    {
        /*
        // ������ ������Ʈ ��ü�� ���ֱ�
        gameObject.SetActive(false);
        playerMission.chaserMissionClearCount++;

        // ����
        if (playerMission.isChaser && playerMission.chaserMissionClearCount >= playerMission.chaserMissionArray.Length)
        {
            // �����ڷ� ĳ���� ����
            EventDispatcher.instance.SendEvent((int)NHR.EventType.eEventType.Change_Monster);
            // ī���� �ʱ�ȭ
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
