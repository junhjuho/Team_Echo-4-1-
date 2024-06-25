using NHR;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static NHR.App;
using static SeongMin.RoundManager;
using static UnityEngine.Rendering.DebugUI;

namespace SeongMin
{
    public class RoundManager : MonoBehaviour
    {
        [Header("맵 세팅 준비완료 여부")]
        public bool isMapSettingDone = false;
        [Header("모든 플레이어 연결 완료 여부")]
        public bool isPlayerAllConnected = false;
        [Header("연결된 플레이어  수")]
        public int playerCount = 0;
        [Header("현재 완료된 플레이어 미션 수")]
        public int currentRoundPlayersMissionCount = 0;
        [Header("현재 라운드 미션 전체 진행율")]
        public int currentRoundPlayersMissionPerSent = 0;
        [Header("목표 진행률 설정하기")]
        public int needPersent = 60;

        public PhotonView photonView;

        private WaitForSeconds popupTime = new WaitForSeconds(8f);

        private void Awake()
        {
            GameManager.Instance.roundManager = this;
            photonView = GetComponent<PhotonView>();
        }
        // 1라운드 세팅 
        private IEnumerator Start()
        {
            // 나의 클라이언트가 네트워크에 연결될때까지 기달리기
            yield return new WaitUntil(() => PhotonNetwork.IsConnected);
            //내 플레이어 생성
            var _player = PhotonNetwork.Instantiate("Player", Vector3.up, Quaternion.identity);
            GameManager.Instance.playerManager.playerController = _player.GetComponent<PlayerController>();
            // 1라운드 세팅
            StartCoroutine(RoundOneSetting());
        }

        private IEnumerator RoundOneSetting()
        {
            // 방장이면 맵 세팅 실행
            if (PhotonNetwork.IsMasterClient)
                RoundMapSetting();
            // 내 클라이언트의 공용 데이터 초기화 하기
            InGamePublicDataReset();
            // 내가 들어 왔음을 알리기
            photonView.RPC("CountPlayer", RpcTarget.MasterClient);
            if (PhotonNetwork.IsMasterClient)
            {
                // 모든 플레이어 연결 했으면
                yield return new WaitUntil(() => isPlayerAllConnected);
                // 특정 플레이어에게 역할 세팅하기
                ChaserSetting();
                // 특정 플레이어에게 팀미션 담당하게 하기
                TeamMissionSetting();
                // 모든 플레이어에게 개인 미션 세팅하기
                MissionSetting();
                // 전체 플레이어 위치 세팅하기
                GameManager.Instance.inGameMapManager.PlayerPositionSetting();
                // 전체 플레이어에게 세팅 완료 알리기
                photonView.RPC("IsMapSettingDone", RpcTarget.All);
            }
            // 내 클라이언트가 방장이 세팅을 다 할 때까지 기다리기
            yield return new WaitUntil(() => isMapSettingDone);
            // 역할 안내 UI
            RoleSettingEvent();
            //라운드 시작 UI
            EventDispatcher.instance.SendEvent<string>((int)NHR.EventType.eEventType.Notice_EventUI, "roundStart");
            // 라운드 타이머 시작
            GameManager.Instance.roundTimer.TimerStart();

            //초기 Tips
            EventDispatcher.instance.SendEvent<string>((int)NHR.EventType.eEventType.Popup_Tip, "Move");
            yield return this.popupTime;
            EventDispatcher.instance.SendEvent<string>((int)NHR.EventType.eEventType.Popup_Tip, "Grab");

            yield break;
        }
        private void RoleSettingEvent()
        {
            //역할 안내 UI
            if (GameDB.Instance.playerMission.isChaser) EventDispatcher.instance.SendEvent<string>((int)NHR.EventType.eEventType.Notice_Role, "Chaser");
            else EventDispatcher.instance.SendEvent<string>((int)NHR.EventType.eEventType.Notice_Role, "Runner");
        }

        private void RoundMapSetting()
        {
            GameManager.Instance.inGameMapManager.ItemPositionSetting();
        }
        private void InGamePublicDataReset()
        {
            // 이름순으로 정렬 (모든 클라이언트에게 같은 정렬로 된 리스트 가지고 있게 하기 위해)
            GameManager.Instance.inGameMapManager.inGameRunnerItemList.Sort((a, b) => a.name.CompareTo(b.name));
            // 아이템 넘버링 리스트 초기화
            GameManager.Instance.inGameMapManager.ItemNumberListSetting();
        }
        private void MissionSetting()
        {
            GameManager.Instance.missionManager.MissionSetting();
        }

        [PunRPC] //모든 클라이언트가 받는 곳
        public void IsMapSettingDone()
        {
            isMapSettingDone = true;
        }
        private void ChaserSetting()
        {
            int randNumber = Random.Range(0, PhotonNetwork.PlayerList.Length);
            Player _player = PhotonNetwork.PlayerList[randNumber];
            photonView.RPC("ImChaser", _player);
        }
        [PunRPC] // 랜덤으로 선정된 플레이어만 받는 곳
        public void ImChaser()
        {
            GameDB.Instance.playerMission.isChaser = true;
        }
        private void TeamMissionSetting()
        {
            int randNumber = Random.Range(0, PhotonNetwork.PlayerList.Length);
            Player _player = PhotonNetwork.PlayerList[randNumber];
            photonView.RPC("GetTeamMission", _player);
        }
        [PunRPC] // 랜덤으로 선정된 플레이어가 받는 곳
        public void GetTeamMission()
        {
            GameDB.Instance.playerMission.isTeamMission = true;
        }
        [PunRPC] // 방장만 받는 곳
        public void CountPlayer()
        {
            playerCount++;
            if (playerCount == GameDB.Instance.playerCount)
                isPlayerAllConnected = true;
        }

