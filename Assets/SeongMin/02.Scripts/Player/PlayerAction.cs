using Photon.Pun;
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
        PhotonView photonView;
        PlayerMission playerMission;
        private void Start()
        {
            photonView = GetComponent<PhotonView>();
            playerMission = GetComponent<PlayerMission>();
            this.activated.AddListener(ActivedEvent);
        }
        // �÷��̾ �������� ����� ��,
        private void ActivedEvent(ActivateEventArgs args)
        {
            //���� ��ü�� ItemObject ��ũ��Ʈ�� �ִ��� Ȯ�� �� _item �� �ݹ����� �޾ƿ���
            if (args.interactableObject.transform.TryGetComponent(out ItemObject _item) && _item.isFind == false)
            {
                // �÷��̾��� �̼� �迭�� _item ������Ʈ�� ��ġ�ϴ� �� ������ if�� �ȿ� �ڵ带 ����
                if (_item.charactorValue == CharactorValue.runner &&
                    playerMission.MissionItemCheck(_item.gameObject, playerMission.playerMissionArray))
                {
                    _item.isFind = true;
                    playerMission.runnerMissionClearCount++;
                    //TODO �� ������ �κ��丮�� �ֱ�
                }
                else if (_item.charactorValue == CharactorValue.chaser
                    && playerMission.isChaser
                    && playerMission.MissionItemCheck(_item.gameObject, playerMission.chaserMissionArray))
                {
                    _item.isFind = true;
                    playerMission.chaserMissionClearCount++;
                }
                else if (_item.itemValue == ItemValue.teamPlay
                    && playerMission.isTeamMission
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
            if (playerMission.isChaser == false &&
                playerMission.runnerMissionClearCount >= playerMission.playerMissionArray.Length)
            {
                //TODO �̼� Ŭ���� ����
            }
            if (playerMission.isChaser && playerMission.chaserMissionClearCount >= playerMission.chaserMissionArray.Length)
            {
                EventDispatcher.instance.SendEvent((int)NHR.EventType.eEventType.Change_Monster);
                GameManager.Instance.inGameMapManager.PlayerPositionSetting();
            }
        }
    }

}