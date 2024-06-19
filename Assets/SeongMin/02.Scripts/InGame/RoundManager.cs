using NHR;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static NHR.App;
using static SeongMin.RoundManager;

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

        InGameMapManager inGameMapManager;
        public enum Round
        {
            One = 0,
            Two = 1,
            Three = 2
        }
        [Header("현재 라운드")]
        public Round round = Round.One;
        public PhotonView photonView;
        private void Awake()
        {
            GameManager.Instance.roundManager = this;
            photonView = GetComponent<PhotonView>();
        }
        // 1라운드 세팅 
        private IEnumerator Start()
        {
            inGameMapManager = GameManager.Instance.inGameMapManager;
            // 나의 클라이언트가 네트워크에 연결될때까지 기달리기
            yield return new WaitUntil(() => PhotonNetwork.IsConnected);
            // 방장에게 내가 들어 왔음을 알리기
            photonView.RPC("CountPlayer", RpcTarget.MasterClient);
            //내 플레이어 생성
            var _player = PhotonNetwork.Instantiate("Player", Vector3.up, Quaternion.identity);
            //GameManager.Instance.photonManager.SetPlayer();
            GameManager.Instance.playerManager.playerController = _player.GetComponent<PlayerController>();

            //캐릭터 커스텀 설정
            //photonView.RPC("InitPlayerSetting", RpcTarget.AllBuffered);
            //GameManager.Instance.photonManager.OnPlayer();

            //최초 라운드세팅 실행
            RoundMapSetting();
            // 1라운드 세팅
            StartCoroutine(RoundOneSetting());
            // TODO 로딩 구현
        }
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
        }
        
        private IEnumerator RoundOneSetting()
        {
            // 인게임 시작 시 최초 한번만 공용 데이터 초기화 하기
            InGamePublicDataReset();
            // 내 클라이언트 라운드 데이터 초기화하기
            RoundPlayerDataReset();

            if (PhotonNetwork.IsMasterClient)
            {
                //TODO 모든 플레이어 연결 할 때까지 기다리는 UI 구현하기
                // 모든 플레이어 연결 했으면
                yield return new WaitUntil(() => isPlayerAllConnected);
                // 전체 플레이어에게 미션 세팅하기
                MissionSetting();
                //for(int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
                //{
                //    photonView.RPC("InitPlayerSetting", PhotonNetwork.PlayerList[i]);
                //}
            }

            //라운드 시작 UI
            EventDispatcher.instance.SendEvent<string>((int)NHR.EventType.eEventType.Notice_EventUI, "roundStart");

            yield break;
        }
        private void RoundTwoSetting()
        {
            //내 라운드 데이터 초기화하기
            RoundPlayerDataReset();
            ChaserSetting();
            TeamMissionSetting();
            MissionSetting();
            Debug.Log("라운드2 셋팅 완료");
            Invoke("RoleSettingEvent", 2f);
        }
        private void RoleSettingEvent()
        {
            //역할 안내 UI
            if (GameDB.Instance.playerMission.isChaser) EventDispatcher.instance.SendEvent<string>((int)NHR.EventType.eEventType.Notice_Role, "Chaser");
            else EventDispatcher.instance.SendEvent<string>((int)NHR.EventType.eEventType.Notice_Role, "Runner");
        }
        private void RoundThreeSetting()
        {
            //내 라운드 데이터 초기화하기
            RoundPlayerDataReset();
            TeamMissionSetting();
            MissionSetting();
        }
        private void RoundMapSetting()
        {
            if (PhotonNetwork.IsMasterClient)
            {
                inGameMapManager.ItemPositionSetting();
                inGameMapManager.PlayerPositionSetting();
            }
            GameManager.Instance.roundTimer.TimerStart();
        }
        private void InGamePublicDataReset()
        {
            // 이름순으로 정렬하기 (모든 클라이언트에게 같은 정렬로 된 리스트 가지고 있게 하기 위해)
            inGameMapManager.inGameRunnerItemList.Sort((a, b) => a.name.CompareTo(b.name));
            // 아이템 넘버링 리스트 초기화 하기
            inGameMapManager.ItemNumberListSetting();
        }
        // 내 라운드 데이터 초기화하기
        private void RoundPlayerDataReset()
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
            currentRoundPlayersMissionCount = 0;
            currentRoundPlayersMissionPerSent = 0;

        }
        //복수자 배정하기
        private void ChaserSetting()
        {
            if (PhotonNetwork.IsMasterClient)
            {
                int randNumber = Random.Range(0, PhotonNetwork.PlayerList.Length);
                Player _player = PhotonNetwork.PlayerList[randNumber];
                photonView.RPC("ImChaser", _player);
            }
        }
        [PunRPC]
        public void ImChaser()
        {
            GameDB.Instance.playerMission.isChaser = true;
        }
        private void MissionSetting()
        {
            if(PhotonNetwork.IsMasterClient)
            GameManager.Instance.missionManager.MissionSetting();
        }
        //협업 미션 배정하기
        private void TeamMissionSetting()
        {
            if (PhotonNetwork.IsMasterClient)
            {
                int randNumber = Random.Range(0, PhotonNetwork.PlayerList.Length);
                Player _player = PhotonNetwork.PlayerList[randNumber];
                photonView.RPC("GetTeamMission", _player);
            }
        }
        [PunRPC]
        public void GetTeamMission()
        {
            GameDB.Instance.playerMission.isTeamMission = true;
        }
        [PunRPC]
        public void CountPlayer()
        {
            playerCount++;
            if (playerCount == GameDB.Instance.playerCount)
            {
                isPlayerAllConnected = true;
            }

        }
        [PunRPC]
        public void SendAllPlayerMissionScoreUpdate(int _value)
        {
            // 플레이어들이 전체 목표 달성 했을 때. 라운드 체인지
            currentRoundPlayersMissionPerSent = _value;
            if (PhotonNetwork.IsMasterClient)
            {
                if (currentRoundPlayersMissionPerSent >= needPersent)
                {
                    if (round == Round.Three)
                    {
                        GameDB.Instance.Shuffle(inGameMapManager.inGameItemPositionList);
                        PhotonNetwork.Instantiate("FinalKey", inGameMapManager.inGameItemPositionList[0].position, Quaternion.identity);
                    }
                    else
                        RoundChange(round);
                }
                else // 그게 아니라면, 방장이 모든 플레이어에게 전체 미션 진행도 공유하기
                {
                    photonView.RPC("UpdateAllPlayerMissionPersent", RpcTarget.All, _value);
                }
            }
        }
        [PunRPC]
        public void UpdateAllPlayerMissionPersent(int _value)
        {
            //TODO 모든 플레이어에게 UI 갱신 시켜주기
            currentRoundPlayersMissionPerSent = _value;
        }
        //[PunRPC]
        //protected void InitPlayerSetting()
        //{
        //    GameManager.Instance.playerManager.playerController.Init();
        //}
        private void ChangeText(string _text)
        {
            //UIManager.Instance.inGameSceneMenu.roundChangeText.text = _text;
        }
    }

}