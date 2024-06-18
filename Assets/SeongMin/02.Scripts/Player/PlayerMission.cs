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
        public GameObject currentRunnerPrefab;
        [Header("������ ĳƽ�� ������Ʈ")]
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
            //���� ����
            EventDispatcher.instance.AddEventHandler((int)NHR.EventType.eEventType.Change_Monster, new EventHandler((type) =>
            {
                //������ ������ ��쿡�� ���� ����
                if(this.isChaser)
                {
                    Debug.Log("<color=red>���� ���� �Ϸ�</color>");
                    //���� �𵨷� �ٲ�� ��� �÷��̾�� ����ȭ �ϱ�
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