using SeongMin;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using static SeongMin.ItemObject;

namespace SeongMin
{
    public class PlayerAction : XRGrabInteractable
    {
        PlayerMission playerMission;
        private void Start()
        {
            playerMission = GetComponent<PlayerMission>();
            this.activated.AddListener(ActivedEvent);
        }
        // �÷��̾ �������� ����� ��,
        private void ActivedEvent(ActivateEventArgs args)
        {
            //���� ��ü�� ItemObject ��ũ��Ʈ�� �ִ��� Ȯ�� �� _item �� �ݹ����� �޾ƿ���
            if (args.interactableObject.transform.TryGetComponent(out ItemObject _item))
            {
                // �÷��̾��� �̼� �迭�� _item ������Ʈ�� ��ġ�ϴ� �� ������ if�� �ȿ� �ڵ带 ����
                if ( _item.charactorValue == CharactorValue.runner && _item.isFind == false &&
                    playerMission.MissionItemCheck(_item.gameObject, playerMission.playerMissionArray))
                {
                    _item.isFind = true;
                    playerMission.runnerMissionClearCount++;
                    //TODO �� ������ �κ��丮�� �ֱ�
                }
                else if (_item.charactorValue == CharactorValue.chaser && _item.isFind == false
                    && playerMission.MissionItemCheck(_item.gameObject, playerMission.chaserMissionArray))
                {
                    _item.isFind = true;
                    playerMission.chaserMissionClearCount++;
                }
                else if (_item.itemValue == ItemValue.teamPlay && _item.isFind == false
                    && playerMission.MissionItemCheck(_item.gameObject, playerMission.playerTeamPlayMissionArray))
                {
                    _item.isFind = true;
                    playerMission.playerTeamPlayMissionCount++;
                }
                MissionClearCheck();
            }
        }
        private void MissionClearCheck()
        {

            if(playerMission.isChaser == false)
            {
                if (playerMission.runnerMissionClearCount >= playerMission.playerMissionArray.Length)
                {
                    //TODO �̼� Ŭ���� ����
                }
            }
            else
            {
                if (playerMission.chaserMissionClearCount >= playerMission.chaserMissionArray.Length)
                {
                    //TODO �����ϱ� �����ڰ� ������ ���� ������ ��� ����� ��, ��� �÷��̾��� ������ ��ġ �ʱ�ȭ
                    EventDispatcher.instance.SendEvent((int)NHR.EventType.eEventType.Change_Monster);
                    GameManager.Instance.inGameMapManager.PlayerPositionSetting();
                }
            }
        }
    }

}