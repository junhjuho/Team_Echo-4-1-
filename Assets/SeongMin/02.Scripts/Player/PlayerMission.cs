using Photon.Pun;
using SeongMin;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SeongMin
{
    public class PlayerMission : MonoBehaviour
    {
        public PhotonView photonView;
        [Header("복수자 배정 받았는지 여부")]
        public bool isChaser = false;
        [Header("협동 미션 배정 받았는지 여부")]
        public bool isTeamMission = false;
        [Header("현재 배정받은 플레이어의 미션 리스트 ")]
        public GameObject[] playerMissionArray;
        [Header("현재 배정받은 플레이어의 협동 미션 리스트 ")]
        public GameObject[] playerTeamPlayMissionArray;
        [Header("현재 배정받은 복수자의의 미션 리스트 ")]
        public GameObject[] chaserMissionArray;
        [Header("현재 완료한 미션 갯수")]
        public int runnerMissionClearCount = 0;
        [Header("현재 완료한 팀플레이 미션 갯수")]
        public int playerTeamPlayMissionCount = 0;
        [Header("현재 완료한 복수자 미션 갯수")]
        public int chaserMissionClearCount = 0;
        [Header("일반 상태 캐릭터 오브젝트")]
        public GameObject currentRunnerPrefab;
        [Header("복수자 캐틱터 오브젝트")]
        public GameObject chaserPrefab;
        

        private MissionManager missionManager;
        //[Header("")]
        private void Awake()
        {
            
            photonView = GetComponent<PhotonView>();
            chaserPrefab = this.transform.Find("zombie").gameObject;
            GameDB.Instance.playerMission = this;

            if (GameManager.Instance.missionManager != null)
            {
                missionManager = GameManager.Instance.missionManager;
                playerMissionArray = new GameObject[missionManager.runnerMissionCount];

                playerTeamPlayMissionArray = new GameObject[missionManager.teamPlayMissionCount];

                chaserMissionArray = new GameObject[missionManager.chaserMissionCount];
            }
        }
        private void Start()
        {
            //괴물 변신
            EventDispatcher.instance.AddEventHandler((int)NHR.EventType.eEventType.Change_Monster, new EventHandler((type) =>
            {
                //복수자 배정된 경우에만 괴물 변신
                if(this.isChaser)
                {
                    Debug.Log("<color=red>괴물 변신 완료</color>");
                    //괴물 모델로 바뀐걸 모든 플레이어에게 동기화 하기
                    photonView.RPC("CharacterChange", RpcTarget.AllBuffered, "Chaser");
                    GameManager.Instance.inGameMapManager.PlayerPositionSetting();
                }
                GameManager.Instance.roundTimer.MonsterTimerStart();
            }));
        }
        public bool MissionItemCheck(GameObject _item, GameObject[] _array)
        {
            bool _value = false;

            for (int i = 0; i < _array.Length; i++)
            {
                if (_array[i] == _item)
                    _value = true;
            }
            return _value;
        }

        [PunRPC]
        public void WinCheck()
        {

            if (PhotonNetwork.IsMasterClient)
            {

               
            }
        }

        [PunRPC]
        public void CharacterChange(string _value)
        {
            if (_value == "Chaser")
            {
                chaserPrefab.SetActive(true);
                currentRunnerPrefab.SetActive(false);
            }
            else
            {
                currentRunnerPrefab.SetActive(true);
                chaserPrefab.SetActive(false);
                GameManager.Instance.inGameMapManager.ChaserItemResetting();
            }
        }
    }
}