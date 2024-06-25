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
            if(photonView.IsMine)
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
            EventDispatcher.instance.AddEventHandler((int)NHR.EventType.eEventType.Change_Monster, new EventHandler((type) =>
            {
                //������ ������ ��쿡�� ���� ����
                if (this.isChaser)
                {
                    Debug.Log("<color=red>���� ���� �Ϸ�</color>");
                    //���� �𵨷� �ٲ�� ��� �÷��̾�� ����ȭ �ϱ�'

                    
                    photonView.RPC("CharacterChange", RpcTarget.All, "Chaser");
                }
                //GameManager.Instance.roundTimer.MonsterTimerStart();

            }));

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
        [PunRPC]
        public void WinCheck()
        {

            if (PhotonNetwork.IsMasterClient)
            {


            }
        }
        public void AllPlayerMissionScoreUpdate()
        {
            float value = GameManager.Instance.roundManager.currentRoundPlayersMissionCount / (playerMissionArray.Length * PhotonNetwork.PlayerList.Length);
            value = (float)Math.Round(value, 2);
            value *= 100;
            print(value+"���� ���� �� ���� �ۼ�Ʈ");
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
        }
    }
}