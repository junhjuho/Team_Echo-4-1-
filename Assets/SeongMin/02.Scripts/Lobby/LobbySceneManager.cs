using NHR;
using Photon.Pun;
using SeongMin;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SeongMin
{
    public class LobbySceneManager : MonoBehaviour
    {
        [Header("�κ� ������ ��ġ ����Ʈ")]
        public List<Transform> lobbyItemPositionList;
        [Header("�κ� ������ ����Ʈ")]
        public List<GameObject> lobbyItemList;
        [Header("�÷��̾� ���� ��ġ ����Ʈ")]
        public List<Transform> playerSpawnPointList;
        public PhotonView photonView;
        public bool isLobbySetting = false;

        [Header("�÷��̾�")]
        public PlayerMission playerMission;
        public PlayerController playerController;

        private void Awake()
        {
            photonView = GetComponent<PhotonView>();
            GameManager.Instance.lobbySceneManager = this;
        }
        private IEnumerator Start()
        {
            yield return new WaitUntil(() => isLobbySetting);
            if (PhotonNetwork.IsMasterClient)
                ObjectSetting();
        }

        private void ObjectSetting()
        {
            if (lobbyItemList.Count <= lobbyItemPositionList.Count)
            {
                GameDB.Instance.Shuffle(lobbyItemPositionList);
                for (int i = 0; i < lobbyItemList.Count; i++)
                {
                    lobbyItemList[i].transform.position = lobbyItemPositionList[i].position;
                }
            }
            else
            {
                print("�����ؾ��� ������Ʈ ���� ���� ������ ��ġ���� �����ϴ�. ������ġ�� �߰����ּ���.");
            }
        }

        //[PunRPC]
        //protected void InitPlayerSetting()
        //{
        //    if (this.playerController != null) this.playerController.Init();
        //}

    }

}