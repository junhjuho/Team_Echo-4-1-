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
    public class PlayerAction : XRRayInteractor
    {
        PhotonView photonView;
        PlayerMission playerMission;
        private void Start()
        {
            base.Start();
            photonView = GetComponent<PhotonView>();
            playerMission = GameDB.Instance.playerMission;
            this.selectEntered.AddListener(ActivedEvent);
        }
        // 플레이어가 아이템을 잡았을 때,
        private void ActivedEvent(SelectEnterEventArgs args)
        {
            //잡은 물체가 ItemObject 스크립트가 있는지 확인 후 _item 을 콜백으로 받아오기
            if (args.interactableObject.transform.TryGetComponent(out ItemObject _item) && _item.isFind == false)
            {
                // 플레이어의 미션 배열에 _item 오브젝트와 일치하는 게 있으면 if문 안에 코드를 실행
                if (_item.charactorValue == CharactorValue.runner &&
                    playerMission.MissionItemCheck(_item.gameObject, playerMission.playerMissionArray))
                {
                    _item.isFind = true;
                    playerMission.runnerMissionClearCount++;
                    //TODO 이 아이템 인벤토리에 넣기
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
                //TODO 미션 클리어 띄우기
            }
            if (playerMission.isChaser && playerMission.chaserMissionClearCount >= playerMission.chaserMissionArray.Length)
            {
                EventDispatcher.instance.SendEvent((int)NHR.EventType.eEventType.Change_Monster);
                GameManager.Instance.inGameMapManager.PlayerPositionSetting();
            }
        }
    }

}