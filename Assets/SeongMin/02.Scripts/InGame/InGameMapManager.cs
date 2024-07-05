using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace SeongMin
{
    public class InGameMapManager : MonoBehaviour
    {
        public PhotonView photonView;
        [Header("�÷��̾� ���� ��ġ ����Ʈ")]
        public List<Transform> playerSpawnPositionList;
        [Header("�Ϲ� ������ ���� ��ġ ����Ʈ")]
        public List<Transform> inGameItemPositionList;
        [Header("�Ϲ� ������ ����Ʈ")]
        public List<GameObject> inGameRunnerItemList;
        [Header("�Ϲ� ������ �ѹ��� ���� �迭")]
        public int[] inGameRunnerItemNumberArray;
        [Header("������ ������ ����Ʈ")]
        public List<GameObject> inGameChaserItemList;
        [Header("�����̼� ������ ����Ʈ")]
        public List<GameObject> inGameTeamPlayItemList;

        private void Awake()
        {
            GameManager.Instance.inGameMapManager = this;
            photonView = GetComponent<PhotonView>();

            //�ӽ�
            NHR.DataManager.Instance.LoadEventDialogData();
        }
        //inGameRunnerItemList�� ���̸�ŭ ���ڸ� ����Ʈ�� �ֱ�
        //�� �÷��̾ inGameRunnerItemList �� �ڵ����� ������ ��������
        //�ƹ����� �� Ŭ���̾�Ʈ�� ����Ʈ�� �ٲ� �� �־ ������ �󿡼� �ִ°� ���ƺ���
        //�̼� ���ýÿ� ����ȭ ��� ���� ����.
        public void ItemNumberListSetting()
        {
            inGameRunnerItemNumberArray = new int[inGameRunnerItemList.Count];
            for (int i = 0; i < inGameRunnerItemList.Count; i++)
            {
                inGameRunnerItemNumberArray[i] = i;
            }
        }
        public void ItemPositionSetting()
        {
            GameDB.Instance.Shuffle(inGameItemPositionList);
            if (inGameRunnerItemList.Count + inGameChaserItemList.Count + inGameTeamPlayItemList.Count <= inGameItemPositionList.Count)
            {
                int positionIndex = 0;

                // inGameRunnerItemList�� ������ ��ġ ����
                for (int i = 0; i < inGameRunnerItemList.Count; i++)
                {
                    inGameRunnerItemList[i].transform.position = inGameItemPositionList[positionIndex].position;
                    positionIndex++;
                }

                // inGameChaserItemList�� ������ ��ġ ����
                for (int i = 0; i < inGameChaserItemList.Count; i++)
                {
                    inGameChaserItemList[i].transform.position = inGameItemPositionList[positionIndex].position;
                    positionIndex++;
                }

                // inGameTeamPlayItemList�� ������ ��ġ ����
                for (int i = 0; i < inGameTeamPlayItemList.Count; i++)
                {
                    inGameTeamPlayItemList[i].transform.position = inGameItemPositionList[positionIndex].position;
                    positionIndex++;
                }
            }
            else
            {
                print("�����ؾ��� ������Ʈ ���� ���� ������ ��ġ���� �����ϴ�. ������ġ�� �߰����ּ���.");
            }
        }
        public void PlayerPositionSetting()
        {
            GameDB.Instance.Shuffle(GameManager.Instance.inGameMapManager.playerSpawnPositionList);
            int i = 0;
            foreach(Player player in PhotonNetwork.PlayerList)
            {
                Vector3 _playerPosition = GameManager.Instance.inGameMapManager.playerSpawnPositionList[i].position;
                photonView.RPC("GetPlayerPosition", player, _playerPosition);
                i++;
            }
        }

        [PunRPC]
        public void GetPlayerPosition(Vector3 _playerPosition)
        {
            GameDB.Instance.myPlayer.transform.position = _playerPosition;
        }
    }
}

