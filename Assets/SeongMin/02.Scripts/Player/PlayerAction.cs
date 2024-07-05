using Jaewook;
using Photon.Pun;
using SeongMin;
using System.Collections;
using System.Collections.Generic;
using Unity.Services.Analytics;
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
            //���� ��ü�� ItemObject ��ũ��Ʈ�� �ִ��� Ȯ�� �� _item �� �ݹ����� �޾ƿ���
            if (args.interactableObject.transform.TryGetComponent(out ItemObject _item) && _item.isFind == false)
            {
                playerMission = GameDB.Instance.playerMission;
                //Ż�ⱸ ���踦 ��Ҵٸ�
                if(_item.gameObject.TryGetComponent<FinalKey>(out FinalKey finalKey))
                    EventDispatcher.instance.SendEvent((int)NHR.EventType.eEventType.Get_Final_Key);

                // �÷��̾��� �̼� �迭�� _item ������Ʈ�� ��ġ�ϴ� �� ������ if�� �ȿ� �ڵ带 ����
                if (_item.charactorValue == CharactorValue.runner && playerMission.MissionItemCheck(_item.gameObject, playerMission.playerMissionArray))
                {
                    _item.isFind = true;
                    //UI�̺�Ʈ
                    EventDispatcher.instance.SendEvent<string>((int)NHR.EventType.eEventType.Complete_Mission, _item.name);
                    int index = -1;
                    for (int i = 0; i < playerMission.playerMissionArray.Length; i++)
                    {
                        if (playerMission.playerMissionArray[i].name == _item.gameObject.name)
                        {
                            index = i;
                            break;
                        }
                    }
                    EventDispatcher.instance.SendEvent<int>((int)NHR.EventType.eEventType.Remove_Mission, index);

                    // ������ ����
                    _item.triggerObject.SetActive(false);
                    _item.miniMap.SetActive(false);
                    _item.gameObject.SetActive(false);
                    // ������ ǥ�� UI ����
                    var canvas = GameDB.Instance.itemInfomationCanvas;
                    canvas.image.SetActive(false);
                    canvas.text.gameObject.SetActive(false);
                    playerMission.runnerMissionClearCount++;
                    //�Ϲ� �̼� Ŭ������ ���� üũ
                    if (playerMission.runnerMissionClearCount >= playerMission.playerMissionArray.Length - 1)
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
                    // ��� Ŭ���̾�Ʈ���� ��ü �̼� ���� ���� �ø���
                    photonView.RPC("RunnersAllMissionCount", RpcTarget.All);
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
                }
                // ���� ������ �� ���� �����ڿ� ������ ī���� �ϱ�
                else if (_item.charactorValue == CharactorValue.chaser
                    && playerMission.isChaser
                    && playerMission.MissionItemCheck(_item.gameObject, playerMission.chaserMissionArray))
                {
                    //UI�̺�Ʈ
                    EventDispatcher.instance.SendEvent<string>((int)NHR.EventType.eEventType.Complete_Mission, _item.name);
                    int index = -1;
                    for (int i = 0; i < playerMission.chaserMissionArray.Length; i++)
                    {
                        if (playerMission.chaserMissionArray[i].name == _item.gameObject.name)
                        {
                            index = i;
                            break;
                        }
                    }
                    EventDispatcher.instance.SendEvent<int>((int)NHR.EventType.eEventType.Remove_Mission, index);

                    // ������ ����
                    _item.isFind = true;
                    _item.triggerObject.SetActive(false);
                    _item.gameObject.SetActive(false);
                    _item.triggerObject.SetActive(false);
                    _item.miniMap.SetActive(false);
                    // ������ ǥ�� UI ����
                    var canvas = GameDB.Instance.itemInfomationCanvas;
                    canvas.image.SetActive(false);
                    canvas.text.gameObject.SetActive(false);
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
                else if(playerMission.isChaser == false && _item.TryGetComponent(out FinalKey _key))
                {
                    _key.miniMap.SetActive(false);
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

            // �� Ŭ���̾�Ʈ�� �������̸�, ������ �̼� Ŭ���� ������ ������ �̼� �迭�� ���̿� ���ų� ������ ����
            if (playerMission.isChaser && playerMission.chaserMissionClearCount >= playerMission.chaserMissionArray.Length)
            {
                // �����ڷ� ĳ���� ����
                EventDispatcher.instance.SendEvent((int)NHR.EventType.eEventType.Change_Monster);
                GameDB.Instance.playerController.nowCharacter.gameObject.SetActive(false);
                // ī���� �ʱ�ȭ
                playerMission.chaserMissionClearCount = 0;
            }
        }
        [PunRPC]
        private void RunnersAllMissionCount()
        {
            GameManager.Instance.roundManager.currentRoundPlayersMissionCount++;
        }
    }

}