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
        protected override void Start()
        {
            base.Start();
            photonView = GetComponent<PhotonView>();
            this.selectEntered.AddListener(SelectEvent);
            //this.hoverEntered.AddListener(HoverEvent);
            //this.hoverExited.AddListener(HoverEventExit);
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
                    EventDispatcher.instance.SendEvent<string>((int)NHR.EventType.eEventType.Complete_Mission, _item.name);
                    GameManager.Instance.roundManager.currentRoundPlayersMissionCount++;
                    playerMission.AllPlayerMissionScoreUpdate();
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

                if (args.interactableObject.transform.TryGetComponent(out FlashLight _flash))
                {
                    GameDB.Instance.myFlashLight = _flash;
                }

                MissionClearCheck();
            }
        }

        //private void HoverEvent(HoverEnterEventArgs args)
        //{
        //    if(args.interactableObject.transform.TryGetComponent(out HingeJoint _hingeJoint))
        //    {
        //        this.useForceGrab = false;
        //    }
        //}

        //private void HoverEventExit(HoverExitEventArgs args)
        //{
        //    if (args.interactableObject.transform.TryGetComponent(out HingeJoint _hingeJoint))
        //    {
        //        this.useForceGrab = true;
        //    }
        //}
        private void MissionClearCheck()
        {
            playerMission = GameDB.Instance.playerMission;
            //�� Ŭ���̾�Ʈ �÷��̾ �����ڰ� �ƴϰ�, �Ϲ� �̼� Ŭ������ ������ �Ϲ� �̼� �迭�� ���̿� ���ų� ������ ����
            if (playerMission.isChaser == false &&
                playerMission.runnerMissionClearCount >= playerMission.playerMissionArray.Length)
            {
                //TODO �̼� Ŭ���� ����
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