        public void RPCSendScoreUpdate(int _value)
        {
            if (currentRoundPlayersMissionPerSent >= needPersent)
            {
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
            }
            else // 그게 아니라면, 모든 플레이어에게 전체 미션 진행도 공유하기
            {
                photonView.RPC("UpdateAllPlayerMissionPersent", RpcTarget.All, _value);
            }
        }

        [PunRPC]
        public void UpdateAllPlayerMissionPersent(int _value)
        {
            //필요 퍼센트의 1/4, 2/4, 3/4 4/4마다 UI 안내
            int quater = this.needPersent / 4;
            if (_value >= needPersent) EventDispatcher.instance.SendEvent((int)NHR.EventType.eEventType.Complete_RoundMission);
            //else if (_value % quater == 0) EventDispatcher.instance.SendEvent<int>((int)NHR.EventType.eEventType.Notice_TotalMissionPercent, _value);
            else
            {
                string str = string.Format("{0}/{1}", GameDB.Instance.playerMission.runnerMissionClearCount, GameDB.Instance.playerMission.playerMissionArray.Length);
                EventDispatcher.instance.SendEvent<string>((int)NHR.EventType.eEventType.Notice_TotalMissionPercent, str);
            }

        }

        // 내 라운드 데이터 초기화하기
        public void RoundPlayerDataReset()
        {
            // 개인미션 초기화
            for (int i = 0; i < GameDB.Instance.playerMission.playerMissionArray.Length; i++)
                GameDB.Instance.playerMission.playerMissionArray[i] = null;
            // 팀 미션 초기화
            for (int i = 0; i < GameDB.Instance.playerMission.playerTeamPlayMissionArray.Length; i++)
                GameDB.Instance.playerMission.playerTeamPlayMissionArray[i] = null;
            // 팀 미션 수행 하는 사람 초기화
            GameDB.Instance.playerMission.isTeamMission = false;
            // 복수자 미션 초기화
            if (GameDB.Instance.playerMission.isChaser == true)
                for (int i = 0; i < GameDB.Instance.playerMission.chaserMissionArray.Length; i++)
                    GameDB.Instance.playerMission.chaserMissionArray[i] = null;
            // 완료한 미션 갯수 초기화
            GameDB.Instance.playerMission.runnerMissionClearCount = 0;
            GameDB.Instance.playerMission.chaserMissionClearCount = 0;
            GameDB.Instance.playerMission.playerTeamPlayMissionCount = 0;
            GameDB.Instance.escapeDoorPositionList.Clear();
            currentRoundPlayersMissionCount = 0;
            currentRoundPlayersMissionPerSent = 0;

        }
        [PunRPC]
        public void AllPlayerLobbySceneLoad()
        {
            SeongMin.GameManager.Instance.GameDataReset();
            PhotonNetwork.LoadLevel("LobbyScene 1");
        }
        //[PunRPC]
        //protected void InitPlayerSetting()
        //{
        //    GameManager.Instance.playerManager.playerController.Init();
        //}

        /* ------ 라운드 카운트 Enum 이였던것-------
        public enum Round
        {
            One = 0,
            Two = 1,
            Three = 2
        }
        [Header("현재 라운드")]
        public Round round = Round.One;
        *///-----------------------------------------

                    /* -------- 라운드 교체 세팅 코루틴이였던 것 ------
                    public void RoundChange(Round _round)
                    {
                        switch (_round)
                        {
                            // 1라운드에서 2라운드로
                            case Round.One:
                                round = Round.Two;
                                //라운드 UI 알림
                                EventDispatcher.instance.SendEvent<string>((int)NHR.EventType.eEventType.Notice_EventUI, "round1End");
                                RoundMapSetting();
                                Invoke("RoundTwoSetting", 3f);
                                //RoundTwoSetting();
                                ChangeText("2");
                                break;
                            // 2라운드에서 3라운드로
                            case Round.Two:
                                round = Round.Three;
                                //라운드 UI 알림
                                EventDispatcher.instance.SendEvent<string>((int)NHR.EventType.eEventType.Notice_EventUI, "round2End");
                                RoundMapSetting();
                                RoundThreeSetting();
                                ChangeText("3");
                                break;
                            // 3라운드에서 엔딩으로
                            case Round.Three:
                                // TODO 결과 UI나 Scene 띄우기 
                                GameDB.Instance.hasGameData = true;
                                // 로비로 이동하기
                                EventDispatcher.instance.SendEvent<eSceneType>((int)NHR.EventType.eEventType.Change_Scene, eSceneType.Lobby);
                                break;

                        }
                    }*///---------------------------------------
                    /* -------- 라운드 2 세팅 함수였던 것 ------
                    private void RoundTwoSetting()
                    {
                        //내 라운드 데이터 초기화하기
                        RoundPlayerDataReset();
                        ChaserSetting();
                        TeamMissionSetting();
                        MissionSetting();
                        GameManager.Instance.inGameMapManager.PlayerPositionSetting();
                        Debug.Log("라운드2 셋팅 완료");
                        Invoke("RoleSettingEvent", 2f);
                    }
                    *///----------------------------------------
                    /* -------- 라운드 3 세팅 함수였던 것 ------
                    private void RoundThreeSetting()
                    {
                        //내 라운드 데이터 초기화하기
                        RoundPlayerDataReset();
                        TeamMissionSetting();
                        MissionSetting();
                        GameManager.Instance.inGameMapManager.PlayerPositionSetting();
                    }*///---------------------------------------
                }

}