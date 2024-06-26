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

        //��
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
        // �÷��̾ �������� ����� ��,
        private void SelectEvent(SelectEnterEventArgs args)
        {
            Debug.Log(args.interactableObject.transform.name);
            //���� ��ü�� ItemObject ��ũ��Ʈ�� �ִ��� Ȯ�� �� _item �� �ݹ����� �޾ƿ���
            if (args.interactableObject.transform.TryGetComponent(out ItemObject _item) && _item.isFind == false)
            {
                Debug.Log("Get Item1 : " + args.interactableObject.transform.name);
                Debug.Log("Get Item2 : " + _item.charactorValue);

                playerMission = GameDB.Instance.playerMission;
                Debug.Log("Get Item3 : " + playerMission.isChaser);
                //_item.isFind = true;

                // �÷��̾��� �̼� �迭�� _item ������Ʈ�� ��ġ�ϴ� �� ������ if�� �ȿ� �ڵ带 ����
                if (_item.charactorValue == CharactorValue.runner && playerMission.MissionItemCheck(_item.gameObject, playerMission.playerMissionArray))
                {
                    print("��ҽ��ϴ�");
                    _item.isFind = true;
                    _item.gameObject.SetActive(false);
                    playerMission.runnerMissionClearCount++;
                    if (playerMission.runnerMissionClearCount >= 3)
                    {
                        //Test code
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
                    EventDispatcher.instance.SendEvent<string>((int)NHR.EventType.eEventType.Complete_Mission, _item.name);
                    GameManager.Instance.roundManager.currentRoundPlayersMissionCount++;
                    playerMission.AllPlayerMissionScoreUpdate();

                    //��
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

                    //TODO �� ������ �κ��丮�� �ֱ�
                }
                // ���� ������ �� ���� �����ڿ� ������ ī���� �ϱ�
                else if (_item.charactorValue == CharactorValue.chaser
                    && playerMission.isChaser
                    && playerMission.MissionItemCheck(_item.gameObject, playerMission.chaserMissionArray))
                {
                    Debug.Log("Chaser : " + args.interactableObject.transform.name);
                    _item.isFind = true;
                    _item.gameObject.SetActive(false);
                    playerMission.chaserMissionClearCount++;
                }
                // ���� �� �÷��� �̼��� ���� ���� ���÷��̿� ������ ī���� �ϱ�
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

            Debug.Log("Ű ���� �õ�");
            //�� Ŭ���̾�Ʈ �÷��̾ �����ڰ� �ƴϰ�, �Ϲ� �̼� Ŭ������ ������ �Ϲ� �̼� �迭�� ���̿� ���ų� ������ ����
            if (GameDB.Instance.playerMission.isChaser == false &&
                GameDB.Instance.playerMission.runnerMissionClearCount >= GameDB.Instance.playerMission.playerMissionArray.Length)
            {
                Debug.Log("Ű ���� �õ�2");
                //-----------TODO �׽�Ʈ �ڵ� ------------------------------------
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
                //------------------------------------------------------------------
            }
            // �� Ŭ���̾�Ʈ�� �������̸�, ������ �̼� Ŭ���� ������ ������ �̼� �迭�� ���̿� ���ų� ������ ����
            if (playerMission.isChaser && playerMission.chaserMissionClearCount >= playerMission.chaserMissionArray.Length)
            {
                // �����ڷ� ĳ���� ����
                EventDispatcher.instance.SendEvent((int)NHR.EventType.eEventType.Change_Monster);
                // ī���� �ʱ�ȭ
                playerMission.chaserMissionClearCount = 0;
            }
        }
    }

}