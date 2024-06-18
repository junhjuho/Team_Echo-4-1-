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
        [Header("�� ���� �غ�Ϸ� ����")]
        public bool isMapSettingDone = false;
        [Header("��� �÷��̾� ���� �Ϸ� ����")]
        public bool isPlayerAllConnected = false;
        public int playerCount = 0;
        public enum Round
        {
            One = 0,
            Two = 1,
            Three = 2
        }
        public Round round = Round.One;
        private PhotonView photonView;
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
            // ���忡�� ���� ��� ������ �˸���
            photonView.RPC("CountPlayer", RpcTarget.MasterClient);
            //�� �÷��̾� ����
            var _player = PhotonNetwork.Instantiate("Player", Vector3.up, Quaternion.identity);
            GameManager.Instance.playerManager.playerController = _player.GetComponent<PlayerController>();

            //ĳ���� Ŀ���� ����
            //photonView.RPC("InitPlayerSetting", RpcTarget.AllBuffered);
            GameManager.Instance.photonManager.OnPlayer();

            //���� ���弼�� ����
            RoundMapSetting();
            // 1���� ����
            StartCoroutine(RoundOneSetting());
            // TODO �ε� ����
        }
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
        }
        
        private IEnumerator RoundOneSetting()
        {
            // �ΰ��� ���� �� ���� �ѹ��� ���� ������ �ʱ�ȭ �ϱ�
            InGamePublicDataReset();
            // �� Ŭ���̾�Ʈ ���� ������ �ʱ�ȭ�ϱ�
            RoundPlayerDataReset();

            if (PhotonNetwork.IsMasterClient)
            {
                //TODO ��� �÷��̾� ���� �� ������ ��ٸ��� UI �����ϱ�
                // ��� �÷��̾� ���� ������
                yield return new WaitUntil(() => isPlayerAllConnected);
                // ��ü �÷��̾�� �̼� �����ϱ�
                MissionSetting();
            }

            //���� ���� UI
            EventDispatcher.instance.SendEvent<string>((int)NHR.EventType.eEventType.Notice_EventUI, "roundStart");

            yield break;
        }
        private void RoundTwoSetting()
        {
            //�� ���� ������ �ʱ�ȭ�ϱ�
            RoundPlayerDataReset();
            ChaserSetting();
            TeamMissionSetting();
            MissionSetting();
            Debug.Log("����2 ���� �Ϸ�");
            Invoke("RoleSettingEvent", 2f);
        }
        private void RoleSettingEvent()
        {
            //���� �ȳ� UI
            if (GameDB.Instance.playerMission.isChaser) EventDispatcher.instance.SendEvent<string>((int)NHR.EventType.eEventType.Notice_Role, "Chaser");
            else EventDispatcher.instance.SendEvent<string>((int)NHR.EventType.eEventType.Notice_Role, "Runner");
        }
        private void RoundThreeSetting()
        {
            //�� ���� ������ �ʱ�ȭ�ϱ�
            RoundPlayerDataReset();
            TeamMissionSetting();
            MissionSetting();
        }
        private void RoundMapSetting()
        {
            if (PhotonNetwork.IsMasterClient)
            {
                GameManager.Instance.inGameMapManager.ItemPositionSetting();
                GameManager.Instance.inGameMapManager.PlayerPositionSetting();
            }
            GameManager.Instance.roundTimer.TimerStart();
        }
        private void InGamePublicDataReset()
        {
            // �̸������� �����ϱ� (��� Ŭ���̾�Ʈ���� ���� ���ķ� �� ����Ʈ ������ �ְ� �ϱ� ����)
            GameManager.Instance.inGameMapManager.inGameRunnerItemList.Sort((a, b) => a.name.CompareTo(b.name));
            // ������ �ѹ��� ����Ʈ �ʱ�ȭ �ϱ�
            GameManager.Instance.inGameMapManager.ItemNumberListSetting();
        }
        // �� ���� ������ �ʱ�ȭ�ϱ�
        private void RoundPlayerDataReset()
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

        }
        //������ �����ϱ�
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
        //���� �̼� �����ϱ�
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
        protected void InitPlayerSetting()
        {
            var controller = GameManager.Instance.playerManager.playerController;
            if (controller != null) controller.Init();
        }
        private void ChangeText(string _text)
        {
            //UIManager.Instance.inGameSceneMenu.roundChangeText.text = _text;
        }
    }

}