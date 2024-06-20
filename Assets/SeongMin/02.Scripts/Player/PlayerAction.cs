using Jaewook;
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
        public PlayerMission playerMission;
        protected override void Start()
        {
            base.Start();
            photonView = GetComponent<PhotonView>();
            this.selectEntered.AddListener(SelectEvent);
        }
        // 플레이어가 아이템을 잡았을 때,
        private void SelectEvent(SelectEnterEventArgs args)
        {
            //잡은 물체가 ItemObject 스크립트가 있는지 확인 후 _item 을 콜백으로 받아오기
            if (args.interactableObject.transform.TryGetComponent(out ItemObject _item) && _item.isFind == false)
            {
                playerMission = GameDB.Instance.playerMission;

                //_item.isFind = true;

                // 플레이어의 미션 배열에 _item 오브젝트와 일치하는 게 있으면 if문 안에 코드를 실행
                if (_item.charactorValue == CharactorValue.runner && playerMission.MissionItemCheck(_item.gameObject, playerMission.playerMissionArray))
                {
                    print("잡았습니다");
                    _item.isFind = true;
                    playerMission.runnerMissionClearCount++;
                    EventDispatcher.instance.SendEvent<string>((int)NHR.EventType.eEventType.Complete_Mission, _item.name);
                    GameManager.Instance.roundManager.currentRoundPlayersMissionCount++;
                    playerMission.AllPlayerMissionScoreUpdate();
                    //TODO 이 아이템 인벤토리에 넣기
                }
                // 내가 복수자 일 때만 복수자용 아이템 카운팅 하기
                else if (_item.charactorValue == CharactorValue.chaser
                    && playerMission.isChaser
                    && playerMission.MissionItemCheck(_item.gameObject, playerMission.chaserMissionArray))
                {
                    _item.isFind = true;
                    playerMission.chaserMissionClearCount++;
                }
                // 내가 팀 플레이 미션이 있을 때만 팀플레이용 아이템 카운팅 하기
                else if (_item.itemValue == ItemValue.teamPlay
                    && playerMission.isTeamMission
                    && playerMission.MissionItemCheck(_item.gameObject, playerMission.playerTeamPlayMissionArray))
                {
                    _item.isFind = true;
                    playerMission.playerTeamPlayMissionCount++;
                }

                if (args.interactableObject.transform.TryGetComponent(out FlashLight _flash))
                {
                    GameDB.Instance.myFlashLight = _flash;
                }

                MissionClearCheck();
            }
        }
        private void MissionClearCheck()
        {
            playerMission = GameDB.Instance.playerMission;
            //이 클라이언트 플레이어가 복수자가 아니고, 일반 미션 클리어한 갯수가 일반 미션 배열의 길이와 같거나 높으면 실행
            if (playerMission.isChaser == false &&
                playerMission.runnerMissionClearCount >= playerMission.playerMissionArray.Length)
            {
                //TODO 미션 클리어 띄우기
            }
            // 이 클라이언트가 복수자이며, 복수자 미션 클리어 갯수가 복수자 미션 배열의 길이와 같거나 높으면 실행
            if (playerMission.isChaser && playerMission.chaserMissionClearCount >= playerMission.chaserMissionArray.Length)
            {
                // 복수자로 캐릭터 변경
                EventDispatcher.instance.SendEvent((int)NHR.EventType.eEventType.Change_Monster);
                // 카운팅 초기화
                playerMission.chaserMissionClearCount = 0;
            }
        }
    }

}