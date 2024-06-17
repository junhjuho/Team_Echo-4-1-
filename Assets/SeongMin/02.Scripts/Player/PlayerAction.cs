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
        // 플레이어가 아이템을 잡았을 때,
        private void ActivedEvent(ActivateEventArgs args)
        {
            //잡은 물체가 ItemObject 스크립트가 있는지 확인 후 _item 을 콜백으로 받아오기
            if (args.interactableObject.transform.TryGetComponent(out ItemObject _item))
            {
                // 플레이어의 미션 배열에 _item 오브젝트와 일치하는 게 있으면 if문 안에 코드를 실행
                if ( _item.charactorValue == CharactorValue.runner && _item.isFind == false &&
                    playerMission.MissionItemCheck(_item.gameObject, playerMission.playerMissionArray))
                {
                    _item.isFind = true;
                    playerMission.runnerMissionClearCount++;
                    //TODO 이 아이템 인벤토리에 넣기
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
                    //TODO 미션 클리어 띄우기
                }
            }
            else
            {
                if (playerMission.chaserMissionClearCount >= playerMission.chaserMissionArray.Length)
                {
                    //TODO 변신하기 복수자가 변신을 위한 아이템 모두 모았을 시, 즉시 플레이어들과 복수자 위치 초기화
                    EventDispatcher.instance.SendEvent((int)NHR.EventType.eEventType.Change_Monster);
                    GameManager.Instance.inGameMapManager.PlayerPositionSetting();
                }
            }
        }
    }

}