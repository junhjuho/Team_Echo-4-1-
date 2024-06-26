using Jaewook;
using Photon.Pun;
using SeongMin;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.UI;
using static SeongMin.ItemObject;

namespace SeongMin
{
    public class PlayerAction : XRRayInteractor
    {
        public PhotonView photonView;
        public PlayerMission playerMission;

        //팁
        private bool hasTipMap = true;
        private bool hasTipFinalKey = true;

        protected override void Start()
        {
            base.Start();
            photonView = GetComponent<PhotonView>();
            this.selectEntered.AddListener(SelectEvent);
            //this.hoverEntered.AddListener(HoverEvent);
            //this.hoverExited.AddListener(HoverEventExit);
            this.hasTipMap = true;
            this.hasTipFinalKey = true;
        }
        // 플레이어가 아이템을 잡았을 때,
        private void SelectEvent(SelectEnterEventArgs args)
        {
            Debug.Log(args.interactableObject.transform.name);
            //잡은 물체가 ItemObject 스크립트가 있는지 확인 후 _item 을 콜백으로 받아오기
            if (args.interactableObject.transform.TryGetComponent(out ItemObject _item) && _item.isFind == false)
            {
                Debug.Log("Get Item1 : " + args.interactableObject.transform.name);
                Debug.Log("Get Item2 : " + _item.charactorValue);

                playerMission = GameDB.Instance.playerMission;
                Debug.Log("Get Item3 : " + playerMission.isChaser);
                //_item.isFind = true;

                // 플레이어의 미션 배열에 _item 오브젝트와 일치하는 게 있으면 if문 안에 코드를 실행
                if (_item.charactorValue == CharactorValue.runner && playerMission.MissionItemCheck(_item.gameObject, playerMission.playerMissionArray))
                {
                    print("잡았습니다");
                    _item.isFind = true;
                    _item.gameObject.SetActive(false);
                    playerMission.runnerMissionClearCount++;
                    if (playerMission.runnerMissionClearCount >= 3)
                    {
                        //Test code
                        GameDB.Instance.Shuffle(GameManager.Instance.inGameMapManager.inGameItemPositionList);
                        GameObject key = PhotonNetwork.Instantiate("탈출구 열쇠", GameManager.Instance.inGameMapManager.inGameItemPositionList[0].position, Quaternion.identity);
                        // 탈출 지점 2개 생성하기
                        GameDB.Instance.Shuffle(GameDB.Instance.escapeDoorPositionList);
                        GameObject exitDoor1 = PhotonNetwork.Instantiate("EscapeDoor", GameDB.Instance.escapeDoorPositionList[0].position, Quaternion.identity);
                        GameObject exitDoor2 = PhotonNetwork.Instantiate("EscapeDoor", GameDB.Instance.escapeDoorPositionList[1].position, Quaternion.identity);
                        exitDoor1.SetActive(true);
                        exitDoor2.SetActive(true);
                        Debug.Log("키 생성");

                        key.transform.Find("MinimapIcon").gameObject.SetActive(true);
                        exitDoor1.transform.Find("MinimapIcon").gameObject.SetActive(true);
                        exitDoor2.transform.Find("MinimapIcon").gameObject.SetActive(true);
                        Debug.Log("미니맵 생성");
                    }
                    EventDispatcher.instance.SendEvent<string>((int)NHR.EventType.eEventType.Complete_Mission, _item.name);
                    GameManager.Instance.roundManager.currentRoundPlayersMissionCount++;
                    playerMission.AllPlayerMissionScoreUpdate();

                    //팁
                    if (this.hasTipMap)
                    {
                        EventDispatcher.instance.SendEvent<string>((int)NHR.EventType.eEventType.Popup_Tip, "Map");
                        this.hasTipMap = false;
                    }
                    else if (this.hasTipFinalKey && !this.playerMission.isChaser) 
                    {
                        EventDispatcher.instance.SendEvent<string>((int)NHR.EventType.eEventType.Popup_Tip, "FinalKey");
                        this.hasTipFinalKey = false;
                    }

                    //TODO 이 아이템 인벤토리에 넣기
                }
                // 내가 복수자 일 때만 복수자용 아이템 카운팅 하기
                else if (_item.charactorValue == CharactorValue.chaser
                    && playerMission.isChaser
                    && playerMission.MissionItemCheck(_item.gameObject, playerMission.chaserMissionArray))
                {
                    Debug.Log("Chaser : " + args.interactableObject.transform.name);
                    _item.isFind = true;
                    _item.gameObject.SetActive(false);
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

                //if (args.interactableObject.transform.TryGetComponent(out FlashLight _flash))
                //{
                //    GameDB.Instance.myFlashLight = _flash;
                //}

                MissionClearCheck();
            }
        }
        private void MissionClearCheck()
        {
            playerMission = GameDB.Instance.playerMission;

            Debug.Log("키 생성 시도");
            //이 클라이언트 플레이어가 복수자가 아니고, 일반 미션 클리어한 갯수가 일반 미션 배열의 길이와 같거나 높으면 실행
            if (GameDB.Instance.playerMission.isChaser == false &&
                GameDB.Instance.playerMission.runnerMissionClearCount >= GameDB.Instance.playerMission.playerMissionArray.Length)
            {
                Debug.Log("키 생성 시도2");
                //-----------TODO 테스트 코드 ------------------------------------
                // 파이털 키 생성하기
                GameDB.Instance.Shuffle(GameManager.Instance.inGameMapManager.inGameItemPositionList);
                    GameObject key = PhotonNetwork.Instantiate("탈출구 열쇠", GameManager.Instance.inGameMapManager.inGameItemPositionList[0].position, Quaternion.identity);
                    // 탈출 지점 2개 생성하기
                    GameDB.Instance.Shuffle(GameDB.Instance.escapeDoorPositionList);
                    GameObject exitDoor1 = PhotonNetwork.Instantiate("EscapeDoor", GameDB.Instance.escapeDoorPositionList[0].position, Quaternion.identity);
                    GameObject exitDoor2 = PhotonNetwork.Instantiate("EscapeDoor", GameDB.Instance.escapeDoorPositionList[1].position, Quaternion.identity);
                    exitDoor1.SetActive(true);
                    exitDoor2.SetActive(true);
                    Debug.Log("키 생성");

                    key.transform.Find("MinimapIcon").gameObject.SetActive(true);
                    exitDoor1.transform.Find("MinimapIcon").gameObject.SetActive(true);
                    exitDoor2.transform.Find("MinimapIcon").gameObject.SetActive(true);
                    Debug.Log("미니맵 생성");
                //------------------------------------------------------------------
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