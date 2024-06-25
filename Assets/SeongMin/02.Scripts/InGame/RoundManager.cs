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
        [Header("�� ���� �غ�Ϸ� ����")]
        public bool isMapSettingDone = false;
        [Header("��� �÷��̾� ���� �Ϸ� ����")]
        public bool isPlayerAllConnected = false;
        [Header("����� �÷��̾�  ��")]
        public int playerCount = 0;
        [Header("���� �Ϸ�� �÷��̾� �̼� ��")]
        public int currentRoundPlayersMissionCount = 0;
        [Header("���� ���� �̼� ��ü ������")]
        public int currentRoundPlayersMissionPerSent = 0;
        [Header("��ǥ ����� �����ϱ�")]
        public int needPersent = 60;

        public PhotonView photonView;

        private WaitForSeconds popupTime = new WaitForSeconds(8f);

        private void Awake()
        {
            GameManager.Instance.roundManager = this;
            photonView = GetComponent<PhotonView>();
        }
        // 1���� ���� 
        private IEnumerator Start()
        {
            // ���� Ŭ���̾�Ʈ�� ��Ʈ��ũ�� ����ɶ����� ��޸���
            yield return new WaitUntil(() => PhotonNetwork.IsConnected);
            //�� �÷��̾� ����
            var _player = PhotonNetwork.Instantiate("Player", Vector3.up, Quaternion.identity);
            GameManager.Instance.playerManager.playerController = _player.GetComponent<PlayerController>();
            // 1���� ����
            StartCoroutine(RoundOneSetting());
        }

        private IEnumerator RoundOneSetting()
        {
            // �����̸� �� ���� ����
            if (PhotonNetwork.IsMasterClient)
                RoundMapSetting();
            // �� Ŭ���̾�Ʈ�� ���� ������ �ʱ�ȭ �ϱ�
            InGamePublicDataReset();
            // ���� ��� ������ �˸���
            photonView.RPC("CountPlayer", RpcTarget.MasterClient);
            if (PhotonNetwork.IsMasterClient)
            {
                // ��� �÷��̾� ���� ������
                yield return new WaitUntil(() => isPlayerAllConnected);
                // Ư�� �÷��̾�� ���� �����ϱ�
                ChaserSetting();
                // Ư�� �÷��̾�� ���̼� ����ϰ� �ϱ�
                TeamMissionSetting();
                // ��� �÷��̾�� ���� �̼� �����ϱ�
                MissionSetting();
                // ��ü �÷��̾� ��ġ �����ϱ�
                GameManager.Instance.inGameMapManager.PlayerPositionSetting();
                // ��ü �÷��̾�� ���� �Ϸ� �˸���
                photonView.RPC("IsMapSettingDone", RpcTarget.All);
            }
            // �� Ŭ���̾�Ʈ�� ������ ������ �� �� ������ ��ٸ���
            yield return new WaitUntil(() => isMapSettingDone);
            // ���� �ȳ� UI
            RoleSettingEvent();
            //���� ���� UI
            EventDispatcher.instance.SendEvent<string>((int)NHR.EventType.eEventType.Notice_EventUI, "roundStart");
            // ���� Ÿ�̸� ����
            GameManager.Instance.roundTimer.TimerStart();

            //�ʱ� Tips
            EventDispatcher.instance.SendEvent<string>((int)NHR.EventType.eEventType.Popup_Tip, "Move");
            yield return this.popupTime;
            EventDispatcher.instance.SendEvent<string>((int)NHR.EventType.eEventType.Popup_Tip, "Grab");

            yield break;
        }
        private void RoleSettingEvent()
        {
            //���� �ȳ� UI
            if (GameDB.Instance.playerMission.isChaser) EventDispatcher.instance.SendEvent<string>((int)NHR.EventType.eEventType.Notice_Role, "Chaser");
            else EventDispatcher.instance.SendEvent<string>((int)NHR.EventType.eEventType.Notice_Role, "Runner");
        }

        private void RoundMapSetting()
        {
            GameManager.Instance.inGameMapManager.ItemPositionSetting();
        }
        private void InGamePublicDataReset()
        {
            // �̸������� ���� (��� Ŭ���̾�Ʈ���� ���� ���ķ� �� ����Ʈ ������ �ְ� �ϱ� ����)
            GameManager.Instance.inGameMapManager.inGameRunnerItemList.Sort((a, b) => a.name.CompareTo(b.name));
            // ������ �ѹ��� ����Ʈ �ʱ�ȭ
            GameManager.Instance.inGameMapManager.ItemNumberListSetting();
        }
        private void MissionSetting()
        {
            GameManager.Instance.missionManager.MissionSetting();
        }

        [PunRPC] //��� Ŭ���̾�Ʈ�� �޴� ��
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
        [PunRPC] // �������� ������ �÷��̾ �޴� ��
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
        [PunRPC] // �������� ������ �÷��̾ �޴� ��
        public void GetTeamMission()
        {
            GameDB.Instance.playerMission.isTeamMission = true;
        }
        [PunRPC] // ���常 �޴� ��
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
                // ������ Ű �����ϱ�
                GameDB.Instance.Shuffle(GameManager.Instance.inGameMapManager.inGameItemPositionList);
                GameObject key = PhotonNetwork.Instantiate("Ż�ⱸ ����", GameManager.Instance.inGameMapManager.inGameItemPositionList[0].position, Quaternion.identity);
                // Ż�� ���� 2�� �����ϱ�
                GameDB.Instance.Shuffle(GameDB.Instance.escapeDoorPositionList);
                GameObject exitDoor1 = PhotonNetwork.Instantiate("EscapeDoor", GameDB.Instance.escapeDoorPositionList[0].position, Quaternion.identity);
                GameObject exitDoor2 = PhotonNetwork.Instantiate("EscapeDoor", GameDB.Instance.escapeDoorPositionList[1].position, Quaternion.identity);
                exitDoor1.SetActive(true);
                exitDoor2.SetActive(true);
                Debug.Log("Ű ����");

                key.transform.Find("MinimapIcon").gameObject.SetActive(true);
                exitDoor1.transform.Find("MinimapIcon").gameObject.SetActive(true);
                exitDoor2.transform.Find("MinimapIcon").gameObject.SetActive(true);
                Debug.Log("�̴ϸ� ����");
            }
            else // �װ� �ƴ϶��, ��� �÷��̾�� ��ü �̼� ���൵ �����ϱ�
            {
                photonView.RPC("UpdateAllPlayerMissionPersent", RpcTarget.All, _value);
            }
        }

        [PunRPC]
        public void UpdateAllPlayerMissionPersent(int _value)
        {
            //�ʿ� �ۼ�Ʈ�� 1/4, 2/4, 3/4 4/4���� UI �ȳ�
            int quater = this.needPersent / 4;
            if (_value >= needPersent) EventDispatcher.instance.SendEvent((int)NHR.EventType.eEventType.Complete_RoundMission);
            //else if (_value % quater == 0) EventDispatcher.instance.SendEvent<int>((int)NHR.EventType.eEventType.Notice_TotalMissionPercent, _value);
            else
            {
                string str = string.Format("{0}/{1}", GameDB.Instance.playerMission.runnerMissionClearCount, GameDB.Instance.playerMission.playerMissionArray.Length);
                EventDispatcher.instance.SendEvent<string>((int)NHR.EventType.eEventType.Notice_TotalMissionPercent, str);
            }

        }

        // �� ���� ������ �ʱ�ȭ�ϱ�
        public void RoundPlayerDataReset()
        {
            // ���ι̼� �ʱ�ȭ
            for (int i = 0; i < GameDB.Instance.playerMission.playerMissionArray.Length; i++)
                GameDB.Instance.playerMission.playerMissionArray[i] = null;
            // �� �̼� �ʱ�ȭ
            for (int i = 0; i < GameDB.Instance.playerMission.playerTeamPlayMissionArray.Length; i++)
                GameDB.Instance.playerMission.playerTeamPlayMissionArray[i] = null;
            // �� �̼� ���� �ϴ� ��� �ʱ�ȭ
            GameDB.Instance.playerMission.isTeamMission = false;
            // ������ �̼� �ʱ�ȭ
            if (GameDB.Instance.playerMission.isChaser == true)
                for (int i = 0; i < GameDB.Instance.playerMission.chaserMissionArray.Length; i++)
                    GameDB.Instance.playerMission.chaserMissionArray[i] = null;
            // �Ϸ��� �̼� ���� �ʱ�ȭ
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

        /* ------ ���� ī��Ʈ Enum �̿�����-------
        public enum Round
        {
            One = 0,
            Two = 1,
            Three = 2
        }
        [Header("���� ����")]
        public Round round = Round.One;
        *///-----------------------------------------

                    /* -------- ���� ��ü ���� �ڷ�ƾ�̿��� �� ------
                    public void RoundChange(Round _round)
                    {
                        switch (_round)
                        {
                            // 1���忡�� 2�����
                            case Round.One:
                                round = Round.Two;
                                //���� UI �˸�
                                EventDispatcher.instance.SendEvent<string>((int)NHR.EventType.eEventType.Notice_EventUI, "round1End");
                                RoundMapSetting();
                                Invoke("RoundTwoSetting", 3f);
                                //RoundTwoSetting();
                                ChangeText("2");
                                break;
                            // 2���忡�� 3�����
                            case Round.Two:
                                round = Round.Three;
                                //���� UI �˸�
                                EventDispatcher.instance.SendEvent<string>((int)NHR.EventType.eEventType.Notice_EventUI, "round2End");
                                RoundMapSetting();
                                RoundThreeSetting();
                                ChangeText("3");
                                break;
                            // 3���忡�� ��������
                            case Round.Three:
                                // TODO ��� UI�� Scene ���� 
                                GameDB.Instance.hasGameData = true;
                                // �κ�� �̵��ϱ�
                                EventDispatcher.instance.SendEvent<eSceneType>((int)NHR.EventType.eEventType.Change_Scene, eSceneType.Lobby);
                                break;

                        }
                    }*///---------------------------------------
                    /* -------- ���� 2 ���� �Լ����� �� ------
                    private void RoundTwoSetting()
                    {
                        //�� ���� ������ �ʱ�ȭ�ϱ�
                        RoundPlayerDataReset();
                        ChaserSetting();
                        TeamMissionSetting();
                        MissionSetting();
                        GameManager.Instance.inGameMapManager.PlayerPositionSetting();
                        Debug.Log("����2 ���� �Ϸ�");
                        Invoke("RoleSettingEvent", 2f);
                    }
                    *///----------------------------------------
                    /* -------- ���� 3 ���� �Լ����� �� ------
                    private void RoundThreeSetting()
                    {
                        //�� ���� ������ �ʱ�ȭ�ϱ�
                        RoundPlayerDataReset();
                        TeamMissionSetting();
                        MissionSetting();
                        GameManager.Instance.inGameMapManager.PlayerPositionSetting();
                    }*///---------------------------------------
                }

}