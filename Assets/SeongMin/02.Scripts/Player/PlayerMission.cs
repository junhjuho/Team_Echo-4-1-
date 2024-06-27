using NHR;
using Photon.Pun;
using SeongMin;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace SeongMin
{
    public class PlayerMission : MonoBehaviour
    {
        public PhotonView photonView;
        [Header("������ ���� �޾Ҵ��� ����")]
        public bool isChaser = false;
        [Header("���� �̼� ���� �޾Ҵ��� ����")]
        public bool isTeamMission = false;
        [Header("���� �������� �÷��̾��� �̼� ����Ʈ ")]
        public GameObject[] playerMissionArray;
        [Header("���� �������� �÷��̾��� ���� �̼� ����Ʈ ")]
        public GameObject[] playerTeamPlayMissionArray;
        [Header("���� �������� ���������� �̼� ����Ʈ ")]
        public GameObject[] chaserMissionArray;
        [Header("���� �Ϸ��� �̼� ����")]
        public int runnerMissionClearCount = 0;
        [Header("���� �Ϸ��� ���÷��� �̼� ����")]
        public int playerTeamPlayMissionCount = 0;
        [Header("���� �Ϸ��� ������ �̼� ����")]
        public int chaserMissionClearCount = 0;
        [Header("�Ϲ� ���� ĳ���� ������Ʈ")]
        public Character currentRunnerCharacrer;
        public Character[] currentRunnerCharacrers;
        [Header("������ ĳƽ�� ������Ʈ")]
        public GameObject chaserPrefab;


        private MissionManager missionManager;
        //[Header("")]
        private void Awake()
        {

            photonView = GetComponent<PhotonView>();
            chaserPrefab = this.transform.Find("zombie").gameObject;
            if (photonView.IsMine)
                GameDB.Instance.playerMission = this;

            if (GameManager.Instance.missionManager != null)
            {
                missionManager = GameManager.Instance.missionManager;
                playerMissionArray = new GameObject[missionManager.runnerMissionCount];

                playerTeamPlayMissionArray = new GameObject[missionManager.teamPlayMissionCount];

                chaserMissionArray = new GameObject[missionManager.chaserMissionCount];
            }

            //if(GameManager.Instance.lobbySceneManager != null)
            //{
            //    this.chaserMissionArray = new GameObject[GameManager.Instance.lobbySceneManager.lobbyMissionCount];
            //    //퀘스트 임의 지정_복수자
            //    this.chaserMissionArray[0] = GameManager.Instance.lobbySceneManager.knife;
            //    SeongMin.GameManager.Instance.tutorialSceneManager.questObjectManager.Init();

            //}

            //this.currentRunnerCharacrers = this.GetComponentsInChildren<Character>();
        }
        private void Start()
        {
            //���� ����
            StartCoroutine(DelayRoutine());

            //테스트
            //yield return new WaitForSeconds(5f);
            //if (this.isChaser)
            //{
            //    photonView.RPC("CharacterChange", RpcTarget.All, "Chaser");
            //}
        }

        IEnumerator DelayRoutine()
        {
            yield return new WaitForSeconds(5f);

            if (!isChaser)
            {
                foreach (var item in playerMissionArray)
                {
                    item.transform.Find("MinimapIcon");
                }
            }
            else
            {
                foreach (var item in chaserMissionArray)
                {
                    item.transform.Find("MinimapIcon");
                }
            }

        }

        public bool MissionItemCheck(GameObject _item, GameObject[] _array)
        {
            bool _value = false;
            Debug.Log("Item : " + _item);
            for (int i = 0; i < _array.Length; i++)
            {
                //Debug.Log(_item + " / " + _array[i]);
                //Debug.Log(_item.name + " / " + _array[i].name);

                if (_array[i].name == _item.name)
                    _value = true;
            }
            return _value;
        }
       
        public void AllPlayerMissionScoreUpdate()
        {
            float value = GameManager.Instance.roundManager.currentRoundPlayersMissionCount / (playerMissionArray.Length * PhotonNetwork.PlayerList.Length);
            value = (float)Math.Round(value, 2);
            value *= 100;
            print(value + "���� ���� �� ���� �ۼ�Ʈ");
            // ��ü �̼� �ۼ�Ʈ �ٲ� �� �����ϰ� ��û�ϱ�
            //GameManager.Instance.roundManager.photonView.RPC("SendAllPlayerMissionScoreUpdate", RpcTarget.All, (int)value);
            GameManager.Instance.roundManager.RPCSendScoreUpdate((int)value);
        }

        [PunRPC]
        public void CharacterChange(string _value)
        {
            Debug.LogFormat("���� ���� RPC{0}", _value);
            if (_value == "Chaser")
            {
                chaserPrefab.SetActive(true);
                //currentRunnerCharacrer.gameObject.SetActive(false);
                foreach (Character character in currentRunnerCharacrers) character.gameObject.SetActive(false);
            }
            else
            {
                //currentRunnerCharacrer.gameObject.SetActive(true);]
                chaserPrefab.SetActive(false);
            }
            //괴물 변신 알림
            EventDispatcher.instance.SendEvent<string>((int)NHR.EventType.eEventType.Notice_EventUI, "chaserChangeOn");
        }
        public void RunnerSetActive()
        {
            StartCoroutine(RespawnPlayer());
        }

        private IEnumerator RespawnPlayer()
        {
            yield return new WaitForSeconds(2f);
            GameDB.Instance.Shuffle(SeongMin.GameManager.Instance.inGameMapManager.playerSpawnPositionList);
            if (photonView.IsMine)
            {
                photonView.RPC("RunnerSetActiveRPC", RpcTarget.All);
                // 플레이어 캐릭터 다시 키고
                SeongMin.GameManager.Instance.playerManager.humanMovement.gameObject.SetActive(true);
                // 플레이어 재 위치 시키기
                GameDB.Instance.myPlayer.transform.position = SeongMin.GameManager.Instance.inGameMapManager.playerSpawnPositionList[0].position+Vector3.up;
            }


            yield break;
        }
        [PunRPC]
        public void RunnerSetActiveRPC()
        {
            Debug.Log("Respawn Character ID : " + InfoManager.Instance.PlayerInfo.nowCharacterId);
            SeongMin.GameManager.Instance.playerManager.humanMovement.gameObject.SetActive(true);
            GameDB.Instance.myPlayer.transform.position =
            SeongMin.GameManager.Instance.inGameMapManager.playerSpawnPositionList[0].position;
        }

        public void WinCheck(string _value)
        {
            if (_value == "RunnerWin")
            {
                photonView.RPC("RunnerWin", RpcTarget.All);
            }
            else
            {
                photonView.RPC("ChaserWin", RpcTarget.All);
            }
            SeongMin.GameManager.Instance.roundManager.RoundPlayerDataReset();
            SeongMin.GameManager.Instance.roundManager.photonView.RPC("AllPlayerLobbySceneLoad", RpcTarget.MasterClient);
        }
        [PunRPC]
        public void RunnerWin()
        {
            GameDB.Instance.hasGameData = true;
            // 주의 !!!!! 여기서 이 오브젝트를 호출 한 건 다른 PhotonView를 가진
            // 오브젝트가 가지고 있으므로 이전에 할당으로 PhotonView isMine으로 확정된
            // 자기 자신의 로컬 playerMission.isChaser를 검사해야 하므로 
            // 싱글톤에 있는 걸 가지고 와서 검사합니다 ! 
            if (GameDB.Instance.playerMission.isChaser == false)
            {
                GameDB.Instance.isWin = true;
            }
            else
            {
                GameDB.Instance.isWin = false;
            }
        }
        [PunRPC]
        public void ChaserWin()
        {
            GameDB.Instance.hasGameData = true;
            if (GameDB.Instance.playerMission.isChaser == false)
            {
                GameDB.Instance.isWin = false;
            }
            else
            {
                GameDB.Instance.isWin = true;
            }
        }
    }
}