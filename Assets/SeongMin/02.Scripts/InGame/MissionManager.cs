using Photon.Pun;
using Photon.Realtime;
using SeongMin;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SeongMin
{
    public class MissionManager : MonoBehaviour
    {
        public PhotonView photonView;
        [Header("�й� �� ������ �̼� ���� ����")]
        public int runnerMissionCount = 3;
        [Header("�й� �� ���� �̼� ���� ����")]
        public int teamPlayMissionCount = 1;
        [Header("�й� �� ������ �̼� ���� ����")]
        public int chaserMissionCount = 3;
        private void Awake()
        {
            photonView = GetComponent<PhotonView>();
            GameManager.Instance.missionManager = this;
        }
        public void MissionSetting()
        {
            GameDB.Instance.Shuffle(GameManager.Instance.inGameMapManager.inGameRunnerItemNumberArray);
            for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
            {
                int _minValue = i*runnerMissionCount;
                int _maxValue = _minValue + runnerMissionCount;
                int[] _intArray = GameManager.Instance.inGameMapManager.inGameRunnerItemNumberArray;
                photonView.RPC("MissionSend", PhotonNetwork.PlayerList[i], _minValue, _maxValue, _intArray);
            }
        }
        [PunRPC]
        public void MissionSend(int _minValue, int _maxValue, int[] _intArray)
        {
            // �� Ŭ���̾�Ʈ�� �ٸ� Ŭ���̾�Ʈ�� �Ȱ�ġ�� �̼� ���� �ޱ�
            int j = 0;
            for (int i =_minValue; i < _maxValue; i++)
            {
                GameDB.Instance.playerMission.playerMissionArray[j] = 
                    GameManager.Instance.inGameMapManager.inGameRunnerItemList[_intArray[i]];
                //���� �����ڰ� �ƴϸ� �й� ���� �����ڿ� ������ �̴ϸʿ� ���� 
                if(GameDB.Instance.playerMission.playerMissionArray[j].transform.Find(GameDB.Instance.playerMission.playerMissionArray[j].name).TryGetComponent(out ItemObject _itemobject))
                {
                        if(GameDB.Instance.playerMission.isChaser == false)
                    {
                        if (_itemobject.miniMap == null)
                        {
                            Debug.LogError("Not MiniMap");
                        }

                        _itemobject.miniMap.SetActive(true);
                    }

                }
                j++;
            }

            // �������̸� ������ �̼� �ֱ�
            if (GameDB.Instance.playerMission.isChaser == true)
            {
                GameDB.Instance.Shuffle(GameManager.Instance.inGameMapManager.inGameChaserItemList);
                for (int i = 0; i < chaserMissionCount; i++)
                {
                    GameDB.Instance.playerMission.chaserMissionArray[i] =
                        GameManager.Instance.inGameMapManager.inGameChaserItemList[i];
                    //���� �����ڸ� �й� ���� �����ڿ� ������ �̴ϸʿ� ���� 
                    if (GameDB.Instance.playerMission.chaserMissionArray[i].TryGetComponent(out ItemObject _itemObject))
                        _itemObject.miniMap.SetActive(true);
                }
            }
            // �����̼��� �����ؾ��ϸ� �ֱ�
            if (GameDB.Instance.playerMission.isTeamMission == true)
            {
                GameDB.Instance.Shuffle(GameManager.Instance.inGameMapManager.inGameTeamPlayItemList);

                for (int i = 0; i < teamPlayMissionCount; i++)
                {
                    GameDB.Instance.playerMission.playerTeamPlayMissionArray[i] =
                        GameManager.Instance.inGameMapManager.inGameTeamPlayItemList[i];
                }
            }

            EventDispatcher.instance.SendEvent((int)NHR.EventType.eEventType.Get_Mission);

        }
    }

}